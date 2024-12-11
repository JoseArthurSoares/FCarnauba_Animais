using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourcePropriedades
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public Propriedade[] ObtemPropriedades()
        {
            return _fCarnaubaFacade.ObtemPropriedades();

        }

        public Propriedade ObtemPropriedade(int idPropriedade)
        {
            return _fCarnaubaFacade.ObtemPropriedade(idPropriedade);
        }

        public Propriedade ObtemPropriedadeComp(string idsPropriedadesComp)
        {
            return _fCarnaubaFacade.ObtemPropriedadeComp(idsPropriedadesComp);
        }

        public Propriedade[] ObtemPropriedadesComp()
        {
            return _fCarnaubaFacade.ObtemPropriedadesComp();

        }
    }
}