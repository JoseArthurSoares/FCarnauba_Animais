using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FCarnauba_Animais.util
{
    public static class WebUtil
    {
        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        static extern int FindMimeFromData(IntPtr pBC,
              [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
             [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] 
         byte[] pBuffer,
              int cbSize,
                 [MarshalAs(UnmanagedType.LPWStr)]  string pwzMimeProposed,
              int dwMimeFlags,
              out IntPtr ppwzMimeOut,
              int dwReserved);

        public static void AddRowHighlight(GridViewRow Row, string targUrl, string hlColor = "#D7E9FD")
        {

            Row.Attributes.Add("id", "tr_contr_" + Row.RowIndex);
            Row.Attributes.Add("onmouseover", String.Format(
                                 "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='{0}';this.style.cursor='pointer'", hlColor));
            Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            Row.Attributes.Add("onclick", String.Format("location.href='{0}'", targUrl));
        }

        public static void DownloadArquivo(this Page page, string filePath, bool deleteAfterDownload = false)
        {
            page.Response.Clear();
            page.Response.ContentType = "application/zip";
            page.Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            page.Response.WriteFile(filePath);
            page.Response.Flush();
            try
            {
                page.Response.End();
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        public static void DownloadArquivoTxt(this Page page, string filePath, bool deleteAfterDownload = false)
        {
            page.Response.Clear();
            page.Response.ContentType = "text/plain";
            page.Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            page.Response.WriteFile(filePath);
            page.Response.Flush();
            try
            {
                page.Response.End();
            }
            finally
            {
                File.Delete(filePath);
            }


        }

        public static string GetMimeFromFile(string file)
        {
            IntPtr mimeout;
            if (!System.IO.File.Exists(file))
                throw new FileNotFoundException(file + " not found");

            int MaxContent = (int)new FileInfo(file).Length;
            if (MaxContent > 4096) MaxContent = 4096;
            FileStream fs = File.OpenRead(file);


            byte[] buf = new byte[MaxContent];
            fs.Read(buf, 0, MaxContent);
            fs.Close();
            int result = FindMimeFromData(IntPtr.Zero, file, buf, MaxContent, null, 0, out mimeout, 0);

            if (result != 0)
                throw Marshal.GetExceptionForHR(result);
            string mime = Marshal.PtrToStringUni(mimeout);
            Marshal.FreeCoTaskMem(mimeout);
            return mime;
        }
    }
}