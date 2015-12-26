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
    enum MoveDirection
    {
        Down,
        Left,
        Right,
        Up
    }

    class Cube : Part
    {
        static Dictionary<MoveDirection, Vector2i> offsets = new Dictionary<MoveDirection, Vector2i>();
        static Cube()
        {
            offsets.Add(MoveDirection.Down, new Vector2i(0, -1));
            offsets.Add(MoveDirection.Left, new Vector2i(-1, 0));
            offsets.Add(MoveDirection.Right, new Vector2i(1, 0));
            offsets.Add(MoveDirection.Up, new Vector2i(0, 1));
        }

        public Prop<Vector2i> Tile { get; private set; }
        public Vector2i LocalTile { get; set; }
        Tetris tetris;
        //CubeDisplay Display;

        protected override void _Awake()
        {
            tetris = GetPartInParent<Tetris>();
            //Display = UtilResource.LoadPrefab("Cube").AddComponent<CubeDisplay>();

            Tile = new Prop<Vector2i>();
            //Tile.AddListener(OnTileChange);
        }
        protected override void _Release()
        {
        }
        public void Put2Ground()
        {
            tetris.Tiles.AddMask(Tile.Value, Mask.Block);
        }
        public bool CanMoveTo(MoveDirection dir)
        {
            return CanMoveTo(Tile.Value + Cube.offsets[dir]);
        }
        public void MoveTo(MoveDirection dir)
        {
            Tile.Value += Cube.offsets[dir];
        }
        public bool CanRotate()
        {
            return CanMoveTo(Tile.Value + new Vector2i(LocalTile.y, -LocalTile.x));
        }
        public void Rotate()
        {
            LocalTile = new Vector2i(LocalTile.y, -LocalTile.x);
        }
        void OnTileChange()
        {
            //Display.transform.position = tetris.Tiles.ToWorld(Tile.Value, new Vector2f(0.5f, 0.5f)).to3();
        }
        bool CanMoveTo(Vector2i target)
        {
            return tetris.Tiles.Contains(target) && !tetris.Tiles.HasMask(target, Mask.Block);
        }
    }
}