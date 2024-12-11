using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ItemFinanceiro
    {
        private long idFinanceiroField;

        private string diretorioField;

        private int idGrupoField;

        private int IdPropriedadeField;

        private int IdEmpresaField;

        private string descricaoField;

        private System.DateTime dataCadastroField;

        private System.DateTime dataUsuarioField;

        private string usuarioField;

        private System.DateTime dataUsuarioValidacaoField;

        private string usuarioValidacaoField;

        private string descricaoGrupoField;

        private string descricaoPropriedadeField;

        private string descricaoEmpresaField;

        private int quantidadeField;

        private double valorUnitarioField;

        private double valorTotalField;

        private double totalPagoField;

        //string por causa do gridview com componente. Analisar depois
        private string dataField;

        private string documentoField;

        private string propriedadeCompField;

        private string formaPagamentoField;

        private int nParcelasField;

        private bool vendaAnimaisField;

        private string vendaAnimaisStrField;

        private List<Compra> _Compras = new List<Compra>();
        private List<Documento> _Documentos = new List<Documento>();
        private List<Parcela> _Parcelas = new List<Parcela>();

        public long IdFinanceiro
        {
            get
            {
                return this.idFinanceiroField;
            }
            set
            {
                this.idFinanceiroField = value;
            }
        }

        public string Diretorio
        {
            get
            {
                return this.diretorioField;
            }
            set
            {
                this.diretorioField = value;
            }
        }

        public int IdGrupo
        {
            get
            {
                return this.idGrupoField;
            }
            set
            {
                this.idGrupoField = value;
            }
        }

        public int IdPropriedade
        {
            get
            {
                return this.IdPropriedadeField;
            }
            set
            {
                this.IdPropriedadeField = value;
            }
        }

        public int IdEmpresa
        {
            get
            {
                return this.IdEmpresaField;
            }
            set
            {
                this.IdEmpresaField = value;
            }
        }

        public string Descricao
        {
            get
            {
                return this.descricaoField;
            }
            set
            {
                this.descricaoField = value;
            }
        }

        public System.DateTime DataCadastro
        {
            get
            {
                return this.dataCadastroField;
            }
            set
            {
                this.dataCadastroField = value;
            }
        }

        public System.DateTime DataUsuario
        {
            get
            {
                return this.dataUsuarioField;
            }
            set
            {
                this.dataUsuarioField = value;
            }
        }

        public string Usuario
        {
            get
            {
                return this.usuarioField;
            }
            set
            {
                this.usuarioField = value;
            }
        }

        public System.DateTime DataUsuarioValidacao
        {
            get
            {
                return this.dataUsuarioValidacaoField;
            }
            set
            {
                this.dataUsuarioValidacaoField = value;
            }
        }

        public string UsuarioValidacao
        {
            get
            {
                return this.usuarioValidacaoField;
            }
            set
            {
                this.usuarioValidacaoField = value;
            }
        }

        public string DescricaoGrupo
        {
            get
            {
                return this.descricaoGrupoField;
            }
            set
            {
                this.descricaoGrupoField = value;
            }
        }

        public string DescricaoPropriedade
        {
            get
            {
                return this.descricaoPropriedadeField;
            }
            set
            {
                this.descricaoPropriedadeField = value;
            }
        }

        public string DescricaoEmpresa
        {
            get
            {
                return this.descricaoEmpresaField;
            }
            set
            {
                this.descricaoEmpresaField = value;
            }
        }

        public int Quantidade
        {
            get
            {
                return this.quantidadeField;
            }
            set
            {
                this.quantidadeField = value;
            }
        }

        public double ValorUnitario
        {
            get
            {
                return this.valorUnitarioField;
            }
            set
            {
                this.valorUnitarioField = value;
            }
        }

        public double ValorTotal
        {
            get
            {
                return this.valorTotalField;
            }
            set
            {
                this.valorTotalField = value;
            }
        }

        public double TotalPago
        {
            get
            {
                return this.totalPagoField;
            }
            set
            {
                this.totalPagoField = value;
            }
        }

        public string Data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }

        public string Documento
        {
            get
            {
                return this.documentoField;
            }
            set
            {
                this.documentoField = value;
            }
        }

        public string PropriedadeComp
        {
            get
            {
                return this.propriedadeCompField;
            }
            set
            {
                this.propriedadeCompField = value;
            }
        }

        public string FormaPagamento
        {
            get
            {
                return this.formaPagamentoField;
            }
            set
            {
                this.formaPagamentoField = value;
            }
        }

        public int NParcelas
        {
            get
            {
                return this.nParcelasField;
            }
            set
            {
                this.nParcelasField = value;
            }
        }

        public bool VendaAnimais
        {
            get
            {
                return this.vendaAnimaisField;
            }
            set
            {
                this.vendaAnimaisField = value;
            }
        }

        public string VendaAnimaisStr
        {
            get
            {
                return this.vendaAnimaisStrField;
            }
            set
            {
                this.vendaAnimaisStrField = value;
            }
        }

        public List<Compra> Compras
        {
            get { return _Compras; }
            set { _Compras = value; }
        }

        public List<Documento> Documentos
        {
            get { return _Documentos; }
            set { _Documentos = value; }
        }

        public List<Parcela> Parcelas
        {
            get { return _Parcelas; }
            set { _Parcelas = value; }
        }
    }
}
