using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RPeso
    {
        long _Id;
        string _Nome;
        string _NomePropriedade;
        string _Sexo;
        string _Raca;
        double _Peso;
        DateTime _DataControle;
        string _Periodo;

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

        public double Peso
        {
            get
            {
                return _Peso;
            }
            set
            {
                _Peso = value;
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

        public string Periodo
        {
            get
            {
                return _Periodo;
            }
            set
            {
                _Periodo = value;
            }
        }
    }
}
