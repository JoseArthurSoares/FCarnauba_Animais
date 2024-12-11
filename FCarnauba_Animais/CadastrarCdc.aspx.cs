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
    public partial class CadastrarCdc : PaginaBase
    {
        private string act = null;
        public string CdcId;
        public long ultimoId = 0;
        public int cdcId = 0;
        private bool requestedValidation = false;

        public CadastrarCdc()
        {
            _PageType = new CadastrarCdcType();
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
            act = Request.QueryString["act"];
            AddToCurrentPath("<font color=#156AE9> <<-Cruzamentos</font>", "Cdcs.aspx");
            UpdateTitleCdc();
            cdcId = Convert.ToInt32(Request.QueryString["cdcId"]);
            CdcId = Request.QueryString["CdcId"];

            if (!IsPostBackOrCallBack())
            {
                if (!String.IsNullOrEmpty((string)Session["raca"]))
                {
                    ddlRaca.SelectedValue = (string)Session["raca"];
                    ddlRaca.Enabled = false;
                    ddlTouro.DataSource = FCarnaubaFacade.GetPais(ddlRaca.SelectedValue);
                }
                else
                {
                    ddlTouro.DataSource = FCarnaubaFacade.GetPais();

                }

                CdcId = Request.QueryString["CdcId"];
                CheckAllowance();

                cdcId = Convert.ToInt32(Request.Params["cdcId"]);
                var cdc = FCarnaubaFacade.GetCdcById(cdcId.ToString());

                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.DataBind();

                ddlPropriedade.Items.RemoveAt(0);

                
                ddlTouro.DataValueField = "Id";
                ddlTouro.DataTextField = "NomeCompleto";
                ddlTouro.DataBind();

                ddlTouro.Items.RemoveAt(0);
                

                //Matrizes
                UserControlCdcMatrizes1.CdcId = cdcId;
                UserControlCdcMatrizes1.DataSource = cdc.Matrizes;
                UserControlCdcMatrizes1.AddMode = true;
                //DataBind();
                UserControlCdcMatrizes1.LoadDropDowns();

                CheckAllowance();
                if (cdcId != 0)
                {
                    FillCdcFields(cdc);
                }

            }

            if (Request.Params["tabIndex"] != null)
            {
                var tabIndex = Request.Params["tabIndex"];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                    String.Format("setActive('{0}');", tabIndex), true);
            }
            else
            {
                UserControlCdcMatrizes1.EditMode = false;
                UserControlCdcMatrizes1.ReadOnly = true;
            }
            var unicode = new UnicodeEncoding();
            Form.Action = unicode.GetString(unicode.GetBytes(Request.Url.ToString()));

        }

        public bool EditMode
        {
            get
            {
                return (act == "edit" || act != null);
            }
        }

        private bool IsValidDate(string txt)
        {
            try
            {
                Convert.ToDateTime(txt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void cvCdc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvDataCobertura_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataCobertura.Text != "")
            {
                if (IsValidDate(txtDataCobertura.Text))
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = true;
            }

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvVeterinario_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            CdcId = Request.QueryString["CdcId"];
            requestedValidation = true;
            Page.Validate();

            if (IsValid)
            {
                Cdc cdc = new Cdc();

                if (!String.IsNullOrEmpty(txtCdc.Text))
                {
                    cdc.NCdc = Convert.ToInt64(txtCdc.Text);
                }

                if (!String.IsNullOrEmpty(txtDataCobertura.Text))
                {
                    cdc.DataCobertura = Convert.ToDateTime(txtDataCobertura.Text);
                }

                cdc.IdTouro = ddlTouro.SelectedValue;
                cdc.Tipo = ddlTipo.SelectedValue;
                cdc.Raca = ddlRaca.SelectedValue;
                cdc.IdPropriedade = ddlPropriedade.SelectedValue;

                //var nomeTouro = FCarnaubaFacade.GetNomeAnimal(cdc.IdTouro);

                cdc.Veterinario = txtVeterinario.Text;

                if (EditMode)
                {
                    var cdcCorr = FCarnaubaFacade.GetCdcById(CdcId);

                    if (FCarnaubaFacade.CdcExists(txtCdc.Text) && cdcCorr.NCdc != cdc.NCdc)
                    //if (FCarnaubaFacade.CdcExists(txtCdc.Text))
                    {
                        //ShowMessageControleLeiteiro("Lote já cadastrado!", false);
                        ExibeMensagem(TipoDeMensagem.Aviso, " Dados de Cruzamentos editado já cadastrado!");
                        return;
                    }

                    var tabIndex = "Cadastro";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                        String.Format("setActive('{0}');", tabIndex), true);
                    cdc.Usuario = UsuarioLogado.Name + " (editado)";
                    cdc.Id = Convert.ToInt32(CdcId);
                    FCarnaubaFacade.SalvaCdc(cdc);
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Dados de cruzamentos salvos com sucesso");
                }
                else
                {
                    if (FCarnaubaFacade.CdcExists(txtCdc.Text) && EditMode)
                    {
                        //ShowMessageControleLeiteiro("Lote já cadastrado!", false);
                        ExibeMensagem(TipoDeMensagem.Aviso, " Dados de Cruzamentos já cadastrado!");
                        return;
                    }
                    cdc.Usuario = UsuarioLogado.Name;
                    //UserControlMatrizes1.loteId = Convert.ToInt32(FCarnaubaFacade.AdicionaLote(lote));
                    cdc.Id = FCarnaubaFacade.AdicionaCdc(cdc);
                    UserControlCdcMatrizes1.CdcId = Convert.ToInt32(cdc.Id);
                    UserControlCdcMatrizes1.UpdateGridView(cdc.Id);
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Dados de cruzamentos adicionado com sucesso");

                }
            }
        }

        private void FillCdcFields(Cdc cdc)
        {
            txtCdc.Text = cdc.NCdc.ToString();
            txtDataCobertura.Text = Convert.ToString(cdc.DataCobertura);
            ddlTouro.SelectedValue = cdc.IdTouro;
            ddlTipo.SelectedValue = cdc.Tipo;
            ddlRaca.SelectedValue = cdc.Raca;
            txtVeterinario.Text = cdc.Veterinario;
            ddlPropriedade.SelectedValue = cdc.IdPropriedade;
        }
    }
}