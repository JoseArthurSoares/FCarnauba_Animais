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
    public partial class DetalhesLote : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public string SLote;
        private ControleLeiteiro controleLeiteiro;
        

        public DetalhesLote()
        {
            _PageType = new DetalhesLoteType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public string LoteId
        {
            get
            {
                string loteId = Request.Params["loteId"];
                return loteId;
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

            //AddToCurrentPath("<img src='./img/contratoSR.png' />");
            //AddToCurrentPath("<font color=#156AE9>" + "CGE " + rCge + "</font>");
            //UpdateTitle();

            AddToCurrentPath("<font color=#156AE9> <<-Lote</font>", "Lotes.aspx");
            UpdateTitleControleLeiteiro();

            Lote lote = _fCarnaubaFacade.GetLoteById(LoteId);

            lblLote.Text = lote.SLote.ToString();
            lblDataLote.Text = lote.DataControle.ToShortDateString();
            lblPropriedade.Text = lote.NomePropriedade;
            lblCategoria.Text = lote.Categoria;
            lblPOrdenha.Text = lote.POrdenha;
            lblSOrdenha.Text = lote.SOrdenha;
            lblTOrdenha.Text = lote.TOrdenha;
            lblControlador.Text = lote.Controlador;
            lblRaca.Text = lote.Raca;

            if (lote.LiberarLotePesagem)
            {
                lblLiberarLotePesagem.Text = "Sim";
            }
            else
            {
                lblLiberarLotePesagem.Text = "Não";
            }

            //this.UserControlMatrizes1.DataSource = lote.Matrizes;

            if (!String.IsNullOrEmpty(LoteId))
            {
                //this.UserControlCadastraControleLeiteiro1.Controles = _fCarnaubaFacade.GetControlesById(LoteId);
                //Produção de Leite
                UserControlProducaoLeite1.LoteId = Convert.ToInt32(LoteId);
                UserControlProducaoLeite1.DataSource = lote.ProducoesLeite;
                UserControlProducaoLeite1.AddMode = true;
                DataBind();
                //UserControlProducaoLeite1.LoadDropDowns(lote.Raca);
            }
        }

        protected void DeleteControleLeiteiro_Click(int controleLeiteiroId)
        {
            //if (!String.IsNullOrEmpty(controleLeiteiroId.ToString()))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
            //                                        String.Format("setActive('{0}');", "Controle Leiteiro"), true);

            //    _fCarnaubaFacade.RemoveControleLeiteiro(controleLeiteiroId.ToString());
            //    ExibeMensagem(TipoDeMensagem.Sucesso, "Lote remov com sucesso");
            //    this.UserControlCadastraControleLeiteiro1.DataSource = _fCarnaubaFacade.GetControlesById(LoteId);


            //    //Response.Redirect("~/CadastrarLote.aspx?act=edit&loteId=" + LoteId + "&tabIndex=#controleleiteiro-tab");
            //}
        }
    }
}