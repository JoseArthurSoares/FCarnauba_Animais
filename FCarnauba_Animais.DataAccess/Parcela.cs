using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FCarnauba_Animais.DataAccess
{
    public class Parcela
    {
        int _Id;
        int _NParcela;
        //string por causa do gridview com componente. Analisar depois
        string _Data;
        double _ValorInicial;
        double _ValorPago;
        string _DataPagamento;
        DateTime _DataPagamentoDt;
        int _FinanceiroId;
        string _ParcelaFinanceiroId;

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public int NParcela
        {
            get
            {
                return _NParcela;
            }
            set
            {
                _NParcela = value;
            }
        }

        public string Data
        {
            get
            {
                return _Data;
            }
            set
            {
                _Data = value;
            }
        }

        public double ValorInicial
        {
            get
            {
                return _ValorInicial;
            }
            set
            {
                _ValorInicial = value;
            }
        }

        public double ValorPago
        {
            get
            {
                return _ValorPago;
            }
            set
            {
                _ValorPago = value;
            }
        }

        public string DataPagamento
        {
            get
            {
                return _DataPagamento;
            }
            set
            {
                _DataPagamento = value;
            }
        }

        public DateTime DataPagamentoDt
        {
            get
            {
                return _DataPagamentoDt;
            }
            set
            {
                _DataPagamentoDt = value;
            }
        }

        public int FinanceiroId
        {
            get
            {
                return _FinanceiroId;
            }
            set
            {
                _FinanceiroId = value;
            }
        }

        public string ParcelaFinanceiroId
        {
            get
            {
                return _ParcelaFinanceiroId;
            }
            set
            {
                _ParcelaFinanceiroId = value;
            }
        }

    }
}
