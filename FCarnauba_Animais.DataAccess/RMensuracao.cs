using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RMensuracao
    {
        int _Controle;
        long _Id;
        string _IdAnimal;
        string _NomeAnimal;
        string _NomePropriedade;
        string _RgdAnimal;
        string _RgnAnimal;
        DateTime _DataNascimentoAnimal;
        DateTime _DataControle;
        int _IdadeMeses;
        string _SLote;

        //Mensurações
        double _Peso;
        string _RA;
        double _CC;
        double _CE;
        double _AA;
        double _AP;
        double _LG;
        double _CG;
        double _PT;
        double _GMD;
        string _Obs;
        DateTime _DataEntradaControle;
        DateTime _DataSaidaControle;
        bool _SairControle;
        string _SairControleStr;
        string _Motivo;
        string _Raca;
        string _IdLote;
        string _Fazenda;
        double _PesoMaeDesmame;

        public int Controle
        {
            get
            {
                return _Controle;
            }
            set
            {
                _Controle = value;
            }
        }


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

        public string RgdAnimal
        {
            get
            {
                return _RgdAnimal;
            }
            set
            {
                _RgdAnimal = value;
            }
        }

        public string RgnAnimal
        {
            get
            {
                return _RgnAnimal;
            }
            set
            {
                _RgnAnimal = value;
            }
        }

        public DateTime DataNascimentoAnimal
        {
            get
            {
                return _DataNascimentoAnimal;
            }
            set
            {
                _DataNascimentoAnimal = value;
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

        public int IdadeMeses
        {
            get
            {
                return _IdadeMeses;
            }
            set
            {
                _IdadeMeses = value;
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

        public string RA
        {
            get
            {
                return _RA;
            }
            set
            {
                _RA = value;
            }
        }

        public double CC
        {
            get
            {
                return _CC;
            }
            set
            {
                _CC = value;
            }
        }

        public double CE
        {
            get
            {
                return _CE;
            }
            set
            {
                _CE = value;
            }
        }

        public double AA
        {
            get
            {
                return _AA;
            }
            set
            {
                _AA = value;
            }
        }

        public double AP
        {
            get
            {
                return _AP;
            }
            set
            {
                _AP = value;
            }
        }

        public double LG
        {
            get
            {
                return _LG;
            }
            set
            {
                _LG = value;
            }
        }

        public double CG
        {
            get
            {
                return _CG;
            }
            set
            {
                _CG = value;
            }
        }

        public double PT
        {
            get
            {
                return _PT;
            }
            set
            {
                _PT = value;
            }
        }
        public double GMD
        {
            get
            {
                return _GMD;
            }
            set
            {
                _GMD = value;
            }
        }

        public string Obs
        {
            get
            {
                return _Obs;
            }
            set
            {
                _Obs = value;
            }
        }

        public DateTime DataEntradaControle
        {
            get
            {
                return _DataEntradaControle;
            }
            set
            {
                _DataEntradaControle = value;
            }
        }

        public DateTime DataSaidaControle
        {
            get
            {
                return _DataSaidaControle;
            }
            set
            {
                _DataSaidaControle = value;
            }
        }

        public bool SairControle
        {
            get
            {
                return _SairControle;
            }
            set
            {
                _SairControle = value;
            }
        }

        public string SairControleStr
        {
            get
            {
                return _SairControleStr;
            }
            set
            {
                _SairControleStr = value;
            }
        }

        public string Motivo
        {
            get
            {
                return _Motivo;
            }
            set
            {
                _Motivo = value;
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

        public string IdLote
        {
            get
            {
                return _IdLote;
            }
            set
            {
                _IdLote = value;
            }
        }

        public string Fazenda
        {
            get
            {
                return _Fazenda;
            }
            set
            {
                _Fazenda = value;
            }
        }

        public string SLote
        {
            get
            {
                return _SLote;
            }
            set
            {
                _SLote = value;
            }
        }

        public double PesoMaeDesmame
        {
            get
            {
                return _PesoMaeDesmame;
            }
            set
            {
                _PesoMaeDesmame = value;
            }
        }
    }
}
