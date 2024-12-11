using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Propriedade
    {
        int _Id;
        string _Diretorio;
        string _Nome;
        string _IdsPropriedades;
        string _Localizacao;
        double _Area;
        double _AreaUtilizada;
        double _AreaPreservacao;
        string _Municipio;
        string _Uf;

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

        public string Localizacao
        {
            get
            {
                return _Localizacao;
            }
            set
            {
                _Localizacao = value;
            }
        }

        public string IdsPropriedades
        {
            get
            {
                return _IdsPropriedades;
            }
            set
            {
                _IdsPropriedades = value;
            }
        }

        public double Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
            }
        }

        public double AreaUtilizada
        {
            get
            {
                return _AreaUtilizada;
            }
            set
            {
                _AreaUtilizada = value;
            }
        }

        public double AreaPreservacao
        {
            get
            {
                return _AreaPreservacao;
            }
            set
            {
                _AreaPreservacao = value;
            }
        }

        public string Municipio
        {
            get
            {
                return _Municipio;
            }
            set
            {
                _Municipio = value;
            }
        }

        public string Uf
        {
            get
            {
                return _Uf;
            }
            set
            {
                _Uf = value;
            }
        }

    }
}
