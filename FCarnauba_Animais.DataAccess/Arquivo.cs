using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Arquivo
    {
        private Stream fileStream;


        public Arquivo(string fileName, Stream fileStream)
        {
            FileName = FCarnaubaDataAccess.GenerateFilename(fileName);
            FileStream = fileStream;
            OriginalFileName = fileName;
            Dirty = true;
        }

        public Arquivo(string fileName, string gFileName, Stream fileStream)
        {
            FileName = gFileName;
            FileStream = fileStream;
            OriginalFileName = fileName;
            Dirty = false;
        }

        public Arquivo()
        {

        }

        public Stream FileStream
        {
            get { return fileStream; }
            set
            {
                if (value != null)
                {
                    fileStream = value;
                }

            }
        }

        public string FileName { get; set; }

        public bool Dirty
        {
            get;
            set;
        }

        public string OriginalFileName;
    }
}
