//======================================================
// Create by @Peng Guang Hui
// 2015/12/16 11:54:00
//======================================================
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LogicEngine.Unity
{
    public static class UtilScreen
    {
        public static Vector2 GetScreenPosition(Vector2 local)
        {
            return new Vector2(Screen.width * local.x, Screen.height* local.y);
        }
        public static Vector2 GetScreenPosition(Vector2f local)
        {
            return new Vector2(Screen.width * local.x, Screen.height * local.y);
        }
        public static Vector2 GetScreenCenter()
        {
            return GetScreenPosition(Vector2f.half);
        }
    }
}