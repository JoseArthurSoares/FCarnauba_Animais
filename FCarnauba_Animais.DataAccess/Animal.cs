using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Animal
    {
        long _Id;
        string _Diretorio;
        long _NumeroOrdem;
        string _NomeFazenda;
        string _Nome;
        string _NomeRg;
        string _NomeCompleto;
        string _Sexo;
        string _Rgn;
        DateTime _DataNascimento;
        string _Rgd;
        string _Raca;
        double _Pn;
        long _Ipp;
        long _Iep;
        double _KgIep;
        Arquivo _LaudoDna;
        Arquivo _LaudoDna2;
        Arquivo _LaudoDna3;
        Arquivo _LaudoDna4;
        Arquivo _LaudoBetaCaseina;
        Arquivo _LaudoKappaCaseina;
        Arquivo _Foto;
        string _TipoBetaCaseina;
        string _TipoKappaCaseina;
        long _CdnOrigem;
        long _CdcOrigem;
        string _Observacoes;
        DateTime _DataUltimoParto;
        string _RgdSerie;
        long _RgdNumero;
        string _RgnSerie;
        long _RgnNumero;
        string _StrId;
        string _RegistroRgnRaca;
        string _RegistroRgdRaca;

        string _StrReceptoraId;
        string _NomeReceptora;

        string _StrPaiId;
        string _StrMaeId;
        string _NomePai;
        string _NomeMae;
        DateTime _DataCdc;
        string _NCria;
        string _Usuario;
        DateTime _DataUsuario;
        double _Er;
        bool _TemRgn;
        bool _TemRgd;
        bool _TemLaudoDna;
        bool _TemLaudoDna2;
        bool _TemLaudoArquivoPermanente;
        bool _TemLaudoSecundario1;
        bool _TemLaudoSecundario2;
        bool _TemLaudoBetaCaseina;
        bool _TemLaudoKappaCaseina;
        bool _EhFIV;
        bool _EhCria;

        string _TipoParto;
        string _VigorBez;
        string _EstadoCorporalMae;
        string _TamanhoTeta;
        string _MaeBoaLeite;

        bool _MaeOrdenhada;
        bool _AnimalImprodutivo;

        string _RgnRaca;
        string _Temperamento;

        private List<Historico> _Historicos = new List<Historico>();
        private List<Mensuracao> _Mensuracoes = new List<Mensuracao>();

        public bool HasFoto()
        {
            return _Foto != null;
        }

        public bool HasPDFLaudoDna()
        {
            return _LaudoDna != null;
        }

        public bool HasPDFLaudoDna2()
        {
            return _LaudoDna2 != null;
        }

        public bool HasPDFLaudoDna3()
        {
            return _LaudoDna3 != null;
        }

        public bool HasPDFLaudoDna4()
        {
            return _LaudoDna4 != null;
        }

        public bool HasPDFLaudoBetaCaseina()
        {
            return _LaudoBetaCaseina != null;
        }

        public bool HasPDFLaudoKappaCaseina()
        {
            return _LaudoKappaCaseina != null;
        }

        public Arquivo Foto
        {
            get { return _Foto; }
            set { _Foto = value; }
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

        public string NomeFazenda
        {
            get
            {
                return _NomeFazenda;
            }
            set
            {
                _NomeFazenda = value;
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

        public string NomeRg
        {
            get
            {
                return _NomeRg;
            }
            set
            {
                _NomeRg = value;
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

        public string Sexo
        {
            get
            {
                return _Sexo;
            }
            set
            {
                _Sexo = value;
            }
        }

        public string Rgn
        {
            get
            {
                return _Rgn;
            }
            set
            {
                _Rgn = value;
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

        public double Pn
        {
            get
            {
                return _Pn;
            }
            set
            {
                _Pn = value;
            }
        }

        public long Ipp
        {
            get
            {
                return _Ipp;
            }
            set
            {
                _Ipp = value;
            }
        }

        public long Iep
        {
            get
            {
                return _Iep;
            }
            set
            {
                _Iep = value;
            }
        }

        public double KgIep
        {
            get
            {
                return _KgIep;
            }
            set
            {
                _KgIep = value;
            }
        }

        public double Er
        {
            get
            {
                return _Er;
            }
            set
            {
                _Er = value;
            }
        }

        public Arquivo LaudoDna
        {
            get { return _LaudoDna; }
            set { _LaudoDna = value; }
        }

        public Arquivo LaudoDna2
        {
            get { return _LaudoDna2; }
            set { _LaudoDna2 = value; }
        }

        public Arquivo LaudoDna3
        {
            get { return _LaudoDna3; }
            set { _LaudoDna3 = value; }
        }

        public Arquivo LaudoDna4
        {
            get { return _LaudoDna4; }
            set { _LaudoDna4 = value; }
        }

        public Arquivo LaudoBetaCaseina
        {
            get { return _LaudoBetaCaseina; }
            set { _LaudoBetaCaseina = value; }
        }

        public Arquivo LaudoKappaCaseina
        {
            get { return _LaudoKappaCaseina; }
            set { _LaudoKappaCaseina = value; }
        }

        public string TipoBetaCaseina
        {
            get
            {
                return _TipoBetaCaseina;
            }
            set
            {
                _TipoBetaCaseina = value;
            }
        }

        public string TipoKappaCaseina
        {
            get
            {
                return _TipoKappaCaseina;
            }
            set
            {
                _TipoKappaCaseina = value;
            }
        }

        public long CdnOrigem
        {
            get
            {
                return _CdnOrigem;
            }
            set
            {
                _CdnOrigem = value;
            }
        }

        public long CdcOrigem
        {
            get
            {
                return _CdcOrigem;
            }
            set
            {
                _CdcOrigem = value;
            }
        }

        public string Observacoes
        {
            get
            {
                return _Observacoes;
            }
            set
            {
                _Observacoes = value;
            }
        }

        public DateTime DataUltimoParto
        {
            get
            {
                return _DataUltimoParto;
            }
            set
            {
                _DataUltimoParto = value;
            }
        }

        public string RgdSerie
        {
            get
            {
                return _RgdSerie;
            }
            set
            {
                _RgdSerie = value;
            }
        }

        public long RgdNumero
        {
            get
            {
                return _RgdNumero;
            }
            set
            {
                _RgdNumero = value;
            }
        }


        public string RgnSerie
        {
            get
            {
                return _RgnSerie;
            }
            set
            {
                _RgnSerie = value;
            }
        }

        public long RgnNumero
        {
            get
            {
                return _RgnNumero;
            }
            set
            {
                _RgnNumero = value;
            }
        }


        public string StrId
        {
            get
            {
                return _StrId;
            }
            set
            {
                _StrId = value;
            }
        }

        public string StrReceptoraId
        {
            get
            {
                return _StrReceptoraId;
            }
            set
            {
                _StrReceptoraId = value;
            }
        }

        public string NomeReceptora
        {
            get
            {
                return _NomeReceptora;
            }
            set
            {
                _NomeReceptora = value;
            }
        }

        public string StrPaiId
        {
            get
            {
                return _StrPaiId;
            }
            set
            {
                _StrPaiId = value;
            }
        }

        public string StrMaeId
        {
            get
            {
                return _StrMaeId;
            }
            set
            {
                _StrMaeId = value;
            }
        }

        public string NomePai
        {
            get
            {
                return _NomePai;
            }
            set
            {
                _NomePai = value;
            }
        }

        public string NomeMae
        {
            get
            {
                return _NomeMae;
            }
            set
            {
                _NomeMae = value;
            }
        }

        public DateTime DataCdc
        {
            get
            {
                return _DataCdc;
            }
            set
            {
                _DataCdc = value;
            }
        }

        public string NCria
        {
            get
            {
                return _NCria;
            }
            set
            {
                _NCria = value;
            }
        }

        public string Usuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                _Usuario = value;
            }
        }

        public DateTime DataUsuario
        {
            get
            {
                return _DataUsuario;
            }
            set
            {
                _DataUsuario = value;
            }
        }

        public bool TemRgn
        {
            get
            {
                return _TemRgn;
            }
            set
            {
                _TemRgn = value;
            }
        }

        public bool TemRgd
        {
            get
            {
                return _TemRgd;
            }
            set
            {
                _TemRgd = value;
            }
        }

        public bool TemLaudoDna
        {
            get
            {
                return _TemLaudoDna;
            }
            set
            {
                _TemLaudoDna = value;
            }
        }

        public bool TemLaudoDna2
        {
            get
            {
                return _TemLaudoDna2;
            }
            set
            {
                _TemLaudoDna2 = value;
            }
        }

        public bool TemLaudoArquivoPermanente
        {
            get
            {
                return _TemLaudoArquivoPermanente;
            }
            set
            {
                _TemLaudoArquivoPermanente = value;
            }
        }

        public bool TemLaudoSecundario1
        {
            get
            {
                return _TemLaudoSecundario1;
            }
            set
            {
                _TemLaudoSecundario1 = value;
            }
        }

        public bool TemLaudoSecundario2
        {
            get
            {
                return _TemLaudoSecundario2;
            }
            set
            {
                _TemLaudoSecundario2 = value;
            }
        }

        public bool TemLaudoBetaCaseina
        {
            get
            {
                return _TemLaudoBetaCaseina;
            }
            set
            {
                _TemLaudoBetaCaseina = value;
            }
        }

        public bool TemLaudoKappaCaseina
        {
            get
            {
                return _TemLaudoKappaCaseina;
            }
            set
            {
                _TemLaudoKappaCaseina = value;
            }
        }

        public bool EhFIV
        {
            get
            {
                return _EhFIV;
            }
            set
            {
                _EhFIV = value;
            }
        }

        public bool EhCria
        {
            get
            {
                return _EhCria;
            }
            set
            {
                _EhCria = value;
            }
        }

        public string TipoParto
        {
            get
            {
                return _TipoParto;
            }
            set
            {
                _TipoParto = value;
            }
        }

        public string VigorBez
        {
            get
            {
                return _VigorBez;
            }
            set
            {
                _VigorBez = value;
            }
        }

        public string EstadoCorporalMae
        {
            get
            {
                return _EstadoCorporalMae;
            }
            set
            {
                _EstadoCorporalMae = value;
            }
        }

        public string TamanhoTeta
        {
            get
            {
                return _TamanhoTeta;
            }
            set
            {
                _TamanhoTeta = value;
            }
        }

        public string MaeBoaLeite
        {
            get
            {
                return _MaeBoaLeite;
            }
            set
            {
                _MaeBoaLeite = value;
            }
        }

        public bool MaeOrdenhada
        {
            get
            {
                return _MaeOrdenhada;
            }
            set
            {
                _MaeOrdenhada = value;
            }
        }

        public bool AnimalImprodutivo
        {
            get
            {
                return _AnimalImprodutivo;
            }
            set
            {
                _AnimalImprodutivo = value;
            }
        }

        public string RgnRaca
        {
            get
            {
                return _RgnRaca;
            }
            set
            {
                _RgnRaca = value;
            }
        }

        public string Temperamento
        {
            get
            {
                return _Temperamento;
            }
            set
            {
                _Temperamento = value;
            }
        }

        public string RegistroRgnRaca
        {
            get
            {
                return _RegistroRgnRaca;
            }
            set
            {
                _RegistroRgnRaca = value;
            }
        }

        public string RegistroRgdRaca
        {
            get
            {
                return _RegistroRgdRaca;
            }
            set
            {
                _RegistroRgdRaca = value;
            }
        }

        public List<Historico> Historicos
        {
            get { return _Historicos; }
            set { _Historicos = value; }
        }

        public List<Mensuracao> Mensuracoes
        {
            get { return _Mensuracoes; }
            set { _Mensuracoes = value; }
        }

    }
}
