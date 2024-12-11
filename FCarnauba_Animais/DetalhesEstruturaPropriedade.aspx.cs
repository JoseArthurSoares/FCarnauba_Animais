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
    public partial class DetalhesEstruturaPropriedade : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DetalhesEstruturaPropriedade()
        {
            _PageType = new DetalhesEstruturaPropriedadeType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public string EstruturaPropriedadeId
        {
            get
            {
                string estruturaPropriedadeId = Request.Params["estruturaPropriedadeId"];
                return estruturaPropriedadeId;
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
            AddToCurrentPath("<font color=#156AE9> <<-Propriedades</font>", "EstruturasPropriedades.aspx");
            UpdateTitleEstruturaPropriedade();

            EstruturaPropriedade estruturaPropriedade = _fCarnaubaFacade.GetEstruturaPropriedadeById(EstruturaPropriedadeId);

            lblData.Text = estruturaPropriedade.Data.ToShortDateString();
            lblPropriedade.Text = estruturaPropriedade.NomePropriedade;

            lblTotalPastagens.Text = Math.Round(Convert.ToDecimal(estruturaPropriedade.TotalPastagens), 2).ToString();
            lblTotalAgricultura.Text = Math.Round(Convert.ToDecimal(estruturaPropriedade.TotalAgricultura), 2).ToString();

            lblBenfeitorias.Text = Math.Round(Convert.ToDecimal(estruturaPropriedade.TotalBenfeitoria), 2).ToString();
            lblArrendamentos.Text = Math.Round(Convert.ToDecimal(estruturaPropriedade.TotalArrendamento), 2).ToString();
            lblReserva.Text = Math.Round(Convert.ToDecimal(estruturaPropriedade.Reserva), 2).ToString();
            //lblPalmaForrageira.Text = Math.Round(Convert.ToDecimal(estruturaPropriedade.PalmaForrageira), 2).ToString();
            lblOutras.Text = Math.Round(Convert.ToDecimal(estruturaPropriedade.TotalOutras), 2).ToString();
            

            if (!String.IsNullOrEmpty(EstruturaPropriedadeId))
            {
                //this.UserControlCadastraControleLeiteiro1.Controles = _fCarnaubaFacade.GetControlesById(LoteId);
                //Mensurações
                UserControlPastagem1.EstruturaPropriedadeId = Convert.ToInt32(EstruturaPropriedadeId);
                UserControlPastagem1.DataSource = estruturaPropriedade.Pastagens;
                UserControlPastagem1.AddMode = true;

                UserControlAgricultura1.EstruturaPropriedadeId = Convert.ToInt32(EstruturaPropriedadeId);
                UserControlAgricultura1.DataSource = estruturaPropriedade.Agriculturas;
                UserControlAgricultura1.AddMode = true;

                UserControlBenfeitoria1.EstruturaPropriedadeId = Convert.ToInt32(EstruturaPropriedadeId);
                UserControlBenfeitoria1.DataSource = estruturaPropriedade.Benfeitorias;
                UserControlBenfeitoria1.AddMode = true;

                UserControlArrendamento1.EstruturaPropriedadeId = Convert.ToInt32(EstruturaPropriedadeId);
                UserControlArrendamento1.DataSource = estruturaPropriedade.Arrendamentos;
                UserControlArrendamento1.AddMode = true;

                UserControlOutra1.EstruturaPropriedadeId = Convert.ToInt32(EstruturaPropriedadeId);
                UserControlOutra1.DataSource = estruturaPropriedade.Outras;
                UserControlOutra1.AddMode = true;

                DataBind();
                //UserControlPastagem1.LoadDropDowns(lotePonderal.Raca);
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