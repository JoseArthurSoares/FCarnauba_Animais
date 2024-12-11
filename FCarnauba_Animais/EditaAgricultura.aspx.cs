using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaAgricultura : PaginaBase
    {
        public int id;
        public int a_ind;

        public EditaAgricultura()
        {
            _PageType = new EditaAgriculturaType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            a_ind = Convert.ToInt32(Request.Params["a"]);
            var agricultura = FCarnaubaFacade.GetAgriculturaByIndex(id, a_ind);
            var estruturaPropriedade = FCarnaubaFacade.GetEstruturaPropriedadeById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Propriedades", "DetalhesEstruturaPropriedade.aspx?estruturaPropriedadeId=" + id + "&tabIndex=#agricultura-tab");

                UpdateTitleEstruturaPropriedade();

                //Agricultura
                this.UserControlAgricultura1.EstruturaPropriedadeId = id;
                this.UserControlAgricultura1.AgriculturaInd = a_ind;
                UserControlAgricultura1.DataSource = estruturaPropriedade.Agriculturas;
                UserControlAgricultura1.FillAValues(agricultura);

            }
        }
    }
}