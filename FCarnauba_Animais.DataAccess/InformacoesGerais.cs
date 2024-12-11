using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class InformacoesGerais
    {
        string _Localizacao;
        double _AreaTotal;
        double _AreaPastagens;
        double _AreaAgricultura;
        double _Pluviometria;

        public string Localizacao
        {
            get
            {
                return _Localizacao;
            }
            set
            {
                _Localizacao = value;
            }
        }

        public double AreaTotal
        {
            get
            {
                return _AreaTotal;
            }
            set
            {
                _AreaTotal = value;
            }
        }

        public double AreaPastagens
        {
            get
            {
                return _AreaPastagens;
            }
            set
            {
                _AreaPastagens = value;
            }
        }

        public double AreaAgricultura
        {
            get
            {
                return _AreaAgricultura;
            }
            set
            {
                _AreaAgricultura = value;
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
    }
}
