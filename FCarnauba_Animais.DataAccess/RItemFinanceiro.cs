using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RItemFinanceiro
    {
        long _Id;
        string _Descricao;
        int _Quantidade;
        double _ValorUnitario;
        double _ValorTotal;
        string _NomePropriedade;
        string _Periodo;
        string _Cliente;
        DateTime _Data;
        string _FormaPagamento;

        public long Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        public string Descricao
        {
            get
            {
                return this._Descricao;
            }
            set
            {
                this._Descricao = value;
            }
        }

        public int Quantidade
        {
            get
            {
                return this._Quantidade;
            }
            set
            {
                this._Quantidade = value;
            }
        }

        public double ValorUnitario
        {
            get
            {
                return this._ValorUnitario;
            }
            set
            {
                this._ValorUnitario = value;
            }
        }

        public double ValorTotal
        {
            get
            {
                return this._ValorTotal;
            }
            set
            {
                this._ValorTotal = value;
            }
        }

        public string NomePropriedade
        {
            get
            {
                return this._NomePropriedade;
            }
            set
            {
                this._NomePropriedade = value;
            }
        }

        public string Periodo
        {
            get
            {
                return this._Periodo;
            }
            set
            {
                this._Periodo = value;
            }
        }

        public string Cliente
        {
            get
            {
                return this._Cliente;
            }
            set
            {
                this._Cliente = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                this._Data = value;
            }
        }

        public string FormaPagamento
        {
            get
            {
                return this._FormaPagamento;
            }
            set
            {
                this._FormaPagamento = value;
            }
        }

    }
}
