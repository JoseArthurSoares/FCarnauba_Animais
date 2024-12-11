using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais_WebMobile.util;
using LightInfocon.GoldenAccess.General;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;
using Image = iTextSharp.text.Image;
using ListItem = System.Web.UI.WebControls.ListItem;
using FCarnauba_Animais_WebMobile.DataSources;

namespace FCarnauba_Animais_WebMobile
{
    public partial class EditaMensuracao : PaginaBaseMobile
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        private int lotePonderalId;
        private int totalMensuracoes = 0;
        public Mensuracao mensuracao = null;
        private string lotePonderalID;
        string lastMensuracao = null;

        public EditaMensuracao()
        {
            _PageType = new EditaMensuracaoType();
        }

        public string LotePonderalId
        {
            get
            {
                string lotePonderalId = Request.Params["lotePonderalId"];
                return lotePonderalId;
            }
        }

        public string MensuracaoId
        {
            get
            {
                string mensuracaoId = Request.Params["p"];
                return mensuracaoId;
            }
        }

        public string NomeMatriz
        {
            get { return lblNomeMatriz.Text; }
            set { lblNomeMatriz.Text = value; }
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

        public string CondicaoCriacao
        {
            get { return ddlCondicoesCriacao.SelectedValue; }
            set { ddlCondicoesCriacao.SelectedValue = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             CheckAllowance();
             if (!IsPostBackOrCallBack())
             {
                 mensuracao = _fCarnaubaFacade.GetMensuracaoByIndex(Convert.ToInt32(LotePonderalId), Convert.ToInt32(MensuracaoId));
                 FillMValues(mensuracao);
             }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                mensuracao = _fCarnaubaFacade.GetMensuracaoByIndex(Convert.ToInt32(LotePonderalId), Convert.ToInt32(MensuracaoId));
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

                mensuracao.CaracterizacaoRacial = ddlCaracterizacoesRaciais.SelectedValue;
                if (mensuracao.CaracterizacaoRacial == "Caracterização Racial")
                    mensuracao.CaracterizacaoRacial = "";
                mensuracao.ClassificacaoUbere = ddlClassificacoesUbere.SelectedValue;
                if (mensuracao.ClassificacaoUbere == "Classificação de Úbere")
                    mensuracao.ClassificacaoUbere = "";
                mensuracao.RegimeAlimentar = ddlRegimeAlimentar.SelectedValue;
                if (mensuracao.RegimeAlimentar == "Regime Alimentar")
                    mensuracao.RegimeAlimentar = "";
                mensuracao.CondicaoCriacao = ddlCondicoesCriacao.SelectedValue;
                if (mensuracao.CondicaoCriacao == "Condição de Criação")
                    mensuracao.CondicaoCriacao = "";

                _fCarnaubaFacade.SalvaMensuracao(Convert.ToInt32(LotePonderalId), mensuracao, Convert.ToInt32(MensuracaoId));
                Response.Redirect("Ponderal.aspx?lotePonderalId=" + LotePonderalId);
            }
            catch (Exception)
            {
                lblMensagem.Text = "Dados inválidos";
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ponderal.aspx?lotePonderalId=" + LotePonderalId);
        }

        public void FillMValues(Mensuracao mensuracao)
        {
            NomeMatriz = mensuracao.NomeAnimal;
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
            CondicaoCriacao = mensuracao.CondicaoCriacao;
        }
    }
}