using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class CriterioPesquisaParcelas : CriterioPesquisa
    {
        public CriterioPesquisaParcelas()
        {
            IsSearch = false;
        }
        public long IdFinanceiro { get; set; }

        public string TodosOsCampos { get; set; }

        public string Predicate { get; set; }

        public bool IsSearch { get; set; }

        public string Filter
        {
            get
            {
                string filter = "";
                filter = AddParametro(filter, TodosOsCampos);
                if (IdFinanceiro > 0)
                {
                    filter = AddParametro(filter, "id", IdFinanceiro.ToString());
                }

                return filter;
            }
        }
    }
}
