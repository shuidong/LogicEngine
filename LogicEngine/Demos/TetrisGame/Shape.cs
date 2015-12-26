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
    enum ShapeType
    {
        t1,
        t2,
        t3,
        t4,
        t5,
        t6,
        t7
    }

    class Shape : Part
    {
        public Prop<Vector2i> Tile { get; private set; }
        Tetris tetris;
        List<Cube> cubes;

        protected override void _Awake()
        {
            tetris = GetPartInParent<Tetris>();
            cubes = new List<Cube>();

            Tile = new Prop<Vector2i>();
            //Tile.AddListener(OnTileChange);
        }
        protected override void _Release()
        {
        }

        public void SetType(ShapeType type)
        {
            switch (type)
            {
                default:
                case ShapeType.t1:
                    SetCubes(new Vector2i(0, 0), new Vector2i(0, -1), new Vector2i(-1, -1), new Vector2i(-1, 0));
                    break;
                case ShapeType.t2:
                    SetCubes(new Vector2i(-2, 0), new Vector2i(-1, 0), new Vector2i(0, 0), new Vector2i(1, 0));
                    break;
                case ShapeType.t3:
                    SetCubes(new Vector2i(0, 0), new Vector2i(1, 0), new Vector2i(0, 1), new Vector2i(-1, 1));
                    break;
                case ShapeType.t4:
                    SetCubes(new Vector2i(0, 0), new Vector2i(-1, 0), new Vector2i(0, 1), new Vector2i(1, 1));
                    break;
                case ShapeType.t5:
                    SetCubes(new Vector2i(0, 0), new Vector2i(1, 0), new Vector2i(2, 0), new Vector2i(0, 1));
                    break;
                case ShapeType.t6:
                    SetCubes(new Vector2i(0, 0), new Vector2i(-1, 0), new Vector2i(-2, 0), new Vector2i(0, 1));
                    break;
                case ShapeType.t7:
                    SetCubes(new Vector2i(0, 0), new Vector2i(-1, 0), new Vector2i(1, 0), new Vector2i(0, 1));
                    break;
            }
        }
        public bool CanMoveTo(MoveDirection dir)
        {
            foreach (var it in cubes)
            {
                if (!it.CanMoveTo(dir)) return false;
            }
            return true;
        }
        public void MoveTo(MoveDirection dir)
        {
            foreach (var it in cubes)
            {
                it.MoveTo(dir);
            }
        }
        public bool CanRotate()
        {
            foreach (var it in cubes)
            {
                if (!it.CanRotate()) return false;
            }
            return true;
        }
        public void Rotate()
        {
            foreach (var it in cubes)
            {
                it.Rotate();
            }
            //Tile.SetDirty();
        }
        public void Put2Ground()
        {
            foreach (var it in cubes)
            {
                it.Put2Ground();
            }
            cubes.Clear();
        }
        void OnTileChange()
        {
            var tile = Tile.Value;
            foreach (var it in cubes)
            {
                it.Tile.Value = tile + it.LocalTile;
            }
        }
        void SetCubes(params Vector2i[] locals)
        {
            cubes.Clear();
            foreach (var it in locals)
            {
                cubes.Add(tetris.GetCube(it));
            }
        }
    }
}