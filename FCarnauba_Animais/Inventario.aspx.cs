using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.DataSources;
using FCarnauba_Animais.UserControls;
using FCarnauba_Animais.util;
using System.Globalization;
using Microsoft.Reporting.WebForms;

namespace FCarnauba_Animais
{
    public partial class Inventario : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public Propriedade propriedade;
        public EstruturaPropriedade estruturaPropriedade;
        public List<RPluviometria> pluviometrias;
        public InformacoesFinanceiras informacoesFinanceiras;

        public Inventario()
        {
            _PageType = new InventarioType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        protected virtual void TrataExcecao(string mensagemPadrao, Exception exc)
        {
            //Log(exc);
            ExibeMensagem(TipoDeMensagem.Erro, mensagemPadrao + ": " + exc.Message);
        }

        protected void ExibeMensagemDeStatus(string mensagemSucesso, string mensagemErro, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //Log(e.Exception);
                ExibeMensagem(TipoDeMensagem.Erro, mensagemErro);
                e.ExceptionHandled = true;
                return;
            }
            ExibeMensagem(TipoDeMensagem.Sucesso, mensagemSucesso);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.SelectedValue = "8";
                ddlPropriedade.DataBind();

                ddlPropriedade.Items.RemoveAt(0);
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            CultureInfo provider = new CultureInfo("pt-BR");
            string format = "d";

            string dateStringInicio = txtDataInicio.Text;
            DateTime dataInicio;

            string dateStringFim = txtDataFim.Text;
            DateTime dataFim;

            string anoPecuario;

            try
            {
                dataInicio = DateTime.ParseExact(dateStringInicio, format, provider);
                dataFim = DateTime.ParseExact(dateStringFim, format, provider);
            }
            catch (FormatException)
            {
                //ExibeMensagem(TipoDeMensagem.Aviso, "Informe datas válidas!");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Informe datas válidas!');", true);
                return;
            }

            int anoInicio = dataInicio.Year;
            int anoFim = dataInicio.Year + 1;
            anoPecuario = anoInicio + "-" + anoFim;

            ZerarLabels();

            string idPropriedade = ddlPropriedade.SelectedValue;


            propriedade = _fCarnaubaFacade.ObtemPropriedadeCompleta(Convert.ToInt32(idPropriedade));

            if (propriedade.Area > 0)
            {
                lblArea.Text = Math.Round(Convert.ToDecimal(propriedade.Area), 2).ToString("N2");
            }

            lblLocalizacao.Text = propriedade.Localizacao;

            //string idEstPropriedade = _fCarnaubaFacade.GetIdEstruturaPropriedade(idPropriedade);
            estruturaPropriedade = _fCarnaubaFacade.GetEstruturaPropriedade(idPropriedade, dataInicio, dataFim);

            if (!String.IsNullOrEmpty(estruturaPropriedade.Id.ToString()))
            {

                double totalAgricultura = 0;
                double totalPastagens = 0;

                estruturaPropriedade = _fCarnaubaFacade.GetEstruturaPropriedade(idPropriedade, dataInicio, dataFim);

                if (estruturaPropriedade.Agriculturas.Count > 0)
                {
                    totalAgricultura = estruturaPropriedade.Agriculturas[estruturaPropriedade.Agriculturas.Count - 1].AreaTotalAcumulada;
                    lblAgricultura.Text = Math.Round(Convert.ToDecimal(totalAgricultura), 2).ToString("N2");
                }
                else
                {
                    lblAgricultura.Text = "0,00";
                }

                if (estruturaPropriedade.Pastagens.Count > 0)
                {
                    totalPastagens = estruturaPropriedade.Pastagens[estruturaPropriedade.Pastagens.Count - 1].AreaTotalAcumulada;
                    lblPastagens.Text = Math.Round(Convert.ToDecimal(totalPastagens), 2).ToString("N2");
                }
                else
                {
                    lblPastagens.Text = "0,00";
                }

            }
            else
            {
                lblAgricultura.Text = "0,00";
                lblPastagens.Text = "0,00";
            }

            string nomePropriedade = _fCarnaubaFacade.GetNomePropriedade(idPropriedade);

            pluviometrias = _fCarnaubaFacade.GetPluviometrias(dataInicio, dataFim, nomePropriedade);

            if (pluviometrias.Count > 0)
            {
                double totalPluviometria = pluviometrias.Sum(item => item.Pluviometria);
                lblPluviometria.Text = Math.Round(Convert.ToDecimal(totalPluviometria), 2).ToString("N2");

                ExibeRelatorio(reportViewerPluviometria, "Relatorios/GraficoPluviometriaDetInv.rdlc", "RPluviometria", _fCarnaubaFacade.GetPluviometriasDet(dataInicio, dataFim, nomePropriedade));

            }

            CriterioPesquisaFinanceiro criterio = new CriterioPesquisaFinanceiro();
            criterio.DataInicio = dataInicio;
            criterio.DataFim = dataFim;
            criterio.PropriedadeComp = idPropriedade;

            informacoesFinanceiras = _fCarnaubaFacade.ObtemInformacoesFinanceiras(criterio);

            if (informacoesFinanceiras != null)
            {
                lblEntradas.Text = Math.Round(Convert.ToDecimal(informacoesFinanceiras.Entradas), 2).ToString("N2");
                lblDesembolsos.Text = Math.Round(Convert.ToDecimal(informacoesFinanceiras.Desembolsos), 2).ToString("N2");
                lblCustosFixos.Text = Math.Round(Convert.ToDecimal(informacoesFinanceiras.CustosFixos), 2).ToString("N2");
                lblCustosVariaveis.Text = Math.Round(Convert.ToDecimal(informacoesFinanceiras.CustosVariaveis), 2).ToString("N2");
                lblCustoAdministrativo.Text = Math.Round(Convert.ToDecimal(informacoesFinanceiras.CustoAdmintrativo), 2).ToString("N2");
                lblCustoTributario.Text = Math.Round(Convert.ToDecimal(informacoesFinanceiras.CustoTributario), 2).ToString("N2");
                lblCustoAlimentar.Text = Math.Round(Convert.ToDecimal(informacoesFinanceiras.CustoAlimentar), 2).ToString("N2");
            }

            InformacoesRebanho informacoesRebanhosGuzera = _fCarnaubaFacade.GetInformacoesRebanho("GUZERÁ", nomePropriedade);
            InformacoesRebanho informacoesRebanhosSindi = _fCarnaubaFacade.GetInformacoesRebanho("SINDI", nomePropriedade);
            InformacoesRebanho informacoesRebanhosCpd = _fCarnaubaFacade.GetInformacoesRebanho("CURRALEIRO PÉ DURO", nomePropriedade);

            if (informacoesRebanhosGuzera != null)
                lblGuzera.Text = informacoesRebanhosGuzera.TotalAnimais.ToString();

            if (informacoesRebanhosSindi != null)
                lblSindi.Text = informacoesRebanhosSindi.TotalAnimais.ToString();

            if (informacoesRebanhosCpd != null)
                lblCpd.Text = informacoesRebanhosCpd.TotalAnimais.ToString();

            List<IndiceZootecnico> lotacao = _fCarnaubaFacade.LotacaoMediaAnual(dataInicio, dataFim, "*", idPropriedade);

            if (lotacao.Count > 0)
            {
                lblLotacao.Text = Math.Round(Convert.ToDecimal(lotacao[0].Valor), 2).ToString("N2");
            }

            List<IndiceZootecnico> taxaFertilidade = _fCarnaubaFacade.IndiceFertilidade(dataInicio, dataFim, "*", idPropriedade);

            if (taxaFertilidade.Count > 0)
            {
                lblTaxaFertilidade.Text = Math.Round(Convert.ToDecimal(taxaFertilidade[0].Valor), 2).ToString("N2");
            }

            List<RProducaoLeite> producoes = _fCarnaubaFacade.GetControleProducaoLeiteiroTotal(dataInicio, dataFim, idPropriedade);
            double totalLeite = producoes.Sum(item => item.ProducaoAcumulada);
            lblProcucaoLeite.Text = Math.Round(totalLeite, 2).ToString("N2");

            double apLeiteVenda = 0;
            double apLeitePerReceita = 0;
            double apLeitePerCusto = 0;
            if (informacoesFinanceiras != null)
            {
                apLeiteVenda = totalLeite * informacoesFinanceiras.VendaLeite;

                if (informacoesFinanceiras.Entradas > 0)
                {
                    apLeitePerReceita = (apLeiteVenda / informacoesFinanceiras.Entradas) * 100;
                }

                if (informacoesFinanceiras.Desembolsos > 0)
                {
                    apLeitePerCusto = (apLeiteVenda / informacoesFinanceiras.Desembolsos) * 100;
                }

            }
            lblApLeite.Text = Math.Round(Convert.ToDecimal(apLeiteVenda), 2).ToString("N2");
            lblApLeitePerReceita.Text = Math.Round(Convert.ToDecimal(apLeitePerReceita), 2).ToString("N2");
            lblApLeitePerCusto.Text = Math.Round(Convert.ToDecimal(apLeitePerCusto), 2).ToString("N2");

            double[] gmds = _fCarnaubaFacade.GetTodosGmds(dataInicio, dataFim, idPropriedade);

            if (gmds.Length > 0)
            {

                double gmdGlobal = Math.Round(gmds.Average(), 2);
                lblGmdGlobal.Text = Math.Round(gmdGlobal, 2).ToString("N2");
            }
            else
            {
                lblGmdGlobal.Text = "0,00";
            }

            List<IndiceZootecnico> taxaCrescimentoVegetativoSindi = _fCarnaubaFacade.TaxaCrescimentoVegetativo(dataInicio, dataFim, "SINDI", idPropriedade);

            if (taxaCrescimentoVegetativoSindi.Count > 0)
            {
                lblTaxaCrescimentoVegetativoSindi.Text = Math.Round(Convert.ToDecimal(taxaCrescimentoVegetativoSindi[0].Valor), 2).ToString("N2");
            }
            else
            {
                lblTaxaCrescimentoVegetativoSindi.Text = "0,00";
            }

            List<IndiceZootecnico> taxaCrescimentoVegetativoGuzera = _fCarnaubaFacade.TaxaCrescimentoVegetativo(dataInicio, dataFim, "GUZERÁ", idPropriedade);

            if (taxaCrescimentoVegetativoGuzera.Count > 0)
            {
                lblTaxaCrescimentoVegetativoGuzera.Text = Math.Round(Convert.ToDecimal(taxaCrescimentoVegetativoGuzera[0].Valor), 2).ToString("N2");
            }
            else
            {
                lblTaxaCrescimentoVegetativoGuzera.Text = "0,00";
            }

            List<IndiceZootecnico> taxaCrescimentoVegetativoCPD = _fCarnaubaFacade.TaxaCrescimentoVegetativo(dataInicio, dataFim, "CURRALEIRO PÉ DURO", idPropriedade);

            if (taxaCrescimentoVegetativoCPD.Count > 0)
            {
                lblTaxaCrescimentoVegetativoCPD.Text = Math.Round(Convert.ToDecimal(taxaCrescimentoVegetativoCPD[0].Valor), 2).ToString("N2");
            }
            else
            {
                lblTaxaCrescimentoVegetativoCPD.Text = "0,00";
            }

            List<IndiceZootecnico> taxaDesfrute = _fCarnaubaFacade.TaxaDesfrute(dataInicio, dataFim, "*", idPropriedade);

            if (taxaDesfrute.Count > 0)
            {
                lblTaxaDesfrute.Text = Math.Round(Convert.ToDecimal(taxaDesfrute[0].Valor), 2).ToString("N2");
            }
            else
            {
                lblTaxaDesfrute.Text = "0,00";
            }

            List<IndiceZootecnico> taxaDesmame = _fCarnaubaFacade.TaxaDesmame(anoPecuario, "*", idPropriedade);

            if (taxaDesmame.Count > 0)
            {
                lblTaxaDesmame.Text = Math.Round(Convert.ToDecimal(taxaDesmame[0].Valor), 2).ToString("N2");
            }
            else
            {
                lblTaxaDesmame.Text = "0,00";
            }

            List<IndiceZootecnico> taxaNatalidade = _fCarnaubaFacade.TaxaNatalidade(anoPecuario, "*", idPropriedade);

            if (taxaNatalidade.Count > 0)
            {
                lblTaxaNatalidade.Text = Math.Round(Convert.ToDecimal(taxaNatalidade[0].Valor), 2).ToString("N2");
            }
            else
            {
                lblTaxaNatalidade.Text = "0,00";
            }

            List<IndiceZootecnico> taxaMortalidadeBezerros = _fCarnaubaFacade.TaxaMortalidade(dataInicio, dataFim, "*", idPropriedade, "BEZERROS");

            if (taxaMortalidadeBezerros.Count > 0)
            {
                lbltaxaMortalidadeBezerros.Text = Math.Round(Convert.ToDecimal(taxaMortalidadeBezerros[0].Valor), 2).ToString("N2");
            }
            else
            {
                lbltaxaMortalidadeBezerros.Text = "0,00";
            }

            List<IndiceZootecnico> taxaMortalidadeAnimaisJovens = _fCarnaubaFacade.TaxaMortalidade(dataInicio, dataFim, "*", idPropriedade, "JOVENS");

            if (taxaMortalidadeAnimaisJovens.Count > 0)
            {
                lbltaxaMortalidadeAnimaisJovens.Text = Math.Round(Convert.ToDecimal(taxaMortalidadeAnimaisJovens[0].Valor), 2).ToString("N2");
            }
            else
            {
                lbltaxaMortalidadeAnimaisJovens.Text = "0,00";
            }

            List<IndiceZootecnico> taxaMortalidadeAnimaisAdultos = _fCarnaubaFacade.TaxaMortalidade(dataInicio, dataFim, "*", idPropriedade, "ADULTOS");

            if (taxaMortalidadeAnimaisAdultos.Count > 0)
            {
                lbltaxaMortalidadeAnimaisAdultos.Text = Math.Round(Convert.ToDecimal(taxaMortalidadeAnimaisAdultos[0].Valor), 2).ToString("N2");
            }
            else
            {
                lbltaxaMortalidadeAnimaisAdultos.Text = "0,00";
            }

            List<IndiceZootecnico> RQMedio = _fCarnaubaFacade.RQMedio(dataInicio, dataFim, "*", idPropriedade);

            if (RQMedio.Count > 0)
            {
                lblRQMedio.Text = Math.Round(Convert.ToDecimal(RQMedio[0].Valor), 2).ToString("N2");
            }
            else
            {
                lblRQMedio.Text = "0,00";
            }
        }

        protected void ZerarLabels()
        {
            lblLocalizacao.Text = "";
            lblArea.Text = "";
            lblAgricultura.Text = "";
            lblPastagens.Text = "";
            lblPluviometria.Text = "";

            lblGuzera.Text = "";
            lblSindi.Text = "";
            lblCpd.Text = "";

            lblLotacao.Text = "";
        }

        private void ExibeRelatorio(ReportViewer reportViewer, string caminhoRelatorio, string dataSourceName, object dataSource)
        {
            reportViewer.Reset();

            reportViewer.LocalReport.ReportPath = caminhoRelatorio;


            ReportDataSource reportDataSource = new ReportDataSource(dataSourceName, dataSource);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
        }
    }
}