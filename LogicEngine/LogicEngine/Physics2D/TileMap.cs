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
    public class TileMap<TMask> : Tile where TMask : struct, IConvertible
    {
        long[] mMasks;

        public TileMap(Vector2i map_size, Vector2f tile_size, Vector2f center)
            : base(map_size, tile_size, center)
        {
            mMasks = new long[map_size.x * map_size.y];
        }

        public void AddMask(Vector2i tile, TMask mask)
        {
            SetMask(tile, UtilMask.Add(GetMask(tile), mask.ToInt64(null)));
        }
        public void RemoveMask(Vector2i tile, TMask mask)
        {
            SetMask(tile, UtilMask.Sub(GetMask(tile), mask.ToInt64(null)));
        }

        public bool HasMask(Vector2i tile, TMask mask)
        {
            return UtilMask.Test(GetMask(tile), mask.ToInt64(null));
        }
        public long GetMask(Vector2i tile)
        {
            if (Contains(tile))
            {
                return mMasks[GetIndexBy(tile.x, tile.y)];
            }
            return 0;
        }
        int GetIndexBy(int x, int y)
        {
            return x + y * Size.x;
        }
        void SetMask(Vector2i tile, long mask)
        {
            if (Contains(tile))
            {
                mMasks[GetIndexBy(tile.x, tile.y)] = mask;
            }
        }

        #region two tile interaction
        public bool CanPlace(Tile target, Vector2i tile, TMask block)
        {
            if (target.Size.x == 0 || target.Size.y == 0)
            {
                return Contains(target.Center);
            }
            var offset = ToWorld(tile, target.Local) - target.Center;
            for (int i = 0; i < target.Size.x; ++i)
            {
                for (int j = 0; j < target.Size.y; ++j)
                {
                    var t = ToTile(target.ToWorld(new Vector2i(i, j), Vector2f.half) + offset);
                    if (!Contains(t) || HasMask(t, block))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void Place(Tile target, Vector2i tile, TMask block)
        {
            target.Center = ToWorld(tile, target.Local);
            target.Foreach(it => AddMask(ToTile(target.ToWorld(it, Vector2f.half)), block));
        }
        public void Pickup(Tile target, TMask block)
        {
            target.Foreach(it => RemoveMask(ToTile(target.ToWorld(it, Vector2f.half)), block));
        }

        public bool CanPlace<TMaskTarget>(TMask this_block, TileMap<TMaskTarget> target, Vector2i tile, TMaskTarget target_block)
            where TMaskTarget : struct, IConvertible
        {
            if (target.Size.x == 0 || target.Size.y == 0)
            {
                return Contains(target.Center);
            }
            var offset = ToWorld(tile, target.Local) - target.Center;
            for (int i = 0; i < target.Size.x; ++i)
            {
                for (int j = 0; j < target.Size.y; ++j)
                {
                    var target_each = new Vector2i(i, j);
                    if (target.HasMask(target_each, target_block))
                    {
                        var t = ToTile(target.ToWorld(new Vector2i(i, j), Vector2f.half) + offset);
                        if (!Contains(t) || HasMask(t, this_block))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public void Place<TMaskTarget>(TMask this_block, TileMap<TMaskTarget> target, Vector2i tile, TMaskTarget target_block)
            where TMaskTarget : struct, IConvertible
        {
            target.Center = ToWorld(tile, target.Local);
            target.Foreach(it =>
            {
                if (target.HasMask(it, target_block))
                {
                    AddMask(ToTile(target.ToWorld(it, Vector2f.half)), this_block);
                }
            });
        }
        public void Pickup<TMaskTarget>(TMask this_block, TileMap<TMaskTarget> target, Vector2i tile, TMaskTarget target_block)
            where TMaskTarget : struct, IConvertible
        {
            target.Foreach(it =>
            {
                if (target.HasMask(it, target_block))
                {
                    RemoveMask(ToTile(target.ToWorld(it, Vector2f.half)), this_block);
                }
            });
        }
        #endregion
    }
}