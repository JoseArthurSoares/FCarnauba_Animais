using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Documento
    {
        int _Id;
        int _FinanceiroId;
        string _DocumentoFinanceiroId;
        string _Descricao;
        Arquivo _PDFDocumento;
        DateTime _DataDocumento;

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public int FinanceiroId
        {
            get
            {
                return _FinanceiroId;
            }
            set
            {
                _FinanceiroId = value;
            }
        }

        public string DocumentoFinanceiroId
        {
            get
            {
                return _DocumentoFinanceiroId;
            }
            set
            {
                _DocumentoFinanceiroId = value;
            }
        }

        public string Descricao
        {
            get
            {
                return _Descricao;
            }
            set
            {
                _Descricao = value;
            }
        }

        public Arquivo PDFDocumento
        {
            get { return _PDFDocumento; }
            set { _PDFDocumento = value; }
        }

        public DateTime DataDocumento
        {
            get
            {
                return _DataDocumento;
            }
            set
            {
                _DataDocumento = value;
            }
        }

        public bool HasPDF()
        {
            return _PDFDocumento != null;
        }

    }
}
