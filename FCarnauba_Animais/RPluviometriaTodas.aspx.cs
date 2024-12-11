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
    public partial class RPluviometriaTodas : PaginaBase
    {

        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public RPluviometriaTodas()
        {
            _PageType = new RPluviometriaTodasType();
        }

        public List<string> getAnos(int anoInicial)
        {
            int anoAtual = DateTime.Now.Year;
            var anos = new List<string>();

            anos.Add("");

            for (int i = anoAtual; i >= anoInicial; i--)
            {
                anos.Add(i.ToString());
            }

            return anos;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlAno.DataSource = getAnos(1960);
                ddlAno.DataBind();
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
            try
            {
                DateTime dataInicio = Convert.ToDateTime(txtDataInicio.Text);
                DateTime dataFim = Convert.ToDateTime(txtDataFim.Text);

                ExibeRelatorio("Relatorios/PluviometriaTodas.rdlc", "RPluviometria", _fCarnaubaFacade.GetTodasPluviometrias(dataInicio, dataFim));
            }
            catch (Exception)
            {
            }
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ano = ddlAno.SelectedValue;
            string dataInicial = null;
            string dataFinal = null;

            dataInicial = "01/01/" + ano;
            dataFinal = "31/12/" + ano;

            txtDataInicio.Text = dataInicial;
            txtDataFim.Text = dataFinal;
        }
    }
}