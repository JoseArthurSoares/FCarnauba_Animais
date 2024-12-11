using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.util;

namespace FCarnauba_Animais.UserControls
{
    public partial class UserControlProducaoLeite : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int loteId;
        private int totalProducoes = 0;
        public ProducaoLeite producao = null;
        private string loteID;
        string lastProducao = null;

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

        public bool AddMode
        {
            get
            {
                if (ViewState["AddMode"] == null)
                {
                    return false;
                }
                return (bool)ViewState["AddMode"];
            }
            set { ViewState["AddMode"] = value; }
        }

        public int LoteId
        {
            get
            {
                return Convert.ToInt32(ViewState["LoteId"]);
            }
            set { ViewState["LoteId"] = value; }
        }

        //private int ControleLeiteiroId
        //{
        //    get
        //    {
        //        var controleLeiteiroId = Convert.ToInt32(Request.Params["controleLeiteiroId"]);
        //        if (controleLeiteiroId == 0)
        //            controleLeiteiroId = Convert.ToInt32(Request.Params["id"]);

        //        return controleLeiteiroId;
        //    }
        //}

        public int ProducaoInd
        {
            get
            {
                if (ViewState["ProducaoInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["ProducaoInd"]);
            }
            set { ViewState["ProducaoInd"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (ProducaoInd > -1)
                {
                    UpdateProducaoData();
                }

                //if (!EditMode)
                //{

                //}
                //else
                //{
                //    lblNumeroMedicaoObra.Text = fisico.Medicao;
                //}

                if (EditMode)
                {
                    //this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes();
                    //ddlMatrizes.DataValueField = "Id";
                    //ddlMatrizes.DataTextField = "NomeCompleto";
                    //ddlMatrizes.DataBind();
                }

                //this.ddlCrias.DataSource = _fCarnaubaFacade.GetCrias(tx);
            }
        }

        //public void LoadDropDowns(string registroCGE)
        //{
        //    //ddlSituacao.DataSource = _sigoFacade.GetSituacoes();
        //    //ddlSituacao.DataBind();
        //    ddlStatus.DataSource = _sigoFacade.GetStatus();
        //    ddlStatus.DataBind();
        //    ddlFonteRecursos.DataSource = _sigoFacade.GetFontes(registroCGE);
        //    ddlFonteRecursos.DataBind();
        //}

        public string IdMatriz
        {
            get { return ddlMatrizes.SelectedValue; }
            set { ddlMatrizes.SelectedValue = value; }
        }


        public string IdCria
        {
            get { return ddlCrias.SelectedValue; }
            set { ddlCrias.SelectedValue = value; }
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

        public bool BezerrosPe
        {
            get { return ckBezerrosPe.Checked; }
            set { ckBezerrosPe.Checked = value; }
        }

        public string TetosFuncionais
        {
            get { return ddlTetosFuncionais.SelectedValue; }
            set { ddlTetosFuncionais.SelectedValue = value; }
        }

        public string Observacoes
        {
            get { return txtObservacoes.Text; }
            set { txtObservacoes.Text = value; }
        }

        public string RegimeAlimentar
        {
            get { return ddlRegimeAlimentar.SelectedValue; }
            set { ddlRegimeAlimentar.SelectedValue = value; }
        }

        public bool SairControle
        {
            get { return ckSairControle.Checked; }
            set { ckSairControle.Checked = value; }
        }

        public string DataSaidaControle
        {
            get { return txtDataSaidaControle.Text; }
            set { txtDataSaidaControle.Text = value; }
        }

        public string Motivo
        {
            get { return ddlMotivo.SelectedValue; }
            set { ddlMotivo.SelectedValue = value; }
        }

        public string GordPOrdenha
        {
            get { return txtGordPOrdenha.Text; }
            set { txtGordPOrdenha.Text = value; }
        }

        public string GordSOrdenha
        {
            get { return txtGordSOrdenha.Text; }
            set { txtGordSOrdenha.Text = value; }
        }

        public string GordTOrdenha
        {
            get { return txtGordTOrdenha.Text; }
            set { txtGordTOrdenha.Text = value; }
        }

        public string ProtPOrdenha
        {
            get { return txtProtPOrdenha.Text; }
            set { txtProtPOrdenha.Text = value; }
        }

        public string ProtSOrdenha
        {
            get { return txtProtSOrdenha.Text; }
            set { txtProtSOrdenha.Text = value; }
        }

        public string ProtTOrdenha
        {
            get { return txtProtTOrdenha.Text; }
            set { txtProtTOrdenha.Text = value; }
        }

        public bool Receptora
        {
            get { return ckReceptora.Checked; }
            set { ckReceptora.Checked = value; }
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

        private void ShowMessage(string msg, bool isGood = true)
        {
            var page = HttpContext.Current.Handler as PaginaBase;
            page.ShowMessageControleLeiteiro(msg, isGood);
        }

        protected void btnCadastrar_Click(object send, EventArgs e)
        {
            FCarnaubaDataAccess fCarnaubaDataAccess = new FCarnaubaDataAccess();
            fCarnaubaDataAccess.OpenConnection();
            //loteID = fCarnaubaDataAccess.GetLoteIdControleLeiteiro(ControleLeiteiroId);
            Lote lote = fCarnaubaDataAccess.GetLoteById(LoteId.ToString());
            requestedValidation = true;

            Page.Validate();

            if (!Page.IsValid)
            {
                //ShowMessage("Complete os campos requeridos corretamente.", false);
                ExibeMensagem(TipoDeMensagem.Aviso, "Complete os campos requeridos corretamente.");
                return;
            }

            fCarnaubaDataAccess.CloseConnection();
            fCarnaubaDataAccess = new FCarnaubaDataAccess();
            fCarnaubaDataAccess.OpenConnection();
            //loteID = fCarnaubaDataAccess.GetLoteIdControleLeiteiro(ControleLeiteiroId);
            lote.Id = LoteId;
            fCarnaubaDataAccess.CloseConnection();

            var producaoLeite = new ProducaoLeite();

            producaoLeite.IdMatriz = ddlMatrizes.SelectedValue;
            producaoLeite.IdCria = ddlCrias.SelectedValue;

            

            
            int diasLactacao = 0;
            DateTime? dataEntradaControle = _fCarnaubaFacade.GetDataEntradaControle(LoteId.ToString(),producaoLeite.IdMatriz, producaoLeite.IdCria);

            if (dataEntradaControle != null)
            {

                TimeSpan diferenca = (TimeSpan)(lote.DataControle - dataEntradaControle);
                if (diferenca.TotalDays > 0)
                {
                    diasLactacao = (int)diferenca.TotalDays;
                }

            }
            else
            {
                if (producaoLeite.IdCria != "0")
                {
                    DateTime dataEntradaControleCria = _fCarnaubaFacade.GetDataNascimentoCria(producaoLeite.IdCria);
                    //dataEntradaControleCria = dataEntradaControleCria.AddDays(7);

                    TimeSpan diferenca = (TimeSpan)(lote.DataControle - dataEntradaControleCria);
                    if (diferenca.TotalDays > 0)
                    {
                        diasLactacao = (int)diferenca.TotalDays;
                    }
                }
            }
            
            producaoLeite.DiasLactacao = diasLactacao;

            if (!String.IsNullOrEmpty(txtPOrdenha.Text))
            {
                producaoLeite.POrdenha = Convert.ToDouble(txtPOrdenha.Text);
            }
            else
            {
                producaoLeite.POrdenha = 0;
            }
            if (!String.IsNullOrEmpty(txtSOrdenha.Text))
            {
                producaoLeite.SOrdenha = Convert.ToDouble(txtSOrdenha.Text);
            }
            else
            {
                producaoLeite.SOrdenha = 0;
            }
            if (!String.IsNullOrEmpty(txtTOrdenha.Text))
            {
                producaoLeite.TOrdenha = Convert.ToDouble(txtTOrdenha.Text);
            }
            else
            {
                producaoLeite.TOrdenha = 0;
            }

            //aqui total

            producaoLeite.Total = producaoLeite.POrdenha + producaoLeite.SOrdenha + producaoLeite.TOrdenha;

            producaoLeite.BezerrosPe = ckBezerrosPe.Checked;

            producaoLeite.TetosFuncionais = Convert.ToInt32(ddlTetosFuncionais.SelectedValue);
            producaoLeite.Obs = txtObservacoes.Text;
            producaoLeite.RegimeAlimentar = ddlRegimeAlimentar.SelectedValue;

            if (IsValidDate(txtDataSaidaControle.Text))
                producaoLeite.DataSaidaControle = Convert.ToDateTime(txtDataSaidaControle.Text);

            
            producaoLeite.Receptora = ckReceptora.Checked;

            if (!String.IsNullOrEmpty(txtGordPOrdenha.Text))
            {
                producaoLeite.GordPOrdenha = Convert.ToDouble(txtGordPOrdenha.Text);
            }
            else
            {
                producaoLeite.GordPOrdenha = 0;
            }

            if (!String.IsNullOrEmpty(txtGordSOrdenha.Text))
            {
                producaoLeite.GordSOrdenha = Convert.ToDouble(txtGordSOrdenha.Text);
            }
            else
            {
                producaoLeite.GordSOrdenha = 0;
            }

            if (!String.IsNullOrEmpty(txtGordTOrdenha.Text))
            {
                producaoLeite.GordTOrdenha = Convert.ToDouble(txtGordTOrdenha.Text);
            }
            else
            {
                producaoLeite.GordTOrdenha = 0;
            }

            if (!String.IsNullOrEmpty(txtProtPOrdenha.Text))
            {
                producaoLeite.ProtPOrdenha = Convert.ToDouble(txtProtPOrdenha.Text);
            }
            else
            {
                producaoLeite.ProtPOrdenha = 0;
            }

            if (!String.IsNullOrEmpty(txtProtSOrdenha.Text))
            {
                producaoLeite.ProtSOrdenha = Convert.ToDouble(txtProtSOrdenha.Text);
            }
            else
            {
                producaoLeite.ProtSOrdenha = 0;
            }

            if (!String.IsNullOrEmpty(txtProtTOrdenha.Text))
            {
                producaoLeite.ProtTOrdenha = Convert.ToDouble(txtProtTOrdenha.Text);
            }
            else
            {
                producaoLeite.ProtTOrdenha = 0;
            }

            producaoLeite.SairControle = ckSairControle.Checked;
            producaoLeite.Motivo = ddlMotivo.SelectedValue;

            if (producaoLeite.POrdenha < 2 || producaoLeite.Total < 4)
            {
                ExibeMensagem(TipoDeMensagem.Aviso, "Matriz com baixa prdução. O usuário tem a opção de encerrar a laçtação nos campos de encerramento.");
            }


            if (EditMode)
            {
                var prodLeiteAnterior = _fCarnaubaFacade.GetProducaoLeiteByIndex(LoteId, ProducaoInd);

                if (prodLeiteAnterior.IdMatriz != producaoLeite.IdMatriz)
                {

                    if (_fCarnaubaFacade.MatrizControleLeiteiroExists(lote.Id.ToString(), producaoLeite.IdMatriz))
                    {
                        ExibeMensagem(TipoDeMensagem.Aviso, "Matriz editada já cadastrada no lote.");
                        return;
                    }
                }

               _fCarnaubaFacade.SalvaProducaoLeite(LoteId, ProducaoInd, producaoLeite, prodLeiteAnterior);

               ExibeMensagem(TipoDeMensagem.Sucesso, "Produção de Leite salva com sucesso");
               //ShowMessage("Dados salvos com sucesso.", true);
               //return;
            }
            else
            {
                if (_fCarnaubaFacade.MatrizControleLeiteiroExists(lote.Id.ToString(), producaoLeite.IdMatriz))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Matriz já cadastrada no lote.");
                    return;
                }

                _fCarnaubaFacade.AdicionaProducaoLeite(LoteId, producaoLeite);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Produção de Leite adicionada com sucesso");
                //ShowMessage("Dados inseridos com sucesso.", true);
                //return;
            }

            UpdateProducaoData();
            UpdateGridView();
        }

        public List<ProducaoLeite> DataSource
        {
            set
            {
                totalProducoes = value.Count;
                this.gridViewProducaoLeite.DataSource = value;
                this.gridViewProducaoLeite.DataBind();
            }
        }

        protected void cvPOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtPOrdenha.Text))
                txtPOrdenha.Text = "0";

            if (Convert.ToDouble(txtPOrdenha.Text) > 0)
            {
                args.IsValid = true;

            }
            else
            {
                if (ckSairControle.Checked)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvSOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtSOrdenha.Text))
                txtSOrdenha.Text = "0";

            if (Convert.ToDouble(txtSOrdenha.Text) > 0)
            {
                args.IsValid = true;

            }
            else
            {
                if (ckSairControle.Checked)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvTOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtTOrdenha.Text))
                txtTOrdenha.Text = "0";

            
            args.IsValid = true;

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvDataSaidaControle_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataSaidaControle.Text != "" && txtDataSaidaControle.Text != "__/__/____")
            {
                if (IsValidDate(txtDataSaidaControle.Text))
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

        protected void cvGordPOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtGordPOrdenha.Text))
                txtGordPOrdenha.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvGordSOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtGordSOrdenha.Text))
                txtGordSOrdenha.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvGordTOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (String.IsNullOrEmpty(txtGordTOrdenha.Text))
                txtGordTOrdenha.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvProtPOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtProtPOrdenha.Text))
                txtProtPOrdenha.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvProtSOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtProtSOrdenha.Text))
                txtProtSOrdenha.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvProtTOrdenha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtProtTOrdenha.Text))
                txtProtTOrdenha.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void ddlMatrizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lote lote = _fCarnaubaFacade.GetLoteById(LoteId.ToString());
            var idMatriz = ddlMatrizes.SelectedValue;
            ddlCrias.DataSource = _fCarnaubaFacade.GetCriasControleLeiteiro(idMatriz, lote.DataControle);
            ddlCrias.DataValueField = "Id";
            ddlCrias.DataTextField = "NomeCompleto";
            ddlCrias.DataBind();

            //Redirect depos aqui
            //Response.Redirect("EditaControleLeiteiro.aspx?controleLeiteiroId=" + ControleLeiteiroId + "&&tabIndex=#producaoleite-tab");
        }

        private void UpdateProducaoData()
        {
            var loteId = Convert.ToInt32(Request.Params["loteId"]);
            if (loteId == 0)
                loteId = Convert.ToInt32(Request.Params["id"]);
            //var obraId = Convert.ToInt32(Request.Params["obraId"]);
            var targInd = ProducaoInd == -1 ? 0 : ProducaoInd;
            producao = _fCarnaubaFacade.GetProducaoLeiteByIndex(loteId, targInd);
            
        }

        private void UpdateGridView()
        {
            
            var producoes = _fCarnaubaFacade.GetProducaoLeite(LoteId);
            totalProducoes = producoes.Count;
            gridViewProducaoLeite.DataSource = producoes;
            gridViewProducaoLeite.DataBind();
        }

        public void UpdateGridView(long loteId)
        {
            LoteId = (int)loteId;
            var producoes = _fCarnaubaFacade.GetProducaoLeite(Convert.ToInt32(loteId));
            totalProducoes = producoes.Count;
            gridViewProducaoLeite.DataSource = producoes;
            gridViewProducaoLeite.DataBind();
        }

        public void FillPLValues(ProducaoLeite producao)
        {
            string raca = _fCarnaubaFacade.GetAnimalById(producao.IdMatriz).Raca;
            this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes(raca);
            ddlMatrizes.DataValueField = "Id";
            ddlMatrizes.DataTextField = "NomeCompleto";
            ddlMatrizes.SelectedValue = producao.IdMatriz;
            ddlMatrizes.DataBind();

            ddlCrias.DataSource = _fCarnaubaFacade.GetCriasControleLeiteiro(producao.IdMatriz);
            ddlCrias.DataValueField = "Id";
            ddlCrias.DataTextField = "NomeCompleto";
            ddlCrias.SelectedValue = producao.IdCria;
            ddlCrias.DataBind();

            POrdenha = producao.POrdenha.ToString();
            SOrdenha = producao.SOrdenha.ToString();
            TOrdenha = producao.TOrdenha.ToString();
            BezerrosPe = producao.BezerrosPe;
            SairControle = producao.SairControle;
            TetosFuncionais = producao.TetosFuncionais.ToString();
            Observacoes = producao.Obs;
            RegimeAlimentar = producao.RegimeAlimentar;
            DataSaidaControle = producao.DataSaidaControle.HasValue ? producao.DataSaidaControle.Value.ToString("dd/MM/yyyy") : "";
            IdCria = producao.IdCria;
            IdMatriz = producao.IdMatriz;
            Receptora = producao.Receptora;
            GordPOrdenha = producao.GordPOrdenha.ToString();
            GordSOrdenha = producao.GordSOrdenha.ToString();
            GordTOrdenha = producao.GordTOrdenha.ToString();
            ProtPOrdenha = producao.ProtPOrdenha.ToString();
            ProtSOrdenha = producao.ProtSOrdenha.ToString();
            ProtTOrdenha = producao.ProtTOrdenha.ToString();
            Motivo = producao.Motivo;

        }

        public void FillPLValues2(ProducaoLeite producao)
        {
            //this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes();
            //ddlMatrizes.DataValueField = "Id";
            //ddlMatrizes.DataTextField = "NomeCompleto";
            //ddlMatrizes.SelectedValue = producao.IdMatriz;
            //ddlMatrizes.DataBind();

            txtPOrdenha.Text = producao.POrdenha.ToString();
            txtSOrdenha.Text = producao.SOrdenha.ToString();
            txtTOrdenha.Text = producao.TOrdenha.ToString();
            ckBezerrosPe.Checked = producao.BezerrosPe;
            ckSairControle.Checked = producao.SairControle;
            ddlTetosFuncionais.SelectedValue = producao.TetosFuncionais.ToString();
            txtObservacoes.Text = producao.Obs;
            ddlRegimeAlimentar.SelectedValue = producao.RegimeAlimentar;
            txtDataSaidaControle.Text = producao.DataSaidaControle.HasValue ? producao.DataSaidaControle.Value.ToString("dd/MM/yyyy") : "";
            //IdCria = producao.IdCria;
            ckReceptora.Checked = producao.Receptora;
            txtGordPOrdenha.Text = producao.GordPOrdenha.ToString();
            txtGordSOrdenha.Text = producao.GordSOrdenha.ToString();
            txtGordTOrdenha.Text = producao.GordTOrdenha.ToString();
            txtProtPOrdenha.Text = producao.ProtPOrdenha.ToString();
            txtProtSOrdenha.Text = producao.ProtSOrdenha.ToString();
            txtProtTOrdenha.Text = producao.ProtTOrdenha.ToString();
            ddlMotivo.SelectedValue = producao.Motivo;


        }

        public void LoadDropDowns(string raca)
        {
            var lote = _fCarnaubaFacade.GetLoteById(LoteId.ToString());

            if (LoteId > 0)
            {
                this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes(raca);
            }
            else
            {
                this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes();
            }

            //this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes(raca);
            ddlMatrizes.DataValueField = "Id";
            ddlMatrizes.DataTextField = "NomeCompleto";
            //ddlMatrizes.SelectedValue = producao.IdMatriz;
            ddlMatrizes.DataBind();


        }

        protected void btnDeleteControleLeiteiro_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int producaoLeiteId = Convert.ToInt32(btn.CommandArgument);

            _fCarnaubaFacade.RemoveProducaoLeite(LoteId, producaoLeiteId);
            

            UpdateGridView();
        }

        protected void gridViewProducaoLeite_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var strUrl = String.Format("DetalhesProducaoLeite.aspx?loteId={0}&pl={1}", LoteId, e.Row.RowIndex);
                WebUtil.AddRowHighlight(e.Row, strUrl);

            }
        }

        protected void btnPesquisarDDlMatriz_Click(object send, EventArgs e)
        {
            var lote = _fCarnaubaFacade.GetLoteById(LoteId.ToString());
            string pesquisa = txtPesquisaDdlMatriz.Text;
            if (String.IsNullOrEmpty(txtPesquisaDdlMatriz.Text))
                pesquisa = "*";

            if (LoteId > 0)
            {

                this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes(lote.Raca, pesquisa);
            }
            else
            {
                this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes();
            }

            //this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes(raca);
            ddlMatrizes.DataValueField = "Id";
            ddlMatrizes.DataTextField = "NomeCompleto";
            //ddlMatrizes.SelectedValue = producao.IdMatriz;
            ddlMatrizes.DataBind();
        }
    }

}