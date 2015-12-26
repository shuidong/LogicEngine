//======================================================
// Create by @Peng Guang Hui
// 2015/9/15 15:57:01
//======================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using LogicEngine.Unity;

namespace LogicEngine.Edit
{
    [CustomEditor(typeof(Text_i18n), true)]
    [CanEditMultipleObjects]
    public class Text_i18nEditor : UnityEditor.UI.TextEditor
    {
        SerializedProperty m_KeyString;

        protected override void OnEnable()
        {
            base.OnEnable();
            //m_KeyString = serializedObject.FindProperty("m_KeyString");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //EditorGUILayout.PropertyField(m_KeyString);
            AppearanceControlsGUI();
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}