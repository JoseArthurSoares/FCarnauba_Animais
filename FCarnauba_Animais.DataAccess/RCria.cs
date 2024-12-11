using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RCria
    {
        long _Id;
        string _NCria;
        string _Sexo;
        DateTime _DataNascimento;
        double _Pn;
        string _NomePai;
        string _NomeCompletoPai;
        string _NomeMae;
        string _NomeCompletoMae;
        string _Nome;
        string _NomeCompleto;
        string _Rgd;
        string _RgdPai;
        string _RgdMae;
        long _IppIep;
        double _Er;
        double _KgIep;
        double _PMedia;
        double _PMaxima;
        double _Gmd;
        double _PInicial;
        double _PFinal;
        string _PaiId;
        string _MaeId;
        double _IppIepMedio;
        double _ErMedio;
        double _KgIepMedio;
        bool _Fiv;
        string _RgdReceptora;

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

        public string NCria
        {
            get
            {
                return _NCria;
            }
            set
            {
                _NCria = value;
            }
        }

        public string Sexo
        {
            get
            {
                return _Sexo;
            }
            set
            {
                _Sexo = value;
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

        public double Pn
        {
            get
            {
                return _Pn;
            }
            set
            {
                _Pn = value;
            }
        }

        public string NomePai
        {
            get
            {
                return _NomePai;
            }
            set
            {
                _NomePai = value;
            }
        }

        public string NomeCompletoPai
        {
            get
            {
                return _NomeCompletoPai;
            }
            set
            {
                _NomeCompletoPai = value;
            }
        }

        public string NomeCompletoMae
        {
            get
            {
                return _NomeCompletoMae;
            }
            set
            {
                _NomeCompletoMae = value;
            }
        }

        public string NomeMae
        {
            get
            {
                return _NomeMae;
            }
            set
            {
                _NomeMae = value;
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

        public string PaiId
        {
            get
            {
                return _PaiId;
            }
            set
            {
                _PaiId = value;
            }
        }

        public string MaeId
        {
            get
            {
                return _MaeId;
            }
            set
            {
                _MaeId = value;
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

        public string RgdPai
        {
            get
            {
                return _RgdPai;
            }
            set
            {
                _RgdPai = value;
            }
        }

        public string RgdMae
        {
            get
            {
                return _Rgd;
            }
            set
            {
                _RgdMae = value;
            }
        }

        public long IppIep
        {
            get
            {
                return _IppIep;
            }
            set
            {
                _IppIep = value;
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

        public double PMedia
        {
            get
            {
                return _PMedia;
            }
            set
            {
                _PMedia = value;
            }
        }

        public double PMaxima
        {
            get
            {
                return _PMaxima;
            }
            set
            {
                _PMaxima = value;
            }
        }

        public double Gmd
        {
            get
            {
                return _Gmd;
            }
            set
            {
                _Gmd = value;
            }
        }

        public double PInicial
        {
            get
            {
                return _PInicial;
            }
            set
            {
                _PInicial = value;
            }
        }

        public double PFinal
        {
            get
            {
                return _PFinal;
            }
            set
            {
                _PFinal = value;
            }
        }

        public double IppIepMedio
        {
            get
            {
                return _IppIepMedio;
            }
            set
            {
                _IppIepMedio = value;
            }
        }

        public double ErMedio
        {
            get
            {
                return _ErMedio;
            }
            set
            {
                _ErMedio = value;
            }
        }

        public double KgIepMedio
        {
            get
            {
                return _KgIepMedio;
            }
            set
            {
                _KgIepMedio = value;
            }
        }

        public bool Fiv
        {
            get
            {
                return _Fiv;
            }
            set
            {
                _Fiv = value;
            }
        }

        public string RgdReceptora
        {
            get
            {
                return _RgdReceptora;
            }
            set
            {
                _RgdReceptora = value;
            }
        }
    }
}
