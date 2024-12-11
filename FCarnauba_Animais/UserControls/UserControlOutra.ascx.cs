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
    public partial class UserControlOutra : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int estruturaPropriedadeId;
        private int totalOutras = 0;
        public Outra outra = null;
        private string estruturaPropriedadeID;
        string lastOutra = null;

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

        public int OutraInd
        {
            get
            {
                if (ViewState["OutraInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["OutraInd"]);
            }
            set { ViewState["OutraInd"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (OutraInd > -1)
                {
                    UpdateOutraData();
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
            get { return txtAreaOutra.Text; }
            set { txtAreaOutra.Text = value; }
        }

        public string Data
        {
            get { return txtDataOutra.Text; }
            set { txtDataOutra.Text = value; }
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

            var outra = new Outra();

            outra.Tipo = txtTipo.Text;
            if (!String.IsNullOrEmpty(txtAreaOutra.Text))
            {
                outra.Area = Convert.ToDouble(txtAreaOutra.Text);
            }
            else
            {
                outra.Area = 0;
            }

            if (!String.IsNullOrEmpty(txtDataOutra.Text))
            {
                outra.Data = Convert.ToDateTime(txtDataOutra.Text);
            }

            if (EditMode)
            {

                _fCarnaubaFacade.SalvaOutra(EstruturaPropriedadeId, outra, OutraInd);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Outra área salva com sucesso");
                //ShowMessage("Dados salvos com sucesso.", true);
                //return;
            }
            else
            {
                if (_fCarnaubaFacade.OutraEstruturaPropriedadeExists(estruturaPropriedade.Id.ToString(), outra.Tipo))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Outra área já cadastrada no registro.");
                    return;
                }

                _fCarnaubaFacade.AdicionaOutra(EstruturaPropriedadeId.ToString(), outra);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Outra área adicionada com sucesso");
                //ShowMessage("Dados inseridos com sucesso.", true);
                //return;
            }

            UpdateOutraData();
            UpdateGridView();
        }

        public List<Outra> DataSource
        {
            set
            {
                totalOutras = value.Count;
                this.gridViewOutras.DataSource = value;
                this.gridViewOutras.DataBind();
            }
        }

        protected void cvAreaOutra_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtAreaOutra.Text))
                txtAreaOutra.Text = "0";

            if (Convert.ToDouble(txtAreaOutra.Text) > 0)
            {
                args.IsValid = true;

            }
            else
            {
                args.IsValid = false;
            }

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvDataOutra_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataOutra.Text != "")
            {
                if (IsValidDate(txtDataOutra.Text))
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

        private void UpdateOutraData()
        {
            var estruturaPropriedadeId = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (estruturaPropriedadeId == 0)
                estruturaPropriedadeId = Convert.ToInt32(Request.Params["id"]);

            var targInd = OutraInd == -1 ? 0 : OutraInd;
            outra = _fCarnaubaFacade.GetOutraByIndex(estruturaPropriedadeId, targInd);

        }

        private void UpdateGridView()
        {

            var outras = _fCarnaubaFacade.GetOutras(EstruturaPropriedadeId);
            totalOutras = outras.Count;
            gridViewOutras.DataSource = outras;
            gridViewOutras.DataBind();
        }

        public void UpdateGridView(long estruturaId)
        {
            EstruturaPropriedadeId = (int)estruturaPropriedadeId;
            var outras = _fCarnaubaFacade.GetOutras(Convert.ToInt32(estruturaPropriedadeId));
            totalOutras = outras.Count;
            gridViewOutras.DataSource = outras;
            gridViewOutras.DataBind();
        }

        public void FillAValues(Outra outra)
        {
            Tipo = outra.Tipo;
            Area = outra.Area.ToString();
            Data = outra.Data.ToString();
        }

        protected void btnDeleteEstruturaPropriedadeOutra_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int outraId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemoveOutra(EstruturaPropriedadeId, outraId);


            UpdateGridView();
        }
    }
}