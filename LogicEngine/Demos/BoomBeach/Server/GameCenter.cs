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

namespace Demos.BoomBeach.Server
{
    public class GameCenter : Center<GameCenter>
    {
        public EntityMgr<Play, DycRole> roles { get; private set; }
        public ChatUser ChatUser { get; private set; }

        protected override void _Awake()
        {
            roles = AddPart<EntityMgr<Play, DycRole>>();
            ChatUser = AddPart<ChatUser>();
        }

        protected override void _Release()
        {
        }
        public Play Login(IPeer peer, Url url_mirror, long role_id)
        {
            var dyc = GetInitDycRole(role_id);
            if (dyc.TryRevise())
            {
                var role = roles.Add(dyc);
                role.SetPeer(peer, url_mirror);
                role.entity.AddPart<GuildHooker>();
                //role.entity.AddPart<RoleServer>();
                return role;
            }
            else
            {
                UtilLog.LogError("登录角色：错误：存档数据异常");
                return null;
            }
        }

        DycRole GetInitDycRole(long role_id)
        {
            var dyc = UtilRandom.Dyc<DycRole>(role_id);

            dyc.d0.d0 = "虚拟测试";
            dyc.d0.d1 = 1;
            dyc.d0.d2.Add(UtilRandom.Dyc<DycItem>(SpecialItemID.Gold));
            dyc.d0.d2.Add(UtilRandom.Dyc<DycItem>(SpecialItemID.Wood));
            dyc.d0.d2.Add(UtilRandom.Dyc<DycItem>(SpecialItemID.Cloth));
            dyc.d0.d2.Add(UtilRandom.Dyc<DycItem>(SpecialItemID.Metal));
            dyc.d0.d2.Add(UtilRandom.Dyc<DycItem>(SpecialItemID.Diamond));

            var hero = UtilRandom.Dyc<DycHero>();
            hero.d0 = "Godox";
            hero.d1 = 1;
            dyc.d0.d4.Add(hero);

            Random(ref dyc.d0.d3, 10);
            //Random(ref dyc.d0.d4, 1);

            dyc.d1.d0.d0 = "islet00";
            foreach (var it in CfgMgr.MainbaseObjects)
            {
                if (it.object_type == IsletObjectType.Building)
                {
                    DycBuilding dyc_building = UtilRandom.Dyc<DycBuilding>();
                    dyc_building.d0 = it.object_id;
                    dyc_building.d1 = it.level;
                    dyc_building.d2x = it.tile.x;
                    dyc_building.d2y = it.tile.y;
                    dyc.d1.d0.d1.Add(dyc_building);
                }
                else if (it.object_type == IsletObjectType.Varia)
                {
                    DycVaria dyc_varia = UtilRandom.Dyc<DycVaria>();
                    dyc_varia.d0 = it.object_id;
                    dyc_varia.d2x = it.tile.x;
                    dyc_varia.d2y = it.tile.y;
                    dyc.d1.d0.d2.Add(dyc_varia);
                }
            }

            return dyc;
        }
        void Random<T>(ref List<T> list, int count)
            where T : Dyc, new()
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(UtilRandom.Dyc<T>());
            }
        }
    }

    [RequirePart(typeof(Play))]
    public class GuildHooker : GameCenter.Part
    {
        Play mPlay;

        protected override void _Awake()
        {
            mPlay = GetPart<Play>();
        }

        protected override void _Release()
        {
            throw new NotImplementedException();
        }
    }
}