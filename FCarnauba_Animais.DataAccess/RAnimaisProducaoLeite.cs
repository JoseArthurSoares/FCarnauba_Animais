using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RAnimaisProducaoLeite
    {
        long _Id;
        string _NomeCompleto;
        long _NumeroOrdem;
        string _Raca;
        double _Acumulado;
        double _ProducaoDiariaMaxima;
        double _ProducaoDiariaMedia;

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

        public double Acumulado
        {
            get
            {
                return _Acumulado;
            }
            set
            {
                _Acumulado = value;
            }
        }

        public double ProducaoDiariaMaxima
        {
            get
            {
                return _ProducaoDiariaMaxima;
            }
            set
            {
                _ProducaoDiariaMaxima = value;
            }
        }

        public double ProducaoDiariaMedia
        {
            get
            {
                return _ProducaoDiariaMedia;
            }
            set
            {
                _ProducaoDiariaMedia = value;
            }
        }
    }
}
