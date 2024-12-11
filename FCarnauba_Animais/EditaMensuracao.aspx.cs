using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaMensuracao : PaginaBase
    {

        public int id;
        public int m_ind;

        public EditaMensuracao()
        {
            _PageType = new EditaMensuracaoType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //act = Request.QueryString["act"];
            id = Convert.ToInt32(Request.Params["lotePonderalId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            m_ind = Convert.ToInt32(Request.Params["m"]);
            var mensuracao = FCarnaubaFacade.GetMensuracaoByIndex(id, m_ind);
            var lotePonderal = FCarnaubaFacade.GetLotePonderalById(id.ToString());
            //var controleLeiteiro = FCarnaubaFacade.GetControleLeiteiroById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Lote Ponderal", "DetalhesLotePonderal.aspx?lotePonderalId=" + id + "&tabIndex=#mensuracao-tab");

                UpdateTitleControlePonderal();

                //Mensuracao
                this.UserControlMensuracao1.LotePonderalId = id;
                this.UserControlMensuracao1.MensuracaoInd = m_ind;
                UserControlMensuracao1.DataSource = lotePonderal.Mensuracoes;
                UserControlMensuracao1.FillMValues(mensuracao);
                UserControlMensuracao1.LoadDropDowns(lotePonderal.Raca);

                //if (act == "new")
                //{
                //    this.UserControlProducaoLeite1.AddMode = true;
                //}

                //DataBind();

            }
        }
    }
}