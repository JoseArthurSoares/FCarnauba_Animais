using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RControleLeiteiro
    {
        long _Id;
        string _Proprietario;
        string _Raca;
        string _Categoria;
        string _Fazenda;
        string _Municipio;
        string _Uf;
        DateTime _DataControle;
        DateTime _DataProximaVisita;
        string _HoraPOrdenha;
        string _HoraSOrdenha;
        string _HoraTOrdenha;
        string _Controlador;
        string _IdLote;

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

        public DateTime DataProximaVisita
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

        public string HoraPOrdenha
        {
            get
            {
                return _HoraPOrdenha;
            }
            set
            {
                _HoraPOrdenha = value;
            }
        }

        public string HoraSOrdenha
        {
            get
            {
                return _HoraSOrdenha;
            }
            set
            {
                _HoraSOrdenha = value;
            }
        }

        public string HoraTOrdenha
        {
            get
            {
                return _HoraTOrdenha;
            }
            set
            {
                _HoraTOrdenha = value;
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

        public List<RProducaoLeite> ProducoesLeite
        {
            get { return _ProducoesLeite; }
            set { _ProducoesLeite = value; }
        }


    }
}
