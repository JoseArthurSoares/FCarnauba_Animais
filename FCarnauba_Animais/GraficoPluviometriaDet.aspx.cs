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
using FCarnauba_Animais.UserControls;

namespace FCarnauba_Animais
{
    public partial class GraficoPluviometriaDet : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public GraficoPluviometriaDet()
        {
            _PageType = new GraficoPluviometriaDetType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Nome";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.SelectedValue = "FAZENDA CARNAÚBA";
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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string propriedade = ddlPropriedade.SelectedValue;
            int ano = Convert.ToInt32(ddlAno.SelectedValue);

            ExibeRelatorio("Relatorios/GraficoPluviometriaDet.rdlc", "RPluviometria", _fCarnaubaFacade.GetPluviometriasDet(ano, propriedade));
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