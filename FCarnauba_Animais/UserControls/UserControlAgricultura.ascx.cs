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
    public partial class UserControlAgricultura : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int estruturaPropriedadeId;
        private int totalAgriculturas = 0;
        public Agricultura agricultura = null;
        private string estruturaPropriedadeID;
        string lastAgricultura = null;

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

        public int AgriculturaInd
        {
            get
            {
                if (ViewState["AgriculturaInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["AgriculturaInd"]);
            }
            set { ViewState["AgriculturaInd"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (AgriculturaInd > -1)
                {
                    UpdateAgriculturaData();
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
            get { return txtAreaAgricultura.Text; }
            set { txtAreaAgricultura.Text = value; }
        }

        public string Data
        {
            get { return txtDataAgricultura.Text; }
            set { txtDataAgricultura.Text = value; }
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

            var agricultura = new Agricultura();

            agricultura.Tipo = txtTipo.Text;
            if (!String.IsNullOrEmpty(txtAreaAgricultura.Text))
            {
                agricultura.Area = Convert.ToDouble(txtAreaAgricultura.Text);
            }
            else
            {
                agricultura.Area = 0;
            }

            if (!String.IsNullOrEmpty(txtDataAgricultura.Text))
            {
                agricultura.Data = Convert.ToDateTime(txtDataAgricultura.Text);
            }

            if (EditMode)
            {

                _fCarnaubaFacade.SalvaAgricultura(EstruturaPropriedadeId, agricultura, AgriculturaInd);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Agricultura salva com sucesso");
                //ShowMessage("Dados salvos com sucesso.", true);
                //return;
            }
            else
            {
                if (_fCarnaubaFacade.AgriculturaEstruturaPropriedadeExists(estruturaPropriedade.Id.ToString(), agricultura.Tipo))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Agricultura já cadastrada no registro.");
                    return;
                }

                _fCarnaubaFacade.AdicionaAgricultura(EstruturaPropriedadeId.ToString(), agricultura);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Agricultura adicionada com sucesso");
                //ShowMessage("Dados inseridos com sucesso.", true);
                //return;
            }

            UpdateAgriculturaData();
            UpdateGridView();
        }

        public List<Agricultura> DataSource
        {
            set
            {
                totalAgriculturas = value.Count;
                this.gridViewAgriculturas.DataSource = value;
                this.gridViewAgriculturas.DataBind();
            }
        }


        protected void cvAreaAgricultura_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtAreaAgricultura.Text))
                txtAreaAgricultura.Text = "0";

            if (Convert.ToDouble(txtAreaAgricultura.Text) > 0)
            {
                args.IsValid = true;

            }
            else
            {
                args.IsValid = false;
            }

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvDataAgricultura_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataAgricultura.Text != "")
            {
                if (IsValidDate(txtDataAgricultura.Text))
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

        private void UpdateAgriculturaData()
        {
            var estruturaPropriedadeId = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (estruturaPropriedadeId == 0)
                estruturaPropriedadeId = Convert.ToInt32(Request.Params["id"]);

            var targInd = AgriculturaInd == -1 ? 0 : AgriculturaInd;
            agricultura = _fCarnaubaFacade.GetAgriculturaByIndex(estruturaPropriedadeId, targInd);

        }

        private void UpdateGridView()
        {

            var agriculturas = _fCarnaubaFacade.GetAgriculturas(EstruturaPropriedadeId);
            totalAgriculturas = agriculturas.Count;
            gridViewAgriculturas.DataSource = agriculturas;
            gridViewAgriculturas.DataBind();
        }

        public void UpdateGridView(long estruturaId)
        {
            EstruturaPropriedadeId = (int)estruturaPropriedadeId;
            var agriculturas = _fCarnaubaFacade.GetAgriculturas(Convert.ToInt32(estruturaPropriedadeId));
            totalAgriculturas = agriculturas.Count;
            gridViewAgriculturas.DataSource = agriculturas;
            gridViewAgriculturas.DataBind();
        }

        public void FillAValues(Agricultura agricultura)
        {
            Tipo = agricultura.Tipo;
            Area = agricultura.Area.ToString();
            Data = agricultura.Data.ToString();
        }

        protected void btnDeleteEstruturaPropriedadeAgricultura_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int agriculturaId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemoveAgricultura(EstruturaPropriedadeId, agriculturaId);


            UpdateGridView();
        }

    }
}