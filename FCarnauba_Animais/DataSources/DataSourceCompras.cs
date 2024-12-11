using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceCompras
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DataSourceCompras()
        {
        }

        //public Compra[] ObtemCompras()
        //{
        //    return _fCarnaubaFacade.ObtemCompras();

        //}

        public Compra[] ObtemCompras(CriterioPesquisaCompras criterio)
        {
            return _fCarnaubaFacade.ObtemCompras(criterio);

        }

        public Compra ObtemCompra(int itemFinanceiroID, int compraId)
        {
            return _fCarnaubaFacade.GetCompraByIndex(itemFinanceiroID, compraId);
        }

        public Compra[] ObtemComprasDataSource(CriterioPesquisaCompras criterio)
        {
            return _fCarnaubaFacade.ObtemCompras(criterio);
        }

        public void Salve(Compra compra)
        {
            string[] ids = compra.CompraFinanceiroId.Split(' ');
            int financeiroId = Convert.ToInt32(ids[0]);
            int compraId = Convert.ToInt32(ids[1]);

            _fCarnaubaFacade.AlterarCompra(financeiroId, compraId, compra);
        }

        public void Insira(string itemFinanceiroId, Compra compra)
        {
            _fCarnaubaFacade.InserirCompra(itemFinanceiroId, compra);
        }

        public void Remova(Compra compra)
        {
            string[] ids = compra.CompraFinanceiroId.Split(' ');
            int financeiroId = Convert.ToInt32(ids[0]);
            int compraId = Convert.ToInt32(ids[1]);

            _fCarnaubaFacade.RemoverCompra(financeiroId, compraId);
        }
    }
}