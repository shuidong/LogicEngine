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
using UnityEngine;

namespace LogicEngine.Unity
{
    interface IPolyglot
    {
        void OnTranslationChange();
    }

    public class PolyglotPhrase
    {
        string key = string.Empty;
        object[] values = ObjectArrayEmpty;

        public void SetKeyValues(string key, params object[] values)
        {
            this.key = key;
            this.values = values;
        }
        public void SetValues(params object[] values)
        {
            this.values = values;
        }
        public string GetKey()
        {
            return key;
        }
        public object[] GetValues()
        {
            return values;
        }
        /*
         * 情况:
         * 1.
         */
        internal string GetTranslation()
        {
            try
            {
                for (int i = 0; i < values.Length; i++)
                {
                    var aString = values[i] as string;
                    if (aString == null)
                    {
                        var aPhrase = values[i] as PolyglotPhrase;
                        if (aPhrase != null)
                        {
                            values[i] = aPhrase.GetTranslation();
                        }
                    }
                    else
                    {
                        aString = Translation.Instance.GetTranslation(aString);
                        if (aString != null)
                        {
                            values[i] = aString;
                        }
                    }
                }

                if (key == null)
                {
                    if (values.Length > 0)
                    {
                        //return UtilString.Join(',', values);
                    }
                    return "";
                }
                else
                {
                    var key_translation = Translation.Instance.GetTranslation(key);
                    if (key_translation == null)
                    {
                        return string.Format(key, values);
                    }
                    else
                    {
                        return string.Format(key_translation, values);
                    }
                }
            }
            catch (Exception e)
            {
                UtilLog.LogError("译文转换错误:" + e.ToString());
                return string.IsNullOrEmpty(key) ? "" : key;
            }
        }

        static readonly object[] ObjectArrayEmpty = new object[0];
    }

    class Translation : Singleton<Translation>
    {
        Dictionary<string, string> mTexts = new Dictionary<string, string>();
        List<IPolyglot> mPolyglots = new List<IPolyglot>();

        protected override void _Init()
        {
            #if DEBUG
            if (!Application.isPlaying)
            {
                Load("Cfgs/Translations/Chinese");
                Debug.LogWarning("译文短语数量:" + mTexts.Count);
            }
            #endif
        }

        /*
         * 有Key有译文则返回的是译文
         * 有Key没有译文则返回的是""
         * 没有Key则返回null
         */
        internal string GetTranslation(string key)
        {
            string translation;
            if (mTexts.TryGetValue(key, out translation))
            {
                return translation;
            }
            return null;
        }


        internal string Format(string key, params object[] args)
        {
            string format = null;
            if (mTexts.TryGetValue(key, out format))
            {
                try
                {
                    return string.Format(format, args);
                }
                catch
                {
                    return format;
                }
            }
            return key;
        }

        internal void AddListener(IPolyglot listener)
        {
            mPolyglots.Add(listener);
        }
        internal void RemoveListener(IPolyglot listener)
        {
            mPolyglots.Remove(listener);
        }

        internal void Load(string file)
        {
            using (StringReader reader = new StringReader(UtilResource.LoadText(file).text))
            {
                mTexts.Clear();
                string line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    var values = line.Split('=');
                    if (values.Length == 2)
                    {
                        mTexts.Add(values[0], values[1]);
                    }
                    line = reader.ReadLine();
                }
            }
            foreach (var it in mPolyglots)
            {
                it.OnTranslationChange();
            }
        }
    }
}