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

namespace LogicEngine
{
    /// <summary>
    /// 带旋转的Tile
    /// </summary>
    public class RTile
    {
        Tile mTile;
        float mRotation;

        public Vector2i MapSize
        {
            get { return mTile.Size; }
        }
        public Vector2f TileSize
        {
            get { return mTile.TileSize; }
        }
        public Vector2f Center
        {
            get { return mTile.Center; }
        }

        public RTile(Vector2i map_size, Vector2f tile_size, Vector2f center, float rotation)
        {
            mTile = new Tile(map_size, tile_size, center);
            mRotation = rotation;
        }
        Vector2f RotateToTile(Vector2f world)
        {
            return world;
        }
        Vector2f RotateToWorld(Vector2f tile)
        {
            return tile;
        }
        public Vector2f ToWorld(Vector2i tile, Vector2f local)
        {
            return RotateToWorld(mTile.ToWorld(tile, local));
        }
        public Vector2i ToTile(Vector2f world)
        {
            return mTile.ToTile(RotateToTile(world));
        }
        public Vector2f Align(Vector2f world, Vector2f local)
        {
            return RotateToWorld(mTile.Align(RotateToTile(world), local));
        }
    }
}