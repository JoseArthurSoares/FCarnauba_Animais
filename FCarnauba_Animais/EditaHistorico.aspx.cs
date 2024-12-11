using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaHistorico : PaginaBase
    {
        public int id;
        public int h_ind;

        public EditaHistorico()
        {
            _PageType = new EditaHistoricoType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["animalId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            h_ind = Convert.ToInt32(Request.Params["h"]);
            var historico = FCarnaubaFacade.GetHistoricoByIndex(id, h_ind);
            var animal = FCarnaubaFacade.GetAnimalByIdCompleto(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Animais", "DetalhesAnimal.aspx?animalId=" + id + "&tabIndex=#historico-tab");

                UpdateTitle();

                //Historico
                this.UserControlHistorico1.AnimalId = id;
                this.UserControlHistorico1.HistoricoInd = h_ind;
                UserControlHistorico1.DataSource = animal.Historicos;
                UserControlHistorico1.FillHValues(historico);
                //UserControlPastagem1.LoadDropDowns(lotePonderal.Raca);
            }
        }
    }
}