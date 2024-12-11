using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.XPath;

namespace FCarnauba_Animais.DataAccess
{
    public class FCarnaubaSettings
    {

        public static readonly string RepositorioFinanceiroPrincipal;
        public static readonly string RepositorioFinanceiroTemporario;
        public static readonly string RepositorioAnimalPrincipal;
        public static readonly string RepositorioAnimalTemporario;
        public static readonly string RepositorioIntegracao;
        public static readonly string RepositorioJson;
        public static readonly string UserLbw;
        public static readonly string SenhaLbw;
        public static readonly string ConnLbw;
        public static readonly string UserSql;
        public static readonly string SenhaSql;
        public static readonly string ConnSql;
        public static readonly string RepositorioSessions;
		public static readonly string RepositorioVariaveis;
        public static readonly string DocumentosGerados;

        public const string FCarnaubaConfigPath = @"C:\FCarnaubaConfig.xml";
        private static XPathDocument _docNav;
		

    	public static XPathNavigator ReadConfig()
        {
            try
            {
                _docNav = new XPathDocument(Path.Combine(HttpRuntime.AppDomainAppPath, "FCarnaubaConfig.xml"));
            } catch
            {
                _docNav = new XPathDocument(FCarnaubaConfigPath);
            }
            return _docNav.CreateNavigator();
        }

        static FCarnaubaSettings()
        {
            
            var nav = ReadConfig();
            RepositorioFinanceiroPrincipal = nav.SelectSingleNode("/Settings/Repositories/FinMain").Value;
            RepositorioFinanceiroTemporario = nav.SelectSingleNode("/Settings/Repositories/FinTemp").Value;
            RepositorioAnimalPrincipal = nav.SelectSingleNode("/Settings/Repositories/AniMain").Value;
            RepositorioAnimalTemporario = nav.SelectSingleNode("/Settings/Repositories/AniTemp").Value;
            RepositorioJson = nav.SelectSingleNode("/Settings/Repositories/Json").Value;
            RepositorioSessions = nav.SelectSingleNode("/Settings/Repositories/Sessions").Value;
            DocumentosGerados = nav.SelectSingleNode("/Settings/Repositories/DocumentosGerados").Value;
			RepositorioVariaveis = nav.SelectSingleNode("/Settings/Repositories/Var").Value;
            UserLbw = nav.SelectSingleNode("/Settings/Connections/Lb/User").Value;
            SenhaLbw = nav.SelectSingleNode("/Settings/Connections/Lb/Pass").Value;
            //ConnLbw = nav.SelectSingleNode("/Settings/Connections/Lb/String").Value;
            UserSql = nav.SelectSingleNode("/Settings/Connections/Sql/User").Value;
            SenhaSql = nav.SelectSingleNode("/Settings/Connections/Sql/Pass").Value;
            ConnSql = nav.SelectSingleNode("/Settings/Connections/Sql/String").Value;
            
        }
    }
}
