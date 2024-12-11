using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ParametrosDeBuscaEmAnimais : CriterioPesquisa
    {
        public ParametrosDeBuscaEmAnimais()
        {
            IsSearch = false;
        }

        public string StrId { get; set; }

        public string NomeFazenda { get; set; }

        public string Raca { get; set; }

        public long NumeroOrdem { get; set; }

        public string Nome { get; set; }

        public string NomeCompleto { get; set; }

        public string Sexo { get; set; }

        public DateTime? DataNascimento { get; set; }

        public DateTime? DataNascimentoInicial { get; set; }

        public DateTime? DataNascimentoFinal { get; set; }

        public string Rgd { get; set; }

        public string StrPaiId { get; set; }

        public string StrMaeId { get; set; }

        public string BetaCaseina { get; set; }

        public string KappaCaseina { get; set; }

        public bool Fiv { get; set; }

        public string Movimento { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataPesagem { get; set; }

        public DateTime? DataPesagemInicial { get; set; }

        public DateTime? DataPesagemFinal { get; set; }

        public double Peso { get; set; }

        public double PesoInicial { get; set; }

        public double PesoFinal { get; set; }

        public string TodosOsCampos { get; set; }

        public string Predicate { get; set; }

        public bool IsSearch { get; set; }

        public string Filter
        {
            get
            {
                string filter = "";
                filter = AddParametro(filter, TodosOsCampos);
                filter = AddParametro(filter, "strId", StrId);
                filter = AddParametro(filter, "strNomeFazenda", NomeFazenda);
                filter = AddParametro(filter, "strRaca", Raca);
                filter = AddParametro(filter, "intNumeroOrdem", NumeroOrdem.ToString());
                filter = AddParametro(filter, "strNome", Nome);
                filter = AddParametro(filter, "strNomeCompleto", NomeCompleto);
                filter = AddParametro(filter, "strSexo", Sexo);
                if (DataNascimento != null)
                    filter = AddParametroData(filter, "dtDataNascimento", DataNascimento.Value.ToShortDateString(), ">=");
                if (DataNascimentoInicial != null)
                    filter = AddParametroData(filter, "dtDataNascimento", DataNascimentoInicial.Value.ToShortDateString(), ">=");
                if (DataNascimentoFinal != null)
                    filter = AddParametroData(filter, "dtDataNascimento", DataNascimentoFinal.Value.ToShortDateString(), "<=");
                filter = AddParametro(filter, "strRgd", Rgd);
                filter = AddParametro(filter, "strPaiId", StrPaiId);
                filter = AddParametro(filter, "strMaeId", StrMaeId);
                filter = AddParametro(filter, "strNomeFazenda", NomeFazenda);
                filter = AddParametro(filter, "strTipoBetaCaseina", BetaCaseina);
                filter = AddParametro(filter, "strTipoKappaCaseina", KappaCaseina);
                filter = AddParametro(filter, "vfFiv", Fiv.ToString());
                filter = AddParametro(filter, "MA_strMovimento", Movimento);
                filter = AddParametro(filter, "MA_docObservacao", Observacao);
                if (DataPesagem != null)
                    filter = AddParametroData(filter, "CP_dtDataPesagem", DataPesagem.Value.ToShortDateString(), ">=");
                if (DataPesagemInicial != null)
                    filter = AddParametroData(filter, "CP_dtDataPesagem", DataPesagemInicial.Value.ToShortDateString(), ">=");
                if (DataPesagemFinal != null)
                    filter = AddParametroData(filter, "CP_dtDataPesagem", DataPesagemFinal.Value.ToShortDateString(), "<=");
                if (Peso != null)
                    filter = AddParametroData(filter, "CP_decPesoFinal", Peso.ToString(), ">=");
                if (PesoInicial != null)
                    filter = AddParametroData(filter, "CP_decPesoFinal", PesoInicial.ToString(), ">=");
                if (PesoFinal != null)
                    filter = AddParametroData(filter, "CP_decPesoFinal", PesoFinal.ToString(), "<=");

                return filter;
            }
        }


    }
}
