//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;

namespace LogicEngine.Unity
{
    public class LogicAdapter : Logic.IAdapter
    {
        byte[] Logic.IAdapter.LoadByte(string file_name, string suffix)
        {
            return UtilResource.LoadText(file_name).bytes;
        }
        string Logic.IAdapter.LoadText(string file_name, string suffix)
        {
            return UtilResource.LoadText(file_name).text;
        }
        void Logic.IAdapter.Log(string log)
        {
            Debug.Log(log);
        }
        void Logic.IAdapter.LogWarning(string log)
        {
            Debug.LogWarning(log);
        }
        void Logic.IAdapter.LogError(string log)
        {
            Debug.LogError(log);
        }
        Logic.ISceneLoader Logic.IAdapter.NewLoader()
        {
            return new SceneLoader();
        }
    }
}