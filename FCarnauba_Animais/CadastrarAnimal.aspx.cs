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
    public partial class CadastrarAnimal : PaginaBase
    {
        private string act = null;
        public string AnimalId;
        public long ultimoId = 0;
        public int animalId = 0;
        private bool requestedValidation = false;
        private string op = null;

        public CadastrarAnimal()
        {
            _PageType = new AnimaisType();
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
            AddToCurrentPath("<font color=#156AE9> <<-Animais</font>", "Animais.aspx");
            UpdateTitle();
            animalId = Convert.ToInt32(Request.QueryString["animalId"]);
            AnimalId = Request.QueryString["AnimalId"];

            op = Request.QueryString["op"];

            if (!String.IsNullOrEmpty(op))
                ExibeMensagem(TipoDeMensagem.Sucesso, "Animal adicionado com sucesso");

            if (!IsPostBackOrCallBack())
            {
                if (!String.IsNullOrEmpty((string)Session["raca"]))
                {
                    ddlRaca.SelectedValue = (string)Session["raca"];
                    ddlRaca.Enabled = false;
                }

                AnimalId = Request.QueryString["AnimalId"];
                CheckAllowance();

                animalId = Convert.ToInt32(Request.Params["animalId"]);
                var animal = FCarnaubaFacade.GetAnimalByIdCompleto(animalId.ToString());

                ddlPropriedade.DataSource = _fCarnaubaFacade.GetSimplesPropriedades();
                ddlPropriedade.DataBind();
                ddlPropriedade.Items.Insert(0, new ListItem("", null));
                ddlPropriedade.SelectedIndex = -1;

                ddlPai.DataSource = FCarnaubaFacade.GetPais();
                ddlPai.DataValueField = "Id";
                ddlPai.DataTextField = "NomeCompleto";
                ddlPai.DataBind();

                ddlMae.DataSource = FCarnaubaFacade.GetMaes();
                ddlMae.DataValueField = "Id";
                ddlMae.DataTextField = "NomeCompleto";
                ddlMae.DataBind();

                ddlReceptora.DataSource = FCarnaubaFacade.GetMaes();
                ddlReceptora.DataValueField = "Id";
                ddlReceptora.DataTextField = "NomeCompleto";
                ddlReceptora.DataBind();


                //Histórico
                UserControlHistorico1.AnimalId = animalId;
                UserControlHistorico1.DataSource = animal.Historicos;
                UserControlHistorico1.AddMode = true;

                //Mensuracoes
                UserControlMensuracaoAnimal1.AnimalId = animalId;
                UserControlMensuracaoAnimal1.DataSource = animal.Mensuracoes;
                UserControlMensuracaoAnimal1.AddMode = true;

                CheckAllowance();

                if (animalId != 0)
                    FillAnimalFields(animal);


            }

            if (Request.Params["tabIndex"] != null)
            {
                var tabIndex = Request.Params["tabIndex"];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                    String.Format("setActive('{0}');", tabIndex), true);
            }
            else
            {
                UserControlHistorico1.EditMode = false;
                UserControlHistorico1.ReadOnly = true;

                UserControlMensuracaoAnimal1.EditMode = false;
                UserControlMensuracaoAnimal1.ReadOnly = true;
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

        protected void cvNumeroOrdem_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtNumeroOrdem.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvPn_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtPn.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvNome_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtNome.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvRgnSerie_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtRgnSerie.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvRgnNumero_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtRgnNumero.Text != "")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvDataNascimento_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtDataNascimento.Text != "")
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

        protected void cvFoto_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!String.IsNullOrEmpty(FileUploadFoto.FileName))
            {

                if (FileUploadFoto.FileName.ToLower().Contains(".jpg") || FileUploadFoto.FileName.ToLower().Contains(".gif") || FileUploadFoto.FileName.ToLower().Contains(".png"))
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
        }

        protected void cvLaudoDna_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!String.IsNullOrEmpty(FileUploadLaudoDna.FileName))
            {

                if (FileUploadLaudoDna.FileName.ToLower().Contains(".pdf"))
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
        }

        protected void cvLaudoDna2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!String.IsNullOrEmpty(FileUploadLaudoDna2.FileName))
            {

                if (FileUploadLaudoDna2.FileName.ToLower().Contains(".pdf"))
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
        }

        protected void cvLaudoDna3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!String.IsNullOrEmpty(FileUploadLaudoDna3.FileName))
            {

                if (FileUploadLaudoDna3.FileName.ToLower().Contains(".pdf"))
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
        }

        protected void cvLaudoDna4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!String.IsNullOrEmpty(FileUploadLaudoDna4.FileName))
            {

                if (FileUploadLaudoDna4.FileName.ToLower().Contains(".pdf"))
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
        }

        protected void cvLaudoBetaCaseina_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!String.IsNullOrEmpty(FileUploadLaudoBetaCaseina.FileName))
            {

                if (FileUploadLaudoBetaCaseina.FileName.ToLower().Contains(".pdf"))
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
        }

        protected void cvLaudoKappaCaseina_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!String.IsNullOrEmpty(FileUploadLaudoKappaCaseina.FileName))
            {

                if (FileUploadLaudoKappaCaseina.FileName.ToLower().Contains(".pdf"))
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
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            AnimalId = Request.QueryString["AnimalId"];
            requestedValidation = true;
            Page.Validate();
            if (IsValid)
            {

                Animal animal = new Animal();

                animal.Diretorio = "";
                animal.NumeroOrdem = Convert.ToInt64(txtNumeroOrdem.Text);
                animal.Nome = txtNome.Text;
                animal.NomeCompleto = ddlRaca.SelectedValue + " - " + txtNome.Text + " - " + txtRgdSerie.Text + " " + txtRgdNumero.Text;

                animal.TemRgn = ckRgnOK.Checked;
                animal.RgnSerie = txtRgnSerie.Text;
                animal.RgnNumero = Convert.ToInt64(txtRgnNumero.Text);
                animal.Rgn = txtRgnSerie.Text + "  " + txtRgnNumero.Text;

                animal.TemRgd = ckRgdOk.Checked;
                animal.RgdSerie = txtRgdSerie.Text;
                if (!String.IsNullOrEmpty(txtRgdNumero.Text))
                {
                    animal.RgdNumero = Convert.ToInt64(txtRgdNumero.Text);
                    animal.Rgd = txtRgdSerie.Text + "  " + txtRgdNumero.Text;
                }

                animal.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
                animal.NomeFazenda = ddlPropriedade.SelectedValue;
                animal.Raca = ddlRaca.SelectedValue;
                animal.Sexo = ddlSexo.SelectedValue;

                animal.DataUsuario = DateTime.Today;

                if (!String.IsNullOrEmpty(txtPn.Text))
                {
                    animal.Pn = Convert.ToDouble(txtPn.Text);
                }

                animal.StrPaiId = ddlPai.SelectedValue;
                animal.StrMaeId = ddlMae.SelectedValue;

                if (!String.IsNullOrEmpty(txtCdnOrigem.Text))
                {
                    animal.CdnOrigem = Convert.ToInt64(txtCdnOrigem.Text);
                }

                if (FileUploadFoto.HasFile)
                {
                    animal.Foto = new Arquivo(this.FileUploadFoto.FileName, FileUploadFoto.FileContent);
                }

                if (FileUploadLaudoDna.HasFile)
                {
                    animal.LaudoDna = new Arquivo(this.FileUploadLaudoDna.FileName, FileUploadLaudoDna.FileContent);
                }

                if (FileUploadLaudoDna2.HasFile)
                {
                    animal.LaudoDna2 = new Arquivo(this.FileUploadLaudoDna2.FileName, FileUploadLaudoDna2.FileContent);
                }

                if (FileUploadLaudoDna3.HasFile)
                {
                    animal.LaudoDna3 = new Arquivo(this.FileUploadLaudoDna3.FileName, FileUploadLaudoDna3.FileContent);
                }

                if (FileUploadLaudoDna4.HasFile)
                {
                    animal.LaudoDna4 = new Arquivo(this.FileUploadLaudoDna4.FileName, FileUploadLaudoDna4.FileContent);
                }

                if (FileUploadLaudoBetaCaseina.HasFile)
                {
                    animal.LaudoBetaCaseina = new Arquivo(this.FileUploadLaudoBetaCaseina.FileName, FileUploadLaudoBetaCaseina.FileContent);
                }

                if (FileUploadLaudoKappaCaseina.HasFile)
                {
                    animal.LaudoKappaCaseina = new Arquivo(this.FileUploadLaudoKappaCaseina.FileName, FileUploadLaudoKappaCaseina.FileContent);
                }

                animal.TemLaudoDna = ckLaudoDnaOk.Checked;
                animal.TemLaudoSecundario1 = ckLaudoDna3.Checked;
                animal.TemLaudoSecundario2 = ckLaudoDna4.Checked;
                animal.TipoBetaCaseina = ddlTipoBetaCaseina.SelectedValue;
                animal.TemLaudoBetaCaseina = ckLaudoBetaCaseina.Checked;
                animal.TipoKappaCaseina = ddlTipoKappaCaseina.SelectedValue;
                animal.TemLaudoKappaCaseina = ckLaudoKappaCaseina.Checked;
                animal.TemLaudoArquivoPermanente = ckLaudoDna2.Checked;

                animal.Observacoes = txtObservacoes.Text;
                animal.EhFIV = ckFiv.Checked;
                animal.StrReceptoraId = ddlReceptora.SelectedValue;
                animal.TipoParto = ddlTipoParto.SelectedValue;
                animal.VigorBez = ddlVigorBezerro.SelectedValue;
                animal.EstadoCorporalMae = ddlEstadoCorporalMae.SelectedValue;
                animal.TamanhoTeta = ddlTamanhoTeta.SelectedValue;
                animal.MaeOrdenhada = ckMaeOrdenhada.Checked;
                animal.AnimalImprodutivo = ckAnimalImprodutivo.Checked;
                animal.MaeBoaLeite = ddlMaeBoaLeite.SelectedValue;

                if (animal.DataNascimento > DateTime.Today)
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "A data de nascimento não pode ser maior que a data de hoje.");
                    return;
                }

                if (animal.EhFIV && String.IsNullOrEmpty(animal.StrReceptoraId))
                {
                    ExibeMensagem(TipoDeMensagem.Aviso, "Ao marcar FIV, é obrigatório informar Receptora.");
                    return;
                }


                if (EditMode)
                {
                    var tabIndex = "Cadastro";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                        String.Format("setActive('{0}');", tabIndex), true);
                    animal.Usuario = UsuarioLogado.Name + " (editado)";
                    animal.Id = Convert.ToInt32(AnimalId);
                    FCarnaubaFacade.SalvaAnimal(animal);

                    ExibeMensagem(TipoDeMensagem.Sucesso, "Animal salvo com sucesso");

                }
                else
                {
                    animal.Usuario = UsuarioLogado.Name;
                    
                    animal.Id = FCarnaubaFacade.AdicionaAnimal(animal);

                    ExibeMensagem(TipoDeMensagem.Sucesso, "Animal adicionado com sucesso");

                }
            }
        }

        private void FillAnimalFields(Animal animal)
        {
            txtNumeroOrdem.Text = Convert.ToString(animal.NumeroOrdem);
            txtNome.Text = animal.Nome;
            ddlPropriedade.SelectedValue = animal.NomeFazenda;
            ddlRaca.SelectedValue = animal.Raca;
            ddlSexo.SelectedValue = animal.Sexo;
            txtRgnSerie.Text = animal.RgnSerie;
            txtRgnNumero.Text = Convert.ToString(animal.RgnNumero);
            ckRgnOK.Checked = animal.TemRgn;
            txtRgdSerie.Text = animal.RgdSerie;
            txtRgdNumero.Text = Convert.ToString(animal.RgdNumero);
            ckRgdOk.Checked = animal.TemRgd;
            txtDataNascimento.Text = animal.DataNascimento.ToShortDateString();
            txtPn.Text = Convert.ToString(animal.Pn);
            ddlPai.SelectedValue = animal.StrPaiId;
            ddlMae.SelectedValue = animal.StrMaeId;
            txtCdnOrigem.Text = Convert.ToString(animal.CdnOrigem);
            ckLaudoDnaOk.Checked = animal.TemLaudoDna;
            ckLaudoDna2.Checked = animal.TemLaudoArquivoPermanente;
            ckLaudoDna3.Checked = animal.TemLaudoSecundario1;
            ckLaudoDna4.Checked = animal.TemLaudoSecundario2;
            ddlTipoBetaCaseina.SelectedValue = animal.TipoBetaCaseina;
            ckLaudoBetaCaseina.Checked = animal.TemLaudoBetaCaseina;
            ddlTipoKappaCaseina.SelectedValue = animal.TipoKappaCaseina;
            ckLaudoKappaCaseina.Checked = animal.TemLaudoKappaCaseina;
            txtObservacoes.Text = animal.Observacoes;
            ckFiv.Checked = animal.EhFIV;
            ddlReceptora.SelectedValue = animal.StrReceptoraId;
            ddlTipoParto.SelectedValue = animal.TipoParto;
            ddlVigorBezerro.SelectedValue = animal.VigorBez;
            ddlEstadoCorporalMae.SelectedValue = animal.EstadoCorporalMae;
            ddlTamanhoTeta.SelectedValue = animal.TamanhoTeta;
            ddlMaeBoaLeite.SelectedValue = animal.MaeBoaLeite;
            ckMaeOrdenhada.Checked = animal.MaeOrdenhada;
            ckAnimalImprodutivo.Checked = animal.AnimalImprodutivo;

        }

        protected void btnPesquisarDDlPai_Click(object send, EventArgs e)
        {
            string pesquisa = txtPesquisaDdlPai.Text;
            if (String.IsNullOrEmpty(pesquisa))
                pesquisa = "*";

            this.ddlPai.DataSource = _fCarnaubaFacade.GetPais("*", pesquisa);
            ddlPai.DataValueField = "Id";
            ddlPai.DataTextField = "NomeCompleto";
            ddlPai.DataBind();
        }

        protected void btnPesquisarDDlMae_Click(object send, EventArgs e)
        {
            string pesquisa = txtPesquisaDdlMae.Text;
            if (String.IsNullOrEmpty(pesquisa))
                pesquisa = "*";

            this.ddlMae.DataSource = _fCarnaubaFacade.GetMaes("*", pesquisa);
            ddlMae.DataValueField = "Id";
            ddlMae.DataTextField = "NomeCompleto";
            ddlMae.DataBind();
        }

        protected void DeleteAnimal_Click(int controleLeiteiroId)
        {
        }

    }
}