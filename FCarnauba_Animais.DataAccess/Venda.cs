using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Venda
    {
        private long idVendaField;

        private int IdAnimalField;

        private string descricaoAnimalField;

        private System.DateTime dataField;

        private double valorField;

        private string formaPagamentoField;

        private long nParcelasField;

        private List<Parcela> _Parcelas = new List<Parcela>();

        private List<Documento> _Documentos = new List<Documento>();

        public long IdVenda
        {
            get
            {
                return this.idVendaField;
            }
            set
            {
                this.idVendaField = value;
            }
        }

        public int IdAnimal
        {
            get
            {
                return this.IdAnimalField;
            }
            set
            {
                this.IdAnimalField = value;
            }
        }

        public string DescricaoAninal
        {
            get
            {
                return this.descricaoAnimalField;
            }
            set
            {
                this.descricaoAnimalField = value;
            }
        }

        public System.DateTime Data
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

        public double Valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
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

        public long NParcelas
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

        public List<Parcela> Parcelas
        {
            get { return _Parcelas; }
            set { _Parcelas = value; }
        }

        public List<Documento> Documentos
        {
            get { return _Documentos; }
            set { _Documentos = value; }
        }
    }
}
