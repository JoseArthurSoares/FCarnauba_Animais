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
    public partial class GraficoCusteios : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public GraficoCusteios()
        {
            _PageType = new GraficoCusteiosType();
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
                ddlPropriedade.DataSource = FCarnaubaFacade.ObtemPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.DataBind();
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
            string dataInicial = txtDataInicio.Text;
            string dataFinal = txtDataFim.Text;

            CriterioPesquisaFinanceiro criterio = new CriterioPesquisaFinanceiro();

            criterio.PropriedadeComp = propriedade;


            try
            {
                criterio.DataInicio = Convert.ToDateTime(dataInicial);
                criterio.DataFim = Convert.ToDateTime(dataFinal);
            }
            catch
            {
                ExibeMensagem(TipoDeMensagem.Aviso, "Período informado inválido!");
                return;
            }

            ExibeRelatorio("Relatorios/GraficoCusteios.rdlc", "RItemFinanceiro", _fCarnaubaFacade.ObtemCustosItensFinanceiros(criterio));
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mes = ddlMes.SelectedValue;
            string dataInicial = null;
            string dataFinal = null;
            int ano = DateTime.Today.Year;

            switch (mes)
            {
                case "JANEIRO":
                    dataInicial = "01/01/" + ano;
                    dataFinal = "31/01/" + ano;
                    break;
                case "FEVEREIRO":
                    dataInicial = "01/02/" + ano;

                    if (DateTime.IsLeapYear(ano))
                    {
                        dataFinal = "29/02/" + ano;
                    }
                    else
                    {
                        dataFinal = "28/02/" + ano;
                    }
                    break;
                case "MARÇO":
                    dataInicial = "01/03/" + ano;
                    dataFinal = "31/03/" + ano;
                    break;
                case "ABRIL":
                    dataInicial = "01/04/" + ano;
                    dataFinal = "30/04/" + ano;
                    break;
                case "MAIO":
                    dataInicial = "01/05/" + ano;
                    dataFinal = "31/05/" + ano;
                    break;
                case "JUNHO":
                    dataInicial = "01/06/" + ano;
                    dataFinal = "30/06/" + ano;
                    break;
                case "JULHO":
                    dataInicial = "01/07/" + ano;
                    dataFinal = "31/07/" + ano;
                    break;
                case "AGOSTO":
                    dataInicial = "01/08/" + ano;
                    dataFinal = "31/08/" + ano;
                    break;
                case "SETEMBRO":
                    dataInicial = "01/09/" + ano;
                    dataFinal = "30/09/" + ano;
                    break;
                case "OUTUBRO":
                    dataInicial = "01/10/" + ano;
                    dataFinal = "31/10/" + ano;
                    break;
                case "NOVEMBRO":
                    dataInicial = "01/11/" + ano;
                    dataFinal = "30/11/" + ano;
                    break;
                case "DEZEMBRO":
                    dataInicial = "01/12/" + ano;
                    dataFinal = "31/12/" + ano;
                    break;
                default:
                    dataInicial = "01/01/" + ano;
                    dataFinal = "31/01/" + ano;
                    break;
            }

            txtDataInicio.Text = dataInicial;
            txtDataFim.Text = dataFinal;
        }
    }
}