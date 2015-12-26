//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;
using UnityEngine.UI;

namespace LogicEngine.Unity
{
    [ExecuteInEditMode]
    [AddComponentMenu("UI/i18n/Text", 11)]
    public class Text_i18n : Text, IPolyglot
    {
        PolyglotPhrase phrase = new PolyglotPhrase();

        protected override void Start()
        {
            base.Start();
            Translation.Instance.AddListener(this);
            phrase.SetKeyValues(text);
        }
        protected override void OnDestroy()
        {
            Translation.Instance.RemoveListener(this);
            base.OnDestroy();
        }
        void IPolyglot.OnTranslationChange()
        {
            text = phrase.GetTranslation();
        }
        public void SetPhrase(PolyglotPhrase phrase)
        {
            this.phrase = phrase;
            text = phrase.GetTranslation();
        }
        public void SetPhrase(string key, params object[] values)
        {
            phrase.SetKeyValues(key, values);
            text = phrase.GetTranslation();
        }
        public void SetPhraseValues(object[] values)
        {
            phrase.SetValues(values);
            text = phrase.GetTranslation();
        }
        protected override void OnValidate()
        {
            base.OnValidate();
        }
        #region 编辑模式字符串显示
#if DEBUG
        bool isInText = false;
        public override string text
        {
            get
            {
                if (isInText)
                {
                    phrase.SetKeyValues(base.text);
                    return phrase.GetTranslation();
                }
                return base.text;
            }
            set
            {
                base.text = value;
            }
        }
        protected override void OnPopulateMesh(Mesh toFill)
        {
            isInText = true;
            base.OnPopulateMesh(toFill);
            isInText = false;
        }//https://bitbucket.org/Unity-Technologies
        public override void OnRebuildRequested()
        {
            if (!Application.isPlaying)
            {
                isInText = true;
            }
            base.OnRebuildRequested();
            if (!Application.isPlaying)
            {
                isInText = false;
            }
        }
      
#endif
        #endregion
    }
}