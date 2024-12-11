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
    public partial class Pluviometria : PaginaBase
    {
        public string op = null;
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private List<ResultadoBuscaControlePluviometrico> controles;
        public event Action<int> DeleteControlePluviometricoClick;
        string propriedade;
        string ano;

        public Pluviometria()
        {
            _PageType = new PluviometriaType();
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
                op = Request.QueryString["op"];

                if (op == "add")
                {
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Pluviometrico salvo com sucesso");
                }

                if (op == "edit")
                {
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Pluviometrico salvo com sucesso");
                }

                ddlAno.DataSource = getAnos(1960);
                ddlAno.DataBind();

                ParametrosDeBuscaEmControlePluviometrico parametrosDeBusca = (ParametrosDeBuscaEmControlePluviometrico)Session["parametrosDeBuscaEmControlePluviometrico"];

                if (parametrosDeBusca != null)
                {
                    if (parametrosDeBusca.Diretorio == "FAZENDA CARNAÚBA")
                    {
                        btnCarnauba.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    if (parametrosDeBusca.Diretorio == "FAZENDA PAU LEITE")
                    {
                        btnPauLeite.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    if (parametrosDeBusca.Diretorio == "FAZENDA BONITO")
                    {
                        btnBonito.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    if (parametrosDeBusca.DataInicial != null)
                        ddlAno.SelectedValue = parametrosDeBusca.DataInicial.ToString().Substring(6, 4);

                    controles = FCarnaubaFacade.ConsultaControlePluviometrico(parametrosDeBusca);

                    Session["var"] = controles;

                    gridViewControlesPluviometricos.DataSource = controles;
                    gridViewControlesPluviometricos.DataBind();
                }


            }
        }

        protected void btnCarnauba_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmControlePluviometrico"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            controles = new List<ResultadoBuscaControlePluviometrico>();

            propriedade = "FAZENDA CARNAÚBA";
            Session["propriedade"] = propriedade;
            ano = ddlAno.SelectedValue;
            Session["ano"] = ano;

            var parametrosDeBusca = new ParametrosDeBuscaEmControlePluviometrico { Diretorio = propriedade };

            string sDataInicial = "01/01/" + ddlAno.SelectedValue;
            string sDataFinal = "31/12/" + ddlAno.SelectedValue;
            DateTime? dataInicial = Convert.ToDateTime(sDataInicial);
            DateTime? dataFinal = Convert.ToDateTime(sDataFinal);
            parametrosDeBusca.DataInicial = dataInicial;
            parametrosDeBusca.DataFinal = dataFinal;

            controles = FCarnaubaFacade.ConsultaControlePluviometrico(parametrosDeBusca);

            Session["parametrosDeBuscaEmControlePluviometrico"] = parametrosDeBusca;

            Session["var"] = controles;

            gridViewControlesPluviometricos.DataSource = controles;
            gridViewControlesPluviometricos.DataBind();

            btnCarnauba.BackColor = Color.FromArgb(5, 43,92);
        }

        protected void btnPauLeite_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmControlePluviometrico"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            controles = new List<ResultadoBuscaControlePluviometrico>();

            propriedade = "FAZENDA PAU LEITE";
            Session["propriedade"] = propriedade;
            ano = ddlAno.SelectedValue;
            Session["ano"] = ano;

            var parametrosDeBusca = new ParametrosDeBuscaEmControlePluviometrico { Diretorio = propriedade };

            string sDataInicial = "01/01/" + ddlAno.SelectedValue;
            string sDataFinal = "31/12/" + ddlAno.SelectedValue;
            DateTime? dataInicial = Convert.ToDateTime(sDataInicial);
            DateTime? dataFinal = Convert.ToDateTime(sDataFinal);
            parametrosDeBusca.DataInicial = dataInicial;
            parametrosDeBusca.DataFinal = dataFinal;

            controles = FCarnaubaFacade.ConsultaControlePluviometrico(parametrosDeBusca);

            Session["parametrosDeBuscaEmControlePluviometrico"] = parametrosDeBusca;

            Session["var"] = controles;

            gridViewControlesPluviometricos.DataSource = controles;
            gridViewControlesPluviometricos.DataBind();

            btnPauLeite.BackColor = Color.FromArgb(5, 43, 92);
        }

        protected void btnBonito_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmControlePluviometrico"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            controles = new List<ResultadoBuscaControlePluviometrico>();

            propriedade = "FAZENDA BONITO";
            Session["propriedade"] = propriedade;
            ano = ddlAno.SelectedValue;
            Session["ano"] = ano;

            var parametrosDeBusca = new ParametrosDeBuscaEmControlePluviometrico { Diretorio = propriedade };

            string sDataInicial = "01/01/" + ddlAno.SelectedValue;
            string sDataFinal = "31/12/" + ddlAno.SelectedValue;
            DateTime? dataInicial = Convert.ToDateTime(sDataInicial);
            DateTime? dataFinal = Convert.ToDateTime(sDataFinal);
            parametrosDeBusca.DataInicial = dataInicial;
            parametrosDeBusca.DataFinal = dataFinal;

            controles = FCarnaubaFacade.ConsultaControlePluviometrico(parametrosDeBusca);

            Session["parametrosDeBuscaEmControlePluviometrico"] = parametrosDeBusca;

            Session["var"] = controles;

            gridViewControlesPluviometricos.DataSource = controles;
            gridViewControlesPluviometricos.DataBind();

            btnBonito.BackColor = Color.FromArgb(5, 43, 92);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CheckAllowance();


            Session["parametrosDeBuscaEmControlePluviometrico"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");


            //UpdateTitle();

            controles = new List<ResultadoBuscaControlePluviometrico>();

            var parametrosDeBusca = new ParametrosDeBuscaEmControlePluviometrico { TodosOsCampos = txtBusca.Text };

            string sDataInicial = "01/01/" + ddlAno.SelectedValue;
            string sDataFinal = "31/12/" + ddlAno.SelectedValue;
            DateTime? dataInicial = Convert.ToDateTime(sDataInicial);
            DateTime? dataFinal = Convert.ToDateTime(sDataFinal);
            parametrosDeBusca.DataInicial = dataInicial;
            parametrosDeBusca.DataFinal = dataFinal;

            parametrosDeBusca.Diretorio = propriedade;


            controles = FCarnaubaFacade.ConsultaControlePluviometrico(parametrosDeBusca);

            Session["parametrosDeBuscaEmControlePluviometrico"] = parametrosDeBusca;

            Session["var"] = controles;

            gridViewControlesPluviometricos.DataSource = controles;
            gridViewControlesPluviometricos.DataBind();
        }

        protected void btnDeleteControlePluviometrico_Click(object sender, EventArgs e)
        {
            LinkButton btnDelete = (LinkButton)sender;
            int ControlePluviometricoId = Convert.ToInt32(btnDelete.CommandArgument);

            if (ControlePluviometricoId > 0)
            {
                {
                    _fCarnaubaFacade.RemoveControlePluviometrico(ControlePluviometricoId.ToString());
                    UpdateGridView();
                }
            }
        }

        private void UpdateGridView()
        {
            CheckAllowance();

            //Session["parametrosDeBuscaEmControlePluviometrico"] = null;

            //controles = new List<ResultadoBuscaControlePluviometrico>();

            //propriedade = Session["propriedade"].ToString();

            //var parametrosDeBusca = new ParametrosDeBuscaEmControlePluviometrico { Diretorio = propriedade };

            ParametrosDeBuscaEmControlePluviometrico parametrosDeBusca = (ParametrosDeBuscaEmControlePluviometrico)Session["parametrosDeBuscaEmControlePluviometrico"];

            controles = FCarnaubaFacade.ConsultaControlePluviometrico(parametrosDeBusca);

            Session["parametrosDeBuscaEmControlePluviometrico"] = parametrosDeBusca;

            Session["var"] = controles;

            if (parametrosDeBusca.Diretorio == "FAZENDA CARNAÚBA")
            {
                btnCarnauba.BackColor = Color.FromArgb(5, 43, 92);
            }

            if (parametrosDeBusca.Diretorio == "FAZENDA PAU LEITE")
            {
                btnPauLeite.BackColor = Color.FromArgb(5, 43, 92);
            }

            if (parametrosDeBusca.Diretorio == "FAZENDA BONITO")
            {
                btnBonito.BackColor = Color.FromArgb(5, 43, 92);
            }

            gridViewControlesPluviometricos.DataSource = controles;
            gridViewControlesPluviometricos.DataBind();

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