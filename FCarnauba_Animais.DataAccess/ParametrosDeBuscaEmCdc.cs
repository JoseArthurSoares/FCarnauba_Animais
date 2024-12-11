using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ParametrosDeBuscaEmCdc : CriterioPesquisa
    {
        public ParametrosDeBuscaEmCdc()
        {
            IsSearch = false;
        }

        public long Id { get; set; }

        public string Cdc { get; set; }

        public string Tipo { get; set; }

        public DateTime? DataCobertura { get; set; }

        public DateTime? DataCoberturaInicial { get; set; }

        public DateTime? DataCoberturaFinal { get; set; }

        public DateTime? DataImplantacao { get; set; }

        public DateTime? DataImplantacaoInicial { get; set; }

        public DateTime? DataImplantacaoFinal { get; set; }

        public string IdTouro { get; set; }

        public string IdPropriedade { get; set; }

        public string Veterinario { get; set; }

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
                if (Cdc != null)
                {
                    filter = AddParametro(filter, "intCdc", Cdc.ToString());
                }

                if (DataCobertura != null)
                    filter = AddParametroData(filter, "dtDataCobertura", DataCobertura.Value.ToShortDateString(), ">=");
                if (DataCoberturaInicial != null)
                    filter = AddParametroData(filter, "dtDataCobertura", DataCoberturaInicial.Value.ToShortDateString(), ">=");
                if (DataCoberturaFinal != null)
                    filter = AddParametroData(filter, "dtDataCobertura", DataCoberturaFinal.Value.ToShortDateString(), "<=");

                if (DataImplantacao != null)
                    filter = AddParametroData(filter, "dtDataImplantacao", DataImplantacao.Value.ToShortDateString(), ">=");
                if (DataImplantacaoInicial != null)
                    filter = AddParametroData(filter, "dtDataImplantacao", DataImplantacaoInicial.Value.ToShortDateString(), ">=");
                if (DataImplantacaoFinal != null)
                    filter = AddParametroData(filter, "dtDataImplantacao", DataImplantacaoFinal.Value.ToShortDateString(), "<=");

                filter = AddParametro(filter, "FK_strIdTouro", IdTouro);

                filter = AddParametro(filter, "strIdPropriedade", IdPropriedade);

                filter = AddParametro(filter, "strVeterinario", Veterinario);

                filter = AddParametro(filter, "strRaca", Raca);

                return filter;
            }
        }
    }
}
