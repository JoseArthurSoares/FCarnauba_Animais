using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais_WebMobile.Account;
using FCarnauba_Animais_WebMobile.util;

namespace FCarnauba_Animais_WebMobile
{
    public class PaginaBaseMobile : Page
    {
        protected FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private List<KeyValuePair<string, string>> TitleLink = new List<KeyValuePair<string, string>>();
        protected PageType _PageType;
        protected string NoAuthRedirect;
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

        }

        protected virtual bool AmIAllowed()
        {
            return UsuarioLogado.IsAuthenticated;
        }

        public void CheckAllowance()
        {
            if ((GetCurrentPageName() != "Login.aspx" && !IsValidUser()) || (IsValidUser() && !AmIAllowed()))
            {
                NoAuthRedirect = "~/Account/Login.aspx";
                Response.Redirect(NoAuthRedirect);
            }
        }

        public string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }

        public bool IsValidUser()
        {
            return CheckCredentials();
        }

        public bool IsAuthenticated()
        {
            return IsValidUser() && !UsuarioLogado.IsAuthenticated;
        }

        public bool IsAdmin()
        {
            return IsValidUser() && UsuarioLogado.IsAdm;
        }

        public FCarnaubaFacade FCarnaubaFacade
        {
            get { return _fCarnaubaFacade; }
        }

        public void AddToCurrentPath(string textString, string url = null)
        {
            TitleLink.Add(new KeyValuePair<string, string>(textString, url));
        }

        public void PopLastPath()
        {
            TitleLink.RemoveAt(TitleLink.Count - 1);
        }

        protected bool IsPostBackOrCallBack()
        {
            return this.IsPostBack || this.IsCallback;
        }

        public static CultureInfo CulturaPtBr;

        static PaginaBaseMobile()
        {
            CulturaPtBr = CultureInfo.CreateSpecificCulture("pt-br");
        }

        private User _UsuarioLogado;

        protected internal User UsuarioLogado
        {
            get
            {
                if (_UsuarioLogado == null)
                {
                    var userCookies = HttpContext.Current.Request.Cookies["user"];
                    if (userCookies != null && AuxLogin.VerifyUserCookies(userCookies["login"], userCookies["sessid"]))
                    {
                        _UsuarioLogado = AuxLogin.GetUserFromSession(userCookies["sessid"]);
                    }
                }
                return _UsuarioLogado;
            }
            set
            {
                _UsuarioLogado = value;
            }
        }

        public bool CheckCredentials()
        {
            return UsuarioLogado != null;
        }

        public class LogRestrictionPage : PaginaBaseMobile
        {
            public LogRestrictionPage()
            {
                NoAuthRedirect = "~/Account/Login.aspx";
            }

            protected override bool AmIAllowed()
            {
                return (UsuarioLogado != null);
                //return SigoFacade.IsUserAllowed((dynamic)_PageType, isLogged);
            }
        }


    }
}