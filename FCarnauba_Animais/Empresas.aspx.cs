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
    public partial class Empresas : PaginaBase
    {
        public Empresas()
        {
            _PageType = new EmpresasType();
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
            
        }

        //protected void gvEmpresas_RowDeleted(object sender, GridViewDeletedEventArgs e)
        //{
        //    ExibeMensagem(TipoDeMensagem.Sucesso, "Empresa removida com sucesso");
            
        //}

        //protected void gvEmpresas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        //{

        //    //ExibeMensagem(TipoDeMensagem.Sucesso, "Empresa modificada com sucesso");
            
        //}

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                try
                {
                    Control controlContainer = gvEmpresas.FooterRow;
                    AdicionaItem(controlContainer);
                }
                catch (Exception exc)
                {
                    TrataExcecao("Erro ao tentar adicionar empresa", exc);
                    return;
                }

                ExibeMensagem(TipoDeMensagem.Sucesso, "Empresa adicionada com sucesso");
            }
        }

        protected void empresasDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Empresa removida com sucesso",
                "Ocorreu um erro ao tentar remover o item",
                e);
        }

        protected void empresasDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus(
                "Empresa modificada com sucesso",
                "Ocorreu um erro ao tentar modificar o item",
                e);
        }

        protected void ibtNew_Click(object sender, ImageClickEventArgs e)
        {
            Control container = ((Control)sender).Parent;
            AdicionaItem(container);
            ExibeMensagem(TipoDeMensagem.Sucesso, "Empresa adicionada com sucesso.");
        }

        private void AdicionaItem(Control controlContainer)
        {
            ITextControl txtAddRazaoSocial = (ITextControl)controlContainer.FindControl("txtAddRazaoSocial");
            ITextControl txtAddCnpjCpf = (ITextControl)controlContainer.FindControl("txtAddCnpjCpf");
            ITextControl txtAddEndereco = (ITextControl)controlContainer.FindControl("txtAddEndereco");
            //ITextControl txtAddMunicipio = (ITextControl)controlContainer.FindControl("txtAddMunicipio");
            ITextControl txtAddTelefones = (ITextControl)controlContainer.FindControl("txtAddTelefones");
            ITextControl txtAddEmail = (ITextControl)controlContainer.FindControl("txtAddEmail");
            ListControl ddlAddTipo = (ListControl)controlContainer.FindControl("ddlAddTipo");
            ListControl ddlAddUf = (ListControl)controlContainer.FindControl("ddlAddUf");
            ListControl ddlAddMunicipios = (ListControl)controlContainer.FindControl("ddlAddMunicipios");

            Empresa empresa = new Empresa();
            empresa.RazaoSocial = txtAddRazaoSocial.Text;
            empresa.CnpjCpf = txtAddCnpjCpf.Text;
            empresa.Endereco = txtAddEndereco.Text;
            empresa.Municipio = ddlAddMunicipios.SelectedValue; ;
            empresa.Telefones = txtAddTelefones.Text;
            empresa.Email = txtAddEmail.Text;
            empresa.Tipo = ddlAddTipo.SelectedValue;
            empresa.Uf = ddlAddUf.SelectedValue;

            empresa.DataUsuario = DateTime.Today;
            empresa.Usuario = UsuarioLogado.Name;

            DataSourceEmpresas dataSourceEmpresas = new DataSourceEmpresas();

            dataSourceEmpresas.Insira(empresa);

            txtAddCnpjCpf.Text = "";
            txtAddRazaoSocial.Text = "";
            txtAddEndereco.Text = "";
            //txtAddMunicipio.Text = "";
            txtAddTelefones.Text = "";
            txtAddEmail.Text = "";
            ddlAddTipo.SelectedIndex = -1;
            ddlAddUf.SelectedIndex = -1;
            ddlAddMunicipios.SelectedIndex = -1;
            
            gvEmpresas.DataBind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            gvEmpresas.DataBind();
        }


        protected void empresasDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            CriterioPesquisaEmpresa criterio = new CriterioPesquisaEmpresa();

            if (txtDescricao.Text.Trim() != String.Empty)
            {
                criterio.TodosOsCampos = txtDescricao.Text;
            }
            else
            {
                criterio.TodosOsCampos = "";
            }

            if (!String.IsNullOrEmpty(ddlUf.SelectedValue))
                criterio.Uf = ddlUf.SelectedValue;

            e.InputParameters.Add("criterio", criterio);
        }

        protected void gvEmpresas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Empresa empresa = (Empresa)e.Row.DataItem;

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    DropDownList ddlEditTipo = (DropDownList)e.Row.FindControl("ddlEditTipo");
                    ddlEditTipo.DataBind();
                    ddlEditTipo.SelectedValue = empresa.Tipo;

                    DropDownList ddlEditUf = (DropDownList)e.Row.FindControl("ddlEditUf");
                    ddlEditUf.DataBind();
                    ddlEditUf.SelectedValue = empresa.Uf;

                    DropDownList ddlEditMunicipios = (DropDownList)e.Row.FindControl("ddlEditMunicipios");
                    ddlEditMunicipios.DataBind();
                    ddlEditMunicipios.SelectedValue = empresa.Municipio;

                    e.Row.BackColor = System.Drawing.Color.Bisque;
                }
            }
        }

        protected void gvEmpresas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ListControl ddlEditTipo =
                (ListControl)gvEmpresas.Rows[e.RowIndex].FindControl("ddlEditTipo");

            e.NewValues["Tipo"] = ddlEditTipo.SelectedValue;

            ListControl ddlEditUf =
                (ListControl)gvEmpresas.Rows[e.RowIndex].FindControl("ddlEditUf");

            e.NewValues["Uf"] = ddlEditUf.SelectedValue;

            ListControl ddlEditMunicipios =
                (ListControl)gvEmpresas.Rows[e.RowIndex].FindControl("ddlEditMunicipios");

            e.NewValues["Municipio"] = ddlEditMunicipios.SelectedValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}