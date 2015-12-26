//======================================================
// Create by @Peng Guang Hui
// 2015/7/28 15:53:49
//======================================================
using UnityEngine;
using UnityEditor;
using LogicEngine.Unity;
using System;

namespace LogicEngine.Edit
{
    [CustomEditor(typeof(UiTweenAlpha))]
    public class UiTweenAlphaInspector : UiTweenInspector
    {
        public override void OnInspectorGUI()
        {
            GUI.changed = false;
            EditorGUIUtility.labelWidth = 60f;

            UiTweenAlpha tween = (UiTweenAlpha)target;
            tween.show = UtilEditor.Slider("show", tween.show, 1f);
            tween.hide = UtilEditor.Slider("hide", tween.hide, 0f);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(tween);
            }
            DrawCommonProperties();
        }
    }
}