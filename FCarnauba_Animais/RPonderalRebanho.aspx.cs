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
    public partial class RPonderalRebanho : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public RPonderalRebanho()
        {
            _PageType = new RPonderalRebanhoType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.DataBind();

                ddlPropriedade.Items.RemoveAt(0);
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
            string propriedade = ddlPropriedade.SelectedValue;
            string raca = ddlRaca.SelectedValue;

            DateTime dateValue;

            if ((DateTime.TryParse(txtDataInicio.Text, out dateValue) && txtDataInicio.Text != "__/__/____") && (DateTime.TryParse(txtDataFim.Text, out dateValue) && txtDataFim.Text != "__/__/____"))
            {
                DateTime dataInicio = Convert.ToDateTime(txtDataInicio.Text);
                DateTime dataFim = Convert.ToDateTime(txtDataFim.Text);

                ParametrosDeBuscaEmLotesPonderais parametros = new ParametrosDeBuscaEmLotesPonderais();
                parametros.IdPropriedade = propriedade;
                parametros.Raca = raca;
                parametros.DataControleInicial = dataInicio;
                parametros.DataControleFinal = dataFim;

                reportViewer.Visible = true;

                ExibeRelatorio("Relatorios/PonderalRebanho.rdlc", "RPeso", _fCarnaubaFacade.GetPesos(parametros));
            }
        }
    }
}