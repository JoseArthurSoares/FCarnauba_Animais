using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.util;

namespace FCarnauba_Animais
{
    public partial class SiteControlePonderal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                var usuario = AuxLogin.GetUserFromSession(Request.Cookies["user"]["sessid"]);
                if (usuario != null)
                {
        
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (Request.Params["msg"] != null)
            {
                ShowMessage(Request.Params["msg"].ToString(), true);
            }
        }

        protected void menu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Value == "Sair")
            {
                AuxLogin.EndCurrentSession(Request.Cookies["user"]["sessid"], Response);

                Session["HashTable"] = null;
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void lnkSair_Click(object sender, EventArgs e)
        {
            AuxLogin.EndCurrentSession(Request.Cookies["user"]["sessid"], Response);

            Session["HashTable"] = null;
            Response.Redirect("~/Account/Login.aspx");
        }

        public void SetTitle(string message)
        {
            lblSubTitle.Text = message;
        }

        public void UpdateSearchArgument()
        {
            if (Session["parametrosDeBuscaEmLotesPonderais"] == null)
            {
                if (Session["searchText"] != null && !String.IsNullOrEmpty(Session["searchText"].ToString()))
                {
                    var textStr = Session["searchText"].ToString();
                    if (!ValidateTextSearch(textStr))
                    {
                        Session["searchText"] = null;
                        ShowMessage("Parâmetros de busca inválidos.", false);
                    }
                    return;
                }
                Session["searchText"] = "*";
            }
        }

        public void ShowMessage(string message, bool isGood)
        {
            NotificationPanel.Visible = true;
            NotificationLabel.Text = message;
            if (isGood)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "setCorrectColor", "setCorrectColor(true)", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "setCorrectColor", "setCorrectColor(false)", true);
            }
        }

        public bool ValidateTextSearch(string str)
        {
            return (!str.Contains('"') && !str.Contains('#') && !str.Contains('@') && !str.Contains(';') &&
                    !str.Contains('\'') && !str.Contains('\\'));
        }


        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {

                case "Por Órgão":
                    {
                        UpdateSearchArgument();
                        Session["tipoRelatorio"] = "orgao";
                        break;
                    }
                case "Por Empresa":
                    {
                        UpdateSearchArgument();
                        Session["tipoRelatorio"] = "empresa";
                        break;
                    }
                case "Por Cidade":
                    {
                        UpdateSearchArgument();
                        Session["tipoRelatorio"] = "cidade";
                        break;
                    }
                case "Por Situação":
                    {
                        UpdateSearchArgument();
                        Session["tipoRelatorio"] = "situacao";
                        break;
                    }
            }
            
        }
    }
}