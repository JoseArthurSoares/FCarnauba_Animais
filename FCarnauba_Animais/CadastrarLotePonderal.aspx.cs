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
    public partial class CadastrarLotePonderal : PaginaBase
    {
        private string act = null;
        public string LotePonderalId;
        public long ultimoId = 0;
        public int lotePonderalId = 0;
        private bool requestedValidation = false;
        private string op = null;


        public CadastrarLotePonderal()
        {
            _PageType = new CadastrarLotePonderalType();
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
            AddToCurrentPath("<font color=#156AE9> <<-Lote Ponderal</font>", "LotesPonderais.aspx");
            UpdateTitleControlePonderal();
            lotePonderalId = Convert.ToInt32(Request.QueryString["lotePonderalId"]);
            LotePonderalId = Request.QueryString["LotePonderalId"];

            op = Request.QueryString["op"];

            if (!String.IsNullOrEmpty(op))
                ExibeMensagem(TipoDeMensagem.Sucesso, "Lote ponderal adicionado com sucesso");

            if (!IsPostBackOrCallBack())
            {
                if (!String.IsNullOrEmpty((string)Session["raca"]))
                {
                    ddlRaca.SelectedValue = (string)Session["raca"];
                    ddlRaca.Enabled = false;
                }

                LotePonderalId = Request.QueryString["LotePonderalId"];
                CheckAllowance();

                lotePonderalId = Convert.ToInt32(Request.Params["lotePonderalId"]);
                var lotePonderal = FCarnaubaFacade.GetLotePonderalById(lotePonderalId.ToString());
                //UserControlMatrizes1.DataSource = lote.Matrizes;

                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.DataBind();

                ddlPropriedade.Items.RemoveAt(0);


                //Mensurações
                UserControlMensuracao1.LotePonderalId = lotePonderalId;
                UserControlMensuracao1.DataSource = lotePonderal.Mensuracoes;
                UserControlMensuracao1.AddMode = true;
                //DataBind();
                UserControlMensuracao1.LoadDropDowns(ddlRaca.SelectedValue);

                CheckAllowance();

                if (lotePonderalId != 0)
                    FillLotePonderalFields(lotePonderal);


            }

            if (Request.Params["tabIndex"] != null)
            {
                var tabIndex = Request.Params["tabIndex"];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                    String.Format("setActive('{0}');", tabIndex), true);
            }
            else
            {
                UserControlMensuracao1.EditMode = false;
                UserControlMensuracao1.ReadOnly = true;
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

        protected void cvLote_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvDataLote_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataLote.Text != "")
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

        protected void cvControlador_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            LotePonderalId = Request.QueryString["LotePonderalId"];
            requestedValidation = true;
            Page.Validate();
            if (IsValid)
            {

                LotePonderal lotePonderal = new LotePonderal();

                if (!String.IsNullOrEmpty(txtLote.Text))
                {
                    lotePonderal.SLote = txtLote.Text;
                }

                lotePonderal.DataControle = Convert.ToDateTime(txtDataLote.Text);
                lotePonderal.IdPropriedade = ddlPropriedade.SelectedValue;
                lotePonderal.Raca = ddlRaca.SelectedValue;

                var nomePropriedade = FCarnaubaFacade.GetNomePropriedade(lotePonderal.IdPropriedade);

                lotePonderal.LoteDataPropriedade = lotePonderal.SLote + " - " + lotePonderal.DataControle.ToShortDateString() + " - " + nomePropriedade;
                lotePonderal.DataUsuario = DateTime.Today;
                lotePonderal.Controlador = txtControlador.Text;
                lotePonderal.LiberarLoteMensuracao = ckLiberarLoteMensuracao.Checked;

                if (FCarnaubaFacade.LotePonderalExists(lotePonderal.SLote, lotePonderal.DataControle))
                {
                    //ShowMessageControleLeiteiro("Lote já cadastrado!", false);
                    ExibeMensagem(TipoDeMensagem.Aviso, "Lote ponderal já cadastrado!");
                    return;
                }

                if (EditMode)
                {
                    var tabIndex = "Cadastro";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                        String.Format("setActive('{0}');", tabIndex), true);
                    lotePonderal.Usuario = UsuarioLogado.Name + " (editado)";
                    lotePonderal.Id = Convert.ToInt32(LotePonderalId);
                    FCarnaubaFacade.SalvaLotePonderal(lotePonderal);

                    var lotePonderalTemp = FCarnaubaFacade.GetLotePonderalById(LotePonderalId);

                    if (lotePonderalTemp.Mensuracoes.Count > 0)
                    {
                        ExibeMensagem(TipoDeMensagem.Sucesso, "Lote ponderal salvo com sucesso");
                    }
                    else
                    {
                        ExibeMensagem(TipoDeMensagem.Sucesso, "Lote ponderal salvo com sucesso, mas sem mensurações cadastradas");
                    }

                }
                else
                {

                    lotePonderal.Usuario = UsuarioLogado.Name;
                    //UserControlMatrizes1.loteId = Convert.ToInt32(FCarnaubaFacade.AdicionaLote(lote));
                    lotePonderal.Id = FCarnaubaFacade.AdicionaLotePonderal(lotePonderal);
                    UserControlMensuracao1.UpdateGridView(lotePonderal.Id);
                    //ExibeMensagem(TipoDeMensagem.Sucesso, "Lote adicionado com sucesso");
                    //ShowMessageControleLeiteiro("Lote adicionado com sucesso", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lote adicionado com sucesso')", true);
                    Response.Redirect("CadastrarLotePonderal.aspx?act=edit&lotePonderalId=" + lotePonderal.Id + "&op=add");

                }
            }
        }

        private void FillLotePonderalFields(LotePonderal lotePonderal)
        {
            txtLote.Text = lotePonderal.SLote;
            txtDataLote.Text = Convert.ToString(lotePonderal.DataControle);
            ddlPropriedade.SelectedValue = lotePonderal.IdPropriedade;
            ddlRaca.SelectedValue = lotePonderal.Raca;
            txtControlador.Text = lotePonderal.Controlador;
            ckLiberarLoteMensuracao.Checked = lotePonderal.LiberarLoteMensuracao;
        }

        protected void DeleteControlePonderal_Click(int controlePonderalId)
        {
            //if (!String.IsNullOrEmpty(controleLeiteiroId.ToString()))
            //{
            //    _fCarnaubaFacade.RemoveControleLeiteiro(controleLeiteiroId.ToString());
            //    this.UserControlCadastraControleLeiteiro1.DataSource = _fCarnaubaFacade.GetControlesById(loteId.ToString());
            //    Response.Redirect("~/CadastrarLote.aspx?act=edit&loteId=" + loteId.ToString() + "&tabIndex=Controle Leiteiro");
            //}
        }
    }
}