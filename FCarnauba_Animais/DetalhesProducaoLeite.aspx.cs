using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class DetalhesProducaoLeite : PaginaBase
    {
        public ProducaoLeite producao;
        public int id;
        private int pl_ind;

        public DetalhesProducaoLeite()
        {
            _PageType = new DetalhesProducaoLeiteType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["loteId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            pl_ind = Convert.ToInt32(Request.Params["pl"]);

            producao = FCarnaubaFacade.GetProducaoLeiteByIndex(id, pl_ind);

            CheckAllowance();

            if (!IsPostBackOrCallBack())
            {
                Matriz = producao.NomeMatriz;
                Cria = producao.NomeCria;
                Lactacao = producao.DiasLactacao.ToString();
                POrdenha = Math.Round(Convert.ToDecimal(producao.POrdenha), 2).ToString();
                SOrdenha = Math.Round(Convert.ToDecimal(producao.SOrdenha), 2).ToString();
                TOrdenha = Math.Round(Convert.ToDecimal(producao.TOrdenha), 2).ToString();
                Total = Math.Round(Convert.ToDecimal(producao.Total), 2).ToString();
                GordPOrdenha = Math.Round(Convert.ToDecimal(producao.GordPOrdenha), 2).ToString();
                GordSOrdenha = Math.Round(Convert.ToDecimal(producao.GordSOrdenha), 2).ToString();
                GordTOrdenha = Math.Round(Convert.ToDecimal(producao.GordTOrdenha), 2).ToString();
                ProtPOrdenha = Math.Round(Convert.ToDecimal(producao.ProtPOrdenha), 2).ToString();
                ProtSOrdenha = Math.Round(Convert.ToDecimal(producao.ProtSOrdenha), 2).ToString();
                ProtTOrdenha = Math.Round(Convert.ToDecimal(producao.ProtTOrdenha), 2).ToString();
                if (producao.BezerrosPe)
                {
                    BezerrosPe = "Sim";
                }
                else
                {
                    BezerrosPe = "Não";
                }

                if (producao.SairControle)
                {
                    SairControle = "Sim";
                }
                else
                {
                    SairControle = "Não";
                }

                TetosFuncionais = producao.TetosFuncionais.ToString();
                Observacoes = producao.Obs;
                RegimeAlimentar = producao.RegimeAlimentar;
                if (producao.Receptora)
                {
                    Receptora = "Sim";
                }
                else
                {
                    Receptora = "Não";
                }
                DataSaidaControle = producao.DataSaidaControle.HasValue
                                           ? producao.DataSaidaControle.Value.ToShortDateString()
                                           : "";
                Motivo = producao.Motivo;

                AddToCurrentPath("<<-Lote", "DetalhesLote.aspx?loteId=" + id + "&tabIndex=#producaoleite-tab");

                UpdateTitleControleLeiteiro();
                DataBind();


            }

        }

        public string Matriz
        {
            set { lblMatriz.Text = value; }
        }

        public string Cria
        {
            set { lblCria.Text = value; }
        }

        public string Lactacao
        {
            set { lblLactacao.Text = value; }
        }

        public string POrdenha
        {
            set { lblPOrdenha.Text = value; }
        }

        public string SOrdenha
        {
            set { lblSOrdenha.Text = value; }
        }

        public string TOrdenha
        {
            set { lblTOrdenha.Text = value; }
        }

        public string Total
        {
            set { lblTotal.Text = value; }
        }

        public string GordPOrdenha
        {
            set { lblGordPOrdenha.Text = value; }
        }

        public string GordSOrdenha
        {
            set { lblGordSOrdenha.Text = value; }
        }

        public string GordTOrdenha
        {
            set { lblGordTOrdenha.Text = value; }
        }

        public string ProtPOrdenha
        {
            set { lblProtPOrdenha.Text = value; }
        }

        public string ProtSOrdenha
        {
            set { lblProtSOrdenha.Text = value; }
        }

        public string ProtTOrdenha
        {
            set { lblProtTOrdenha.Text = value; }
        }

        public string BezerrosPe
        {
            set { lblBezerrosPe.Text = value; }
        }

        public string TetosFuncionais
        {
            set { lblTetosFuncionais.Text = value; }
        }

        public string Observacoes
        {
            set { lblObservacoes.Text = value; }
        }

        public string RegimeAlimentar
        {
            set { lblRegimeAlimentar.Text = value; }
        }

        public string Receptora
        {
            set { lblReceptora.Text = value; }
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
            Response.Redirect("EditaProducaoLeite.aspx?loteId=" + Request.Params["loteId"] + "&pl=" + Request.Params["pl"]);
        }

        protected void btnControleLeiteiro_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalhesControleLeiteiro.aspx?loteId=" + id);
        }
    }
}