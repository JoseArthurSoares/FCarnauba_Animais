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
    public partial class UserControlBenfeitoria : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int estruturaPropriedadeId;
        private int totalBenfeitorias = 0;
        public Benfeitoria benfeitoria = null;
        private string estruturaPropriedadeID;
        string lastBenfeitoria = null;

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

        public int BenfeitoriaInd
        {
            get
            {
                if (ViewState["BenfeitoriaInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["BenfeitoriaInd"]);
            }
            set { ViewState["BenfeitoriaInd"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (BenfeitoriaInd > -1)
                {
                    UpdateBenfeitoriaData();
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
            get { return txtAreaBenfeitoria.Text; }
            set { txtAreaBenfeitoria.Text = value; }
        }

        public string Data
        {
            get { return txtDataBenfeitoria.Text; }
            set { txtDataBenfeitoria.Text = value; }
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

            var benfeitoria = new Benfeitoria();

            benfeitoria.Tipo = txtTipo.Text;
            if (!String.IsNullOrEmpty(txtAreaBenfeitoria.Text))
            {
                benfeitoria.Area = Convert.ToDouble(txtAreaBenfeitoria.Text);
            }
            else
            {
                benfeitoria.Area = 0;
            }

            if (!String.IsNullOrEmpty(txtDataBenfeitoria.Text))
            {
                benfeitoria.Data = Convert.ToDateTime(txtDataBenfeitoria.Text);
            }

            if (EditMode)
            {

                _fCarnaubaFacade.SalvaBenfeitoria(EstruturaPropriedadeId, benfeitoria, BenfeitoriaInd);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Benfeitoria salva com sucesso");
                //ShowMessage("Dados salvos com sucesso.", true);
                //return;
            }
            else
            {
                if (_fCarnaubaFacade.BenfeitoriaEstruturaPropriedadeExists(estruturaPropriedade.Id.ToString(), benfeitoria.Tipo))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Benfeitoria já cadastrada no registro.");
                    return;
                }

                _fCarnaubaFacade.AdicionaBenfeitoria(EstruturaPropriedadeId.ToString(), benfeitoria);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Benfeitoria adicionada com sucesso");
                //ShowMessage("Dados inseridos com sucesso.", true);
                //return;
            }

            UpdateBenfeitoriaData();
            UpdateGridView();
        }

        public List<Benfeitoria> DataSource
        {
            set
            {
                totalBenfeitorias = value.Count;
                this.gridViewBenfeitorias.DataSource = value;
                this.gridViewBenfeitorias.DataBind();
            }
        }

        protected void cvAreaBenfeitoria_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtAreaBenfeitoria.Text))
                txtAreaBenfeitoria.Text = "0";

            if (Convert.ToDouble(txtAreaBenfeitoria.Text) > 0)
            {
                args.IsValid = true;

            }
            else
            {
                args.IsValid = false;
            }

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvDataBenfeitoria_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataBenfeitoria.Text != "")
            {
                if (IsValidDate(txtDataBenfeitoria.Text))
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

        private void UpdateBenfeitoriaData()
        {
            var estruturaPropriedadeId = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (estruturaPropriedadeId == 0)
                estruturaPropriedadeId = Convert.ToInt32(Request.Params["id"]);

            var targInd = BenfeitoriaInd == -1 ? 0 : BenfeitoriaInd;
            benfeitoria = _fCarnaubaFacade.GetBenfeitoriaByIndex(estruturaPropriedadeId, targInd);

        }

        private void UpdateGridView()
        {

            var benfeitorias = _fCarnaubaFacade.GetBenfeitorias(EstruturaPropriedadeId);
            totalBenfeitorias = benfeitorias.Count;
            gridViewBenfeitorias.DataSource = benfeitorias;
            gridViewBenfeitorias.DataBind();
        }

        public void UpdateGridView(long estruturaId)
        {
            EstruturaPropriedadeId = (int)estruturaPropriedadeId;
            var benfeitorias = _fCarnaubaFacade.GetBenfeitorias(Convert.ToInt32(estruturaPropriedadeId));
            totalBenfeitorias = benfeitorias.Count;
            gridViewBenfeitorias.DataSource = benfeitorias;
            gridViewBenfeitorias.DataBind();
        }

        public void FillAValues(Benfeitoria benfeitoria)
        {
            Tipo = benfeitoria.Tipo;
            Area = benfeitoria.Area.ToString();
            Data = benfeitoria.Data.ToString();
        }

        protected void btnDeleteEstruturaPropriedadeBenfeitoria_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int benfeitoriaId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemoveBenfeitoria(EstruturaPropriedadeId, benfeitoriaId);


            UpdateGridView();
        }
    }

}