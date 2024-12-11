using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCarnauba_Animais.util
{
    public class SelecaoDeGrupoEventArgs : EventArgs
    {
        private readonly int idGrupo;
        private readonly string nomeGrupo;

        public SelecaoDeGrupoEventArgs(int idGrupo, string nomeGrupo)
        {
            this.idGrupo = idGrupo;
            this.nomeGrupo = nomeGrupo;
        }

        public string NomeGrupo
        {
            get { return nomeGrupo; }
        }

        public int IdGrupo
        {
            get { return idGrupo; }
        }
    }
}