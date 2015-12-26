//======================================================
// Create by @Peng Guang Hui
// 2015/11/30 14:27:28
//======================================================
using System;
using System.Collections.Generic;

namespace LogicEngine
{
    interface ILocalCmd
    {
        void Execute();
    }
    public interface ICmd
    {
        void SetPart(ICmdPart part);
        long GetID();
        //void Execute();
    }
    public interface ICmdPart
    {
        long id { get; }
    }

    [Serializable]
    public abstract class Cmd<TCmdPart> : ICmd, ILocalCmd
        where TCmdPart : class, ICmdPart
    {
        /// <summary>
        /// ICmdPart's id
        /// </summary>
        public long id;
        [NonSerialized]
        ICmdPart part;
        [NonSerialized]
        internal CmdRoute cmdRoute;

        long ICmd.GetID()
        {
            return id;
        }
        void ICmd.SetPart(ICmdPart part)
        {
            this.id = part.id;
            this.part = part;
        }
        public void Execute()
        {
            switch (GetMode())
            {
                case CmdMode.All:
                    LocalExecute();
                    cmdRoute.SendCmd(this);
                    break;
                case CmdMode.Source:
                    LocalExecute();
                    break;
                case CmdMode.Target:
                    cmdRoute.SendCmd(this);
                    break;
                case CmdMode.Obsolete:
                    UtilLog.LogError(GetType().Name + " is Obsolete.");
                    break;
            }
        }
        void ILocalCmd.Execute()
        {
            switch (GetMode())
            {
                case CmdMode.All:
                    LocalExecute();
                    break;
                case CmdMode.Source:
                    UtilLog.LogError(GetType().Name + " 不应该发送出来");
                    break;
                case CmdMode.Target:
                    LocalExecute();
                    break;
                case CmdMode.Obsolete:
                    UtilLog.LogError(GetType().Name + " is Obsolete.");
                    break;
            }
        }
        void LocalExecute()
        {
            TCmdPart tpart = part as TCmdPart;
            if (tpart == null)
            {
                if (part == null)
                {
                    UtilLog.LogError("Cmd.Execute类型错误:part is null");
                }
                else
                {
                    UtilLog.LogError("Cmd.Execute类型错误:"+ part.GetType().Name);
                }
                return;
            }
            _Execute(tpart);
        }
        CmdMode GetMode()
        {
            var posts = Attribute.GetCustomAttributes(GetType(), inConstants.CmdPostAttribute);
            return posts.Length == 0 ? CmdMode.All : (posts[0] as CmdPostAttribute).Mode;
        }
        protected abstract void _Execute(TCmdPart part);
    }


    public enum CmdMode
    {
        All,
        Source,
        Target,
        Obsolete
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class CmdPostAttribute : Attribute
    {
        public CmdMode Mode { get; private set; }

        public CmdPostAttribute(CmdMode mode)
        {
            Mode = mode;
        }
    }
}