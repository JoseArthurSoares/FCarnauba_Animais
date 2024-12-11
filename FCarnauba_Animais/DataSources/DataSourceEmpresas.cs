using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceEmpresas
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DataSourceEmpresas()
        {
        }

        public Empresa[] ObtemEmpresas()
        {
            return _fCarnaubaFacade.ObtemEmpresas();

        }

        public Empresa ObtemEmpresa(int idEmpresa)
        {
            return _fCarnaubaFacade.ObtemEmpresa(idEmpresa);
        }

        public Empresa[] ObtemEmpresasDataSource(string criterio)
        {
            return _fCarnaubaFacade.ObtemEmpresas(criterio);
        }

        public Empresa[] ObtemEmpresas(CriterioPesquisaEmpresa criterio)
        {
            return _fCarnaubaFacade.ConsultaEmpresa(criterio);
        }

        public void Salve(Empresa empresa)
        {
            _fCarnaubaFacade.AlterarEmpresa(empresa);
        }

        public void Insira(Empresa empresa)
        {
            _fCarnaubaFacade.InserirEmpresa(empresa);
        }

        public void Remova(Empresa empresa)
        {
            _fCarnaubaFacade.RemoverEmpresa(empresa.IdEmpresa);
        }
    }
}