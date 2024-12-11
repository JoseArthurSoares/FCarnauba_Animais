using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using System.Data;
using System.Text;
using FCarnauba_Animais.UserControls;

namespace FCarnauba_Animais
{
    public partial class SimuladorEndogamia : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        private Animal _animal;
        private List<Noh> _ListPais2;
        private List<Noh> _ListMaes2;
        private List<Noh> _ListPaisFinal;
        private List<Noh> _ListMaesFinal;
        public Animal animal;
        public Animal paiDdl;
        public Animal maeDdl;
        public double Endogamia;
        public Boolean AncestralComum;

        public Mensagem Mensagem
        {
            get { return mensagem; }
        }

        public void ExibeMensagem(TipoDeMensagem tipo, string textoDaMensagem)
        {
            Mensagem.ExibeMensagem(tipo, textoDaMensagem);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBackOrCallBack())
            {
                ChangeDllRacas();
            }
        }

        protected void ddlRacas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeDllRacas();
        }

        protected void btnSimular_Click(object sender, EventArgs e)
        {
            if (ddlPais.SelectedValue == "0" || ddlMaes.SelectedValue == "0" || String.IsNullOrEmpty(txtNome.Text))
            {
                ExibeMensagem(TipoDeMensagem.Aviso, "Embrião, pai e mãe são obrigatórios!");
                return;
            }

            List<Noh> arvore = new List<Noh>();
            List<Noh> ancestraisComuns = new List<Noh>();
            animal = new Animal();
            animal.NomeCompleto = txtNome.Text;

            paiDdl = _fCarnaubaFacade.GetAnimalById(ddlPais.SelectedValue);
            List<Noh> _ListPais2 = new List<Noh>();
            Noh nohPai = new Noh();
            nohPai.StrId = paiDdl.Id.ToString();
            nohPai.CaminhoPai = nohPai.StrId;
            nohPai.IdPai = _fCarnaubaFacade.GetIdPai(nohPai.StrId);
            nohPai.IdMae = _fCarnaubaFacade.GetIdMae(nohPai.StrId);
            _ListPais2.Add(nohPai);

            maeDdl = _fCarnaubaFacade.GetAnimalById(ddlMaes.SelectedValue);
            List<Noh> _ListMaes2 = new List<Noh>();
            Noh nohMae = new Noh();
            nohMae.StrId = maeDdl.Id.ToString();
            nohMae.CaminhoPai = nohMae.StrId;
            nohMae.IdPai = _fCarnaubaFacade.GetIdPai(nohMae.StrId);
            nohMae.IdMae = _fCarnaubaFacade.GetIdMae(nohMae.StrId);
            _ListMaes2.Add(nohMae);

            int no = 6;

            if (ddlNivel.SelectedValue == "3")
                no = 2;
            if (ddlNivel.SelectedValue == "4")
                no = 6;
            if (ddlNivel.SelectedValue == "5")
                no = 14;

            AncestralComum = false;
            bool teveAncestralComum = false;
            string conc = "";
            string s = "";
            DataTable table = new DataTable();
            DataTable tree = new DataTable();
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("parent", typeof(string));

            animal.StrPaiId = nohPai.StrId;
            animal.StrMaeId = nohMae.StrId;

            string idPai = "0";
            string idMae = "0";

            if (!String.IsNullOrEmpty(animal.StrPaiId))
            {
                idPai = animal.StrPaiId;
            }

            if (!String.IsNullOrEmpty(animal.StrMaeId))
            {
                idMae = animal.StrMaeId;
            }

            var anceM = _fCarnaubaFacade.GetAnimalById(idPai);
            var anceF = _fCarnaubaFacade.GetAnimalById(idMae);

            table.Rows.Add(animal.Id, "");


            table.Rows.Add(anceF.Id, animal.Id);
            table.Rows.Add(anceM.Id, animal.Id);


            for (int a = 1; a < table.Rows.Count; a++)
            {
                try
                {
                    if (a > no)
                        break;

                    var ance = _fCarnaubaFacade.GetAnimalById(table.Rows[a][0].ToString());


                    if (ance.StrMaeId != null)
                    {
                        table.Rows.Add(ance.StrMaeId, ance.Id);
                    }
                    else
                    {
                        table.Rows.Add(0, 0);
                    }

                    if (ance.StrPaiId != null)
                    {
                        table.Rows.Add(ance.StrPaiId, ance.Id);
                    }
                    else
                    {
                        table.Rows.Add(0, 0);
                    }


                }
                catch (Exception)
                {
                    break;
                }
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Animal paiObj = new Animal();
                Animal filhoObj = new Animal();

                if (i == 0)
                {
                    paiObj.Id = 99999;
                    paiObj.NomeRg = txtNome.Text;

                    filhoObj.Id = 0;
                    filhoObj.NomeRg = null;
                }
                else
                {
                    paiObj = _fCarnaubaFacade.GetAnimalById(table.Rows[i][0].ToString());
                    filhoObj = _fCarnaubaFacade.GetAnimalById(table.Rows[i][1].ToString());
                }

                if (i == 1 || i == 2)
                {
                    filhoObj.Id = 99999;
                    filhoObj.NomeRg = txtNome.Text;
                }

                var pai = "<a target=_blank href=RelatorioIndividual.aspx?id=" + paiObj.Id + ">" + paiObj.NomeRg + "</a>";
                var filho = "<a target=_blank href=RelatorioIndividual.aspx?id=" + filhoObj.Id + ">" + filhoObj.NomeRg + "</a>";

                if (filhoObj.NomeRg == null)
                    filho = null;

                if (!String.IsNullOrEmpty(pai))
                    pai = pai.Replace("-", "<br>");

                if (!String.IsNullOrEmpty(filho))
                    filho = filho.Replace("-", "<br>");

                if (paiObj.Id != 0)
                {

                    if (!s.Contains("['" + pai))
                    {
                        teveAncestralComum = false;
                        s = s + "['" + pai + "','" + filho + "'],";
                        conc = conc + pai + filho + " ";

                        Noh noh = new Noh();
                        noh.Id = paiObj.Id;
                        if (filhoObj.Id != null)
                        {
                            if (arvore.Count() > 0)
                            {
                                var nohFilho = arvore.Single(ar => ar.Id == filhoObj.Id);
                                if (!String.IsNullOrEmpty(nohFilho.CaminhoAcumulado))
                                {
                                    noh.CaminhoAcumulado = nohFilho.CaminhoAcumulado + " " + paiObj.Id;
                                }
                                else
                                {
                                    noh.CaminhoAcumulado = paiObj.Id.ToString();
                                }
                            }
                        }

                        arvore.Add(noh);

                    }
                    else
                    {
                        if (!conc.Contains(pai + filho))
                        {
                            Noh noh = new Noh();
                            noh.Id = paiObj.Id;
                            if (filhoObj.Id != null)
                            {
                                if (arvore.Count() > 0)
                                {
                                    var nohAncestralComum = arvore.Single(anc => anc.Id == filhoObj.Id);


                                    var nohAncestralComumExist = arvore.Single(ance => ance.Id == paiObj.Id);

                                    Noh ancestralComum = new Noh();
                                    ancestralComum.Id = nohAncestralComumExist.Id;

                                    if (!String.IsNullOrEmpty(nohAncestralComumExist.CaminhoAcumulado))
                                    {
                                        ancestralComum.CaminhoAcumulado = nohAncestralComumExist.CaminhoAcumulado + " " + nohAncestralComum.CaminhoAcumulado;
                                    }
                                    else
                                    {
                                        ancestralComum.CaminhoAcumulado = nohAncestralComum.CaminhoAcumulado;
                                    }

                                    string elementosComuns = "";
                                    string[] ids = ancestralComum.CaminhoAcumulado.Split(' ');
                                    var collectionWithDistinctElements = ids.Distinct().ToArray();

                                    for (int x = 0; x < collectionWithDistinctElements.Length; x++)
                                    {
                                        if (!String.IsNullOrEmpty(elementosComuns))
                                        {
                                            elementosComuns = elementosComuns + " " + collectionWithDistinctElements[x];
                                        }
                                        else
                                        {
                                            elementosComuns = collectionWithDistinctElements[x];
                                        }
                                    }

                                    ancestralComum.CaminhoAcumulado = elementosComuns;

                                    if (ancestralComum.CaminhoAcumulado.Contains(anceM.Id.ToString()) && ancestralComum.CaminhoAcumulado.Contains(anceF.Id.ToString()))
                                    {
                                        teveAncestralComum = true;
                                        ancestraisComuns.Add(ancestralComum);
                                    }
                                }
                            }

                            if (teveAncestralComum)
                            {
                                s = s + "[{ v: '" + pai + i.ToString() + "', f:'" + pai + "<div style=color:red;>ANCESTRAL COMUM</div>" + "' },'" + filho + "'],";
                            }
                            else
                            {
                                s = s + "[{ v: '" + pai + i.ToString() + "', f:'" + pai + "<div style=color:red;>ANCESTRAL COMUM DESC.</div>" + "' },'" + filho + "'],";
                            }

                            conc = conc + pai + filho + " ";
                            AncestralComum = true;

                        }
                    }
                }

            }

            //Cálculo Endogamia
            Endogamia = CalculaEndogamia(ancestraisComuns);

            s = s.TrimEnd(',');

            String csname1 = "PopupScript";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                StringBuilder cstext1 = new StringBuilder();
                cstext1.Append("<script>");
                cstext1.Append("google.setOnLoadCallback(drawChart);");
                cstext1.Append("function drawChart() {");
                cstext1.Append("var data = new google.visualization.DataTable();");
                cstext1.Append("data.addColumn('string', 'Name'); data.addColumn('string', 'parent');");
                cstext1.Append("data.addRows([" + s + "]);");
                cstext1.Append("data.setRowProperty(0,'style', 'background-color: #3388ff');");
                cstext1.Append("var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));");
                cstext1.Append("chart.draw(data, { allowHtml: true, nodeClass: 'myNodeClass', size: 'small' });");
                cstext1.Append("}");

                cstext1.Append("</script>");

                cs.RegisterStartupScript(cstype, csname1, cstext1.ToString());
            }

            pnlEndogamia.Visible = true;
        }

        public void ChangeDllRacas()
        {
            this.ddlPais.DataSource = _fCarnaubaFacade.GetPais(ddlRacas.SelectedValue);
            ddlPais.DataValueField = "Id";
            ddlPais.DataTextField = "NomeCompleto";
            ddlPais.DataBind();

            this.ddlMaes.DataSource = _fCarnaubaFacade.GetMaes(ddlRacas.SelectedValue);
            ddlMaes.DataValueField = "Id";
            ddlMaes.DataTextField = "NomeCompleto";
            ddlMaes.DataBind();

            pnlEndogamia.Visible = false;
        }

        public double CalculaEndogamia(List<Noh> AncestraisComuns)
        {
            double endogamia = 0;
            foreach (Noh nohCalculo in AncestraisComuns)
            {
                String[] strlist = nohCalculo.CaminhoAcumulado.Split(' ');
                int n = strlist.Length;
                endogamia = endogamia + Math.Pow(0.5, n);
            }

            return endogamia;
        }

        protected void btnPesquisarDDlPai_Click(object send, EventArgs e)
        {
            string pesquisa = txtPesquisaPai.Text;
            if (String.IsNullOrEmpty(pesquisa))
                pesquisa = "*";

            this.ddlPais.DataSource = _fCarnaubaFacade.GetPais(ddlRacas.SelectedValue, pesquisa);
            ddlPais.DataValueField = "Id";
            ddlPais.DataTextField = "NomeCompleto";
            ddlPais.DataBind();
        }

        protected void btnPesquisarDDlMae_Click(object send, EventArgs e)
        {
            string pesquisa = txtPesquisaMae.Text;
            if (String.IsNullOrEmpty(pesquisa))
                pesquisa = "*";

            this.ddlMaes.DataSource = _fCarnaubaFacade.GetMaes(ddlRacas.SelectedValue, pesquisa);
            ddlMaes.DataValueField = "Id";
            ddlMaes.DataTextField = "NomeCompleto";
            ddlMaes.DataBind();
        }
    }
}