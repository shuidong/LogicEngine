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
    public class Prop<T>
    {
        T mValue;
        T mLastValue;

        public Prop()
        {
            mValue = default(T);
            mLastValue = mValue;
        }

        public Prop(T init)
        {
            mValue = init;
        }

        public T LastValue
        {
            get { return mLastValue; }
        }
        public T Value
        {
            get { return mValue; }
            set
            {
                mLastValue = mValue;
                mValue = value;
            }
        }

        public bool IsChange()
        {
            return !mLastValue.Equals(mValue);
        }
       
        public static implicit operator T(Prop<T> sensor)
        {
            return sensor.mValue;
        }

        public override string ToString()
        {
            return mValue == null ? "" : mValue.ToString();
        }
    }
}