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
using Mono.Xml;
using System.Security;
#if DEBUG
using System.Xml;
using System.Reflection;
#endif

namespace LogicEngine
{
    public static class UtilCfg
    {
        public static CfgSet<T> LoadCfgSet<T>(string context) where T : Cfg, new()
        {
            CfgSet<T> set = new CfgSet<T>();

            SecurityParser doc = new SecurityParser();
            doc.LoadXml(context);

            foreach (SecurityElement it in doc.ToXml().Children)
            {
                if (it.Tag == "Cfg")
                {
                    T data = new T();
                    foreach (var field in CachedType<T>.Fields)
                    {
                        string attribute = it.Attribute(field.Name);
                        if (!string.IsNullOrEmpty(attribute))
                        {
                            field.Info.SetValue(data, Converter.ToObject(attribute, field));
                        }
                    }
                    set.Add(data.id, data);
                }
            }

            return set;
        }
        public static PlistSet LoadPlistSet(string context)
        {
            PlistSet set = new PlistSet();

            SecurityParser doc = new SecurityParser();
            doc.LoadXml(context);
            foreach (SecurityElement it in doc.ToXml().Children)
            {
                if (it.Tag == "Cfg")
                {
                    Plist plist = new Plist();
                    Hashtable attributes = it.Attributes;
                    foreach (DictionaryEntry entry in attributes)
                    {
                        string key = entry.Key as string;
                        string value = entry.Value as string;
                        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                        {
                            continue;
                        }
                        plist.Add(key, value);
                    }
                    set.Add(plist.GetString("id", ""), plist);
                }
            }

            return set;
        }
        public static void SaveToXml<T>(List<T> datas, string path) where T : Cfg, new()
        {
#if DEBUG
            XmlDocument doc = new XmlDocument();
            doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), doc.DocumentElement);

            XmlElement sets = doc.CreateElement("Cfgs");
            doc.AppendChild(sets);

            foreach (T data in datas)
            {
                XmlElement node = doc.CreateElement("Cfg");
                foreach (var field in CachedType<T>.Fields)
                {
                    node.SetAttribute(field.Name, Converter.ToStringFrom(data, field));
                }
                sets.AppendChild(node);
            }
            doc.Save(path);
#else
            UtilLog.LogError("RELEASE版本不提供写入XML");
#endif
        }
        public static void SetCfgValue<T>(T obj, string filed_name, object value) where T : Cfg
        {
#if DEBUG
            var info = typeof(T).GetField(filed_name, BindingFlags.Instance | BindingFlags.Public);
            UtilAssert.IsNotNull(info, "Cfg : " + typeof(T).Name + " 没有字段<" + filed_name + ">");
            info.SetValue(obj, value);
#else
            UtilLog.LogError("RELEASE版本不提供SetCfgValue");
#endif
        }
    }
}