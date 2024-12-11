using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RAnimaisAno
    {
        long _Id;
        string _Nome;
        string _NomeCompleto;
        string _Rgd;
        DateTime _DataNascimento;
        int _Ano;
        string _Raca;


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

        public int Ano
        {
            get
            {
                return _Ano;
            }
            set
            {
                _Ano = value;
            }
        }
    }
}
