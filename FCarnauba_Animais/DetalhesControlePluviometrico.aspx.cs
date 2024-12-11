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
    public partial class DetalhesControlePluviometrico : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DetalhesControlePluviometrico()
        {
            _PageType = new DetalhesControlePluviometricoType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public string ControlePluviometricoId
        {
            get
            {
                string controlePluviometricoId = Request.Params["controlePluviometricoId"];
                return controlePluviometricoId;
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
            

            AddToCurrentPath("<font color=#156AE9> <<-Pluviometria</font>", "Pluviometria.aspx");
            UpdateTitleControlePluviometrico();

            ControlePluviometrico controlePluviometrico = _fCarnaubaFacade.GetControlePluviometricoById(ControlePluviometricoId);

            lblPropriedade.Text = controlePluviometrico.Diretorio;
            lblData.Text = controlePluviometrico.Data.ToShortDateString();
            lblPluviometro.Text = controlePluviometrico.Pluviometro;
            lblPluviometria.Text = Math.Round(Convert.ToDecimal(controlePluviometrico.Pluviometria), 2).ToString();
        }
    }
}