using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ControleLeiteiro
    {
        long _Id;
        string _Diretorio;
        string _Proprietario;
        string _Raca;
        string _Categoria;
        string _Fazenda;
        string _Municipio;
        string _Uf;
        DateTime? _DataControle;
        DateTime? _DataProximaVisita;
        string _POrdenha;
        string _SOrdenha;
        string _TOrdenha;
        string _Controlador;
        string _Usuario;
        DateTime? _DataUsuario;
        string _Lote;
        string _IdLote;

        private List<ProducaoLeite> _ProducoesLeite = new List<ProducaoLeite>();

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

        public string Proprietario
        {
            get
            {
                return _Proprietario;
            }
            set
            {
                _Proprietario = value;
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

        public string Fazenda
        {
            get
            {
                return _Fazenda;
            }
            set
            {
                _Fazenda = value;
            }
        }

        public string Municipio
        {
            get
            {
                return _Municipio;
            }
            set
            {
                _Municipio = value;
            }
        }

        public string Uf
        {
            get
            {
                return _Uf;
            }
            set
            {
                _Uf = value;
            }
        }

        public DateTime? DataControle
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

        public DateTime? DataProximaVisita
        {
            get
            {
                return _DataProximaVisita;
            }
            set
            {
                _DataProximaVisita = value;
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

        public DateTime? DataUsuario
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

        public string Lote
        {
            get
            {
                return _Lote;
            }
            set
            {
                _Lote = value;
            }
        }

        public string IdLote
        {
            get
            {
                return _IdLote;
            }
            set
            {
                _IdLote = value;
            }
        }

        public List<ProducaoLeite> ProducoesLeite
        {
            get { return _ProducoesLeite; }
            set { _ProducoesLeite = value; }
        }
    }
}
