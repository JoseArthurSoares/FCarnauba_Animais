using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RAnimaisLactacaoAno
    {
        long _Id;
        string _Nome;
        string _NomeCompleto;
        string _Rgd;
        DateTime _DataControle;
        int _Ano;
        string _Raca;
        int _DiasLactacao;
        double _POrdenha;
        double _Total;
        bool _SairControle;
        string _SairControleStr;

        public long Id
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

        public string Nome
        {
            get
            {
                return _Nome;
            }
            set
            {
                _Nome = value;
            }
        }

        public string NomeCompleto
        {
            get
            {
                return _NomeCompleto;
            }
            set
            {
                _NomeCompleto = value;
            }
        }

        public string Rgd
        {
            get
            {
                return _Rgd;
            }
            set
            {
                _Rgd = value;
            }
        }

        public DateTime DataControle
        {
            get
            {
                return _DataControle;
            }
            set
            {
                _DataControle = value;
            }
        }

        public string Raca
        {
            get
            {
                return _Raca;
            }
            set
            {
                _Raca = value;
            }
        }

        public int Ano
        {
            get
            {
                return _Ano;
            }
            set
            {
                _Ano = value;
            }
        }

        public int DiasLactacao
        {
            get
            {
                return _DiasLactacao;
            }
            set
            {
                _DiasLactacao = value;
            }
        }

        public double Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
            }
        }

        public double POrdenha
        {
            get
            {
                return _POrdenha;
            }
            set
            {
                _POrdenha = value;
            }
        }

        public bool SairControle
        {
            get
            {
                return _SairControle;
            }
            set
            {
                _SairControle = value;
            }
        }

        public string SairControleStr
        {
            get
            {
                return _SairControleStr;
            }
            set
            {
                _SairControleStr = value;
            }
        }

    }
}
