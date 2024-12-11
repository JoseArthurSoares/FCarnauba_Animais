using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaPastagem : PaginaBase
    {
        public int id;
        public int p_ind;

        public EditaPastagem()
        {
            _PageType = new EditaPastagemType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["estruturaPropriedadeId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            p_ind = Convert.ToInt32(Request.Params["p"]);
            var pastagem = FCarnaubaFacade.GetPastagemByIndex(id, p_ind);
            var estruturaPropriedade = FCarnaubaFacade.GetEstruturaPropriedadeById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Propriedades", "DetalhesEstruturaPropriedade.aspx?estruturaPropriedadeId=" + id + "&tabIndex=#pastagem-tab");

                UpdateTitleEstruturaPropriedade();

                //Pastagem
                this.UserControlPastagem1.EstruturaPropriedadeId = id;
                this.UserControlPastagem1.PastagemInd = p_ind;
                UserControlPastagem1.DataSource = estruturaPropriedade.Pastagens;
                UserControlPastagem1.FillPValues(pastagem);
                //UserControlPastagem1.LoadDropDowns(lotePonderal.Raca);


            }
        }
    }
}