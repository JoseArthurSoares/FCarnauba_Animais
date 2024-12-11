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
    public partial class RelatoriosControleLeiteiro : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public string loteId = null;
        public string loteIdPes = null;
        public string matrizId = null;

        public RelatoriosControleLeiteiro()
        {
            _PageType = new RelatoriosControleLeiteiroType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            loteId = Request.Params["loteId"];
            loteIdPes = Request.Params["loteIdPes"];
            matrizId = Request.Params["matrizId"];

            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlLoteData.DataSource = _fCarnaubaFacade.GetLotes();
                ddlLoteData.DataValueField = "Id";
                ddlLoteData.DataTextField = "LoteDataPropriedade";
                ddlLoteData.DataBind();

                if (!String.IsNullOrEmpty(loteId))
                {
                    ExibeRelatorio("Relatorios/ControleLeiteiro.rdlc", "RProducaoLeite", _fCarnaubaFacade.GetControleProducaoLeiteiroData(Convert.ToInt32(loteId)));
                }

                if (!String.IsNullOrEmpty(loteIdPes))
                {
                    ExibeRelatorio("Relatorios/ControleLeiteiroPes.rdlc", "RProducaoLeite", _fCarnaubaFacade.GetControleProducaoLeiteiroData(Convert.ToInt32(loteIdPes)));
                }

                if (!String.IsNullOrEmpty(matrizId))
                {
                    long ultimoLote = _fCarnaubaFacade.UltimoLote(matrizId);
                    var lote = _fCarnaubaFacade.GetLoteById(ultimoLote.ToString());

                    reportViewer.Reset();
                    reportViewer.LocalReport.ReportPath = "Relatorios/IndividualLactacao.rdlc";

                    ReportDataSource reportDataSourcePesos = new ReportDataSource("RProducaoLeite", _fCarnaubaFacade.GetPesagens(lote.DataControle, matrizId));

                    if (lote.Id <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Esse animal não tem RIL');</script>", false);
                        reportViewer.Visible = false;
                        
                    }

                    else
                    {
                        reportViewer.LocalReport.DataSources.Clear();
                        reportViewer.LocalReport.DataSources.Add(reportDataSourcePesos);
                    }
                }



            }
        }

        private void ExibeRelatorio(string caminhoRelatorio, string dataSourceName, object dataSource)
        {
            reportViewer.Reset();

            reportViewer.LocalReport.ReportPath = caminhoRelatorio;


            ReportDataSource reportDataSource = new ReportDataSource(dataSourceName, dataSource);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            long loteData = Convert.ToInt32(ddlLoteData.SelectedValue);

            ExibeRelatorio("Relatorios/ControleLeiteiro.rdlc", "RProducaoLeite", _fCarnaubaFacade.GetControleProducaoLeiteiroData(loteData));
        }
    }
}