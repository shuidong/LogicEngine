//==============================================================================
// Copyright (C) 2015 Peng Guanghui
// All rights reserved
//
// Create by 彭光辉 at 7/19/2015 11:25:45 AM
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;
using UnityEditor;
using LogicEngine.Unity;
using System;

namespace LogicEngine.Edit
{
    [CustomEditor(typeof(UiTweenScale))]
    public class UiTweenScaleInspector : UiTweenInspector
    {
        public override void OnInspectorGUI()
        {
            GUI.changed = false;
            EditorGUIUtility.labelWidth = 60f;

            UiTweenScale tween = (UiTweenScale)target;
            tween.show = UtilEditor.Vector3("show", tween.show, Vector3.one);
            tween.hide = UtilEditor.Vector3("hide", tween.hide, Vector3.zero);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(tween);
            }
            DrawCommonProperties();
        }
    }
}