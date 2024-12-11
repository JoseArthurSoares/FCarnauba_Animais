using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCarnauba_Animais.DataAccess;
using Newtonsoft.Json;
using System.Net;

namespace FCarnauba_Animais.DataSources
{
    public class DataSourceCidades
    {
        public Municipios[] ObtemMunicipios()
        {
            string url = "http://servicodados.ibge.gov.br/api/v1/localidades/municipios";
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            string json = (client).DownloadString(url);
            //var response = client.DownloadString(new Uri(HttpContext.Current.Server.MapPath("~/Scripts/uf.json")));
            var mun = JsonConvert.DeserializeObject<List<Municipios>>(json);

            var munOrd = mun.OrderBy(s => s.Nome);

            var DistinctItems = new List<Municipios>(munOrd.GroupBy(x => x.Nome).Select(y => y.First()));

            return DistinctItems.ToArray();
        }
    }
}