using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FCarnauba_Animais.DataAccess;


namespace FCarnauba_Animais.UserControls
{
    public partial class UserControlCadastraControleLeiteiro : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        public event Action<int> DeleteControleLeiteiroClick;
        public int loteId;

        protected Mensagem Mensagem
        {
            get { return mensagem; }
        }

        protected void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);

        }

        public bool ReadOnly
        {
            get
            {
                if (ViewState["ReadOnly"] == null)
                {
                    return false;
                }
                return (bool)ViewState["ReadOnly"];
            }
            set { ViewState["ReadOnly"] = value; }
        }

        public bool EditMode
        {
            get
            {
                if (ViewState["EditMode"] == null)
                {
                    return false;
                }
                return (bool)ViewState["EditMode"];
            }
            set { ViewState["EditMode"] = value; }
        }

        private int ControleLeiteiroId
        {
            get
            {
                var controleLeiteiroId = Convert.ToInt32(Request.Params["controleLeiteiroId"]);
                if (controleLeiteiroId == 0)
                    controleLeiteiroId = Convert.ToInt32(Request.Params["id"]);

                return controleLeiteiroId;
            }
        }

        private string Act
        {
            get { return Request.Params["act"]; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            loteId = Convert.ToInt32(Request.Params["loteId"]);

            if (IsPostBack && Request.Form["__EVENTTARGET"] == "Button1")
            {
                btnAdicionaControleLeiteiro_Click(null, null);

            }

            else if (!IsPostBack && !Page.IsCallback)
            {

                if (ReadOnly)
                {

                    this.Panel1.Visible = false;
                }
                else
                {
                    //LoadDropDowns();
                    //ddlTipo.DataSource = //_sigoFacade.GetT
                }
            }

            //if (gridControles.Rows.Count > 0)
            //{
            //    gridControles.Visible = true;
            //    //lblMensagemMatrizes.Visible = false;
            //}
            //else
            //{
            //    gridControles.Visible = false;
            //    //lblMensagemMatrizes.Visible = true;
            //}
        }

        private static void PreenchePrimeiroVazioDdl(DropDownList drop)
        {
            drop.Items.Insert(0, "");
        }

        public void LoadDropDowns()
        {
            //ddlCidade.DataSource = _sigoFacade.GetCidades();
            //ddlCidade.DataBind();
            ////ddlSituacao.DataSource = _sigoFacade.GetSituacoes();
            ////ddlSituacao.DataBind();
            //ddlOrgaoExecutor.DataSource = _sigoFacade.GetOrgaos();
            //ddlOrgaoExecutor.DataBind();
            //ddlOrgaoInteressado.DataSource = _sigoFacade.GetOrgaos();
            //ddlOrgaoInteressado.DataBind();
            //PreenchePrimeiroVazioDdl(ddlOrgaoInteressado);
            //ddlTipo.DataSource = _sigoFacade.GetTiposObra();
            //ddlTipo.DataBind();
            ////ddlFormaGeorrefInicial.DataSource = _sigoFacade.GetFormasGeorreferenciamento();
            ////ddlFormaGeorrefInicial.DataBind();
            ////ddlKmlInput.DataSource = _sigoFacade.GetFormasGeorreferenciamento();
            ////ddlKmlInput.DataBind();
            //ddlComplexo.DataSource = _sigoFacade.GetComplexos();
            //ddlComplexo.DataValueField = "Id";
            //ddlComplexo.DataTextField = "Descricao";
            //ddlComplexo.DataBind();
            ////PreenchePrimeiroVazioDdl(ddlComplexo);

            //ddlPrograma.DataSource = _sigoFacade.GetProgramas();
            //ddlPrograma.DataBind();
            //PreenchePrimeiroVazioDdl(ddlPrograma);

            //ddlAcao.DataSource = _sigoFacade.GetAcoes();
            //ddlAcao.DataBind();
            //PreenchePrimeiroVazioDdl(ddlAcao);
        }

        public string LoteId
        {
            get { return (string)this.ViewState["loteId"]; }
            set { this.ViewState["loteId"] = value; }
        }

        public string Categoria
        {
            get { return txtCategoria.Text; }
            set { txtCategoria.Text = value; }
        }

        public string DataControle
        {
            get { return txtDataControle.Text; }
            set { txtDataControle.Text = value; }
        }

        public string DataProximaVisita
        {
            get { return txtDataProximaVisita.Text; }
            set { txtDataProximaVisita.Text = value; }
        }

        public string POrdenha
        {
            get { return txtPOrdenha.Text; }
            set { txtPOrdenha.Text = value; }
        }

        public string SOrdenha
        {
            get { return txtSOrdenha.Text; }
            set { txtSOrdenha.Text = value; }
        }

        public string TOrdenha
        {
            get { return txtTOrdenha.Text; }
            set { txtTOrdenha.Text = value; }
        }

        public string Controlador
        {
            get { return txtControlador.Text; }
            set { txtControlador.Text = value; }
        }

        public void FillControleLeiteiroValues(ControleLeiteiro controleLeiteiro)
        {
            LoteId = controleLeiteiro.IdLote;
            Categoria = controleLeiteiro.Categoria;
            DataControle = controleLeiteiro.DataControle.HasValue ? controleLeiteiro.DataControle.Value.ToString("dd/MM/yyyy") : "";
            DataProximaVisita = controleLeiteiro.DataProximaVisita.HasValue ? controleLeiteiro.DataProximaVisita.Value.ToString("dd/MM/yyyy") : "";
            POrdenha = controleLeiteiro.POrdenha;
            SOrdenha = controleLeiteiro.SOrdenha;
            TOrdenha = controleLeiteiro.TOrdenha;
            Controlador = controleLeiteiro.Controlador;

        }

        protected void cvCategoria_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvPOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvSOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvTOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvControlador_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvDataControle_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataControle.Text != "")
            {
                if (IsValidDate(txtDataControle.Text))
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
                args.IsValid = false;
            }
            
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvDataProximaVisita_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataProximaVisita.Text != "")
            {
                if (IsValidDate(txtDataProximaVisita.Text))
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

        protected void btnAdicionaControleLeiteiro_Click(object sender, EventArgs e)
        {
            ControleLeiteiro controleLeiteiroEdicao = _fCarnaubaFacade.GetControleLeiteiroById(ControleLeiteiroId.ToString());

            requestedValidation = true;

            Page.Validate();
            if (!Page.IsValid)
            {
                //ShowMessage("Complete os campos requeridos corretamente!", false);
                ExibeMensagem(TipoDeMensagem.Aviso, "Complete os campos requeridos corretamente!");
                //return;
            }
            else
            {

                if (loteId == 0)
                    loteId = Convert.ToInt32(LoteId);

                var controleLeiteiro = new ControleLeiteiro();
                controleLeiteiro.IdLote = loteId.ToString();
                controleLeiteiro.Categoria = txtCategoria.Text;
                if (IsValidDate(txtDataControle.Text))
                    controleLeiteiro.DataControle = Convert.ToDateTime(txtDataControle.Text);
                if (IsValidDate(txtDataProximaVisita.Text))
                    controleLeiteiro.DataProximaVisita = Convert.ToDateTime(txtDataProximaVisita.Text);
                controleLeiteiro.POrdenha = txtPOrdenha.Text;
                controleLeiteiro.SOrdenha = txtSOrdenha.Text;
                controleLeiteiro.TOrdenha = txtTOrdenha.Text;
                controleLeiteiro.Controlador = txtControlador.Text;
                controleLeiteiro.DataUsuario = DateTime.Today;

                if (!EditMode)
                {
                    //controleLeiteiro.DataCadastro = DateTime.Now;
                    //usuario
                    controleLeiteiro.Usuario = UsuarioLogado.Name;
                    _fCarnaubaFacade.AdicionaControleLeiteiro(controleLeiteiro);
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Leiteiro adicionado com sucesso");
                    //ShowMessage("Dados inseridos com sucesso.", true);

                }

                else
                {
                    controleLeiteiro.Id = ControleLeiteiroId;

                    controleLeiteiro.Usuario = UsuarioLogado.Name + " (editado)";
                    _fCarnaubaFacade.SalvaControleLeiteiro(controleLeiteiro);
                    //ShowMessage("Dados salvos com sucesso.", true);
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Leiteiro salvo com sucesso");

                }

                UpdateGridView();
                gridControles.Visible = true;
                //lblMensagemMatrizes.Visible = false;
                //Response.Redirect("~/CadastrarLote.aspx?act=edit&loteId=" + loteId + "&tabIndex=Controle Leiteiro");
            }



        }

        public List<ControleLeiteiro> Controles
        {
            set
            {
                this.gridControles.DataSource = value;
                this.gridControles.DataBind();
            }
        }

        public List<ControleLeiteiro> DataSource
        {
            set
            {
                this.gridControles.DataSource = value;
                this.gridControles.DataBind();
            }
        }

        public void SetEditMode()
        {
            ReadOnly = true;
        }


        private void ShowMessage(string msg, bool isGood = true)
        {
            var page = HttpContext.Current.Handler as PaginaBase;
            page.ShowMessageControleLeiteiro(msg, isGood);
        }

        protected void gridControles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                var targReg = (e.Row.DataItem as ControleLeiteiro).Id;

                //string key = gvObras.DataKeys[e.Row.RowIndex].Value.ToString();

                e.Row.Attributes.Add("id", "tr_contr_" + e.Row.RowIndex);
                e.Row.Attributes.Add("onmouseover",
                                     "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#D7E9FD';this.style.cursor='pointer'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", String.Format("location.href='DetalhesControleLeiteiro.aspx?controleLeiteiroId={0}';", targReg));
                //e.Row.Attributes.Add("onclick", "LoadDataFromJSON('" + (e.Row.DataItem as Obra).Id + "');");
            }
        }

        protected void btnDeleteControleLeiteiro_Click(object sender, EventArgs e)
        {
            if (DeleteControleLeiteiroClick != null)
            {

                LinkButton btnDelete = (LinkButton)sender;
                int ControleLeiteiroId = Convert.ToInt32(btnDelete.CommandArgument);

                //DeleteControleLeiteiroClick(ControleLeiteiroId);

                if (ControleLeiteiroId > 0)
                {
                    {
                        _fCarnaubaFacade.RemoveControleLeiteiro(ControleLeiteiroId.ToString());
                        //this.UserControlCadastraControleLeiteiro1.DataSource = _fCarnaubaFacade.GetControlesById(loteId.ToString());
                        UpdateGridView();
                        Response.Redirect("~/CadastrarLote.aspx?act=edit&loteId=" + loteId.ToString() + "&tabIndex=Controle Leiteiro");
                        //ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Leiteiro removido com sucesso");
                    }
                }

                

            }
        }

        protected void btnControleLeiteiro_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalhesControleleiteiro.aspx?controleLeiteiroId=" + ControleLeiteiroId);
        }

        private void UpdateGridView()
        {
            if (!String.IsNullOrEmpty(loteId.ToString()))
            {

                this.gridControles.DataSource = _fCarnaubaFacade.GetControlesById(loteId.ToString());
                this.gridControles.DataBind();
            }
        }
    }
}