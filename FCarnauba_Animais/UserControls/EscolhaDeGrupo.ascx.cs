using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.DataSources;
using FCarnauba_Animais.util;
using Infragistics.WebUI.UltraWebNavigator;

namespace FCarnauba_Animais.UserControls
{
    public partial class EscolhaDeGrupo : System.Web.UI.UserControl
    {
        public event EventHandler<SelecaoDeGrupoEventArgs> GrupoSelecionado;

        protected void OnGrupoSelecionado(object sender, SelecaoDeGrupoEventArgs e)
        {
            if (GrupoSelecionado != null)
            {
                GrupoSelecionado(sender, e);
            }
        }

        public object IdGrupoSelecionado
        {
            get
            {
                if (uwtGrupos.SelectedNode == null)
                {
                    return null;
                }
                return uwtGrupos.SelectedNode.Tag;
            }
            set
            {
                DataSourceGrupos dataSourceGrupos = new DataSourceGrupos();
                GrupoFinanceiro[] grupoEHierarquia = dataSourceGrupos.ObtemGrupo(Convert.ToInt32(value), true);

                ArvoreDeGrupos arvore = new ArvoreDeGrupos(uwtGrupos);
                arvore.MontaArvoreGrupos(grupoEHierarquia);

                uwtGrupos.SelectedNode = uwtGrupos.Find(value);
            }
        }

        protected void lbtBuscar_Click(object sender, EventArgs e)
        {
            DataSourceGrupos dataSourceGrupos = new DataSourceGrupos();

            GrupoFinanceiro[] grupos;
            if (txtBuscaGrupo.Text.Trim().Length == 0)
            {
                grupos = dataSourceGrupos.ObtemFilhosDe(new GrupoFinanceiro().IdNulo);
            }
            else
            {
                grupos = dataSourceGrupos.ObtemGrupos(txtBuscaGrupo.Text.Trim());
            }

            uwtGrupos.ClearAll();

            ArvoreDeGrupos arvore = new ArvoreDeGrupos(uwtGrupos);
            arvore.MontaArvoreGrupos(grupos);

            uwtGrupos.ExpandAll();
            uwtGrupos.SelectedNode = null;
        }

        protected void uwtGrupos_NodeExpanded(object sender, WebTreeNodeEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                return;
            }

            DataSourceGrupos dataSource = new DataSourceGrupos();

            GrupoFinanceiro[] grupos = dataSource.ObtemFilhosDe(Convert.ToInt32(e.Node.Tag));
            ArvoreDeGrupos arvore = new ArvoreDeGrupos(uwtGrupos);
            arvore.CriaFilhosEm(e.Node, grupos);
        }

        protected void uwtGrupos_NodeSelectionChanged(object sender, WebTreeNodeEventArgs e)
        {
            SelecaoDeGrupoEventArgs evt =
                new SelecaoDeGrupoEventArgs(
                    Convert.ToInt32(e.Node.Tag),
                    e.Node.Text);
            OnGrupoSelecionado(this, evt);
        }
    }
}