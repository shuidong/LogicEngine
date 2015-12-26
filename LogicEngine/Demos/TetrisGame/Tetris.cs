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
using LogicEngine;

namespace Demos.TetrisGame
{
    enum Mask : int
    {
        None = 0,
        Block = 1 << 1,
    }
    class Tetris : Part
    {
        public TileMap<Mask> Tiles { get; private set; }
        public Shape Current { get; private set; }
        public Shape Next { get; private set; }

        protected override void _Awake()
        {
            Tiles = new TileMap<Mask>(new Vector2i(10, 24), new Vector2f(1f, 1f), new Vector2f(0, 3f));
            //UtilMono.Debug2<GizmoTileMap>(it => it.Init(TileMap));

            RandomSharp();
        }
        protected override void _Release()
        {
        }
        public Cube GetCube(Vector2i local_tile)
        {
            var cube = AddChild().AddPart<Cube>();
            cube.LocalTile = local_tile;
            return cube;
        }
        public void RandomSharp()
        {
            Current = Next;
            if (Current != null) Current.Tile.Value = new Vector2i(Tiles.Size.x / 2, Tiles.Size.y - 4);
            Next = AddChild().AddPart<Shape>();
            Next.SetType((ShapeType)UnityEngine.Random.Range(0, 8));
            Next.Tile.Value = new Vector2i(12, 10);
        }
    }
}