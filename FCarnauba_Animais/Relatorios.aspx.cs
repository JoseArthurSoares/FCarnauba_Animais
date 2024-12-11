using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class Relatorios : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private Animal _animal;
        private List<ResultadoBuscaAnimal> animais;
        private List<RRankingPeso> rankingsPeso;
        private List<RRankingFilhos> rankingsFilhos;

        public string Rel
        {
            get
            {
                string rel = Request.Params["rel"];
                return rel;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBackOrCallBack())
            {
                if (Session["parametrosDeBuscaEmAnimais"] != null)
                {
                    switch (Rel)
                    {
                        case "1":
                            Animais();
                            break;
                        case "2":
                            RankingPeso();
                            break;
                        case "3":
                            RankingFilhos();
                            break;
                        case "4":
                            RankingIpp();
                            break;
                        case "5":
                            RankingIep();
                            break;
                        case "6":
                            RankingEr();
                            break;
                        case "7":
                            AnimaisAno();
                            break;
                        case "8":
                            FilhosRaca();
                            break;
                        case "9":
                            ERRaca();
                            break;
                        case "10":
                            AnimaisLactacaoAno();
                            break;
                        case "11":
                            RankingPeso();
                            break;
                        case "12":
                            RankingProducaoDiariaMaxima();
                            break;
                        case "13":
                            RankingProducaoAcumulada();
                            break;
                        default:
                            Response.Redirect("~/Animais.aspx");
                            break;
                    }
                }
                else
                {
                    Response.Redirect("~/Animais.aspx");
                }
            }

        }

        protected new bool IsPostBackOrCallBack()
        {
            return this.IsPostBack || this.IsCallback;
        }

        private void ExibeRelatorio(string caminhoRelatorio, string dataSourceName, object dataSource)
        {
            reportViewer.Reset();

            reportViewer.LocalReport.ReportPath = caminhoRelatorio;


            ReportDataSource reportDataSource = new ReportDataSource(dataSourceName, dataSource);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
        }

        protected void btnRankingPeso_Click(object sender, EventArgs e)
        {
            rankingsPeso = new List<RRankingPeso>();
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingPeso.rdlc", "RRankingPeso", _fCarnaubaFacade.ConsultaRankingPeso(parametrosDeBusca));
        }

        protected void btnRankingFilhos_Click(object sender, EventArgs e)
        {
            rankingsFilhos = new List<RRankingFilhos>();
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingFilhos.rdlc", "RRankingFilhos", _fCarnaubaFacade.ConsultaRankingFilhos(parametrosDeBusca));
        }

        protected void btnAnimais_Click(object sender, EventArgs e)
        {
            animais = new List<ResultadoBuscaAnimal>();
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/Animais.rdlc", "ResultadoBuscaAnimal", _fCarnaubaFacade.ConsultaAnimal(parametrosDeBusca));
        }


        protected void RankingPeso()
        {
            rankingsPeso = new List<RRankingPeso>();
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingPeso.rdlc", "RRankingPeso", _fCarnaubaFacade.ConsultaRankingPeso(parametrosDeBusca));
        }

        protected void RankingFilhos()
        {
            rankingsFilhos = new List<RRankingFilhos>();
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingFilhos.rdlc", "RRankingFilhos", _fCarnaubaFacade.ConsultaRankingFilhos(parametrosDeBusca));
        }

        protected void Animais()
        {
            animais = new List<ResultadoBuscaAnimal>();
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/Animais.rdlc", "ResultadoBuscaAnimal", _fCarnaubaFacade.ConsultaAnimal(parametrosDeBusca));
        }

        protected void RankingIpp()
        {
            
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingIpp.rdlc", "RRankingIppIepEr", _fCarnaubaFacade.ConsultaRankingIpp(parametrosDeBusca));
        }

        protected void RankingIep()
        {
           
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingIep.rdlc", "RRankingIppIepEr", _fCarnaubaFacade.ConsultaRankingIep(parametrosDeBusca));
        }

        protected void RankingEr()
        {
            
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingEr.rdlc", "RRankingIppIepEr", _fCarnaubaFacade.ConsultaRankingEr(parametrosDeBusca));
        }

        protected void AnimaisAno()
        {

            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/AnimaisAno.rdlc", "RAnimaisAno", _fCarnaubaFacade.ConsultaAnimaisAno(parametrosDeBusca));
        }

        protected void FilhosRaca()
        {

            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/FilhosRaca.rdlc", "RRankingFilhos", _fCarnaubaFacade.ConsultaRankingFilhos(parametrosDeBusca));
        }

        protected void ERRaca()
        {

            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/ERRaca.rdlc", "RRankingIppIepEr", _fCarnaubaFacade.ConsultaRankingEr(parametrosDeBusca));
        }

        protected void AnimaisLactacaoAno()
        {

            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/AnimaisLactacaoAno.rdlc", "RAnimaisLactacaoAno", _fCarnaubaFacade.ConsultaAnimaisLactacaoAno(parametrosDeBusca));
        }

        protected void RankingProducaoDiariaMedia()
        {
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingProducaoDiariaMedia.rdlc", "RAnimaisProducaoLeite", _fCarnaubaFacade.ConsultaProducaoDiariaMedia(parametrosDeBusca));
        }

        protected void RankingProducaoDiariaMaxima()
        {
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingProducaoDiariaMaxima.rdlc", "RAnimaisProducaoLeite", _fCarnaubaFacade.ConsultaProducaoDiariaMaxima(parametrosDeBusca));
        }

        protected void RankingProducaoAcumulada()
        {
            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            ExibeRelatorio("Relatorios/RankingProducaoAcumulada.rdlc", "RAnimaisProducaoLeite", _fCarnaubaFacade.ConsultaProducaoAcumulada(parametrosDeBusca));
        }


    }
}