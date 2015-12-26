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

namespace LogicEngine.Unity
{
    public class ForEdit : MonoBehaviour
    {
#if DEBUG
        static readonly GUIStyle Style = new GUIStyle();
        ICollection<object> mObservers = new MutableCollection<object>();
#endif
        static ForEdit()
        {
            #if DEBUG
            Style.fontSize = 16;
            Style.alignment = TextAnchor.UpperLeft;
            Style.normal.textColor = Color.yellow;
#endif
        }
        public void Attach(object observer)
        {
#if DEBUG
            mObservers.Add(observer);
#endif
        }

        public void Detach(object observer)
        {
#if DEBUG
            mObservers.Remove(observer);
#endif
        }

        protected void SendEditMessage(string name, params object[] args)
        {
#if DEBUG
            UtilMessage.SendMessage(mObservers, name, args);
#endif
        }

        protected Vector2 ClampScreen(Vector2 screen, Vector2 size)
        {
            return new Vector2(Mathf.Clamp(screen.x, 0, Screen.width - size.x), Mathf.Clamp(screen.y, 0, Screen.height - size.y));
        }
        protected void TipString(Vector2 screen, Vector2 size, string tip, Color color)
        {
#if DEBUG
            Style.normal.textColor = color;
            GUI.Label(GetRect(screen, size), tip, Style);
#endif
        }
        //string TipInputField(float x, float y, float size_x, float size_y, string tip)
        //{
        //    return GUI.TextField(GetRect(x, y, size_x, size_y), tip);
        //}
        //bool TipButton(float x, float y, float size_x, float size_y, string tip)
        //{
        //    return GUI.Button(GetRect(x, y, size_x, size_y), tip);
        //}
        Rect GetRect(Vector2 screen, Vector2 size)
        {
            return new Rect(screen.x, Screen.height - screen.y - size.y, size.x, size.y);
        }
    }
}