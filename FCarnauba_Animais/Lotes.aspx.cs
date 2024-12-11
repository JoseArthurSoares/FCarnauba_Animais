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

namespace FCarnauba_Animais
{
    public partial class Lotes : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private List<ResultadoBuscaLote> lotes;
        public event Action<int> DeleteLoteClick;
        string raca;

        public Lotes()
        {
            _PageType = new LotesType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                //FCarnaubaFacade.AtualizaDiasLactacao();

                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.SelectedValue = "8";
                ddlPropriedade.DataBind();

                ddlPropriedade.Items.RemoveAt(0);

                ParametrosDeBuscaEmLotes parametrosDeBusca = (ParametrosDeBuscaEmLotes)Session["parametrosDeBuscaEmLotes"];

                if (parametrosDeBusca != null)
                {
                    if (parametrosDeBusca.Raca == "SINDI")
                    {
                        btnSindi.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    if (parametrosDeBusca.Raca == "GUZERÁ")
                    {
                        btnGuzera.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    if (parametrosDeBusca.Raca == "CURRALEIRO PÉ DURO")
                    {
                        btnCpd.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    lotes = FCarnaubaFacade.ConsultaLote(parametrosDeBusca);

                    Session["var"] = lotes;

                    gridViewLotes.DataSource = lotes;
                    gridViewLotes.DataBind();
                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CheckAllowance();
            

            Session["parametrosDeBuscaEmLotes"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");
            

            //UpdateTitle();

            lotes = new List<ResultadoBuscaLote>();

            Session["raca"] = "";

            string propriedade = ddlPropriedade.SelectedValue;

            var parametrosDeBusca = new ParametrosDeBuscaEmLotes { TodosOsCampos = txtBusca.Text, IdPropriedade = propriedade };
            //parametrosDeBusca.Raca = (string)Session["raca"];
            

            lotes = FCarnaubaFacade.ConsultaLote(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotes"] = parametrosDeBusca;

            Session["var"] = lotes;

            gridViewLotes.DataSource = lotes;
            gridViewLotes.DataBind();

        }

        protected void btnGuzera_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmLotes"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            lotes = new List<ResultadoBuscaLote>();

            raca = "GUZERÁ";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmLotes { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            lotes = FCarnaubaFacade.ConsultaLote(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotes"] = parametrosDeBusca;

            Session["var"] = lotes;

            gridViewLotes.DataSource = lotes;
            gridViewLotes.DataBind();

            btnGuzera.BackColor = Color.FromArgb(5, 43, 92);
            

        }

        protected void btnSindi_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmLotes"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            lotes = new List<ResultadoBuscaLote>();

            raca = "SINDI";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmLotes { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            lotes = FCarnaubaFacade.ConsultaLote(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotes"] = parametrosDeBusca;

            Session["var"] = lotes;

            gridViewLotes.DataSource = lotes;
            gridViewLotes.DataBind();

            btnSindi.BackColor = Color.FromArgb(5, 43, 92);
        }

        protected void btnCpd_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmLotes"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            lotes = new List<ResultadoBuscaLote>();

            raca = "CURRALEIRO PÉ DURO";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmLotes { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            lotes = FCarnaubaFacade.ConsultaLote(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotes"] = parametrosDeBusca;

            Session["var"] = lotes;

            gridViewLotes.DataSource = lotes;
            gridViewLotes.DataBind();

            btnCpd.BackColor = Color.FromArgb(5, 43, 92);
        }

        protected void btnDeleteLote_Click(object sender, EventArgs e)
        {
           

                LinkButton btnDelete = (LinkButton)sender;
                int LoteId = Convert.ToInt32(btnDelete.CommandArgument);

                //DeleteControleLeiteiroClick(ControleLeiteiroId);

                if (LoteId > 0)
                {
                    {
                        _fCarnaubaFacade.RemoveLote(LoteId.ToString());
                        //this.UserControlCadastraControleLeiteiro1.DataSource = _fCarnaubaFacade.GetControlesById(loteId.ToString());
                        UpdateGridView();
                        //Response.Redirect("~/CadastrarLote.aspx?act=edit&loteId=" + loteId.ToString() + "&tabIndex=Controle Leiteiro");
                        //ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Leiteiro removido com sucesso");
                    }
                }



            
        }

        private void UpdateGridView()
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmLotes"] = null;

            lotes = new List<ResultadoBuscaLote>();

            raca = Session["raca"].ToString();

            var parametrosDeBusca = new ParametrosDeBuscaEmLotes { Raca = raca };

            lotes = FCarnaubaFacade.ConsultaLote(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotes"] = parametrosDeBusca;

            Session["var"] = lotes;

            gridViewLotes.DataSource = lotes;
            gridViewLotes.DataBind();
            
        }
    }
}