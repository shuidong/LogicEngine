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

namespace LogicEngine.Physics2D
{
    public class FlowField
    {
        public Vector2i MapSize { get { return tilemap.Size; } }
        public Vector2f TileSize { get { return tilemap.TileSize; } }
        public Vector2f Center { get { return tilemap.Center; } }
        Tile tilemap;
        Vector2f[][] field;

        public FlowField(Vector2i map_size, Vector2f tile_size, Vector2f center)
        {
            tilemap = new Tile(map_size, tile_size, center);

            field = new Vector2f[MapSize.x][];
            for (int i = 0; i < MapSize.x; i++)
            {
                field[i] = new Vector2f[MapSize.y];
            }
        }
        public void Random()
        {
            for (int i = 0; i < MapSize.x; i++)
            {
                for (int j = 0; j < MapSize.y; j++)
                {
                    field[i][j] = UtilRandom.onUnitCircle;
                }
            }
        }
        //public void Foreach(Action<Vector2f, Vector2f> fun)
        //{
        //    UtilLambda.Foreach(MapSize.x, MapSize.x, (i, j) => fun(tilemap.ToWorld(new Vector2i(i,j), Vector2f.half), field[i][j]));
        //}
        public Vector2f GetPosition(int i, int j)
        {
            return tilemap.ToWorld(new Vector2i(i, j), Vector2f.half);
        }
        public Vector2f GetDir(int i, int j)
        {
            return field[i][j];
        }
        public Vector2f GetDir(Vector2f world)
        {
            var tile = tilemap.ToTile(world);
            if (tilemap.Contains(tile))
            {
                return field[tile.x][tile.y];
            }
            return Vector2f.zero;
        }
        public bool Contains(Vector2f world)
        {
            return tilemap.Contains(world);
        }
    }
}