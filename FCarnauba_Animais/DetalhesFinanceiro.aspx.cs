using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.util;
using LightInfocon.GoldenAccess.General;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;
using Image = iTextSharp.text.Image;
using ListItem = System.Web.UI.WebControls.ListItem;
using FCarnauba_Animais.UserControls;
using FCarnauba_Animais.DataSources;

namespace FCarnauba_Animais
{
    public partial class DetalhesFinanceiro : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public string SFinanceiro;
        public double TotalPago;
        public ItemFinanceiro financeiro;
        //private Compra compra;

        public DetalhesFinanceiro()
        {
            _PageType = new DetalhesFinanceiroType();
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

        public string FinanceiroId
        {
            get
            {
                string financeiroId = Request.Params["financeiroId"];
                return financeiroId;
            }
        }

        protected void gvCompras_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            ExibeMensagem(TipoDeMensagem.Sucesso, "Compra removida com sucesso");
        }

        protected void gvDocumentos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            ExibeMensagem(TipoDeMensagem.Sucesso, "Documento removido com sucesso");
        }

        protected void gvParcelas_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            ExibeMensagem(TipoDeMensagem.Sucesso, "Parcela removida com sucesso");
        }

        protected void gvCompras_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

            ExibeMensagem(TipoDeMensagem.Sucesso, "Compra modificada com sucesso");
        }

        protected void gvDocumentos_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

            ExibeMensagem(TipoDeMensagem.Sucesso, "Documento modificado com sucesso");
        }

        protected void gvParcelas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

            ExibeMensagem(TipoDeMensagem.Sucesso, "Parcela modificada com sucesso");
        }

        protected void gvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                try
                {
                    System.Web.UI.Control controlContainer = gvCompras.FooterRow;
                    AdicionaItem(controlContainer);
                }
                catch (Exception exc)
                {
                    TrataExcecao("Erro ao tentar adicionar compra", exc);
                    return;
                }

                ExibeMensagem(TipoDeMensagem.Sucesso, "Compra adicionada com sucesso");
            }
        }

        protected void gvParcelas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                try
                {
                    System.Web.UI.Control controlContainer = gvParcelas.FooterRow;
                    AdicionaItemParcela(controlContainer);
                }
                catch (Exception exc)
                {
                    TrataExcecao("Erro ao tentar adicionar parcela", exc);
                    return;
                }

                ExibeMensagem(TipoDeMensagem.Sucesso, "Parcela adicionada com sucesso");
            }
        }

        protected void gvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                try
                {
                    System.Web.UI.Control controlContainer = gvDocumentos.FooterRow;
                    AdicionaItemDocumento(controlContainer);
                }
                catch (Exception exc)
                {
                    TrataExcecao("Erro ao tentar adicionar documento", exc);
                    return;
                }

                ExibeMensagem(TipoDeMensagem.Sucesso, "Documento adicionado com sucesso");
            }
        }

        protected void comprasDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Compra removida com sucesso",
                "Ocorreu um erro ao tentar remover o item",
                e);
        }

        protected void comprasDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Compra modificada com sucesso",
                "Ocorreu um erro ao tentar modificar o item",
                e);
        }

        protected void documentosDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Documento removido com sucesso",
                "Ocorreu um erro ao tentar remover o item",
                e);
        }

        protected void documentosDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Documento modificado com sucesso",
                "Ocorreu um erro ao tentar modificar o item",
                e);
        }

        protected void parcelasDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Parcela removida com sucesso",
                "Ocorreu um erro ao tentar remover o item",
                e);
        }

        protected void parcelasDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Parcela modificada com sucesso",
                "Ocorreu um erro ao tentar modificar o item",
                e);
        }

        protected void gvCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Compra compra = (Compra)e.Row.DataItem;

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ITextControl lblIdAnimal = (ITextControl)e.Row.FindControl("lblIdAnimal");
                    Animal animal = GetAnimal(Convert.ToInt32(compra.IdAnimal));
                    if (animal != null)
                    {
                        lblIdAnimal.Text = animal.NomeCompleto;
                    }


                }

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    DropDownList ddlEditIdAnimal = (DropDownList)e.Row.FindControl("ddlEditIdAnimal");

                    ITextControl lblIdAnimal = (ITextControl)e.Row.FindControl("lblIdAnimal");
                    Animal animal = GetAnimal(Convert.ToInt32(compra.IdAnimal));

                    List<Animal> animais = new List<Animal>();
                    animais.Add(animal);
                    ddlEditIdAnimal.DataSource = animais;

                    ddlEditIdAnimal.DataBind();
                    ddlEditIdAnimal.SelectedValue = compra.IdAnimal.ToString();

                    ITextControl txtEditValor = (ITextControl)e.Row.FindControl("txtEditValor");
                    txtEditValor.Text = String.Format("{0:N2}", Convert.ToDouble(txtEditValor.Text));
                    txtEditValor.Text = txtEditValor.Text.Replace(".", "");

                    e.Row.BackColor = System.Drawing.Color.Bisque;

                }
            }


        }

        protected void gvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Documento documento = (Documento)e.Row.DataItem;

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton lnkDocumento = (LinkButton)e.Row.FindControl("lnkDocumento");

                    Documento doc = GetDocumento(documento.DocumentoFinanceiroId);

                    lnkDocumento.Text = doc.PDFDocumento.OriginalFileName;
                    lnkDocumento.CommandArgument = doc.DocumentoFinanceiroId;



                }

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {

                    e.Row.BackColor = System.Drawing.Color.Bisque;

                }
            }

        }

        protected void gvParcelas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ITextControl lblValorPago = (ITextControl)e.Row.FindControl("lblValorPago");
                    TotalPago += Convert.ToDouble(lblValorPago.Text);
                    financeiro.TotalPago = TotalPago;
                }

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    
                    ITextControl txtEditValorInicial = (ITextControl)e.Row.FindControl("txtEditValorInicial");
                    txtEditValorInicial.Text = String.Format("{0:N2}", Convert.ToDouble(txtEditValorInicial.Text));
                    txtEditValorInicial.Text = txtEditValorInicial.Text.Replace(".", "");

                    ITextControl txtEditValorPago = (ITextControl)e.Row.FindControl("txtEditValorPago");
                    txtEditValorPago.Text = String.Format("{0:N2}", Convert.ToDouble(txtEditValorPago.Text));
                    txtEditValorPago.Text = txtEditValorPago.Text.Replace(".", "");

                    e.Row.BackColor = System.Drawing.Color.Bisque;

                }
            }


        }

        protected void lnkDocumento_Click(object sender, EventArgs e)
        {
            Control container = ((Control)sender).Parent;
            LinkButton lnkDocumento = (LinkButton)container.FindControl("lnkDocumento");

            Documento doc = GetDocumento(lnkDocumento.CommandArgument);

            string fullPath = FCarnaubaDataAccess.GetPathFor(doc.PDFDocumento.FileName);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + doc.PDFDocumento.OriginalFileName);
            Response.WriteFile(fullPath);
            Response.End();
        }

        private static Animal GetAnimal(int idAnimal)
        {
            DataSourceAnimais dataSource = new DataSourceAnimais();
            return dataSource.ObtemAnimal(idAnimal);
        }

        private static Documento GetDocumento(string documentoFinanceiroId)
        {
            string[] ids = documentoFinanceiroId.Split(' ');
            int financeiroId = Convert.ToInt32(ids[0]);
            int documentoId = Convert.ToInt32(ids[1]);

            DataSourceDocumentos dataSource = new DataSourceDocumentos();
            return dataSource.ObtemDocumento(financeiroId, documentoId);
        }

        private static Parcela GetParcela(string parcelaFinanceiroId)
        {
            string[] ids = parcelaFinanceiroId.Split(' ');
            int financeiroId = Convert.ToInt32(ids[0]);
            int parcelaId = Convert.ToInt32(ids[1]);

            DataSourceParcelas dataSource = new DataSourceParcelas();
            return dataSource.ObtemParcela(financeiroId, parcelaId);
        }

        protected void ibtNew_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Control container = ((Control)sender).Parent;
                AdicionaItem(container);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Compra adicionada com sucesso.");
            }
            catch (Exception)
            {
                ExibeMensagem(TipoDeMensagem.Aviso, "Todos os campos são obrigatórios.");
            }
        }

        protected void ibtNewDocumento_Click(object sender, ImageClickEventArgs e)
        {
            Control container = ((Control)sender).Parent;
            AdicionaItemDocumento(container);
            ExibeMensagem(TipoDeMensagem.Sucesso, "Documento adicionado com sucesso.");
        }

        protected void ibtNewParcela_Click(object sender, ImageClickEventArgs e)
        {
            Control container = ((Control)sender).Parent;
            AdicionaItemParcela(container);
            ExibeMensagem(TipoDeMensagem.Sucesso, "Parcela adicionada com sucesso.");
        }

        private void AdicionaItem(Control controlContainer)
        {
            ITextControl txtAddDescricao = (ITextControl)controlContainer.FindControl("txtAddDescricao");
            ListControl ddlAddIdAnimal = (ListControl)controlContainer.FindControl("ddlAddIdAnimal");
            ITextControl txtAddValor = (ITextControl)controlContainer.FindControl("txtAddValor");

            if (String.IsNullOrEmpty(txtAddDescricao.Text) || String.IsNullOrEmpty(ddlAddIdAnimal.SelectedValue) || String.IsNullOrEmpty(txtAddValor.Text))
            {
                Exception except = new Exception("Todos os campos são obrigátórios.");
                throw except;
            }
            else
            {


                Compra compra = new Compra();

                compra.Descricao = txtAddDescricao.Text;
                compra.IdAnimal = ddlAddIdAnimal.SelectedValue;
                compra.Valor = Convert.ToDouble(txtAddValor.Text);

                DataSourceCompras dataSourceCompras = new DataSourceCompras();
                dataSourceCompras.Insira(FinanceiroId, compra);

                txtAddDescricao.Text = "";
                txtAddValor.Text = "";
                ddlAddIdAnimal.SelectedIndex = -1;
                gvCompras.DataBind();
            }
        }

        private void AdicionaItemDocumento(Control controlContainer)
        {
            ITextControl txtAddDescricaoDocumento = (ITextControl)controlContainer.FindControl("txtAddDescricaoDocumento");
            FileUpload uploadAddDocumento = (FileUpload)controlContainer.FindControl("uploadAddDocumento");

            Documento documento = new Documento();

            documento.Descricao = txtAddDescricaoDocumento.Text;
            //documento.DataDocumento = DateTime.Today;

            if (uploadAddDocumento.HasFile)
            {
                documento.PDFDocumento = new Arquivo(uploadAddDocumento.FileName,
                                                           uploadAddDocumento.FileContent);
            }
            

            DataSourceDocumentos dataSourceDocumentos = new DataSourceDocumentos();
            dataSourceDocumentos.Insira(FinanceiroId, documento);

            txtAddDescricaoDocumento.Text = "";
            
            gvDocumentos.DataBind();
        }

        private void AdicionaItemParcela(Control controlContainer)
        {
            ITextControl txtAddNParcela = (ITextControl)controlContainer.FindControl("txtAddNParcela");
            ITextControl txtAddData = (ITextControl)controlContainer.FindControl("txtAddData");
            ITextControl txtAddValorInicial = (ITextControl)controlContainer.FindControl("txtAddValorInicial");
            ITextControl txtAddValorPago = (ITextControl)controlContainer.FindControl("txtAddValorPago");
            ITextControl txtAddDataPagamento = (ITextControl)controlContainer.FindControl("txtAddDataPagamento");


            Parcela parcela = new Parcela();

            parcela.NParcela = Convert.ToInt32(txtAddNParcela.Text);
            parcela.Data = txtAddData.Text;
            parcela.ValorInicial = Convert.ToDouble(txtAddValorInicial.Text);
            if (!String.IsNullOrEmpty(txtAddValorPago.Text))
            {
                parcela.ValorPago = Convert.ToDouble(txtAddValorPago.Text);
            }
            else
            {
                parcela.ValorPago = 0;
            }

            parcela.DataPagamento = txtAddDataPagamento.Text;

            DataSourceParcelas dataSourceParcelas = new DataSourceParcelas();
            dataSourceParcelas.Insira(FinanceiroId, parcela);

            txtAddNParcela.Text = "";
            txtAddValorInicial.Text = "";
            txtAddValorPago.Text = "";
            txtAddData.Text = "";
            txtAddDataPagamento.Text = "";
            gvParcelas.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            financeiro = _fCarnaubaFacade.GetFinanceiroById(FinanceiroId);

            if (!IsPostBackOrCallBack())
            {
                if (ItemFinanceiroValidado(Convert.ToInt64(FinanceiroId)))
                {
                    lblIdFinanceiro.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblIdFinanceiro.ForeColor = System.Drawing.Color.Red;
                }

                //var parametrosDeBusca = new CriterioPesquisaCompras { IdFinanceiro = Convert.ToInt64(FinanceiroId) };

                //var compras = _fCarnaubaFacade.ObtemCompras(parametrosDeBusca);
                //gvCompras.DataSource = compras;
                //gvCompras.DataBind();


                AddToCurrentPath("<font color=#156AE9> <<-Receita/Custo</font>", "Financeiro.aspx");
                UpdateTitleFluxoCaixa();

                

                if (financeiro.VendaAnimais)
                {
                    pnlVendas.Visible = true;
                }
                else
                {
                    pnlVendas.Visible = false;
                }

                lblIdFinanceiro.Text = financeiro.IdFinanceiro.ToString();
                lblDescricao.Text = financeiro.Descricao;

                if (!String.IsNullOrEmpty(financeiro.Data))
                {
                    lblData.Text = Convert.ToDateTime(financeiro.Data).ToString("dd/MM/yyyy");
                }
                else
                {
                    lblData.Text = "";
                }

                lblPropriedade.Text = financeiro.DescricaoPropriedade;
                lblGrupo.Text = financeiro.DescricaoGrupo;
                lblValorUnitario.Text = Math.Round(Convert.ToDecimal(financeiro.ValorUnitario), 2).ToString("C");
                lblValorTotal.Text = Math.Round(Convert.ToDecimal(financeiro.ValorTotal), 2).ToString("C");
                lblFornecedorCliente.Text = financeiro.DescricaoEmpresa;
                lblQuantidade.Text = financeiro.Quantidade.ToString();
            }

        }

        protected void comprasDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
           
            CriterioPesquisaCompras parametrosDeBusca = new CriterioPesquisaCompras { IdFinanceiro = Convert.ToInt64(FinanceiroId) };
            e.InputParameters.Add("criterio", parametrosDeBusca);
        }

        protected void documentosDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            CriterioPesquisaDocumentos parametrosDeBusca = new CriterioPesquisaDocumentos { IdFinanceiro = Convert.ToInt64(FinanceiroId) };
            e.InputParameters.Add("criterio", parametrosDeBusca);
        }

        protected void parcelasDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            CriterioPesquisaParcelas parametrosDeBusca = new CriterioPesquisaParcelas { IdFinanceiro = Convert.ToInt64(FinanceiroId) };
            e.InputParameters.Add("criterio", parametrosDeBusca);
        }

        protected void gvCompras_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ListControl ddlEditIdAnimal =
                (ListControl)gvCompras.Rows[e.RowIndex].FindControl("ddlEditIdAnimal");

            e.NewValues["IdAnimal"] = ddlEditIdAnimal.SelectedValue;

            ITextControl editValor = (ITextControl)gvCompras.Rows[e.RowIndex].FindControl("txtEditValor");
            string valor = editValor.Text;
            string valor2 = valor.Replace(",", ".");
            e.NewValues["Valor"] = valor2;
        }

        protected void gvParcelas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ITextControl editValorInicial = (ITextControl)gvParcelas.Rows[e.RowIndex].FindControl("txtEditValorInicial");
            string valorInicial = editValorInicial.Text;
            string valorInicial2 = valorInicial.Replace(",", ".");
            e.NewValues["ValorInicial"] = valorInicial2;

            ITextControl editValorPago = (ITextControl)gvParcelas.Rows[e.RowIndex].FindControl("txtEditValorPago");
            string valorPago = editValorPago.Text;
            string valorPago2 = valorPago.Replace(",", ".");
            e.NewValues["ValorPago"] = valorPago2;
        }

        protected void btnPesquisarDDlAnimal_Click(object sender, EventArgs e)
        {
            Control container = ((Control)sender).Parent;
            Button btnPesquisar = (Button)container.FindControl("btnPesquisarDDlAnimal");
            TextBox txtPesquisar = (TextBox)container.FindControl("txtAddPesquisaDdlAnimal");
            DropDownList ddlAddIdAnimal = (DropDownList)container.FindControl("ddlAddIdAnimal"); 

            CriterioPesquisaAnimal criterio = new CriterioPesquisaAnimal();

            criterio.TodosOsCampos = txtPesquisar.Text;

            var animais = _fCarnaubaFacade.ConsultaDdlAnimal(criterio);

            ddlAddIdAnimal.DataSource = animais;
            ddlAddIdAnimal.DataValueField = "Id";
            ddlAddIdAnimal.DataTextField = "NomeCompleto";
            ddlAddIdAnimal.DataBind();
            
        }

        protected void btnEditPesquisarDDlAnimal_Click(object sender, EventArgs e)
        {
            Control container = ((Control)sender).Parent;
            Button btnEditPesquisar = (Button)container.FindControl("btnEditPesquisarDDlAnimal");
            TextBox txtPesquisar = (TextBox)container.FindControl("txtEditPesquisaDdlAnimal");
            DropDownList ddlEditIdAnimal = (DropDownList)container.FindControl("ddlEditIdAnimal");
            //ITextControl lblIdAnimal = (ITextControl)container.FindControl("lblIdAnimal");

            CriterioPesquisaAnimal criterio = new CriterioPesquisaAnimal();

            criterio.TodosOsCampos = txtPesquisar.Text;

            var animais = _fCarnaubaFacade.ConsultaDdlAnimal(criterio);

            ddlEditIdAnimal.DataSource = animais;
            ddlEditIdAnimal.DataValueField = "Id";
            ddlEditIdAnimal.DataTextField = "NomeCompleto";
            //ddlEditIdAnimal.SelectedValue = lblIdAnimal.Text;
            ddlEditIdAnimal.DataBind();

        }

        protected void ibtValida_Click(object sender, EventArgs e)
        {
            string usuarioValidacao = UsuarioLogado.Name;
            DateTime usuarioDataValidacao = DateTime.Today;

            DataSourceFinanceiros dataSourceFinanceiros = new DataSourceFinanceiros();
            dataSourceFinanceiros.Valida(Convert.ToInt64(FinanceiroId), usuarioValidacao, usuarioDataValidacao);
            lblIdFinanceiro.ForeColor = System.Drawing.Color.Green;


        }

        private bool ItemFinanceiroValidado(long idFinanceiro)
        {
            DataSourceFinanceiros dataSource = new DataSourceFinanceiros();
            return dataSource.ItemFinanceiroValidado(idFinanceiro);
        }
    }
}