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
    public static class Logic
    {
        internal static IAdapter Adapter { get; private set; }
        static ICollection<Action<float>> mUpdate = new MutableCollection<Action<float>>();
        static Entity root;

        static Logic()
        {
            root = new Entity();
        }

        public static void Init(IAdapter adapter)
        {
            Adapter = adapter;
            //Entity.root.AddChild().AddPart<SystemService>();
        }
        public static Entity AddChild()
        {
            return root.AddChild();
        }
        public static void Release()
        {
            Entity.ReleaseALL(root);
        }

        public static void Update(float secs)
        {
            foreach (var it in mUpdate)
            {
                it(secs);
            }
        }
        public static void AddUpdate(Action<float> update)
        {
            mUpdate.Add(update);
        }
        public static void RemoveUpdate(Action<float> update)
        {
            mUpdate.Remove(update);
        }

        public interface IAdapter
        {
            byte[] LoadByte(string file_name, string suffix);
            string LoadText(string file_name, string suffix);
            void Log(string log);
            void LogWarning(string log);
            void LogError(string log);
            ISceneLoader NewLoader();
        }

        public interface ISceneLoader
        {
            void Run(IEnumerator coroutine);
            void Stop();
        }
    }
}
