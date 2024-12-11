using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ItemDescFinanceiro
    {
        int _Id;
        string _Diretorio;
        string _Descricao;
        int _IdGrupo;

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

        public string Descricao
        {
            get
            {
                return _Descricao;
            }
            set
            {
                _Descricao = value;
            }
        }

        public int IdGrupo
        {
            get
            {
                return _IdGrupo;
            }
            set
            {
                _IdGrupo = value;
            }
        }
    }
}
