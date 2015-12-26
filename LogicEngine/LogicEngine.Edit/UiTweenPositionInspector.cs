//======================================================
// Create by @Peng Guang Hui
// 2015/6/23 19:15:59
//======================================================
using UnityEngine;
using UnityEditor;
using LogicEngine.Unity;
using System;

namespace LogicEngine.Edit
{
    [CustomEditor(typeof(UiTweenPosition))]
    public class UiTweenPositionInspector : UiTweenInspector
    {
        public override void OnInspectorGUI()
        {
            GUI.changed = false;
            EditorGUIUtility.labelWidth = 60f;

            UiTweenPosition tween = (UiTweenPosition)target;
            tween.show = UtilEditor.FFFFF("show", tween.show, () => { return (tween.transform as RectTransform).anchoredPosition; });
            tween.hide = UtilEditor.FFFFF("hide", tween.hide, () => { return (tween.transform as RectTransform).anchoredPosition; });

            if (GUI.changed)
            {
                EditorUtility.SetDirty(tween);
            }
            DrawCommonProperties();
        }
    }
}