using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ResultadoBuscaLote
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
        bool _LiberarLotePesagem;
        string _LiberarLotePesagemStr;

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

        public bool LiberarLotePesagem
        {
            get
            {
                return _LiberarLotePesagem;
            }
            set
            {
                _LiberarLotePesagem = value;
            }
        }

        public string LiberarLotePesagemStr
        {
            get
            {
                return _LiberarLotePesagemStr;
            }
            set
            {
                _LiberarLotePesagemStr = value;
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
