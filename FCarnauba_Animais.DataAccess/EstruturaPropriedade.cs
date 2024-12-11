using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class EstruturaPropriedade
    {
        long _Id;
        string _StrId;
        string _Diretorio;
        string _IdPropriedade;
        string _NomePropriedade;
        string _RegistroOficial;
        double _Area;
        double _AreaUtilizada;
        double _AreaPreservacao;
        string _Localizacao;
        string _Atividades;
        string _Municipio;
        string _Uf;
        DateTime _Data;
        double _TotalPastagens;
        double _TotalAgricultura;
        double _TotalBenfeitoria;
        double _TotalArrendamento;
        double _TotalOutras;
        double _Reserva;
        double _PalmaForrageira;
        string _Usuario;
        DateTime _DataUsuario;

        private List<Pastagem> _Pastagens = new List<Pastagem>();
        private List<Agricultura> _Agriculturas = new List<Agricultura>();
        private List<Benfeitoria> _Benfeitorias = new List<Benfeitoria>();
        private List<Arrendamento> _Arrendamentos = new List<Arrendamento>();
        private List<Outra> _Outras = new List<Outra>();

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

        public string StrId
        {
            get
            {
                return _StrId;
            }
            set
            {
                _StrId = value;
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

        public double TotalPastagens
        {
            get
            {
                return _TotalPastagens;
            }
            set
            {
                _TotalPastagens = value;
            }
        }

        public double TotalAgricultura
        {
            get
            {
                return _TotalAgricultura;
            }
            set
            {
                _TotalAgricultura = value;
            }
        }

        public double TotalBenfeitoria
        {
            get
            {
                return _TotalBenfeitoria;
            }
            set
            {
                _TotalBenfeitoria = value;
            }
        }

        public double TotalArrendamento
        {
            get
            {
                return _TotalArrendamento;
            }
            set
            {
                _TotalArrendamento = value;
            }
        }

        public double TotalOutras
        {
            get
            {
                return _TotalOutras;
            }
            set
            {
                _TotalOutras = value;
            }
        }

        public double Reserva
        {
            get
            {
                return _Reserva;
            }
            set
            {
                _Reserva = value;
            }
        }

        public double PalmaForrageira
        {
            get
            {
                return _PalmaForrageira;
            }
            set
            {
                _PalmaForrageira = value;
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

        public string RegistroOficial
        {
            get
            {
                return _RegistroOficial;
            }
            set
            {
                _RegistroOficial = value;
            }
        }

        public double Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
            }
        }

        public double AreaUtilizada
        {
            get
            {
                return _AreaUtilizada;
            }
            set
            {
                _AreaUtilizada = value;
            }
        }

        public double AreaPreservacao
        {
            get
            {
                return _AreaPreservacao;
            }
            set
            {
                _AreaPreservacao = value;
            }
        }

        public string Localizacao
        {
            get
            {
                return _Localizacao;
            }
            set
            {
                _Localizacao = value;
            }
        }

        public string Atividades
        {
            get
            {
                return _Atividades;
            }
            set
            {
                _Atividades = value;
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

        public List<Pastagem> Pastagens
        {
            get { return _Pastagens; }
            set { _Pastagens = value; }
        }

        public List<Agricultura> Agriculturas
        {
            get { return _Agriculturas; }
            set { _Agriculturas = value; }
        }

        public List<Benfeitoria> Benfeitorias
        {
            get { return _Benfeitorias; }
            set { _Benfeitorias = value; }
        }

        public List<Arrendamento> Arrendamentos
        {
            get { return _Arrendamentos; }
            set { _Arrendamentos = value; }
        }

        public List<Outra> Outras
        {
            get { return _Outras; }
            set { _Outras = value; }
        }
    }
}
