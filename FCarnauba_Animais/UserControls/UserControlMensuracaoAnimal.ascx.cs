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
    public partial class UserControlMensuracaoAnimal : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int animalId;
        private int totalMensuracoes = 0;
        public Mensuracao mensuracao = null;
        private string animalID;
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

        public int AnimalId
        {
            get
            {
                return Convert.ToInt32(ViewState["AnimalId"]);
            }
            set { ViewState["AnimalId"] = value; }
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
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (MensuracaoInd > -1)
                {
                    UpdateMensuracaoData();
                }
            }

        }

        public string DataPesagem
        {
            get { return txtDataPesagem.Text; }
            set { txtDataPesagem.Text = value; }
        }

        public string Peso
        {
            get { return txtPeso.Text; }
            set { txtPeso.Text = value; }
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

            var mensuracao = new Mensuracao();

            if (!String.IsNullOrEmpty(txtDataPesagem.Text))
            {
                mensuracao.DataPesagem = Convert.ToDateTime(txtDataPesagem.Text);
            }

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
            mensuracao.RegimeAlimentar = ddlRegimeAlimentar.SelectedValue;

            

            if (EditMode)
            {

                _fCarnaubaFacade.SalvaMensuracaoAnimal(AnimalId, mensuracao, MensuracaoInd);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Mensuração salva com sucesso");

            }
            else
            {


                _fCarnaubaFacade.AdicionaMensuracaoAnimal(AnimalId.ToString(), mensuracao);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Mensuracao adicionada com sucesso");

            }

            UpdateMensuracaoData();
            UpdateGridView();
        }

        public List<Mensuracao> DataSource
        {
            set
            {
                totalMensuracoes = value.Count;
                this.gridViewMensuracaoAnimal.DataSource = value;
                this.gridViewMensuracaoAnimal.DataBind();
            }
        }

        protected void cvDataPesagem_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataPesagem.Text != "")
            {
                if (IsValidDate(txtDataPesagem.Text))
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
                args.IsValid = false;
            }

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

        private void UpdateMensuracaoData()
        {
            var animalId = Convert.ToInt32(Request.Params["animalId"]);
            if (animalId == 0)
                animalId = Convert.ToInt32(Request.Params["id"]);

            var targInd = MensuracaoInd == -1 ? 0 : MensuracaoInd;
            mensuracao = _fCarnaubaFacade.GetMensuracaoAnimalByIndex(animalId, targInd);

        }

        private void UpdateGridView()
        {

            var mensuracoes = _fCarnaubaFacade.GetMensuracaoAnimal(AnimalId);
            totalMensuracoes = mensuracoes.Count;
            gridViewMensuracaoAnimal.DataSource = mensuracoes;
            gridViewMensuracaoAnimal.DataBind();
        }

        public void UpdateGridView(long animId)
        {
            AnimalId = (int)animalId;
            var mensuracao = _fCarnaubaFacade.GetMensuracaoAnimal(Convert.ToInt32(animalId));
            totalMensuracoes = mensuracao.Count;
            gridViewMensuracaoAnimal.DataSource = mensuracao;
            gridViewMensuracaoAnimal.DataBind();
        }

        public void FillMValues(Mensuracao mensuracao)
        {
            DataPesagem = mensuracao.DataPesagem.ToString();
            Peso = mensuracao.Peso.ToString();
            CEscrotal = mensuracao.CEscrotal.ToString();
            AAnterior = mensuracao.AAnterior.ToString();
            APosterior = mensuracao.APosterior.ToString();
            LGarupa = mensuracao.LGarupa.ToString();
            CGarupa = mensuracao.CGarupa.ToString();
            CCorporal = mensuracao.CCorporal.ToString();
            PToracico = mensuracao.PToracico.ToString();
            CaracterizacaoRacial = mensuracao.CaracterizacaoRacial;
            ClassificacaoUbere = mensuracao.ClassificacaoUbere;
            RegimeAlimentar = mensuracao.RegimeAlimentar;
        }

        protected void btnDeleteMensuracao_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int mensuracaoId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemoveMensuracaoAnimal(AnimalId, mensuracaoId);


            UpdateGridView();
        }

        protected void gridViewMensuracaoAnimal_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var strUrl = String.Format("DetalhesMensuracaoAnimal.aspx?animalId={0}&m={1}", AnimalId, e.Row.RowIndex);
                WebUtil.AddRowHighlight(e.Row, strUrl);

            }
        }
   
    }
}