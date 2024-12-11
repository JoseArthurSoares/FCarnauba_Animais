using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RRankingIppIepEr
    {
        long _Id;
        long _NumeroOrdem;
        string _Nome;
        string _NomeCompleto;
        string _Rgd;
        string _Raca;
        long _Ipp;
        long _Iep;
        double _Er;
        double _KgIep;
        double _AcumuladaTotal;
        DateTime _DataNascimento;
        

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

        public long NumeroOrdem
        {
            get
            {
                return _NumeroOrdem;
            }
            set
            {
                _NumeroOrdem = value;
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

        public long Ipp
        {
            get
            {
                return _Ipp;
            }
            set
            {
                _Ipp = value;
            }
        }

        public long Iep
        {
            get
            {
                return _Iep;
            }
            set
            {
                _Iep = value;
            }
        }

        public double KgIep
        {
            get
            {
                return _KgIep;
            }
            set
            {
                _KgIep = value;
            }
        }


        public double Er
        {
            get
            {
                return _Er;
            }
            set
            {
                _Er = value;
            }
        }

        public DateTime DataNascimento
        {
            get
            {
                return _DataNascimento;
            }
            set
            {
                _DataNascimento = value;
            }
        }

        public double AcumuladaTotal
        {
            get
            {
                return _AcumuladaTotal;
            }
            set
            {
                _AcumuladaTotal = value;
            }
        }

    }


}
