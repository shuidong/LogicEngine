//======================================================
// Create by @Peng Guang Hui
// 2015/12/2 18:23:54
//======================================================
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LogicEngine.Reflection
{
    class BeanMgr
    {
        Dictionary<Type, Bean> mBeans = new Dictionary<Type, Bean>();
        Dictionary<ushort, Bean> mBeans2 = new Dictionary<ushort, Bean>();

        public void AddBean(ushort bean_id, Type target)
        { 
            var bean = new Bean(target);
            mBeans2.Add(bean_id, bean);
            mBeans.Add(target, bean);
        }

        public byte[] GetBytes(object value)
        {
            Bean bean;
            if (mBeans.TryGetValue(value.GetType(), out bean))
            {
                return bean.GetBytes(value);
            }
            throw new KeyNotFoundException();
        }
        public object GetValue(byte[] bytes)
        {
            var bean_id = BitConverter.ToUInt16(bytes, 0);
            Bean bean;
            if (mBeans2.TryGetValue(bean_id, out bean))
            {
                return bean.GetValue(bytes);
            }
            throw new KeyNotFoundException();
        }
    }


    class Bean
    {
        int mLength;
        FieldInfo[] mFields;

        public Bean(Type type)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
        }
        public byte[] GetBytes(object value)
        {
            byte[] values = new byte[mLength];

            foreach (var it in mFields)
            {
                var v = it.GetValue(value);
            }

            return values;
        }
        public object GetValue(byte[] bytes)
        {
            return null;
        }
    }
}