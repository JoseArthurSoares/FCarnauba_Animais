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
    public partial class RelatoriosVendasAnimais : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public RelatoriosVendasAnimais()
        {
            _PageType = new RelatoriosVendasAnimaisType();
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
                ddlCliente.DataSource = FCarnaubaFacade.ObtemClientes();
                ddlCliente.DataValueField = "IdEmpresa";
                ddlCliente.DataTextField = "RazaoSocial";
                ddlCliente.DataBind();
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
            string cliente = ddlCliente.SelectedValue;
            string dataInicial = txtDataInicio.Text;
            string dataFinal = txtDataFim.Text;

            CriterioPesquisaFinanceiro criterio = new CriterioPesquisaFinanceiro();

            if (!String.IsNullOrEmpty(ddlCliente.SelectedValue))
                criterio.IdEmpresa = Convert.ToInt32(ddlCliente.SelectedValue);


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

            ExibeRelatorio("Relatorios/RelatorioVendasAnimais.rdlc", "RItemFinanceiro", _fCarnaubaFacade.ObtemVendasAnimais(criterio));
        }
    }
}