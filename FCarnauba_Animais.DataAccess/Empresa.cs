using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCarnauba_Animais.DataAccess
{
    [Serializable]
    public class Empresa
    {
        private long idEmpresaField;

        private string diretorioField;

        private string razaoSocialField;

        private string cnpjCpfField;

        private string enderecoField;

        private System.DateTime dataUsuarioField;

        private string usuarioField;

        private string inscEstadualRgField;

        private string municipioField;

        private string ufField;

        private string telefonesField;

        private string tipoField;

        private string emailField;

        public long IdEmpresa
        {
            get
            {
                return this.idEmpresaField;
            }
            set
            {
                this.idEmpresaField = value;
            }
        }

        public string Diretorio
        {
            get
            {
                return this.diretorioField;
            }
            set
            {
                this.diretorioField = value;
            }
        }

        public string RazaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        public string CnpjCpf
        {
            get
            {
                return this.cnpjCpfField;
            }
            set
            {
                this.cnpjCpfField = value;
            }
        }

        public string Endereco
        {
            get
            {
                return this.enderecoField;
            }
            set
            {
                this.enderecoField = value;
            }
        }

        public System.DateTime DataUsuario
        {
            get
            {
                return this.dataUsuarioField;
            }
            set
            {
                this.dataUsuarioField = value;
            }
        }

        public string Usuario
        {
            get
            {
                return this.usuarioField;
            }
            set
            {
                this.usuarioField = value;
            }
        }

        public string InscEstadualRg
        {
            get
            {
                return this.inscEstadualRgField;
            }
            set
            {
                this.inscEstadualRgField = value;
            }
        }

        public string Municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = value;
            }
        }

        public string Uf
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        public string Telefones
        {
            get
            {
                return this.telefonesField;
            }
            set
            {
                this.telefonesField = value;
            }
        }

        public string Tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }

        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }
}
