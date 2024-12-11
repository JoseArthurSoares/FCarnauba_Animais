using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class DetalhesMensuracao : PaginaBase
    {
        public Mensuracao mensuracao;
        public int id;
        private int m_ind;

        public DetalhesMensuracao()
        {
            _PageType = new DetalhesMensuracaoType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["lotePonderalId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            m_ind = Convert.ToInt32(Request.Params["m"]);

            mensuracao = FCarnaubaFacade.GetMensuracaoByIndex(id, m_ind);

            CheckAllowance();

            if (!IsPostBackOrCallBack())
            {
                Animal = mensuracao.NomeAnimal;
                Peso = Math.Round(Convert.ToDecimal(mensuracao.Peso), 2).ToString();
                PesoMaeDesmame = Math.Round(Convert.ToDecimal(mensuracao.PesoMaeDesmame), 2).ToString();
                CEscrotal = Math.Round(Convert.ToDecimal(mensuracao.CEscrotal), 2).ToString();
                AAnterior = Math.Round(Convert.ToDecimal(mensuracao.AAnterior), 2).ToString();
                APosterior = Math.Round(Convert.ToDecimal(mensuracao.APosterior), 2).ToString();
                LGarupa = Math.Round(Convert.ToDecimal(mensuracao.LGarupa), 2).ToString();
                CGarupa = Math.Round(Convert.ToDecimal(mensuracao.CGarupa), 2).ToString();
                CCorporal = Math.Round(Convert.ToDecimal(mensuracao.CCorporal), 2).ToString();
                PToracico = Math.Round(Convert.ToDecimal(mensuracao.PToracico), 2).ToString();


                if (mensuracao.SairControle)
                {
                    SairControle = "Sim";
                }
                else
                {
                    SairControle = "Não";
                }

                CaracterizacaoRacial = mensuracao.CaracterizacaoRacial;
                ClassificacaoUbere = mensuracao.ClassificacaoUbere;

                RegimeAlimentar = mensuracao.RegimeAlimentar;

                DataSaidaControle = mensuracao.DataSaidaControle.HasValue
                                           ? mensuracao.DataSaidaControle.Value.ToShortDateString()
                                           : "";
                Motivo = mensuracao.Motivo;

                AddToCurrentPath("<<-Lote Ponderal", "DetalhesLotePonderal.aspx?lotePonderalId=" + id + "&tabIndex=#mensuracao-tab");

                UpdateTitleControlePonderal();
                DataBind();


            }

        }

        public string Animal
        {
            set { lblAnimal.Text = value; }
        }

        public string Peso
        {
            set { lblPeso.Text = value; }
        }

        public string PesoMaeDesmame
        {
            set { lblPesoMaeDesmame.Text = value; }
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

        public string DataSaidaControle
        {
            set { lblDataSaidaControle.Text = value; }
        }

        public string SairControle
        {
            set { lblSairControle.Text = value; }
        }

        public string Motivo
        {
            set { lblMotivo.Text = value; }
        }

        protected void EditImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EditaMensuracao.aspx?lotePonderalId=" + Request.Params["lotePonderalId"] + "&m=" + Request.Params["m"]);
        }

        protected void btnControlePonderal_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalhesControlePonderal.aspx?lotePonderalId=" + id);
        }


    }
}