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
    public partial class RTaxaNatalidade : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public RTaxaNatalidade()
        {
            _PageType = new RTaxaNatalidadeType();
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

                ddlAnoPecuario.DataSource = getAnosPecuarios(1960);
                ddlAnoPecuario.DataBind();


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
            string anoPecuario = ddlAnoPecuario.SelectedValue;

            reportViewer.Visible = true;

            ExibeRelatorio("Relatorios/TaxaNatalidade.rdlc", "TaxaNatalidade", _fCarnaubaFacade.TaxaNatalidade(anoPecuario, raca, propriedade));

        }

        public List<string> getAnosPecuarios(int anoInicial)
        {
            int ultimoAno = DateTime.Now.Year;
            var anosPecuarios = new List<string>();

            for (int i = ultimoAno; i >= anoInicial; i--)
            {
                string anoPecuario = i + "-" + Convert.ToUInt32(i + 1);
                anosPecuarios.Add(anoPecuario);
            }

            return anosPecuarios;
        }
    }
}