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
using UnityEngine;

namespace LogicEngine.Unity.Services
{
    public class LanguageService : Part
    {
        public List<SystemLanguage> SupportLanguages { get; private set; }
        public SystemLanguage DefaultLanguage { get; private set; }
        public SystemLanguage CurrentLanguage { get; private set; }
        Dictionary<SystemLanguage, string> fileNames = new Dictionary<SystemLanguage, string>();
        string fileRootName = "Cfgs/Translations/";

        protected override void _Awake()
        {
            SupportLanguages = new List<SystemLanguage>();

            AddLanguage(SystemLanguage.ChineseSimplified, "Chinese");
            AddLanguage(SystemLanguage.ChineseTraditional, "Chinese");
            AddLanguage(SystemLanguage.English, "English");
            AddLanguage(SystemLanguage.Japanese, "Chinese");

            DefaultLanguage = SystemLanguage.English;
            SwitchLanguage(UtilSetting.GetEnum<SystemLanguage>(SystemSettingKeys.KeyLanguage, Application.systemLanguage));
        }
        protected override void _Release()
        {
        }
        public bool Support(SystemLanguage language)
        {
            return SupportLanguages.Contains(language);
        }
        public void SwitchLanguage(SystemLanguage language)
        {
            if (Support(language))
            {
                inSwitchLanguage(language);
            }
            else
            {
                inSwitchLanguage(DefaultLanguage);
            }
        }
        void AddLanguage(SystemLanguage language, string file)
        {
            fileNames.Add(language, file);
            SupportLanguages.Add(language);
        }
        void inSwitchLanguage(SystemLanguage language)
        {
            if (CurrentLanguage == language) return;
            CurrentLanguage = language;
            Translation.Instance.Load(fileRootName + fileNames[CurrentLanguage]);
            UtilSetting.SetEnum<SystemLanguage>(SystemSettingKeys.KeyLanguage, CurrentLanguage);
        }
    }
}