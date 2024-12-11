using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourcePluviometria
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public DataSourcePluviometria()
        {
        }

        public ControlePluviometrico[] ObtemPluviometriasDataSource(string criterio)
        {
            return _fCarnaubaFacade.ObtemPluviometrias(criterio);
        }

        public ControlePluviometrico[] ObtemPluviometrias(ParametrosDeBuscaEmControlePluviometrico criterio)
        {
            return _fCarnaubaFacade.ConsultaPluviometria(criterio);
        }

        public void Salve(ControlePluviometrico Pluviometria)
        {
            _fCarnaubaFacade.AlterarPluviometria(Pluviometria);
        }

        public void Insira(ControlePluviometrico Pluviometria)
        {
            _fCarnaubaFacade.InserirPluviometria(Pluviometria);
        }

        public void Remova(ControlePluviometrico Pluviometria)
        {
            _fCarnaubaFacade.RemoverPluviometria(Pluviometria.Id);
        }
    }
}