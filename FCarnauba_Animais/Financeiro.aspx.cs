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
    public partial class Financeiro : PaginaBase
    {
        public Financeiro()
        {
            _PageType = new FinanceiroType();
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

        protected void lbtBuscar_Click(object sender, EventArgs e)
        {
            RedefineFormularioNovoGrupo();
        }

        protected void gvFinanceiros_PageIndexChanged(object sender, EventArgs e)
        {
            RedefineFormularioNovoGrupo();
        }

        protected void gvFinanceiros_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            ExibeMensagem(TipoDeMensagem.Sucesso, "Item financeiro removido com sucesso");
            RedefineFormularioNovoGrupo();
        }

        protected void gvFinanceiros_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            
            ExibeMensagem(TipoDeMensagem.Sucesso, "Item financeiro modificado com sucesso");
            RedefineFormularioNovoGrupo();
        }

        
        protected void gvFinanceiros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                try
                {
                    Control controlContainer = gvFinanceiros.FooterRow;
                    AdicionaItem(controlContainer);
                }
                catch (Exception exc)
                {
                    TrataExcecao("Erro ao tentar adicionar item financeiro", exc);
                    return;
                }

                ExibeMensagem(TipoDeMensagem.Sucesso, "Item financeiro adicionado com sucesso");
            }
        }

        protected void financeirosDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Item financeiro removido com sucesso",
                "Ocorreu um erro ao tentar remover o item",
                e);
        }

        protected void financeirosDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Item financeiro modificado com sucesso",
                "Ocorreu um erro ao tentar modificar o item",
                e);
        }

        protected void gvFinanceiros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ItemFinanceiro financeiro = (ItemFinanceiro)e.Row.DataItem;

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ITextControl lblIdGrupo = (ITextControl)e.Row.FindControl("lblIdGrupo");
                    DataSourceGrupos dataSourceGrupos = new DataSourceGrupos();

                    // Potencialmente ineficiente (uma chamada ao serviço por linha do grid)
                    GrupoFinanceiro[] grupo = dataSourceGrupos.ObtemGrupo(financeiro.IdGrupo, false);
                    lblIdGrupo.Text = grupo[0].Descricao;

                    ITextControl lblIdPropriedade = (ITextControl)e.Row.FindControl("lblIdPropriedade");
                    Propriedade propriedade = GetPropriedadeComp(financeiro.PropriedadeComp);
                    if (propriedade != null)
                    {
                        lblIdPropriedade.Text = propriedade.Nome;
                    }

                    ITextControl lblIdEmpresa = (ITextControl)e.Row.FindControl("lblIdEmpresa");
                    Empresa empresa = GetEmpresa(financeiro.IdEmpresa);
                    if (empresa != null)
                    {
                        lblIdEmpresa.Text = empresa.RazaoSocial;
                    }

                    Label lblValorTotal = (Label)e.Row.FindControl("lblValorTotal");
                    int entradaDesembolso = GetEntradaDesembolsoIdGrupo(financeiro.IdGrupo);

                    if (entradaDesembolso == 1)
                    {
                        lblValorTotal.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblValorTotal.ForeColor = System.Drawing.Color.Red;
                    }

                    Label lblIdFinanceiro = (Label)e.Row.FindControl("lblIdFinanceiro");

                    if (ItemFinanceiroValidado(financeiro.IdFinanceiro))
                    {
                        lblIdFinanceiro.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblIdFinanceiro.ForeColor = System.Drawing.Color.Red;
                    }

                    Label lblVenda = (Label)e.Row.FindControl("lblVenda");

                    if (lblVenda.Text == "True")
                    {
                        lblVenda.Text = "Sim";
                    }
                    else
                    {
                        lblVenda.Text = "Não";
                    }


                }

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    DropDownList ddlEditIdPropriedade = (DropDownList)e.Row.FindControl("ddlEditIdPropriedade");
                    ddlEditIdPropriedade.DataBind();
                    ddlEditIdPropriedade.SelectedValue = financeiro.PropriedadeComp;

                    DropDownList ddlEditIdEmpresa = (DropDownList)e.Row.FindControl("ddlEditIdEmpresa");
                    ddlEditIdEmpresa.DataBind();
                    ddlEditIdEmpresa.SelectedValue = financeiro.IdEmpresa.ToString();

                    DropDownList ddlEditFormaPagamento = (DropDownList)e.Row.FindControl("ddlEditFormaPagamento");
                    ddlEditFormaPagamento.DataBind();
                    ddlEditFormaPagamento.SelectedValue = financeiro.FormaPagamento;

                    EscolhaDeGrupo editGrupo = (EscolhaDeGrupo)e.Row.FindControl("editGrupo");
                    editGrupo.IdGrupoSelecionado = financeiro.IdGrupo;
                    ITextControl txtEditValorUnitario = (ITextControl)e.Row.FindControl("txtEditValorUnitario");
                    txtEditValorUnitario.Text = String.Format("{0:N2}", Convert.ToDouble(txtEditValorUnitario.Text));
                    txtEditValorUnitario.Text = txtEditValorUnitario.Text.Replace(".", "");
                    //txtEditValorUnitario.Text = txtEditValorUnitario.Text.Replace(",", ".");

                    ITextControl txtEditData = (ITextControl)e.Row.FindControl("txtEditData");
                    txtEditData.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(txtEditData.Text));

                    e.Row.BackColor = System.Drawing.Color.Bisque;

                }
            }


        }

        private static Propriedade GetPropriedadeComp(string idsPropriedadesComp)
        {
            DataSourcePropriedades dataSource = new DataSourcePropriedades();
            return dataSource.ObtemPropriedadeComp(idsPropriedadesComp);
        }

        private static Empresa GetEmpresa(int idEmpresa)
        {
            DataSourceEmpresas dataSource = new DataSourceEmpresas();
            return dataSource.ObtemEmpresa(idEmpresa);
        }

        private int GetEntradaDesembolsoIdGrupo(int idGrupo)
        {
            DataSourceGrupos dataSource = new DataSourceGrupos();
            return dataSource.GetEntradaDesembolsoIdGrupo(idGrupo);
        }

        private bool ItemFinanceiroValidado(long idFinanceiro)
        {
            DataSourceFinanceiros dataSource = new DataSourceFinanceiros();
            return dataSource.ItemFinanceiroValidado(idFinanceiro);
        }

        protected void gvFinanceiros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ListControl ddlEditFormaPagamento =
                (ListControl)gvFinanceiros.Rows[e.RowIndex].FindControl("ddlEditFormaPagamento");

            e.NewValues["FormaPagamento"] = ddlEditFormaPagamento.SelectedValue;

            ListControl ddlEditIdPropriedade =
                (ListControl)gvFinanceiros.Rows[e.RowIndex].FindControl("ddlEditIdPropriedade");

            EscolhaDeGrupo editGrupo = (EscolhaDeGrupo)gvFinanceiros.Rows[e.RowIndex].FindControl("editGrupo");

            e.NewValues["PropriedadeComp"] = ddlEditIdPropriedade.SelectedValue;

            ListControl ddlEditIdEmpresa =
                (ListControl)gvFinanceiros.Rows[e.RowIndex].FindControl("ddlEditIdEmpresa");

            e.NewValues["IdEmpresa"] = ddlEditIdEmpresa.SelectedValue;

            VerificaGrupo(editGrupo);
            e.NewValues["IdGrupo"] = editGrupo.IdGrupoSelecionado;

            ITextControl editValorUnitario = (ITextControl)gvFinanceiros.Rows[e.RowIndex].FindControl("txtEditValorUnitario");
            string valor = editValorUnitario.Text;
            string valor2 = valor.Replace(",",".");
            e.NewValues["ValorUnitario"] = valor2;
        }

        protected void ibtNew_Click(object sender, ImageClickEventArgs e)
        {
            Control container = ((Control)sender).Parent;
            AdicionaItem(container);
            RedefineFormularioNovoGrupo();
            ExibeMensagem(TipoDeMensagem.Sucesso, "Item financeiro adicionado com sucesso.");
        }

        private void RedefineFormularioNovoGrupo()
        {
            RedefineFormularioNovoGrupo(gvFinanceiros.FooterRow);
        }

        private static void RedefineFormularioNovoGrupo(Control container)
        {
            ITextControl lblAddGrupo = (ITextControl)container.FindControl("lblAddGrupo");
            lblAddGrupo.Text = "Grupo...";
        }

        private void AdicionaItem(Control controlContainer)
        {
            string itemDescricao = "";
            ITextControl txtAddData = (ITextControl)controlContainer.FindControl("txtAddData");
            ITextControl txtAddDescricao = (ITextControl)controlContainer.FindControl("txtAddDescricao");
            ITextControl txtAddQuantidade = (ITextControl)controlContainer.FindControl("txtAddQuantidade");
            ITextControl txtAddValorUnitario = (ITextControl)controlContainer.FindControl("txtAddValorUnitario");
            ListControl ddlAddIdPropriedade = (ListControl)controlContainer.FindControl("ddlAddIdPropriedade");
            ListControl ddlAddIdEmpresa = (ListControl)controlContainer.FindControl("ddlAddIdEmpresa");
            ListControl ddlAddItemDescricao = (ListControl)controlContainer.FindControl("ddlAddItemDescricao");
            //ITextControl txtAddDocumento = (ITextControl)controlContainer.FindControl("txtAddDocumento");
            ListControl ddlAddFormaPagamento = (ListControl)controlContainer.FindControl("ddlAddFormaPagamento");
            ICheckBoxControl ckAddVenda = (ICheckBoxControl)controlContainer.FindControl("ckAddVenda");

            EscolhaDeGrupo addGrupo = (EscolhaDeGrupo)controlContainer.FindControl("addGrupo");

            itemDescricao = ddlAddItemDescricao.SelectedValue;

            ItemFinanceiro financeiro = new ItemFinanceiro();
            financeiro.Data = Convert.ToString(txtAddData.Text);
            financeiro.Descricao = txtAddDescricao.Text;
            //financeiro.Documento = txtAddDocumento.Text;
            financeiro.FormaPagamento = ddlAddFormaPagamento.SelectedValue;

            if (!String.IsNullOrEmpty(itemDescricao))
                financeiro.Descricao = itemDescricao + " " + financeiro.Descricao;
            //financeiro.IdPropriedade = Convert.ToInt32(ddlAddIdPropriedade.SelectedValue);
            financeiro.PropriedadeComp = ddlAddIdPropriedade.SelectedValue;
            financeiro.IdEmpresa = Convert.ToInt32(ddlAddIdEmpresa.SelectedValue);
            VerificaGrupo(addGrupo);
            financeiro.IdGrupo = Convert.ToInt32(addGrupo.IdGrupoSelecionado);
            financeiro.Quantidade = Convert.ToInt32(txtAddQuantidade.Text);
            financeiro.ValorUnitario = Convert.ToDouble(txtAddValorUnitario.Text);

            financeiro.VendaAnimais = ckAddVenda.Checked;

            financeiro.DataUsuario = DateTime.Today;
            financeiro.Usuario = UsuarioLogado.Name;

            DataSourceFinanceiros dataSourceFinanceiros = new DataSourceFinanceiros();
            long ultimoId = dataSourceFinanceiros.Insira(financeiro);

            if (financeiro.FormaPagamento != "À Vista" && !String.IsNullOrEmpty(financeiro.FormaPagamento))
            {
                double valorTotal = financeiro.ValorUnitario * financeiro.Quantidade;
                dataSourceFinanceiros.InserirParcelas(Convert.ToDateTime(financeiro.Data), valorTotal, Convert.ToInt32(financeiro.FormaPagamento), ultimoId);
            }

            txtAddData.Text = "";
            ckAddVenda.Checked = false;
            txtAddDescricao.Text = "";
            //txtAddDocumento.Text = "";
            //ddlAddIdPropriedade.SelectedIndex = -1;
            ddlAddIdPropriedade.SelectedValue = "8";
            ddlAddIdEmpresa.SelectedIndex = -1;
            txtAddQuantidade.Text = "";
            txtAddValorUnitario.Text = "";
            ddlAddItemDescricao.SelectedIndex = -1;
            ddlAddFormaPagamento.SelectedIndex = -1;

            txtDescricao.Text = "";
            ddlPropriedade.SelectedValue = financeiro.PropriedadeComp;
            txtDataInicio.Text = "";
            txtDataFim.Text = "";
            ddlFornCliente.SelectedIndex = -1;
            ckVenda.Checked = false;

            gvFinanceiros.DataBind();
        }

        private static void VerificaGrupo(EscolhaDeGrupo addGrupo)
        {
            if (addGrupo.IdGrupoSelecionado == null)
            {
                throw new Exception("Um grupo deve ser escolhido");
            }

            if (Convert.ToInt32(addGrupo.IdGrupoSelecionado) == 0)
            {
                throw new Exception("Um grupo válido deve ser escolhido");
            }
        }

        protected void addGrupo_GrupoSelecionado(object sender, SelecaoDeGrupoEventArgs evt)
        {
            Control container = ((Control)sender).Parent;
            ITextControl lblAddGrupo = (ITextControl)container.FindControl("lblAddGrupo");
            if (evt.IdGrupo != 0)
            {
                lblAddGrupo.Text = evt.NomeGrupo;
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            gvFinanceiros.DataBind();
        }

        protected void financeirosDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            CriterioPesquisaFinanceiro criterio = new CriterioPesquisaFinanceiro();
            
            //if (txtDescricaoGrupo.Text.Trim() != String.Empty)
            //{
            //    criterio.DescricaoGrupo = txtDescricaoGrupo.Text;
            //}
            if (txtDescricao.Text.Trim() != String.Empty)
            {
                criterio.Descricao = txtDescricao.Text;
            }
            else
            {
                criterio.Descricao = "";
            }

            if (txtDataInicio.Text.Trim() != String.Empty && txtDataInicio.Text != "__/__/____")
            {
                criterio.DataInicio = Convert.ToDateTime(txtDataInicio.Text);
            }

            if (txtDataFim.Text.Trim() != String.Empty && txtDataFim.Text != "__/__/____")
            {
                criterio.DataFim = Convert.ToDateTime(txtDataFim.Text);
            }

            //if (!String.IsNullOrEmpty(ddlPropriedade.SelectedValue))
            //    criterio.IdPropriedade = Convert.ToInt32(ddlPropriedade.SelectedValue);

            if (!String.IsNullOrEmpty(ddlPropriedade.SelectedValue))
                criterio.PropriedadeComp = ddlPropriedade.SelectedValue;

            if (!String.IsNullOrEmpty(ddlFornCliente.SelectedValue))
                criterio.IdEmpresa = Convert.ToInt32(ddlFornCliente.SelectedValue);

            if (ckVenda.Checked)
                criterio.Venda = true;

            e.InputParameters.Add("criterio", criterio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlPropriedade.DataSource = FCarnaubaFacade.ObtemPropriedadesComp();
                ddlPropriedade.DataValueField = "IdsPropriedades";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.SelectedValue = "8";
                ddlPropriedade.DataBind();

                ddlFornCliente.DataSource = FCarnaubaFacade.ObtemEmpresas();
                ddlFornCliente.DataValueField = "IdEmpresa";
                ddlFornCliente.DataTextField = "RazaoSocial";
                ddlFornCliente.DataBind();

                
            }
        }

        //public List<string> GetDescricoesFinanceiro(string prefixText, int count)
        //{
        //    return FCarnaubaFacade.GetDescricoesFinanceiro(prefixText);

        //}

        protected void ddlAddItemDescricaoFooter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList dropDownList = sender as DropDownList;
            //string id = dropDownList.ID;
            //string item = dropDownList.SelectedValue;
            //int idGrupo = 0;
            //string descricao = "";

            //if (!String.IsNullOrEmpty(item))
            //{
            //    idGrupo = FCarnaubaFacade.GetIdGrupoDescricaoFinanceiro(item);
            //    descricao = FCarnaubaFacade.GetDescricaoIdGrupo(idGrupo);

            //    ITextControl lblAddGrupo = (ITextControl)gvFinanceiros.FooterRow.FindControl("lblAddGrupo");
            //    EscolhaDeGrupo addGrupo = (EscolhaDeGrupo)gvFinanceiros.FooterRow.FindControl("addGrupo");

            //    if (idGrupo > 0)
            //    {

            //        lblAddGrupo.Text = descricao;
            //        addGrupo.IdGrupoSelecionado = idGrupo;
            //    }
            //}

        }

        protected void ddlAddItemDescricao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList dropDownList = sender as DropDownList;
            //string id = dropDownList.ID;
            //string item = dropDownList.SelectedValue;
            //int idGrupo = 0;
            //string descricao = "";
            //GridViewRow row = (GridViewRow)dropDownList.Parent.Parent;

            //if (!String.IsNullOrEmpty(item))
            //{
            //    idGrupo = FCarnaubaFacade.GetIdGrupoDescricaoFinanceiro(item);
            //    descricao = FCarnaubaFacade.GetDescricaoIdGrupo(idGrupo);

            //    ITextControl lblAddGrupo = (ITextControl)row.FindControl("lblAddGrupo");
            //    EscolhaDeGrupo addGrupo = (EscolhaDeGrupo)row.FindControl("addGrupo");

            //    if (idGrupo > 0)
            //    {

            //        lblAddGrupo.Text = descricao;
            //        addGrupo.IdGrupoSelecionado = idGrupo;
            //    }
            //}

        }

        //protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string mes = ddlMes.SelectedValue;
        //    string dataInicial = null;
        //    string dataFinal = null;
        //    //int ano = DateTime.Today.Year;
        //    int ano = Convert.ToInt32(ddlAno.SelectedValue);

        //    switch (mes)
        //    {
        //        case "JANEIRO":
        //            dataInicial = "01/01/" + ano;
        //            dataFinal = "31/01/" + ano;
        //            break;
        //        case "FEVEREIRO":
        //            dataInicial = "01/02/" + ano;

        //            if (DateTime.IsLeapYear(ano))
        //            {
        //                dataFinal = "29/02/" + ano;
        //            }
        //            else
        //            {
        //                dataFinal = "28/02/" + ano;
        //            }
        //            break;
        //        case "MARÇO":
        //            dataInicial = "01/03/" + ano;
        //            dataFinal = "31/03/" + ano;
        //            break;
        //        case "ABRIL":
        //            dataInicial = "01/04/" + ano;
        //            dataFinal = "30/04/" + ano;
        //            break;
        //        case "MAIO":
        //            dataInicial = "01/05/" + ano;
        //            dataFinal = "31/05/" + ano;
        //            break;
        //        case "JUNHO":
        //            dataInicial = "01/06/" + ano;
        //            dataFinal = "30/06/" + ano;
        //            break;
        //        case "JULHO":
        //            dataInicial = "01/07/" + ano;
        //            dataFinal = "31/07/" + ano;
        //            break;
        //        case "AGOSTO":
        //            dataInicial = "01/08/" + ano;
        //            dataFinal = "31/08/" + ano;
        //            break;
        //        case "SETEMBRO":
        //            dataInicial = "01/09/" + ano;
        //            dataFinal = "30/09/" + ano;
        //            break;
        //        case "OUTUBRO":
        //            dataInicial = "01/10/" + ano;
        //            dataFinal = "31/10/" + ano;
        //            break;
        //        case "NOVEMBRO":
        //            dataInicial = "01/11/" + ano;
        //            dataFinal = "30/11/" + ano;
        //            break;
        //        case "DEZEMBRO":
        //            dataInicial = "01/12/" + ano;
        //            dataFinal = "31/12/" + ano;
        //            break;
        //        default:
        //            dataInicial = "01/01/" + ano;
        //            dataFinal = "31/01/" + ano;
        //            break;
        //    }

        //    txtDataInicio.Text = dataInicial;
        //    txtDataFim.Text = dataFinal;
        //}

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

        public List<string> getParcelas(int parcFinal)
        {
            int parcInicial = 2;
            var parcelas = new List<string>();

            parcelas.Add("");
            parcelas.Add("À Vista");

            for (int i = parcInicial; i >= parcFinal; i++)
            {
                parcelas.Add(i.ToString());
            }

            return parcelas;
        }

        protected void ibtDetail_Click(object sender, EventArgs e)
        {
            
            ImageButton btn = (ImageButton)sender;
            string idFinanceiro = btn.CommandArgument;
            Response.Redirect("DetalhesFinanceiro.aspx?financeiroId=" + idFinanceiro);
            

        }

    }
}