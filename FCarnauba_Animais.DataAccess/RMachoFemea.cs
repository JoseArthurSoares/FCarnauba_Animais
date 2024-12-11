using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RMachoFemea
    {
        long _Id;
        string _Nome;
        string _NomeCompleto;
        string _Rgd;
        string _PaiId;
        string _MaeId;
        DateTime _DataNascimento;
        string _NomePai;
        string _NomeMae;
        string _NomeCompletoPai;
        string _NomeCompletoMae;
        string _RgdPai;
        string _RgdMae;
        string _Raca;
        string _Sexo;

        private List<RCria> _Crias = new List<RCria>();

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
                return _RgdMae;
            }
            set
            {
                _RgdMae = value;
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

        public List<RCria> Crias
        {
            get { return _Crias; }
            set { _Crias = value; }
        }
    }
}
