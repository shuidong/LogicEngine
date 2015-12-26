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
    public class Tile
    {
        Vector2f mCenter;
        Vector2f mOffset;

        public Vector2i Size
        {
            get;
            private set;
        }
        public Vector2f TileSize
        {
            get;
            private set;
        }
        public Vector2f Center
        {
            get
            {
                return mCenter;
            }
            internal set
            {
                mCenter = value;
                Origin = new Vector2f(mCenter.x - mOffset.x, mCenter.y - mOffset.y);
            }
        }
        public Vector2f Origin
        {
            get;
            private set;
        }
        internal Vector2f Local
        {
            get;
            private set;
        }

        public Tile(Vector2i map_size, Vector2f tile_size, Vector2f center)
        {
            Size = map_size;
            Local = new Vector2f(
                    (Size.x.IsOdd() || Size.x == 0) ? 0.5f : 0f,
                    (Size.y.IsOdd() || Size.y == 0) ? 0.5f : 0f);
            TileSize = tile_size;
            mOffset =
                new Vector2f(
                    (((int)(Size.x / 2)) + (Size.x.IsOdd() ? 0.5f : 0f)) * TileSize.x,
                    (((int)(Size.y / 2)) + (Size.y.IsOdd() ? 0.5f : 0f)) * TileSize.y);
            Center = center;
        }

        public bool Contains(Vector2i tile)
        {
            return tile.x >= 0 && tile.x < Size.x && tile.y >= 0 && tile.y < Size.y;
        }
        public bool Contains(Vector2f world)
        {
            return Contains(ToTile(world));
        }
        public void Foreach(Action<Vector2i> fun)
        {
            for (int index_x = 0; index_x < Size.x; ++index_x)
            {
                for (int index_y = 0; index_y < Size.y; ++index_y)
                {
                    fun(new Vector2i(index_x, index_y));
                }
            }
        }
        public bool All(Predicate<Vector2i> fun)
        {
            for (int index_x = 0; index_x < Size.x; ++index_x)
            {
                for (int index_y = 0; index_y < Size.y; ++index_y)
                {
                    if (!fun(new Vector2i(index_x, index_y)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool Any(Func<Vector2i, bool> fun)
        {
            for (int index_x = 0; index_x < Size.x; ++index_x)
            {
                for (int index_y = 0; index_y < Size.y; ++index_y)
                {
                    if (fun(new Vector2i(index_x, index_y)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #region 坐标转换相关函数
        public Vector2f ToWorld(Vector2i tile, Vector2f local)
        {
            Vector2f ret;
            ret.x = Origin.x + TileSize.x * tile.x + TileSize.x * local.x;
            ret.y = Origin.y + TileSize.y * tile.y + TileSize.y * local.y;
            return ret;
        }
        public Vector2i ToTile(Vector2f world)
        {
            Vector2i ret;
            ret.x = (int)((world.x - Origin.x) / TileSize.x);
            ret.y = (int)((world.y - Origin.y) / TileSize.y);
            return ret;
        }
        public Vector2f Align(Vector2f world, Vector2f local)
        {
            return ToWorld(ToTile(world), local);
        }
        //public Vector2f Align(Tile tiles, Vector2f world)
        //{
        //    return ToWorld(ToTile(world), tiles.Local);
        //}
        #endregion
    }

    public class Tile<T> : Tile
    {
        T[] mValues;
        public Tile(Vector2i map_size, Vector2f tile_size, Vector2f center)
            : base(map_size, tile_size, center)
        {
            mValues = new T[map_size.x * map_size.y];
        }

        public T this[int x, int y]
        {
            get
            {
                return mValues[0];
            }
            set
            {
                mValues[0] = value;
            }
        }
        public T GetValue(Vector2i tile)
        {
            return this[tile.x, tile.y];
        }
        public void SetValue(Vector2i tile, T value)
        {
            this[tile.x, tile.y] = value;
        }
        int GetIndexBy(int x, int y)
        {
            return x + y * Size.x;
        }
    }
}