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
    public partial class SRQ : PaginaBase
    {
        public SRQ()
        {
            _PageType = new SRQType();
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
            string propriedade = ddlPropriedade.SelectedValue;
            string raca = ddlRaca.SelectedValue;
            int ano = Convert.ToInt32(ddlAno.SelectedValue);

            reportViewer.Visible = true;

            ExibeRelatorio("Relatorios/RendimentoQueijeiroLac.rdlc", "ProducoesLeiteEnc", _fCarnaubaFacade.ProducoesLeiteEnc(ano, propriedade, raca, "RQueijeiro"));

        }

        public List<int> getAnos(int anoInicial)
        {
            int anoAtual = DateTime.Now.Year;
            var anos = new List<int>();

            for (int i = anoAtual; i >= anoInicial; i--)
            {
                anos.Add(i);
            }

            return anos;
        }
    }
}