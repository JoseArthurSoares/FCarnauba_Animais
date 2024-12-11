using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.util;
using LightInfocon.GoldenAccess.General;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;
using Image = iTextSharp.text.Image;
using ListItem = System.Web.UI.WebControls.ListItem;
using FCarnauba_Animais.UserControls;

namespace FCarnauba_Animais
{
    public partial class DetalhesFluxoCaixa : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DetalhesFluxoCaixa()
        {
            _PageType = new DetalhesFluxoCaixaType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public string FluxoCaixaId
        {
            get
            {
                string fluxoCaixaId = Request.Params["fluxoCaixaId"];
                return fluxoCaixaId;
            }
        }

        public string Act
        {
            get
            {
                string act = Request.Params["act"];
                return act;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AddToCurrentPath("<font color=#156AE9> <<-Fluxo de Caixa</font>", "FluxosCaixa.aspx");
            UpdateTitleFluxoCaixa();

            FluxoCaixa fluxoCaixa = _fCarnaubaFacade.GetFluxoCaixaById(FluxoCaixaId);

            lblPropriedade.Text = fluxoCaixa.NomePropriedade;
            lblCentroCusto.Text = fluxoCaixa.DescricaoCentroCusto;
            lblData.Text = fluxoCaixa.Data.ToShortDateString();
            lblTipo.Text = fluxoCaixa.Tipo;
            lblDescricao.Text = fluxoCaixa.Descricao;
            lblValor.Text = Math.Round(Convert.ToDecimal(fluxoCaixa.Valor), 2).ToString();
            lblValor.Text = String.Format("{0:N2}", fluxoCaixa.Valor);

        }
    }
}