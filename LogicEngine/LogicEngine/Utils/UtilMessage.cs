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
    public class Subject
    {
        ICollection<object> mObservers = new MutableCollection<object>();

        public void Attach(Object observer)
        {
            mObservers.Add(observer);
        }

        public void Detach(Object observer)
        {
            mObservers.Remove(observer);
        }

        protected void SendMessage(string name, params object[] args)
        {
            UtilMessage.SendMessage(mObservers, name, args);
        }
    }

    public static class UtilMessage
    {
        public static void SendMessage(System.Collections.IEnumerable lisenters, string messgae, object[] args)
        {
            foreach (var it in lisenters)
            {
                SendMessage(it, messgae, args);
            }
        }
        internal static void SendMessage(object lisenter, string messgae, params object[] args)
        {
            SendMessage(lisenter.GetType(), lisenter, messgae, args);
        }

        internal static void SendMessage(Type type, object lisenter, string messgae, params object[] args)
        {
            var fun = type.GetMethod(messgae, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (fun == null)
            {
                //UtilLog.LogWarningFormat("未发现消息{0} -> {1}", lisenter.GetType(), messgae);
            }
            else
            {
                var parameters = fun.GetParameters();
                if (parameters.Length == args.Length)
                {
                    for (int i = 0; i < parameters.Length; ++i)
                    {
                        if (parameters[i].ParameterType != args[i].GetType())
                        {
                            UtilLog.LogWarningFormat("{0}->{1}:第{2}个参数类型不匹配", lisenter.GetType(), messgae, i + 1);
                            return;
                        }
                    }
                    if (fun != null) fun.Invoke(lisenter, args);
                }
                else
                {
                    UtilLog.LogWarningFormat("{0}->{1}:参数长度不匹配", lisenter.GetType(), messgae);
                }
            }
        }
    }
}