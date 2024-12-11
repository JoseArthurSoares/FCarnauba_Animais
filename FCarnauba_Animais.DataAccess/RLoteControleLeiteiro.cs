using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RLoteControleLeiteiro
    {
        long _Id;
        string _Diretorio;
        string _SLote;
        DateTime _DataControle;
        string _IdPropriedade;
        string _Raca;
        string _Categoria;
        string _POrdenha;
        string _SOrdenha;
        string _TOrdenha;
        string _Controlador;
        string _LoteDataPropriedade;
        string _Usuario;
        DateTime _DataUsuario;
        string _NomePropriedade;

        private List<RProducaoLeite> _ProducoesLeite = new List<RProducaoLeite>();

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

        public string Categoria
        {
            get
            {
                return _Categoria;
            }
            set
            {
                _Categoria = value;
            }
        }

        public string POrdenha
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

        public string SOrdenha
        {
            get
            {
                return _SOrdenha;
            }
            set
            {
                _SOrdenha = value;
            }
        }

        public string TOrdenha
        {
            get
            {
                return _TOrdenha;
            }
            set
            {
                _TOrdenha = value;
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

        public List<RProducaoLeite> ProducoesLeite
        {
            get { return _ProducoesLeite; }
            set { _ProducoesLeite = value; }
        }
    }
}
