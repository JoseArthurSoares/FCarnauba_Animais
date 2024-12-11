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
    public partial class RelatoriosFluxoCaixa : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public RelatoriosFluxoCaixa()
        {
            _PageType = new RelatoriosFluxoCaixaType();
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
            int ano = Convert.ToInt32(ddlAno.SelectedValue);
            string mes = ddlMes.SelectedValue;
            string dataInicial = null;
            string dataFinal = null;

            ParametrosDeBuscaEmFluxoCaixa parametrosDeBuscaEmFluxoCaixa = new ParametrosDeBuscaEmFluxoCaixa();

            parametrosDeBuscaEmFluxoCaixa.IdPropriedade = propriedade;

            switch (mes)
            {
                case "Janeiro":
                    dataInicial = "01/01/" + ano;
                    dataFinal = "31/01/" + ano;
                    break;
                case "Fevereiro":
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
                case "Março":
                    dataInicial = "01/03/" + ano;
                    dataFinal = "31/03/" + ano;
                    break;
                case "Abril":
                    dataInicial = "01/04/" + ano;
                    dataFinal = "30/04/" + ano;
                    break;
                case "Maio":
                    dataInicial = "01/05/" + ano;
                    dataFinal = "31/05/" + ano;
                    break;
                case "Junho":
                    dataInicial = "01/06/" + ano;
                    dataFinal = "30/06/" + ano;
                    break;
                case "Julho":
                    dataInicial = "01/07/" + ano;
                    dataFinal = "31/07/" + ano;
                    break;
                case "Agosto":
                    dataInicial = "01/08/" + ano;
                    dataFinal = "31/08/" + ano;
                    break;
                case "Setembro":
                    dataInicial = "01/09/" + ano;
                    dataFinal = "30/09/" + ano;
                    break;
                case "Outubro":
                    dataInicial = "01/10/" + ano;
                    dataFinal = "31/10/" + ano;
                    break;
                case "Novembro":
                    dataInicial = "01/11/" + ano;
                    dataFinal = "30/11/" + ano;
                    break;
                case "Dezembro":
                    dataInicial = "01/12/" + ano;
                    dataFinal = "31/12/" + ano;
                    break;
                default:
                    dataInicial = "01/01/" + ano;
                    dataFinal = "31/01/" + ano;
                    break;
            }

            parametrosDeBuscaEmFluxoCaixa.DataInicial = Convert.ToDateTime(dataInicial);
            parametrosDeBuscaEmFluxoCaixa.DataFinal = Convert.ToDateTime(dataFinal);

            ExibeRelatorio("Relatorios/FluxoCaixa.rdlc", "ResultadoBuscaFluxoCaixa", _fCarnaubaFacade.ConsultaFluxoCaixa(parametrosDeBuscaEmFluxoCaixa));
        }
    }
}