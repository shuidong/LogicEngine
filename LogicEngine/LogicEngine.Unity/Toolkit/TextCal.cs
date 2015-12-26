//======================================================
// Create by @Peng Guang Hui
// 2015/12/12 16:57:21
//======================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LogicEngine.Unity.Toolkit
{
    class TextCal
    {
        public TextGenerator Generator { get { return mTextGenerator; } }
        Font mFont;
        FontData mFontData = FontData.defaultFontData;
        float pixelsPerUnit = 1;
        public TextGenerator mTextGenerator = new TextGenerator();

        public TextCal()
        {
            mFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
        }

        TextGenerationSettings GetGenerationSettings(Vector2 extents, Color color)
        {
            var settings = new TextGenerationSettings();

            settings.generationExtents = extents;
            if (mFont != null && mFont.dynamic)
            {
                settings.fontSize = mFontData.fontSize;
                settings.resizeTextMinSize = mFontData.minSize;
                settings.resizeTextMaxSize = mFontData.maxSize;
            }

            // Other settings
            settings.textAnchor = mFontData.alignment;
            settings.scaleFactor = pixelsPerUnit;
            settings.color = color;
            settings.font = mFont;
            settings.pivot = Vector2f.zero.to2();
            settings.richText = mFontData.richText;
            settings.lineSpacing = mFontData.lineSpacing;
            settings.fontStyle = mFontData.fontStyle;
            settings.resizeTextForBestFit = mFontData.bestFit;
            settings.updateBounds = false;
            settings.horizontalOverflow = mFontData.horizontalOverflow;
            settings.verticalOverflow = mFontData.verticalOverflow;

            return settings;
        }
        public void Populate(string str, Color color)
        {
            var settings = GetGenerationSettings(new Vector2(960, 640), color);
            mTextGenerator.Populate(str, settings);
        }
    }
}