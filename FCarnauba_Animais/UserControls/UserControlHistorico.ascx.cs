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
    public partial class UserControlHistorico : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int animalId;
        private int totalHistoricos = 0;
        public Historico historico = null;
        private string animalID;
        string lastHistorico = null;

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

        public int AnimalId
        {
            get
            {
                return Convert.ToInt32(ViewState["AnimalId"]);
            }
            set { ViewState["AnimalId"] = value; }
        }

        public int HistoricoInd
        {
            get
            {
                if (ViewState["HistoricoInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["HistoricoInd"]);
            }
            set { ViewState["HistoricoInd"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (HistoricoInd > -1)
                {
                    UpdateHistoricoData();
                }
            }

        }

        public string Movimento
        {
            get { return ddlMovimentos.SelectedValue; }
            set { ddlMovimentos.SelectedValue = value; }
        }

        public string DataManejo
        {
            get { return txtDataHistorico.Text; }
            set { txtDataHistorico.Text = value; }
        }

        public string Observacoes
        {
            get { return txtObservacoes.Text; }
            set { txtObservacoes.Text = value; }
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
            page.ShowMessage(msg, isGood);
        }

        protected void btnCadastrar_Click(object send, EventArgs e)
        {
            FCarnaubaDataAccess fCarnaubaDataAccess = new FCarnaubaDataAccess();
            fCarnaubaDataAccess.OpenConnection();

            Animal animal = fCarnaubaDataAccess.GetAnimalByIdCompleto(AnimalId.ToString());
            requestedValidation = true;

            Page.Validate();

            if (!Page.IsValid)
            {

                ExibeMensagem(TipoDeMensagem.Aviso, "Complete os campos requeridos corretamente.");
                return;
            }

            fCarnaubaDataAccess.CloseConnection();
            fCarnaubaDataAccess = new FCarnaubaDataAccess();
            fCarnaubaDataAccess.OpenConnection();

            animal.Id = AnimalId;
            fCarnaubaDataAccess.CloseConnection();

            var historico = new Historico();

            historico.Movimento = ddlMovimentos.SelectedValue;

            if (!String.IsNullOrEmpty(txtDataHistorico.Text))
            {
                historico.DataManejo = Convert.ToDateTime(txtDataHistorico.Text);
            }

            historico.Observacao = txtObservacoes.Text;

            if (EditMode)
            {

                _fCarnaubaFacade.SalvaHistorico(AnimalId.ToString(), historico, HistoricoInd);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Histórico salvo com sucesso");

            }
            else
            {


                _fCarnaubaFacade.AdicionaHistorico(AnimalId.ToString(), historico);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Histórico adicionado com sucesso");

            }

            UpdateHistoricoData();
            UpdateGridView();
        }

        public List<Historico> DataSource
        {
            set
            {
                totalHistoricos = value.Count;
                this.gridViewHistorico.DataSource = value;
                this.gridViewHistorico.DataBind();
            }
        }

        protected void cvDataHistorico_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataHistorico.Text != "")
            {
                if (IsValidDate(txtDataHistorico.Text))
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

        private void UpdateHistoricoData()
        {
            var animalId = Convert.ToInt32(Request.Params["animalId"]);
            if (animalId == 0)
                animalId = Convert.ToInt32(Request.Params["id"]);

            var targInd = HistoricoInd == -1 ? 0 : HistoricoInd;
            historico = _fCarnaubaFacade.GetHistoricoByIndex(animalId, targInd);

        }

        private void UpdateGridView()
        {

            var historicos = _fCarnaubaFacade.GetHistorico(AnimalId);
            totalHistoricos = historicos.Count;
            gridViewHistorico.DataSource = historicos;
            gridViewHistorico.DataBind();
        }

        public void UpdateGridView(long animId)
        {
            AnimalId = (int)animalId;
            var historico = _fCarnaubaFacade.GetHistorico(Convert.ToInt32(animalId));
            totalHistoricos = historico.Count;
            gridViewHistorico.DataSource = historico;
            gridViewHistorico.DataBind();
        }

        public void FillHValues(Historico historico)
        {
            ddlMovimentos.SelectedValue = historico.Movimento;
            Observacoes = historico.Observacao.ToString();
            DataManejo = historico.DataManejo.ToString();
        }

        protected void btnDeleteHistorico_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int historicoId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemoveHistorico(AnimalId, historicoId);


            UpdateGridView();
        }
    }
}