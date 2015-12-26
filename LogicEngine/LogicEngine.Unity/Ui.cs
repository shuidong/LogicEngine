//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LogicEngine.Unity
{
    public abstract class Ui : Subject
    {
        public bool inputEnabled { get { return fsm.IsState<StShow>(); } }
        public bool isShow { get { return fsm.IsState<StShow>(); } }

        public void Show()
        {
            //SendMessage("OnShow");
            if (fsm.IsState<StHiding>())
            {
                //fsm.Switch<StHide>();
                fsm.Switch<StShowing>();
            }
            else if (fsm.IsState<StHide>())
            {
                fsm.Switch<StShowing>();
            }
        }
        public void Hide()
        {
            if (fsm.IsState<StShowing>() || fsm.IsState<StShow>())
            {
                fsm.Switch<StHiding>();
                //mService.Pop();
            }
        }
        public void Release()
        {
            Logic.RemoveUpdate(Update);
            GameObject.Destroy(prefab);
        }
        public void SetLayer(UiLayer layer)
        {
            SetParent(UtilUi.GetLayer(layer));
        }
        void Update(float elapsed_sec) { fsm.Update(elapsed_sec); }

        protected void Listen(Button button, Action action)
        {
            if (action == null) return;
            button.onClick.AddListener(
                () =>
                {
                    if (inputEnabled)
                    {
                        action();
                    }
                });
        }
        protected void Listen(Image image, Action action)
        {
            if (action == null) return;
            UtilGameObject.GetOrAddComponent<UiListener>(image.gameObject).OnClick +=
                () =>
                {
                    if (inputEnabled)
                    {
                        action();
                    }
                };
        }
        class UiListener : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
        {
            public Action OnClick;

            void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
            {
                if (OnClick == null) return;
                OnClick();
            }
        }

        #region internal
        GameObject prefab;
        UiTween[] tweens;
        Fsm<Ui> fsm;

        internal void Init(GameObject prefab)
        {
            this.prefab = prefab;
            tweens = prefab.GetComponentsInChildren<UiTween>(true);
            fsm = new Fsm<Ui>(this);
            _Init0(prefab);
            _Init();
            fsm.Switch<StHide>();

            Logic.AddUpdate(Update);
        }
        internal virtual void _Init0(GameObject prefab) { }
        protected abstract void _Init();


        internal void SetParent(RectTransform parent)
        {
            prefab.transform.SetParent(parent);
            prefab.transform.SetAsStretch();
        }
        void ForeachTween(Action<UiTween> fun, params ToggleStyle[] moments)
        {
            for (int index = 0; index < tweens.Length; ++index)
            {
                for (int index_moments = 0; index_moments < moments.Length; ++index_moments)
                {
                    if (tweens[index].toggleStyle == moments[index_moments])
                    {
                        fun(tweens[index]);
                    }
                }
            }
        }
        #region states
        class StShowing : Fsm<Ui>.State
        {
            int mCount;

            protected override void _Enter()
            {
                Holder.prefab.SetActive(true);
                Holder.prefab.transform.SetAsLastSibling();
                mCount = 0;
                Holder.ForeachTween(Move, ToggleStyle.Show, ToggleStyle.ShowOrHide);
                Holder.ForeachTween(Jump, ToggleStyle.Hide);
                //Holder.State.Value = UiState.Showing;
            }
            protected override void _Update(float elapsed_sec)
            {
                if (mCount <= 0)
                {
                    Switch<StShow>();
                }
            }
            protected override void _Exit()
            {
                Holder.ForeachTween(Reset, ToggleStyle.Show, ToggleStyle.ShowOrHide);
            }

            void Move(UiTween tween)
            {
                ++mCount;
                tween.OnFinished += _Finished;
                tween.PalyBackward();
            }

            void Jump(UiTween tween)
            {
                tween.Reset(true);
            }

            void Reset(UiTween tween)
            {
                tween.OnFinished -= _Finished;
            }

            void _Finished()
            {
                --mCount;
            }
        }
        class StShow : Fsm<Ui>.State
        {
            protected override void _Enter()
            {
                //Holder.State.Value = UiState.Show;
            }
            protected override void _Update(float elapsed_sec)
            {
            }
            protected override void _Exit()
            {
            }
        }
        class StHiding : Fsm<Ui>.State
        {
            int mTweenCount;

            protected override void _Enter()
            {
                mTweenCount = 0;
                Holder.ForeachTween(Move, ToggleStyle.Hide, ToggleStyle.ShowOrHide);
                //Holder.State.Value = UiState.Hiding;
            }
            protected override void _Update(float elapsed_sec)
            {
                if (mTweenCount <= 0)
                {
                    Switch<StHide>();
                }
            }
            protected override void _Exit()
            {
                Holder.ForeachTween(Reset, ToggleStyle.Hide, ToggleStyle.ShowOrHide);
            }

            void Move(UiTween tween)
            {
                ++mTweenCount;
                tween.OnFinished += _Finished;
                tween.PalyForward();
            }
            void Reset(UiTween tween)
            {
                tween.OnFinished -= _Finished;
            }
            void _Finished()
            {
                --mTweenCount;
            }
        }
        class StHide : Fsm<Ui>.State
        {
            protected override void _Enter()
            {
                Holder.prefab.SetActive(false);
                Holder.ForeachTween(jump, ToggleStyle.Show, ToggleStyle.Hide, ToggleStyle.ShowOrHide);
                //Holder.State.Value = UiState.Hide;
            }
            protected override void _Update(float elapsed_sec)
            {
            }
            protected override void _Exit()
            {
            }
            void jump(UiTween tween)
            {
                tween.Reset(false);
            }
        }
        class StPushing : Fsm<Ui>.State
        {
            int mTweenCount;

            protected override void _Enter()
            {
                mTweenCount = 0;
                Holder.ForeachTween(Move, ToggleStyle.PushOrPop);
                //Holder.State.Value = UiState.Hiding;
            }
            protected override void _Update(float elapsed_sec)
            {
                if (mTweenCount <= 0)
                {
                    Switch<StPush>();
                }
            }
            protected override void _Exit()
            {
                Holder.ForeachTween(Reset, ToggleStyle.PushOrPop);
            }

            void Move(UiTween tween)
            {
                ++mTweenCount;
                tween.OnFinished += _Finished;
                tween.PalyForward();
            }
            void Reset(UiTween tween)
            {
                tween.OnFinished -= _Finished;
            }
            void _Finished()
            {
                --mTweenCount;
            }
        }
        class StPush : Fsm<Ui>.State
        {
            protected override void _Enter()
            {
                //Holder.State.Value = UiState.Hide;
            }
            protected override void _Update(float elapsed_sec)
            {
            }
            protected override void _Exit()
            {
            }
        }
        class StPoping : Fsm<Ui>.State
        {
            int mCount;

            protected override void _Enter()
            {
                Holder.prefab.SetActive(true);
                Holder.prefab.transform.SetAsLastSibling();
                mCount = 0;
                Holder.ForeachTween(Move, ToggleStyle.PushOrPop);
                //Status.ForeachTween(Jump, UiMoment.Hide);
                //Holder.State.Value = UiState.Showing;
            }
            protected override void _Update(float elapsed_sec)
            {
                if (mCount <= 0)
                {
                    Switch<StShow>();
                }
            }
            protected override void _Exit()
            {
                Holder.ForeachTween(Reset, ToggleStyle.PushOrPop);
            }

            void Move(UiTween tween)
            {
                ++mCount;
                tween.OnFinished += _Finished;
                tween.PalyBackward();
            }

            void Jump(UiTween tween)
            {
                tween.Reset(true);
            }

            void Reset(UiTween tween)
            {
                tween.OnFinished -= _Finished;
            }

            void _Finished()
            {
                --mCount;
            }
        }
        #endregion
        #endregion
    }

    /// <summary>
    /// Ui链接Ui控件和Ui逻辑代码的脚本
    /// </summary>
    public abstract class Refer : Recyclable
    {
        internal bool withBackground;
    }
    /// <summary>
    /// 2D UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Ui<T> : Ui where T : Refer
    {
        protected T Refer { get; private set; }

        internal override void _Init0(GameObject prefab)
        {
            Refer = prefab.GetComponent<T>();
        }
    }
    /// <summary>
    /// 3D UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Ui3 : Ui
    {
    }
    public abstract class Ui3<T> : Ui3 where T : Refer
    {
        protected T Refer { get; private set; }

        internal override void _Init0(GameObject prefab)
        {
            Refer = prefab.GetComponent<T>();
        }
    }
}