using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceAVistaParc
    {
        public string[] ObtemParcelas()
        {
            int parcInicial = 2;
            int parcFinal = 360;
            var parcelas = new List<string>();

            parcelas.Add("");
            parcelas.Add("À Vista");

            for (int i = parcInicial; i <= parcFinal; i++)
            {
                parcelas.Add(i.ToString());
            }

            return parcelas.ToArray();
        }
    }
}