using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaMatriz : PaginaBase
    {
        public EditaMatriz()
        {
            _PageType = new EditaMatrizType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBackOrCallBack())
            {

                int loteId = Convert.ToInt32(Request.Params["loteId"]);



                //int id = Convert.ToInt32(Request.Params["obraId"]);

                CheckAllowance();
                int mat_ind = Convert.ToInt32(Request.Params["matrizId"]);
                var matriz = FCarnaubaFacade.GetMatrizByIndex(loteId, mat_ind);
                var lote = FCarnaubaFacade.GetLoteById(loteId.ToString());


                //AddToCurrentPath("<font color=#029956>Responsável: </font>");
                //AddToCurrentPath("<font color=#029956>" + responsavel.Nome + "</font>");
                //UpdateTitle();

                //UserControlMatrizes.DataSource = lote.Matrizes;
                //UserControlMatrizes.FillMatValues(matriz);
            }

            
        }
    }
}