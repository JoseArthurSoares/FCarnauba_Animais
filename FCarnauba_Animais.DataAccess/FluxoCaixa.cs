using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class FluxoCaixa
    {
        long _Id;
        string _Diretorio;
        string _Tipo;
        string _Descricao;
        double _Valor;
        DateTime _Data;
        string _IdPropriedade;
        string _IdCentroCusto;
        string _Usuario;
        DateTime _DataUsuario;
        string _NomePropriedade;
        string _DescricaoCentroCusto;

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

        public string Diretorio
        {
            get
            {
                return _Diretorio;
            }
            set
            {
                _Diretorio = value;
            }
        }

        public DateTime Data
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

        public string IdPropriedade
        {
            get
            {
                return _IdPropriedade;
            }
            set
            {
                _IdPropriedade = value;
            }
        }

        public string IdCentroCusto
        {
            get
            {
                return _IdCentroCusto;
            }
            set
            {
                _IdCentroCusto = value;
            }
        }

        public string Usuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                _Usuario = value;
            }
        }

        public DateTime DataUsuario
        {
            get
            {
                return _DataUsuario;
            }
            set
            {
                _DataUsuario = value;
            }
        }

        public string Tipo
        {
            get
            {
                return _Tipo;
            }
            set
            {
                _Tipo = value;
            }
        }

        public string Descricao
        {
            get
            {
                return _Descricao;
            }
            set
            {
                _Descricao = value;
            }
        }

        public double Valor
        {
            get
            {
                return _Valor;
            }
            set
            {
                _Valor = value;
            }
        }

        public string NomePropriedade
        {
            get
            {
                return _NomePropriedade;
            }
            set
            {
                _NomePropriedade = value;
            }
        }

        public string DescricaoCentroCusto
        {
            get
            {
                return _DescricaoCentroCusto;
            }
            set
            {
                _DescricaoCentroCusto = value;
            }
        }

    }
}
