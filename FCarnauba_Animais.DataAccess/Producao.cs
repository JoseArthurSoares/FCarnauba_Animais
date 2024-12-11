using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{

    public class Producao
    {
        double _Acumulada;
        double _Anterior;

        public double Acumulada
        {
            get
            {
                return _Acumulada;
            }
            set
            {
                _Acumulada = value;
            }
        }

        public double Anterior
        {
            get
            {
                return _Anterior;
            }
            set
            {
                _Anterior = value;
            }
        }
    }
}
