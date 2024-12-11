using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.util;
using LightInfocon.GoldenAccess.General;
using System.Globalization;
using Color = System.Drawing.Color;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace FCarnauba_Animais
{
    public partial class Cdcs : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private List<ResultadoBuscaCdc> cdcs;
        string raca;

        public Cdcs()
        {
            _PageType = new CdcsType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.SelectedValue = "8";
                ddlPropriedade.DataBind();

                ddlPropriedade.Items.RemoveAt(0);

                ParametrosDeBuscaEmCdc parametrosDeBusca = (ParametrosDeBuscaEmCdc)Session["parametrosDeBuscaEmCdc"];

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

                    cdcs = FCarnaubaFacade.ConsultaCdc(parametrosDeBusca);

                    Session["var"] = cdcs;

                    gridViewCdcs.DataSource = cdcs;
                    gridViewCdcs.DataBind();
                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CheckAllowance();


            Session["parametrosDeBuscaEmCdc"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");


            //UpdateTitle();

            cdcs = new List<ResultadoBuscaCdc>();

            Session["raca"] = "";

            string propriedade = ddlPropriedade.SelectedValue;

            var parametrosDeBusca = new ParametrosDeBuscaEmCdc { TodosOsCampos = txtBusca.Text, IdPropriedade = propriedade };
            //parametrosDeBusca.Raca = raca;


            cdcs = FCarnaubaFacade.ConsultaCdc(parametrosDeBusca);

            Session["parametrosDeBuscaEmCdc"] = parametrosDeBusca;

            Session["var"] = cdcs;

            gridViewCdcs.DataSource = cdcs;
            gridViewCdcs.DataBind();

        }

        protected void btnGuzera_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmCdc"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            cdcs = new List<ResultadoBuscaCdc>();

            raca = "GUZERÁ";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmCdc { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            cdcs = FCarnaubaFacade.ConsultaCdc(parametrosDeBusca);

            Session["parametrosDeBuscaEmCdc"] = parametrosDeBusca;

            Session["var"] = cdcs;

            gridViewCdcs.DataSource = cdcs;
            gridViewCdcs.DataBind();

            btnGuzera.BackColor = Color.FromArgb(5, 43, 92);


        }

        protected void btnSindi_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmCdc"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            cdcs = new List<ResultadoBuscaCdc>();

            raca = "SINDI";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmCdc { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            cdcs = FCarnaubaFacade.ConsultaCdc(parametrosDeBusca);

            Session["parametrosDeBuscaEmCdc"] = parametrosDeBusca;

            Session["var"] = cdcs;

            gridViewCdcs.DataSource = cdcs;
            gridViewCdcs.DataBind();

            btnSindi.BackColor = Color.FromArgb(5, 43, 92);
        }

        protected void btnCpd_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmCdc"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            cdcs = new List<ResultadoBuscaCdc>();

            raca = "CURRALEIRO PÉ DURO";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmCdc { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            cdcs = FCarnaubaFacade.ConsultaCdc(parametrosDeBusca);

            Session["parametrosDeBuscaEmCdc"] = parametrosDeBusca;

            Session["var"] = cdcs;

            gridViewCdcs.DataSource = cdcs;
            gridViewCdcs.DataBind();

            btnCpd.BackColor = Color.FromArgb(5, 43, 92);
        }

        protected void btnDeleteCdc_Click(object sender, EventArgs e)
        {


            LinkButton btnDelete = (LinkButton)sender;
            int CdcId = Convert.ToInt32(btnDelete.CommandArgument);

            //DeleteControleLeiteiroClick(ControleLeiteiroId);

            if (CdcId > 0)
            {
                {
                    _fCarnaubaFacade.RemoveCdc(CdcId.ToString());
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

            Session["parametrosDeBuscaEmCdc"] = null;

            cdcs = new List<ResultadoBuscaCdc>();

            raca = Session["raca"].ToString();

            var parametrosDeBusca = new ParametrosDeBuscaEmCdc { Raca = raca };

            cdcs = FCarnaubaFacade.ConsultaCdc(parametrosDeBusca);

            Session["parametrosDeBuscaEmCdc"] = parametrosDeBusca;

            Session["var"] = cdcs;

            gridViewCdcs.DataSource = cdcs;
            gridViewCdcs.DataBind();

        }
    }
}