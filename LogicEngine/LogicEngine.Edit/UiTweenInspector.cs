//======================================================
// Create by @Peng Guang Hui
// 2015/6/23 19:15:59
//======================================================
using UnityEngine;
using UnityEditor;
using LogicEngine.Unity;

namespace LogicEngine.Edit
{
   [CustomEditor(typeof(UiTween), true)]
    public abstract class UiTweenInspector : Editor
    {
        protected void DrawCommonProperties()
        {
            UiTween tween = target as UiTween;
            if (UtilEditor.DrawHeader("UiTween", "UiTween", false))
            {
                UtilEditor.BeginContents();
                EditorGUIUtility.labelWidth = 110f;

                GUI.changed = false;

                tween.toggleStyle = (ToggleStyle)EditorGUILayout.EnumPopup("Toggle Style", tween.toggleStyle);
                tween.wrapMode = (UiTween.WrapMode)EditorGUILayout.EnumPopup("Wrap Mode", tween.wrapMode);

                tween.customCurve = EditorGUILayout.Toggle("Custom Curve", tween.customCurve);
                if (tween.customCurve)
                {
                    tween.animationCurve =
                        EditorGUILayout.CurveField("Animation Curve", tween.animationCurve, GUILayout.Width(170f), GUILayout.Height(62f));
                }
                else 
                {
                    tween.tweenStyle = (TweenStyle)EditorGUILayout.EnumPopup("Play Style", tween.tweenStyle);
                }

                GUILayout.BeginHorizontal();
                tween.duration = EditorGUILayout.FloatField("Duration", tween.duration, GUILayout.Width(170f));
                GUILayout.Label("seconds");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                tween.delay = EditorGUILayout.FloatField("Start Delay", tween.delay, GUILayout.Width(170f));
                GUILayout.Label("seconds");
                GUILayout.EndHorizontal();

                tween.ignoreTimeScale = EditorGUILayout.Toggle("Ignore TimeScale", tween.ignoreTimeScale);

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(tween);
                }
                UtilEditor.EndContents();
            }
        }
    }
}