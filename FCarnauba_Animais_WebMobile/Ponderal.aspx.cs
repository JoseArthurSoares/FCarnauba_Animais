using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais_WebMobile.util;
using LightInfocon.GoldenAccess.General;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;
using Image = iTextSharp.text.Image;
using ListItem = System.Web.UI.WebControls.ListItem;
using FCarnauba_Animais_WebMobile.DataSources;

namespace FCarnauba_Animais_WebMobile
{
    public partial class Ponderal : PaginaBaseMobile
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private bool requestedValidation = false;
        public int lotePonderalId = 0;
        private int totalMensuracoes = 0;
        public Mensuracao mensuracao = null;
        private string lotePonderalID;
        string lastMensuracao = null;

        public Ponderal()
        {
            _PageType = new PonderalType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlLoteData.DataSource = _fCarnaubaFacade.GetLotesPonderaisParaMensuracoes();
                ddlLoteData.DataValueField = "Id";
                ddlLoteData.DataTextField = "LoteDataPropriedade";
                ddlLoteData.DataBind();

                lotePonderalID = Request.Params["lotePonderalId"];
                
                if (!String.IsNullOrEmpty(lotePonderalID))
                {
                    lotePonderalId = Convert.ToInt32(lotePonderalID);
                    ddlLoteData.SelectedValue = lotePonderalID;
                    AtualizaGrid(Convert.ToInt32(lotePonderalID));
                }

                if (lotePonderalId != 0)
                {
                    btnEncerrar.Visible = true;
                    //btnInicio.Visible = false;
                }
                else
                {
                    btnEncerrar.Visible = false;
                    //btnInicio.Visible = true;
                }
            }

        }

        protected void ddlLoteData_SelectedIndexChanged(object sender, EventArgs e)
        {
            lotePonderalId = Convert.ToInt32(ddlLoteData.SelectedValue);
            AtualizaGrid(lotePonderalId);

            if (lotePonderalId != 0)
            {
                btnEncerrar.Visible = true;
                //btnInicio.Visible = false;
            }
            else
            {
                btnEncerrar.Visible = false;
                //btnInicio.Visible = true;
            }
        }

        protected void btnEncerrar_Click(object sender, EventArgs e)
        {
            lotePonderalId = Convert.ToInt32(ddlLoteData.SelectedValue);
            _fCarnaubaFacade.EncerrarMensuracoes(lotePonderalId);
            Response.Redirect("Principal.aspx");
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        private void AtualizaGrid(int lotePonderalId)
        {
            var mensuracoes = _fCarnaubaFacade.GetMensuracao(lotePonderalId);
            totalMensuracoes = mensuracoes.Count;
            gridViewMensuracoes.DataSource = mensuracoes;
            gridViewMensuracoes.DataBind();
        }
    }
}