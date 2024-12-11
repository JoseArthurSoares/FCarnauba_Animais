using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaOutra : PaginaBase
    {
        public int id;
        public int o_ind;

        public EditaOutra()
        {
            _PageType = new EditaOutraType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            o_ind = Convert.ToInt32(Request.Params["o"]);
            var outra = FCarnaubaFacade.GetOutraByIndex(id, o_ind);
            var estruturaPropriedade = FCarnaubaFacade.GetEstruturaPropriedadeById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Propriedades", "DetalhesEstruturaPropriedade.aspx?estruturaPropriedadeId=" + id + "&tabIndex=#outra-tab");

                UpdateTitleEstruturaPropriedade();

                //Outras
                this.UserControlOutra1.EstruturaPropriedadeId = id;
                this.UserControlOutra1.OutraInd = o_ind;
                UserControlOutra1.DataSource = estruturaPropriedade.Outras;
                UserControlOutra1.FillAValues(outra);

            }
        }
    }
}