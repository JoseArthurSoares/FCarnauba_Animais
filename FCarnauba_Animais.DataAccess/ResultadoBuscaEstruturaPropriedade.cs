using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ResultadoBuscaEstruturaPropriedade
    {
        long _Id;
        string _IdSemHighLight;
        string _Diretorio;
        DateTime _Data;
        string _Usuario;
        DateTime _DataUsuario;
        string _IdPropriedade;
        string _NomePropriedade;
        double _TotalPastagens;
        double _TotalAgricultura;
        double _Benfeitorias;
        double _Arrendamentos;
        double _Reserva;
        double _PalmaForrageira;
        double _Outras;

        double _Area;
        double _AreaUtilizada;
        double _AreaPreservacao;

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

        public string IdSemHighLight
        {
            get
            {
                return _IdSemHighLight;
            }
            set
            {
                _IdSemHighLight = value;
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

        public double Benfeitorias
        {
            get
            {
                return _Benfeitorias;
            }
            set
            {
                _Benfeitorias = value;
            }
        }

        public double Arrendamentos
        {
            get
            {
                return _Arrendamentos;
            }
            set
            {
                _Arrendamentos = value;
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

        public double Outras
        {
            get
            {
                return _Outras;
            }
            set
            {
                _Outras = value;
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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (ResultadoBuscaLote)obj;

            return IdSemHighLight.Equals(other.IdSemHighLight);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
