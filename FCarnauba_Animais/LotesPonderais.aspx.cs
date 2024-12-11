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

namespace FCarnauba_Animais
{
    public partial class LotesPonderais : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private List<ResultadoBuscaLotePonderal> lotesPonderais;
        public event Action<int> DeleteLotePonderalClick;
        string raca;

        public LotesPonderais()
        {
            _PageType = new LotesPonderaisType();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAllowance();
            if (!IsPostBackOrCallBack())
            {
                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Id";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.SelectedValue = "8";
                ddlPropriedade.DataBind();

                ddlPropriedade.Items.RemoveAt(0);

                ParametrosDeBuscaEmLotesPonderais parametrosDeBusca = (ParametrosDeBuscaEmLotesPonderais)Session["parametrosDeBuscaEmLotesPonderais"];

                if (parametrosDeBusca != null)
                {
                    if (parametrosDeBusca.Raca == "SINDI")
                    {
                        btnSindi.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    if (parametrosDeBusca.Raca == "GUZERÁ")
                    {
                        btnGuzera.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    if (parametrosDeBusca.Raca == "CURRALEIRO PÉ DURO")
                    {
                        btnCpd.BackColor = Color.FromArgb(5, 43, 92);
                    }

                    lotesPonderais = FCarnaubaFacade.ConsultaLotePonderal(parametrosDeBusca);

                    Session["var"] = lotesPonderais;

                    gridViewLotesPonderais.DataSource = lotesPonderais;
                    gridViewLotesPonderais.DataBind();
                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CheckAllowance();
            

            Session["parametrosDeBuscaEmLotesPonderais"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");
            

            //UpdateTitle();

            lotesPonderais = new List<ResultadoBuscaLotePonderal>();

            Session["raca"] = "";

            string propriedade = ddlPropriedade.SelectedValue;

            var parametrosDeBusca = new ParametrosDeBuscaEmLotesPonderais { TodosOsCampos = txtBusca.Text, IdPropriedade = propriedade };
            //parametrosDeBusca.Raca = (string)Session["raca"];
            

            lotesPonderais = FCarnaubaFacade.ConsultaLotePonderal(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotesPonderais"] = parametrosDeBusca;

            Session["var"] = lotesPonderais;

            gridViewLotesPonderais.DataSource = lotesPonderais;
            gridViewLotesPonderais.DataBind();

        }

        protected void btnGuzera_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmLotesPonderais"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            lotesPonderais = new List<ResultadoBuscaLotePonderal>();

            raca = "GUZERÁ";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmLotesPonderais { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            lotesPonderais = FCarnaubaFacade.ConsultaLotePonderal(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotesPonderais"] = parametrosDeBusca;

            Session["var"] = lotesPonderais;

            gridViewLotesPonderais.DataSource = lotesPonderais;
            gridViewLotesPonderais.DataBind();

            btnGuzera.BackColor = Color.FromArgb(5, 43, 92);
            

        }

        protected void btnSindi_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmLotesPonderais"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            lotesPonderais = new List<ResultadoBuscaLotePonderal>();

            raca = "SINDI";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmLotesPonderais { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            lotesPonderais = FCarnaubaFacade.ConsultaLotePonderal(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotesPonderais"] = parametrosDeBusca;

            Session["var"] = lotesPonderais;

            gridViewLotesPonderais.DataSource = lotesPonderais;
            gridViewLotesPonderais.DataBind();

            btnSindi.BackColor = Color.FromArgb(5, 43, 92);
        }

        protected void btnCpd_Click(object sender, EventArgs e)
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmLotesPonderais"] = null;

            //AddToCurrentPath("<font color=#156AE9>Lotes</font>");

            //UpdateTitle();

            lotesPonderais = new List<ResultadoBuscaLotePonderal>();

            raca = "CURRALEIRO PÉ DURO";
            Session["raca"] = raca;

            var parametrosDeBusca = new ParametrosDeBuscaEmLotesPonderais { Raca = raca };

            parametrosDeBusca.IdPropriedade = ddlPropriedade.SelectedValue;

            lotesPonderais = FCarnaubaFacade.ConsultaLotePonderal(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotesPonderais"] = parametrosDeBusca;

            Session["var"] = lotesPonderais;

            gridViewLotesPonderais.DataSource = lotesPonderais;
            gridViewLotesPonderais.DataBind();

            btnCpd.BackColor = Color.FromArgb(5, 43, 92);
        }

        protected void btnDeleteLotePonderal_Click(object sender, EventArgs e)
        {
           

                LinkButton btnDelete = (LinkButton)sender;
                int LotePonderalId = Convert.ToInt32(btnDelete.CommandArgument);

                //DeleteControleLeiteiroClick(ControleLeiteiroId);

                if (LotePonderalId > 0)
                {
                    {
                        _fCarnaubaFacade.RemoveLotePonderal(LotePonderalId.ToString());
                        //this.UserControlCadastraControleLeiteiro1.DataSource = _fCarnaubaFacade.GetControlesById(loteId.ToString());
                        UpdateGridView();
                        //Response.Redirect("~/CadastrarLote.aspx?act=edit&loteId=" + loteId.ToString() + "&tabIndex=Controle Leiteiro");
                        //ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Leiteiro removido com sucesso");
                    }
                }



            
        }

        private void UpdateGridView()
        {
            CheckAllowance();

            Session["parametrosDeBuscaEmLotesPonderais"] = null;

            lotesPonderais = new List<ResultadoBuscaLotePonderal>();

            raca = Session["raca"].ToString();

            var parametrosDeBusca = new ParametrosDeBuscaEmLotesPonderais { Raca = raca };

            lotesPonderais = FCarnaubaFacade.ConsultaLotePonderal(parametrosDeBusca);

            Session["parametrosDeBuscaEmLotesPonderais"] = parametrosDeBusca;

            Session["var"] = lotesPonderais;

            gridViewLotesPonderais.DataSource = lotesPonderais;
            gridViewLotesPonderais.DataBind();
            
        }
    }
}