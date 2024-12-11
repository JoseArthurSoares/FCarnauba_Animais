using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RRankingPeso
    {
        long _Id;
        long _NumeroOrdem;
        string _NomeCompleto;
        string _Sexo;
        string _Raca;
        double _Pn;

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
    }
}
