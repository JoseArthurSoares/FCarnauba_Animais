using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceItensDescricao
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public ItemDescFinanceiro[] ObtemItensDescricao()
        {
            return _fCarnaubaFacade.GetDescricoesFinanceiro();

        }
    }
}