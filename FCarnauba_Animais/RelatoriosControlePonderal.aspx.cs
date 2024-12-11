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
    public partial class RelatoriosControlePonderal : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public string lotePonderalId = null;
        public string animalId = null;

        public RelatoriosControlePonderal()
        {
            _PageType = new RelatoriosControlePonderalType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lotePonderalId = Request.Params["lotePonderalId"];
            animalId = Request.Params["animalId"];

            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlLoteData.DataSource = _fCarnaubaFacade.GetLotesPonderais();
                ddlLoteData.DataValueField = "Id";
                ddlLoteData.DataTextField = "LoteDataPropriedade";
                ddlLoteData.DataBind();

                if (!String.IsNullOrEmpty(lotePonderalId))
                {
                    ExibeRelatorio("Relatorios/ControlePonderalPes.rdlc", "RMensuracao", _fCarnaubaFacade.GetMensuracoesPes(Convert.ToInt32(lotePonderalId)));
                }

                if (!String.IsNullOrEmpty(animalId))
                {
                    long ultimoLote = _fCarnaubaFacade.UltimoLotePonderal(animalId);
                    var lote = _fCarnaubaFacade.GetLotePonderalById(ultimoLote.ToString());

                    reportViewer.Reset();
                    reportViewer.LocalReport.ReportPath = "Relatorios/PonderalIndividual.rdlc";

                    ReportDataSource reportDataSourcePesos = new ReportDataSource("RMensuracao", _fCarnaubaFacade.GetMensuracoes(lote.DataControle, animalId));

                    if (lote.Id <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Esse animal não tem RPI');</script>", false);
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
            int loteData = Convert.ToInt32(ddlLoteData.SelectedValue);

            ExibeRelatorio("Relatorios/ControlePonderalPes.rdlc", "RMensuracao", _fCarnaubaFacade.GetMensuracoesPes(loteData));
        }
    }
}