using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Compra
    {
        int _Id;
        int _FinanceiroId;
        string _CompraFinanceiroId;
        string _IdAnimal;
        string _NomeAnimal;
        string _Evento;
        string _Descricao;
        double _Valor;

        public int FinanceiroId
        {
            get
            {
                return _FinanceiroId;
            }
            set
            {
                _FinanceiroId = value;
            }
        }

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

        public string IdAnimal
        {
            get
            {
                return _IdAnimal;
            }
            set
            {
                _IdAnimal = value;
            }
        }

        public string CompraFinanceiroId
        {
            get
            {
                return _CompraFinanceiroId;
            }
            set
            {
                _CompraFinanceiroId = value;
            }
        }

        public string NomeAnimal
        {
            get
            {
                return _NomeAnimal;
            }
            set
            {
                _NomeAnimal = value;
            }
        }

        public string Evento
        {
            get
            {
                return _Evento;
            }
            set
            {
                _Evento = value;
            }
        }

        public string Descricao
        {
            get
            {
                return _Descricao;
            }
            set
            {
                _Descricao = value;
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

    }
}
