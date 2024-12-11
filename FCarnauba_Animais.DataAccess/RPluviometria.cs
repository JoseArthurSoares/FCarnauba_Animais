using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RPluviometria
    {
        long _Id;
        string _Diretorio;
        string _Propriedade;
        int _Dia;
        int _Mes;
        string _SMes;
        int _Ano;
        double _Pluviometria;
        double _PluviometriaMensal;
        double _PluviometriaAnual;
        string _Pluviometro;
        DateTime _Data;
        int _DiasCChuva;
        int _DiasME5;
        int _DiasE510;
        int _DiasMA10;
        int _Dias;

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

        public int Dia
        {
            get
            {
                return _Dia;
            }
            set
            {
                _Dia = value;
            }
        }

        public int Mes
        {
            get
            {
                return _Mes;
            }
            set
            {
                _Mes = value;
            }
        }

        public string SMes
        {
            get
            {
                return _SMes;
            }
            set
            {
                _SMes = value;
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

        public double PluviometriaMensal
        {
            get
            {
                return _PluviometriaMensal;
            }
            set
            {
                _PluviometriaMensal = value;
            }
        }

        public double PluviometriaAnual
        {
            get
            {
                return _PluviometriaAnual;
            }
            set
            {
                _PluviometriaAnual = value;
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

        public int DiasCChuva
        {
            get
            {
                return _DiasCChuva;
            }
            set
            {
                _DiasCChuva = value;
            }
        }

        public int DiasME5
        {
            get
            {
                return _DiasME5;
            }
            set
            {
                _DiasME5 = value;
            }
        }

        public int DiasE510
        {
            get
            {
                return _DiasE510;
            }
            set
            {
                _DiasE510 = value;
            }
        }

        public int DiasMA10
        {
            get
            {
                return _DiasMA10;
            }
            set
            {
                _DiasMA10 = value;
            }
        }

        public int Dias
        {
            get
            {
                return _Dias;
            }
            set
            {
                _Dias = value;
            }
        }

    }
}
