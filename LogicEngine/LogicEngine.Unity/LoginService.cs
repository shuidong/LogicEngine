using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicEngine.Unity
{
    //public class LoginService : AgeOfDiscovery.Part
    //{
    //    public UpgradeService upgradeService { get; private set; }

    //    protected override void _Awake()
    //    {
    //        upgradeService = AddPart<UpgradeService>();
    //    }
    //    protected override void _Release()
    //    {
    //    }
    //    public void Register(string username, string password)
    //    {
    //    }
    //    public bool CanLogin(string username, string password)
    //    {
    //        return false;
    //    }
    //    public void Login<T>(string username, string password)
    //       where T : Part, IRoleService, new()
    //    {
    //        if (CanLogin(username, password))
    //        {
    //        }
    //    }

    //    public class UpgradeService : Part
    //    {
    //        public float progress { get; private set; }

    //        protected override void _Awake()
    //        {
    //        }
    //        protected override void _Release()
    //        {
    //        }
    //    }
    //}
    //public class CLoginService : CPart<LoginService>
    //{
    //    protected override void _Awake()
    //    {
    //        part.upgradeService.AddListener(OnUpgradeChange);
    //    }
    //    protected override void _Release()
    //    {
    //        part.upgradeService.RemoveListener(OnUpgradeChange);
    //    }
    //    protected override void _OnChange()
    //    {
    //    }
    //    void OnUpgradeChange()
    //    {
    //        if (part.upgradeService.progress >= 1f)
    //        {
    //            //uiLogin.OpenLogin();
    //        }
    //        else
    //        {
    //            //uiLogin.SetProgress(loginService.upgradeService.progress);
    //        }
    //    }
    //}
}