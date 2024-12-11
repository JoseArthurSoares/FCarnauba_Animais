using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.util;
using FCarnauba_Animais.UserControls;

namespace FCarnauba_Animais
{
    public partial class CadastrarEstruturaPropriedade : PaginaBase
    {
        private string act = null;
        public string EstruturaPropriedadeId;
        public long ultimoId = 0;
        public int estruturaPropriedadeId = 0;
        private bool requestedValidation = false;
        private string op = null;


        public CadastrarEstruturaPropriedade()
        {
            _PageType = new CadastrarEstruturaPropriedadeType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            act = Request.QueryString["act"];
            AddToCurrentPath("<font color=#156AE9> <<-Propriedades</font>", "EstruturasPropriedades.aspx");
            UpdateTitleEstruturaPropriedade();
            estruturaPropriedadeId = Convert.ToInt32(Request.QueryString["estruturaPropriedadeId"]);
            EstruturaPropriedadeId = Request.QueryString["EstruturaPropriedadeId"];

            op = Request.QueryString["op"];

            if (!String.IsNullOrEmpty(op))
                ExibeMensagem(TipoDeMensagem.Sucesso, "Informações das propriedades adicionada com sucesso");

            if (!IsPostBackOrCallBack())
            {
                //if (!String.IsNullOrEmpty((string)Session["raca"]))
                //{
                //    ddlRaca.SelectedValue = (string)Session["raca"];
                //    ddlRaca.Enabled = false;
                //}

                EstruturaPropriedadeId = Request.QueryString["EstruturaPropriedadeId"];
                CheckAllowance();

                estruturaPropriedadeId = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
                var estruturaPropriedade = FCarnaubaFacade.GetEstruturaPropriedadeById(estruturaPropriedadeId.ToString());
                //UserControlMatrizes1.DataSource = lote.Matrizes;


                //Pastagens
                UserControlPastagem1.EstruturaPropriedadeId = estruturaPropriedadeId;
                UserControlPastagem1.DataSource = estruturaPropriedade.Pastagens;
                UserControlPastagem1.AddMode = true;

                //Agricultura
                UserControlAgricultura1.EstruturaPropriedadeId = estruturaPropriedadeId;
                UserControlAgricultura1.DataSource = estruturaPropriedade.Agriculturas;
                UserControlAgricultura1.AddMode = true;

                //Benfeitoria
                UserControlBenfeitoria1.EstruturaPropriedadeId = estruturaPropriedadeId;
                UserControlBenfeitoria1.DataSource = estruturaPropriedade.Benfeitorias;
                UserControlBenfeitoria1.AddMode = true;

                //Arrendamento
                UserControlArrendamento1.EstruturaPropriedadeId = estruturaPropriedadeId;
                UserControlArrendamento1.DataSource = estruturaPropriedade.Arrendamentos;
                UserControlArrendamento1.AddMode = true;

                //Outra
                UserControlOutra1.EstruturaPropriedadeId = estruturaPropriedadeId;
                UserControlOutra1.DataSource = estruturaPropriedade.Outras;
                UserControlOutra1.AddMode = true;

                //if (EditMode == false && act != null)
                //    UserControlPastagem1.ReadOnly = true;

                //DataBind();
                
                CheckAllowance();

                if (estruturaPropriedadeId != 0)
                    FillEstruturaPropriedadeFields(estruturaPropriedade);


            }

            if (Request.Params["tabIndex"] != null)
            {
                var tabIndex = Request.Params["tabIndex"];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                    String.Format("setActive('{0}');", tabIndex), true);
            }
            else
            {
                UserControlPastagem1.EditMode = false;
                UserControlPastagem1.ReadOnly = true;

                UserControlAgricultura1.EditMode = false;
                UserControlAgricultura1.ReadOnly = true;

                UserControlBenfeitoria1.EditMode = false;
                UserControlBenfeitoria1.ReadOnly = true;

                UserControlArrendamento1.EditMode = false;
                UserControlArrendamento1.ReadOnly = true;

                UserControlOutra1.EditMode = false;
                UserControlOutra1.ReadOnly = true;
            }
            var unicode = new UnicodeEncoding();
            Form.Action = unicode.GetString(unicode.GetBytes(Request.Url.ToString()));
        }

        public bool EditMode
        {
            get
            {
                return (act == "edit" || act != null);
            }
        }

        protected void cvData_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtData.Text != "")
            {
                try
                {
                    args.IsValid = true;
                    return;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
            args.IsValid = false;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvNome_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtNome.Text != "")
            {
                try
                {
                    args.IsValid = true;
                    return;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
            args.IsValid = false;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvLocalizacao_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtLocalizacao.Text != "")
            {
                try
                {
                    args.IsValid = true;
                    return;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
            args.IsValid = false;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvRegistroOficial_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtRegistroOficial.Text != "")
            {
                try
                {
                    args.IsValid = true;
                    return;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
            args.IsValid = false;
            if (!requestedValidation) args.IsValid = true;
        }



        protected void cvArea_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtArea.Text))
                txtArea.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvAreaUtilizada_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtAreaUtilizada.Text))
                txtAreaUtilizada.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvReserva_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtReserva.Text))
                txtReserva.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvAtividades_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtAtividades.Text != "")
            {
                try
                {
                    args.IsValid = true;
                    return;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
            args.IsValid = false;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvMunicipio_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtMunicipio.Text != "")
            {
                try
                {
                    args.IsValid = true;
                    return;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
            args.IsValid = false;
            if (!requestedValidation) args.IsValid = true;
        }


        protected void btnProximo_Click(object sender, EventArgs e)
        {
            EstruturaPropriedadeId = Request.QueryString["EstruturaPropriedadeId"];
            requestedValidation = true;
            Page.Validate();

            if (!Page.IsValid)
            {
                //ShowMessage("Complete os campos requeridos corretamente.", false);
                ExibeMensagem(TipoDeMensagem.Aviso, "Complete os campos requeridos corretamente.");
                return;
            }

            EstruturaPropriedade estruturaPropriedade = new EstruturaPropriedade();

            estruturaPropriedade.Data = Convert.ToDateTime(txtData.Text);
            estruturaPropriedade.NomePropriedade = txtNome.Text;
            estruturaPropriedade.Localizacao = txtLocalizacao.Text;
            estruturaPropriedade.RegistroOficial = txtRegistroOficial.Text;
            estruturaPropriedade.Area = Convert.ToDouble(txtArea.Text);
            estruturaPropriedade.AreaUtilizada = Convert.ToDouble(txtAreaUtilizada.Text);
            estruturaPropriedade.Reserva = Convert.ToDouble(txtReserva.Text);
            estruturaPropriedade.Atividades = txtAtividades.Text;
            estruturaPropriedade.Municipio = txtMunicipio.Text;
            estruturaPropriedade.Uf = ddlUf.SelectedValue;

            estruturaPropriedade.DataUsuario = DateTime.Today;

            if (EditMode)
            {
                var estruturaPropriedadeCorr = FCarnaubaFacade.GetEstruturaPropriedadeById(EstruturaPropriedadeId);

                //if (FCarnaubaFacade.EstruturaPropriedadeExists(estruturaPropriedade.IdPropriedade, estruturaPropriedade.Data) && (estruturaPropriedadeCorr.Data != estruturaPropriedade.Data || estruturaPropriedadeCorr.IdPropriedade != estruturaPropriedade.IdPropriedade))
                //{
                //    //ShowMessageControleLeiteiro("Lote já cadastrado!", false);
                //    ExibeMensagem(TipoDeMensagem.Aviso, "Informações da propriedade já cadastrada na data informada!");
                //    return;
                //}

                var tabIndex = "Cadastro";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                    String.Format("setActive('{0}');", tabIndex), true);
                estruturaPropriedade.Usuario = UsuarioLogado.Name + " (editado)";
                estruturaPropriedade.Id = Convert.ToInt32(EstruturaPropriedadeId);
                FCarnaubaFacade.SalvaEstruturaPropriedade(estruturaPropriedade);

                //var estruturaPropriedadeTemp = FCarnaubaFacade.GetEstruturaPropriedadeById(EstruturaPropriedadeId);

                //if (estruturaPropriedadeCorr.Pastagens.Count > 0)
                //{
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Informações da propriedade salva com sucesso");
                //}
                //else
                //{
                    //ExibeMensagem(TipoDeMensagem.Sucesso, "Informações da propriedade salva com sucesso, mas sem pastagens cadastradas");
                //}

            }
            else
            {
                //if (FCarnaubaFacade.EstruturaPropriedadeExists(estruturaPropriedade.IdPropriedade, estruturaPropriedade.Data))
                //{
                //    //ShowMessageControleLeiteiro("Lote já cadastrado!", false);
                //    ExibeMensagem(TipoDeMensagem.Aviso, "Informações da propriedade já cadastrada na data informada!");
                //    return;
                //}

                estruturaPropriedade.Usuario = UsuarioLogado.Name;
                estruturaPropriedade.Id =  FCarnaubaFacade.AdicionaEstruturaPropriedade(estruturaPropriedade);
                //UserControlMensuracao1.UpdateGridView(lotePonderal.Id);
                ExibeMensagem(TipoDeMensagem.Sucesso, "Informações da propriedade adicionada com sucesso");

            }


        }


        private void FillEstruturaPropriedadeFields(EstruturaPropriedade estruturaPropriedade)
        {
            txtData.Text = Convert.ToString(estruturaPropriedade.Data);
            txtNome.Text = estruturaPropriedade.NomePropriedade;
            txtLocalizacao.Text = estruturaPropriedade.Localizacao;
            txtRegistroOficial.Text = estruturaPropriedade.RegistroOficial;
            txtArea.Text = Convert.ToString(estruturaPropriedade.Area);
            txtAreaUtilizada.Text = Convert.ToString(estruturaPropriedade.AreaUtilizada);
            txtReserva.Text = Convert.ToString(estruturaPropriedade.Reserva);
            txtAtividades.Text = estruturaPropriedade.Atividades;
            txtMunicipio.Text = estruturaPropriedade.Municipio;
            ddlUf.SelectedValue = estruturaPropriedade.Uf;
        }

        protected void DeleteEstruturaPropriedade_Click(int controlePonderalId)
        {
            //if (!String.IsNullOrEmpty(controleLeiteiroId.ToString()))
            //{
            //    _fCarnaubaFacade.RemoveControleLeiteiro(controleLeiteiroId.ToString());
            //    this.UserControlCadastraControleLeiteiro1.DataSource = _fCarnaubaFacade.GetControlesById(loteId.ToString());
            //    Response.Redirect("~/CadastrarLote.aspx?act=edit&loteId=" + loteId.ToString() + "&tabIndex=Controle Leiteiro");
            //}
        }
    }
}