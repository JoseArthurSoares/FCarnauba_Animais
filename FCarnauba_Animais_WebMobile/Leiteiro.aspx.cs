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
    public partial class Leiteiro : PaginaBaseMobile
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public string SLote;
        public double Total;
        public Lote lote;
        private int loteId = 0;
        private int totalPesagensLeite = 0;

        public Leiteiro()
        {
            _PageType = new LeiteiroType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlLoteData.DataSource = _fCarnaubaFacade.GetLotesParaPesagens();
                ddlLoteData.DataValueField = "Id";
                ddlLoteData.DataTextField = "LoteDataPropriedade";
                ddlLoteData.DataBind();

            }
            else
            {
                loteId = Convert.ToInt32(ddlLoteData.SelectedValue);
            }

            lote = _fCarnaubaFacade.GetLoteById(LoteId);

            if (LoteId != "0")
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

        protected void ddlLoteData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loteId = Convert.ToInt32(ddlLoteData.SelectedValue);
            SLote = loteId.ToString();
            var producoes = _fCarnaubaFacade.GetProducaoLeite(loteId);
            totalPesagensLeite = producoes.Count;
            gvPesagensLeite.DataBind();

            lote = _fCarnaubaFacade.GetLoteById(LoteId);
        }

        public string LoteId
        {
            get
            {
                return loteId.ToString();
            }
        }

        protected void gvPesagensLeite_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            
        }

        protected void gvPesagensLeite_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
        }

        protected void gvPesagensLeite_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        protected void pesagensLeiteDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ExibeMensagemDeStatus("Pesagem modificada com sucesso", "Ocorreu um erro ao tentar modificar o item", e);
        }

        protected void pesagensLeiteDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            
        }

        protected void gvPesagensLeite_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProducaoLeite producaoLeite = (ProducaoLeite)e.Row.DataItem;

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    Label lblBezerrosPe = (Label)e.Row.FindControl("lblBezerrosPe");

                    if (lblBezerrosPe.Text == "True")
                    {
                        lblBezerrosPe.Text = "S";
                    }
                    else
                    {
                        lblBezerrosPe.Text = "N";
                    }
                }

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {

                    ITextControl txtEditPOrdenha = (ITextControl)e.Row.FindControl("txtEditPOrdenha");
                    txtEditPOrdenha.Text = String.Format("{0:N2}", Convert.ToDouble(txtEditPOrdenha.Text));
                    txtEditPOrdenha.Text = txtEditPOrdenha.Text.Replace(".", "");

                    ITextControl txtEditSOrdenha = (ITextControl)e.Row.FindControl("txtEditSOrdenha");
                    txtEditSOrdenha.Text = String.Format("{0:N2}", Convert.ToDouble(txtEditSOrdenha.Text));
                    txtEditSOrdenha.Text = txtEditSOrdenha.Text.Replace(".", "");

                    DropDownList ddlEditTetosFuncionais = (DropDownList)e.Row.FindControl("ddlEditTetosFuncionais");
                    ddlEditTetosFuncionais.DataBind();
                    ddlEditTetosFuncionais.SelectedValue = producaoLeite.TetosFuncionais.ToString();

                    e.Row.BackColor = System.Drawing.Color.Bisque;

                }
            }
        }

        private static ProducaoLeite GetPesagemLeite(string pesagemLoteId)
        {
            string[] ids = pesagemLoteId.Split(' ');
            int loteId = Convert.ToInt32(ids[0]);
            int pesagemId = Convert.ToInt32(ids[1]);

            DataSourcePesagensLeite dataSource = new DataSourcePesagensLeite();
            return dataSource.ObtemPesagemLeite(loteId, pesagemId);
        }

        protected void ibtNewPesagemLeite_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        private void AdicionaItemPesagemLeite(Control controlContainer)
        {
        }

        protected void pesagensLeiteDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            CriterioPesquisaPesagensLeite parametrosDeBusca = new CriterioPesquisaPesagensLeite { IdLote = Convert.ToInt64(LoteId) };
            e.InputParameters.Add("criterio", parametrosDeBusca);
        }

        protected void gvPesagensLeite_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ITextControl editPOrdenha = (ITextControl)gvPesagensLeite.Rows[e.RowIndex].FindControl("txtEditPOrdenha");
            string pOrdenha = editPOrdenha.Text;
            string pOrdenha2 = pOrdenha.Replace(",", ".");
            e.NewValues["POrdenha"] = pOrdenha2;

            ITextControl editSOrdenha = (ITextControl)gvPesagensLeite.Rows[e.RowIndex].FindControl("txtEditSOrdenha");
            string sOrdenha = editSOrdenha.Text;
            string sOrdenha2 = sOrdenha.Replace(",", ".");
            e.NewValues["SOrdenha"] = sOrdenha2;

            ListControl ddlEditTetosFuncionais =
                (ListControl)gvPesagensLeite.Rows[e.RowIndex].FindControl("ddlEditTetosFuncionais");

            e.NewValues["TetosFuncionais"] = ddlEditTetosFuncionais.SelectedValue;
        }

        protected void ExibeMensagemDeStatus(string mensagemSucesso, string mensagemErro, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //Log(e.Exception);
                //ExibeMensagem(TipoDeMensagem.Erro, mensagemErro);
                e.ExceptionHandled = true;
                return;
            }
            //ExibeMensagem(TipoDeMensagem.Sucesso, mensagemSucesso);
        }

        protected void btnEncerrar_Click(object sender, EventArgs e)
        {
            _fCarnaubaFacade.EncerrarPesagensLeite(Convert.ToInt64(LoteId));
            Response.Redirect("Principal.aspx");
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

    }
}