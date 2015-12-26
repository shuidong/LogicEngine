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
using LogicEngine;

namespace Demos.TetrisGame
{
    class Play : UPart
    {
        Tetris tetris;
        Fsm<Play> fsm;

        protected override void _Awake()
        {
            tetris = entity.AddPart<Tetris>();

            fsm = new Fsm<Play>(this);
            fsm.Switch<StWaitStart>();
        }
        protected override void _Update(float sec)
        {
            fsm.Update(sec);
            //TouchScreen.OnKeyDown(UnityEngine.KeyCode.S, Start);
            //TouchScreen.OnKeyDown(UnityEngine.KeyCode.LeftArrow, OnLeft);
            //TouchScreen.OnKeyDown(UnityEngine.KeyCode.RightArrow, OnRight);
            //TouchScreen.OnKeyDown(UnityEngine.KeyCode.DownArrow, OnDown);
            //TouchScreen.OnKeyDown(UnityEngine.KeyCode.Space, OnRotate);
        }
        protected override void _Release()
        {
        }
        void Start()
        {
            if (fsm.IsState<StWaitStart>())
            {
                fsm.Switch<StDrop>();
            }
        }
        void OnDown()
        {
            MoveTo(MoveDirection.Down);
        }
        void OnLeft()
        {
            MoveTo(MoveDirection.Left);
        }

        void OnRight()
        {
            MoveTo(MoveDirection.Right);
        }
        void OnRotate()
        {
            if (IsGameRunning() && tetris.Current.CanRotate())
            {
                tetris.Current.Rotate();
            }
        }
        void MoveTo(MoveDirection dir)
        {
            if (IsGameRunning() && tetris.Current.CanMoveTo(dir))
            {
                tetris.Current.MoveTo(dir);
            }
        }
        bool IsGameRunning()
        {
            return fsm.IsState<StDrop>();
        }
        List<int> NeedClearCubes()
        {
            List<int> result = new List<int>();
            for (int index_y = 0; index_y < tetris.Tiles.Size.y; ++index_y)
            {
                bool need = true;
                for (int index_x = 0; index_x < tetris.Tiles.Size.x; ++index_x)
                {
                    if (!tetris.Tiles.HasMask(new Vector2i(index_x, index_y), Mask.Block))
                    {
                        need = false;
                        break;
                    }
                }
                if (need)
                {
                    result.Add(index_y);
                }
            }
            return result;
        }
        class StWaitStart : Fsm<Play>.State
        {
            protected override void _Enter()
            {
            }
            protected override void _Update(float elapsed_sec)
            {
            }
            protected override void _Exit()
            {
            }
        }
        class StDrop : Fsm<Play>.State
        {
            float duration;

            protected override void _Enter()
            {
                Holder.tetris.RandomSharp();
                duration = 0f;
            }
            protected override void _Update(float elapsed_sec)
            {
                duration -= elapsed_sec;
                if (duration > 0)
                {
                    return;
                }
                duration = 1f;
                if (Holder.tetris.Current.CanMoveTo(MoveDirection.Down))
                {
                    Holder.tetris.Current.MoveTo(MoveDirection.Down);
                }
                else
                {
                    Switch<StTouchGround>();
                }
            }
            protected override void _Exit()
            {
            }
        }
        class StTouchGround : Fsm<Play>.State
        {
            protected override void _Enter()
            {
                Holder.tetris.Current.Put2Ground();
                UtilLog.LogWarning(UtilString.Join<int>(',', Holder.NeedClearCubes().ToArray()));

            }
            protected override void _Update(float elapsed_sec)
            {
                if (Holder.NeedClearCubes().Count > 0)
                {
                    Switch<StBlink>();
                }
                else
                {
                    Switch<StDrop>();
                }
            }
            protected override void _Exit()
            {
            }
        }
        class StBlink : Fsm<Play>.State
        {
            float duration;
            protected override void _Enter()
            {
                duration = 1f;
            }
            protected override void _Update(float elapsed_sec)
            {
                duration -= elapsed_sec;
                if (duration < 0)
                {

                }
            }
            protected override void _Exit()
            {
            }
        }
    }
}