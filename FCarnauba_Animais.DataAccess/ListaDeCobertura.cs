using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class ListaDeCobertura
    {
        public int Id { get; set; }

        public long Cdc { get; set; }

        public DateTime DataCobertura { get; set; }

        public DateTime DataParto { get; set; }

        public string RgdTouro { get; set; }

        public string NomeTouro { get; set; }

        public string RgCria { get; set; }

        public string NomeCria { get; set; }

        public bool Deleted { get; set; }
    }
}
