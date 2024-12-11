using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.util;
using FCarnauba_Animais.UserControls;

namespace FCarnauba_Animais
{
    public partial class CadastrarFluxoCaixa : PaginaBase
    {
        private string act = null;
        public string FluxoCaixaId;
        public long ultimoId = 0;
        public int fluxoCaixaId = 0;
        private bool requestedValidation = false;

        public CadastrarFluxoCaixa()
        {
            _PageType = new CadastrarFluxoCaixaType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public bool EditMode
        {
            get
            {
                return (act == "edit" || act != null);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AddToCurrentPath("<font color=#156AE9> <<-Fluxo de Caixa</font>", "FluxosCaixa.aspx");
            UpdateTitleFluxoCaixa();
            fluxoCaixaId = Convert.ToInt32(Request.QueryString["fluxoCaixaId"]);
            FluxoCaixaId = Request.QueryString["FluxoCaixaId"];

            act = Request.QueryString["act"];

            if (!IsPostBackOrCallBack())
            {
                FluxoCaixaId = Request.QueryString["FluxoCaixaId"];
                CheckAllowance();

                fluxoCaixaId = Convert.ToInt32(Request.Params["fluxoCaixaId"]);
                var fluxoCaixa = FCarnaubaFacade.GetFluxoCaixaById(fluxoCaixaId.ToString());

                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.DataBind();
                ddlPropriedade.Items.RemoveAt(0);

                ddlCentroCusto.DataSource = FCarnaubaFacade.GetCentrosCusto();
                ddlCentroCusto.DataValueField = "Id";
                ddlCentroCusto.DataTextField = "Descricao";
                ddlCentroCusto.DataBind();
                ddlCentroCusto.Items.RemoveAt(0);

                if (act != null)
                {
                    CheckAllowance();
                    FillFluxoCaixaFields(fluxoCaixa);
                }
            }
        }

        private void FillFluxoCaixaFields(FluxoCaixa fluxoCaixa)
        {
            ddlPropriedade.SelectedValue = fluxoCaixa.IdPropriedade;
            ddlCentroCusto.SelectedValue = fluxoCaixa.IdCentroCusto;
            txtData.Text = Convert.ToString(fluxoCaixa.Data);
            ddlTipo.SelectedValue = fluxoCaixa.Tipo;
            txtValor.Text = Convert.ToString(fluxoCaixa.Valor);
            txtDescricao.Text = fluxoCaixa.Descricao;
        }

        protected void cvData_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtData.Text != "")
            {
                try
                {
                    args.IsValid = true;
                    return;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
            args.IsValid = false;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvValor_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtValor.Text))
                txtValor.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvDescricao_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtDescricao.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            FluxoCaixaId = Request.QueryString["FluxoCaixaId"];
            requestedValidation = true;
            Page.Validate();
            if (IsValid)
            {

                FluxoCaixa fluxoCaixa = new FluxoCaixa();

                fluxoCaixa.IdPropriedade = ddlPropriedade.SelectedValue;
                fluxoCaixa.IdCentroCusto = ddlCentroCusto.SelectedValue;
                fluxoCaixa.Data = Convert.ToDateTime(txtData.Text);
                fluxoCaixa.Tipo = ddlTipo.SelectedValue;
                fluxoCaixa.Valor = Convert.ToDouble(txtValor.Text);
                fluxoCaixa.Descricao = txtDescricao.Text;
                fluxoCaixa.DataUsuario = DateTime.Today;

                if (EditMode)
                {


                    fluxoCaixa.Usuario = UsuarioLogado.Name + " (editado)";
                    fluxoCaixa.Id = Convert.ToInt32(FluxoCaixaId);
                    FCarnaubaFacade.SalvaFluxoCaixa(fluxoCaixa);
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Fluxo de Caixa salvo com sucesso");

                }
                else
                {
                    //if (FCarnaubaFacade.LoteExists(txtLote.Text) && EditMode)
                    //{
                    //    //ShowMessageControleLeiteiro("Lote já cadastrado!", false);
                    //    ExibeMensagem(TipoDeMensagem.Aviso, "Lote já cadastrado!");
                    //    return;
                    //}
                    fluxoCaixa.Usuario = UsuarioLogado.Name;
                    FCarnaubaFacade.AdicionaFluxoCaixa(fluxoCaixa);
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Fluxo de Caixa adicionado com sucesso");

                }
            }
        }
    }
}