using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class CriterioPesquisaEmpresa : CriterioPesquisa
    {
        public CriterioPesquisaEmpresa()
        {
            IsSearch = false;
        }

        public long IdEmpresa { get; set; }

        public string RazaoSocial { get; set; }

        public string CnpjCpf { get; set; }

        public string Endereco { get; set; }

        public string Uf { get; set; }

        public string TodosOsCampos { get; set; }

        public string Predicate { get; set; }

        public bool IsSearch { get; set; }

        public string Filter
        {
            get
            {
                string filter = "";
                filter = AddParametroTextual(filter, TodosOsCampos);
                if (IdEmpresa > 0)
                {
                    filter = AddParametro(filter, "id", IdEmpresa.ToString());
                }

                filter = AddParametro(filter, "strRazaoSocial", RazaoSocial);

                filter = AddParametro(filter, "strCnpjCpf", CnpjCpf);

                filter = AddParametro(filter, "strUf", Uf);

                filter = AddParametro(filter, "strEndereco", Endereco);

                return filter;
            }
        }
    }
}
