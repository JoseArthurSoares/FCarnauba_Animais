using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.util;



namespace FCarnauba_Animais.Account
{
    public partial class Login : PaginaBase
    {
        public string Op
        {
            get
            {
                string op = Request.Params["op"];
                return op;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Op == "sair" && (IsValidUser()))
            {
                AuxLogin.EndCurrentSession(Request.Cookies["user"]["sessid"], Response);
                Session["HashTable"] = null;
                Session["Pais"] = null;
                Session["Maes"] = null;
            }
            
        }

        protected void btnEntra_Click(object sender, EventArgs e)
        {
            try
            {

                var usuario = FCarnaubaFacade.AutenticaUsuario(txtUser.Text, txtPassword.Text);
                if (usuario.IsAuthenticated)
                {
                    if (!usuario.IsAdm)
                    {
                        lblMessage.Text = "Usuário sem permissão de acesso";
                    }
                    else
                    {

                        Session["Pais"] = FCarnaubaFacade.GetPais();
                        Session["Maes"] = FCarnaubaFacade.GetMaes();
                        var curSessId = AuxLogin.SaveCurrentSession(usuario);
                        AuxLogin.SetCurrentSessionCookies(usuario, curSessId, this);

                        UsuarioLogado = usuario;
                        Response.Redirect("~/Inicio.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Erro na autenticação";
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnConvidado_Click(object sender, EventArgs e)
        {
            try
            {
                btnConvidado.Text = "Convidado";
                var usuario = FCarnaubaFacade.AutenticaUsuario("convidado", "convidado");
                if (usuario.IsAuthenticated)
                {
                    var curSessId = AuxLogin.SaveCurrentSession(usuario);
                    AuxLogin.SetCurrentSessionCookies(usuario, curSessId, this);

                    UsuarioLogado = usuario;
                    Response.Redirect("~/Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                //this.LoginUser.FailureText = ex.Message;
            }

        }
    }
}
