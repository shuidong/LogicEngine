//======================================================
// Create by @Peng Guang Hui
// 2015/10/21 15:47:34
//======================================================
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LogicEngine.Unity
{
    static class SystemSettingKeys
    {
        static readonly string KeySystemSetting = "SystemSetting.";
        public static readonly string KeyLanguage = KeySystemSetting + "Language";
    }
    public static class UtilSetting
    {
        public static T GetEnum<T>(string key, T default_value)
        {
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    var type = typeof(SystemLanguage);
                    if (type.IsEnum)
                    {
                        return (T)Enum.Parse(type, PlayerPrefs.GetString(key));
                    }
                }
            }
            catch
            {
            }
            return default_value;
        }
        public static void SetEnum<T>(string key, T value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }
    }
}