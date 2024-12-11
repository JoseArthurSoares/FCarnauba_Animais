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
    public partial class UserControlPastagem : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int estruturaPropriedadeId;
        private int totalPastagens = 0;
        public Pastagem pastagem = null;
        private string estruturaPropriedadeID;
        string lastPastagem = null;

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

        public int PastagemInd
        {
            get
            {
                if (ViewState["PastagemInd"] == null)
                {
                    return -1;
                }
                return Convert.ToInt32(ViewState["PastagemInd"]);
            }
            set { ViewState["PastagemInd"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
                if (PastagemInd > -1)
                {
                    UpdatePastagemData();
                }
            }
        }

        public string Tipo
        {
            get { return ddlTipos.SelectedValue; }
            set { ddlTipos.SelectedValue = value; }
        }

        public string Area
        {
            get { return txtAreaPastagem.Text; }
            set { txtAreaPastagem.Text = value; }
        }

        public string Data
        {
            get { return txtDataPastagem.Text; }
            set { txtDataPastagem.Text = value; }
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

            var pastagem = new Pastagem();

            pastagem.Tipo = ddlTipos.SelectedValue;
            if (!String.IsNullOrEmpty(txtAreaPastagem.Text))
            {
                pastagem.Area = Convert.ToDouble(txtAreaPastagem.Text);
            }
            else
            {
                pastagem.Area = 0;
            }

            if (!String.IsNullOrEmpty(txtDataPastagem.Text))
            {
                pastagem.Data = Convert.ToDateTime(txtDataPastagem.Text);
            }

            if (EditMode)
            {

                _fCarnaubaFacade.SalvaPastagem(EstruturaPropriedadeId, pastagem, PastagemInd);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Pastagem salva com sucesso");
                //ShowMessage("Dados salvos com sucesso.", true);
                //return;
            }
            else
            {
                //if (_fCarnaubaFacade.PastagemEstruturaPropriedadeExists(estruturaPropriedade.Id.ToString(), pastagem.Tipo))
                //{
                //    ExibeMensagem(TipoDeMensagem.Aviso, "Pastagem já cadastrada no registro.");
                //    return;
                //}

                _fCarnaubaFacade.AdicionaPastagem(EstruturaPropriedadeId.ToString(), pastagem);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Pastagem adicionada com sucesso");
                //ShowMessage("Dados inseridos com sucesso.", true);
                //return;
            }

            UpdatePastagemData();
            UpdateGridView();
        }

        public List<Pastagem> DataSource
        {
            set
            {
                totalPastagens = value.Count;
                this.gridViewPastagens.DataSource = value;
                this.gridViewPastagens.DataBind();
            }
        }


        protected void cvAreaPastagem_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtAreaPastagem.Text))
                txtAreaPastagem.Text = "0";

            if (Convert.ToDouble(txtAreaPastagem.Text) > 0)
            {
                args.IsValid = true;

            }
            else
            {
                args.IsValid = false;
            }

            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvDataPastagem_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataPastagem.Text != "")
            {
                if (IsValidDate(txtDataPastagem.Text))
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

        private void UpdatePastagemData()
        {
            var estruturaPropriedadeId = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (estruturaPropriedadeId == 0)
                estruturaPropriedadeId = Convert.ToInt32(Request.Params["id"]);
            
            var targInd = PastagemInd == -1 ? 0 : PastagemInd;
            pastagem = _fCarnaubaFacade.GetPastagemByIndex(estruturaPropriedadeId, targInd);

        }

        private void UpdateGridView()
        {

            var pastagens = _fCarnaubaFacade.GetPastagem(EstruturaPropriedadeId);
            totalPastagens = pastagens.Count;
            gridViewPastagens.DataSource = pastagens;
            gridViewPastagens.DataBind();
        }

        public void UpdateGridView(long estruturaId)
        {
            EstruturaPropriedadeId = (int)estruturaPropriedadeId;
            var pastagens = _fCarnaubaFacade.GetPastagem(Convert.ToInt32(estruturaPropriedadeId));
            totalPastagens = pastagens.Count;
            gridViewPastagens.DataSource = pastagens;
            gridViewPastagens.DataBind();
        }

        public void FillPValues(Pastagem pastagem)
        {
            ddlTipos.SelectedValue = pastagem.Tipo;
            Area = pastagem.Area.ToString();
            Data = pastagem.Data.ToString();
        }

        protected void btnDeleteEstruturaPropriedade_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int pastagemId = Convert.ToInt32(btn.CommandArgument);


            _fCarnaubaFacade.RemovePastagem(EstruturaPropriedadeId, pastagemId);


            UpdateGridView();
        }

    }
}