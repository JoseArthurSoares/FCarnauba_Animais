using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ParametrosDeBuscaEmLotes : CriterioPesquisa
    {
        public ParametrosDeBuscaEmLotes()
        {
            IsSearch = false;
        }

        public long Id { get; set; }

        public string Lote { get; set; }

        public DateTime? DataControle { get; set; }

        public DateTime? DataControleInicial { get; set; }

        public DateTime? DataControleFinal { get; set; }

        public string IdPropriedade { get; set; }

        public string Raca { get; set; }

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
                if (Lote != null)
                {
                    filter = AddParametro(filter, "strLote", Lote.ToString());
                }

                if (DataControle != null)
                    filter = AddParametroData(filter, "dtDataControle", DataControle.Value.ToShortDateString(), ">=");
                if (DataControleInicial != null)
                    filter = AddParametroData(filter, "dtDataControle", DataControleInicial.Value.ToShortDateString(), ">=");
                if (DataControleFinal != null)
                    filter = AddParametroData(filter, "dtDataControle", DataControleFinal.Value.ToShortDateString(), "<=");

                filter = AddParametro(filter, "strIdPropriedade", IdPropriedade);

                filter = AddParametro(filter, "strRaca", Raca);

                return filter;
            }
        }
    }
}
