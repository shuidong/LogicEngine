//======================================================
// Create by @Peng Guang Hui
// 2015/7/9 14:06:42
//======================================================
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace LogicEngine.Edit
{
    sealed class EditLibraryAdapter : Singleton<EditLibraryAdapter>, Logic.IAdapter
    {
        protected override void _Init()
        {
        }
        byte[] Logic.IAdapter.LoadByte(string file_name, string suffix)
        {
            throw new NotImplementedException();
        }
        string Logic.IAdapter.LoadText(string file_name, string suffix)
        {
            using (StreamReader reader = new StreamReader(file_name + "." + suffix))
            {
                return reader.ReadToEnd();
            }
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
            throw new NotImplementedException();
        }
        Logic.ISceneLoader Logic.IAdapter.NewLoader()
        {
            throw new NotImplementedException();
        }
    }
}