using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais_WebMobile.DataSources
{
    public class DataSourcePesagensLeite
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DataSourcePesagensLeite()
        {
        }

        public ProducaoLeite[] ObtemPesagensLeite(CriterioPesquisaPesagensLeite criterio)
        {
            return _fCarnaubaFacade.ObtemPesagensLeite(criterio);

        }

        public ProducaoLeite ObtemPesagemLeite(int loteID, int pesagemLeiteId)
        {
            return _fCarnaubaFacade.GetPesagemLeiteByIndex(loteID, pesagemLeiteId);
        }

        public ProducaoLeite[] ObtemPesagensLeiteDataSource(CriterioPesquisaPesagensLeite criterio)
        {
            return _fCarnaubaFacade.ObtemPesagensLeite(criterio);
        }

        public void Salve(ProducaoLeite pesagemLeite)
        {
            string[] ids = pesagemLeite.PesagemLoteId.Split(' ');
            int loteId = Convert.ToInt32(ids[0]);
            int pesagemLeiteId = Convert.ToInt32(ids[1]);

            _fCarnaubaFacade.AlterarPesagemLeite(loteId, pesagemLeiteId, pesagemLeite);
        }
    }
}
