using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.util;
using FCarnauba_Animais.UserControls;
using Microsoft.Reporting.WebForms;

namespace FCarnauba_Animais
{
    public partial class CadastrarControlePluviometrico : PaginaBase
    {
        private string act = null;
        public string msg = null;
        public string ControlePluviometricoId;
        public long ultimoId = 0;
        public int controlePluviometricoId = 0;
        private bool requestedValidation = false;

        public CadastrarControlePluviometrico()
        {
            _PageType = new CadastrarControlePluviometricoType();
        }

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        public bool EditMode
        {
            get
            {
                return (act == "edit" || act != null);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AddToCurrentPath("<font color=#156AE9> <<-Pluviometria</font>", "Pluviometria.aspx");
            UpdateTitleControlePluviometrico();
            controlePluviometricoId = Convert.ToInt32(Request.QueryString["controlePluviometricoId"]);
            ControlePluviometricoId = Request.QueryString["ControlePluviometricoId"];
            int ano = 0;
            string propriedade = "";

            act = Request.QueryString["act"];

            if (!IsPostBackOrCallBack())
            {
                reportViewer.Visible = false;
                ControlePluviometricoId = Request.QueryString["ControlePluviometricoId"];
                CheckAllowance();

                controlePluviometricoId = Convert.ToInt32(Request.Params["controlePluviometricoId"]);
                var controlePluviometrico = FCarnaubaFacade.GetControlePluviometricoById(controlePluviometricoId.ToString());
                
                ddlPropriedade.DataSource = FCarnaubaFacade.GetPropriedades();
                ddlPropriedade.DataValueField = "Nome";
                ddlPropriedade.DataTextField = "Nome";
                ddlPropriedade.DataBind();
                ddlPropriedade.Items.RemoveAt(0);

                if (act != null)
                {
                    CheckAllowance();
                    FillControlePluviometricoFields(controlePluviometrico);
                }
                else
                {
                    if ((!String.IsNullOrEmpty((string)Session["propriedade"])) && (!String.IsNullOrEmpty((string)Session["ano"])))
                    {
                        ddlPropriedade.SelectedValue = (string)Session["propriedade"];
                        ddlPropriedade.Enabled = false;
                        propriedade = (string)Session["propriedade"];
                        ano = Convert.ToInt32(Session["ano"]);
                        ExibeRelatorio("Relatorios/PluviometriaCad.rdlc", "RPluviometria", _fCarnaubaFacade.GetPluviometrias(ano, propriedade));
                        reportViewer.Visible = true;
                    }
                }
            }
        }


        private void FillControlePluviometricoFields(ControlePluviometrico controlePluviometrico)
        {
            ddlPropriedade.SelectedValue = controlePluviometrico.Diretorio;
            txtData.Text = Convert.ToString(controlePluviometrico.Data);
            ddlPluviometro.SelectedValue = controlePluviometrico.Pluviometro;
            txtPluviometria.Text = Convert.ToString(controlePluviometrico.Pluviometria);
        }

        protected void cvData_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dateValue;

            if (DateTime.TryParse(txtData.Text, out dateValue))
            {
                try
                {
                    args.IsValid = true;
                    return;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
            args.IsValid = false;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvPluviometria_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtPluviometria.Text))
                txtPluviometria.Text = "0";

            args.IsValid = true;
            if (!requestedValidation) args.IsValid = true;
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            ControlePluviometricoId = Request.QueryString["ControlePluviometricoId"];
            requestedValidation = true;
            Page.Validate();
            if (IsValid)
            {

                ControlePluviometrico controlePluviometrico = new ControlePluviometrico();

                controlePluviometrico.Diretorio = ddlPropriedade.SelectedValue;
                controlePluviometrico.Data = Convert.ToDateTime(txtData.Text);
                controlePluviometrico.Pluviometro = ddlPluviometro.SelectedValue;
                controlePluviometrico.Pluviometria = Convert.ToDouble(txtPluviometria.Text);
                controlePluviometrico.DataUsuario = DateTime.Today;


                if (EditMode)
                {
                    var controlePluviometricoCorr = FCarnaubaFacade.GetControlePluviometricoById(ControlePluviometricoId);

                    if (FCarnaubaFacade.PluviometriaExists(controlePluviometrico.Diretorio, controlePluviometrico.Data, controlePluviometrico.Pluviometro) && (controlePluviometricoCorr.Data != controlePluviometrico.Data || controlePluviometricoCorr.Diretorio != controlePluviometrico.Diretorio || controlePluviometricoCorr.Pluviometro != controlePluviometrico.Pluviometro))
                    {
                        ExibeMensagem(TipoDeMensagem.Aviso, "Data do controle já existente");
                        return;
                    }

                    controlePluviometrico.Usuario = UsuarioLogado.Name + " (editado)";
                    controlePluviometrico.Id = Convert.ToInt32(ControlePluviometricoId);
                    FCarnaubaFacade.SalvaControlePluviometrico(controlePluviometrico);
                    //ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Pluviometrico salvo com sucesso");
                    Response.Redirect("Pluviometria.aspx?op=edit");

                }
                else
                {

                    if (FCarnaubaFacade.PluviometriaExists(controlePluviometrico.Diretorio, controlePluviometrico.Data, controlePluviometrico.Pluviometro))
                    {
                        ExibeMensagem(TipoDeMensagem.Aviso, "Data do controle já existente");
                        return;
                    }

                    controlePluviometrico.Usuario = UsuarioLogado.Name;
                    FCarnaubaFacade.AdicionaControlePluviometrico(controlePluviometrico);
                    //ExibeMensagem(TipoDeMensagem.Sucesso, "Controle Pluviometrico adicionado com sucesso");
                    Response.Redirect("Pluviometria.aspx?op=add");

                }
            }
        }

        protected void txtData_TextChanged(object sender, EventArgs e)
        {
            DateTime dateValue;

            if (DateTime.TryParse(txtData.Text, out dateValue) && txtData.Text != "__/__/____")
            {
                DateTime data = Convert.ToDateTime(txtData.Text);
                int ano = data.Year;
                string propriedade = ddlPropriedade.SelectedValue;
                reportViewer.Visible = true;

                ExibeRelatorio("Relatorios/PluviometriaCad.rdlc", "RPluviometria", _fCarnaubaFacade.GetPluviometrias(ano, propriedade));
            }
        }

        protected void ddlPropriedade_SelectedIndexChanged(object sender, EventArgs e)
        {
            reportViewer.Visible = false;
        }

        private void ExibeRelatorio(string caminhoRelatorio, string dataSourceName, object dataSource)
        {
            reportViewer.Reset();

            reportViewer.LocalReport.ReportPath = caminhoRelatorio;


            ReportDataSource reportDataSource = new ReportDataSource(dataSourceName, dataSource);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
        }
    }
}