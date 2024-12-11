using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ProducaoLeite
    {
        int _Id;
        string _IdMatriz;
        string _LoteId;
        string _NomeMatriz;
        string _Raca;
        string _RgdMatriz;
        string _NomePropriedade;
        int _DiasLactacao;
        double _Esgota;
        double _POrdenha;
        double _SOrdenha;
        double _TOrdenha;
        double _Total;
        double _AcumuladoLactacao;
        double _Acumulado;
        double _Media;
        double _Maxima;
        bool _BezerrosPe;
        string _BezerrosPeSr;
        int _TetosFuncionais;
        string _Obs;
        string _RegimeAlimentar;
        DateTime? _DataEntradaControle;
        DateTime? _DataSaidaControle;
        string _IdCria;
        string _NomeCria;
        bool _Receptora;
        double _GordPOrdenha;
        double _GordSOrdenha;
        double _GordTOrdenha;
        double _ProtPOrdenha;
        double _ProtSOrdenha;
        double _ProtTOrdenha;
        double _GordMedia;
        double _ProtMedia;
        double _RQueijeiro;
        bool _SairControle;
        string _SairControleSr;
        string _Motivo;
        int _Ano;
        string _PesagemLoteId;
        string _NomePaiMae;


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

        public string LoteId
        {
            get
            {
                return _LoteId;
            }
            set
            {
                _LoteId = value;
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

        public double AcumuladoLactacao
        {
            get
            {
                return _AcumuladoLactacao;
            }
            set
            {
                _AcumuladoLactacao = value;
            }
        }

        public double Media
        {
            get
            {
                return _Media;
            }
            set
            {
                _Media = value;
            }
        }

        public double Maxima
        {
            get
            {
                return _Maxima;
            }
            set
            {
                _Maxima = value;
            }
        }

        public bool BezerrosPe
        {
            get
            {
                return _BezerrosPe;
            }
            set
            {
                _BezerrosPe = value;
            }
        }

        public string BezerrosPeSr
        {
            get
            {
                return _BezerrosPeSr;
            }
            set
            {
                _BezerrosPeSr = value;
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

        public string NomeCria
        {
            get
            {
                return _NomeCria;
            }
            set
            {
                _NomeCria = value;
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

        public double ProtMedia
        {
            get
            {
                return _ProtMedia;
            }
            set
            {
                _ProtMedia = value;
            }
        }

        public double GordMedia
        {
            get
            {
                return _GordMedia;
            }
            set
            {
                _GordMedia = value;
            }
        }

        public double RQueijeiro
        {
            get
            {
                return _RQueijeiro;
            }
            set
            {
                _RQueijeiro = value;
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

        public string PesagemLoteId
        {
            get
            {
                return _PesagemLoteId;
            }
            set
            {
                _PesagemLoteId = value;
            }
        }

        public string NomePaiMae
        {
            get
            {
                return _NomePaiMae;
            }
            set
            {
                _NomePaiMae = value;
            }
        }

    }
}
