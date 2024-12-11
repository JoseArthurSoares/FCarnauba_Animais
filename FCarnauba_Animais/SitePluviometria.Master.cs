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
    public partial class SitePluviometria : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (NotificationPanel.Visible == true) NotificationPanel.Visible = false;

            try
            {

                var usuario = AuxLogin.GetUserFromSession(Request.Cookies["user"]["sessid"]);
                if (usuario != null)
                {
                    //this.lnkSair.Visible = true;
                    ////this.LoginName.Text = "Bem vindo, <span class=\"bold\">" + usuario.Name + "</span>";
                    //this.LoginName.Text = "Bem vindo ";
                    //if (!usuario.IsAdm)
                    //{
                    //    NavigationMenu.Items.Remove(NavigationMenu.FindItem("Admin"));
                    //    //NavigationMenu.Items.Remove(NavigationMenu.FindItem("Obras"));
                    //    //NavigationMenu.Items.Remove(NavigationMenu.FindItem("Medições"));
                    //    //NavigationMenu.Items.Remove(NavigationMenu.FindItem("Arquivo Siaf"));

                    //    if (usuario.HasGroup("GESTOR"))
                    //    {
                    //        NavigationMenu.Items.Remove(NavigationMenu.FindItem("Admin"));
                    //        NavigationMenu.Items.Remove(NavigationMenu.FindItem("Cadastrar"));
                    //        //NavigationMenu.FindItem("Obras").ChildItems.Remove(NavigationMenu.FindItem("Obras/Cadastrar"));
                    //        //NavigationMenu.Items.Remove(NavigationMenu.FindItem("ArquivoSiaf"));
                    //        //NavigationMenu.FindItem("Admin").ChildItems.Remove(NavigationMenu.FindItem("Admin/Integração GeoPB"));
                    //    }
                    //}


                    //if (usuario.HasGroup("GESTOR") && (usuario.IsAdm))
                    //{

                    //    NavigationMenu.Items.Remove(NavigationMenu.FindItem("Admin"));
                    //    //NavigationMenu.Items.Remove(NavigationMenu.FindItem("Mudar Senha"));
                    //    //NavigationMenu.FindItem("Ajuda").ChildItems.Remove(NavigationMenu.FindItem("Ajuda/Mudar Senha"));
                    //    NavigationMenu.Items.Remove(NavigationMenu.FindItem("Cadastrar"));
                    //    //NavigationMenu.FindItem("Obras").ChildItems.Remove(NavigationMenu.FindItem("Obras/Cadastrar"));
                    //    //NavigationMenu.Items.Remove(NavigationMenu.FindItem("ArquivoSiaf"));
                    //    //NavigationMenu.FindItem("Admin").ChildItems.Remove(NavigationMenu.FindItem("Admin/Integração GeoPB"));
                    //}

                    //if (!usuario.HasGroup("ADMIN"))
                    //    NavigationMenu.Items.Remove(NavigationMenu.FindItem("Integração"));

                    //NavigationMenu.FindItem("Consultas").ChildItems.Remove(NavigationMenu.FindItem("Consultas/Por Órgão"));
                    //NavigationMenu.FindItem("Consultas").ChildItems.Remove(NavigationMenu.FindItem("Consultas/Por Empresa"));
                    //NavigationMenu.FindItem("Consultas").ChildItems.Remove(NavigationMenu.FindItem("Consultas/Por Cidade"));
                    //NavigationMenu.FindItem("Consultas").ChildItems.Remove(NavigationMenu.FindItem("Consultas/Por Situação"));
                    //NavigationMenu.FindItem("Consultas").ChildItems.Remove(NavigationMenu.FindItem("Consultas/Por Data"));
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

                //AuxiliarDeSessao.SetUsuarioLogado(this.Page, null);
                Session["HashTable"] = null;
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void lnkSair_Click(object sender, EventArgs e)
        {
            AuxLogin.EndCurrentSession(Request.Cookies["user"]["sessid"], Response);

            //AuxiliarDeSessao.SetUsuarioLogado(this.Page, null);
            Session["HashTable"] = null;
            Response.Redirect("~/Account/Login.aspx");
        }

        public void SetTitle(string message)
        {
            lblSubTitle.Text = message;
        }

        public void UpdateSearchArgument()
        {
            if (Session["parametrosDeBuscaEmLotes"] == null)
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

                //case "Sair":
                //    {
                //        //e.Item.Target = "_parent";
                //        AuxLogin.EndCurrentSession(Request.Cookies["user"]["sessid"], Response);

                //        //AuxiliarDeSessao.SetUsuarioLogado(this.Page, null);
                //        Session["HashTable"] = null;
                //        Response.Redirect("~/Account/Login.aspx");
                //        break;
                //    }
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
            //Response.Redirect("Relatorio.aspx");
        }
    }
}