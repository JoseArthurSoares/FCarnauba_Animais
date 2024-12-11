using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    public class Regiao
    {
        public int Id { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }
    }

    public class UF
    {
        public int Id { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public Regiao regiao { get; set; }
    }

    public class Mesorregiao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public UF Uf { get; set; }
    }

    public class Microrregiao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Mesorregiao Mesorregiao { get; set; }
    }

    public class Municipios
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Microrregiao Microrregiao { get; set; }
    }
}
