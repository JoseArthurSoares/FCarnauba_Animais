using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Historico
    {
        public int Id { get; set; }

        public string Movimento { get; set; }

        public string NomeQAQ { get; set; }

        public DateTime DataManejo { get; set; }

        public string Observacao { get; set; }

        public bool Deleted { get; set; }

    }
}
