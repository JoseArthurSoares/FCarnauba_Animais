using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Matriz
    {
        public int Id { get; set; }
        public string IdMatriz { get; set; }
        public int CioRepeticao { get; set; }
        public string IdCria { get; set; }
        public string NomeMatriz { get; set; }
        public string NomeCria { get; set; }
        public DateTime? DataEntradaControle { get; set; }
        public DateTime? DataSaidaControle { get; set; }
        public bool EmControleLeiteiro { get; set; }
        public string EmControleLeiteiroStr { get; set; }
        public bool CdcEfetiva { get; set; }
        public string CdcEfetivaStr { get; set; }
        public DateTime? DataCdcEfetiva { get; set; }
        public bool Deleted { get; set; }
    }
}
