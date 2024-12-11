using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ResultadoBuscaLotePonderal
    {
        long _Id;
        string _IdSemHighLight;
        string _Diretorio;
        string _SLote;
        DateTime _DataControle;
        string _LoteDataPropriedade;
        string _Usuario;
        DateTime _DataUsuario;
        string _IdPropriedade;
        string _NomePropriedade;
        bool _LiberarLoteMensuracao;
        string _LiberarLoteMensuracaoStr;

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

        public string IdSemHighLight
        {
            get
            {
                return _IdSemHighLight;
            }
            set
            {
                _IdSemHighLight = value;
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

        public string LoteDataPropriedade
        {
            get
            {
                return _LoteDataPropriedade;
            }
            set
            {
                _LoteDataPropriedade = value;
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

        public string IdPropriedade
        {
            get
            {
                return _IdPropriedade;
            }
            set
            {
                _IdPropriedade = value;
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

        public bool LiberarLoteMensuracao
        {
            get
            {
                return _LiberarLoteMensuracao;
            }
            set
            {
                _LiberarLoteMensuracao = value;
            }
        }

        public string LiberarLoteMensuracaoStr
        {
            get
            {
                return _LiberarLoteMensuracaoStr;
            }
            set
            {
                _LiberarLoteMensuracaoStr = value;
            }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (ResultadoBuscaLote)obj;

            return IdSemHighLight.Equals(other.IdSemHighLight);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
