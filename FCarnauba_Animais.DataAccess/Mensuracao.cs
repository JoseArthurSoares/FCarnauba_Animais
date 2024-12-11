using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Mensuracao
    {
        int _Id;
        string _IdAnimal;
        string _NomeAnimal;
        double _Peso;
        DateTime _DataPesagem;
        string _RegimeAlimentar;
        double _CEscrotal;
        double _AAnterior;
        double _APosterior;
        double _LGarupa;
        double _CGarupa;
        double _CCorporal;
        double _PToracico;
        string _CaracterizacaoRacial;
        string _ClassificacaoUbere;
        DateTime? _DataEntradaControle;
        DateTime? _DataSaidaControle;
        bool _SairControle;
        string _SairControleSr;
        string _Motivo;
        string _CondicaoCriacao;
        DateTime? _DataDesmame;
        DateTime? _DataDiagnosticoPrenhez;
        DateTime? _DataParto;
        DateTime? _DataEntradaControleLeiteiro;
        DateTime? _DataEncerramentoLactacao;
        double _PesoMaeDesmame;
        double _RelacaoDesmama;

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

        public DateTime DataPesagem
        {
            get
            {
                return _DataPesagem;
            }
            set
            {
                _DataPesagem = value;
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

        public double CEscrotal
        {
            get
            {
                return _CEscrotal;
            }
            set
            {
                _CEscrotal = value;
            }
        }

        public double AAnterior
        {
            get
            {
                return _AAnterior;
            }
            set
            {
                _AAnterior = value;
            }
        }

        public double APosterior
        {
            get
            {
                return _APosterior;
            }
            set
            {
                _APosterior = value;
            }
        }

        public double LGarupa
        {
            get
            {
                return _LGarupa;
            }
            set
            {
                _LGarupa = value;
            }
        }

        public double CGarupa
        {
            get
            {
                return _CGarupa;
            }
            set
            {
                _CGarupa = value;
            }
        }

        public double CCorporal
        {
            get
            {
                return _CCorporal;
            }
            set
            {
                _CCorporal = value;
            }
        }

        public double PToracico
        {
            get
            {
                return _PToracico;
            }
            set
            {
                _PToracico = value;
            }
        }

        public string CaracterizacaoRacial
        {
            get
            {
                return _CaracterizacaoRacial;
            }
            set
            {
                _CaracterizacaoRacial = value;
            }
        }

        public string ClassificacaoUbere
        {
            get
            {
                return _ClassificacaoUbere;
            }
            set
            {
                _ClassificacaoUbere = value;
            }
        }

        public DateTime? DataEntradaControle
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

        public DateTime? DataSaidaControle
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

        public string SairControleSr
        {
            get
            {
                return _SairControleSr;
            }
            set
            {
                _SairControleSr = value;
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

        public string CondicaoCriacao
        {
            get
            {
                return _CondicaoCriacao;
            }
            set
            {
                _CondicaoCriacao = value;
            }
        }


        public DateTime? DataDesmame
        {
            get
            {
                return _DataDesmame;
            }
            set
            {
                _DataDesmame = value;
            }
        }

        public DateTime? DataDiagnosticoPrenhez
        {
            get
            {
                return _DataDiagnosticoPrenhez;
            }
            set
            {
                _DataDiagnosticoPrenhez = value;
            }
        }

        public DateTime? DataParto
        {
            get
            {
                return _DataParto;
            }
            set
            {
                _DataParto = value;
            }
        }

        public DateTime? DataEntradaControleLeiteiro
        {
            get
            {
                return _DataEntradaControleLeiteiro;
            }
            set
            {
                _DataEntradaControleLeiteiro = value;
            }
        }

        public DateTime? DataEncerramentoLactacao
        {
            get
            {
                return _DataEncerramentoLactacao;
            }
            set
            {
                _DataEncerramentoLactacao = value;
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

        public double RelacaoDesmama
        {
            get
            {
                return _RelacaoDesmama;
            }
            set
            {
                _RelacaoDesmama = value;
            }
        }


        public bool Deleted { get; set; }
    }
}
