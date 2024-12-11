using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.UserControls
{
    public partial class UserControlMatrizes : UserControlBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public int loteId;
        public int matInd;
        public bool lastMatDead = false;
        private bool requestedValidation = false;

        protected Mensagem Mensagem
        {
            get { return mensagem; }
        }

        protected void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);

        }

        public bool ReadOnly
        {
            get
            {
                if (ViewState["ReadOnly"] == null)
                {
                    return false;
                }
                return (bool)ViewState["ReadOnly"];
            }
            set { ViewState["ReadOnly"] = value; }
        }

        public bool EditMode
        {
            get
            {
                if (ViewState["EditMode"] == null)
                {
                    return false;
                }
                return (bool)ViewState["EditMode"];
            }
            set { ViewState["EditMode"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            loteId = Convert.ToInt32(Request.Params["loteId"]);
            
            if (!this.Page.IsPostBack && !this.Page.IsCallback)
            {
                if (gridViewMatrizes.Rows.Count > 0)
                {
                    gridViewMatrizes.Visible = true;
                    lblMensagemMatrizes.Visible = false;
                }
                else
                {
                    gridViewMatrizes.Visible = false;
                    lblMensagemMatrizes.Visible = true;
                }

                if (EditMode)
                {
                    matInd = Convert.ToInt32(Request.Params["matrizId"]);
                    var matriz = _fCarnaubaFacade.GetMatrizByIndex(loteId, matInd);
                    FillMatValues(matriz);

                }


                this.ddlMatriz.DataSource = _fCarnaubaFacade.GetMaes();
                ddlMatriz.DataValueField = "Id";
                ddlMatriz.DataTextField = "NomeCompleto";
                ddlMatriz.DataBind();


                
                UpdateGridView(loteId);
                if (ReadOnly)
                {
                    this.pnlFields.Visible = false;
                }
            }
        }

        protected void ddlMatriz_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idMatriz = ddlMatriz.SelectedValue;
            ddlCria.DataSource = _fCarnaubaFacade.GetCriasControleLeiteiro(idMatriz);
            ddlCria.DataValueField = "Id";
            ddlCria.DataTextField = "NomeCompleto";
            ddlCria.DataBind();

            //Redirect depos aqui
            //Response.Redirect("EditaControleLeiteiro.aspx?controleLeiteiroId=" + ControleLeiteiroId + "&&tabIndex=#producaoleite-tab");
        }

        protected void ddCria_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEntradaControle.Text = "";
        }

        private void ShowMessage(string msg, bool isGood = true)
        {
            var page = HttpContext.Current.Handler as PaginaBase;
            page.ShowMessageControleLeiteiro(msg, isGood);
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            requestedValidation = true;
            Page.Validate();
            if (!Page.IsValid)
            {
                //ShowMessage("Complete os campos requeridos corretamente.", false);
                ExibeMensagem(TipoDeMensagem.Aviso, "Complete os campos requeridos corretamente.");
                //return;
            }

            else
            {

                loteId = Convert.ToInt32(Request.Params["loteId"]);
                matInd = Convert.ToInt32(Request.Params["matrizId"]);

                Matriz matriz = new Matriz();
                matriz.IdMatriz = ddlMatriz.SelectedValue;
                matriz.IdCria = ddlCria.SelectedValue;
                if (!String.IsNullOrEmpty(txtEntradaControle.Text))
                {
                    matriz.DataEntradaControle = Convert.ToDateTime(txtEntradaControle.Text);
                }
                else
                {
                    if (matriz.IdCria != "0")
                    {
                        DateTime dataNascimentoCria = _fCarnaubaFacade.GetDataNascimentoCria(matriz.IdCria);
                        //matriz.DataEntradaControle = dataNascimentoCria.AddDays(7);
                        matriz.DataEntradaControle = dataNascimentoCria;
                    }
                    else
                    {
                        matriz.DataEntradaControle = null;
                    }
                }
                if (!String.IsNullOrEmpty(txtSaidaControle.Text))
                {
                    matriz.DataSaidaControle = Convert.ToDateTime(txtSaidaControle.Text);
                }
                matriz.EmControleLeiteiro = ckControleLeiteiro.Checked;

                if (EditMode)
                {
                    _fCarnaubaFacade.SalvaMatriz(loteId, matInd, matriz);
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Matriz salva com sucesso");
                }
                else
                {
                    _fCarnaubaFacade.AdicionaMatriz(loteId, matriz);
                    ExibeMensagem(TipoDeMensagem.Sucesso, "Matriz adicionada com sucesso");
                }

                UpdateGridView(loteId);
                FillMatValues(matriz);
                gridViewMatrizes.Visible = true;
                lblMensagemMatrizes.Visible = false;

            }

            //Response.Redirect("~/CadastrarLote.aspx?act=new&loteId=" + loteId + "&tabIndex=Matrizes");
        }

        public void FillMatValues(Matriz matriz)
        {
            this.ddlMatriz.DataSource = _fCarnaubaFacade.GetMaes();
            ddlMatriz.DataValueField = "Id";
            ddlMatriz.DataTextField = "NomeCompleto";
            ddlMatriz.SelectedValue = matriz.IdMatriz;
            ddlMatriz.DataBind();

            ddlCria.DataSource = _fCarnaubaFacade.GetCriasControleLeiteiro(matriz.IdMatriz);
            ddlCria.DataValueField = "Id";
            ddlCria.DataTextField = "NomeCompleto";
            ddlCria.SelectedValue = matriz.IdCria;
            ddlCria.DataBind();


            txtEntradaControle.Text = matriz.DataEntradaControle.ToString();
            txtSaidaControle.Text = matriz.DataSaidaControle.ToString();
            ddlMatriz.SelectedValue = matriz.IdMatriz;
            ddlCria.SelectedValue = matriz.IdCria;
            ckControleLeiteiro.Checked = matriz.EmControleLeiteiro;
        }

        protected void cvEntradaControle_ServerValidate(object source, ServerValidateEventArgs args)
        {

            var criaTemp = ddlCria.SelectedValue;
            
            if (txtEntradaControle.Text != "")
            {
                if (IsValidDate(txtEntradaControle.Text))
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                if (criaTemp != "" && criaTemp != "0")
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            if (!requestedValidation) args.IsValid = true;
        }

        protected void cvSaidaControle_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (txtSaidaControle.Text != "")
            {
                if (IsValidDate(txtSaidaControle.Text))
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = true;
            }

            if (!requestedValidation) args.IsValid = true;
        }

        protected void btnDeleteMatriz_Click(object sender, EventArgs e)
        {
            
            LinkButton btn = (LinkButton)sender;
            int responsavelId = Convert.ToInt32(btn.CommandArgument);
            _fCarnaubaFacade.RemoveMatriz(loteId, responsavelId);
            UpdateGridView(loteId);
            Response.Redirect("~/CadastrarLote.aspx?loteId=" + loteId + "&act=new&tabIndex=Matrizes");
            
        }

        private void UpdateGridView(int loteId)
        {
            var mats = _fCarnaubaFacade.GetMatrizes(loteId);
            
            //mats.Sort((mat1, mat2) => mat1.Deleted.CompareTo(mat2.Deleted));
            //mats.Reverse();
            

            this.gridViewMatrizes.DataSource = mats;
            this.gridViewMatrizes.DataBind();
        }

        public List<Matriz> DataSource
        {
            set
            {
                this.gridViewMatrizes.DataSource = value;
                this.gridViewMatrizes.DataBind();
            }
        }

        protected void gridViewMatrizes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    int i = 0;
            //    foreach (DataControlField column in gridViewResponsaveis.Columns)
            //    {

            //        if (UsuarioLogado.HasGroup("GESTOR"))
            //        {
            //            e.Row.FindControl("btnEditaResponsavel").Visible = false;
            //            e.Row.FindControl("btnDeleteResponsavel").Visible = false;
            //        }

            //        i++;
            //    }

            //    //string key = gvObras.DataKeys[e.Row.RowIndex].Value.ToString();

            //    e.Row.Attributes.Add("id", "tr_contr_" + e.Row.RowIndex);
            //    e.Row.Attributes.Add("onmouseover",
            //                         "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#D7E9FD';this.style.cursor='pointer'");
            //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            //    e.Row.Attributes.Add("onclick", String.Format("location.href='DetalhesResponsavel.aspx?obraId={0}&responsavelId={1}';", obraId, e.Row.RowIndex));
            //    var resp = (Responsavel)e.Row.DataItem;
            //    if (resp.Deleted)
            //    {
            //        e.Row.Attributes.Add("style", "background-color:#BCC2C1");
            //    }
            //    else
            //    {
            //        if (lastRespDead == false)
            //        {
            //            e.Row.Attributes.Add("style", "border-top:4px solid black");
            //        }
            //    }
            //    lastRespDead = resp.Deleted;
            //}
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DetalhesLote.aspx?loteId="+loteId);
        }

        private bool IsValidDate(string txt)
        {
            try
            {
                Convert.ToDateTime(txt);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}