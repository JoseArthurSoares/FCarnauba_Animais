using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using LightInfocon.GoldenAccess.General;
using FCarnauba_Animais.DataAccess;

namespace FCarnauba_Animais.util
{
    public class AuxLogin
    {
        private static User CachedSession;
        private static string SessionPath = "";
        private static LoginSession CurLogin = new LoginSession();
        static AuxLogin()
        {
            SetRepositoryPath(FCarnaubaSettings.RepositorioSessions);
        }

        public static void SetRepositoryPath(string path)
        {
            SessionPath = path;
        }

        public static void SetCurrentSessionCookies(User informacoesUsuario, string targSession, Page resp)
        {
            var userInfo = new HttpCookie("user");
            userInfo["login"] = informacoesUsuario.Login;
            userInfo["sessid"] = targSession;
            userInfo["ip"] = resp.Request.ServerVariables["REMOTE_ADDR"];
            userInfo.Expires = DateTime.Today.AddYears(5);
            resp.Response.Cookies.Add(userInfo);
        }

        public static string SaveCurrentSession(User informacoesUsuario)
        {
            var targSession = Path.GetRandomFileName() + ".bin";
            using (Stream fileStream = new FileStream(Path.Combine(SessionPath, targSession), FileMode.Create,
                           FileAccess.Write, FileShare.None))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, informacoesUsuario);
            }
            return targSession;
        }

        public static void EndCurrentSession(string targSession, HttpResponse resp)
        {
            var userInfo = new HttpCookie("user");
            userInfo.Expires = DateTime.Now.AddDays(-1);
            resp.Cookies.Add(userInfo);
            File.Delete(Path.Combine(SessionPath, targSession));
        }

        public static bool VerifyUserCookies(string login, string sessId)
        {
            try
            {
                using (Stream fileStream = new FileStream(Path.Combine(SessionPath, sessId), FileMode.Open,
                                                       FileAccess.Read, FileShare.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    CachedSession = (User)formatter.Deserialize(fileStream);
                    if (CachedSession.Login == login) return true;
                }
            }
            catch { }
            return false;
        }

        public static User GetUserFromSession(string userCooky)
        {
            return CachedSession;
        }

        public static void SaveCurrentForms(Page curPage, Control controlToSave)
        {
            //CurLogin.PageData[];
        }

        public static void SaveCurrentForms(UserControl curPage, Control controlToSave)
        {
            throw new NotImplementedException();
        }
    }
}