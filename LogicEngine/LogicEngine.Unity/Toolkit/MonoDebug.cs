//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicEngine.Unity
{
    class MonoDebug : MonoBehaviour
    {
        class DebugBtn
        {
            public string name;
            public Action fun;
            public DebugBtn(string name, Action fun)
            {
                this.name = name;
                this.fun = fun;
            }
        }
        class DebugLabel
        {
            public string name;
            public Func<string> fun;
            public int lines;

            public DebugLabel(string name, Func<string> fun, int lines)
            {
                this.name = name;
                this.fun = fun;
                this.lines = lines;
            }
        }

        public bool enableDebug;
        List<DebugBtn> mDebugBtns = new List<DebugBtn>();
        List<DebugLabel> mDebugLabels = new List<DebugLabel>();
        GUIStyle mGuiStyle = new GUIStyle();

        void Awake()
        {
            mGuiStyle.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            mGuiStyle.fontSize = 30;
            InitScreenshot();
            InitFPS();
        }
        void Update()
        {
            UpdateFPS();
            if (Input.GetKeyDown(KeyCode.F1))
            {
                enableDebug = !enableDebug;
            }
        }
        public void AddDebugBtn(string name, Action fun)
        {
            mDebugBtns.Add(new DebugBtn(name, fun));
        }
        public void AddDebugLabel(string name, Func<string> fun, int lines = 1)
        {
            mDebugLabels.Add(new DebugLabel(name, fun, lines));
        }
        void OnGUI()
        {
            if (enableDebug)
            {
                float element_width = 100f;
                float element_height = 64f;
                float max_height = Screen.height - element_height;
                Vector2f tile = new Vector2f(Screen.width - element_width, 0);
                for (int i = 0; i < mDebugBtns.Count; i++)
                {
                    var btn = mDebugBtns[i];
                    if (GUI.Button(new Rect(tile.x, tile.y, element_width, element_height), btn.name))
                    {
                        btn.fun();
                    }

                    tile.y += element_height;
                    if (tile.y > max_height)
                    {
                        tile.x -= element_width;
                        tile.y = 0;
                    }
                }
                tile = Vector2f.zero;
                element_height = 25f;
                max_height = Screen.height - element_height;
                float line_height = 0;
                for (int i = 0; i < mDebugLabels.Count; i++)
                {
                    var label = mDebugLabels[i];
                    GUI.Label(new Rect(tile.x, tile.y, element_width, element_height * label.lines), label.name + " : " + label.fun());
                    //GUI.Label(new Rect(tile.x, tile.y + element_height, element_width, line_height), "-----");
                    tile.y += element_height + line_height;

                    if (tile.y > max_height)
                    {
                        tile.x += element_width;
                        tile.y = 0;
                    } 
                }
            }
        }
        #region 截屏
        string mScreenshotDirectoryName = "../Screenshots/";
        void InitScreenshot()
        {
            mScreenshotDirectoryName = Application.dataPath + "/../Screenshots/";
            if (!System.IO.Directory.Exists(mScreenshotDirectoryName))
            {
                System.IO.Directory.CreateDirectory(mScreenshotDirectoryName);
            }
            AddDebugBtn("截屏", () => StartCoroutine(CaptureScreenshot()));
            AddDebugBtn("截屏main", () => StartCoroutine(CaptureScreenshot(Camera.main, new Rect(0, 0, Screen.width, Screen.height))));
        }
        string GetScreenshotFileName()
        {
            var now = DateTime.UtcNow;
            return mScreenshotDirectoryName + now.Year + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Minute + "_" + now.Second + "_" + now.Millisecond + ".png";
        }
        IEnumerator CaptureScreenshot()
        {
            var enable = enableDebug;
            enableDebug = false;
            yield return new WaitForEndOfFrame();
            Application.CaptureScreenshot(GetScreenshotFileName());
            enableDebug = enable;
        }
        IEnumerator CaptureScreenshot(Rect rect)
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();
            System.IO.File.WriteAllBytes(GetScreenshotFileName(), texture.EncodeToPNG());
            GameObject.Destroy(texture);
        }
        IEnumerator CaptureScreenshot(Camera camera, Rect rect)
        {
            yield return new WaitForEndOfFrame();
            RenderTexture render_texture = RenderTexture.GetTemporary((int)rect.width, (int)rect.height);
            camera.targetTexture = render_texture;
            camera.Render();

            RenderTexture.active = render_texture;
            Texture2D texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();

            camera.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(render_texture);

            System.IO.File.WriteAllBytes(GetScreenshotFileName(), texture.EncodeToPNG());
            GameObject.Destroy(texture);
        }
        #endregion
        #region FPS
        const float mFPSDeltaTime = 0.1f;
        float mFPSLastTime;
        float mFrameCounter;
        float mFPS;
        void InitFPS()
        {
            AddDebugLabel("FPS", () => mFPS.ToString("F2"));
        }
        void UpdateFPS()
        {
            mFrameCounter++;
            if (Time.realtimeSinceStartup - mFPSLastTime >= mFPSDeltaTime)
            {
                mFPS = mFrameCounter / (Time.realtimeSinceStartup - mFPSLastTime);
                mFPSLastTime = Time.realtimeSinceStartup;
                mFrameCounter = 0;
            }
        }
        #endregion
    }
}