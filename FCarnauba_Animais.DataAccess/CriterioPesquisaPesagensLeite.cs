using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class CriterioPesquisaPesagensLeite : CriterioPesquisa
    {

        public CriterioPesquisaPesagensLeite()
        {
            IsSearch = false;
        }
        public long IdLote { get; set; }

        public string TodosOsCampos { get; set; }

        public string Predicate { get; set; }

        public bool IsSearch { get; set; }

        public string Filter
        {
            get
            {
                string filter = "";
                filter = AddParametro(filter, TodosOsCampos);
                if (IdLote > 0)
                {
                    filter = AddParametro(filter, "id", IdLote.ToString());
                }

                return filter;
            }
        }
    }
}
