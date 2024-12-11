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
    public partial class CadastrarLote : PaginaBase
    {
        private string act = null;
        public string LoteId;
        public long ultimoId = 0;
        public int loteId = 0;
        private bool requestedValidation = false;
        private string op = null;


        public CadastrarLote()
        {
            _PageType = new CadastrarLoteType();
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
            AddToCurrentPath("<font color=#156AE9> <<-Lote</font>", "Lotes.aspx");
            UpdateTitleControleLeiteiro();
            loteId = Convert.ToInt32(Request.QueryString["loteId"]);
            LoteId = Request.QueryString["LoteId"];

            op = Request.QueryString["op"];

            if(!String.IsNullOrEmpty(op))
                ExibeMensagem(TipoDeMensagem.Sucesso, "Lote adicionado com sucesso");

            if (!IsPostBackOrCallBack())
            {
                if (!String.IsNullOrEmpty((string)Session["raca"]))
                {
                    ddlRaca.SelectedValue = (string)Session["raca"];
                    ddlRaca.Enabled = false;
                }

                LoteId = Request.QueryString["LoteId"];
                CheckAllowance();

                loteId = Convert.ToInt32(Request.Params["loteId"]);
                var lote = FCarnaubaFacade.GetLoteById(loteId.ToString());
                //UserControlMatrizes1.DataSource = lote.Matrizes;

                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.DataBind();

                ddlPropriedade.Items.RemoveAt(0);


                //Produção de Leite
                UserControlProducaoLeite1.LoteId = loteId;
                UserControlProducaoLeite1.DataSource = lote.ProducoesLeite;
                UserControlProducaoLeite1.AddMode = true;
                //DataBind();
                UserControlProducaoLeite1.LoadDropDowns(ddlRaca.SelectedValue);

                CheckAllowance();

                if (loteId != 0)
                    FillLoteFields(lote);


            }

            if (Request.Params["tabIndex"] != null)
            {
                var tabIndex = Request.Params["tabIndex"];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                    String.Format("setActive('{0}');", tabIndex), true);
            }
            else
            {
                UserControlProducaoLeite1.EditMode = false;
                UserControlProducaoLeite1.ReadOnly = true;
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

        protected void cvCategoria_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvPOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtPOrdenha.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvSOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtSOrdenha.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvTOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtTOrdenha.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvControlador_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            LoteId = Request.QueryString["LoteId"];
            requestedValidation = true;
            Page.Validate();
            if (IsValid)
            {
                
                Lote lote = new Lote();

                if (!String.IsNullOrEmpty(txtLote.Text))
                {
                    lote.SLote = txtLote.Text;
                }

                lote.DataControle = Convert.ToDateTime(txtDataLote.Text);
                lote.IdPropriedade = ddlPropriedade.SelectedValue;
                lote.Raca = ddlRaca.SelectedValue;

                var nomePropriedade = FCarnaubaFacade.GetNomePropriedade(lote.IdPropriedade);

                lote.LoteDataPropriedade = lote.SLote + " - " + lote.DataControle.ToShortDateString() + " - " + nomePropriedade;

                lote.DataUsuario = DateTime.Today;

                lote.Categoria = txtCategoria.Text;
                lote.POrdenha = txtPOrdenha.Text;
                lote.SOrdenha = txtSOrdenha.Text;
                lote.TOrdenha = txtTOrdenha.Text;
                lote.Controlador = txtControlador.Text;
                lote.LiberarLotePesagem = ckLiberarLotePesagem.Checked;

                string POrdenha = lote.POrdenha.Replace(":", "").Replace("__", "");
                string SOrdenha = lote.SOrdenha.Replace(":", "").Replace("__", "");
                string TOrdenha = lote.TOrdenha.Replace(":", "").Replace("__", "");

                int IPOrdenha = Convert.ToInt32(POrdenha);
                int ISOrdenha = Convert.ToInt32(SOrdenha);

                if (IPOrdenha >= ISOrdenha)
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Hora da 1ª ordenha tem que ser menor que a 2ª ordenha.");
                    return;
                }

                if (!String.IsNullOrEmpty(TOrdenha))
                {
                    int ITOrdenha = Convert.ToInt32(TOrdenha);
                    if (ISOrdenha >= ITOrdenha)
                    {
                        ExibeMensagem(TipoDeMensagem.Aviso, "Hora da 2ª ordenha tem que ser menor que a 3ª ordenha.");
                        return;
                    }
                }


                if (EditMode)
                {
                    var loteCorr = FCarnaubaFacade.GetLoteById(LoteId);

                    if (FCarnaubaFacade.LoteExists(lote.SLote, lote.DataControle) && (loteCorr.DataControle != lote.DataControle || loteCorr.IdPropriedade != loteCorr.IdPropriedade))
                    {
                        //ShowMessageControleLeiteiro("Lote já cadastrado!", false);
                        ExibeMensagem(TipoDeMensagem.Aviso, "Lote já cadastrado!");
                        return;
                    }

                    var tabIndex = "Cadastro";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                        String.Format("setActive('{0}');", tabIndex), true);
                    lote.Usuario = UsuarioLogado.Name + " (editado)";
                    lote.Id = Convert.ToInt32(LoteId);
                    FCarnaubaFacade.SalvaLote(lote);

                    //var loteTemp = FCarnaubaFacade.GetLoteById(LoteId);

                    if (loteCorr.ProducoesLeite.Count > 0)
                    {
                        ExibeMensagem(TipoDeMensagem.Sucesso, "Lote salvo com sucesso");
                    }
                    else
                    {
                        ExibeMensagem(TipoDeMensagem.Sucesso, "Lote salvo com sucesso, mas sem matrizes cadastradas");
                    }

                }
                else
                {
                    if (FCarnaubaFacade.LoteExists(lote.SLote, lote.DataControle))
                    {
                        //ShowMessageControleLeiteiro("Lote já cadastrado!", false);
                        ExibeMensagem(TipoDeMensagem.Aviso, "Lote já cadastrado!");
                        return;
                    }

                    lote.Usuario = UsuarioLogado.Name;
                    //UserControlMatrizes1.loteId = Convert.ToInt32(FCarnaubaFacade.AdicionaLote(lote));
                    lote.Id = FCarnaubaFacade.AdicionaLote(lote);
                    UserControlProducaoLeite1.UpdateGridView(lote.Id);
                    //ExibeMensagem(TipoDeMensagem.Sucesso, "Lote adicionado com sucesso");
                    //ShowMessageControleLeiteiro("Lote adicionado com sucesso", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lote adicionado com sucesso')", true);
                    Response.Redirect("CadastrarLote.aspx?act=edit&loteId=" + lote.Id + "&op=add");

                }
            }
        }

        private void FillLoteFields(Lote lote)
        {
            txtLote.Text = lote.SLote;
            txtDataLote.Text = Convert.ToString(lote.DataControle);
            ddlPropriedade.SelectedValue = lote.IdPropriedade;
            ddlRaca.SelectedValue = lote.Raca;
            txtCategoria.Text = lote.Categoria;
            txtPOrdenha.Text = lote.POrdenha;
            txtSOrdenha.Text = lote.SOrdenha;
            txtTOrdenha.Text = lote.TOrdenha;
            txtControlador.Text = lote.Controlador;
            ckLiberarLotePesagem.Checked = lote.LiberarLotePesagem;
        }

        protected void DeleteControleLeiteiro_Click(int controleLeiteiroId)
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