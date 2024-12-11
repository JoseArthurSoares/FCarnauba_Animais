using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class DetalhesMensuracaoAnimal : PaginaBase
    {
        public Mensuracao mensuracao;
        public int id;
        private int m_ind;

        public DetalhesMensuracaoAnimal()
        {
            _PageType = new DetalhesMensuracaoAnimalType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["animalId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            m_ind = Convert.ToInt32(Request.Params["m"]);

            mensuracao = FCarnaubaFacade.GetMensuracaoAnimalByIndex(id, m_ind);

            CheckAllowance();

            if (!IsPostBackOrCallBack())
            {
                Peso = Math.Round(Convert.ToDecimal(mensuracao.Peso), 2).ToString();
                CEscrotal = Math.Round(Convert.ToDecimal(mensuracao.CEscrotal), 2).ToString();
                AAnterior = Math.Round(Convert.ToDecimal(mensuracao.AAnterior), 2).ToString();
                APosterior = Math.Round(Convert.ToDecimal(mensuracao.APosterior), 2).ToString();
                LGarupa = Math.Round(Convert.ToDecimal(mensuracao.LGarupa), 2).ToString();
                CGarupa = Math.Round(Convert.ToDecimal(mensuracao.CGarupa), 2).ToString();
                CCorporal = Math.Round(Convert.ToDecimal(mensuracao.CCorporal), 2).ToString();
                PToracico = Math.Round(Convert.ToDecimal(mensuracao.PToracico), 2).ToString();

                CaracterizacaoRacial = mensuracao.CaracterizacaoRacial;
                ClassificacaoUbere = mensuracao.ClassificacaoUbere;
                RegimeAlimentar = mensuracao.RegimeAlimentar;

                DataPesagem = mensuracao.DataPesagem.ToShortDateString();

                AddToCurrentPath("<<-Animal", "DetalhesAnimal.aspx?animalId=" + id + "&tabIndex=#mensuracao-tab");

                UpdateTitle();
                DataBind();


            }

        }

        public string Peso
        {
            set { lblPeso.Text = value; }
        }

        public string CEscrotal
        {
            set { lblCEscrotal.Text = value; }
        }

        public string AAnterior
        {
            set { lblAAnterior.Text = value; }
        }

        public string APosterior
        {
            set { lblAPosterior.Text = value; }
        }

        public string LGarupa
        {
            set { lblLGarupa.Text = value; }
        }

        public string CGarupa
        {
            set { lblCGarupa.Text = value; }
        }

        public string CCorporal
        {
            set { lblCCorporal.Text = value; }
        }

        public string PToracico
        {
            set { lblPToracico.Text = value; }
        }

        public string CaracterizacaoRacial
        {
            set { lblCaracterizacaoRacial.Text = value; }
        }

        public string ClassificacaoUbere
        {
            set { lblClassificacaoUbere.Text = value; }
        }

        public string RegimeAlimentar
        {
            set { lblRegimeAlimentar.Text = value; }
        }

        public string DataPesagem
        {
            set { lblDataPesagem.Text = value; }
        }

        protected void EditImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EditaMensuracaoAnimal.aspx?animalId=" + Request.Params["animalId"] + "&m=" + Request.Params["m"]);
        }

        protected void btnAnimal_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalhesAnimal.aspx?animalId=" + id);
        }
    }
}