﻿using System;
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

namespace FCarnauba_Animais
{
    public partial class Animais : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private Animal _animal;
        private List<ResultadoBuscaAnimal> animais;

        public Animais()
        {
            _PageType = new AnimaisType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                if (Session["Pais"] == null)
                {

                    Session["Pais"] = _fCarnaubaFacade.GetPais();

                }


                if (Session["Maes"] == null)
                {
                    Session["Maes"] = _fCarnaubaFacade.GetPais();

                }


                ddlPai.DataSource = Session["Pais"];
                ddlPai.DataValueField = "Id";
                ddlPai.DataTextField = "NomeCompleto";
                ddlPai.DataBind();

                ddlMae.DataSource = Session["Maes"];
                ddlMae.DataValueField = "Id";
                ddlMae.DataTextField = "NomeCompleto";
                ddlMae.DataBind();

                ddlPropriedade.DataSource = _fCarnaubaFacade.GetSimplesPropriedades();
                ddlPropriedade.DataBind();
                ddlPropriedade.Items.Insert(0, new ListItem("", null));
                ddlPropriedade.SelectedIndex = -1;

                ddlRaca.Items.Insert(0, new ListItem("", null));
                ddlRaca.SelectedIndex = -1;

                ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

                if (parametrosDeBusca != null)
                {
                    animais = FCarnaubaFacade.ConsultaAnimal(parametrosDeBusca);

                    Session["var"] = animais;

                    gridViewAnimais.DataSource = animais;
                    gridViewAnimais.DataBind();
                }
                
                AddToCurrentPath("<font color=#156AE9>Animais</font>");
               

            }
            else if (Request["__EVENTTARGET"] == "btnBuscaAvancadaOK_Click") btnBuscaAvancadaOK_Click(null, null);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CheckAllowance();
            

            Session["parametrosDeBuscaEmAnimais"] = null;
                
            AddToCurrentPath("<font color=#156AE9>Animais</font>");

            animais = new List<ResultadoBuscaAnimal>();

            var parametrosDeBusca = new ParametrosDeBuscaEmAnimais { TodosOsCampos = txtBusca.Text };

            animais = FCarnaubaFacade.ConsultaAnimal(parametrosDeBusca);

            Session["parametrosDeBuscaEmAnimais"] = parametrosDeBusca;

            Session["var"] = animais;

            gridViewAnimais.DataSource = animais;
            gridViewAnimais.DataBind();

        }

        protected void btnBuscaAvancadaOK_Click(object sender, EventArgs e)
        {
            CheckAllowance();
            Session["parametrosDeBuscaEmAnimais"] = null;
            AddToCurrentPath("<font color=#156AE9>Animais</font>");

            animais = new List<ResultadoBuscaAnimal>();

            var parametrosDeBusca = new ParametrosDeBuscaEmAnimais();

            parametrosDeBusca.Nome = txtNome.Text;
            parametrosDeBusca.Rgd = txtRgd.Text;
            parametrosDeBusca.Sexo = ddlSexo.SelectedValue;

            if (ddlPai.SelectedValue != "0")
                parametrosDeBusca.StrPaiId = ddlPai.SelectedValue;
            if (ddlMae.SelectedValue != "0")
                parametrosDeBusca.StrMaeId = ddlMae.SelectedValue;
            parametrosDeBusca.Raca = ddlRaca.SelectedValue;


            try
            {
                parametrosDeBusca.DataNascimentoInicial = Convert.ToDateTime(Request.Form["calNascimentoInicial"], CulturaPtBr);
                parametrosDeBusca.DataNascimentoFinal = Convert.ToDateTime(Request.Form["calNascimentoFinal"], CulturaPtBr);
            }
            catch
            {
                parametrosDeBusca.DataNascimentoInicial = null;
                parametrosDeBusca.DataNascimentoFinal = null;
            }

            if (ddlPropriedade.SelectedValue != "")
                parametrosDeBusca.NomeFazenda = ddlPropriedade.SelectedValue;

            if (!String.IsNullOrEmpty(txtOrdem.Text))
            {
                parametrosDeBusca.NumeroOrdem = Convert.ToInt32(txtOrdem.Text);
            }

            if (ddlBetaCaseina.SelectedValue != "")
                parametrosDeBusca.BetaCaseina = ddlBetaCaseina.SelectedValue;

            if (ddlKappaCaseina.SelectedValue != "")
                parametrosDeBusca.KappaCaseina = ddlKappaCaseina.SelectedValue;

            if (ddlFiv.SelectedValue == "Sim")
            {
                parametrosDeBusca.Fiv = true;
            }
            else
            {
                parametrosDeBusca.Fiv = false;
            }

            if (ddlMovimento.SelectedValue != "")
                parametrosDeBusca.Movimento = ddlMovimento.SelectedValue;

            parametrosDeBusca.Observacao = txtObservacao.Text;

            animais = FCarnaubaFacade.ConsultaAnimal(parametrosDeBusca);

            Session["parametrosDeBuscaEmAnimais"] = parametrosDeBusca;

            Session["var"] = animais;

            gridViewAnimais.DataSource = animais;
            gridViewAnimais.DataBind();

        }

        protected void btnDeleteAnimal_Click(object sender, EventArgs e)
        {
            LinkButton btnDelete = (LinkButton)sender;
            int AnimalId = Convert.ToInt32(btnDelete.CommandArgument);

            if (AnimalId > 0)
            {
                {
                    _fCarnaubaFacade.RemoveAnimal(AnimalId.ToString());
                    UpdateGridView();
                }
            }
        }

        private void UpdateGridView()
        {
            CheckAllowance();

            ParametrosDeBuscaEmAnimais parametrosDeBusca = (ParametrosDeBuscaEmAnimais)Session["parametrosDeBuscaEmAnimais"];

            animais = FCarnaubaFacade.ConsultaAnimal(parametrosDeBusca);

            Session["parametrosDeBuscaEmAnimais"] = parametrosDeBusca;

            Session["var"] = animais;

            gridViewAnimais.DataSource = animais;
            gridViewAnimais.DataBind();

        }
        
    }
}