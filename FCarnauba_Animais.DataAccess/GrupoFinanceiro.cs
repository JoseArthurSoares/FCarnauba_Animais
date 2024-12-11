using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class GrupoFinanceiro
    {
        private int idNuloField;
        
        private int idGrupoField;
        
        private string descricaoField;
        
        private int idGrupoPaiField;

        private string idsGrupoSupField;

        private bool ehUltimoNohField;

        public GrupoFinanceiro()
        {
            this.idNuloField = 0;
        }

        public int IdNulo
        {
            get
            {
                return this.idNuloField;
            }
            set
            {
                this.idNuloField = value;
            }
        }

        public int IdGrupo
        {
            get
            {
                return this.idGrupoField;
            }
            set
            {
                this.idGrupoField = value;
            }
        }

        public string Descricao
        {
            get
            {
                return this.descricaoField;
            }
            set
            {
                this.descricaoField = value;
            }
        }

        public int IdGrupoPai
        {
            get
            {
                return this.idGrupoPaiField;
            }
            set
            {
                this.idGrupoPaiField = value;
            }
        }

        public string IdsGrupoSup
        {
            get
            {
                return this.idsGrupoSupField;
            }
            set
            {
                this.idsGrupoSupField = value;
            }
        }

        public bool EhUltimoNoh
        {
            get
            {
                return this.ehUltimoNohField;
            }
            set
            {
                this.ehUltimoNohField = value;
            }
        }
    }
}
