using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.util;
using LightInfocon.GoldenAccess.General;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;
using Image = iTextSharp.text.Image;
using ListItem = System.Web.UI.WebControls.ListItem;
using Microsoft.Reporting.WebForms;

namespace FCarnauba_Animais
{
    public partial class EstruturasPropriedades : PaginaBase
    {
        public string op = null;
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private List<ResultadoBuscaEstruturaPropriedade> estruturas;
        public event Action<int> DeleteEstruturaPropriedadeClick;
        string propriedade;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                //ParametrosDeBuscaEmEstruturaPropriedades parametrosDeBusca = (ParametrosDeBuscaEmEstruturaPropriedades)Session["parametrosDeBuscaEmEstruturaPropriedades"];

                var parametrosDeBusca = new ParametrosDeBuscaEmEstruturaPropriedades { TodosOsCampos = "*" };

                if (parametrosDeBusca != null)
                {
                    estruturas = FCarnaubaFacade.ConsultaEstruturaPropriedade(parametrosDeBusca);

                    Session["var"] = estruturas;

                    gridViewEstruturasPropriedades.DataSource = estruturas;
                    gridViewEstruturasPropriedades.DataBind();
                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmEstruturaPropriedades"] = null;

            estruturas = new List<ResultadoBuscaEstruturaPropriedade>();

            var parametrosDeBusca = new ParametrosDeBuscaEmEstruturaPropriedades { TodosOsCampos = txtBusca.Text };

            parametrosDeBusca.IdPropriedade = propriedade;


            estruturas = FCarnaubaFacade.ConsultaEstruturaPropriedade(parametrosDeBusca);

            Session["parametrosDeBuscaEmEstruturaPropriedades"] = parametrosDeBusca;

            Session["var"] = estruturas;

            gridViewEstruturasPropriedades.DataSource = estruturas;
            gridViewEstruturasPropriedades.DataBind();
        }

        protected void btnDeleteEstruturaPropriedade_Click(object sender, EventArgs e)
        {
            LinkButton btnDelete = (LinkButton)sender;
            int EstruturaPropriedadeId = Convert.ToInt32(btnDelete.CommandArgument);

            if (EstruturaPropriedadeId > 0)
            {
                {
                    //_fCarnaubaFacade.RemoveEstruturaPropriedade(EstruturaPropriedadeId.ToString());
                    //UpdateGridView();
                }
            }
        }

        private void UpdateGridView()
        {
            CheckAllowance();

            ParametrosDeBuscaEmEstruturaPropriedades parametrosDeBusca = (ParametrosDeBuscaEmEstruturaPropriedades)Session["parametrosDeBuscaEmEstruturaPropriedades"];

            estruturas = FCarnaubaFacade.ConsultaEstruturaPropriedade(parametrosDeBusca);

            Session["parametrosDeBuscaEmEstruturaPropriedades"] = parametrosDeBusca;

            Session["var"] = estruturas;

            gridViewEstruturasPropriedades.DataSource = estruturas;
            gridViewEstruturasPropriedades.DataBind();

        }
    }
}