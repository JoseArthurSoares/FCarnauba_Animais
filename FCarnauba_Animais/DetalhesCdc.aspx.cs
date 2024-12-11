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
    public partial class DetalhesCdc : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public long NCdc;
        //private ControleLeiteiro controleLeiteiro;

        public DetalhesCdc()
        {
            _PageType = new DetalhesCdcType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public string CdcId
        {
            get
            {
                string cdcId = Request.Params["cdcId"];
                return cdcId;
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
            AddToCurrentPath("<font color=#156AE9> <<-Cruzamentos</font>", "Cdcs.aspx");
            UpdateTitleCdc();

            Cdc cdc = _fCarnaubaFacade.GetCdcById(CdcId);

            lblCdc.Text = cdc.NCdc.ToString();
            lblDataCobertura.Text = cdc.DataCobertura.ToShortDateString();
            lblTipo.Text = cdc.Tipo;
            lblRaca.Text = cdc.Raca;
            lblTouro.Text = cdc.NomeTouro;
            lblPropriedade.Text = cdc.NomePropriedade;
            lblVeterinario.Text = cdc.Veterinario;

            if (!String.IsNullOrEmpty(CdcId))
            {
                UserControlCdcMatrizes1.CdcId = Convert.ToInt32(CdcId);
                UserControlCdcMatrizes1.DataSource = cdc.Matrizes;
                UserControlCdcMatrizes1.AddMode = true;
                DataBind();
                UserControlCdcMatrizes1.LoadDropDowns();
            }
        }
    }
}