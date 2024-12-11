using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.util;

namespace FCarnauba_Animais.UserControls
{
    public class UserControlBase : System.Web.UI.UserControl
    {
        protected User UsuarioLogado
        {
            // ReSharper disable PossibleNullReferenceException
            get { return (this.Page as PaginaBase).UsuarioLogado; }

            set { (this.Page as PaginaBase).UsuarioLogado = value; }
            // ReSharper restore PossibleNullReferenceException
        }

    }
}