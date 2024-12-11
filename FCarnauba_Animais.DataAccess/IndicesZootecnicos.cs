using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class IndicesZootecnicos
    {
        double _Lotacao;
        double _TaxaFertilidade;
        double _TaxaNatalidade;
        double _TaxaMortalidade;
        double _GmdGlobal;
        double _ErMedia;
        double _IppIepMedios;
        double _TaxaDesmame;
        double _TaxaDesfrute;
        double _TaxaCrescimentoVegetativo;
        double _ProducaoLeite;
        double _RqMedio;

        public double Lotacao
        {
            get
            {
                return _Lotacao;
            }
            set
            {
                _Lotacao = value;
            }
        }

        public double TaxaFertilidade
        {
            get
            {
                return _TaxaFertilidade;
            }
            set
            {
                _TaxaFertilidade = value;
            }
        }

        public double TaxaNatalidade
        {
            get
            {
                return _TaxaNatalidade;
            }
            set
            {
                _TaxaNatalidade = value;
            }
        }

        public double TaxaMortalidade
        {
            get
            {
                return _TaxaMortalidade;
            }
            set
            {
                _TaxaMortalidade = value;
            }
        }

        public double GmdGlobal
        {
            get
            {
                return _GmdGlobal;
            }
            set
            {
                _GmdGlobal = value;
            }
        }

        public double ErMedia
        {
            get
            {
                return _ErMedia;
            }
            set
            {
                _ErMedia = value;
            }
        }

        public double IppIepMedios
        {
            get
            {
                return _IppIepMedios;
            }
            set
            {
                _IppIepMedios = value;
            }
        }

        public double TaxaDesmame
        {
            get
            {
                return _TaxaDesmame;
            }
            set
            {
                _TaxaDesmame = value;
            }
        }

        public double TaxaDesfrute
        {
            get
            {
                return _TaxaDesfrute;
            }
            set
            {
                _TaxaDesfrute = value;
            }
        }

        public double TaxaCrescimentoVegetativo
        {
            get
            {
                return _TaxaCrescimentoVegetativo;
            }
            set
            {
                _TaxaCrescimentoVegetativo = value;
            }
        }

        public double ProducaoLeite
        {
            get
            {
                return _ProducaoLeite;
            }
            set
            {
                _ProducaoLeite = value;
            }
        }

        public double RqMedio
        {
            get
            {
                return _RqMedio;
            }
            set
            {
                _RqMedio = value;
            }
        }
    }
}
