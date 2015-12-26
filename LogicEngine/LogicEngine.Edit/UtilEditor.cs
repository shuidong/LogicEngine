//======================================================
// Create by @Peng Guang Hui
// 2015/7/18 13:05:43
//======================================================
using UnityEngine;
using UnityEditor;
using LogicEngine.Unity;

namespace LogicEngine.Edit
{
    public static class UtilEditor
    {
        public static void Popup(PopupStrings strings, string name)
        {
            strings.index.Value = EditorGUILayout.Popup(name, strings.index.Value, strings.strings);
        }

        public static void RegisterUndo(string name, params Object[] objects)
        {
            if (objects != null && objects.Length > 0)
            {
                Undo.RecordObjects(objects, name);
                foreach (Object obj in objects)
                {
                    if (obj == null) continue;
                    EditorUtility.SetDirty(obj);
                }
            }
        }

        public static  bool DrawHeader(string key, string text, bool forceOn)
        {
            bool state = EditorPrefs.GetBool(key, true);

            GUILayout.Space(3f);
            if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
            GUILayout.BeginHorizontal();
            GUILayout.Space(3f);

            GUI.changed = false;

            text = "<b><size=11>" + text + "</size></b>";
            if (state) text = "\u25B2 " + text;
            else text = "\u25BC " + text;
            if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
            if (GUI.changed) EditorPrefs.SetBool(key, state);

            GUILayout.Space(2f);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
            if (!forceOn && !state) GUILayout.Space(3f);
            return state;
        }

        public static  void BeginContents()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(4f);
            EditorGUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(10f));
            GUILayout.BeginVertical();
            GUILayout.Space(2f);
        }

        public static  void EndContents()
        {
            GUILayout.Space(3f);
            GUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(3f);
            GUILayout.EndHorizontal();
            GUILayout.Space(3f);
        }

        public static Vector2 FFFFF(string name, Vector2 value, System.Func<Vector2> fun)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(name, GUILayout.MinWidth(20f));
            value = EditorGUILayout.Vector2Field("", value, GUILayout.MinWidth(60f));
            if (GUILayout.Button("x", GUILayout.Width(20f)))
            {
                value = fun();
            }
            GUILayout.EndHorizontal();
            return value;
        }

        public static float Slider(string name, float value, float default_value)
        {
            GUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField(name, GUILayout.MinWidth(20f));
            value = EditorGUILayout.FloatField(name, value, GUILayout.MinWidth(60f));
            if (GUILayout.Button("x", GUILayout.Width(20f)))
            {
                value = default_value;
            }
            GUILayout.EndHorizontal();
            return value;
        }
        public static Vector3 Vector3(string name, Vector3 value, Vector3 default_value)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(name, GUILayout.MinWidth(20f));
            value = EditorGUILayout.Vector3Field("", value, GUILayout.MinWidth(60f));
            if (GUILayout.Button("x", GUILayout.Width(20f)))
            {
                value = default_value;
            }
            GUILayout.EndHorizontal();
            return value;
        }
    }
}