using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaArrendamento : PaginaBase
    {
        public int id;
        public int arr_ind;

        public EditaArrendamento()
        {
            _PageType = new EditaArrendamentoType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            arr_ind = Convert.ToInt32(Request.Params["arr"]);
            var arrendamento = FCarnaubaFacade.GetArrendamentoByIndex(id, arr_ind);
            var estruturaPropriedade = FCarnaubaFacade.GetEstruturaPropriedadeById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Propriedades", "DetalhesEstruturaPropriedade.aspx?estruturaPropriedadeId=" + id + "&tabIndex=#arrendamento-tab");

                UpdateTitleEstruturaPropriedade();

                //Arrendamento
                this.UserControlArrendamento1.EstruturaPropriedadeId = id;
                this.UserControlArrendamento1.ArrendamentoInd = arr_ind;
                UserControlArrendamento1.DataSource = estruturaPropriedade.Arrendamentos;
                UserControlArrendamento1.FillAValues(arrendamento);

            }
        }
    }
}