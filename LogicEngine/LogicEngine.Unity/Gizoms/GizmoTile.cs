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
    public class GizmoTile : ForEdit
    {
        Tile mTile;

        public void SetTile(Tile tile)
        {
            mTile = tile;
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
        }
    }
}