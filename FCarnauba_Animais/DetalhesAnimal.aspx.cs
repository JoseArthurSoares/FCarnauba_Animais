using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.util;
using LightInfocon.GoldenAccess.General;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;
using Image = iTextSharp.text.Image;
using ListItem = System.Web.UI.WebControls.ListItem;
using FCarnauba_Animais.UserControls;

namespace FCarnauba_Animais
{
    public partial class DetalhesAnimal : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public string SAnimal;
        private Animal animal;

        public DetalhesAnimal()
        {
            _PageType = new DetalhesAnimalType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public string AnimalId
        {
            get
            {
                string animalId = Request.Params["animalId"];
                return animalId;
            }
        }

        public string Act
        {
            get
            {
                string act = Request.Params["act"];
                return act;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AddToCurrentPath("<font color=#156AE9> <<-Animais</font>", "Animais.aspx");
            UpdateTitle();

            animal = _fCarnaubaFacade.GetAnimalByIdCompleto(AnimalId);

            lblNome.Text = animal.Nome;
            lblNomeCompleto.Text = animal.NomeCompleto;
            lblRaca.Text = animal.Raca;
            lblPropriedade.Text = animal.NomeFazenda;
            lblNumeroOrdem.Text = animal.NumeroOrdem.ToString();
            lblSexo.Text = animal.Sexo;
            lblRgnSerie.Text = animal.RgnSerie;
            lblRgnNumero.Text = animal.RgnNumero.ToString();
            lblRgn.Text = animal.Rgn;

            if (animal.TemRgn)
            {
                lblTemRgn.Text = "Sim";
            }
            else
            {
                lblTemRgn.Text = "Não";
            }

            lblRgdSerie.Text = animal.RgdSerie;
            lblRgdNumero.Text = animal.RgdNumero.ToString();
            lblRgd.Text = animal.Rgd;

            if (animal.TemRgd)
            {
                lblTemRgd.Text = "Sim";
            }
            else
            {
                lblTemRgd.Text = "Não";
            }


            lblDataNascimento.Text = animal.DataNascimento.ToShortDateString();
            lblPn.Text = animal.Pn.ToString();
            lblNomePai.Text = animal.NomePai;
            lblNomeMae.Text = animal.NomeMae;
            lblCdcOrigem.Text = animal.CdcOrigem.ToString();
            if (animal.DataCdc.Year > 1)
            {
                lblDataCdc.Text = animal.DataCdc.ToShortDateString();
            }
            else
            {
                lblDataCdc.Text = "";
            }
            lblCdnOrigem.Text = animal.CdnOrigem.ToString();
            //strFoto
            if (animal.HasFoto())
            {
                lnkFoto.Text = _fCarnaubaFacade.GetNomeArquivoOriginal(animal.Foto.OriginalFileName);
            }
            else
            {
                lnkFoto.Visible = false;
            }

            //strLaudoDna
            if (animal.HasPDFLaudoDna())
            {
                lnkLaudoDNA.Text = _fCarnaubaFacade.GetNomeArquivoOriginal(animal.LaudoDna.OriginalFileName);
            }
            else
            {
                lnkLaudoDNA.Visible = false;
            }

            if (animal.TemLaudoDna)
            {
                lblTemLaudoDNA.Text = "Sim";
            }
            else
            {
                lblTemLaudoDNA.Text = "Não";
            }

            //strLaudoDna2
            if (animal.HasPDFLaudoDna2())
            {
                lnkLaudoDNA2.Text = _fCarnaubaFacade.GetNomeArquivoOriginal(animal.LaudoDna2.OriginalFileName);
            }
            else
            {
                lnkLaudoDNA2.Visible = false;
            }

            if (animal.TemLaudoArquivoPermanente)
            {
                lblTemLaudoArquivoPermanente.Text = "Sim";
            }
            else
            {
                lblTemLaudoArquivoPermanente.Text = "Não";
            }

            //strLaudoDna3
            if (animal.HasPDFLaudoDna3())
            {
                lnkLaudoDNA3.Text = _fCarnaubaFacade.GetNomeArquivoOriginal(animal.LaudoDna3.OriginalFileName);
            }
            else
            {
                lnkLaudoDNA3.Visible = false;
            }

            if (animal.TemLaudoSecundario1)
            {
                lblTemLaudoSecundario1.Text = "Sim";
            }
            else
            {
                lblTemLaudoSecundario1.Text = "Não";
            }

            //strLaudoDna4
            if (animal.HasPDFLaudoDna4())
            {
                lnkLaudoDNA4.Text = _fCarnaubaFacade.GetNomeArquivoOriginal(animal.LaudoDna4.OriginalFileName);
            }
            else
            {
                lnkLaudoDNA4.Visible = false;
            }

            if (animal.TemLaudoSecundario2)
            {
                lblTemLaudoSecundario2.Text = "Sim";
            }
            else
            {
                lblTemLaudoSecundario2.Text = "Não";
            }

            //strLaudoBetaCaseina
            if (animal.HasPDFLaudoBetaCaseina())
            {
                lnkLaudoBetaCaseina.Text = _fCarnaubaFacade.GetNomeArquivoOriginal(animal.LaudoBetaCaseina.OriginalFileName);
            }
            else
            {
                lnkLaudoBetaCaseina.Visible = false;
            }

            lblTipoBetaCaseina.Text = animal.TipoBetaCaseina;

            if (animal.TemLaudoBetaCaseina)
            {
                lblTemLaudoBetaCaseina.Text = "Sim";
            }
            else
            {
                lblTemLaudoBetaCaseina.Text = "Não";
            }

            //strLaudoKappaCaseina
            if (animal.HasPDFLaudoKappaCaseina())
            {
                lnkLaudoKappaCaseina.Text = _fCarnaubaFacade.GetNomeArquivoOriginal(animal.LaudoKappaCaseina.OriginalFileName);
            }
            else
            {
                lnkLaudoKappaCaseina.Visible = false;
            }

            lblTipoKappaCaseina.Text = animal.TipoKappaCaseina;

            if (animal.TemLaudoKappaCaseina)
            {
                lblTemLaudoKappaCaseina.Text = "Sim";
            }
            else
            {
                lblTemLaudoKappaCaseina.Text = "Não";
            }

            lblObservacoes.Text = animal.Observacoes;
            lblUsuario.Text = animal.Usuario;
            lblDataUsuario.Text = animal.DataUsuario.ToShortDateString();

            if (animal.EhFIV)
            {
                lblFiv.Text = "Sim";
            }
            else
            {
                lblFiv.Text = "Não";
            }

            lblReceptora.Text = animal.NomeReceptora;
            lblTipoParto.Text = animal.TipoParto;
            lblVigorBezerro.Text = animal.VigorBez;
            lblEstadoCorporalMae.Text = animal.EstadoCorporalMae;
            lblTamanhoTeta.Text = animal.TamanhoTeta;
            lblMaeBoaLeite.Text = animal.MaeBoaLeite;

            if (animal.MaeOrdenhada)
            {
                lblMaeOrdenhada.Text = "Sim";
            }
            else
            {
                lblMaeOrdenhada.Text = "Não";
            }

            if (animal.AnimalImprodutivo)
            {
                lblAnimalImprodutivo.Text = "Sim";
            }
            else
            {
                lblAnimalImprodutivo.Text = "Não";
            }


            if (!String.IsNullOrEmpty(AnimalId))
            {
                //Histórico
                UserControlHistorico1.AnimalId = Convert.ToInt32(AnimalId);
                UserControlHistorico1.DataSource = animal.Historicos;
                UserControlHistorico1.AddMode = true;
                DataBind();

                //Mensuracoes
                UserControlMensuracaoAnimal1.AnimalId = Convert.ToInt32(AnimalId);
                UserControlMensuracaoAnimal1.DataSource = animal.Mensuracoes;
                UserControlMensuracaoAnimal1.AddMode = true;
                DataBind();
                
            }
        }

        protected void DeleteControleLeiteiro_Click(int controleLeiteiroId)
        {
            //if (!String.IsNullOrEmpty(controleLeiteiroId.ToString()))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
            //                                        String.Format("setActive('{0}');", "Controle Leiteiro"), true);

            //    _fCarnaubaFacade.RemoveControleLeiteiro(controleLeiteiroId.ToString());
            //    ExibeMensagem(TipoDeMensagem.Sucesso, "Lote remov com sucesso");
            //    this.UserControlCadastraControleLeiteiro1.DataSource = _fCarnaubaFacade.GetControlesById(LoteId);


            //    //Response.Redirect("~/CadastrarLote.aspx?act=edit&loteId=" + LoteId + "&tabIndex=#controleleiteiro-tab");
            //}
        }

        protected void lnkLaudoDNA_Click(object sender, EventArgs e)
        {
            string fullPath = FCarnaubaDataAccess.GetPathForAnimal(animal.LaudoDna.OriginalFileName);

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkLaudoDNA.Text);
            Response.WriteFile(fullPath);
            Response.End();
        }

        protected void lnkLaudoDNA2_Click(object sender, EventArgs e)
        {
            string fullPath = FCarnaubaDataAccess.GetPathForAnimal(animal.LaudoDna2.OriginalFileName);

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkLaudoDNA2.Text);
            Response.WriteFile(fullPath);
            Response.End();
        }

        protected void lnkLaudoDNA3_Click(object sender, EventArgs e)
        {
            string fullPath = FCarnaubaDataAccess.GetPathForAnimal(animal.LaudoDna3.OriginalFileName);

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkLaudoDNA3.Text);
            Response.WriteFile(fullPath);
            Response.End();
        }

        protected void lnkLaudoDNA4_Click(object sender, EventArgs e)
        {
            string fullPath = FCarnaubaDataAccess.GetPathForAnimal(animal.LaudoDna4.OriginalFileName);

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkLaudoDNA4.Text);
            Response.WriteFile(fullPath);
            Response.End();
        }

        protected void lnkLaudoBetaCaseina_Click(object sender, EventArgs e)
        {
            string fullPath = FCarnaubaDataAccess.GetPathForAnimal(animal.LaudoBetaCaseina.OriginalFileName);

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkLaudoBetaCaseina.Text);
            Response.WriteFile(fullPath);
            Response.End();
        }

        protected void lnkLaudoKappaCaseina_Click(object sender, EventArgs e)
        {
            string fullPath = FCarnaubaDataAccess.GetPathForAnimal(animal.LaudoKappaCaseina.OriginalFileName);

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkLaudoKappaCaseina.Text);
            Response.WriteFile(fullPath);
            Response.End();
        }

        protected void lnkFoto_Click(object sender, EventArgs e)
        {
            string fullPath = FCarnaubaDataAccess.GetPathForAnimal(animal.Foto.OriginalFileName);

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkFoto.Text);
            Response.WriteFile(fullPath);
            Response.End();
        }
    }
}