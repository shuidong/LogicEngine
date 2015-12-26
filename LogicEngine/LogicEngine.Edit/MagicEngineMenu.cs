//======================================================
// Create by @Peng Guang Hui
// 2015/7/7 15:33:58
//======================================================
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LogicEngine.Edit
{
    static class LogicEngineMenu
    {
        #region Project
        [MenuItem("LogicEngine/Project/Init Empty Project")]
        static void InitEmptyProject()
        {
            var directories = Directory.GetDirectories(Application.dataPath);
            if (directories.Length == 1 && Path.GetFileNameWithoutExtension(directories[0]) == "Plugins")
            {
                Directory.CreateDirectory(Application.dataPath + "/Arts");

                Directory.CreateDirectory(Application.dataPath + "/Codes");
                Directory.CreateDirectory(Application.dataPath + "/Codes/Plays");
                Directory.CreateDirectory(Application.dataPath + "/Codes/Shaders");
                Directory.CreateDirectory(Application.dataPath + "/Codes/Ui");

                Directory.CreateDirectory(Application.dataPath + "/Resources");
                Directory.CreateDirectory(Application.dataPath + "/Resources/Cfgs");
                Directory.CreateDirectory(Application.dataPath + "/Resources/Icons");
                Directory.CreateDirectory(Application.dataPath + "/Resources/Prefabs");
                Directory.CreateDirectory(Application.dataPath + "/Resources/Prefabs/Ui");

                Directory.CreateDirectory(Application.dataPath + "/Scenes");
            }
            else
            {
                Debug.LogWarning("不是空项目无法初始化.");
            }
        }

        [MenuItem("LogicEngine/Project/Sync Plugins")]
        static void SyncPlugins()
        {
            string source_path = Application.dataPath + UtilPath.Normalize(Cfg.Get("SyncPlugins").GetString("Logic_path", "/../../../LogicEngine/Plugins/"));
            string target_path = Application.dataPath + "/Plugins/";

            CopyPlugin(source_path, target_path, "LogicEngine");
            CopyPlugin(source_path, target_path, "LogicEngine.Unity");
            CopyPlugin(source_path, target_path + "Editor/", "LogicEngine.UnityEditor");

            var plays_path = UtilPath.Combine(Application.dataPath, Cfg.Get("SyncPlugins").GetString("plays_path", null));
            var plays_name = Cfg.Get("SyncPlugins").GetString("plays_name", null);
            if (!string.IsNullOrEmpty(plays_path) && !string.IsNullOrEmpty(plays_name))
            {
                CopyPlugin(plays_path, target_path, plays_name);
            }

            Debug.LogWarning("同步Plugins成功.");
        }
        static void CopyPlugin(string source_path, string target_path, string dll_name)
        {
            CopyFile(source_path, target_path, dll_name + ".dll");
            CopyFile(source_path, target_path, dll_name + ".pdb");
            CopyFile(source_path, target_path, dll_name + ".XML");
        }
        static void CopyFile(string source_path, string target_path, string file_name)
        {
            if (File.Exists(source_path + file_name))
            {
                File.Copy(source_path + file_name, target_path + file_name, true);
            }
            else
            {
                UtilLog.LogWarning("不存在文件 ： " + source_path + file_name);
            }
        }
        static LogicEngineMenu()
        {
            //Logic.Init(EditLibraryAdapter.Instance);
            //Cfg = UtilCfg.ReadPListByFileName(Application.dataPath + "/Plugins/Editor/CfgEditor");
        }
        static PlistSet Cfg;
        #endregion
        #region GameObject
        /// <summary>
        /// 创建一个MiniMap
        /// </summary>
        [MenuItem("LogicEngine/GameObject/Create MiniMap")]
        static void CreateMiniMap()
        {
            var root = new GameObject("MiniMap");
            var camera = new GameObject("Camera").AddComponent<Camera>();
            camera.transform.SetParent(root.transform);
        }
        #endregion
        #region Tween
        [MenuItem("LogicEngine/Tween/Tween Position")]
        static void TweenPosition()
        {
            if (Selection.activeGameObject != null) Selection.activeGameObject.AddComponent<Unity.TweenPosition>();
        }
        #endregion

        static void Cleanup(Transform transform)
        {
            var components = transform.gameObject.GetComponentsInChildren<Component>();

            // Create a serialized object so that we can edit the component list
            var serializedObject = new SerializedObject(transform.gameObject);
            // Find the component list property
            var prop = serializedObject.FindProperty("m_Component");

            // Track how many components we've removed
            int r = 0;

            // Iterate over all components
            for (int j = 0; j < components.Length; j++)
            {
                // Check if the ref is null
                if (components[j] == null)
                {
                    // If so, remove from the serialized component array
                    prop.DeleteArrayElementAtIndex(j - r);
                    // Increment removed count
                    r++;
                }
            }

            // Apply our changes to the game object
            serializedObject.ApplyModifiedProperties();
            //这一行一定要加！！！
            EditorUtility.SetDirty(transform.gameObject);

            foreach (Transform child in transform)
            {
                Cleanup(transform);
            }
        }

        [MenuItem("LogicEngine/EditPrafab/Cleanup Missing Scripts")]
        static void CleanupMissingScripts()
        {
            foreach (Transform t in Selection.transforms)
            {
                Cleanup(t);  
            }
            return;

            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                var gameObject = Selection.gameObjects[i];

                // We must use the GetComponents array to actually detect missing components
                var components = gameObject.GetComponentsInChildren<Component>();

                // Create a serialized object so that we can edit the component list
                var serializedObject = new SerializedObject(gameObject);
                // Find the component list property
                var prop = serializedObject.FindProperty("m_Component");

                // Track how many components we've removed
                int r = 0;

                // Iterate over all components
                for (int j = 0; j < components.Length; j++)
                {
                    // Check if the ref is null
                    if (components[j] == null)
                    {
                        // If so, remove from the serialized component array
                        prop.DeleteArrayElementAtIndex(j - r);
                        // Increment removed count
                        r++;
                    }
                }

                // Apply our changes to the game object
                serializedObject.ApplyModifiedProperties();
                //这一行一定要加！！！
                EditorUtility.SetDirty(gameObject);
            }
        }
    }
}