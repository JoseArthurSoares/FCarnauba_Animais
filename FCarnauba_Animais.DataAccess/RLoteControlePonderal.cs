using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RLoteControlePonderal
    {
        long _Id;
        string _Diretorio;
        string _SLote;
        DateTime _DataControle;
        string _IdPropriedade;
        string _Raca;
        string _LoteDataPropriedade;
        string _Usuario;
        DateTime _DataUsuario;
        string _NomePropriedade;
        string _Controlador;

        private List<RMensuracao> _Mensuracoes = new List<RMensuracao>();

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

        public string SLote
        {
            get
            {
                return _SLote;
            }
            set
            {
                _SLote = value;
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

        public string LoteDataPropriedade
        {
            get
            {
                return _LoteDataPropriedade;
            }
            set
            {
                _LoteDataPropriedade = value;
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

        public string Controlador
        {
            get
            {
                return _Controlador;
            }
            set
            {
                _Controlador = value;
            }
        }

        public List<RMensuracao> Mensuracoes
        {
            get { return _Mensuracoes; }
            set { _Mensuracoes = value; }
        }
    }
}
