using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class RProducaoReal
    {
        int _Controle;
        long _Id;
        string _IdMatriz;
        string _NomeMatriz;
        string _RgdMatriz;
        DateTime _DataNascimentoMatriz;
        string _NomeFazendaMatriz;
        string _IdCria;
        int _DiasLactacao;
        double _Esgota;
        double _POrdenha;
        double _SOrdenha;
        double _TOrdenha;
        double _Total;
        bool _BezerrosAoPe;
        int _TetosFuncionais;
        string _Obs;
        string _RegimeAlimentar;
        DateTime _DataEntradaControle;
        DateTime _DataSaidaControle;
        bool _Receptora;
        double _GordPOrdenha;
        double _GordSOrdenha;
        double _GordTOrdenha;
        double _ProtPOrdenha;
        double _ProtSOrdenha;
        double _ProtTOrdenha;
        double _ProducaoAnterior;
        double _ProducaoAcumulada;
        bool _SairControle;
        string _Motivo;

        //Produção real
        int _DiasLactacaoReal;
        double _ProducaoAcumuladaReal;
        double _MediaReal;

        //Controle Leiteiro
        string _Fazenda;
        string _Raca;
        string _IdLote;
        DateTime _DataControle;
        DateTime _DataProximaVisita; string _HoraPOrdenha;
        string _HoraSOrdenha;
        string _HoraTOrdenha;

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

        public string IdMatriz
        {
            get
            {
                return _IdMatriz;
            }
            set
            {
                _IdMatriz = value;
            }
        }

        public string NomeMatriz
        {
            get
            {
                return _NomeMatriz;
            }
            set
            {
                _NomeMatriz = value;
            }
        }

        public string RgdMatriz
        {
            get
            {
                return _RgdMatriz;
            }
            set
            {
                _RgdMatriz = value;
            }
        }

        public DateTime DataNascimentoMatriz
        {
            get
            {
                return _DataNascimentoMatriz;
            }
            set
            {
                _DataNascimentoMatriz = value;
            }
        }

        public string NomeFazendaMatriz
        {
            get
            {
                return _NomeFazendaMatriz;
            }
            set
            {
                _NomeFazendaMatriz = value;
            }
        }

        public string IdCria
        {
            get
            {
                return _IdCria;
            }
            set
            {
                _IdCria = value;
            }
        }

        public int DiasLactacao
        {
            get
            {
                return _DiasLactacao;
            }
            set
            {
                _DiasLactacao = value;
            }
        }

        public double Esgota
        {
            get
            {
                return _Esgota;
            }
            set
            {
                _Esgota = value;
            }
        }

        public double POrdenha
        {
            get
            {
                return _POrdenha;
            }
            set
            {
                _POrdenha = value;
            }
        }

        public double SOrdenha
        {
            get
            {
                return _SOrdenha;
            }
            set
            {
                _SOrdenha = value;
            }
        }

        public double TOrdenha
        {
            get
            {
                return _TOrdenha;
            }
            set
            {
                _TOrdenha = value;
            }
        }

        public double Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
            }
        }

        public bool BezerrosAoPe
        {
            get
            {
                return _BezerrosAoPe;
            }
            set
            {
                _BezerrosAoPe = value;
            }
        }

        public int TetosFuncionais
        {
            get
            {
                return _TetosFuncionais;
            }
            set
            {
                _TetosFuncionais = value;
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

        public string RegimeAlimentar
        {
            get
            {
                return _RegimeAlimentar;
            }
            set
            {
                _RegimeAlimentar = value;
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

        public bool Receptora
        {
            get
            {
                return _Receptora;
            }
            set
            {
                _Receptora = value;
            }
        }

        public double GordPOrdenha
        {
            get
            {
                return _GordPOrdenha;
            }
            set
            {
                _GordPOrdenha = value;
            }
        }

        public double GordSOrdenha
        {
            get
            {
                return _GordSOrdenha;
            }
            set
            {
                _GordSOrdenha = value;
            }
        }

        public double GordTOrdenha
        {
            get
            {
                return _GordTOrdenha;
            }
            set
            {
                _GordTOrdenha = value;
            }
        }

        public double ProtPOrdenha
        {
            get
            {
                return _ProtPOrdenha;
            }
            set
            {
                _ProtPOrdenha = value;
            }
        }

        public double ProtSOrdenha
        {
            get
            {
                return _ProtSOrdenha;
            }
            set
            {
                _ProtSOrdenha = value;
            }
        }

        public double ProtTOrdenha
        {
            get
            {
                return _ProtTOrdenha;
            }
            set
            {
                _ProtTOrdenha = value;
            }
        }

        public double ProducaoAnterior
        {
            get
            {
                return _ProducaoAnterior;
            }
            set
            {
                _ProducaoAnterior = value;
            }
        }

        public double ProducaoAcumulada
        {
            get
            {
                return _ProducaoAcumulada;
            }
            set
            {
                _ProducaoAcumulada = value;
            }
        }



        //Produção Real

        public int DiasLactacaoReal
        {
            get
            {
                return _DiasLactacaoReal;
            }
            set
            {
                _DiasLactacaoReal = value;
            }
        }

        public double ProducaoAcumuladaReal
        {
            get
            {
                return _ProducaoAcumulada;
            }
            set
            {
                _ProducaoAcumulada = value;
            }
        }

        public double MediaReal
        {
            get
            {
                return _MediaReal;
            }
            set
            {
                _MediaReal = value;
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

        //Controle Leiteiro

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

        public DateTime DataProximaVisita
        {
            get
            {
                return _DataProximaVisita;
            }
            set
            {
                _DataProximaVisita = value;
            }
        }

        public string HoraPOrdenha
        {
            get
            {
                return _HoraPOrdenha;
            }
            set
            {
                _HoraPOrdenha = value;
            }
        }

        public string HoraSOrdenha
        {
            get
            {
                return _HoraSOrdenha;
            }
            set
            {
                _HoraSOrdenha = value;
            }
        }

        public string HoraTOrdenha
        {
            get
            {
                return _HoraTOrdenha;
            }
            set
            {
                _HoraTOrdenha = value;
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

    }
}

