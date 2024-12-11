using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaProducaoLeite : PaginaBase
    {
        //private string act = null;
        public int id;
        public int pl_ind;

        public EditaProducaoLeite()
        {
            _PageType = new EditaProducaoLeiteType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //act = Request.QueryString["act"];
            id = Convert.ToInt32(Request.Params["loteId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            pl_ind = Convert.ToInt32(Request.Params["pl"]);
            var producao = FCarnaubaFacade.GetProducaoLeiteByIndex(id, pl_ind);
            var lote = FCarnaubaFacade.GetLoteById(id.ToString());
            //var controleLeiteiro = FCarnaubaFacade.GetControleLeiteiroById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                AddToCurrentPath("<<-Lote", "DetalhesLote.aspx?loteId=" + id + "&tabIndex=#producaoleite-tab");

                UpdateTitleControleLeiteiro();

                //Produção de leite
                this.UserControlProducaoLeite1.LoteId = id;
                this.UserControlProducaoLeite1.ProducaoInd = pl_ind;
                UserControlProducaoLeite1.DataSource = lote.ProducoesLeite;
                UserControlProducaoLeite1.FillPLValues(producao);
                //UserControlProducaoLeite1.LoadDropDowns(lote.Raca);

                //if (act == "new")
                //{
                //    this.UserControlProducaoLeite1.AddMode = true;
                //}

                //DataBind();
                
            }
        }
    }
}