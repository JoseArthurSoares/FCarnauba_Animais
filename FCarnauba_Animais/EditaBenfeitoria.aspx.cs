using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaBenfeitoria : PaginaBase
    {
        public int id;
        public int b_ind;

        public EditaBenfeitoria()
        {
            _PageType = new EditaBenfeitoriaType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            b_ind = Convert.ToInt32(Request.Params["b"]);
            var benfeitoria = FCarnaubaFacade.GetBenfeitoriaByIndex(id, b_ind);
            var estruturaPropriedade = FCarnaubaFacade.GetEstruturaPropriedadeById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Propriedades", "DetalhesEstruturaPropriedade.aspx?estruturaPropriedadeId=" + id + "&tabIndex=#benfeitoria-tab");

                UpdateTitleEstruturaPropriedade();

                //Benfeitoria
                this.UserControlBenfeitoria1.EstruturaPropriedadeId = id;
                this.UserControlBenfeitoria1.BenfeitoriaInd = b_ind;
                UserControlBenfeitoria1.DataSource = estruturaPropriedade.Benfeitorias;
                UserControlBenfeitoria1.FillAValues(benfeitoria);

            }
        }
    }
}