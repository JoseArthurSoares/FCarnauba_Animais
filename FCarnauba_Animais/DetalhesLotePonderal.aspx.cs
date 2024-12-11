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
    public partial class DetalhesLotePonderal : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public string SLote;

         public DetalhesLotePonderal()
        {
            _PageType = new DetalhesLotePonderalType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public string LotePonderalId
        {
            get
            {
                string lotePonderalId = Request.Params["lotePonderalId"];
                return lotePonderalId;
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
            AddToCurrentPath("<font color=#156AE9> <<-Lote Ponderal</font>", "LotesPonderais.aspx");
            UpdateTitleControlePonderal();

            LotePonderal lotePonderal = _fCarnaubaFacade.GetLotePonderalById(LotePonderalId);

            lblLotePonderal.Text = lotePonderal.SLote.ToString();
            lblDataLotePonderal.Text = lotePonderal.DataControle.ToShortDateString();
            lblPropriedade.Text = lotePonderal.NomePropriedade;
            lblControlador.Text = lotePonderal.Controlador;
            lblRaca.Text = lotePonderal.Raca;

            if (lotePonderal.LiberarLoteMensuracao)
            {
                lblLiberarLotePonderalMensuracao.Text = "Sim";
            }
            else
            {
                lblLiberarLotePonderalMensuracao.Text = "Não";
            }

            if (!String.IsNullOrEmpty(LotePonderalId))
            {
                //this.UserControlCadastraControleLeiteiro1.Controles = _fCarnaubaFacade.GetControlesById(LoteId);
                //Mensurações
                UserControlMensuracao1.LotePonderalId = Convert.ToInt32(LotePonderalId);
                UserControlMensuracao1.DataSource = lotePonderal.Mensuracoes;
                UserControlMensuracao1.AddMode = true;
                DataBind();
                UserControlMensuracao1.LoadDropDowns(lotePonderal.Raca);
            }
        }

        protected void DeleteControlePonderal_Click(int controleLeiteiroId)
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