using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Agricultura
    {
        int _Id;
        string _Tipo;
        double _Area;
        double _AreaTotalAcumulada;
        DateTime _Data;

        public int Id
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

        public string Tipo
        {
            get
            {
                return _Tipo;
            }
            set
            {
                _Tipo = value;
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

        public double AreaTotalAcumulada
        {
            get
            {
                return _AreaTotalAcumulada;
            }
            set
            {
                _AreaTotalAcumulada = value;
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

        public bool Deleted { get; set; }
    }
}
