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
    public partial class RContLeiteiroEncFilhas : PaginaBase
    {
        public RContLeiteiroEncFilhas()
        {
            _PageType = new RContLeiteiroEncFilhasType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlAnimal.DataSource = _fCarnaubaFacade.GetAnimais();
                ddlAnimal.DataValueField = "Id";
                ddlAnimal.DataTextField = "NomeCompleto";
                ddlAnimal.DataBind();

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
            string idAnimal = ddlAnimal.SelectedValue;
            int ano = Convert.ToInt32(ddlAno.SelectedValue);

            reportViewer.Visible = true;

            ExibeRelatorio("Relatorios/ContLeiteiroEncFilhas.rdlc", "ProducaoLeite", _fCarnaubaFacade.ProducoesLeiteEnc(ano, idAnimal));

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