using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceGrupos
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();

        public GrupoFinanceiro[] ObtemGrupos(string criterio)
        {
            return _fCarnaubaFacade.ObtemGrupos(criterio);
            
        }

        public GrupoFinanceiro[] ObtemFilhosDe(int idGrupo)
        {
            return _fCarnaubaFacade.ObtemFilhosDe(idGrupo);
            
        }

        public GrupoFinanceiro[] ObtemGrupo(long idGrupo, bool comHierarquia)
        {
            return _fCarnaubaFacade.ObtemGrupo(idGrupo, comHierarquia);
            
        }

        public int GetEntradaDesembolsoIdGrupo(int idGrupo)
        {
            return _fCarnaubaFacade.GetEntradaDesembolsoIdGrupo(idGrupo);
        }
    }
}