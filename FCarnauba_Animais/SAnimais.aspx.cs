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
    public partial class SAnimais : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public SAnimais()
        {
            _PageType = new SAnimaisType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                if (Session["Pais"] == null)
                {

                    Session["Pais"] = _fCarnaubaFacade.GetPais();

                }


                if (Session["Maes"] == null)
                {
                    Session["Maes"] = _fCarnaubaFacade.GetPais();

                }

                ddlPai.DataSource = Session["Pais"];
                ddlPai.DataValueField = "Id";
                ddlPai.DataTextField = "NomeCompleto";
                ddlPai.DataBind();

                ddlMae.DataSource = Session["Maes"];
                ddlMae.DataValueField = "Id";
                ddlMae.DataTextField = "NomeCompleto";
                ddlMae.DataBind();

                ddlPropriedade.DataSource = _fCarnaubaFacade.GetSimplesPropriedades();
                ddlPropriedade.DataBind();
                ddlPropriedade.Items.Insert(0, new ListItem("", null));
                ddlPropriedade.SelectedIndex = -1;

                ddlRaca.Items.Insert(0, new ListItem("", null));
                ddlRaca.SelectedIndex = -1;

                var parametrosDeBusca = new ParametrosDeBuscaEmAnimais { TodosOsCampos = "*" };

            }
            else if (Request["__EVENTTARGET"] == "btnBuscaAvancadaOK_Click") btnBuscaAvancadaOK_Click(null, null);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            var parametrosDeBusca = new ParametrosDeBuscaEmAnimais { TodosOsCampos = txtBusca.Text };

            Animais(parametrosDeBusca);

        }


        protected void btnBuscaAvancadaOK_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            var parametrosDeBusca = new ParametrosDeBuscaEmAnimais();

            parametrosDeBusca.Nome = txtNome.Text;
            parametrosDeBusca.Rgd = txtRgd.Text;
            parametrosDeBusca.Sexo = ddlSexo.SelectedValue;

            if (ddlPai.SelectedValue != "0")
                parametrosDeBusca.StrPaiId = ddlPai.SelectedValue;
            if (ddlMae.SelectedValue != "0")
                parametrosDeBusca.StrMaeId = ddlMae.SelectedValue;
            parametrosDeBusca.Raca = ddlRaca.SelectedValue;


            try
            {
                parametrosDeBusca.DataNascimentoInicial = Convert.ToDateTime(Request.Form["calNascimentoInicial"], CulturaPtBr);
                parametrosDeBusca.DataNascimentoFinal = Convert.ToDateTime(Request.Form["calNascimentoFinal"], CulturaPtBr);
            }
            catch
            {
                parametrosDeBusca.DataNascimentoInicial = null;
                parametrosDeBusca.DataNascimentoFinal = null;
            }

            if (ddlPropriedade.SelectedValue != "")
                parametrosDeBusca.NomeFazenda = ddlPropriedade.SelectedValue;

            if (!String.IsNullOrEmpty(txtOrdem.Text))
            {
                parametrosDeBusca.NumeroOrdem = Convert.ToInt32(txtOrdem.Text);
            }

            if (ddlBetaCaseina.SelectedValue != "")
                parametrosDeBusca.BetaCaseina = ddlBetaCaseina.SelectedValue;

            if (ddlKappaCaseina.SelectedValue != "")
                parametrosDeBusca.KappaCaseina = ddlKappaCaseina.SelectedValue;

            if (ddlFiv.SelectedValue == "Sim")
            {
                parametrosDeBusca.Fiv = true;
            }
            else
            {
                parametrosDeBusca.Fiv = false;
            }

            if (ddlMovimento.SelectedValue != "")
                parametrosDeBusca.Movimento = ddlMovimento.SelectedValue;

            parametrosDeBusca.Observacao = txtObservacao.Text;

            Animais(parametrosDeBusca);


        }


        protected void Animais(ParametrosDeBuscaEmAnimais parametrosDeBusca)
        {

            ExibeRelatorio("Relatorios/Animais.rdlc", "ResultadoBuscaAnimal", _fCarnaubaFacade.ConsultaAnimal(parametrosDeBusca));
        }

        private void ExibeRelatorio(string caminhoRelatorio, string dataSourceName, object dataSource)
        {
            reportViewer.Reset();

            reportViewer.LocalReport.ReportPath = caminhoRelatorio;


            ReportDataSource reportDataSource = new ReportDataSource(dataSourceName, dataSource);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
        }
    }
}