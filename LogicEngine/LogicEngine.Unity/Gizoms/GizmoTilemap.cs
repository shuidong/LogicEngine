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

namespace LogicEngine.Unity
{
    public abstract class GizmoTileMap<T> : ForEdit
         where T : struct, IConvertible
    {
        TileMap<T> mTile;
        T mMaskShow;

        public void SetTile(TileMap<T> tile, T mask_show)
        {
            mTile = tile;
            mMaskShow = mask_show;
        }

        void OnDrawGizmos()
        {
            if (mTile == null) return;

            UtilGizmos.DrawTile(mTile.Size, mTile.TileSize, mTile.Origin, Color.cyan);

            float radius = (mTile.TileSize.x + mTile.TileSize.y) * 0.5f * 0.4f;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(mTile.Origin.to3(), radius);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(mTile.Center.to3(), radius);

            radius = (mTile.TileSize.x + mTile.TileSize.y) * 0.5f * 0.8f;
            UtilLambda.Foreach(mTile.Size.x, mTile.Size.y,
                    (int x, int y) =>
                    {
                        Gizmos.color = Color.red;
                        if (mTile.HasMask(new Vector2i(x, y), mMaskShow))
                            Gizmos.DrawCube(mTile.ToWorld(new Vector2i(x, y), Vector2f.one * 0.5f).to3(), new Vector3(radius, 0, radius));
                    }
                    );
        }

        void OnGUI()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (mTile == null || Camera.main == null) return;
                var cell = mTile.ToTile(Camera.main.Screen2Ground(Input.mousePosition).to2());
                Vector2 screen = Input.mousePosition;
                Vector2 size = new Vector2(50, 20);
                TipString(ClampScreen(screen, size), size, cell.ToString(), Color.yellow);
            }
        }
    }
}