using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class InformacoesRebanho
    {
        string _Raca;
        int _TotalAnimais;
        int _Bezerros;
        int _Bezerras;
        int _Garrotes;
        int _Novilhas;
        int _Touros;
        int _Matrizes;

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

        public int TotalAnimais
        {
            get
            {
                return _TotalAnimais;
            }
            set
            {
                _TotalAnimais = value;
            }
        }

        public int Bezerros
        {
            get
            {
                return _Bezerros;
            }
            set
            {
                _Bezerros = value;
            }
        }

        public int Bezerras
        {
            get
            {
                return _Bezerras;
            }
            set
            {
                _Bezerras = value;
            }
        }

        public int Garrotes
        {
            get
            {
                return _Garrotes;
            }
            set
            {
                _Garrotes = value;
            }
        }

        public int Novilhas
        {
            get
            {
                return _Novilhas;
            }
            set
            {
                _Novilhas = value;
            }
        }

        public int Touros
        {
            get
            {
                return _Touros;
            }
            set
            {
                _Touros = value;
            }
        }

        public int Matrizes
        {
            get
            {
                return _Matrizes;
            }
            set
            {
                _Matrizes = value;
            }
        }
    }
}
