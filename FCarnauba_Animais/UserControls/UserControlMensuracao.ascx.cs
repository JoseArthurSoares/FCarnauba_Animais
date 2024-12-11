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
    public partial class UserControlMensuracao : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int lotePonderalId;
        private int totalMensuracoes = 0;
        public Mensuracao mensuracao = null;
        private string lotePonderalID;
        string lastMensuracao = null;

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


        public int LotePonderalId
        {
            get
            {
                return Convert.ToInt32(ViewState["LotePonderalId"]);
            }
            set { ViewState["LotePonderalId"] = value; }
        }


        public int MensuracaoInd
        {
            get
            {
                if (ViewState["MensuracaoInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["MensuracaoInd"]);
            }
            set { ViewState["MensuracaoInd"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {

                if (ddlCondicoesCriacao.SelectedValue == "DESMAMADO")
                {
                    pnlDesmame.Visible = true;
                }

                if (ddlCondicoesCriacao.SelectedValue == "DIAGNÓSTICO DE PRENHEZ")
                {
                    pnlDiagnosticoPrenhez.Visible = true;
                }

                if (ddlCondicoesCriacao.SelectedValue == "PARTO")
                {
                    pnlParto.Visible = true;
                }

                if (ddlCondicoesCriacao.SelectedValue == "ENTRADA EM CONTROLE LEITEIRO")
                {
                    pnlEntradaControleLeiteiro.Visible = true;
                }

                if (ddlCondicoesCriacao.SelectedValue == "ENCERRAMENTO DA LACTAÇÃO")
                {
                    pnlEncerramentoLactacao.Visible = true;
                }


                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (MensuracaoInd > -1)
                {
                    UpdateMensuracaoData();
                }

                

                if (EditMode)
                {
                    
                }

                
            }
        }

        public string IdAnimal
        {
            get { return ddlAnimais.SelectedValue; }
            set { ddlAnimais.SelectedValue = value; }
        }

        public string Peso
        {
            get { return txtPeso.Text; }
            set { txtPeso.Text = value; }
        }

        public string PesoMaeDesmame
        {
            get { return txtPesoMaeDesmame.Text; }
            set { txtPesoMaeDesmame.Text = value; }
        }

        public string CEscrotal
        {
            get { return txtCEscrotal.Text; }
            set { txtCEscrotal.Text = value; }
        }

        public string AAnterior
        {
            get { return txtAAnterior.Text; }
            set { txtAAnterior.Text = value; }
        }

        public string APosterior
        {
            get { return txtAPosterior.Text; }
            set { txtAPosterior.Text = value; }
        }

        public string LGarupa
        {
            get { return txtLGarupa.Text; }
            set { txtLGarupa.Text = value; }
        }

        public string CGarupa
        {
            get { return txtCGarupa.Text; }
            set { txtCGarupa.Text = value; }
        }

        public string CCorporal
        {
            get { return txtCCorporal.Text; }
            set { txtCCorporal.Text = value; }
        }

        public string PToracico
        {
            get { return txtPToracico.Text; }
            set { txtPToracico.Text = value; }
        }

        public string CaracterizacaoRacial
        {
            get { return ddlCaracterizacoesRaciais.SelectedValue; }
            set { ddlCaracterizacoesRaciais.SelectedValue = value; }
        }

        public string ClassificacaoUbere
        {
            get { return ddlClassificacoesUbere.SelectedValue; }
            set { ddlClassificacoesUbere.SelectedValue = value; }
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

        public string CondicaoCriacao
        {
            get { return ddlCondicoesCriacao.SelectedValue; }
            set { ddlCondicoesCriacao.SelectedValue = value; }
        }

        public string DataDesmame
        {
            get { return txtDataDesmame.Text; }
            set { txtDataDesmame.Text = value; }
        }

        public string DataDiagnosticoPrenhez
        {
            get { return txtDataDiagnosticoPrenhez.Text; }
            set { txtDataDiagnosticoPrenhez.Text = value; }
        }

        public string DataParto
        {
            get { return txtDataParto.Text; }
            set { txtDataParto.Text = value; }
        }

        public string DataEntradaControleLeiteiro
        {
            get { return txtDataEntradaControleLeiteiro.Text; }
            set { txtDataEntradaControleLeiteiro.Text = value; }
        }

        public string DataEncerramentoLactacao
        {
            get { return txtDataEncerramentoLactacao.Text; }
            set { txtDataEncerramentoLactacao.Text = value; }
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
            page.ShowMessageControlePonderal(msg, isGood);
        }

        protected void btnCadastrar_Click(object send, EventArgs e)
        {
            FCarnaubaDataAccess fCarnaubaDataAccess = new FCarnaubaDataAccess();
            fCarnaubaDataAccess.OpenConnection();
            //loteID = fCarnaubaDataAccess.GetLoteIdControleLeiteiro(ControleLeiteiroId);
            LotePonderal lotePonderal = fCarnaubaDataAccess.GetLotePonderalById(LotePonderalId.ToString());
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
            lotePonderal.Id = LotePonderalId;
            fCarnaubaDataAccess.CloseConnection();

            var mensuracao = new Mensuracao();

            mensuracao.IdAnimal = ddlAnimais.SelectedValue;
           
            //int diasLactacao = 0;
            //DateTime? dataEntradaControle = _fCarnaubaFacade.GetDataEntradaControle(LoteId.ToString(), producaoLeite.IdMatriz, producaoLeite.IdCria);

            //if (dataEntradaControle != null)
            //{

            //    TimeSpan diferenca = (TimeSpan)(lote.DataControle - dataEntradaControle);
            //    if (diferenca.TotalDays > 0)
            //    {
            //        diasLactacao = (int)diferenca.TotalDays;
            //    }

            //}
            //else
            //{
            //    if (producaoLeite.IdCria != "0")
            //    {
            //        DateTime dataEntradaControleCria = _fCarnaubaFacade.GetDataNascimentoCria(producaoLeite.IdCria);
            //        dataEntradaControleCria = dataEntradaControleCria.AddDays(7);

            //        TimeSpan diferenca = (TimeSpan)(lote.DataControle - dataEntradaControleCria);
            //        if (diferenca.TotalDays > 0)
            //        {
            //            diasLactacao = (int)diferenca.TotalDays;
            //        }
            //    }
            //}

            //producaoLeite.DiasLactacao = diasLactacao;

            if (!String.IsNullOrEmpty(txtPeso.Text))
            {
                mensuracao.Peso = Convert.ToDouble(txtPeso.Text);
            }
            else
            {
                mensuracao.Peso = 0;
            }
            if (!String.IsNullOrEmpty(txtCEscrotal.Text))
            {
                mensuracao.CEscrotal = Convert.ToDouble(txtCEscrotal.Text);
            }
            else
            {
                mensuracao.CEscrotal = 0;
            }

            if (!String.IsNullOrEmpty(txtAAnterior.Text))
            {
                mensuracao.AAnterior = Convert.ToDouble(txtAAnterior.Text);
            }
            else
            {
                mensuracao.AAnterior = 0;
            }

            mensuracao.RegimeAlimentar = ddlRegimeAlimentar.SelectedValue;

            if (IsValidDate(txtDataSaidaControle.Text))
                mensuracao.DataSaidaControle = Convert.ToDateTime(txtDataSaidaControle.Text);


            if (!String.IsNullOrEmpty(txtAPosterior.Text))
            {
                mensuracao.APosterior = Convert.ToDouble(txtAPosterior.Text);
            }
            else
            {
                mensuracao.APosterior = 0;
            }

            if (!String.IsNullOrEmpty(txtLGarupa.Text))
            {
                mensuracao.LGarupa = Convert.ToDouble(txtLGarupa.Text);
            }
            else
            {
                mensuracao.LGarupa = 0;
            }

            if (!String.IsNullOrEmpty(txtCGarupa.Text))
            {
                mensuracao.CGarupa = Convert.ToDouble(txtCGarupa.Text);
            }
            else
            {
                mensuracao.CGarupa = 0;
            }

            if (!String.IsNullOrEmpty(txtCCorporal.Text))
            {
                mensuracao.CCorporal = Convert.ToDouble(txtCCorporal.Text);
            }
            else
            {
                mensuracao.CCorporal = 0;
            }

            if (!String.IsNullOrEmpty(txtPToracico.Text))
            {
                mensuracao.PToracico = Convert.ToDouble(txtPToracico.Text);
            }
            else
            {
                mensuracao.PToracico = 0;
            }

            mensuracao.CaracterizacaoRacial = ddlCaracterizacoesRaciais.SelectedValue;
            mensuracao.ClassificacaoUbere = ddlClassificacoesUbere.SelectedValue;

            mensuracao.SairControle = ckSairControle.Checked;
            mensuracao.Motivo = ddlMotivo.SelectedValue;

            mensuracao.CondicaoCriacao = ddlCondicoesCriacao.SelectedValue;

            if (IsValidDate(txtDataDesmame.Text))
                mensuracao.DataDesmame = Convert.ToDateTime(txtDataDesmame.Text);

            if (IsValidDate(txtDataDiagnosticoPrenhez.Text))
                mensuracao.DataDiagnosticoPrenhez = Convert.ToDateTime(txtDataDiagnosticoPrenhez.Text);

            if (IsValidDate(txtDataParto.Text))
                mensuracao.DataParto = Convert.ToDateTime(txtDataParto.Text);

            if (IsValidDate(txtDataEntradaControleLeiteiro.Text))
                mensuracao.DataEntradaControleLeiteiro = Convert.ToDateTime(txtDataEntradaControleLeiteiro.Text);

            if (IsValidDate(txtDataEncerramentoLactacao.Text))
                mensuracao.DataEncerramentoLactacao = Convert.ToDateTime(txtDataEncerramentoLactacao.Text);

            if (!String.IsNullOrEmpty(txtPesoMaeDesmame.Text))
            {
                mensuracao.PesoMaeDesmame = Convert.ToDouble(txtPesoMaeDesmame.Text);
            }
            else
            {
                mensuracao.PesoMaeDesmame = 0;
            }


            if (EditMode)
            {

                _fCarnaubaFacade.SalvaMensuracao(LotePonderalId, mensuracao, MensuracaoInd);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Mensuração salva com sucesso");
                //ShowMessage("Dados salvos com sucesso.", true);
                //return;
            }
            else
            {
                if (_fCarnaubaFacade.AnimalLotePonderalExists(lotePonderal.Id.ToString(), mensuracao.IdAnimal))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Animal já cadastrada no lote.");
                    return;
                }

                _fCarnaubaFacade.AdicionaMensuracao(LotePonderalId.ToString(), mensuracao);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Mensuração adicionada com sucesso");
                //ShowMessage("Dados inseridos com sucesso.", true);
                //return;
            }

            UpdateMensuracaoData();
            UpdateGridView();
        }


        public List<Mensuracao> DataSource
        {
            set
            {
                totalMensuracoes = value.Count;
                this.gridViewMensuracao.DataSource = value;
                this.gridViewMensuracao.DataBind();
            }
        }

        protected void cvPeso_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtPeso.Text))
                txtPeso.Text = "0";

            if (Convert.ToDouble(txtPeso.Text) > 0)
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

        protected void cvPesoMaeDesmame_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtPesoMaeDesmame.Text))
                txtPesoMaeDesmame.Text = "0";


            args.IsValid = true;

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvCEscrotal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtCEscrotal.Text))
                txtCEscrotal.Text = "0";


            args.IsValid = true;

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvAAnterior_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtAAnterior.Text))
                txtAAnterior.Text = "0";


            args.IsValid = true;

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvAPosterior_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtAPosterior.Text))
                txtAPosterior.Text = "0";


            args.IsValid = true;

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvLGarupa_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtLGarupa.Text))
                txtLGarupa.Text = "0";


            args.IsValid = true;

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvCGarupa_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtCGarupa.Text))
                txtCGarupa.Text = "0";


            args.IsValid = true;

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvCCorporal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtCCorporal.Text))
                txtCCorporal.Text = "0";


            args.IsValid = true;

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvPToracico_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtPToracico.Text))
                txtPToracico.Text = "0";


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


        protected void cvDataDataDesmame_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataDesmame.Text != "" && txtDataDesmame.Text != "__/__/____")
            {
                if (IsValidDate(txtDataDesmame.Text))
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

        protected void cvDataDiagnosticoPrenhez_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataDiagnosticoPrenhez.Text != "" && txtDataDiagnosticoPrenhez.Text != "__/__/____")
            {
                if (IsValidDate(txtDataDiagnosticoPrenhez.Text))
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

        protected void cvDataParto_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataParto.Text != "" && txtDataParto.Text != "__/__/____")
            {
                if (IsValidDate(txtDataParto.Text))
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

        protected void cvDataEntradaControleLeiteiro_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataEntradaControleLeiteiro.Text != "" && txtDataEntradaControleLeiteiro.Text != "__/__/____")
            {
                if (IsValidDate(txtDataEntradaControleLeiteiro.Text))
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

        protected void cvDataEncerramentoLactacao_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataEncerramentoLactacao.Text != "" && txtDataEncerramentoLactacao.Text != "__/__/____")
            {
                if (IsValidDate(txtDataEncerramentoLactacao.Text))
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



        protected void ddlAnimais_SelectedIndexChanged(object sender, EventArgs e)
        {
            LotePonderal lotePonderal = _fCarnaubaFacade.GetLotePonderalById(LotePonderalId.ToString());
            var idAnimal = ddlAnimais.SelectedValue;

            //Redirect depos aqui
            //Response.Redirect("EditaControleLeiteiro.aspx?controleLeiteiroId=" + ControleLeiteiroId + "&&tabIndex=#producaoleite-tab");
        }

        private void UpdateMensuracaoData()
        {
            var lotePonderalId = Convert.ToInt32(Request.Params["lotePonderalId"]);
            if (lotePonderalId == 0)
                lotePonderalId = Convert.ToInt32(Request.Params["id"]);
            //var obraId = Convert.ToInt32(Request.Params["obraId"]);
            var targInd = MensuracaoInd == -1 ? 0 : MensuracaoInd;
            mensuracao = _fCarnaubaFacade.GetMensuracaoByIndex(lotePonderalId, targInd);

        }

        private void UpdateGridView()
        {

            var mensuracoes = _fCarnaubaFacade.GetMensuracao(LotePonderalId);
            totalMensuracoes = mensuracoes.Count;
            gridViewMensuracao.DataSource = mensuracoes;
            gridViewMensuracao.DataBind();
        }

        public void UpdateGridView(long loteId)
        {
            LotePonderalId = (int)lotePonderalId;
            var mensuracoes = _fCarnaubaFacade.GetMensuracao(Convert.ToInt32(lotePonderalId));
            totalMensuracoes = mensuracoes.Count;
            gridViewMensuracao.DataSource = mensuracoes;
            gridViewMensuracao.DataBind();
        }

        public void FillMValues(Mensuracao mensuracao)
        {
            this.ddlAnimais.DataSource = _fCarnaubaFacade.GetAnimais();
            ddlAnimais.DataValueField = "Id";
            ddlAnimais.DataTextField = "NomeCompleto";
            ddlAnimais.SelectedValue = mensuracao.IdAnimal;
            ddlAnimais.DataBind();

            Peso = mensuracao.Peso.ToString();
            PesoMaeDesmame = mensuracao.PesoMaeDesmame.ToString();
            CEscrotal = mensuracao.CEscrotal.ToString();
            AAnterior = mensuracao.AAnterior.ToString();
            APosterior = mensuracao.APosterior.ToString();
            LGarupa = mensuracao.LGarupa.ToString();
            CGarupa = mensuracao.CGarupa.ToString();
            CCorporal = mensuracao.CCorporal.ToString();
            PToracico = mensuracao.PToracico.ToString();
            CaracterizacaoRacial = mensuracao.CaracterizacaoRacial;
            ClassificacaoUbere = mensuracao.ClassificacaoUbere;
            SairControle = mensuracao.SairControle;
            RegimeAlimentar = mensuracao.RegimeAlimentar;
            DataSaidaControle = mensuracao.DataSaidaControle.HasValue ? mensuracao.DataSaidaControle.Value.ToString("dd/MM/yyyy") : "";
            IdAnimal = mensuracao.IdAnimal;
            Motivo = mensuracao.Motivo;

            CondicaoCriacao = mensuracao.CondicaoCriacao;
            DataDesmame = mensuracao.DataDesmame.HasValue ? mensuracao.DataDesmame.Value.ToString("dd/MM/yyyy") : "";
            DataDiagnosticoPrenhez = mensuracao.DataDiagnosticoPrenhez.HasValue ? mensuracao.DataDiagnosticoPrenhez.Value.ToString("dd/MM/yyyy") : "";
            DataParto = mensuracao.DataParto.HasValue ? mensuracao.DataParto.Value.ToString("dd/MM/yyyy") : "";
            DataEntradaControleLeiteiro = mensuracao.DataEntradaControleLeiteiro.HasValue ? mensuracao.DataEntradaControleLeiteiro.Value.ToString("dd/MM/yyyy") : "";
            DataEncerramentoLactacao = mensuracao.DataEncerramentoLactacao.HasValue ? mensuracao.DataEncerramentoLactacao.Value.ToString("dd/MM/yyyy") : "";


        }

        public void LoadDropDowns(string raca)
        {
            var lotePonderal = _fCarnaubaFacade.GetLotePonderalById(LotePonderalId.ToString());

            if (LotePonderalId > 0)
            {
                this.ddlAnimais.DataSource = _fCarnaubaFacade.GetAnimais(raca);
            }
            else
            {
                this.ddlAnimais.DataSource = _fCarnaubaFacade.GetAnimais();
            }

            //this.ddlAnimais.DataSource = _fCarnaubaFacade.GetAnimais(raca);
            ddlAnimais.DataValueField = "Id";
            ddlAnimais.DataTextField = "NomeCompleto";
            //ddlMatrizes.SelectedValue = producao.IdMatriz;
            ddlAnimais.DataBind();


        }

        protected void btnDeleteControlePonderal_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int mensuracaoId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemoveMensuracao(LotePonderalId, mensuracaoId);


            UpdateGridView();
        }

        protected void gridViewMensuracao_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var strUrl = String.Format("DetalhesMensuracao.aspx?lotePonderalId={0}&m={1}", LotePonderalId, e.Row.RowIndex);
                WebUtil.AddRowHighlight(e.Row, strUrl);

            }
        }

        protected void ddlCondicoesCriacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCondicoesCriacao.SelectedValue == "DESMAMADO")
            {
                pnlDesmame.Visible = true;
            }
            else
            {
                pnlDesmame.Visible = false;
            }

            if (ddlCondicoesCriacao.SelectedValue == "DIAGNÓSTICO DE PRENHEZ")
            {
                pnlDiagnosticoPrenhez.Visible = true;
            }
            else
            {
                pnlDiagnosticoPrenhez.Visible = false;
            }

            if (ddlCondicoesCriacao.SelectedValue == "PARTO")
            {
                pnlParto.Visible = true;
            }
            else
            {
                pnlParto.Visible = false;
            }

            if (ddlCondicoesCriacao.SelectedValue == "ENTRADA EM CONTROLE LEITEIRO")
            {
                pnlEntradaControleLeiteiro.Visible = true;
            }
            else
            {
                pnlEntradaControleLeiteiro.Visible = false;
            }

            if (ddlCondicoesCriacao.SelectedValue == "ENCERRAMENTO DA LACTAÇÃO")
            {
                pnlEncerramentoLactacao.Visible = true;
            }
            else
            {
                pnlEncerramentoLactacao.Visible = false;
            }
        }

        protected void btnPesquisarDDlAnimal_Click(object send, EventArgs e)
        {
            var lotePonderal = _fCarnaubaFacade.GetLotePonderalById(LotePonderalId.ToString());

            string pesquisa = txtPesquisaDdlAnimal.Text;
            if (String.IsNullOrEmpty(txtPesquisaDdlAnimal.Text))
                pesquisa = "*";

            if (LotePonderalId > 0)
            {

                this.ddlAnimais.DataSource = _fCarnaubaFacade.GetAnimais(lotePonderal.Raca, pesquisa);
            }
            else
            {
                this.ddlAnimais.DataSource = _fCarnaubaFacade.GetAnimais();
            }

            ddlAnimais.DataValueField = "Id";
            ddlAnimais.DataTextField = "NomeCompleto";
            ddlAnimais.DataBind();
        }
    }
}