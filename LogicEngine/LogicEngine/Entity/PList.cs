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
using System.Text;

namespace LogicEngine
{
    public class Plist
    {
        Dictionary<string, string> plist = new Dictionary<string, string>();

        public Plist Add(string key, string value)
        {
            plist[key] = value;
            return this;
        }

        public void Clear()
        {
            plist.Clear();
        }

        public bool GetBool(string key, bool default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                bool value;
                if (bool.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to boolean", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public char GetChar(string key, char default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                char value;
                if (char.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to char", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public sbyte GetSByte(string key, sbyte default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                sbyte value;
                if (sbyte.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to sbyte", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public byte GetByte(string key, byte default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                byte value;
                if (byte.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to byte", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public ushort GetUShort(string key, ushort default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                ushort value;
                if (ushort.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to ushort", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public uint GetUInt(string key, uint default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                uint value;
                if (uint.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to uint", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public ulong GetULong(string key, ulong default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                ulong value;
                if (ulong.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to ulong", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public short GetShort(string key, short default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                short value;
                if (short.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to short", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public int GetInt(string key, int default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                int value;
                if (int.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to int", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public long GetLong(string key, long default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                long value;
                if (long.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to long", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public float GetFloat(string key, float default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                float value;
                if (float.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to float", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public double GetDouble(string key, double default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                double value;
                if (double.TryParse(svalue, out value))
                {
                    return value;
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't parse {0} to double", svalue);
                }
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public string GetString(string key, string default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                return svalue;
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public string[] GetStringArray(string key, char sep = ',')
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                return svalue.Split(sep);
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return new string[0];
        }
        public T GetEnum<T>(string key, T default_value) where T : struct
        {
            var type = typeof(T);
            if (type.IsEnum)
            {
                string svalue = null;
                if (plist.TryGetValue(key, out svalue))
                {
                    try
                    {
                        return (T)Enum.Parse(type, svalue);
                    }
                    catch
                    {
                        UtilLog.LogErrorFormat("Cann't parse {0} to {1}", svalue, type.Name);
                    }
                }
                else
                {
                    UtilLog.LogErrorFormat("Cann't find key : {0}", key);
                }
            }
            else
            {
                UtilLog.LogErrorFormat("GetEnum's generic param must be a enum, but actually is {0}", type.Name);
            }
            return default_value;
        }
        public Vector2i GetVector2i(string key, Vector2i default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                string[] pairs = svalue.Split(',');
                if (pairs.Length == 2 || pairs.Length == 3)
                {
                    int x;
                    if (int.TryParse(pairs[0], out x))
                    {
                        int y;
                        if (int.TryParse(pairs[1], out y))
                        {
                            return new Vector2i(x, y);
                        }
                    }
                }
                UtilLog.LogErrorFormat("Cann't parse {0} to Vector2i", svalue);
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public Vector3i GetVector3i(string key, Vector3i default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                string[] pairs = svalue.Split(',');
                if (pairs.Length == 3)
                {
                    int x;
                    if (int.TryParse(pairs[0], out x))
                    {
                        int y;
                        if (int.TryParse(pairs[1], out y))
                        {
                            int z;
                            if (int.TryParse(pairs[2], out z))
                            {
                                return new Vector3i(x, y, z);
                            }
                        }
                    }
                }
                UtilLog.LogErrorFormat("Cann't parse {0} to Vector3i", svalue);
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public Vector2f GetVector2f(string key, Vector2f default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                string[] pairs = svalue.Split(',');
                if (pairs.Length == 2 || pairs.Length == 3)
                {
                    float x;
                    if (float.TryParse(pairs[0], out x))
                    {
                        float y;
                        if (float.TryParse(pairs[1], out y))
                        {
                            return new Vector2f(x, y);
                        }
                    }
                }
                UtilLog.LogErrorFormat("Cann't parse {0} to Vector2f", svalue);
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }
        public Vector3f GetVector3f(string key, Vector3f default_value)
        {
            string svalue = null;
            if (plist.TryGetValue(key, out svalue))
            {
                string[] pairs = svalue.Split(',');
                if (pairs.Length == 3)
                {
                    float x;
                    if (float.TryParse(pairs[0], out x))
                    {
                        float y;
                        if (float.TryParse(pairs[1], out y))
                        {
                            float z;
                            if (float.TryParse(pairs[2], out z))
                            {
                                return new Vector3f(x, y, z);
                            }
                        }
                    }
                }
                UtilLog.LogErrorFormat("Cann't parse {0} to Vector3f", svalue);
            }
            UtilLog.LogErrorFormat("Cann't find key : {0}", key);
            return default_value;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (var it in plist)
            {
                result.Append(it.Key + "=" + it.Value + (index == plist.Count - 1 ? "" : "\n"));
                index++;
            }
            return result.ToString();
        }
    }

    public class PlistSet : IEnumerable<Plist>
    {
        Dictionary<string, Plist> mCfgs = new Dictionary<string, Plist>();

        public int Count { get { return mCfgs.Count; } }

        public bool Contains(string id)
        {
            return mCfgs.ContainsKey(id);
        }

        public Plist Get(string id)
        {
            Plist v = default(Plist);
            if (!mCfgs.TryGetValue(id, out v))
            {
                UtilLog.LogWarning("数据集没有id:" + id + "的数据");
            }
            return v;
        }
        public void Foreach(Action<Plist> fun)
        {
            foreach (var it in mCfgs)
            {
                fun(it.Value);
            }
        }

        internal void Add(string id, Plist v)
        {
            mCfgs.Add(id, v);
        }
        public List<Plist> ToList()
        {
            List<Plist> list = new List<Plist>(mCfgs.Count);
            foreach (var it in mCfgs)
            {
                list.Add(it.Value);
            }
            return list;
        }
        IEnumerator<Plist> IEnumerable<Plist>.GetEnumerator()
        {
            return new Enumerator(mCfgs);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(mCfgs);
        }
        public struct Enumerator : IEnumerator<Plist>
        {
            Dictionary<string, Plist>.Enumerator mEnumerator;
            internal Enumerator(Dictionary<string, Plist> values)
            {
                mEnumerator = values.GetEnumerator();
            }
            Plist IEnumerator<Plist>.Current
            {
                get { return mEnumerator.Current.Value; }
            }
            void IDisposable.Dispose()
            {
                mEnumerator.Dispose();
            }
            object IEnumerator.Current
            {
                get { return mEnumerator.Current.Value; }
            }
            bool IEnumerator.MoveNext()
            {
                return mEnumerator.MoveNext();
            }
            void IEnumerator.Reset()
            {
                (mEnumerator as IEnumerator).Reset();
            }
        }
    }
}