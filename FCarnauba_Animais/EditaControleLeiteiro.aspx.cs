using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class EditaControleLeiteiro : PaginaBase
    {

        public int id;
        public ControleLeiteiro controleLeiteiro;

        public EditaControleLeiteiro()
        {
            _PageType = new EditaControleLeiteiroType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["controleLeiteiroId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            CheckAllowance();

            controleLeiteiro = FCarnaubaFacade.GetControleLeiteiroById(id.ToString());

            if (!IsPostBackOrCallBack())
            {
                //Produção de leite
                //this.UserControlProducaoLeite1.ControleLeiteiroId = id;

                AddToCurrentPath("<<-Controle Leiteiro", "DetalhesLote.aspx?loteId=" + controleLeiteiro.IdLote + "#controleleiteiro-tab");
                UpdateTitleControleLeiteiro();

                CadastraControleLeiteiroUserControl1.LoadDropDowns();
                CadastraControleLeiteiroUserControl1.FillControleLeiteiroValues(controleLeiteiro);

                //Produção de Leite
                //UserControlProducaoLeite1.ControleLeiteiroId = id;
                //UserControlProducaoLeite1.DataSource = controleLeiteiro.ProducoesLeite;
                //UserControlProducaoLeite1.AddMode = true;
                //DataBind();
                //UserControlProducaoLeite1.LoadDropDowns();

                //UserControlFisicoFinanceiro1.DataSource = obra.FisicoFinanceiro;
                //DataBind();
                //UserControlFisicoFinanceiro1.LoadDropDowns(obra.RegistroCGE);

            }

            if (Request.Params["tabIndex"] != null)
            {
                var tabIndex = Request.Params["tabIndex"];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RemovEdits",
                                                    String.Format("setActive('{0}');", tabIndex), true);
            }
            else
            {
                UserControlProducaoLeite1.EditMode = false;
                UserControlProducaoLeite1.ReadOnly = true;
            }
            var unicode = new UnicodeEncoding();
            Form.Action = unicode.GetString(unicode.GetBytes(Request.Url.ToString()));
        }

        protected void btnLote_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalhesLote.aspx?loteId=" + controleLeiteiro.IdLote);
        }
    }
}