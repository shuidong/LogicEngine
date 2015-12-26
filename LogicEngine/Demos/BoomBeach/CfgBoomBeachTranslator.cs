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
using System.IO;
using LogicEngine;

namespace Demos.BoomBeach
{
    public static class CfgBoomBeachTranslator
    {
        public static void TranslateLevels()
        {
            //UnityEngine.Application.dataPath+"/Resources/Cfgs/levels/"

            var length = (UnityEngine.Application.dataPath + "/Resources/Cfgs/").Length;
            TileMap<Islet.Mask> tilemap = new TileMap<Islet.Mask>(new Vector2i(53, 61), new Vector2f(0.5f, 0.5f), new Vector2f(0.8242877f, -1.864281f));
            foreach (var file in Directory.GetFiles(UnityEngine.Application.dataPath + "/Resources/Cfgs/levels/", "*.xml", SearchOption.AllDirectories))
            {
                var name = file.Substring(length, file.Length - length).Split('.')[0];
                name = UtilPath.Normalize(name);
                TranslateLevel(name, "newlevels/" + name, tilemap);
            }
        }
        static void TranslateLevel(string path_source, string path_target, TileMap<Islet.Mask> tilemap)
        {

            Action<CfgIsletObjectSource, CfgIsletObject> fun = (source, target) =>
                {
                    UtilCfg.SetCfgValue(target, "id", source.id);
                    UtilCfg.SetCfgValue(target, "name", source.Name);
                    UtilCfg.SetCfgValue(target, "object_id", source.ObjectID);
                    UtilCfg.SetCfgValue(target, "object_type", source.ObjectType == "1" ? IsletObjectType.Building : IsletObjectType.Varia);
                    UtilCfg.SetCfgValue(target, "level", source.Level);
                    UtilCfg.SetCfgValue(target, "tile", tilemap.ToTile(new Vector2f(source.Position.x, source.Position.z)));
                };
            Convert(path_source, path_target, fun);
        }
        static void Convert<T1, T2>(string path_source, string path_target, Action<T1, T2> fun)
            where T1 : Cfg, new()
            where T2 : Cfg, new()
        {
            var objs = CfgMgr.Load<T1>(path_source);
            UtilLog.LogWarning(path_source + "-> "+ objs.Count);
            List<T2> targets = new List<T2>();
            foreach (var source in objs.ToList())
            {
                T2 target = new T2();
                fun(source, target);
                targets.Add(target);
            }
            UtilCfg.SaveToXml(targets, UnityEngine.Application.dataPath + "/Resources/Cfgs/" + path_target + ".xml");
        }

        public class CfgIsletObjectSource : Cfg
        {
            public readonly string Name;
            public readonly string ObjectID;
            public readonly string ObjectType;
            public readonly int Level;

            public readonly Vector3f Position;
        }
    }
}