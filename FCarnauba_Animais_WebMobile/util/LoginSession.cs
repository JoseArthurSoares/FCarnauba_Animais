using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using LightInfocon.GoldenAccess.General;

namespace FCarnauba_Animais_WebMobile.util
{
    public class FormData
    {
        public Dictionary<string, object> Forms = new Dictionary<string, object>();
    }

    public class LoginSession
    {
        public User GoldenAccessUser;
        public Dictionary<Page, FormData> PageData = new Dictionary<Page, FormData>();

    }
}