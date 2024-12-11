using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais
{
    public partial class DetalhesCdcMatriz : PaginaBase
    {
        public Matriz matriz;
        public int id;
        private int m_ind;

        public DetalhesCdcMatriz()
        {
            _PageType = new DetalhesCdcMatrizType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.Params["cdcId"]);
            if (id == 0)
                id = Convert.ToInt32(Request.Params["id"]);

            m_ind = Convert.ToInt32(Request.Params["m"]);

            matriz = FCarnaubaFacade.GetCdcMatrizByIndex(id, m_ind);

            CheckAllowance();

            if (!IsPostBackOrCallBack())
            {
                Matriz = matriz.NomeMatriz;

                Cio = matriz.CioRepeticao.ToString();

                if (matriz.CdcEfetiva)
                {
                    CdcEfetiva = "Sim";
                }
                else
                {
                    CdcEfetiva = "Não";
                }

                AddToCurrentPath("<<-Cruzamentos", "DetalhesCdc.aspx?cdcId=" + id + "&tabIndex=#cdcmatrizes-tab");

                UpdateTitleCdc();
                DataBind();
            }
        }

        public string Matriz
        {
            set { lblMatriz.Text = value; }
        }

        public string Cio
        {
            set { lblCio.Text = value; }
        }

        public string CdcEfetiva
        {
            set { lblCoberturaEfetiva.Text = value; }
        }


        protected void EditImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EditaCdcMatriz.aspx?cdcId=" + Request.Params["cdcId"] + "&m=" + Request.Params["m"]);
        }

        protected void btnControleLeiteiro_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalhesControleLeiteiro.aspx?loteId=" + id);
        }
    }
}