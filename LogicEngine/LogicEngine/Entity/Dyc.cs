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

namespace LogicEngine
{
    /// <summary>
    /// Dynamic Data
    /// </summary>
    public abstract class Dyc
    {
        public long id;

        /// <summary>
        /// 修正数据
        /// </summary>
        /// <returns></returns>
        public bool TryRevise()
        {
            return _TryRevise();
        }
        protected abstract bool _TryRevise();
        
        protected static bool HasSthWrong(Dyc dyc)
        {
            return !dyc.TryRevise();
        }
        protected static bool HasSthWrong<TDyc>(List<TDyc> list)
            where TDyc : Dyc
        {
            foreach (var it in list)
            {
                if (it.TryRevise())
                {
                }
                else 
                {
                    return true;
                }
            }
            return false;
        }
    }

    public interface IDycPart<TDyc>
         where TDyc : Dyc
    {
        long id { get; }
        void Sync(TDyc dyc);
        TDyc Save();
    }
}