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
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace LogicEngine.Unity
{
    public static class UnityEngineExtension
    {
        #region Vector Convert
        public static Vector2 to2(this Vector2i v)
        {
            return new Vector2(v.x, v.y);
        }
        public static Vector2 to2(this Vector2f v)
        {
            return new Vector2(v.x, v.y);
        }
        public static Vector3 to3(this Vector2f v)
        {
            return new Vector3(v.x, 0f, v.y);
        }
        public static Vector3 to3(this Vector2f v, float height)
        {
            return new Vector3(v.x, height, v.y);
        }
        public static Vector3 to3(this Vector3f v)
        {
            return new Vector3(v.x, v.z, v.y);
        }
        public static Vector2f to2(this Vector3 v)
        {
            return new Vector2f(v.x, v.z);
        }
        //public static Vector3f to3(this Vector3 v)
        //{
        //    return new Vector3f(v.x, v.y, v.z);
        //}
        #endregion
        #region Transform
        public static void SetPositionX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        public static void SetPositionY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        public static void SetPositionZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        public static void SetLocalScale(this Transform transform, float scale)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
        /// <summary>
        /// 设置成Stretch模式，子节点可以锚点
        /// </summary>
        /// <param name="transform"></param>
        public static void SetStretch(this RectTransform transform)
        {
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;

            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;

            transform.offsetMin = Vector2.zero;
            transform.offsetMax = Vector2.zero;
        }
        public static void SetAsStretch(this Transform transform)
        {
            var rect_transform = transform as RectTransform;
            if (rect_transform != null)
            {
                SetStretch(rect_transform);
            }
        }

        public static void Print(this RectTransform transform)
        {
            Debug.LogWarning(
                transform.name +
                ":[localPosition:" + transform.localPosition + "]" +
                ":[localRotation:" + transform.localRotation + "]" +
                ":[localScale:" + transform.localScale + "]" +
                ":[position:" + transform.position + "]" +
                ":[rotation:" + transform.rotation + "]" +
                ":[anchoredPosition:" + transform.anchoredPosition + "]" +
                ":[anchoredPosition3D:" + transform.anchoredPosition3D + "]" +
                ":[anchorMax:" + transform.anchorMax + "]" +
                ":[anchorMin:" + transform.anchorMin + "]" +
                ":[offsetMax:" + transform.offsetMax + "]" +
                ":[offsetMin:" + transform.offsetMin + "]" +
                ":[pivot:" + transform.pivot + "]" +
                ":[rect:" + transform.rect + "]" +
                ":[sizeDelta:" + transform.sizeDelta + "]");
        }
        #endregion


        /// <summary>
        /// 用于分帧加载场景视图对象，以免卡帧
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="async"></param>
        /// <param name="fun"></param>
        /// <param name="each_run_count"></param>
        /// <returns></returns>
        public static IEnumerator AsyncRun<T>(this IEnumerable<T> async, Action<T> fun, int each_run_count)
        {
            int count = 0;
            foreach (var it in async)
            {
                fun(it);
                count++;
                if (count > each_run_count)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        public static Vector2i ToTile<T>(this Camera camera, TileMap<T> map, Vector2 screen)
            where T : struct, global::System.IConvertible
        {
            return map.ToTile(camera.Screen2Ground(screen).to2());
        }
        public static Vector3 Screen2Ground(this Camera camera, Vector2 screen)
        {
            Ray ray = camera.ScreenPointToRay(screen);
            return Ray2Ground(ray.origin, ray.direction, 0f);
        }

        static Vector3 Ray2Ground(Vector3 start, Vector3 direction, float y)
        {
            direction.Normalize();
            float v = (y - start.y) / direction.y;
            return new Vector3(start.x + direction.x * v, y, start.z + direction.z * v);
        }
        public static void LoadSprite(this Image image, string file_name)
        {
            image.sprite = UtilResource.LoadSprite(file_name);
        }
        public static void SetStar(this Image[] stars, int star)
        {
            for (int i = 0; i< stars.Length;i++)
            {
                stars[i].gameObject.SetActive(i < star);
            }
        }
        #region UnityEngine
       

        public static void SetParent(this UIBehaviour ui, GameObject go)
        {
            RectTransform parent = go.transform as RectTransform;
            if (parent == null) return;

            ui.transform.SetParent(parent);
            ui.transform.localScale = Vector3.one;
            ui.transform.localPosition = Vector3.zero;
        }

        public static void SetAlpha(this Graphic ui, float a)
        {
            ui.color = new Color(ui.color.r, ui.color.g, ui.color.b, a);
        }
        public static void SetColor(this Graphic ui, Color color)
        {
            ui.color = new Color(color.r, color.g, color.b, ui.color.a);
        }

        public static void SetSprite(this Image image, string path)
        {
            if (image.overrideSprite != null)
            {
                Sprite.Destroy(image.overrideSprite);
            }
            image.overrideSprite = Resources.Load<Sprite>(path);
        }

        public static void CopyFrom(this Text text, Text other)
        {
            text.alignment = other.alignment;
            text.font = other.font;
            text.fontSize = other.fontSize;
            text.fontStyle = other.fontStyle;
            text.horizontalOverflow = other.horizontalOverflow;
            text.lineSpacing = other.lineSpacing;
            text.resizeTextForBestFit = other.resizeTextForBestFit;
            text.resizeTextMaxSize = other.resizeTextMaxSize;
            text.resizeTextMinSize = other.resizeTextMinSize;
            text.supportRichText = other.supportRichText;
            text.text = other.text;
            text.verticalOverflow = other.verticalOverflow;
        }
        #endregion
    }
}