using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class IndiceZootecnico
    {
        string _Indice;
        double _Valor;
        double _ValorReferencia;
        string _Propriedade;
        string _Raca;
        string _Periodo;
        string _AnoPecuario;

        public string Indice
        {
            get
            {
                return _Indice;
            }
            set
            {
                _Indice = value;
            }
        }

        public double Valor
        {
            get
            {
                return _Valor;
            }
            set
            {
                _Valor = value;
            }
        }

        public double ValorReferencia
        {
            get
            {
                return _ValorReferencia;
            }
            set
            {
                _ValorReferencia = value;
            }
        }

        public string Propriedade
        {
            get
            {
                return _Propriedade;
            }
            set
            {
                _Propriedade = value;
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

        public string AnoPecuario
        {
            get
            {
                return _AnoPecuario;
            }
            set
            {
                _AnoPecuario = value;
            }
        }
    }
}
