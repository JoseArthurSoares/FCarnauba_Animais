using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaCdcMatriz : PaginaBase
    {
         public int id;
        public int m_ind;

        public EditaCdcMatriz()
        {
            _PageType = new EditaCdcMatrizType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //act = Request.QueryString["act"];
            id = Convert.ToInt32(Request.Params["cdcId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            m_ind = Convert.ToInt32(Request.Params["m"]);
            var matriz = FCarnaubaFacade.GetCdcMatrizByIndex(id, m_ind);
            var cdc = FCarnaubaFacade.GetCdcById(id.ToString());
            //var controleLeiteiro = FCarnaubaFacade.GetControleLeiteiroById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Cruzamentos", "DetalhesCdc.aspx?cdcId=" + id + "&tabIndex=#cdcmatrizes-tab");

                UpdateTitleCdc();

                //Matriz
                this.UserControlCdcMatrizes1.CdcId = id;
                this.UserControlCdcMatrizes1.CdcMatrizInd = m_ind;
                UserControlCdcMatrizes1.DataSource = cdc.Matrizes;
                UserControlCdcMatrizes1.FillMValues(matriz);
                UserControlCdcMatrizes1.LoadDropDowns();

                //if (act == "new")
                //{
                //    this.UserControlProducaoLeite1.AddMode = true;
                //}

                //DataBind();

            }
        }
    }
}