//======================================================
// Create by @Peng Guang Hui
// 2015/6/23 19:15:59
//======================================================
namespace LogicEngine.Edit
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Reflection;

    public static class UtilCfgWriter
    {
        public static string WriteToXml<T>(List<T> datas) where T : Cfg, new()
        {
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
            return doc.InnerXml;
        }

        public static void SaveToXml<T>(List<T> datas, string path) where T : Cfg, new()
        {
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
        }

        public static void SetCfgValue<T>(T obj, string filed_name, object value) where T : Cfg
        {
            var info = typeof(T).GetField(filed_name, BindingFlags.Instance | BindingFlags.Public);
            UtilAssert.IsNotNull(info, "Cfg : " + typeof(T).Name + " 没有字段<" + filed_name + ">");
            info.SetValue(obj, value);
        }
    }
}