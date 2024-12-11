using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ParametrosDeBuscaEmControlePluviometrico : CriterioPesquisa
    {
        public ParametrosDeBuscaEmControlePluviometrico()
        {
            IsSearch = false;
        }

        public long Id { get; set; }

        public string Diretorio { get; set; }

        public DateTime? Data { get; set; }

        public DateTime? DataInicial { get; set; }

        public DateTime? DataFinal { get; set; }

        public double Pluviometria { get; set; }

        public double PluviometriaInicial { get; set; }

        public double PluviometriaFinal { get; set; }

        public string IdPropriedade { get; set; }

        public string Pluviometro { get; set; }

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

                filter = AddParametro(filter, "diretorio", Diretorio);

                if (Data != null)
                    filter = AddParametroData(filter, "dtData", Data.Value.ToShortDateString(), ">=");
                if (DataInicial != null)
                    filter = AddParametroData(filter, "dtData", DataInicial.Value.ToShortDateString(), ">=");
                if (DataFinal != null)
                    filter = AddParametroData(filter, "dtData", DataFinal.Value.ToShortDateString(), "<=");

                filter = AddParametro(filter, "strPluviometro", Pluviometro);

                return filter;
            }
        }

    }
}
