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
using Microsoft.Reporting.WebForms;

namespace FCarnauba_Animais
{
    public partial class FluxosCaixa : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private List<ResultadoBuscaFluxoCaixa> fluxos;
        public event Action<int> DeleteFluxoCaixaClick;
        string propriedade;

        public FluxosCaixa()
        {
            _PageType = new FluxosCaixaType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {

                //ddlAno.DataSource = getAnos(1960);
                //ddlAno.DataBind();

                ParametrosDeBuscaEmFluxoCaixa parametrosDeBusca = (ParametrosDeBuscaEmFluxoCaixa)Session["parametrosDeBuscaEmFluxoCaixa"];

                if (parametrosDeBusca != null)
                {
                    //if (parametrosDeBusca.DataInicial != null)
                    //    ddlAno.SelectedValue = parametrosDeBusca.DataInicial.ToString().Substring(6, 4);

                    fluxos = FCarnaubaFacade.ConsultaFluxoCaixa(parametrosDeBusca);

                    Session["var"] = fluxos;

                    gridViewFluxosCaixas.DataSource = fluxos;
                    gridViewFluxosCaixas.DataBind();
                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CheckAllowance();


            Session["parametrosDeBuscaEmFluxoCaixa"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");


            //UpdateTitle();

            fluxos = new List<ResultadoBuscaFluxoCaixa>();

            var parametrosDeBusca = new ParametrosDeBuscaEmFluxoCaixa { TodosOsCampos = txtBusca.Text };

            //string sDataInicial = "01/01/" + ddlAno.SelectedValue;
            //string sDataFinal = "31/12/" + ddlAno.SelectedValue;
            //DateTime? dataInicial = Convert.ToDateTime(sDataInicial);
            //DateTime? dataFinal = Convert.ToDateTime(sDataFinal);
            //parametrosDeBusca.DataInicial = dataInicial;
            //parametrosDeBusca.DataFinal = dataFinal;

            parametrosDeBusca.Diretorio = propriedade;


            fluxos = FCarnaubaFacade.ConsultaFluxoCaixa(parametrosDeBusca);

            Session["parametrosDeBuscaEmFluxoCaixa"] = parametrosDeBusca;

            Session["var"] = fluxos;

            gridViewFluxosCaixas.DataSource = fluxos;
            gridViewFluxosCaixas.DataBind();
        }

        protected void btnDeleteFluxoCaixa_Click(object sender, EventArgs e)
        {
            LinkButton btnDelete = (LinkButton)sender;
            int FluxoCaixaId = Convert.ToInt32(btnDelete.CommandArgument);

            if (FluxoCaixaId > 0)
            {
                {
                    _fCarnaubaFacade.RemoveFluxoCaixa(FluxoCaixaId.ToString());
                    UpdateGridView();
                }
            }
        }

        private void UpdateGridView()
        {
            CheckAllowance();

            //Session["parametrosDeBuscaEmFluxoCaixa"] = null;

            fluxos = new List<ResultadoBuscaFluxoCaixa>();

            //propriedade = Session["propriedade"].ToString();

            ParametrosDeBuscaEmFluxoCaixa parametrosDeBusca = (ParametrosDeBuscaEmFluxoCaixa)Session["parametrosDeBuscaEmFluxoCaixa"];

            if (parametrosDeBusca != null)
            {

                fluxos = FCarnaubaFacade.ConsultaFluxoCaixa(parametrosDeBusca);

                Session["parametrosDeBuscaEmFluxoCaixa"] = parametrosDeBusca;

                Session["var"] = fluxos;

                gridViewFluxosCaixas.DataSource = fluxos;
                gridViewFluxosCaixas.DataBind();
            }

        }
    }
}