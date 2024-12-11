using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceFinanceiros
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DataSourceFinanceiros()
        {
        }

        public ItemFinanceiro[] ObtemFinanceirosDataSource(string criterio)
        {
            return _fCarnaubaFacade.ObtemFinanceiros(criterio);
        }

        public ItemFinanceiro[] ObtemFinanceiros(CriterioPesquisaFinanceiro criterio)
        {
            return _fCarnaubaFacade.ConsultaFinanceiro(criterio);
        }

        public void Salve(ItemFinanceiro Financeiro)
        {
            _fCarnaubaFacade.AlterarFinanceiro(Financeiro);
        }

        public void Valida(long idFinanceiro, string usuarioValida, DateTime dataUsuarioValida)
        {
            _fCarnaubaFacade.ValidarFinanceiro(idFinanceiro, usuarioValida, dataUsuarioValida);
        }

        public long Insira(ItemFinanceiro Financeiro)
        {
            long ultimoId = _fCarnaubaFacade.InserirFinanceiro(Financeiro);
            return ultimoId;
        }

        public void Remova(ItemFinanceiro Financeiro)
        {
            _fCarnaubaFacade.RemoverFinanceiro(Financeiro.IdFinanceiro);
        }

        public bool ItemFinanceiroValidado(long idFinanceiro)
        {
            return _fCarnaubaFacade.ItemFinanceiroValidado(idFinanceiro);
        }

        public void InserirParcelas(DateTime data, double valorTotal, int nParcelas, long itemFinanceiroId)
        {
            _fCarnaubaFacade.InserirParcelas(data, valorTotal, nParcelas, itemFinanceiroId);
        }
    }
}