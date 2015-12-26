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

namespace LogicEngine
{
    public interface IScene
    {
    }
    interface inIScene
    {
        void AddPart(IScenePart part);
        void RemovPart(IScenePart part);
        void OnShow();
        void OnHide();
    }
    interface IScenePart : Entity.IPart
    {
        //Entity entity { get; }
        void OnShow();
        void OnHide();
    }

    public abstract class Scene<TPart> : Entity.IPart, IScene, inIScene
        where TPart : global::LogicEngine.Part, new()
    {
        protected TPart Part { get; private set; }
        Logic.ISceneLoader mLoader;
        List<IScenePart> mParts = new List<IScenePart>();
        SceneStage mStage = SceneStage.None;

        void Entity.IPart.Awake(Entity entity)
        {
            Part = entity.GetPartInParent<TPart>();
            mLoader = Logic.Adapter.NewLoader();
        }
        void Entity.IPart.Release()
        {
            mStage = SceneStage.Release;
            mLoader.Stop();
            foreach (var it in mParts)
            {
                it.Release();
            }
            mParts.Clear();
        }
        void inIScene.OnShow()
        {
            switch (mStage)
            {
                case SceneStage.None:
                    mLoader.Run(Run());
                    break;
                case SceneStage.Loading:
                    break;
                case SceneStage.Show:
                    break;
                case SceneStage.Hide:
                    foreach (var it in mParts)
                    {
                        it.OnShow();
                    }
                    break;
            }
        }
        void inIScene.OnHide()
        {
            switch (mStage)
            {
                case SceneStage.None:
                case SceneStage.Loading:
                    mStage = SceneStage.None;
                    foreach (var it in mParts)
                    {
                        it.Release();
                    }
                    break;
                case SceneStage.Show:
                    foreach (var it in mParts)
                    {
                        it.OnHide();
                    }
                    break;
                case SceneStage.Hide:
                    break;
            }
        }

        void inIScene.AddPart(IScenePart part)
        {
            mParts.Add(part);
            switch (mStage)
            {
                case SceneStage.None:
                    mParts.Add(part);
                    break;
                case SceneStage.Loading:

                    break;
                case SceneStage.Show:
                    mParts.Add(part);
                    part.OnShow();
                    break;
                case SceneStage.Hide:
                    UtilLog.LogError("Scene is hide, can't add cpart.");
                    break;
            }
        }
        void inIScene.RemovPart(IScenePart part)
        {
            switch (mStage)
            {
                case SceneStage.None:
                    mParts.Remove(part);
                    break;
                case SceneStage.Loading:
                    break;
                case SceneStage.Show:
                    mParts.Remove(part);
                    break;
                case SceneStage.Hide:
                    mParts.Remove(part);
                    break;
            }
        }
        IEnumerator Run()
        {
            yield return _Awake();
            mStage = SceneStage.Loading;
            for (int i = mParts.Count - 1; i >= 0; i--)
            {
                mParts[i].OnShow();
            }
            mStage = SceneStage.Show;
        }
        protected abstract IEnumerator _Awake();
        enum SceneStage
        {
            None,
            Loading,
            Show,
            Hide,
            Release,
        }
    }


    public class SceneMgr : Entity.IPart
    {
        internal inIScene currentScene { get; private set; }
        Entity entity;
        Dictionary<string, IScene> mScenes = new Dictionary<string, IScene>();

        public void Awake(Entity entity)
        {
            this.entity = entity;
        }
        public void Release()
        {
        }
        public void Register<TScene>(string scene_name, Part part)
          where TScene : class, Entity.IPart, IScene, new()
        {
            mScenes.Add(scene_name, part.entity.AddPart<TScene>());
        }
        public void Switch(string scene_name)
        {
            if (currentScene != null)
            {
                currentScene.OnHide();
                //(currentScene as Entity.IPart).
            }
            currentScene = mScenes[scene_name] as inIScene;
            currentScene.OnShow();
        }
    }
}