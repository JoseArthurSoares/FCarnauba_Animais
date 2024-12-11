using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais_WebMobile.util;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais_WebMobile
{
    public partial class Principal : PaginaBaseMobile
    {
        public Principal()
        {
            _PageType = new PrincipalType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            AuxLogin.EndCurrentSession(Request.Cookies["user"]["sessid"], Response);

            //AuxiliarDeSessao.SetUsuarioLogado(this.Page, null);
            Session["HashTable"] = null;
            Response.Redirect("~/Account/Login.aspx");
        }
    }
}