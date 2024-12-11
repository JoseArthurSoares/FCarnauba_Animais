using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class CriterioPesquisaVendas : CriterioPesquisa
    {
        public CriterioPesquisaVendas()
        {
            IsSearch = false;
        }

        public long IdEmpresa { get; set; }

        public string TodosOsCampos { get; set; }

        public string Predicate { get; set; }

        public bool IsSearch { get; set; }

        public string Filter
        {
            get
            {
                string filter = "";
                filter = AddParametro(filter, TodosOsCampos);
                if (IdEmpresa > 0)
                {
                    filter = AddParametro(filter, "id", IdEmpresa.ToString());
                }

                return filter;
            }
        }
    }
}
