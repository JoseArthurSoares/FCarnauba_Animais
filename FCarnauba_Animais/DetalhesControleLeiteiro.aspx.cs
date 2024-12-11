using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;


namespace FCarnauba_Animais
{
    public partial class DetalhesControleLeiteiro : PaginaBase
    {
        private ControleLeiteiro controleLeiteiro;
        public int id = 0;
        public int idLote = 0;
        public string idstr = null;
        public string dimFinal = "none";

        public DetalhesControleLeiteiro()
        {
            _PageType = new DetalhesControleLeiteiroType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["controleLeiteiroId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);
            //id = Convert.ToInt32(Request.Params["obraId"]);
            idstr = id.ToString();
            //TargObra = id;
            CheckAllowance();

            controleLeiteiro = FCarnaubaFacade.GetControleLeiteiroById(id.ToString());
            if (!String.IsNullOrEmpty(controleLeiteiro.IdLote))
            {
                idLote = Convert.ToInt32(controleLeiteiro.IdLote);
            }

            if (!IsPostBackOrCallBack())
            {
                
                //this.UserControlProducaoLeite1.ControleLeiteiroId = id;

                AddToCurrentPath("<<-Controle Leiteiro", "DetalhesLote.aspx?loteId=" + controleLeiteiro.IdLote + "&tabIndex=Controle Leiteiro");
                UpdateTitleControleLeiteiro();

                lblCategoria.Text = controleLeiteiro.Categoria;

                var dataControle = controleLeiteiro.DataControle.ToString();
                var dataProximaVisita = controleLeiteiro.DataProximaVisita.ToString();

                if (!String.IsNullOrEmpty(dataControle))
                {
                    lblDataControle.Text = Convert.ToDateTime(dataControle).ToString("dd/MM/yyyy");
                }
                else
                {
                    lblDataControle.Text = "";
                }

                if (!String.IsNullOrEmpty(dataProximaVisita))
                {
                    lblDataproximaVisita.Text = Convert.ToDateTime(dataProximaVisita).ToString("dd/MM/yyyy");
                }
                else
                {
                    lblDataproximaVisita.Text = "";
                }

                lblPOrdenha.Text = controleLeiteiro.POrdenha;
                lblSOrdenha.Text = controleLeiteiro.SOrdenha;
                lblTOrdenha.Text = controleLeiteiro.TOrdenha;
                lblControlador.Text = controleLeiteiro.Controlador;
                
                this.UserControlProducaoLeite1.DataSource = controleLeiteiro.ProducoesLeite;

                // Avaliar se precisa
                //ObjectDataSource1.SelectParameters.Clear();
                //ObjectDataSource1.SelectParameters.Add(new Parameter("obraId", DbType.String, idstr));
                //ReportGraficoCronograma.LocalReport.Refresh();

            }
        }
    }
}