using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class CriterioPesquisaAnimal : CriterioPesquisa
    {
        public CriterioPesquisaAnimal()
        {
            IsSearch = false;
        }

        public long Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Rgd { get; set; }

        public string TodosOsCampos { get; set; }

        public string Predicate { get; set; }

        public bool IsSearch { get; set; }

        public string Filter
        {
            get
            {
                string filter = "";
                filter = AddParametroTextual(filter, TodosOsCampos);
                if (Id > 0)
                {
                    filter = AddParametro(filter, "id", Id.ToString());
                }

                filter = AddParametro(filter, "strNomeCompleto", NomeCompleto);

                filter = AddParametro(filter, "strRgd", Rgd);

                return filter;
            }
        }

        
    }
}
