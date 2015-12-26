//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicEngine.Unity.Toolkit
{
    [RequireComponent(typeof(Image))]
    public class Sketch : MonoBehaviour
    {
        public int width { get { return mTexture.width; } }
        public int height { get { return mTexture.height; } }
        Image mImage;
        Texture2D mTexture;
        Color[] mColors;
        TextCal mTextCal;

        void Awake()
        {
            mImage = GetComponent<Image>();
            mTexture = new Texture2D(Screen.width, Screen.height);
            mTexture.anisoLevel = 9;
            Resize(Screen.width, Screen.height);

            mTextCal = new TextCal();
            //mTextCal.Pop();
        }
        void OnDestroy()
        {
            //if (sprite != null)
            //{
            //    UnityEngine.Object.Destroy(sprite);
            //}
            UnityEngine.Object.Destroy(mTexture);
        }

        public void Resize(int width, int height)
        {
            if (mImage.sprite != null)
            {
                UnityEngine.Object.Destroy(mImage.sprite);
            }
            mTexture.Resize(width, height);
            mColors = new Color[width * height];
            mImage.sprite = Sprite.Create(mTexture, new Rect(0, 0, mTexture.width, mTexture.height), Vector2.zero);
            mImage.SetNativeSize();
        }
        public void Clear(Color color)
        {
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    mColors[i * height + j] = color;
                    //texture.SetPixel(i, j, color);
                }
            }
            mTexture.SetPixels(mColors, 0);
        }
        public void Apply()
        {
            mTexture.Apply();
        }

        #region draw functions
        public void DrawPoint(int x, int y, Color color)
        {
            mTexture.SetPixel(x, y, color);
        }
        public void DrawPoint(Vector2i point, Color color)
        {
            DrawPoint(point.x, point.y, color);
        }
        public void DrawPoint(Vector2f point, Color color)
        {
            DrawPoint(UtilMath.RoundToInt(point.x), UtilMath.RoundToInt(point.y), color);
        }
        public void DrawLine(int a_x, int a_y, int b_x, int b_y, Color color)
        {
            bool steep = UtilMath.Abs(b_y - a_y) > UtilMath.Abs(b_x - a_x);
            if (steep)
            {
                UtilMath.Swap(ref a_x, ref a_y);
                UtilMath.Swap(ref b_x, ref b_y);
            }
            if (a_x > b_x)
            {
                UtilMath.Swap(ref a_x, ref b_x);
                UtilMath.Swap(ref a_y, ref b_y);
            }
            int deltax = b_x - a_x;
            int deltay = UtilMath.Abs(b_y - a_y);
            int error = deltax / 2;
            int ystep = a_y < b_y ? 1 : -1;
            int y = a_y;

            if (steep)
            {
                for (int x = a_x; x <= b_x; ++x)
                {
                    DrawPoint(y, x, color);
                    error = error - deltay;
                    if (error < 0)
                    {
                        y = y + ystep;
                        error = error + deltax;
                    }
                }
            }
            else
            {
                for (int x = a_x; x <= b_x; ++x)
                {
                    DrawPoint(x, y, color);
                    error = error - deltay;
                    if (error < 0)
                    {
                        y = y + ystep;
                        error = error + deltax;
                    }
                }
            }
        }
        public void DrawLine(Vector2i a, Vector2i b, Color color)
        {
            DrawLine(a.x, a.y, b.x, b.y, color);
        }
        public void DrawLine(Vector2f a, Vector2f b, Color color)
        {
            DrawLine(UtilMath.RoundToInt(a.x), UtilMath.RoundToInt(a.y), UtilMath.RoundToInt(b.x), UtilMath.RoundToInt(b.y), color);
        }
        public void DrawArray(int a_x, int a_y, int b_x, int b_y, float array_size, Color color)
        {
            DrawLine(a_x, a_y, b_x, b_y, color);
            var b = new Vector2f(b_x, b_y);
            var dir = new Vector2f(b_x - a_x, b_y - a_y).normalized * array_size;
            var a0 = (b - UtilGeometry.Rotate(dir, 30f)).RoundToVector2i();
            var a1 = (b - UtilGeometry.Rotate(dir, -30f)).RoundToVector2i();
            DrawLine(a0.x, a0.y, b_x, b_y, color);
            DrawLine(a1.x, a1.y, b_x, b_y, color);
        }
        public void DrawArray(Vector2i a, Vector2i b, float array_size, Color color)
        {
            DrawArray(a.x, a.y, b.x, b.y, array_size, color);
        }
        public void DrawArray(Vector2f a, Vector2f b, float array_size, Color color)
        {
            DrawArray(UtilMath.RoundToInt(a.x), UtilMath.RoundToInt(a.y), UtilMath.RoundToInt(b.x), UtilMath.RoundToInt(b.y), array_size, color);
        }
        public void DrawRect(Physics2D.Rect rect, Color color)
        {
            DrawLine((int)rect.xMin, (int)rect.yMin, (int)rect.xMax, (int)rect.yMin, color);
            DrawLine((int)rect.xMax, (int)rect.yMin, (int)rect.xMax, (int)rect.yMax, color);
            DrawLine((int)rect.xMax, (int)rect.yMax, (int)rect.xMin, (int)rect.yMax, color);
            DrawLine((int)rect.xMin, (int)rect.yMax, (int)rect.xMin, (int)rect.yMin, color);
        }
        public void DrawFlowField(Physics2D.FlowField field, Color color)
        {
            var halfsize = UtilMath.Min(field.TileSize.x, field.TileSize.y) * 0.45f;
            var array_size = halfsize * 0.5f;
            for (int index_x = 0; index_x < field.MapSize.x; ++index_x)
            {
                for (int index_y = 0; index_y < field.MapSize.y; ++index_y)
                {
                    var mid = field.GetPosition(index_x, index_y);
                    var offset = field.GetDir(index_x, index_y) * halfsize;
                    DrawArray(mid - offset, mid + offset, array_size, color);
                }
            }
        }
        public void DrawCellular(Physics2D.Cellular1 cellular, Vector2f center, Vector2f cell_size, Color color, Color linecolor)
        {
            var map_size = new Vector2i(cellular.width, cellular.height);
            var offset =
              new Vector2f(
                  (((int)(map_size.x / 2)) + (map_size.x.IsOdd() ? 0.5f : 0f)) * cell_size.x,
                  (((int)(map_size.y / 2)) + (map_size.y.IsOdd() ? 0.5f : 0f)) * cell_size.y);
            var origin = new Vector2f(center.x - offset.x, center.y - offset.y);

            float y0 = origin.y;
            float y1 = y0 + cell_size.y * map_size.y;
            UtilLambda.Foreach(map_size.x + 1,
                (int x) =>
                {
                    float xx = origin.x + cell_size.x * x;
                    DrawLine(new Vector2f(xx, y0), new Vector2f(xx, y1), linecolor);
                }
                );
            float x0 = origin.x;
            float x1 = x0 + cell_size.x * map_size.x;
            UtilLambda.Foreach(map_size.y + 1,
               (int y) =>
               {
                   float yy = origin.y + cell_size.y * y;
                   DrawLine(new Vector2f(x0, yy), new Vector2f(x1, yy), linecolor);
               }
               );

            float sizex = cell_size.x / 4;
            float sizey = cell_size.y / 4;
            for (int i = 0; i < cellular.width; i++)
            {
                for (int j = 0; j < cellular.height; j++)
                {
                    if (cellular.cells[i][j])
                    {
                        Vector2f ret;
                        ret.x = origin.x + cell_size.x * i + cell_size.x * 0.5f;
                        ret.y = origin.y + cell_size.y * j + cell_size.y * 0.5f;

                        int xx0 = (int)(ret.x - cell_size.x * 0.5f);
                        int xx1 = (int)(ret.x + cell_size.x * 0.5f);
                        int yy0 = (int)(ret.y - cell_size.y * 0.5f);
                        int yy1 = (int)(ret.y + cell_size.y * 0.5f);

                        DrawLine(xx0, (int)(ret.y - sizey), xx1, (int)(ret.y - sizey), color);
                        DrawLine(xx0, (int)(ret.y), xx1, (int)(ret.y), color);
                        DrawLine(xx0, (int)(ret.y + sizey), xx1, (int)(ret.y + sizey), color);

                        DrawLine((int)(ret.x - sizex), yy0, (int)(ret.x - sizex), yy1, color);
                        DrawLine((int)(ret.x), yy0, (int)(ret.x), yy1, color);
                        DrawLine((int)(ret.x + sizex), yy0, (int)(ret.x + sizex), yy1, color);
                    }
                }
            }
        }
        public void DrawString(string str, Color color)
        {
            return;
            mTextCal.Populate(str, color);

            DrawMesh(TextGenToMesh(mTextCal.Generator));

            //var verts = mTextCal.Generator.verts;

            //for (int i = 0; i < verts.Count; i++)
            //{
            //    var vert_a = verts[i];
            //    DrawPoint((int)vert_a.position.x, (int)vert_a.position.y, color);
            //}

            //for (int i = 0; i < verts.Count; i = i + 4)
            //{


            //    var vert_a = verts[i];
            //    for (int j = i + 1; j < i + 4; ++j)
            //    {
            //        var vert_b = verts[j];
            //        var a = vert_a.position;
            //        var b = vert_b.position;
            //        //DrawLine((int)a.x, (int)a.y, (int)b.x, (int)b.y, Color.red);
            //        Debug.LogWarning(a + "->" +b);
            //    }
            //}
        }
        Mesh TextGenToMesh(TextGenerator generator, Mesh mesh = null)
        {
            if (mesh = null)
                mesh = new Mesh();

            mesh.vertices = generator.verts.Map(v => v.position).ToArray();
            mesh.colors32 = generator.verts.Map(v => v.color).ToArray();
            mesh.uv = generator.verts.Map(v => v.uv0).ToArray();
            mesh.triangles = new int[generator.vertexCount * 6];
            for (var i = 0; i < mesh.triangles.Length; )
            {
                var t = i;
                mesh.triangles[i++] = t;
                mesh.triangles[i++] = t + 1;
                mesh.triangles[i++] = t + 2;
                mesh.triangles[i++] = t;
                mesh.triangles[i++] = t + 2;
                mesh.triangles[i++] = t + 3;
            }
            return mesh;
        }
        public void DrawPath(Physics2D.Path path, Color color)
        {
            Vector2f pre = path[0];
            for (int index_x = 1; index_x < path.Count; ++index_x)
            {
                var current = path[index_x];
                DrawLine((int)pre.x, (int)pre.y, (int)current.x, (int)current.y, color);
            }
        }
        public void DrawMesh(Mesh mesh)
        {

        }
        public void DrawCircle(Vector2f p, float r)
        {
        }
        void DrawCircleEight(int x, int y, float r, Color color)
        {
            DrawPoint(x, y, color);
            DrawPoint(y, x, color);
            DrawPoint(-x, y, color);
            DrawPoint(-y, x, color);
            DrawPoint(-y, -x, color);
            DrawPoint(-x, -y, color);
            DrawPoint(x, -y, color);
            DrawPoint(y, -x, color);
        }
        #endregion
    }
}