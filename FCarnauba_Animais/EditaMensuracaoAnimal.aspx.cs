using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaMensuracaoAnimal : PaginaBase
    {
        public int id;
        public int m_ind;

        public EditaMensuracaoAnimal()
        {
            _PageType = new EditaMensuracaoAnimalType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["animalId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            m_ind = Convert.ToInt32(Request.Params["m"]);
            var mensuracao = FCarnaubaFacade.GetMensuracaoAnimalByIndex(id, m_ind);
            var animal = FCarnaubaFacade.GetAnimalByIdCompleto(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Animais", "DetalhesAnimal.aspx?animalId=" + id + "&tabIndex=#mensuracao-tab");

                UpdateTitle();

                //Historico
                this.UserControlMensuracaoAnimal1.AnimalId = id;
                this.UserControlMensuracaoAnimal1.MensuracaoInd = m_ind;
                UserControlMensuracaoAnimal1.DataSource = animal.Mensuracoes;
                UserControlMensuracaoAnimal1.FillMValues(mensuracao);
            }
        }
    }
}