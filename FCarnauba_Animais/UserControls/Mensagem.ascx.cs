using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FCarnauba_Animais.UserControls
{
    public partial class Mensagem : UserControl
    {
        public void ExibeMensagem(TipoDeMensagem tipo, string mensagem)
        {
            lblMensagem.Text = mensagem;
            imgMensagem.ImageUrl = "~/img/" + tipo + ".png";
            lblTituloMensagem.Text = tipo.ToString();
            mpeMensagem.Show();
        }
    }

    public enum TipoDeMensagem { Aviso, Erro, Sucesso }
}