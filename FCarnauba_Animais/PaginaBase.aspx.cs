using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.Account;
using FCarnauba_Animais.util;
using FCarnauba_Animais.UserControls;

namespace FCarnauba_Animais
{
    public class PaginaBase : Page
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
            return IsAdmin();
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

        public bool IsValidBusca()
        {
            return (Session["parametrosDeBuscaEmAnimais"] != null);
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

        public void UpdateTitle()
        {
            List<string> outStr = new List<string>();
            foreach (KeyValuePair<string, string> kv in TitleLink)
            {
                if (kv.Value != null)
                {
                    outStr.Add("<a href=\"" + kv.Value + "\" onclick='window.parent.scrollTo(0,0);'>" + kv.Key + "</a>");
                }
                else
                {
                    outStr.Add(kv.Key);
                }
            }
            var masterPage = ((SiteMaster)Master);
            masterPage.SetTitle(String.Join(" ", outStr));
        }

        public void UpdateTitleInicio()
        {
            List<string> outStr = new List<string>();
            foreach (KeyValuePair<string, string> kv in TitleLink)
            {
                if (kv.Value != null)
                {
                    outStr.Add("<a href=\"" + kv.Value + "\" onclick='window.parent.scrollTo(0,0);'>" + kv.Key + "</a>");
                }
                else
                {
                    outStr.Add(kv.Key);
                }
            }
            var masterPage = ((SiteInicio)Master);
            masterPage.SetTitle(String.Join(" ", outStr));
        }

        public void UpdateTitleControleLeiteiro()
        {
            List<string> outStr = new List<string>();
            foreach (KeyValuePair<string, string> kv in TitleLink)
            {
                if (kv.Value != null)
                {
                    outStr.Add("<a href=\"" + kv.Value + "\" onclick='window.parent.scrollTo(0,0);'>" + kv.Key + "</a>");
                }
                else
                {
                    outStr.Add(kv.Key);
                }
            }
            var masterPage = ((SiteControleLeiteiro)Master);
            masterPage.SetTitle(String.Join(" ", outStr));
        }

        public void UpdateTitleControlePonderal()
        {
            List<string> outStr = new List<string>();
            foreach (KeyValuePair<string, string> kv in TitleLink)
            {
                if (kv.Value != null)
                {
                    outStr.Add("<a href=\"" + kv.Value + "\" onclick='window.parent.scrollTo(0,0);'>" + kv.Key + "</a>");
                }
                else
                {
                    outStr.Add(kv.Key);
                }
            }
            var masterPage = ((SiteControlePonderal)Master);
            masterPage.SetTitle(String.Join(" ", outStr));
        }

        public void UpdateTitleControlePluviometrico()
        {
            List<string> outStr = new List<string>();
            foreach (KeyValuePair<string, string> kv in TitleLink)
            {
                if (kv.Value != null)
                {
                    outStr.Add("<a href=\"" + kv.Value + "\" onclick='window.parent.scrollTo(0,0);'>" + kv.Key + "</a>");
                }
                else
                {
                    outStr.Add(kv.Key);
                }
            }
            var masterPage = ((SitePluviometria)Master);
            masterPage.SetTitle(String.Join(" ", outStr));
        }

        public void UpdateTitleFluxoCaixa()
        {
            List<string> outStr = new List<string>();
            foreach (KeyValuePair<string, string> kv in TitleLink)
            {
                if (kv.Value != null)
                {
                    outStr.Add("<a href=\"" + kv.Value + "\" onclick='window.parent.scrollTo(0,0);'>" + kv.Key + "</a>");
                }
                else
                {
                    outStr.Add(kv.Key);
                }
            }
            var masterPage = ((SiteFluxoCaixa)Master);
            masterPage.SetTitle(String.Join(" ", outStr));
        }

        public void UpdateTitleCdc()
        {
            List<string> outStr = new List<string>();
            foreach (KeyValuePair<string, string> kv in TitleLink)
            {
                if (kv.Value != null)
                {
                    outStr.Add("<a href=\"" + kv.Value + "\" onclick='window.parent.scrollTo(0,0);'>" + kv.Key + "</a>");
                }
                else
                {
                    outStr.Add(kv.Key);
                }
            }
            var masterPage = ((SiteCDC)Master);
            masterPage.SetTitle(String.Join(" ", outStr));
        }

        public void UpdateTitleEstruturaPropriedade()
        {
            List<string> outStr = new List<string>();
            foreach (KeyValuePair<string, string> kv in TitleLink)
            {
                if (kv.Value != null)
                {
                    outStr.Add("<a href=\"" + kv.Value + "\" onclick='window.parent.scrollTo(0,0);'>" + kv.Key + "</a>");
                }
                else
                {
                    outStr.Add(kv.Key);
                }
            }
            var masterPage = ((SiteEstruturaPropriedade)Master);
            masterPage.SetTitle(String.Join(" ", outStr));
        }

        public void UpdateSearchArgument()
        {
            ((SiteMaster)Master).UpdateSearchArgument();
        }

        public void UpdateSearchArgumentInicio()
        {
            ((SiteInicio)Master).UpdateSearchArgument();
        }

        public void UpdateSearchArgumentControleLeiteiro()
        {
            ((SiteControleLeiteiro)Master).UpdateSearchArgument();
        }

        public void UpdateSearchArgumentControlePonderal()
        {
            ((SiteControlePonderal)Master).UpdateSearchArgument();
        }

        public void UpdateSearchArgumentCdc()
        {
            ((SiteCDC)Master).UpdateSearchArgument();
        }

        public void UpdateSearchArgumentEstruturaPropriedade()
        {
            ((SiteEstruturaPropriedade)Master).UpdateSearchArgument();
        }

        public bool ValidateTextSearch(string txt)
        {
            return ((SiteMaster)Master).ValidateTextSearch(txt);
        }

        public bool ValidateTextSearchInicio(string txt)
        {
            return ((SiteInicio)Master).ValidateTextSearch(txt);
        }

        public bool ValidateTextSearchControleLeiteiro(string txt)
        {
            return ((SiteControleLeiteiro)Master).ValidateTextSearch(txt);
        }

        public bool ValidateTextSearchControlePonderal(string txt)
        {
            return ((SiteControlePonderal)Master).ValidateTextSearch(txt);
        }

        public bool ValidateTextSearchControlePluviometrico(string txt)
        {
            return ((SitePluviometria)Master).ValidateTextSearch(txt);
        }

        public bool ValidateTextSearchFluxoCaixa(string txt)
        {
            return ((SiteFluxoCaixa)Master).ValidateTextSearch(txt);
        }

        public bool ValidateTextSearchCdc(string txt)
        {
            return ((SiteCDC)Master).ValidateTextSearch(txt);
        }

        public bool ValidateTextSearchEstruturaPropriedade(string txt)
        {
            return ((SiteEstruturaPropriedade)Master).ValidateTextSearch(txt);
        }

        public void ShowMessage(string message, bool isGood = true)
        {
            var masterPage = ((SiteMaster)Master);
            masterPage.ShowMessage(message, isGood);
        }

        public void ShowMessageInicio(string message, bool isGood = true)
        {
            var masterPage = ((SiteInicio)Master);
            masterPage.ShowMessage(message, isGood);
        }

        public void ShowMessageControleLeiteiro(string message, bool isGood = true)
        {
            var masterPage = ((SiteControleLeiteiro)Master);
            masterPage.ShowMessage(message, isGood);
        }

        public void ShowMessageControlePonderal(string message, bool isGood = true)
        {
            var masterPage = ((SiteControlePonderal)Master);
            masterPage.ShowMessage(message, isGood);
        }

        public void ShowMessageControlePluviometrico(string message, bool isGood = true)
        {
            var masterPage = ((SitePluviometria)Master);
            masterPage.ShowMessage(message, isGood);
        }

        public void ShowMessageFluxoCaixa(string message, bool isGood = true)
        {
            var masterPage = ((SiteFluxoCaixa)Master);
            masterPage.ShowMessage(message, isGood);
        }

        public void ShowMessageCdc(string message, bool isGood = true)
        {
            var masterPage = ((SiteCDC)Master);
            masterPage.ShowMessage(message, isGood);
        }

        public void ShowMessageEstruturaPropriedade(string message, bool isGood = true)
        {
            var masterPage = ((SiteEstruturaPropriedade)Master);
            masterPage.ShowMessage(message, isGood);
        }

        protected bool IsPostBackOrCallBack()
        {
            return this.IsPostBack || this.IsCallback;
        }

        public static CultureInfo CulturaPtBr;

        static PaginaBase()
        {
            CulturaPtBr = CultureInfo.CreateSpecificCulture("pt-br");
        }


        //public static string GetUserOrgao(User usuario)
        //{
        //    var orgaos = usuario.Groups.ToList();

        //    if (orgaos.Contains("GESTOR"))
        //        orgaos.Remove("GESTOR");

        //    orgaos.Remove("TODOS");
        //    return orgaos[0];
        //}

        //public string GetUserOrgao()
        //{
        //    return GetUserOrgao(UsuarioLogado);
        //}

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

        public class LogRestrictionPage : PaginaBase
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

        //protected virtual void TrataExcecao(string mensagemPadrao, Exception exc)
        //{
        //    //Log(exc);
        //    ExibeMensagem(TipoDeMensagem.Erro, mensagemPadrao + ": " + exc.Message);
        //}

        //protected void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        //{
        //    ExibeMensagem(tipo, textoDaMensagem);
        //}

    }
}