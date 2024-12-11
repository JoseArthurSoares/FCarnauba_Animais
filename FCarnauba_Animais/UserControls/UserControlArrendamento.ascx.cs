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
    public partial class UserControlArrendamento : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int estruturaPropriedadeId;
        private int totalArrendamentos = 0;
        public Arrendamento arrendamento = null;
        private string estruturaPropriedadeID;
        string lastArrendamento = null;

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

        public int EstruturaPropriedadeId
        {
            get
            {
                return Convert.ToInt32(ViewState["EstruturaPropriedadeId"]);
            }
            set { ViewState["EstruturaPropriedadeId"] = value; }
        }

        public int ArrendamentoInd
        {
            get
            {
                if (ViewState["ArrendamentoInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["ArrendamentoInd"]);
            }
            set { ViewState["ArrendamentoInd"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (ArrendamentoInd > -1)
                {
                    UpdateArrendamentoData();
                }
            }
        }

        public string Tipo
        {
            get { return txtTipo.Text; }
            set { txtTipo.Text = value; }
        }

        public string Area
        {
            get { return txtAreaArrendamento.Text; }
            set { txtAreaArrendamento.Text = value; }
        }

        public string Data
        {
            get { return txtDataArrendamento.Text; }
            set { txtDataArrendamento.Text = value; }
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
            page.ShowMessageEstruturaPropriedade(msg, isGood);
        }

        protected void btnCadastrar_Click(object send, EventArgs e)
        {
            FCarnaubaDataAccess fCarnaubaDataAccess = new FCarnaubaDataAccess();
            fCarnaubaDataAccess.OpenConnection();

            EstruturaPropriedade estruturaPropriedade = fCarnaubaDataAccess.GetEstruturaPropriedadeById(EstruturaPropriedadeId.ToString());
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

            estruturaPropriedade.Id = EstruturaPropriedadeId;
            fCarnaubaDataAccess.CloseConnection();

            var arrendamento = new Arrendamento();

            arrendamento.Tipo = txtTipo.Text;
            if (!String.IsNullOrEmpty(txtAreaArrendamento.Text))
            {
                arrendamento.Area = Convert.ToDouble(txtAreaArrendamento.Text);
            }
            else
            {
                arrendamento.Area = 0;
            }

            if (!String.IsNullOrEmpty(txtDataArrendamento.Text))
            {
                arrendamento.Data = Convert.ToDateTime(txtDataArrendamento.Text);
            }

            if (EditMode)
            {

                _fCarnaubaFacade.SalvaArrendamento(EstruturaPropriedadeId, arrendamento, ArrendamentoInd);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Arrendamento salvo com sucesso");
                //ShowMessage("Dados salvos com sucesso.", true);
                //return;
            }
            else
            {
                if (_fCarnaubaFacade.ArrendamentoEstruturaPropriedadeExists(estruturaPropriedade.Id.ToString(), arrendamento.Tipo))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Arrendamento já cadastrado no registro.");
                    return;
                }

                _fCarnaubaFacade.AdicionaArrendamento(EstruturaPropriedadeId.ToString(), arrendamento);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Arrendamento adicionado com sucesso");
                //ShowMessage("Dados inseridos com sucesso.", true);
                //return;
            }

            UpdateArrendamentoData();
            UpdateGridView();
        }

        public List<Arrendamento> DataSource
        {
            set
            {
                totalArrendamentos = value.Count;
                this.gridViewArrendamentos.DataSource = value;
                this.gridViewArrendamentos.DataBind();
            }
        }


        protected void cvAreaArrendamento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtAreaArrendamento.Text))
                txtAreaArrendamento.Text = "0";

            if (Convert.ToDouble(txtAreaArrendamento.Text) > 0)
            {
                args.IsValid = true;

            }
            else
            {
                args.IsValid = false;
            }

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvDataArrendamento_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataArrendamento.Text != "")
            {
                if (IsValidDate(txtDataArrendamento.Text))
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

        protected void cvTipo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtTipo.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

            if (!requestedValidation) args.IsValid = true;
        }

        private void UpdateArrendamentoData()
        {
            var estruturaPropriedadeId = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (estruturaPropriedadeId == 0)
                estruturaPropriedadeId = Convert.ToInt32(Request.Params["id"]);

            var targInd = ArrendamentoInd == -1 ? 0 : ArrendamentoInd;
            arrendamento = _fCarnaubaFacade.GetArrendamentoByIndex(estruturaPropriedadeId, targInd);

        }

        private void UpdateGridView()
        {

            var arrendamentos = _fCarnaubaFacade.GetArrendamentos(EstruturaPropriedadeId);
            totalArrendamentos = arrendamentos.Count;
            gridViewArrendamentos.DataSource = arrendamentos;
            gridViewArrendamentos.DataBind();
        }

        public void UpdateGridView(long estruturaId)
        {
            EstruturaPropriedadeId = (int)estruturaPropriedadeId;
            var arrendamentos = _fCarnaubaFacade.GetArrendamentos(Convert.ToInt32(estruturaPropriedadeId));
            totalArrendamentos = arrendamentos.Count;
            gridViewArrendamentos.DataSource = arrendamentos;
            gridViewArrendamentos.DataBind();
        }

        public void FillAValues(Arrendamento arrendamento)
        {
            Tipo = arrendamento.Tipo;
            Area = arrendamento.Area.ToString();
            Data = arrendamento.Data.ToString();
        }


        protected void btnDeleteEstruturaPropriedadeArrendamento_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int arrendamentoId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemoveArrendamento(EstruturaPropriedadeId, arrendamentoId);


            UpdateGridView();
        }
    }
}