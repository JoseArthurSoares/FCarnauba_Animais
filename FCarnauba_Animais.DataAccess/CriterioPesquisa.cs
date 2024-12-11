using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    public class CriterioPesquisa
    {
        public static string AddParametro(string filter, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                {
                    filter += " e ";
                }
                filter += value;
            }
            return filter;
        }

        public static string AddParametro(string filter, string databaseField, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                {
                    filter += " e ";
                }

                filter += "\"" + value + "\"" + "[" + databaseField + "]";
            }
            return filter;
        }

        public static string AddParametroData(string filter, string databaseField, string value, string oper)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                {
                    filter += " e ";
                }

                filter += oper + value + "[" + databaseField + "]";
            }
            return filter;
        }

        public static string AddParametroTextual(string filter, string value)
        {
            value = value.Replace(",", " ");
            const string quote = "\"";
            string[] words = value.Split(' ');

            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word) && word != "e" && word != "ou" && word != "de" && word != "da" && word != "das" && word != "do" && word != "dos")
                {
                    if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                    {

                        filter += " e ";

                    }
                    //filter += quote + value + quote + "[" + databaseField + "]";
                    filter += word;
                }

            }

            return filter;
        }

        public static string AddParametroTextual(string filter, string databaseField, string value)
        {
            const string quote = "\"";
            string[] words = value.Split(' ');

            foreach (string word in words)
            {

                if (!string.IsNullOrEmpty(word) && word != "e" && word != "ou" && word != "de" && word != "da" && word != "das" && word != "do" && word != "dos")
                {
                    if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                    {

                        filter += " e ";

                    }
                    //filter += quote + value + quote + "[" + databaseField + "]";
                    filter += word + "[" + databaseField + "]";
                }

            }

            return filter;
        }
    }
}
