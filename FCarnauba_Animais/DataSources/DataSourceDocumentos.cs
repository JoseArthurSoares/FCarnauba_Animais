using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceDocumentos
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DataSourceDocumentos()
        {
        }

        public Documento[] ObtemDocumentos(CriterioPesquisaDocumentos criterio)
        {
            return _fCarnaubaFacade.ObtemDocumentos(criterio);

        }

        public Documento ObtemDocumento(int itemFinanceiroID, int documentoId)
        {
            return _fCarnaubaFacade.GetDocumentoByIndex(itemFinanceiroID, documentoId);
        }

        public Documento[] ObtemDocumentosDataSource(CriterioPesquisaDocumentos criterio)
        {
            return _fCarnaubaFacade.ObtemDocumentos(criterio);
        }

        public void Salve(Documento documento)
        {
            string[] ids = documento.DocumentoFinanceiroId.Split(' ');
            int financeiroId = Convert.ToInt32(ids[0]);
            int documentoId = Convert.ToInt32(ids[1]);

            _fCarnaubaFacade.AlterarDocumento(financeiroId, documentoId, documento);
        }

        public void Insira(string itemFinanceiroId, Documento documento)
        {
            _fCarnaubaFacade.InserirDocumento(itemFinanceiroId, documento);
        }

        public void Remova(Documento documento)
        {
            string[] ids = documento.DocumentoFinanceiroId.Split(' ');
            int financeiroId = Convert.ToInt32(ids[0]);
            int documentoId = Convert.ToInt32(ids[1]);

            _fCarnaubaFacade.RemoverDocumento(financeiroId, documentoId);
        }
    }
}