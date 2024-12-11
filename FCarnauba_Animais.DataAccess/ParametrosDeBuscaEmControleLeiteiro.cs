using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ParametrosDeBuscaEmControleLeiteiro : CriterioPesquisa
    {
        public ParametrosDeBuscaEmControleLeiteiro()
        {
            IsSearch = false;
        }

        public long Id { get; set; }

        public string Raca { get; set; }

        public string Categoria { get; set; }

        public string Fazenda { get; set; }

        public DateTime? DataControle { get; set; }

        public DateTime? DataControleInicial { get; set; }

        public DateTime? DataControleFinal { get; set; }

        public string IdLote { get; set; }

        public string TodosOsCampos { get; set; }

        public string Predicate { get; set; }

        public bool IsSearch { get; set; }

        public string Filter
        {
            get
            {
                string filter = "";
                filter = AddParametro(filter, TodosOsCampos);
                filter = AddParametro(filter, "id", Id.ToString());
                filter = AddParametro(filter, "strRaca", Raca);
                filter = AddParametro(filter, "strCategoria", Categoria);
                filter = AddParametro(filter, "strFazenda", Fazenda);

                if (DataControle != null)
                    filter = AddParametroData(filter, "dtDataControle", DataControle.Value.ToShortDateString(), ">=");
                if (DataControleInicial != null)
                    filter = AddParametroData(filter, "dtDataControle", DataControleInicial.Value.ToShortDateString(), ">=");
                if (DataControleFinal != null)
                    filter = AddParametroData(filter, "dtDataControle", DataControleFinal.Value.ToShortDateString(), "<=");

                filter = AddParametro(filter, "FK_strIdLote", IdLote);

                return filter;
            }
        }
    }
}
