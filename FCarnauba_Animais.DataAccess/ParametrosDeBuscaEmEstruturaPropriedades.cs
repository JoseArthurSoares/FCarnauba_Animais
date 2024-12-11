using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ParametrosDeBuscaEmEstruturaPropriedades : CriterioPesquisa
    {
        public ParametrosDeBuscaEmEstruturaPropriedades()
        {
            IsSearch = false;
        }

        public long Id { get; set; }

        public DateTime? Data { get; set; }

        public DateTime? DataInicial { get; set; }

        public DateTime? DataFinal { get; set; }

        public string IdPropriedade { get; set; }

        public string NomePropriedade { get; set; }

        public string TodosOsCampos { get; set; }

        public string Predicate { get; set; }

        public bool IsSearch { get; set; }

        public string Filter
        {
            get
            {
                string filter = "";
                filter = AddParametro(filter, TodosOsCampos);
                if (Id > 0)
                {
                    filter = AddParametro(filter, "id", Id.ToString());
                }

                filter = AddParametro(filter, "strNome", NomePropriedade);

                return filter;
            }
        }

    }
}
