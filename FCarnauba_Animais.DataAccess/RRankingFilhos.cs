using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RRankingFilhos
    {
        long _Id;
        long _NumeroOrdem;
        string _NomeCompleto;
        string _Sexo;
        string _Raca;
        int _NumeroFilhos;
        string _StrPaiId;
        string _StrMaeId;

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

        public int NumeroFilhos
        {
            get
            {
                return _NumeroFilhos;
            }
            set
            {
                _NumeroFilhos = value;
            }
        }

        public string StrPaiId
        {
            get
            {
                return _StrPaiId;
            }
            set
            {
                _StrPaiId = value;
            }
        }

        public string StrMaeId
        {
            get
            {
                return _StrMaeId;
            }
            set
            {
                _StrMaeId = value;
            }
        }
    }
}
