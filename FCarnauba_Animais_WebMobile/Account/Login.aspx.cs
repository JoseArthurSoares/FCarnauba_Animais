using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais_WebMobile.util;

namespace FCarnauba_Animais_WebMobile.Account
{
    public partial class Login : PaginaBaseMobile
    {
        private string _imagem;
        private bool _enviarMensagem;

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

        public string Imagem
        {
            get
            {
                return _imagem;
            }
            set
            {
                _imagem = value;
            }
        }

        public bool EnviarMensagem
        {
            get
            {
                return _enviarMensagem;
            }
            set
            {
                _enviarMensagem = value;
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {

                var usuario = FCarnaubaFacade.AutenticaUsuario(txtUsuario.Text, txtSenha.Text);
                if (usuario.IsAuthenticated)
                {
                    Session["Pais"] = FCarnaubaFacade.GetPais();
                    Session["Maes"] = FCarnaubaFacade.GetMaes();
                    var curSessId = AuxLogin.SaveCurrentSession(usuario);
                    AuxLogin.SetCurrentSessionCookies(usuario, curSessId, this);

                    UsuarioLogado = usuario;
                    Response.Redirect("~/Principal.aspx");
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                //lblMessage.Text = ex.Message;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtSenha.Text = "";
        }
    }
}

