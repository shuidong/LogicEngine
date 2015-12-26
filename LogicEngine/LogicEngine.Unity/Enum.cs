//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================

namespace LogicEngine.Unity
{
    public enum GameLayer : int
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        UI3D = 8,
    }

    public enum GameTag
    {
        Untagged,
        Respawn,
        Finish,
        EditorOnly,
        MainCamera,
        Player,
        GameController
    }

    public static class GameTag2
    {
        public const string Untagged = "Untagged";
        public const string Respawn = "Respawn";
        public const string Finish = "Finish";
        public const string EditorOnly = "EditorOnly";
        public const string MainCamera = "MainCamera";
        public const string Player = "Player";
        public const string GameController = "GameController";
    }

    public enum UiLayer
    {
        Bg,
        Normal,
        Fg,
        /// <summary>
        /// 过场界面，覆盖所有其他UI
        /// </summary>
        CutScene,

        Debug
    }

    public enum ToggleStyle : int
    {
        ShowOrHide,
        Show,
        Hide,
        PushOrPop,
        Active,
        Auto
    }
}
