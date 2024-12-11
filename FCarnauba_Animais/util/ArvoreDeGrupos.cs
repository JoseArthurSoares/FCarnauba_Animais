using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.DataSources;
using Infragistics.WebUI.UltraWebNavigator;

namespace FCarnauba_Animais.util
{
    public class ArvoreDeGrupos
    {
        private readonly UltraWebTree controleArvore;

        public ArvoreDeGrupos(UltraWebTree controleArvore)
        {
            this.controleArvore = controleArvore;
        }

        public void MontaArvoreGrupos(GrupoFinanceiro[] grupos)
        {
            Node noderoot = new Node();
            noderoot.Text = "Grupos do Financeiro";
            noderoot.Tag = new GrupoFinanceiro().IdNulo;
            controleArvore.Nodes.Add(noderoot);

            CriaFilhosEm(noderoot, grupos);
        }

        public void CriaFilhosEm(Node nohCorrente, IEnumerable grupos)
        {
            foreach (GrupoFinanceiro grupo in grupos)
            {
                Node node = CriaNoh(grupo);
                if (grupo.IdGrupoPai != 1 || grupo.IdGrupoPai != 8)
                {
                    nohCorrente.Nodes.Add(node);
                }
                else
                {
                    Node nodePai = ObtemNodePai(grupo.IdGrupoPai);
                    nodePai.Nodes.Add(node);
                }
            }
        }

        private static Node CriaNoh(GrupoFinanceiro grupo)
        {
            Node node = new Node();
            node.Text = grupo.Descricao;
            node.Tag = grupo.IdGrupo;
            node.ShowExpand = true;
            return node;
        }

        private Node ObtemNodePai(int idGrupoPai)
        {
            return controleArvore.Find(idGrupoPai);
        }

        //public void AtualizaGrupo(Node node)
        //{
        //    DataSourceGrupos dataSourceGrupos = new DataSourceGrupos();

        //    int id = (int)node.Tag;
        //    string novoNome = node.Text;
        //    int idGrupoPai = (int)node.Parent.Tag;
        //    GrupoProduto grupo = new GrupoProduto();
        //    grupo.IdGrupo = id;
        //    grupo.Descricao = novoNome;
        //    grupo.IdGrupoPai = idGrupoPai;
        //    dataSourceGrupos.AlterarGrupo(grupo);

        //}

        public bool HaAlgumGrupoSelecionado()
        {
            return controleArvore.SelectedNode != null;
        }

        //public void MoveGrupo(WebTreeNodeDroppedEventArgs e)
        //{
        //    Node node = controleArvore.Find(Convert.ToInt32(e.SourceData));
        //    node.Parent.Nodes.Remove(node);
        //    e.Node.Nodes.Add(node);
        //    AtualizaGrupo(node);
        //}

        //public void RemoveGrupoSelecionado()
        //{
        //    Node selectedNode = controleArvore.SelectedNode;
        //    DataSourceGrupos dataSourceGrupos = new DataSourceGrupos();
        //    dataSourceGrupos.RemoverGrupo(Convert.ToInt32(selectedNode.Tag));
        //    controleArvore.SelectedNode.Parent.Nodes.Remove(selectedNode);
        //    selectedNode.Selected = false;
        //}

        //public void Adiciona(GrupoProduto grupo)
        //{
        //    if (controleArvore.SelectedNode != null)
        //    {
        //        grupo.IdGrupoPai = Convert.ToInt32(controleArvore.SelectedNode.Tag);
        //    }
        //    DataSourceGrupos dataSourceGrupos = new DataSourceGrupos();
        //    int idGrupoNovo = dataSourceGrupos.InserirGrupo(grupo);

        //    grupo.IdGrupo = idGrupoNovo;
        //    Node nodeNovo = CriaNoh(grupo);

        //    Node nodePai = ObtemNodePai(grupo.IdGrupoPai);
        //    nodePai.Nodes.Add(nodeNovo);
        //}
    }
}