using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ResultadoBuscaAnimal
    {

        long _Id;
        string _Diretorio;
        long _NumeroOrdem;
        string _NomeFazenda;
        string _Nome;
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
        Arquivo _Foto;
        long _CdnOrigem;
        long _CdcOrigem;
        string _Observacoes;
        DateTime _DataUltimoParto;
        string _RgdSerie;
        long _RgdNumero;
        string _RgnSerie;
        long _RgnNumero;
        string _StrId;
        string _StrIdSemHighLight;
        string _StrPaiId;
        string _StrMaeId;
        DateTime _DataCdc;
        string _NCria;
        string _Usuario;
        DateTime _DataUsuario;
        double _Er;
        int _NumeroFilhos;
        string _NomeCompletoPai;
        string _NomePai;
        string _RgdPai;
        string _NomeCompletoMae;
        string _NomeMae;
        string _RgdMae;


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
                _RgdSerie = value;
            }
        }

        public long RgnNumero
        {
            get
            {
                return _RgdNumero;
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


        public string StrIdSemHighLight
        {
            get
            {
                return _StrIdSemHighLight;
            }
            set
            {
                _StrIdSemHighLight = value;
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


        public int NumeroFilhos
        {
            get
            {
                return _NumeroFilhos;
            }
            set
            {
                _NumeroFilhos = value;
            }
        }

        public string NomeCompletoPai
        {
            get
            {
                return _NomeCompletoPai;
            }
            set
            {
                _NomeCompletoPai = value;
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

        public string RgdPai
        {
            get
            {
                return _RgdPai;
            }
            set
            {
                _RgdPai = value;
            }
        }

        public string NomeCompletoMae
        {
            get
            {
                return _NomeCompletoMae;
            }
            set
            {
                _NomeCompletoMae = value;
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

        public string RgdMae
        {
            get
            {
                return _RgdMae;
            }
            set
            {
                _RgdMae = value;
            }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (ResultadoBuscaAnimal)obj;

            return StrIdSemHighLight.Equals(other.StrIdSemHighLight);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
