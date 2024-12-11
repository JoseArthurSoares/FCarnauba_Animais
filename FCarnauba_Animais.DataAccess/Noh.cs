using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    public class Noh
    {
        long _Id;
        string _strId;
        string _IdPai;
        string _IdMae;
        string _CaminhoPai;
        string _CaminhoMae;
        string _CaminhoAcumulado;

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

        public string StrId
        {
            get
            {
                return _strId;
            }
            set
            {
                _strId = value;
            }
        }

        public string IdPai
        {
            get
            {
                return _IdPai;
            }
            set
            {
                _IdPai = value;
            }
        }

        public string IdMae
        {
            get
            {
                return _IdMae;
            }
            set
            {
                _IdMae = value;
            }
        }

        public string CaminhoPai
        {
            get
            {
                return _CaminhoPai;
            }
            set
            {
                _CaminhoPai = value;
            }
        }

        public string CaminhoMae
        {
            get
            {
                return _CaminhoMae;
            }
            set
            {
                _CaminhoMae = value;
            }
        }

        public string CaminhoAcumulado
        {
            get
            {
                return _CaminhoAcumulado;
            }
            set
            {
                _CaminhoAcumulado = value;
            }
        }
    }

}
