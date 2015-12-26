//======================================================
// Create by @Peng Guang Hui
// 2015/8/29 17:24:13
//======================================================
using System;
using System.Collections.Generic;

namespace LogicEngine.Unity
{
    public class PopupStrings
    {
        public Prop<int> index { get; set; }
        public string[] strings { get; private set; }

        public PopupStrings()
        {
            index = new Prop<int>();
            strings = new string[0];
        }

        public void SetStrings(List<string> strings)
        {
            this.strings = strings.ToArray();
            index.Value = 0;
        }
        public void SetStrings(params string[] strings)
        {
            this.strings = strings;
            index.Value = 0;
        }

        public string Current
        {
            get
            {
                return strings[index];
            }
        }
    }
}