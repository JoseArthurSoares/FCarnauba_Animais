using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class InformacoesFinanceiras
    {
        double _Entradas;
        double _Desembolsos;
        double _CustoAlimentar;
        double _CustosFixos;
        double _CustosVariaveis;
        double _CustoAdministrativo;
        double _CustoTributario;
        double _VendaLeite;
        double _CustoAlimentarHA;
        string _NomePropriedade;
        string _Periodo;

        public double Entradas
        {
            get
            {
                return _Entradas;
            }
            set
            {
                _Entradas = value;
            }
        }

        public double Desembolsos
        {
            get
            {
                return _Desembolsos;
            }
            set
            {
                _Desembolsos = value;
            }
        }

        public double CustoAlimentar
        {
            get
            {
                return _CustoAlimentar;
            }
            set
            {
                _CustoAlimentar = value;
            }
        }

        public double CustosFixos
        {
            get
            {
                return _CustosFixos;
            }
            set
            {
                _CustosFixos = value;
            }
        }

        public double CustosVariaveis
        {
            get
            {
                return _CustosVariaveis;
            }
            set
            {
                _CustosVariaveis = value;
            }
        }

        public double CustoAdmintrativo
        {
            get
            {
                return _CustoAdministrativo;
            }
            set
            {
                _CustoAdministrativo = value;
            }
        }

        public double CustoTributario
        {
            get
            {
                return _CustoTributario;
            }
            set
            {
                _CustoTributario = value;
            }
        }

        public double VendaLeite
        {
            get
            {
                return _VendaLeite;
            }
            set
            {
                _VendaLeite = value;
            }
        }

        public double CustoAlimentarHA
        {
            get
            {
                return _CustoAlimentarHA;
            }
            set
            {
                _CustoAlimentarHA = value;
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

        public string Periodo
        {
            get
            {
                return _Periodo;
            }
            set
            {
                _Periodo = value;
            }
        }
    }
}
