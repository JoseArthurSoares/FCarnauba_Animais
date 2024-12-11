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

namespace FCarnauba_Animais
{
    public partial class Pluviometrico : PaginaBase
    {
        public Pluviometrico()
        {
            _PageType = new PluviometricoType();
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

        protected void gvPluviometrias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                try
                {
                    Control controlContainer = gvPluviometrias.FooterRow;
                    AdicionaItem(controlContainer);
                }
                catch (Exception exc)
                {
                    TrataExcecao("Erro ao tentar adicionar pluviometria", exc);
                    return;
                }

                ExibeMensagem(TipoDeMensagem.Sucesso, "Pluviometria adicionada com sucesso");
            }
        }

        protected void pluviometriasDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Pluviometria removida com sucesso",
                "Ocorreu um erro ao tentar remover a pluviometria",
                e);
        }

        protected void pluviometriasDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Pluviometria modificada com sucesso",
                "Ocorreu um erro ao tentar modificar a pluviometria",
                e);
        }

        protected void gvPluviometrias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ControlePluviometrico pluviometria = (ControlePluviometrico)e.Row.DataItem;

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    DropDownList ddlEditPluviometro = (DropDownList)e.Row.FindControl("ddlEditPluviometro");
                    ddlEditPluviometro.DataBind();
                    ddlEditPluviometro.SelectedValue = pluviometria.Pluviometro;

                    ITextControl txtEditPluviometria = (ITextControl)e.Row.FindControl("txtEditPluviometria");
                    txtEditPluviometria.Text = String.Format("{0:N2}", Convert.ToDouble(txtEditPluviometria.Text));
                    txtEditPluviometria.Text = txtEditPluviometria.Text.Replace(".", "");

                    ITextControl txtEditData = (ITextControl)e.Row.FindControl("txtEditData");
                    txtEditData.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(txtEditData.Text));

                    e.Row.BackColor = System.Drawing.Color.Bisque;

                }
            }


        }

        protected void gvPluviometrias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ListControl ddlEditPluviometro =
                (ListControl)gvPluviometrias.Rows[e.RowIndex].FindControl("ddlEditPluviometro");

            e.NewValues["Pluviometro"] = ddlEditPluviometro.SelectedValue;

            ITextControl editPluviometria = (ITextControl)gvPluviometrias.Rows[e.RowIndex].FindControl("txtEditPluviometria");
            string valor = editPluviometria.Text;
            string valor2 = valor.Replace(",", ".");
            e.NewValues["Pluviometria"] = valor2;
        }

        protected void ibtNew_Click(object sender, ImageClickEventArgs e)
        {
            Control container = ((Control)sender).Parent;
            AdicionaItem(container);
            ExibeMensagem(TipoDeMensagem.Sucesso, "Pluviometria adicionada com sucesso.");
        }

        private void AdicionaItem(Control controlContainer)
        {
            ITextControl txtAddData = (ITextControl)controlContainer.FindControl("txtAddData");
            ITextControl txtAddPluviometria = (ITextControl)controlContainer.FindControl("txtAddPluviometria");
            ListControl ddlAddPluviometro = (ListControl)controlContainer.FindControl("ddlAddPluviometro");

            ControlePluviometrico pluviometria = new ControlePluviometrico();
            pluviometria.Diretorio = ddlPropriedade.SelectedValue;
            pluviometria.Data = Convert.ToDateTime(txtAddData.Text);
            pluviometria.DataStr = Convert.ToString(txtAddData.Text);
            pluviometria.Pluviometro = ddlAddPluviometro.SelectedValue;
            pluviometria.Pluviometria = Convert.ToDouble(txtAddPluviometria.Text);

            pluviometria.DataUsuario = DateTime.Today;
            pluviometria.Usuario = UsuarioLogado.Name;

            if (pluviometria.Data.Year == Convert.ToInt32(ddlAno.SelectedValue))
            {

                DataSourcePluviometria dataSourcePluviometrias = new DataSourcePluviometria();
                dataSourcePluviometrias.Insira(pluviometria);
            }
            else
            {

                Exception except = new Exception("Pluviometria informada com ano diferente da pesquisa!");
                throw except;
            }

            txtAddData.Text = "";
            ddlAddPluviometro.SelectedIndex = -1;
            txtAddPluviometria.Text = "";
            gvPluviometrias.DataBind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            gvPluviometrias.DataBind();
        }

        protected void pluviometriasDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            ParametrosDeBuscaEmControlePluviometrico criterio = new ParametrosDeBuscaEmControlePluviometrico();

            if (!String.IsNullOrEmpty(ddlPropriedade.SelectedValue))
                criterio.Diretorio = ddlPropriedade.SelectedValue;

            string sDataInicial = "01/01/" + ddlAno.SelectedValue;
            string sDataFinal = "31/12/" + ddlAno.SelectedValue;
            DateTime? dataInicial = Convert.ToDateTime(sDataInicial);
            DateTime? dataFinal = Convert.ToDateTime(sDataFinal);
            criterio.DataInicial = dataInicial;
            criterio.DataFinal = dataFinal;

            e.InputParameters.Add("criterio", criterio);
        }

        protected void ddlPropriedade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParametrosDeBuscaEmControlePluviometrico criterio = new ParametrosDeBuscaEmControlePluviometrico();

            if (!String.IsNullOrEmpty(ddlPropriedade.SelectedValue))
                criterio.Diretorio = ddlPropriedade.SelectedValue;

            string sDataInicial = "01/01/" + ddlAno.SelectedValue;
            string sDataFinal = "31/12/" + ddlAno.SelectedValue;
            DateTime? dataInicial = Convert.ToDateTime(sDataInicial);
            DateTime? dataFinal = Convert.ToDateTime(sDataFinal);
            criterio.DataInicial = dataInicial;
            criterio.DataFinal = dataFinal;

            gvPluviometrias.DataBind();
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