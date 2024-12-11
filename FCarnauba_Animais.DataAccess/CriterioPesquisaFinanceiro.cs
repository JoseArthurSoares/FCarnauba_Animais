using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class CriterioPesquisaFinanceiro : CriterioPesquisa
    {
        public CriterioPesquisaFinanceiro()
        {
            IsSearch = false;
        }

        public long IdFinanceiro { get; set; }

        public DateTime? Data { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public double ValorTotal { get; set; }

        public double ValorTotalInicial { get; set; }

        public double ValorTotalFinal { get; set; }

        public int IdPropriedade { get; set; }

        public int IdEmpresa { get; set; }

        public bool Venda { get; set; }

        public long IdGrupo { get; set; }

        public string Descricao { get; set; }

        public string PropriedadeComp { get; set; }

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

                if (Data != null)
                    filter = AddParametroData(filter, "dtData", Data.Value.ToShortDateString(), ">=");
                if (DataInicio != null)
                    filter = AddParametroData(filter, "dtData", DataInicio.Value.ToShortDateString(), ">=");
                if (DataFim != null)
                    filter = AddParametroData(filter, "dtData", DataFim.Value.ToShortDateString(), "<=");

                if (IdGrupo > 0)
                {
                    filter = AddParametro(filter, "FK_intID_Grupo", IdGrupo.ToString());
                }

                if (IdEmpresa > 0)
                {
                    filter = AddParametro(filter, "FK_IdEmpresa", IdEmpresa.ToString());
                }

                if (Venda)
                {
                    filter = AddParametro(filter, "vfVendaAnimais", "1");
                }


                if (ValorTotal > 0)
                    filter = AddParametroData(filter, "moeValorTotal", ValorTotal.ToString(), ">=");
                if (ValorTotalInicial > 0)
                    filter = AddParametroData(filter, "moeValorTotal", ValorTotalInicial.ToString(), ">=");
                if (ValorTotalFinal > 0)
                    filter = AddParametroData(filter, "moeValorTotal", ValorTotalFinal.ToString(), "<=");

                filter = AddParametro(filter, "strDescricao", Descricao);

                filter = AddParametroTextual(filter, "strPropriedadeComp", PropriedadeComp);

                return filter;
            }
        }


    }
}
