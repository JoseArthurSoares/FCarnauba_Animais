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
    public partial class UserControlCdcMatrizes : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int cdcId;
        public Matriz cdcMatriz = null;
        private string cdcID;
        string lastMatriz = null;

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

        public int CdcId
        {
            get
            {
                return Convert.ToInt32(ViewState["CdcId"]);
            }
            set { ViewState["CdcId"] = value; }
        }

        public int CdcMatrizInd
        {
            get
            {
                if (ViewState["CdcMatrizInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["CdcMatrizInd"]);
            }
            set { ViewState["CdcMatrizInd"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (CdcMatrizInd > -1)
                {
                    UpdateCdcMatrizData();
                }


            }
        }

        public string IdMatriz
        {
            get { return ddlMatrizes.SelectedValue; }
            set { ddlMatrizes.SelectedValue = value; }
        }

        public bool CoberturaEfetiva
        {
            get { return ckCoberturaEfetiva.Checked; }
            set { ckCoberturaEfetiva.Checked = value; }
        }

        //public string CioRepeticao
        //{
        //    get { return ddlCioRepeticao.SelectedValue; }
        //    set { ddlCioRepeticao.SelectedValue = value; }
        //}

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
            page.ShowMessageCdc(msg, isGood);
        }

        protected void btnCadastrar_Click(object send, EventArgs e)
        {
            FCarnaubaDataAccess fCarnaubaDataAccess = new FCarnaubaDataAccess();
            fCarnaubaDataAccess.OpenConnection();

            Cdc cdc = fCarnaubaDataAccess.GetCdcById(CdcId.ToString());
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
            cdc.Id = CdcId;
            fCarnaubaDataAccess.CloseConnection();

            var cdcMatriz = new Matriz();

            cdcMatriz.IdMatriz = ddlMatrizes.SelectedValue;
            cdcMatriz.CdcEfetiva = ckCoberturaEfetiva.Checked;
            //cdcMatriz.CioRepeticao = ddlCioRepeticao.SelectedValue;

            if (EditMode)
            {
                var cdcMatrizCorr = _fCarnaubaFacade.GetCdcMatrizByIndex(CdcId, CdcMatrizInd);

                if (_fCarnaubaFacade.MatrizCdcExists(cdc.Id.ToString(), cdcMatriz.IdMatriz) && cdcMatrizCorr.IdMatriz != cdcMatriz.IdMatriz)
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Matriz editada já cadastrada no lote.");
                    return;
                }

                _fCarnaubaFacade.SalvaMatriz(CdcId, CdcMatrizInd, cdcMatriz);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Matriz salva com sucesso");
                //ShowMessage("Dados salvos com sucesso.", true);
                //return;
            }
            else
            {
                if (_fCarnaubaFacade.MatrizCdcExists(cdc.Id.ToString(), cdcMatriz.IdMatriz))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Matriz já cadastrada no lote.");
                    return;
                }

                _fCarnaubaFacade.AdicionaMatriz(CdcId, cdcMatriz);

                if (!_fCarnaubaFacade.TemCriasAnoPecuarioAnterior(cdcMatriz.IdMatriz, cdc.DataCobertura))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Matriz adicionada, mas não tem cria no ano pecuário anterior. Verifique cadastro dos animais");
                }
                else
                {
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Matriz adicionada com sucesso");
                }
                //ShowMessage("Dados inseridos com sucesso.", true);
                //return;
            }

            UpdateCdcMatrizData();
            UpdateGridView();
        }

        public List<Matriz> DataSource
        {
            set
            {
                //totalProducoes = value.Count;
                this.gridViewCdcMatrizes.DataSource = value;
                this.gridViewCdcMatrizes.DataBind();
            }
        }

        private void UpdateCdcMatrizData()
        {
            var cdcId = Convert.ToInt32(Request.Params["cdcId"]);
            if (cdcId == 0)
                cdcId = Convert.ToInt32(Request.Params["id"]);
            //var obraId = Convert.ToInt32(Request.Params["obraId"]);
            var targInd = CdcMatrizInd == -1 ? 0 : CdcMatrizInd;
            cdcMatriz = _fCarnaubaFacade.GetCdcMatrizByIndex(cdcId, targInd);

        }

        private void UpdateGridView()
        {

            var cdcMatrizes = _fCarnaubaFacade.GetMatrizes(CdcId);
            //totalProducoes = producoes.Count;
            gridViewCdcMatrizes.DataSource = cdcMatrizes;
            gridViewCdcMatrizes.DataBind();
        }

        public void UpdateGridView(long cdcId)
        {

            var cdcMatrizes = _fCarnaubaFacade.GetMatrizes(Convert.ToInt32(cdcId));
            //totalProducoes = producoes.Count;
            gridViewCdcMatrizes.DataSource = cdcMatrizes;
            gridViewCdcMatrizes.DataBind();
        }


        public void FillMValues(Matriz matriz)
        {
            this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes();
            ddlMatrizes.DataValueField = "Id";
            ddlMatrizes.DataTextField = "NomeCompleto";
            ddlMatrizes.SelectedValue = matriz.IdMatriz;
            ddlMatrizes.DataBind();
            
            IdMatriz = matriz.IdMatriz;
            CoberturaEfetiva = matriz.CdcEfetiva;
            //CioRepeticao = matriz.CioRepeticao;
            

        }

        public void LoadDropDowns()
        {
            var cdc = _fCarnaubaFacade.GetCdcById(CdcId.ToString());

            if (CdcId > 0)
            {
                this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes(cdc.Raca);
            }
            else
            {
                this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes();
            }

            
            ddlMatrizes.DataValueField = "Id";
            ddlMatrizes.DataTextField = "NomeCompleto";
            //ddlMatrizes.SelectedValue = producao.IdMatriz;
            ddlMatrizes.DataBind();

        }

        protected void btnDeleteCdcMatriz_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int cdcMatrizId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemoveMatriz(CdcId, cdcMatrizId);


            UpdateGridView();
        }

        protected void gridViewCdcMatrizes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var strUrl = String.Format("DetalhesCdcMatriz.aspx?cdcId={0}&m={1}", CdcId, e.Row.RowIndex);
                WebUtil.AddRowHighlight(e.Row, strUrl);

            }
        }

        protected void btnPesquisarDDlMatriz_Click(object send, EventArgs e)
        {
            var cdc = _fCarnaubaFacade.GetCdcById(CdcId.ToString());

            string pesquisa = txtPesquisaDdlMatriz.Text;
            if (String.IsNullOrEmpty(txtPesquisaDdlMatriz.Text))
                pesquisa = "*";

            if (CdcId > 0)
            {

                this.ddlMatrizes.DataSource = _fCarnaubaFacade.GetMaes(cdc.Raca, pesquisa);
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