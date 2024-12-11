using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceParcelas
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DataSourceParcelas()
        {
        }

        public Parcela[] ObtemParcelas(CriterioPesquisaParcelas criterio)
        {
            return _fCarnaubaFacade.ObtemParcelas(criterio);

        }

        public Parcela ObtemParcela(int itemFinanceiroID, int parcelaId)
        {
            return _fCarnaubaFacade.GetParcelaByIndex(itemFinanceiroID, parcelaId);
        }

        public Parcela[] ObtemParcelasDataSource(CriterioPesquisaParcelas criterio)
        {
            return _fCarnaubaFacade.ObtemParcelas(criterio);
        }

        public void Salve(Parcela parcela)
        {
            string[] ids = parcela.ParcelaFinanceiroId.Split(' ');
            int financeiroId = Convert.ToInt32(ids[0]);
            int parcelaId = Convert.ToInt32(ids[1]);

            _fCarnaubaFacade.AlterarParcela(financeiroId, parcelaId, parcela);
        }

        public void Insira(string itemFinanceiroId, Parcela parcela)
        {
            _fCarnaubaFacade.InserirParcela(itemFinanceiroId, parcela);
        }

        public void Remova(Parcela parcela)
        {
            string[] ids = parcela.ParcelaFinanceiroId.Split(' ');
            int financeiroId = Convert.ToInt32(ids[0]);
            int parcelaId = Convert.ToInt32(ids[1]);

            _fCarnaubaFacade.RemoverParcela(financeiroId, parcelaId);
        }
    }
}