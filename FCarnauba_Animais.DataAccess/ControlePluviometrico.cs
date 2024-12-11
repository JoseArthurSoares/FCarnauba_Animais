using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ControlePluviometrico
    {
        long _Id;
        string _Diretorio;
        DateTime _Data;
        string _DataStr;
        double _Pluviometria;
        string _IdPropriedade;
        string _Usuario;
        DateTime _DataUsuario;
        string _Pluviometro;

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

        public string DataStr
        {
            get
            {
                return _DataStr;
            }
            set
            {
                _DataStr = value;
            }
        }

        public double Pluviometria
        {
            get
            {
                return _Pluviometria;
            }
            set
            {
                _Pluviometria = value;
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

        public string Pluviometro
        {
            get
            {
                return _Pluviometro;
            }
            set
            {
                _Pluviometro = value;
            }
        }
    }
}
