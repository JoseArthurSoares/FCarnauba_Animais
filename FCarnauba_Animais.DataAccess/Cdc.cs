using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Cdc
    {
        long _Id;
        string _CioRepeticao;
        string _Diretorio;
        long _NCdc;
        string _Tipo;
        DateTime _DataCobertura;
        DateTime _DataImplantacao;
        string _IdTouro;
        string _Usuario;
        string _Veterinario;
        DateTime _DataUsuario;
        string _NomeTouro;
        bool _CioEfetivo;
        string _CioEfetivoStr;
        string _Raca;
        string _IdPropriedade;
        string _NomePropriedade;

        private List<Matriz> _Matrizes = new List<Matriz>();

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

        public string CioRepeticao
        {
            get
            {
                return _CioRepeticao;
            }
            set
            {
                _CioRepeticao = value;
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

        public long NCdc
        {
            get
            {
                return _NCdc;
            }
            set
            {
                _NCdc = value;
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

        public DateTime DataCobertura
        {
            get
            {
                return _DataCobertura;
            }
            set
            {
                _DataCobertura = value;
            }
        }

        public DateTime DataImplantacao
        {
            get
            {
                return _DataImplantacao;
            }
            set
            {
                _DataImplantacao = value;
            }
        }

        public string IdTouro
        {
            get
            {
                return _IdTouro;
            }
            set
            {
                _IdTouro = value;
            }
        }

        public string NomeTouro
        {
            get
            {
                return _NomeTouro;
            }
            set
            {
                _NomeTouro = value;
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

        public string Veterinario
        {
            get
            {
                return _Veterinario;
            }
            set
            {
                _Veterinario = value;
            }
        }

        public bool CioEfetivo
        {
            get
            {
                return _CioEfetivo;
            }
            set
            {
                _CioEfetivo = value;
            }
        }

        public string CioEfetivoStr
        {
            get
            {
                return _CioEfetivoStr;
            }
            set
            {
                _CioEfetivoStr = value;
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

        public List<Matriz> Matrizes
        {
            get { return _Matrizes; }
            set { _Matrizes = value; }
        }
    }
}
