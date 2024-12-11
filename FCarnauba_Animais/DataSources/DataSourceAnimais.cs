using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceAnimais
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public Animal[] ObtemAnimais()
        {
            return _fCarnaubaFacade.GetAnimaisComVazio().ToArray();

        }

        public Animal ObtemAnimal(int idAnimal)
        {
            return _fCarnaubaFacade.GetAnimalById(idAnimal.ToString());
        }

        public ResultadoBuscaAnimal[] ObtemAnimais(ParametrosDeBuscaEmAnimais criterio)
        {
            return _fCarnaubaFacade.ConsultaAnimal(criterio).ToArray();
        }

        public Animal[] ObtemDdlAnimais(CriterioPesquisaAnimal criterio)
        {
            return _fCarnaubaFacade.ConsultaDdlAnimal(criterio);
        }
    }
}