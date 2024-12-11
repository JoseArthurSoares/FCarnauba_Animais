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
    public partial class ControleLeiteiro : PaginaBase
    {

        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public ControleLeiteiro()
        {
            _PageType = new ControleLeiteiroType();
        }
       

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlLoteData.DataSource = _fCarnaubaFacade.GetLotes();
                ddlLoteData.DataValueField = "Id";
                ddlLoteData.DataTextField = "LoteDataPropriedade";
                ddlLoteData.DataBind();
                
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