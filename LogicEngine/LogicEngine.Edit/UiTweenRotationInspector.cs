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
    [CustomEditor(typeof(UiTweenRotation))]
    public class UiTweenRotationInspector : UiTweenInspector
    {
        public override void OnInspectorGUI()
        {
            GUI.changed = false;
            EditorGUIUtility.labelWidth = 60f;

            UiTweenRotation tween = (UiTweenRotation)target;
            tween.show = UtilEditor.Vector3("show", tween.show, Vector3.zero);
            tween.hide = UtilEditor.Vector3("hide", tween.hide, Vector3.zero);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(tween);
            }
            DrawCommonProperties();
        }
    }
}