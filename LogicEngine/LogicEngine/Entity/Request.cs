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
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class RequestPostAttribute : Attribute
    {
        public long ID { get; private set; }
        public RequestPostAttribute(long id)
        {
            ID = id;
        }
    }
    public abstract class Request
    {
        [NonSerialized]
        internal IPeer peer;

        internal bool TryVerify(ref string error)
        {
            return _TryVerify(ref error);
        }
        protected abstract bool _TryVerify(ref string error);
        internal abstract Response inCreateResponseError(string error);

        public void Excute()
        {
            //peer.SendJson(UtilJson.ToJson(this));
            throw new NotImplementedException();
        }

        public class Response
        {
            public string error;

            public bool IsCorrect()
            {
                return string.IsNullOrEmpty(error);
            }
            public bool IsError()
            {
                return !IsCorrect();
            }
        }
    }

    public abstract class Request<T> : Request where T : Request.Response, new()
    {
        public T CreateResponse()
        {
            return new T();
        }
        public T CreateResponseError(string error)
        {
            var response = new T();
            response.error = "请求{" + GetType().Name + "}出错:" + error;
            return response;
        }
        internal override Response inCreateResponseError(string error)
        {
            return CreateResponseError(error);
        }
    }
}
