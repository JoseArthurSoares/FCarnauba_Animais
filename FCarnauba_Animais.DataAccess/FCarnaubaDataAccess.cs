using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LightInfocon.Data.LightBaseProvider;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Globalization;

namespace FCarnauba_Animais.DataAccess
{
    public class FCarnaubaDataAccess
    {

        private string _ConnectionString;
        private LightBaseConnection _Connection;
        private bool _AdLinha;
        private static bool _realizouBusca;
        public static int NumConnections = 0;
        private const string ConectorAndSql = "and";


        public FCarnaubaDataAccess(string connStr)
        {
            _ConnectionString = connStr;
        }

        public FCarnaubaDataAccess()
        {
            var conStr = String.Format("user={0};password={1};server=localhost;udb=DEFUDB;", FCarnaubaSettings.UserLbw,
                                       FCarnaubaSettings.SenhaLbw);
            _ConnectionString = conStr;
        }

        public void OpenConnection()
        {
            _Connection = new LightBaseConnection(_ConnectionString);
            _Connection.Open();
            NumConnections++;
            //Debug.WriteLine("connection count: " + NumConnections);
        }

        public void CloseConnection()
        {
            _Connection.Close();
            NumConnections--;
            //Debug.WriteLine("connection count: " + NumConnections);
        }

        public string GetFileNameFor(string originalName)
        {
            throw new NotImplementedException();
        }

        public static string GenerateFilename(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName)) return "";
            return Guid.NewGuid() + Path.GetExtension(fileName);
        }

        public void SalvaMapeamento(string f1, string f2)
        {
            var command = new LightBaseCommand(@"insert into FCARNAUBA_ARQUIVOS strArquivoGerado, strNomeOriginal values (@strArquivoGerado,@strNomeOriginal)");

            ((LightBaseParameter)command.Parameters["strArquivoGerado"]).Value = f2;
            ((LightBaseParameter)command.Parameters["strNomeOriginal"]).Value = f1;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        private void SalvaArquivoGerado(Arquivo arquivo)
        {
            if (!arquivo.Dirty) return;
            string filePath = GetPathFor(arquivo.FileName);

            Stream sw = new FileStream(filePath, FileMode.CreateNew);
            if (!arquivo.FileStream.CanRead)
            {
                new MemoryStream((arquivo.FileStream as MemoryStream).ToArray()).CopyTo(sw);
            }
            else
            {
                arquivo.FileStream.CopyTo(sw);
            }
            sw.Close();
        }

        private void SalvaArquivoGeradoAnimal(Arquivo arquivo)
        {
            if (!arquivo.Dirty) return;
            string filePath = GetPathForAnimal(arquivo.FileName);

            Stream sw = new FileStream(filePath, FileMode.CreateNew);
            if (!arquivo.FileStream.CanRead)
            {
                new MemoryStream((arquivo.FileStream as MemoryStream).ToArray()).CopyTo(sw);
            }
            else
            {
                arquivo.FileStream.CopyTo(sw);
            }
            sw.Close();
        }

        private void SalvaMapeiaArquivoGerado(Arquivo arquivo)
        {
            if (!arquivo.Dirty) return;
            string filePath = GetPathFor(arquivo.FileName);

            Stream sw = new FileStream(filePath, FileMode.CreateNew);
            if (!arquivo.FileStream.CanRead)
            {
                new MemoryStream((arquivo.FileStream as MemoryStream).ToArray()).CopyTo(sw);
            }
            else
            {
                arquivo.FileStream.CopyTo(sw);
            }
            sw.Close();
            SalvaMapeamento(arquivo.OriginalFileName, arquivo.FileName);
        }

        private void SalvaMapeiaArquivoGeradoAnimal(Arquivo arquivo)
        {
            if (!arquivo.Dirty) return;
            string filePath = GetPathForAnimal(arquivo.FileName);

            Stream sw = new FileStream(filePath, FileMode.CreateNew);
            if (!arquivo.FileStream.CanRead)
            {
                new MemoryStream((arquivo.FileStream as MemoryStream).ToArray()).CopyTo(sw);
            }
            else
            {
                arquivo.FileStream.CopyTo(sw);
            }
            sw.Close();
            SalvaMapeamento(arquivo.OriginalFileName, arquivo.FileName);
        }

        private void GeraSalvaThumbnail(string path)
        {
            var actualPath = GetPathFor(path);
            var actualName = GetPathFor(Path.GetFileNameWithoutExtension(path) + "_thumb.png");
            Image.FromFile(actualPath).GetThumbnailImage(120, 120, null, new IntPtr()).Save(actualName, ImageFormat.Png);
        }

        private void GeraSalvaThumbnailAnimal(string path)
        {
            var actualPath = GetPathForAnimal(path);
            var actualName = GetPathForAnimal(Path.GetFileNameWithoutExtension(path) + "_thumb.png");
            Image.FromFile(actualPath).GetThumbnailImage(120, 120, null, new IntPtr()).Save(actualName, ImageFormat.Png);
        }

        public static string SalvaArquivoTemporario(Arquivo arquivo)
        {
            string filePath = FCarnaubaSettings.RepositorioFinanceiroPrincipal + "/tmp/" + arquivo.FileName;

            Stream sw = new FileStream(filePath, FileMode.CreateNew);
            if (!arquivo.FileStream.CanRead)
            {
                new MemoryStream((arquivo.FileStream as MemoryStream).ToArray()).CopyTo(sw);
            }
            else
            {
                arquivo.FileStream.CopyTo(sw);
            }
            sw.Close();
            return filePath;
        }

        public static string SalvaArquivoTemporarioAnimal(Arquivo arquivo)
        {
            string filePath = FCarnaubaSettings.RepositorioAnimalPrincipal + "/tmp/" + arquivo.FileName;

            Stream sw = new FileStream(filePath, FileMode.CreateNew);
            if (!arquivo.FileStream.CanRead)
            {
                new MemoryStream((arquivo.FileStream as MemoryStream).ToArray()).CopyTo(sw);
            }
            else
            {
                arquivo.FileStream.CopyTo(sw);
            }
            sw.Close();
            return filePath;
        }

        public static string GetTempFilesPath()
        {
            return FCarnaubaSettings.RepositorioFinanceiroPrincipal + "/tmp/";
        }

        public static string GetTempFilesPathAnimal()
        {
            return FCarnaubaSettings.RepositorioAnimalPrincipal + "/tmp/";
        }

        public static string GetPathFor(string arquivo)
        {
            return FCarnaubaSettings.RepositorioFinanceiroPrincipal + "/" + arquivo;
        }

        public static string GetPathForAnimal(string arquivo)
        {
            return FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + arquivo;
        }

        private string BuildAnimalString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_ANIMAIS set " + String.Join(",", outCampos) + " where strId = @strId";
        }

        private string BuildLoteString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_LOTE_CONTROLE_LEITEIRO set " + String.Join(",", outCampos) + " where id = @id";
        }

        private string BuildLotePonderalString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_LOTE_CONTROLE_PONDERAL set " + String.Join(",", outCampos) + " where id = @id";
        }

        private string BuildEstruturaPropriedadeString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_PROPRIEDADE set " + String.Join(",", outCampos) + " where id = @id";
        }

        private string BuildCdcString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_CDC set " + String.Join(",", outCampos) + " where id = @id";
        }

        private string BuildControlePluviometricoString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_CONT_PLUVIOMETRICO set " + String.Join(",", outCampos) + " where id = @id";
        }

        private string BuildFluxoCaixaString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_FLUXO_CAIXA set " + String.Join(",", outCampos) + " where id = @id";
        }

        private string BuildControleLeiteiroString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_CONTROLE_LEITEIRO set " + String.Join(",", outCampos) + " where id = @id";
        }

        private static string AddParametro(string filter, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                {
                    filter += " e ";
                }
                filter += "\"" + value + "\"";
            }
            return filter;
        }

        private static string AddParametro(string filter, string databaseField, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                {
                    filter += " e ";
                }

                filter += "\"" + value + "\"" + "[" + databaseField + "]";
            }
            return filter;
        }

        private static string AddParametroTextual(string filter, string databaseField, string value)
        {
            const string quote = "\"";
            string[] words = value.Split(' ');

            foreach (string word in words)
            {

                if (!string.IsNullOrEmpty(word) && word != "e" && word != "ou" && word != "de" && word != "da" && word != "das" && word != "do" && word != "dos")
                {
                    if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                    {

                        filter += " e ";

                    }
                    filter += word + "[" + databaseField + "]";
                }

            }

            return filter;
        }

        private static string AddParametroData(string filter, string databaseField, string value, string oper)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrWhiteSpace(filter)) //Não é o primeiro parâmetro
                {
                    filter += " e ";
                }

                filter += oper + value + "[" + databaseField + "]";
            }
            return filter;
        }

        public long AdicionaAnimal(Animal animal)
        {
            var command = new LightBaseCommand(@"insert into FCARNAUBA_ANIMAIS
                diretorio,
                intNumeroOrdem,
                strNomeFazenda,
                strNome,
                strNomeCompleto,
                strSexo,
                strRgn,
                dtDataNascimento,
                strRgd,
                strRaca,
                vfCria,
                decPn,
                strLaudoDna, 
                intCdnOrigem, 
                intCdcOrigem, 
                docObservacoes,
                strFoto, 
                strRgdSerie,  
                strRgnSerie,   
                strPaiId, 
                strMaeId,
                dtDataCdc,
                strUsuario,
                dtDataUsuario,
                strRgdNumero,
                strRgnNumero,
                strRgdRaca,
                strRgnRaca,
                vfFiv,
                strReceptoraId,
                strLaudoBetaCaseina,
                strTipoBetaCaseina,
                strLaudoKappaCaseina,
                strTipoKappaCaseina,
                strTemperamento,
                vfLaudoDna,
                strLaudoDna2,
                vfLaudoArquivoPermanente,
                strLaudoDna3,
                vfLaudoSecundario1,
                strLaudoDna4,
                vfLaudoSecundario2,
                vfLaudoBetaCaseina,
                vfLaudoKappaCaseina,
                vfRgd,
                vfRgn,
                strTipoParto,
                strVigorBez,
                strEstadoCorporalMae,
                strTamanhoTeta,
                strMaeBoaLeite,
                vfMaeOrdenhada,
                vfAnimalImprodutivo
                 
                values
                
                (@diretorio,
                @intNumeroOrdem,
                @strNomeFazenda,
                @strNome,
                @strNomeCompleto,
                @strSexo,
                @strRgn,
                @dtDataNascimento,
                @strRgd,
                @strRaca,
                @vfCria,
                @decPn,
                @strLaudoDna,
                @intCdnOrigem,
                @intCdcOrigem, 
                @docObservacoes, 
                @strFoto, 
                @strRgdSerie, 
                @strRgnSerie,  
                @strPaiId, 
                @strMaeId, 
                @dtDataCdc, 
                @strUsuario, 
                @dtDataUsuario,
                @strRgdNumero,
                @strRgnNumero,
                @strRgdRaca,
                @strRgnRaca, 
                @vfFiv,
                @strReceptoraId,
                @strLaudoBetaCaseina,
                @strTipoBetaCaseina,
                @strLaudoKappaCaseina,
                @strTipoKappaCaseina,
                @strTemperamento,
                @vfLaudoDna,
                @strLaudoDna2,
                @vfLaudoArquivoPermanente,
                @strLaudoDna3,
                @vfLaudoSecundario1,
                @strLaudoDna4,
                @vfLaudoSecundario2,
                @vfLaudoBetaCaseina,
                @vfLaudoKappaCaseina,
                @vfRgd,
                @vfRgn,
                @strTipoParto,
                @strVigorBez,
                @strEstadoCorporalMae,
                @strTamanhoTeta,
                @strMaeBoaLeite,
                @vfMaeOrdenhada,
                @vfAnimalImprodutivo)", _Connection);
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = animal.Diretorio;
            ((LightBaseParameter)command.Parameters["intNumeroOrdem"]).Value = animal.NumeroOrdem;
            ((LightBaseParameter)command.Parameters["strNomeFazenda"]).Value = animal.NomeFazenda;
            ((LightBaseParameter)command.Parameters["strNome"]).Value = animal.Nome;
            ((LightBaseParameter)command.Parameters["strNomeCompleto"]).Value = animal.NomeCompleto;
            ((LightBaseParameter)command.Parameters["strSexo"]).Value = animal.Sexo;
            ((LightBaseParameter)command.Parameters["strRgn"]).Value = animal.Rgn;
            ((LightBaseParameter)command.Parameters["dtDataNascimento"]).Value = animal.DataNascimento;
            ((LightBaseParameter)command.Parameters["strRgd"]).Value = animal.Rgd;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = animal.Raca;
            ((LightBaseParameter)command.Parameters["vfCria"]).Value = animal.EhCria;
            ((LightBaseParameter)command.Parameters["decPn"]).Value = animal.Pn;
            ((LightBaseParameter)command.Parameters["strPaiId"]).Value = animal.StrPaiId;
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = animal.StrMaeId;

            if (animal.HasPDFLaudoDna())
            {
                ((LightBaseParameter)command.Parameters["strLaudoDna"]).Value = animal.LaudoDna.FileName;

            }
            else
            {
                ((LightBaseParameter)command.Parameters["strLaudoDna"]).Value = "";
            }

            ((LightBaseParameter)command.Parameters["intCdnOrigem"]).Value = animal.CdnOrigem;
            ((LightBaseParameter)command.Parameters["intCdcOrigem"]).Value = GetUltimoCDC(animal.StrPaiId);
            ((LightBaseParameter)command.Parameters["docObservacoes"]).Value = animal.Observacoes;

            if (animal.HasFoto())
            {
                ((LightBaseParameter)command.Parameters["strFoto"]).Value = animal.Foto.FileName;

            }
            else
            {
                ((LightBaseParameter)command.Parameters["strFoto"]).Value = "";
            }

            ((LightBaseParameter)command.Parameters["strRgdSerie"]).Value = animal.RgdSerie;
            ((LightBaseParameter)command.Parameters["strRgnSerie"]).Value = animal.RgnSerie;
            ((LightBaseParameter)command.Parameters["dtDataCdc"]).Value = GetUltimaDataCDC(animal.StrPaiId);
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = animal.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = animal.DataUsuario;
            ((LightBaseParameter)command.Parameters["strRgdNumero"]).Value = animal.RgdNumero;
            ((LightBaseParameter)command.Parameters["strRgnNumero"]).Value = animal.RgnNumero;
            ((LightBaseParameter)command.Parameters["vfFiv"]).Value = animal.EhFIV;
            ((LightBaseParameter)command.Parameters["strReceptoraId"]).Value = animal.StrReceptoraId;
            ((LightBaseParameter)command.Parameters["strRgdRaca"]).Value = animal.Raca + " " + animal.RgdSerie + " " + animal.RgdNumero;
            ((LightBaseParameter)command.Parameters["strRgnRaca"]).Value = animal.Raca + " " + animal.RgnSerie + " " + animal.RgnNumero;



            if (animal.HasPDFLaudoBetaCaseina())
            {
                ((LightBaseParameter)command.Parameters["strLaudoBetaCaseina"]).Value = animal.LaudoBetaCaseina.FileName;

            }
            else
            {
                ((LightBaseParameter)command.Parameters["strLaudoBetaCaseina"]).Value = "";
            }

            ((LightBaseParameter)command.Parameters["strTipoBetaCaseina"]).Value = animal.TipoBetaCaseina;

            if (animal.HasPDFLaudoKappaCaseina())
            {
                ((LightBaseParameter)command.Parameters["strLaudoKappaCaseina"]).Value = animal.LaudoKappaCaseina.FileName;

            }
            else
            {
                ((LightBaseParameter)command.Parameters["strLaudoKappaCaseina"]).Value = "";
            }

            ((LightBaseParameter)command.Parameters["strTipoKappaCaseina"]).Value = animal.TipoKappaCaseina;
            ((LightBaseParameter)command.Parameters["strTemperamento"]).Value = "";
            ((LightBaseParameter)command.Parameters["vfLaudoDna"]).Value = animal.TemLaudoDna;

            if (animal.HasPDFLaudoDna2())
            {
                ((LightBaseParameter)command.Parameters["strLaudoDna2"]).Value = animal.LaudoDna2.FileName;

            }
            else
            {
                ((LightBaseParameter)command.Parameters["strLaudoDna2"]).Value = "";
            }

            ((LightBaseParameter)command.Parameters["vfLaudoArquivoPermanente"]).Value = animal.TemLaudoArquivoPermanente;

            if (animal.HasPDFLaudoDna3())
            {
                ((LightBaseParameter)command.Parameters["strLaudoDna3"]).Value = animal.LaudoDna3.FileName;

            }
            else
            {
                ((LightBaseParameter)command.Parameters["strLaudoDna3"]).Value = "";
            }

            ((LightBaseParameter)command.Parameters["vfLaudoSecundario1"]).Value = animal.TemLaudoSecundario1;

            if (animal.HasPDFLaudoDna4())
            {
                ((LightBaseParameter)command.Parameters["strLaudoDna4"]).Value = animal.LaudoDna4.FileName;

            }
            else
            {
                ((LightBaseParameter)command.Parameters["strLaudoDna4"]).Value = "";
            }

            ((LightBaseParameter)command.Parameters["vfLaudoSecundario2"]).Value = animal.TemLaudoSecundario2;
            ((LightBaseParameter)command.Parameters["vfLaudoBetaCaseina"]).Value = animal.TemLaudoBetaCaseina;
            ((LightBaseParameter)command.Parameters["vfLaudoKappaCaseina"]).Value = animal.TemLaudoKappaCaseina;
            ((LightBaseParameter)command.Parameters["vfRgd"]).Value = animal.TemRgd;
            ((LightBaseParameter)command.Parameters["vfRgn"]).Value = animal.TemRgn;
            ((LightBaseParameter)command.Parameters["strTipoParto"]).Value = animal.TipoParto;
            ((LightBaseParameter)command.Parameters["strVigorBez"]).Value = animal.VigorBez;
            ((LightBaseParameter)command.Parameters["strEstadoCorporalMae"]).Value = animal.EstadoCorporalMae;
            ((LightBaseParameter)command.Parameters["strTamanhoTeta"]).Value = animal.TamanhoTeta;
            ((LightBaseParameter)command.Parameters["strMaeBoaLeite"]).Value = animal.MaeBoaLeite;
            ((LightBaseParameter)command.Parameters["vfMaeOrdenhada"]).Value = animal.MaeOrdenhada;
            ((LightBaseParameter)command.Parameters["vfAnimalImprodutivo"]).Value = animal.AnimalImprodutivo;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

            const string idRetrievingCommand = "@@Id";
            LightBaseCommand lastIdCommand = new LightBaseCommand(idRetrievingCommand, _Connection);
            long ultimoId = Convert.ToInt32(lastIdCommand.ExecuteScalar());

            var commandUpdate = new LightBaseCommand(@"update FCARNAUBA_ANIMAIS set strId=@strId where id = @id");
            ((LightBaseParameter)commandUpdate.Parameters["strId"]).Value = ultimoId.ToString();
            ((LightBaseParameter)commandUpdate.Parameters["id"]).Value = ultimoId.ToString();

            commandUpdate.Connection = _Connection;
            commandUpdate.ExecuteNonQuery();

            if (animal.HasPDFLaudoDna())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoDna);

            }

            if (animal.HasFoto())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.Foto);
                GeraSalvaThumbnailAnimal(animal.Foto.FileName);

            }

            if (animal.HasPDFLaudoBetaCaseina())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoBetaCaseina);

            }

            if (animal.HasPDFLaudoKappaCaseina())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoKappaCaseina);

            }


            if (animal.HasPDFLaudoDna2())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoDna2);

            }

            if (animal.HasPDFLaudoDna3())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoDna3);

            }

            if (animal.HasPDFLaudoDna4())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoDna4);

            }

            return ultimoId;

        }


        public void SalvaAnimal(Animal animal)
        {

            List<string> campos = new List<string>()
                                      {
                                          "diretorio",
                                          "intNumeroOrdem",
                                          "strNomeFazenda",
                                          "strNome",
                                          "strNomeCompleto",
                                          "strSexo","strRgn","dtDataNascimento",
                                          "strRgd",
                                          "strRaca",
                                          "vfCria",
                                          "decPn"
                                      };
            if (animal.LaudoDna != null)
            {
                campos.Add("strLaudoDna");
            }

            campos.Add("intCdnOrigem");
            campos.Add("intCdcOrigem");
            campos.Add("docObservacoes");


            if (animal.Foto != null)
            {
                campos.Add("strFoto");
            }

            campos.Add("strRgdSerie");
            campos.Add("strRgnSerie");
            campos.Add("strPaiId");
            campos.Add("strMaeId");
            campos.Add("dtDataCdc");
            campos.Add("strUsuario");
            campos.Add("dtDataUsuario");
            campos.Add("strRgdNumero");
            campos.Add("strRgnNumero");
            campos.Add("strRgdRaca");
            campos.Add("strRgnRaca");
            campos.Add("vfFiv");
            campos.Add("strReceptoraId");

            if (animal.LaudoBetaCaseina != null)
            {
                campos.Add("strLaudoBetaCaseina");
            }

            campos.Add("strTipoBetaCaseina");

            if (animal.LaudoKappaCaseina != null)
            {
                campos.Add("strLaudoKappaCaseina");
            }

            campos.Add("strTipoKappaCaseina");
            campos.Add("strTemperamento");
            campos.Add("vfLaudoDna");

            if (animal.LaudoDna2 != null)
            {
                campos.Add("strLaudoDna2");
            }

            campos.Add("vfLaudoArquivoPermanente");

            if (animal.LaudoDna3 != null)
            {
                campos.Add("strLaudoDna3");
            }

            campos.Add("vfLaudoSecundario1");

            campos.Add("vfLaudoArquivoPermanente");

            if (animal.LaudoDna4 != null)
            {
                campos.Add("strLaudoDna4");
            }

            campos.Add("vfLaudoSecundario2");
            campos.Add("vfLaudoBetaCaseina");
            campos.Add("vfLaudoKappaCaseina");
            campos.Add("vfRgd");
            campos.Add("vfRgn");
            campos.Add("strTipoParto");
            campos.Add("strVigorBez");
            campos.Add("strEstadoCorporalMae");
            campos.Add("strTamanhoTeta");
            campos.Add("strMaeBoaLeite");
            campos.Add("vfMaeOrdenhada");
            campos.Add("vfAnimalImprodutivo");

            var command = new LightBaseCommand(BuildAnimalString(campos));

            ((LightBaseParameter)command.Parameters["strId"]).Value = animal.Id;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = "";
            ((LightBaseParameter)command.Parameters["intNumeroOrdem"]).Value = animal.NumeroOrdem;
            ((LightBaseParameter)command.Parameters["strNomeFazenda"]).Value = animal.NomeFazenda;
            ((LightBaseParameter)command.Parameters["strNome"]).Value = animal.Nome;
            ((LightBaseParameter)command.Parameters["strNomeCompleto"]).Value = animal.NomeCompleto;
            ((LightBaseParameter)command.Parameters["strSexo"]).Value = animal.Sexo;
            ((LightBaseParameter)command.Parameters["strRgn"]).Value = animal.Rgn;
            ((LightBaseParameter)command.Parameters["dtDataNascimento"]).Value = animal.DataNascimento;
            ((LightBaseParameter)command.Parameters["strRgd"]).Value = animal.Rgd;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = animal.Raca;
            ((LightBaseParameter)command.Parameters["vfCria"]).Value = animal.EhCria;
            ((LightBaseParameter)command.Parameters["decPn"]).Value = animal.Pn;
            ((LightBaseParameter)command.Parameters["strPaiId"]).Value = animal.StrPaiId;
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = animal.StrMaeId;

            if (campos.Contains("strLaudoDna")) ((LightBaseParameter)command.Parameters["strLaudoDna"]).Value = animal.LaudoDna.FileName;

            ((LightBaseParameter)command.Parameters["intCdnOrigem"]).Value = animal.CdnOrigem;
            ((LightBaseParameter)command.Parameters["intCdcOrigem"]).Value = GetUltimoCDC(animal.StrPaiId);
            ((LightBaseParameter)command.Parameters["docObservacoes"]).Value = animal.Observacoes;

            if (campos.Contains("strFoto")) ((LightBaseParameter)command.Parameters["strFoto"]).Value = animal.Foto.FileName;

            ((LightBaseParameter)command.Parameters["strRgdSerie"]).Value = animal.RgdSerie;
            ((LightBaseParameter)command.Parameters["strRgnSerie"]).Value = animal.RgnSerie;
            
            ((LightBaseParameter)command.Parameters["dtDataCdc"]).Value = GetUltimaDataCDC(animal.StrPaiId);
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = animal.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = animal.DataUsuario;
            ((LightBaseParameter)command.Parameters["strRgdNumero"]).Value = animal.RgdNumero;
            ((LightBaseParameter)command.Parameters["strRgnNumero"]).Value = animal.RgnNumero;
            ((LightBaseParameter)command.Parameters["vfFiv"]).Value = animal.EhFIV;
            ((LightBaseParameter)command.Parameters["strReceptoraId"]).Value = animal.StrReceptoraId;

            ((LightBaseParameter)command.Parameters["strRgdRaca"]).Value = animal.Raca + " " + animal.RgdSerie + " " + animal.RgdNumero;
            ((LightBaseParameter)command.Parameters["strRgnRaca"]).Value = animal.Raca + " " + animal.RgnSerie + " " + animal.RgnNumero;

            if (campos.Contains("strLaudoBetaCaseina")) ((LightBaseParameter)command.Parameters["strLaudoBetaCaseina"]).Value = animal.LaudoBetaCaseina.FileName;

            ((LightBaseParameter)command.Parameters["strTipoBetaCaseina"]).Value = animal.TipoBetaCaseina;

            if (campos.Contains("strLaudoKappaCaseina")) ((LightBaseParameter)command.Parameters["strLaudoKappaCaseina"]).Value = animal.LaudoKappaCaseina.FileName;

            ((LightBaseParameter)command.Parameters["strTipoKappaCaseina"]).Value = animal.TipoKappaCaseina;
            ((LightBaseParameter)command.Parameters["strTemperamento"]).Value = animal.Temperamento;
            ((LightBaseParameter)command.Parameters["vfLaudoDna"]).Value = animal.TemLaudoDna;

            if (campos.Contains("strLaudoDna2")) ((LightBaseParameter)command.Parameters["strLaudoDna2"]).Value = animal.LaudoDna2.FileName;

            ((LightBaseParameter)command.Parameters["vfLaudoArquivoPermanente"]).Value = animal.TemLaudoArquivoPermanente;

            if (campos.Contains("strLaudoDna3")) ((LightBaseParameter)command.Parameters["strLaudoDna3"]).Value = animal.LaudoDna3.FileName;

            ((LightBaseParameter)command.Parameters["vfLaudoSecundario1"]).Value = animal.TemLaudoSecundario1;

            if (campos.Contains("strLaudoDna4")) ((LightBaseParameter)command.Parameters["strLaudoDna4"]).Value = animal.LaudoDna4.FileName;

            ((LightBaseParameter)command.Parameters["vfLaudoSecundario2"]).Value = animal.TemLaudoSecundario2;
            ((LightBaseParameter)command.Parameters["vfLaudoBetaCaseina"]).Value = animal.TemLaudoBetaCaseina;
            ((LightBaseParameter)command.Parameters["vfLaudoKappaCaseina"]).Value = animal.TemLaudoKappaCaseina;
            ((LightBaseParameter)command.Parameters["vfRgd"]).Value = animal.TemRgd;
            ((LightBaseParameter)command.Parameters["vfRgn"]).Value = animal.TemRgn;
            ((LightBaseParameter)command.Parameters["strTipoParto"]).Value = animal.TipoParto;
            ((LightBaseParameter)command.Parameters["strVigorBez"]).Value = animal.VigorBez;
            ((LightBaseParameter)command.Parameters["strEstadoCorporalMae"]).Value = animal.EstadoCorporalMae;
            ((LightBaseParameter)command.Parameters["strTamanhoTeta"]).Value = animal.TamanhoTeta;
            ((LightBaseParameter)command.Parameters["strMaeBoaLeite"]).Value = animal.MaeBoaLeite;
            ((LightBaseParameter)command.Parameters["vfMaeOrdenhada"]).Value = animal.MaeOrdenhada;
            ((LightBaseParameter)command.Parameters["vfAnimalImprodutivo"]).Value = animal.AnimalImprodutivo;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

            if (animal.HasPDFLaudoDna())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoDna);

            }

            if (animal.HasFoto())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.Foto);
                GeraSalvaThumbnailAnimal(animal.Foto.FileName);

            }

            if (animal.HasPDFLaudoBetaCaseina())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoBetaCaseina);

            }

            if (animal.HasPDFLaudoKappaCaseina())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoKappaCaseina);

            }


            if (animal.HasPDFLaudoDna2())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoDna2);

            }

            if (animal.HasPDFLaudoDna3())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoDna3);

            }

            if (animal.HasPDFLaudoDna4())
            {
                SalvaMapeiaArquivoGeradoAnimal(animal.LaudoDna4);

            }


        }


        public void RemoveAnimal(string strId)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_ANIMAIS where strId = @strId");
            ((LightBaseParameter)command.Parameters["strId"]).Value = strId;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }

        public Animal GetAnimalByIdCompleto(string strId)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strId = @strId");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strId"]).Value = strId;
            var qReader = command.ExecuteReader();
            var retVal = new Animal();

            if (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt32(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt32(qReader["intNumeroOrdem"]);
                if (qReader["strNomeFazenda"] != DBNull.Value) retVal.NomeFazenda = qReader["strNomeFazenda"].ToString();
                if (qReader["strNome"] != DBNull.Value) retVal.Nome = qReader["strNome"].ToString();

                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["strSexo"] != DBNull.Value) retVal.Sexo = qReader["strSexo"].ToString();
                if (qReader["strRgn"] != DBNull.Value) retVal.Rgn = qReader["strRgn"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                if (qReader["strRgd"] != DBNull.Value) retVal.Rgd = qReader["strRgd"].ToString();
                if (qReader["strRgnNumero"] != DBNull.Value) retVal.RgnNumero = Convert.ToInt64(qReader["strRgnNumero"]);
                if (qReader["strRgdNumero"] != DBNull.Value) retVal.RgdNumero = Convert.ToInt64(qReader["strRgdNumero"]);
                if (qReader["strRgnSerie"] != DBNull.Value) retVal.RgnSerie = qReader["strRgnSerie"].ToString();
                if (qReader["strRgdSerie"] != DBNull.Value) retVal.RgdSerie = qReader["strRgdSerie"].ToString();

                if (qReader["strReceptoraId"] != DBNull.Value)
                {
                    retVal.StrReceptoraId = qReader["strReceptoraId"].ToString();
                    retVal.NomeReceptora = GetNomeAnimal(retVal.StrReceptoraId);
                }

                if (qReader["vfRgd"] != DBNull.Value)
                {
                    retVal.TemRgd = (bool)qReader["vfRgd"];
                }

                if (qReader["vfLaudoDna"] != DBNull.Value)
                {
                    retVal.TemLaudoDna = (bool)qReader["vfLaudoDna"];
                }

                if (qReader["vfLaudoArquivoPermanente"] != DBNull.Value)
                {
                    retVal.TemLaudoArquivoPermanente = (bool)qReader["vfLaudoArquivoPermanente"];
                }

                if (qReader["vfLaudoSecundario1"] != DBNull.Value)
                {
                    retVal.TemLaudoSecundario1 = (bool)qReader["vfLaudoSecundario1"];
                }

                if (qReader["vfLaudoSecundario2"] != DBNull.Value)
                {
                    retVal.TemLaudoSecundario2 = (bool)qReader["vfLaudoSecundario2"];
                }

                if (qReader["vfLaudoBetaCaseina"] != DBNull.Value)
                {
                    retVal.TemLaudoBetaCaseina = (bool)qReader["vfLaudoBetaCaseina"];
                }

                if (qReader["vfLaudoKappaCaseina"] != DBNull.Value)
                {
                    retVal.TemLaudoKappaCaseina = (bool)qReader["vfLaudoKappaCaseina"];
                }

                if (qReader["vfFiv"] != DBNull.Value)
                {
                    retVal.EhFIV = (bool)qReader["vfFiv"];
                }

                if (!String.IsNullOrEmpty(retVal.Nome))
                {
                    retVal.NomeRg = retVal.Nome + " - " + retVal.Rgd;
                }

                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["decPn"] != DBNull.Value) retVal.Pn = (double)qReader["decPn"];


                if (qReader["strLaudoDna"] != DBNull.Value)
                {
                    string fileNameFinal = qReader["strLaudoDna"].ToString();
                    string fullPathFinal = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal;
                    retVal.LaudoDna = new Arquivo(fileNameFinal, null);
                }

                if (qReader["strLaudoDna2"] != DBNull.Value)
                {
                    string fileNameFinal = qReader["strLaudoDna2"].ToString();
                    string fullPathFinal = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal;
                    retVal.LaudoDna2 = new Arquivo(fileNameFinal, null);
                }

                if (qReader["strLaudoDna3"] != DBNull.Value)
                {
                    string fileNameFinal = qReader["strLaudoDna3"].ToString();
                    string fullPathFinal = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal;
                    retVal.LaudoDna3 = new Arquivo(fileNameFinal, null);
                }

                if (qReader["strLaudoDna4"] != DBNull.Value)
                {
                    string fileNameFinal = qReader["strLaudoDna4"].ToString();
                    string fullPathFinal = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal;
                    retVal.LaudoDna4 = new Arquivo(fileNameFinal, null);
                }

                if (qReader["strLaudoBetaCaseina"] != DBNull.Value)
                {
                    string fileNameFinal = qReader["strLaudoBetaCaseina"].ToString();
                    string fullPathFinal = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal;
                    retVal.LaudoBetaCaseina = new Arquivo(fileNameFinal, null);
                }

                if (qReader["strLaudoKappaCaseina"] != DBNull.Value)
                {
                    string fileNameFinal = qReader["strLaudoKappaCaseina"].ToString();
                    string fullPathFinal = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal;
                    retVal.LaudoKappaCaseina = new Arquivo(fileNameFinal, null);
                }

                if (qReader["strTipoBetaCaseina"] != DBNull.Value) retVal.TipoBetaCaseina = qReader["strTipoBetaCaseina"].ToString();

                if (qReader["strTipoKappaCaseina"] != DBNull.Value) retVal.TipoKappaCaseina = qReader["strTipoKappaCaseina"].ToString();

                if (qReader["intCdnOrigem"] != DBNull.Value) retVal.CdnOrigem = Convert.ToInt64(qReader["intCdnOrigem"]);
                if (qReader["intCdcOrigem"] != DBNull.Value) retVal.CdcOrigem = Convert.ToInt64(qReader["intCdcOrigem"]);
                if (qReader["docObservacoes"] != DBNull.Value) retVal.Observacoes = qReader["docObservacoes"].ToString();

                if (qReader["strFoto"] != DBNull.Value)
                {
                    string fileNameFinal2 = qReader["strFoto"].ToString();
                    string fullPathFinal2 = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal2;
                    retVal.Foto = new Arquivo(fileNameFinal2, null);
                }

                if (qReader["dtDataUltimoParto"] != DBNull.Value) retVal.DataUltimoParto = (DateTime)qReader["dtDataUltimoParto"];
                if (qReader["strRgdSerie"] != DBNull.Value) retVal.RgdSerie = qReader["strRgdSerie"].ToString();
                if (qReader["strId"] != DBNull.Value) retVal.StrId = qReader["strId"].ToString();

                if (qReader["strPaiId"] != DBNull.Value)
                {
                    retVal.StrPaiId = qReader["strPaiId"].ToString();
                    retVal.NomePai = GetNomeAnimal(retVal.StrPaiId);
                }

                if (qReader["strMaeId"] != DBNull.Value)
                {
                    retVal.StrMaeId = qReader["strMaeId"].ToString();
                    retVal.NomeMae = GetNomeAnimal(retVal.StrMaeId);
                }

                if (qReader["dtDataCdc"] != DBNull.Value) retVal.DataCdc = (DateTime)qReader["dtDataCdc"];
                if (qReader["strCria"] != DBNull.Value) retVal.NCria = qReader["strCria"].ToString();
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];

                if (qReader["vfRgn"] != DBNull.Value)
                {
                    retVal.TemRgn = (bool)qReader["vfRgn"];
                }

                if (qReader["strTipoParto"] != DBNull.Value) retVal.TipoParto = qReader["strTipoParto"].ToString();
                if (qReader["strVigorBez"] != DBNull.Value) retVal.VigorBez = qReader["strVigorBez"].ToString();
                if (qReader["strEstadoCorporalMae"] != DBNull.Value) retVal.EstadoCorporalMae = qReader["strEstadoCorporalMae"].ToString();
                if (qReader["strTamanhoTeta"] != DBNull.Value) retVal.TamanhoTeta = qReader["strTamanhoTeta"].ToString();
                if (qReader["strMaeBoaLeite"] != DBNull.Value) retVal.MaeBoaLeite = qReader["strMaeBoaLeite"].ToString();

                if (qReader["vfMaeOrdenhada"] != DBNull.Value)
                {
                    retVal.MaeOrdenhada = (bool)qReader["vfMaeOrdenhada"];
                }

                if (qReader["vfAnimalImprodutivo"] != DBNull.Value)
                {
                    retVal.AnimalImprodutivo = (bool)qReader["vfAnimalImprodutivo"];
                }

                DataTable tableHistoricos = (DataTable)qReader["Historico"];
                int i = 0;
                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    Historico historico = DataRowToHistorico(rowHistorico);
                    historico.Id = i;
                    retVal.Historicos.Add(historico);
                    i++;
                }

                DataTable tableMensuracoes = (DataTable)qReader["Mensuracoes"];
                int j = 0;
                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    Mensuracao mensuracao = DataRowToMensuracaoAnimal(rowMensuracao);
                    mensuracao.Id = j;
                    retVal.Mensuracoes.Add(mensuracao);
                    j++;
                }

            }

            qReader.Close();
            return retVal;
        }


        public Animal GetAnimalById(string strId)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strId = @strId");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strId"]).Value = strId;
            var qReader = command.ExecuteReader();
            var retVal = new Animal();

            if (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt32(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt32(qReader["intNumeroOrdem"]);
                if (qReader["strNomeFazenda"] != DBNull.Value) retVal.NomeFazenda = qReader["strNomeFazenda"].ToString();
                if (qReader["strNome"] != DBNull.Value) retVal.Nome = qReader["strNome"].ToString();

                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["strSexo"] != DBNull.Value) retVal.Sexo = qReader["strSexo"].ToString();
                if (qReader["strRgn"] != DBNull.Value) retVal.Rgn = qReader["strRgn"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                if (qReader["strRgd"] != DBNull.Value) retVal.Rgd = qReader["strRgd"].ToString();

                if (!String.IsNullOrEmpty(retVal.Nome))
                {
                    retVal.NomeRg = retVal.Nome + " - " + retVal.Rgd;
                }

                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();


                if (qReader["strPaiId"] != DBNull.Value) retVal.StrPaiId = qReader["strPaiId"].ToString();
                if (qReader["strMaeId"] != DBNull.Value) retVal.StrMaeId = qReader["strMaeId"].ToString();


            }


            qReader.Close();
            return retVal;
        }

        public long AdicionaLote(Lote lote)
        {
            var ultimoLote = GetUltimoLote(lote.Raca, lote.IdPropriedade, lote.DataControle);

            var command = new LightBaseCommand(@"insert into FCARNAUBA_LOTE_CONTROLE_LEITEIRO
                diretorio,
                strLote,
                dtDataControle,
                strIdPropriedade,
                strRaca,
                strLoteDataPropriedade,
                strCategoria,
                str1Ordenha,
                str2Ordenha,
                str3Ordenha,
                strControlador,
                strUsuario,
                dtDataUsuario,
                vfLiberarLotePesagem
                 
                values
                
                (@diretorio,
                @strLote,
                @dtDataControle,
                @strIdPropriedade,
                @strRaca,
                @strLoteDataPropriedade,
                @strCategoria,
                @str1Ordenha,
                @str2Ordenha,
                @str3Ordenha,
                @strControlador,
                @strUsuario,
                @dtDataUsuario,
                @vfLiberarLotePesagem)", _Connection);
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = lote.Diretorio;
            ((LightBaseParameter)command.Parameters["strLote"]).Value = lote.SLote;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = lote.DataControle;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = lote.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = lote.DataUsuario;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = lote.IdPropriedade;
            ((LightBaseParameter)command.Parameters["strLoteDataPropriedade"]).Value = lote.LoteDataPropriedade;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = lote.Raca;
            ((LightBaseParameter)command.Parameters["strCategoria"]).Value = lote.Categoria;
            ((LightBaseParameter)command.Parameters["str1Ordenha"]).Value = lote.POrdenha;
            ((LightBaseParameter)command.Parameters["str2Ordenha"]).Value = lote.SOrdenha;
            ((LightBaseParameter)command.Parameters["str3Ordenha"]).Value = lote.TOrdenha;
            ((LightBaseParameter)command.Parameters["strControlador"]).Value = lote.Controlador;
            ((LightBaseParameter)command.Parameters["vfLiberarLotePesagem"]).Value = lote.LiberarLotePesagem;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

            const string idRetrievingCommand = "@@Id";
            LightBaseCommand lastIdCommand = new LightBaseCommand(idRetrievingCommand, _Connection);
            long ultimoId = Convert.ToInt32(lastIdCommand.ExecuteScalar());


            if (ultimoLote != null)
            {
                var producoes = ultimoLote.ProducoesLeite;

                var orderProducoes = producoes.OrderBy(s => s.NomeMatriz);

                foreach (ProducaoLeite producao in orderProducoes)
                {
                    if (!producao.SairControle)
                    {
                        ProducaoLeite producaoLeite = new ProducaoLeite();
                        producaoLeite.IdMatriz = producao.IdMatriz;
                        producaoLeite.IdCria = producao.IdCria;
                        producaoLeite.DataEntradaControle = producao.DataEntradaControle;
                        AdicionaProducaoLeite(Convert.ToInt32(ultimoId), producaoLeite);
                    }
                }
            }


            return ultimoId;

        }


        public void SalvaLote(Lote lote)
        {

            List<string> campos = new List<string>()
                                      {
                                          "diretorio",
                                          "strLote",
                                          "dtDataControle",
                                          "strIdPropriedade",
                                          "strRaca",
                                          "strLoteDataPropriedade",
                                          "strCategoria",
                                          "str1Ordenha",
                                          "str2Ordenha",
                                          "str3Ordenha",
                                          "strControlador",
                                          "strUsuario",
                                          "dtDataUsuario",
                                          "vfLiberarLotePesagem"
                                      };

            var command = new LightBaseCommand(BuildLoteString(campos));
            ((LightBaseParameter)command.Parameters["id"]).Value = lote.Id;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = lote.Diretorio;
            ((LightBaseParameter)command.Parameters["strLote"]).Value = lote.SLote;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = lote.DataControle;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = lote.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = lote.DataUsuario;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = lote.IdPropriedade;
            ((LightBaseParameter)command.Parameters["strLoteDataPropriedade"]).Value = lote.LoteDataPropriedade;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = lote.Raca;
            ((LightBaseParameter)command.Parameters["strCategoria"]).Value = lote.Categoria;
            ((LightBaseParameter)command.Parameters["str1Ordenha"]).Value = lote.POrdenha;
            ((LightBaseParameter)command.Parameters["str2Ordenha"]).Value = lote.SOrdenha;
            ((LightBaseParameter)command.Parameters["str3Ordenha"]).Value = lote.TOrdenha;
            ((LightBaseParameter)command.Parameters["strControlador"]).Value = lote.Controlador;
            ((LightBaseParameter)command.Parameters["vfLiberarLotePesagem"]).Value = lote.LiberarLotePesagem;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public List<ResultadoBuscaLote> ConsultaLote(ParametrosDeBuscaEmLotes parametrosBuscaEmLotes)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmLotes.TodosOsCampos);
            if (parametrosBuscaEmLotes.Id > 0)
            {
                filter = AddParametro(filter, "id", parametrosBuscaEmLotes.Id.ToString());
            }
            if (parametrosBuscaEmLotes.Lote != null)
            {
                filter = AddParametro(filter, "strLote", parametrosBuscaEmLotes.Lote.ToString());
            }

            if (parametrosBuscaEmLotes.DataControle != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmLotes.DataControle.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmLotes.DataControleInicial != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmLotes.DataControleInicial.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmLotes.DataControleFinal != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmLotes.DataControleFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strIdPropriedade", parametrosBuscaEmLotes.IdPropriedade);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmLotes.Raca);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var lotes = new List<ResultadoBuscaLote>();

            while (qReader.Read())
            {

                var retVal = new ResultadoBuscaLote();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }
                if (qReader["strLoteDataPropriedade"] != DBNull.Value) retVal.LoteDataPropriedade = qReader["strLoteDataPropriedade"].ToString();

                if (qReader["vfLiberarLotePesagem"] != DBNull.Value)
                {
                    retVal.LiberarLotePesagem = (bool)qReader["vfLiberarLotePesagem"];
                    if (retVal.LiberarLotePesagem)
                    {
                        retVal.LiberarLotePesagemStr = "SIM";
                    }
                    else
                    {
                        retVal.LiberarLotePesagemStr = "NÃO";
                    }
                }
                else
                {
                    retVal.LiberarLotePesagemStr = "NÃO";
                    retVal.LiberarLotePesagem = false;
                }

                lotes.Add(retVal);
            }
            qReader.Close();

            var orderLotes = lotes.OrderBy(s => s.DataControle);

            return orderLotes.ToList();
        }


        public Lote GetLoteById(string id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new Lote();

            if (qReader.Read())
            {

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }
                if (qReader["strLoteDataPropriedade"] != DBNull.Value) retVal.LoteDataPropriedade = qReader["strLoteDataPropriedade"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                if (qReader["strCategoria"] != DBNull.Value) retVal.Categoria = qReader["strCategoria"].ToString();
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();
                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["strControlador"].ToString();

                if (qReader["vfLiberarLotePesagem"] != DBNull.Value)
                {
                    retVal.LiberarLotePesagem = (bool)qReader["vfLiberarLotePesagem"];
                }

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                int i = 0;
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    ProducaoLeite producaoLeite = DataRowToProducaoLeiteCompleto(rowProducoesLeite);
                    producaoLeite.Id = i;
                    var prod = GetProducaoAcumulada(retVal.DataControle, producaoLeite.DiasLactacao, retVal.Id.ToString(), producaoLeite.IdMatriz);
                    producaoLeite.Acumulado = prod.Acumulada;
                    retVal.ProducoesLeite.Add(producaoLeite);
                    i++;
                }

            }

            qReader.Close();
            return retVal;
        }

        public List<ProducaoLeite> ProducoesLeiteEnc(int ano, string idFazenda, string raca)
        {
            string filter = "";

            DateTime dataInicio = new DateTime(ano, 01, 01);
            DateTime dataFim = new DateTime(ano, 12, 31);

            filter = AddParametro(filter, "strIdPropriedade", idFazenda);
            filter = AddParametro(filter, "strRaca", raca);
            filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
            filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

            filter = AddParametro(filter, "PL_vfSairControle", "1");

            var command = new LightBaseCommand("textsearch in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            List<ProducaoLeite> producoesLeite = new List<ProducaoLeite>();

            while (qReader.Read())
            {
                var retVal = new Lote();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                int i = 0;
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    ProducaoLeite producaoLeite = DataRowToProducaoLeiteCompleto(rowProducoesLeite);

                    if (producaoLeite.SairControleSr == "SIM")
                    {

                        producaoLeite.Id = i;
                        var prod = GetProducaoAcumulada(retVal.DataControle, retVal.Id.ToString(), producaoLeite.IdMatriz);
                        producaoLeite.AcumuladoLactacao = prod.Acumulada;
                        producaoLeite.Maxima = GetProducaoMaximaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                        producaoLeite.Media = GetProducaoMediaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                        producaoLeite.Raca = raca;
                        producaoLeite.NomePropriedade = GetNomePropriedade(idFazenda);
                        producaoLeite.Ano = ano;
                        producoesLeite.Add(producaoLeite);

                    }
                    i++;
                }

            }


            qReader.Close();
            return producoesLeite;
        }

        public List<ProducaoLeite> ProducoesLeiteEnc(int ano, string idAnimal)
        {
            DateTime dataInicio = new DateTime(ano, 01, 01);
            DateTime dataFim = new DateTime(ano, 12, 31);
            List<Animal> crias = new List<Animal>();
            string campo = "";
            LightBaseCommand command = null;
            var animal = GetAnimalById(idAnimal);

            string raca = animal.Raca;
            string idFazenda = GetIdPropriedade(animal.NomeFazenda);

            if (animal.Sexo == "Fêmea")
            {
                campo = "strMaeId";
                command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strMaeId=@strMaeId order by strNome");
            }
            else
            {
                campo = "strPaiId";
                command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strPaiId=@strPaiId order by strNome");
            }

            ((LightBaseParameter)command.Parameters[campo]).Value = idAnimal;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            List<ProducaoLeite> producoesLeite = new List<ProducaoLeite>();

            while (qReader.Read())
            {
                string idCria = qReader["id"].ToString();
                var cria = GetAnimalById(idCria);
                if (cria.Sexo == "Fêmea")
                {
                    crias.Add(cria);
                }
            }
            qReader.Close();

            foreach (Animal criaTemp in crias) {

                string filter = "";
                filter = AddParametro(filter, "FK_PL_strIdMatriz", criaTemp.Id.ToString());
                //filter = AddParametro(filter, "PL_vfSairControle", "1");
                filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
                filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

                var command2 = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

                command2.Connection = _Connection;

                var qReader2 = command2.ExecuteReader();
                //int i = 0;
                bool encontrouProducao = false;
                while (qReader2.Read())
                {
                    var retVal = new Lote();

                    CultureInfo cult = new CultureInfo("pt-BR");

                    if (qReader2["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader2["id"]);
                    if (qReader2["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader2["dtDataControle"];

                    DataTable tableProducoesLeite = (DataTable)qReader2["Producao_de_Leite"];
                    
                    foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                    {
                        ProducaoLeite producaoLeite = DataRowToProducaoLeiteCompleto(rowProducoesLeite);

                        if (producaoLeite.SairControleSr == "SIM" && producaoLeite.IdMatriz == criaTemp.Id.ToString())
                        {

                            //producaoLeite.Id = i;
                            producaoLeite.NomePaiMae = animal.NomeCompleto;
                            var prod = GetProducaoAcumulada(retVal.DataControle, retVal.Id.ToString(), producaoLeite.IdMatriz);
                            producaoLeite.AcumuladoLactacao = prod.Acumulada;
                            producaoLeite.Maxima = GetProducaoMaximaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                            producaoLeite.Media = GetProducaoMediaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                            producaoLeite.Raca = raca;
                            //producaoLeite.NomePropriedade = GetNomePropriedade(idFazenda);
                            producaoLeite.Ano = ano;
                            producoesLeite.Add(producaoLeite);
                            //i++;
                            encontrouProducao = true;
                            break;
                        }
                        
                    }
                    if (encontrouProducao)
                        break;
                }

                qReader2.Close();
            }

            return producoesLeite;
        }

        public List<ProducaoLeite> ProducoesLeiteEnc(int ano, string idFazenda, string raca, string tipo)
        {
            string filter = "";

            DateTime dataInicio = new DateTime(ano, 01, 01);
            DateTime dataFim = new DateTime(ano, 12, 31);

            filter = AddParametro(filter, "strIdPropriedade", idFazenda);
            filter = AddParametro(filter, "strRaca", raca);
            filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
            filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

            filter = AddParametro(filter, "PL_vfSairControle", "1");

            var command = new LightBaseCommand("textsearch in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            List<ProducaoLeite> producoesLeite = new List<ProducaoLeite>();

            while (qReader.Read())
            {
                var retVal = new Lote();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                int i = 0;
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    ProducaoLeite producaoLeite = DataRowToProducaoLeiteCompleto(rowProducoesLeite);

                    if (producaoLeite.SairControleSr == "SIM")
                    {

                        producaoLeite.Id = i;

                        if (tipo == "Acumulado")
                        {
                            var prod = GetProducaoAcumulada(retVal.DataControle, retVal.Id.ToString(), producaoLeite.IdMatriz);
                            producaoLeite.AcumuladoLactacao = prod.Acumulada;
                        }

                        else if (tipo == "Maxima")
                        {
                            producaoLeite.Maxima = GetProducaoMaximaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                        }

                        else if (tipo == "Gordura")
                        {
                            producaoLeite.GordMedia = GetGorduraMediaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                        }

                        else if (tipo == "Proteina")
                        {
                            producaoLeite.ProtMedia = GetProteinaMediaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                        }

                        else if (tipo == "RQueijeiro")
                        {
                            var prodLeite = GetGordProtMediaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                            var prod = GetProducaoAcumulada(retVal.DataControle, retVal.Id.ToString(), producaoLeite.IdMatriz);

                            double rendQueijeiro = (((3.5 * prodLeite.ProtMedia) + (1.23 * prodLeite.GordMedia) - 0.88) / 100) * prod.Acumulada;

                            producaoLeite.RQueijeiro = rendQueijeiro;
                        }

                        else
                        {
                            producaoLeite.Media = GetProducaoMediaMatriz(producaoLeite.IdMatriz, ano, idFazenda, raca);
                        }
                        producaoLeite.Raca = raca;
                        producaoLeite.NomePropriedade = GetNomePropriedade(idFazenda);
                        producaoLeite.Ano = ano;
                        producoesLeite.Add(producaoLeite);

                    }
                    i++;
                }

            }


            qReader.Close();

            if (tipo == "Maxima")
            {
                var orderProducoes = producoesLeite.OrderByDescending(s => s.Maxima);
                return orderProducoes.ToList();
            }

            else if (tipo == "Acumulado")
            {
                var orderProducoes = producoesLeite.OrderByDescending(s => s.AcumuladoLactacao);
                return orderProducoes.ToList();
            }

            else if (tipo == "Gordura")
            {
                var orderProducoes = producoesLeite.OrderByDescending(s => s.GordMedia);
                return orderProducoes.ToList();
            }

            else if (tipo == "Proteina")
            {
                var orderProducoes = producoesLeite.OrderByDescending(s => s.ProtMedia);
                return orderProducoes.ToList();
            }

            else if (tipo == "RQueijeiro")
            {
                var orderProducoes = producoesLeite.OrderByDescending(s => s.RQueijeiro);
                return orderProducoes.ToList();
            }

            else
            {
                var orderProducoes = producoesLeite.OrderByDescending(s => s.Media);
                return orderProducoes.ToList();
            }

        }

        public Lote GetUltimoLote(string raca, string idPropriedade)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade order by id desc");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = idPropriedade;
            var qReader = command.ExecuteReader();
            var retVal = new Lote();

            if (qReader.Read())
            {

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }
                if (qReader["strLoteDataPropriedade"] != DBNull.Value) retVal.LoteDataPropriedade = qReader["strLoteDataPropriedade"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                if (qReader["strCategoria"] != DBNull.Value) retVal.Categoria = qReader["strCategoria"].ToString();
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();
                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["strControlador"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                int i = 0;
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    ProducaoLeite producaoLeite = DataRowToProducaoLeiteCompleto(rowProducoesLeite);
                    producaoLeite.Id = i;
                    retVal.ProducoesLeite.Add(producaoLeite);
                    i++;
                }
            }

            qReader.Close();
            return retVal;
        }


        public Lote GetUltimoLote(string raca, string idPropriedade, DateTime dataControle)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataControle<@dtDataControle order by dtDataControle desc");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = idPropriedade;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControle;
            var qReader = command.ExecuteReader();
            var retVal = new Lote();

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }
                if (qReader["strLoteDataPropriedade"] != DBNull.Value) retVal.LoteDataPropriedade = qReader["strLoteDataPropriedade"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                if (qReader["strCategoria"] != DBNull.Value) retVal.Categoria = qReader["strCategoria"].ToString();
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();
                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["strControlador"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                int i = 0;
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    ProducaoLeite producaoLeite = DataRowToProducaoLeiteCompleto(rowProducoesLeite);
                    producaoLeite.Id = i;
                    retVal.ProducoesLeite.Add(producaoLeite);
                    i++;
                }

            }


            qReader.Close();
            return retVal;
        }

        public void RemoveLote(string id)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }

        public long AdicionaLotePonderal(LotePonderal lotePonderal)
        {
            var ultimoLotePonderal = GetUltimoLotePonderal(lotePonderal.Raca, lotePonderal.IdPropriedade, lotePonderal.DataControle);

            var command = new LightBaseCommand(@"insert into FCARNAUBA_LOTE_CONTROLE_PONDERAL
                diretorio,
                strLote,
                dtDataControle,
                strIdPropriedade,
                strRaca,
                strLoteDataPropriedade,
                strControlador,
                strUsuario,
                dtDataUsuario,
                vfLiberarLoteMensuracao
                 
                values
                
                (@diretorio,
                @strLote,
                @dtDataControle,
                @strIdPropriedade,
                @strRaca,
                @strLoteDataPropriedade,
                @strControlador,
                @strUsuario,
                @dtDataUsuario,
                @vfLiberarLoteMensuracao)", _Connection);
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = lotePonderal.Diretorio;
            ((LightBaseParameter)command.Parameters["strLote"]).Value = lotePonderal.SLote;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = lotePonderal.DataControle;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = lotePonderal.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = lotePonderal.DataUsuario;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = lotePonderal.IdPropriedade;
            ((LightBaseParameter)command.Parameters["strLoteDataPropriedade"]).Value = lotePonderal.LoteDataPropriedade;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = lotePonderal.Raca;
            ((LightBaseParameter)command.Parameters["strControlador"]).Value = lotePonderal.Controlador;
            ((LightBaseParameter)command.Parameters["vfLiberarLoteMensuracao"]).Value = lotePonderal.LiberarLoteMensuracao;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

            const string idRetrievingCommand = "@@Id";
            LightBaseCommand lastIdCommand = new LightBaseCommand(idRetrievingCommand, _Connection);
            long ultimoId = Convert.ToInt32(lastIdCommand.ExecuteScalar());


            if (ultimoLotePonderal != null)
            {
                var mensuracoes = ultimoLotePonderal.Mensuracoes;

                var orderMensuracoes = mensuracoes.OrderBy(s => s.NomeAnimal);

                foreach (Mensuracao mensuracao in orderMensuracoes)
                {
                    if (!mensuracao.SairControle)
                    {
                        Mensuracao mensuracaoMedidas = new Mensuracao();
                        mensuracaoMedidas.IdAnimal = mensuracao.IdAnimal; ;
                        mensuracaoMedidas.DataEntradaControle = mensuracao.DataEntradaControle;
                        AdicionaMensuracao(ultimoId.ToString(), mensuracaoMedidas);
                    }
                }
            }


            return ultimoId;

        }

        public void SalvaLotePonderal(LotePonderal lotePonderal)
        {

            List<string> campos = new List<string>()
                                      {
                                          "diretorio",
                                          "strLote",
                                          "dtDataControle",
                                          "strIdPropriedade",
                                          "strRaca",
                                          "strLoteDataPropriedade",
                                          "strControlador",
                                          "strUsuario",
                                          "dtDataUsuario",
                                          "vfLiberarLoteMensuracao"      
                                      };

            var command = new LightBaseCommand(BuildLotePonderalString(campos));
            ((LightBaseParameter)command.Parameters["id"]).Value = lotePonderal.Id;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = lotePonderal.Diretorio;
            ((LightBaseParameter)command.Parameters["strLote"]).Value = lotePonderal.SLote;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = lotePonderal.DataControle;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = lotePonderal.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = lotePonderal.DataUsuario;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = lotePonderal.IdPropriedade;
            ((LightBaseParameter)command.Parameters["strLoteDataPropriedade"]).Value = lotePonderal.LoteDataPropriedade;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = lotePonderal.Raca;
            ((LightBaseParameter)command.Parameters["strControlador"]).Value = lotePonderal.Controlador;
            ((LightBaseParameter)command.Parameters["vfLiberarLoteMensuracao"]).Value = lotePonderal.LiberarLoteMensuracao;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public List<ResultadoBuscaLotePonderal> ConsultaLotePonderal(ParametrosDeBuscaEmLotesPonderais parametrosBuscaEmLotesPonderais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmLotesPonderais.TodosOsCampos);
            if (parametrosBuscaEmLotesPonderais.Id > 0)
            {
                filter = AddParametro(filter, "id", parametrosBuscaEmLotesPonderais.Id.ToString());
            }
            if (parametrosBuscaEmLotesPonderais.Lote != null)
            {
                filter = AddParametro(filter, "strLote", parametrosBuscaEmLotesPonderais.Lote.ToString());
            }

            if (parametrosBuscaEmLotesPonderais.DataControle != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmLotesPonderais.DataControle.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmLotesPonderais.DataControleInicial != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmLotesPonderais.DataControleInicial.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmLotesPonderais.DataControleFinal != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmLotesPonderais.DataControleFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strIdPropriedade", parametrosBuscaEmLotesPonderais.IdPropriedade);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmLotesPonderais.Raca);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_LOTE_CONTROLE_PONDERAL " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var lotesPonderais = new List<ResultadoBuscaLotePonderal>();

            while (qReader.Read())
            {

                var retVal = new ResultadoBuscaLotePonderal();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }
                if (qReader["strLoteDataPropriedade"] != DBNull.Value) retVal.LoteDataPropriedade = qReader["strLoteDataPropriedade"].ToString();

                if (qReader["vfLiberarLoteMensuracao"] != DBNull.Value)
                {
                    retVal.LiberarLoteMensuracao = (bool)qReader["vfLiberarLoteMensuracao"];
                    if (retVal.LiberarLoteMensuracao)
                    {
                        retVal.LiberarLoteMensuracaoStr = "SIM";
                    }
                    else
                    {
                        retVal.LiberarLoteMensuracaoStr = "NÃO";
                    }
                }
                else
                {
                    retVal.LiberarLoteMensuracaoStr = "NÃO";
                    retVal.LiberarLoteMensuracao = false;
                }

                lotesPonderais.Add(retVal);
            }
            qReader.Close();

            var orderLotesPonderais = lotesPonderais.OrderBy(s => s.DataControle);

            return orderLotesPonderais.ToList();
        }

        public LotePonderal GetUltimoLotePonderal(string raca, string idPropriedade, DateTime dataControle)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataControle<@dtDataControle order by id desc");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = idPropriedade;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControle;
            var qReader = command.ExecuteReader();
            var retVal = new LotePonderal();

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }
                if (qReader["strLoteDataPropriedade"] != DBNull.Value) retVal.LoteDataPropriedade = qReader["strLoteDataPropriedade"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["strControlador"].ToString();

                DataTable tableMensuracoes = (DataTable)qReader["Mensuracoes"];
                int i = 0;
                foreach (DataRow rowMensuracoes in tableMensuracoes.Rows)
                {
                    Mensuracao mensuracao = DataRowToMensuracao(rowMensuracoes);
                    mensuracao.Id = i;
                    retVal.Mensuracoes.Add(mensuracao);
                    i++;
                }

            }


            qReader.Close();
            return retVal;
        }


        public LotePonderal GetLotePonderalById(string id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new LotePonderal();

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }
                if (qReader["strLoteDataPropriedade"] != DBNull.Value) retVal.LoteDataPropriedade = qReader["strLoteDataPropriedade"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["strControlador"].ToString();

                if (qReader["vfLiberarLoteMensuracao"] != DBNull.Value)
                {
                    retVal.LiberarLoteMensuracao = (bool)qReader["vfLiberarLoteMensuracao"];
                }

                DataTable tableMensuracoes = (DataTable)qReader["Mensuracoes"];
                int i = 0;
                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    Mensuracao mensuracao = DataRowToMensuracao(rowMensuracao);
                    mensuracao.Id = i;
                    retVal.Mensuracoes.Add(mensuracao);
                    i++;
                }

            }


            qReader.Close();
            return retVal;
        }

        public List<LotePonderal> GetLotesPonderais()
        {
            var command = new LightBaseCommand(@"select id, strLote, dtDataControle, strIdPropriedade from FCARNAUBA_LOTE_CONTROLE_PONDERAL order by dtDataControle desc");

            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var lotes = new List<LotePonderal>();

            while (qReader.Read())
            {

                LotePonderal lote = new LotePonderal();
                if (qReader["id"] != DBNull.Value) lote.Id = Convert.ToInt32(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) lote.SLote = Convert.ToString(qReader["strLote"]);
                if (qReader["dtDataControle"] != DBNull.Value) lote.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strIdPropriedade"] != DBNull.Value) lote.IdPropriedade = Convert.ToString(qReader["strIdPropriedade"]);
                var nomePropriedade = GetNomePropriedade(lote.IdPropriedade);
                DateTime dataControle = (DateTime)qReader["dtDataControle"];
                lote.LoteDataPropriedade = lote.SLote + " - " + dataControle.ToString("dd/MM/yyyy") + " - " + nomePropriedade;
                lotes.Add(lote);

            }
            qReader.Close();
            return lotes;
        }

        public List<LotePonderal> GetLotesPonderaisParaMensuracoes()
        {
            var command = new LightBaseCommand(@"select id, strLote, dtDataControle, strIdPropriedade from FCARNAUBA_LOTE_CONTROLE_PONDERAL where vfLiberarLoteMensuracao = @vfLiberarLoteMensuracao order by dtDataControle desc");
            ((LightBaseParameter)command.Parameters["vfLiberarLoteMensuracao"]).Value = 1;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var lotes = new List<LotePonderal>();
            LotePonderal loteVazio = new LotePonderal();
            loteVazio.Id = 0;
            loteVazio.LoteDataPropriedade = "Selecione o Lote";
            lotes.Add(loteVazio);

            while (qReader.Read())
            {

                LotePonderal lote = new LotePonderal();
                if (qReader["id"] != DBNull.Value) lote.Id = Convert.ToInt32(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) lote.SLote = Convert.ToString(qReader["strLote"]);
                if (qReader["dtDataControle"] != DBNull.Value) lote.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strIdPropriedade"] != DBNull.Value) lote.IdPropriedade = Convert.ToString(qReader["strIdPropriedade"]);
                var nomePropriedade = GetNomePropriedade(lote.IdPropriedade);
                DateTime dataControle = (DateTime)qReader["dtDataControle"];
                lote.LoteDataPropriedade = lote.SLote + " - " + dataControle.ToString("dd/MM/yyyy") + " - " + nomePropriedade;
                lotes.Add(lote);

            }
            qReader.Close();
            return lotes;
        }

        public void EncerrarMensuracoes(long idLotePonderal)
        {
            var command = new LightBaseCommand(@"update FCARNAUBA_LOTE_CONTROLE_PONDERAL set vfLiberarLoteMensuracao=@vfLiberarLoteMensuracao where id = @id");
            ((LightBaseParameter)command.Parameters["vfLiberarLoteMensuracao"]).Value = 0;
            ((LightBaseParameter)command.Parameters["id"]).Value = idLotePonderal;

            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public void RemoveLotePonderal(string id)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_LOTE_CONTROLE_PONDERAL where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }

        public long AdicionaCdc(Cdc cdc)
        {
            var command = new LightBaseCommand(@"insert into FCARNAUBA_CDC
                diretorio,
                intCdc,
                strTipo,
                dtDataCobertura,
                FK_strIdTouro,
                strUsuario,
                dtDataUsuario,
                strVeterinario
                strRaca
                strIdPropriedade
                 
                values
                
                (@diretorio,
                @intCdc,
                @strTipo,
                @dtDataCobertura,
                @FK_strIdTouro,
                @strUsuario,
                @dtDataUsuario,
                @strVeterinario,
                @strRaca,
                @strIdPropriedade)", _Connection);
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = cdc.Diretorio;
            ((LightBaseParameter)command.Parameters["intCdc"]).Value = cdc.NCdc;
            ((LightBaseParameter)command.Parameters["strTipo"]).Value = cdc.Tipo;
            ((LightBaseParameter)command.Parameters["dtDataCobertura"]).Value = cdc.DataCobertura;
            ((LightBaseParameter)command.Parameters["FK_strIdTouro"]).Value = cdc.IdTouro;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = cdc.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = cdc.DataUsuario;
            ((LightBaseParameter)command.Parameters["strVeterinario"]).Value = cdc.Veterinario;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = cdc.Raca;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = cdc.IdPropriedade;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

            const string idRetrievingCommand = "@@Id";
            LightBaseCommand lastIdCommand = new LightBaseCommand(idRetrievingCommand, _Connection);
            long ultimoId = Convert.ToInt32(lastIdCommand.ExecuteScalar());

            return ultimoId;

        }


        public void SalvaCdc(Cdc cdc)
        {

            List<string> campos = new List<string>()
                                      {
                                          "diretorio",
                                          "intCdc",
                                          "strTipo",
                                          "dtDataCobertura",
                                          "FK_strIdTouro",
                                          "strUsuario",
                                          "dtDataUsuario",
                                          "strVeterinario",
                                          "strRaca",
                                          "strIdPropriedade"
                                      };

            var command = new LightBaseCommand(BuildCdcString(campos));
            ((LightBaseParameter)command.Parameters["id"]).Value = cdc.Id;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = cdc.Diretorio;
            ((LightBaseParameter)command.Parameters["intCdc"]).Value = cdc.NCdc;
            ((LightBaseParameter)command.Parameters["strTipo"]).Value = cdc.Tipo;
            ((LightBaseParameter)command.Parameters["dtDataCobertura"]).Value = cdc.DataCobertura;
            ((LightBaseParameter)command.Parameters["FK_strIdTouro"]).Value = cdc.IdTouro;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = cdc.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = cdc.DataUsuario;
            ((LightBaseParameter)command.Parameters["strVeterinario"]).Value = cdc.Veterinario;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = cdc.Raca;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = cdc.IdPropriedade;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public List<ResultadoBuscaCdc> ConsultaCdc(ParametrosDeBuscaEmCdc parametrosBuscaEmCdc)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmCdc.TodosOsCampos);
            if (parametrosBuscaEmCdc.Id > 0)
            {
                filter = AddParametro(filter, "id", parametrosBuscaEmCdc.Id.ToString());
            }
            if (parametrosBuscaEmCdc.Cdc != null)
            {
                filter = AddParametro(filter, "intCdc", parametrosBuscaEmCdc.Cdc.ToString());
            }

            if (parametrosBuscaEmCdc.DataCobertura != null)
                filter = AddParametroData(filter, "dtDataCobertura", parametrosBuscaEmCdc.DataCobertura.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmCdc.DataCoberturaInicial != null)
                filter = AddParametroData(filter, "dtDataCobertura", parametrosBuscaEmCdc.DataCoberturaInicial.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmCdc.DataCoberturaFinal != null)
                filter = AddParametroData(filter, "dtDataCobertura", parametrosBuscaEmCdc.DataCoberturaFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "FK_strIdTouro", parametrosBuscaEmCdc.IdTouro);

            filter = AddParametro(filter, "strIdPropriedade", parametrosBuscaEmCdc.IdPropriedade);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmCdc.Raca);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_CDC " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var cdcs = new List<ResultadoBuscaCdc>();

            while (qReader.Read())
            {

                var retVal = new ResultadoBuscaCdc();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intCdc"] != DBNull.Value) retVal.NCdc = Convert.ToInt64(qReader["intCdc"]);
                if (qReader["dtDataCobertura"] != DBNull.Value) retVal.DataCobertura = (DateTime)qReader["dtDataCobertura"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["FK_strIdTouro"] != DBNull.Value)
                {
                    retVal.IdTouro = qReader["FK_strIdTouro"].ToString();
                    retVal.NomeTouro = GetNomeAnimal(retVal.IdTouro);
                }

                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }

                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                cdcs.Add(retVal);
            }
            qReader.Close();

            var orderCds = cdcs.OrderByDescending(s => s.DataCobertura);

            return orderCds.ToList();
        }


        public Cdc GetCdcById(string id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_CDC where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new Cdc();

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intCdc"] != DBNull.Value) retVal.NCdc = Convert.ToInt64(qReader["intCdc"]);
                if (qReader["strTipo"] != DBNull.Value) retVal.Tipo = qReader["strTipo"].ToString();

                if (qReader["dtDataCobertura"] != DBNull.Value) retVal.DataCobertura = (DateTime)qReader["dtDataCobertura"];

                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strVeterinario"] != DBNull.Value) retVal.Veterinario = qReader["strVeterinario"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["FK_strIdTouro"] != DBNull.Value)
                {
                    retVal.IdTouro = qReader["FK_strIdTouro"].ToString();
                    retVal.NomeTouro = GetNomeAnimal(retVal.IdTouro);
                }

                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }

                DataTable tableMatrizes = (DataTable)qReader["Matrizes"];
                int i = 0;
                foreach (DataRow rowMatrizes in tableMatrizes.Rows)
                {
                    Matriz matriz = DataRowToMatriz(rowMatrizes);
                    matriz.Id = i;
                    matriz.CioRepeticao = GetNumeroCio(matriz.IdMatriz, retVal.DataCobertura);
                    retVal.Matrizes.Add(matriz);
                    i++;
                }

            }


            qReader.Close();
            return retVal;
        }

        public void RemoveCdc(string id)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_CDC where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }

        public void AdicionaMatriz(int cdcId, Matriz matriz)
        {
            var command = new LightBaseCommand(@"insert into FCARNAUBA_CDC Matrizes values ({{@MAT_FK_strIdMatriz, @MAT_vfCoberturaEfetiva}}) parent id = @id");

            ((LightBaseParameter)command.Parameters["id"]).Value = cdcId;
            ((LightBaseParameter)command.Parameters["MAT_FK_strIdMatriz"]).Value = matriz.IdMatriz;
            ((LightBaseParameter)command.Parameters["MAT_vfCoberturaEfetiva"]).Value = matriz.CdcEfetiva;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public void RemoveMatriz(int cdcId, int matrizId)
        {
            var command = new LightBaseCommand(@"delete from FCARNAUBA_CDC.Matrizes[" + matrizId + "] where id = " + cdcId);
            OpenConnection();
            command.Connection = _Connection;
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public List<Matriz> GetMatrizes(int cdcId)
        {
            var command = new LightBaseCommand(@"select Matrizes from FCARNAUBA_CDC where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = cdcId;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var retVal = new List<Matriz>();
            DateTime dataCobertura = GetDataCobertura(cdcId.ToString());
            while (qReader.Read())
            {
                DataTable mat = (DataTable)qReader["Matrizes"];
                int i = 0;
                foreach (DataRow dRow in mat.Rows)
                {
                    var curMat = DataRowToMatriz(dRow);
                    curMat.Id = i;
                    curMat.NomeMatriz = GetNomeAnimal(curMat.IdMatriz);
                    curMat.CioRepeticao = GetNumeroCio(curMat.IdMatriz.ToString(), dataCobertura);
                    i++;
                    retVal.Add(curMat);
                }

            }
            qReader.Close();
            return retVal;
        }

        private string BuildMatrizString(List<string> campos, int matrizId)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + ":@" + campo);
            }
            return "update FCARNAUBA_CDC set Matrizes[" + matrizId + "] = {" + String.Join(",", outCampos) +
                   "} where id = @id";
        }

        public void SalvaMatriz(int CdcId, int matrizId, Matriz matriz)
        {
            List<string> campos = new List<string>()
                                      { "MAT_FK_strIdMatriz",
                                        "MAT_vfCoberturaEfetiva",
                                      };

            var command = new LightBaseCommand(BuildMatrizString(campos, matrizId));
            ((LightBaseParameter)command.Parameters["id"]).Value = CdcId;
            ((LightBaseParameter)command.Parameters["MAT_FK_strIdMatriz"]).Value = matriz.IdMatriz;
            ((LightBaseParameter)command.Parameters["MAT_vfCoberturaEfetiva"]).Value = matriz.CdcEfetiva;

            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }


        public void AdicionaControleLeiteiro(ControleLeiteiro controleLeiteiro)
        {
            var command = new LightBaseCommand(@"insert into FCARNAUBA_CONTROLE_LEITEIRO
                diretorio,
                strProprietario,
                strRaca,
                strCategoria,
                strFazenda,
                strMunicipio,
                strUf,
                dtDataControle,
                dtDataProximaVisita,
                str1Ordenha,
                str2Ordenha,
                str3Ordenha,
                strControlador,
                strUsuario,
                dtDataUsuario,
                strLote,
                FK_strIdLote
                 
                values
                
                (@diretorio,
                @strProprietario,
                @strRaca,
                @strCategoria,
                @strFazenda,
                @strMunicipio,
                @strUf,
                @dtDataControle,
                @dtDataProximaVisita,
                @str1Ordenha,
                @str2Ordenha,
                @str3Ordenha,
                @strControlador,
                @strUsuario,
                @dtDataUsuario,
                @strLote,
                @FK_strIdLote)", _Connection);
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = controleLeiteiro.Diretorio;
            ((LightBaseParameter)command.Parameters["strProprietario"]).Value = controleLeiteiro.Proprietario;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = controleLeiteiro.Raca;
            ((LightBaseParameter)command.Parameters["strCategoria"]).Value = controleLeiteiro.Categoria;
            ((LightBaseParameter)command.Parameters["strFazenda"]).Value = controleLeiteiro.Fazenda;
            ((LightBaseParameter)command.Parameters["strMunicipio"]).Value = controleLeiteiro.Municipio;
            ((LightBaseParameter)command.Parameters["strUf"]).Value = controleLeiteiro.Uf;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = controleLeiteiro.DataControle;
            ((LightBaseParameter)command.Parameters["dtDataProximaVisita"]).Value = controleLeiteiro.DataProximaVisita;
            ((LightBaseParameter)command.Parameters["str1Ordenha"]).Value = controleLeiteiro.POrdenha;
            ((LightBaseParameter)command.Parameters["str2Ordenha"]).Value = controleLeiteiro.SOrdenha;
            ((LightBaseParameter)command.Parameters["str3Ordenha"]).Value = controleLeiteiro.TOrdenha;
            ((LightBaseParameter)command.Parameters["strControlador"]).Value = controleLeiteiro.Controlador;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = controleLeiteiro.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = controleLeiteiro.DataUsuario;
            ((LightBaseParameter)command.Parameters["strLote"]).Value = controleLeiteiro.Lote;
            ((LightBaseParameter)command.Parameters["FK_strIdLote"]).Value = controleLeiteiro.IdLote;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

            const string idRetrievingCommand = "@@Id";
            LightBaseCommand lastIdCommand = new LightBaseCommand(idRetrievingCommand, _Connection);
            controleLeiteiro.Id = Convert.ToInt32(lastIdCommand.ExecuteScalar());

            var matrizes = GetMatrizes(Convert.ToInt32(controleLeiteiro.IdLote));

            foreach (Matriz matriz in matrizes)
            {
                if (matriz.EmControleLeiteiro)
                {
                    ProducaoLeite producaoLeite = new ProducaoLeite();
                    producaoLeite.IdMatriz = matriz.IdMatriz;
                    producaoLeite.IdCria = matriz.IdCria;
                    producaoLeite.DataEntradaControle = matriz.DataEntradaControle;

                    AdicionaProducaoLeite(Convert.ToInt32(controleLeiteiro.Id), producaoLeite);
                }
            }

        }

        public void SalvaControleLeiteiro(ControleLeiteiro controleLeiteiro)
        {

            List<string> campos = new List<string>()
                                      {
                                          "diretorio",
                                          "strProprietario",
                                          "strRaca",
                                          "strCategoria",
                                          "strFazenda",
                                          "strMunicipio",
                                          "strUf",
                                          "dtDataControle",
                                          "dtDataProximaVisita",
                                          "str1Ordenha",
                                          "str2Ordenha",
                                          "str3Ordenha",
                                          "strControlador",
                                          "strUsuario",
                                          "dtDataUsuario",
                                          "strLote",
                                          "FK_strIdLote"
                                      };

            var command = new LightBaseCommand(BuildControleLeiteiroString(campos));

            ((LightBaseParameter)command.Parameters["diretorio"]).Value = controleLeiteiro.Diretorio;
            ((LightBaseParameter)command.Parameters["strProprietario"]).Value = controleLeiteiro.Proprietario;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = controleLeiteiro.Raca;
            ((LightBaseParameter)command.Parameters["strCategoria"]).Value = controleLeiteiro.Categoria;
            ((LightBaseParameter)command.Parameters["strFazenda"]).Value = controleLeiteiro.Fazenda;
            ((LightBaseParameter)command.Parameters["strMunicipio"]).Value = controleLeiteiro.Municipio;
            ((LightBaseParameter)command.Parameters["strUf"]).Value = controleLeiteiro.Uf;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = controleLeiteiro.DataControle;
            ((LightBaseParameter)command.Parameters["dtDataProximaVisita"]).Value = controleLeiteiro.DataProximaVisita;
            ((LightBaseParameter)command.Parameters["str1Ordenha"]).Value = controleLeiteiro.POrdenha;
            ((LightBaseParameter)command.Parameters["str2Ordenha"]).Value = controleLeiteiro.SOrdenha;
            ((LightBaseParameter)command.Parameters["str3Ordenha"]).Value = controleLeiteiro.TOrdenha;
            ((LightBaseParameter)command.Parameters["strControlador"]).Value = controleLeiteiro.Controlador;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = controleLeiteiro.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = controleLeiteiro.DataUsuario;
            ((LightBaseParameter)command.Parameters["strLote"]).Value = controleLeiteiro.Lote;
            ((LightBaseParameter)command.Parameters["FK_strIdLote"]).Value = controleLeiteiro.IdLote;
            ((LightBaseParameter)command.Parameters["id"]).Value = controleLeiteiro.Id;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public List<ControleLeiteiro> ConsultaControleLeiteiro(ParametrosDeBuscaEmControleLeiteiro parametrosBuscaEmControleLeiteiro)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmControleLeiteiro.TodosOsCampos);
            filter = AddParametro(filter, "id", parametrosBuscaEmControleLeiteiro.Id.ToString());
            filter = AddParametro(filter, "strRaca", parametrosBuscaEmControleLeiteiro.Raca);
            filter = AddParametro(filter, "strCategoria", parametrosBuscaEmControleLeiteiro.Categoria);
            filter = AddParametro(filter, "strFazenda", parametrosBuscaEmControleLeiteiro.Fazenda);

            if (parametrosBuscaEmControleLeiteiro.DataControle != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmControleLeiteiro.DataControle.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmControleLeiteiro.DataControleInicial != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmControleLeiteiro.DataControleInicial.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmControleLeiteiro.DataControleFinal != null)
                filter = AddParametroData(filter, "dtDataControle", parametrosBuscaEmControleLeiteiro.DataControleFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "FK_strIdLote", parametrosBuscaEmControleLeiteiro.IdLote);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var controles = new List<ControleLeiteiro>();

            while (qReader.Read())
            {

                var retVal = new ControleLeiteiro();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strCategoria"] != DBNull.Value) retVal.Categoria = qReader["strCategoria"].ToString();
                if (qReader["strFazenda"] != DBNull.Value) retVal.Fazenda = qReader["strFazenda"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["dtDataProximaVisita"] != DBNull.Value) retVal.DataProximaVisita = (DateTime)qReader["dtDataProximaVisita"];
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();
                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["strControlador"].ToString();

                if (qReader["FK_strIdLote"] != DBNull.Value)
                {
                    retVal.IdLote = qReader["FK_strIdLote"].ToString();
                    retVal.Lote = "";
                }

                controles.Add(retVal);
            }
            qReader.Close();

            return controles.ToList();
        }

        public ControleLeiteiro GetControleLeiteiroById(string id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_CONTROLE_LEITEIRO where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new ControleLeiteiro();

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strCategoria"] != DBNull.Value) retVal.Categoria = qReader["strCategoria"].ToString();
                if (qReader["strFazenda"] != DBNull.Value) retVal.Fazenda = qReader["strFazenda"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["dtDataProximaVisita"] != DBNull.Value) retVal.DataProximaVisita = (DateTime)qReader["dtDataProximaVisita"];
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();
                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["strControlador"].ToString();

                if (qReader["FK_strIdLote"] != DBNull.Value)
                {
                    retVal.IdLote = qReader["FK_strIdLote"].ToString();
                    retVal.Lote = "";
                }

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                int i = 0;
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    ProducaoLeite producaoLeite = DataRowToProducaoLeiteCompleto(rowProducoesLeite);
                    producaoLeite.Id = i;
                    retVal.ProducoesLeite.Add(producaoLeite);
                    i++;
                }

            }


            qReader.Close();
            return retVal;
        }

        public List<ControleLeiteiro> GetControlesById(string idLote)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_CONTROLE_LEITEIRO where FK_strIdLote = @FK_strIdLote");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_strIdLote"]).Value = idLote;
            var qReader = command.ExecuteReader();
            var controles = new List<ControleLeiteiro>();

            while (qReader.Read())
            {
                var retVal = new ControleLeiteiro();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strCategoria"] != DBNull.Value) retVal.Categoria = qReader["strCategoria"].ToString();
                if (qReader["strFazenda"] != DBNull.Value) retVal.Fazenda = qReader["strFazenda"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["dtDataProximaVisita"] != DBNull.Value) retVal.DataProximaVisita = (DateTime)qReader["dtDataProximaVisita"];
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();
                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["strControlador"].ToString();

                if (qReader["FK_strIdLote"] != DBNull.Value)
                {
                    retVal.IdLote = qReader["FK_strIdLote"].ToString();
                    retVal.Lote = "";
                }

                controles.Add(retVal);

            }


            qReader.Close();
            return controles;
        }

        public void RemoveControleLeiteiro(string id)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_CONTROLE_LEITEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }

        public void RemoveControlesLeiteirosLote(string idLote)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_CONTROLE_LEITEIRO where FK_strIdLote = @FK_strIdLote");
            ((LightBaseParameter)command.Parameters["FK_strIdLote"]).Value = idLote;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }

        public void AdicionaProducaoLeite(int loteControleLeiteiroId, ProducaoLeite producaoLeite)
        {
            DateTime dataControleLeiteiro = GetDataControleLeiteiro(loteControleLeiteiroId.ToString());
            var matriz = GetAnimalById(producaoLeite.IdMatriz);

            var command = new LightBaseCommand(@"insert into FCARNAUBA_LOTE_CONTROLE_LEITEIRO Producao_de_Leite values ({{@FK_PL_strIdMatriz,
                                                                                       @PL_intDiasLactacao,
                                                                                       @PL_decEsgota,
                                                                                       @PL_dec1Ordenha,
                                                                                       @PL_dec2Ordenha,
                                                                                       @PL_dec3Ordenha,
                                                                                       @PL_decTotal,
                                                                                       @PL_vfBezerrosPe,
                                                                                       @PL_intTetosFuncionais,
                                                                                       @PL_docObs,
                                                                                       @PL_strRegimeAlimentar,
                                                                                       @PL_dtDataEntradaControle,
                                                                                       @PL_dtDataSaidaControle,
                                                                                       @FK_PL_strIdCria,
                                                                                       @PL_vfReceptora,
                                                                                       @PL_decGord1Ordenha,
                                                                                       @PL_decGord2Ordenha,
                                                                                       @PL_decGord3Ordenha,
                                                                                       @PL_decProt1Ordenha,
                                                                                       @PL_decProt2Ordenha,
                                                                                       @PL_decProt3Ordenha,
                                                                                       @PL_vfSairControle,
                                                                                       @PL_strMotivo
                                                                                }}) parent id = @id");

            ((LightBaseParameter)command.Parameters["id"]).Value = loteControleLeiteiroId;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = producaoLeite.IdMatriz;
            ((LightBaseParameter)command.Parameters["PL_intDiasLactacao"]).Value = producaoLeite.DiasLactacao;
            ((LightBaseParameter)command.Parameters["PL_decEsgota"]).Value = producaoLeite.Esgota;
            ((LightBaseParameter)command.Parameters["PL_dec1Ordenha"]).Value = producaoLeite.POrdenha;
            ((LightBaseParameter)command.Parameters["PL_dec2Ordenha"]).Value = producaoLeite.SOrdenha;
            ((LightBaseParameter)command.Parameters["PL_dec3Ordenha"]).Value = producaoLeite.TOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decTotal"]).Value = producaoLeite.Total;
            ((LightBaseParameter)command.Parameters["PL_vfBezerrosPe"]).Value = producaoLeite.BezerrosPe;
            ((LightBaseParameter)command.Parameters["PL_intTetosFuncionais"]).Value = producaoLeite.TetosFuncionais;
            ((LightBaseParameter)command.Parameters["PL_docObs"]).Value = producaoLeite.Obs;
            ((LightBaseParameter)command.Parameters["PL_strRegimeAlimentar"]).Value = producaoLeite.RegimeAlimentar;
            ((LightBaseParameter)command.Parameters["PL_dtDataEntradaControle"]).Value = producaoLeite.DataEntradaControle;
            ((LightBaseParameter)command.Parameters["PL_dtDataSaidaControle"]).Value = producaoLeite.DataSaidaControle;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdCria"]).Value = producaoLeite.IdCria;
            ((LightBaseParameter)command.Parameters["PL_vfReceptora"]).Value = producaoLeite.Receptora;
            ((LightBaseParameter)command.Parameters["PL_decGord1Ordenha"]).Value = producaoLeite.GordPOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decGord2Ordenha"]).Value = producaoLeite.GordSOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decGord3Ordenha"]).Value = producaoLeite.GordTOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decProt1Ordenha"]).Value = producaoLeite.ProtPOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decProt2Ordenha"]).Value = producaoLeite.ProtSOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decProt3Ordenha"]).Value = producaoLeite.ProtTOrdenha;
            ((LightBaseParameter)command.Parameters["PL_vfSairControle"]).Value = producaoLeite.SairControle;
            ((LightBaseParameter)command.Parameters["PL_strMotivo"]).Value = producaoLeite.Motivo;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

            if (!producaoLeite.SairControle)
            {
                AtualizaAdicaoControleLeiteiro(producaoLeite, dataControleLeiteiro, matriz.Raca);
            }
        }

        public void SalvaProducaoLeite(int loteControleLeiteiroId, int producaoLeiteId, ProducaoLeite producaoLeite, ProducaoLeite prodLeiteAnterior)
        {
            DateTime dataControleLeiteiro = GetDataControleLeiteiro(loteControleLeiteiroId.ToString());
            var matriz = GetAnimalById(producaoLeite.IdMatriz);

            List<string> campos = new List<string>()
                                      { "FK_PL_strIdMatriz",
                                        "PL_intDiasLactacao",
                                        "PL_decEsgota",
                                        "PL_dec1Ordenha",
                                        "PL_dec2Ordenha",
                                        "PL_dec3Ordenha",
                                        "PL_decTotal",
                                        "PL_vfBezerrosPe",
                                        "PL_intTetosFuncionais",
                                        "PL_docObs",
                                        "PL_strRegimeAlimentar",
                                        "PL_dtDataEntradaControle",
                                        "PL_dtDataSaidaControle",
                                        "FK_PL_strIdCria",
                                        "PL_vfReceptora",
                                        "PL_decGord1Ordenha",
                                        "PL_decGord2Ordenha",
                                        "PL_decGord3Ordenha",
                                        "PL_decProt1Ordenha",
                                        "PL_decProt2Ordenha",
                                        "PL_decProt3Ordenha",
                                        "PL_vfSairControle",
                                        "PL_strMotivo"
                                      };

            var command = new LightBaseCommand(BuildProducaoLeiteString(campos, producaoLeiteId));
            ((LightBaseParameter)command.Parameters["id"]).Value = loteControleLeiteiroId;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = producaoLeite.IdMatriz;
            ((LightBaseParameter)command.Parameters["PL_intDiasLactacao"]).Value = producaoLeite.DiasLactacao;
            ((LightBaseParameter)command.Parameters["PL_decEsgota"]).Value = producaoLeite.Esgota;
            ((LightBaseParameter)command.Parameters["PL_dec1Ordenha"]).Value = producaoLeite.POrdenha;
            ((LightBaseParameter)command.Parameters["PL_dec2Ordenha"]).Value = producaoLeite.SOrdenha;
            ((LightBaseParameter)command.Parameters["PL_dec3Ordenha"]).Value = producaoLeite.TOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decTotal"]).Value = producaoLeite.Total;
            ((LightBaseParameter)command.Parameters["PL_vfBezerrosPe"]).Value = producaoLeite.BezerrosPe;
            ((LightBaseParameter)command.Parameters["PL_intTetosFuncionais"]).Value = producaoLeite.TetosFuncionais;
            ((LightBaseParameter)command.Parameters["PL_docObs"]).Value = producaoLeite.Obs;
            ((LightBaseParameter)command.Parameters["PL_strRegimeAlimentar"]).Value = producaoLeite.RegimeAlimentar;
            ((LightBaseParameter)command.Parameters["PL_dtDataEntradaControle"]).Value = producaoLeite.DataEntradaControle;
            ((LightBaseParameter)command.Parameters["PL_dtDataSaidaControle"]).Value = producaoLeite.DataSaidaControle;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdCria"]).Value = producaoLeite.IdCria;
            ((LightBaseParameter)command.Parameters["PL_vfReceptora"]).Value = producaoLeite.Receptora;
            ((LightBaseParameter)command.Parameters["PL_decGord1Ordenha"]).Value = producaoLeite.GordPOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decGord2Ordenha"]).Value = producaoLeite.GordSOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decGord3Ordenha"]).Value = producaoLeite.GordTOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decProt1Ordenha"]).Value = producaoLeite.ProtPOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decProt2Ordenha"]).Value = producaoLeite.ProtSOrdenha;
            ((LightBaseParameter)command.Parameters["PL_decProt3Ordenha"]).Value = producaoLeite.ProtTOrdenha;
            ((LightBaseParameter)command.Parameters["PL_vfSairControle"]).Value = producaoLeite.SairControle;
            ((LightBaseParameter)command.Parameters["PL_strMotivo"]).Value = producaoLeite.Motivo;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

            if (producaoLeite.IdMatriz == prodLeiteAnterior.IdMatriz)
            {
                if (producaoLeite.SairControle)
                {
                    if (producaoLeite.SairControle != prodLeiteAnterior.SairControle)
                    {
                        AtualizaRemocaoControleLeiteiro(producaoLeite.IdMatriz, dataControleLeiteiro, matriz.Raca);
                    }
                }
                else
                {
                    if (producaoLeite.SairControle != prodLeiteAnterior.SairControle)
                    {
                        AtualizaAdicaoControleLeiteiro(producaoLeite, dataControleLeiteiro, matriz.Raca);

                    }
                }
            }
            else
            {
                //Mudou a matriz

                AtualizaRemocaoControleLeiteiro(prodLeiteAnterior.IdMatriz, dataControleLeiteiro, matriz.Raca);

                if (!producaoLeite.SairControle)
                {
                    AtualizaAdicaoControleLeiteiro(producaoLeite, dataControleLeiteiro, matriz.Raca);
                }
            }

        }

        public void AlterarPesagemLeite(int loteId, int pesagemLeiteId, ProducaoLeite pesagem)
        {
            try
            {
                List<string> campos = new List<string>()
                                      { "PL_dec1Ordenha",
                                        "PL_dec2Ordenha",
                                        "PL_dec3Ordenha",
                                        "PL_decTotal",
                                        "PL_vfBezerrosPe",
                                        "PL_intTetosFuncionais",
                                      };

                var command = new LightBaseCommand(BuildProducaoLeiteString(campos, pesagemLeiteId));
                ((LightBaseParameter)command.Parameters["id"]).Value = loteId;
                ((LightBaseParameter)command.Parameters["PL_dec1Ordenha"]).Value = pesagem.POrdenha;
                ((LightBaseParameter)command.Parameters["PL_dec2Ordenha"]).Value = pesagem.SOrdenha;
                ((LightBaseParameter)command.Parameters["PL_dec3Ordenha"]).Value = pesagem.TOrdenha;
                ((LightBaseParameter)command.Parameters["PL_vfBezerrosPe"]).Value = pesagem.BezerrosPe;
                ((LightBaseParameter)command.Parameters["PL_intTetosFuncionais"]).Value = pesagem.TetosFuncionais;
                ((LightBaseParameter)command.Parameters["PL_decTotal"]).Value = pesagem.POrdenha + pesagem.SOrdenha + pesagem.TOrdenha;

                command.Connection = _Connection;
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro na edição", exc);
                throw except;
            }
        }

        public List<ProducaoLeite> GetPesagemLeite(int loteID)
        {
            var command = new LightBaseCommand(@"select Producao_de_Leite from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = loteID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<ProducaoLeite>();

            if (qReader.Read())
            {
                var res = (DataTable)qReader["Producao_de_Leite"];
                int p = 0;
                string id = qReader["id"].ToString();

                foreach (DataRow dRow in res.Rows)
                {
                    var pesagem = DataRowToProducaoLeite(dRow);
                    pesagem.Id = p;
                    pesagem.LoteId = id;
                    pesagem.PesagemLoteId = pesagem.LoteId + " " + pesagem.Id;
                    retVal.Add(pesagem);
                    p++;
                }


            }

            qReader.Close();
            return retVal;
        }

        public ProducaoLeite GetPesagemLeiteById(int loteID, int pesagemLeiteId)
        {
            var pesagemLeiteList = GetPesagemLeite(loteID);
            return pesagemLeiteList[pesagemLeiteId];
        }

        public ProducaoLeite[] ObtemPesagensLeite(CriterioPesquisaPesagensLeite criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
                ((LightBaseParameter)command.Parameters["id"]).Value = criterio.IdLote;
                command.Connection = _Connection;
                var qReader = command.ExecuteReader();

                var retVal = new List<ProducaoLeite>();

                if (qReader.Read())
                {
                    var res = (DataTable)qReader["Producao_de_Leite"];
                    int p = 0;
                    string id = qReader["id"].ToString();

                    foreach (DataRow dRow in res.Rows)
                    {
                        var pesagem = DataRowToProducaoLeite(dRow);
                        pesagem.Id = p;
                        pesagem.LoteId = id;
                        pesagem.PesagemLoteId = pesagem.LoteId + " " + pesagem.Id;
                        retVal.Add(pesagem);
                        p++;
                    }


                }

                qReader.Close();

                return retVal.ToArray();
            }

            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }


            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public void EncerrarPesagensLeite(long idLote)
        {
            var command = new LightBaseCommand(@"update FCARNAUBA_LOTE_CONTROLE_LEITEIRO set vfLiberarLotePesagem=@vfLiberarLotePesagem where id = @id");
            ((LightBaseParameter)command.Parameters["vfLiberarLotePesagem"]).Value = 0;
            ((LightBaseParameter)command.Parameters["id"]).Value = idLote;

            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public void RemoveProducaoLeite(int loteControleLeiteiroId, int producaoLeiteId)
        {
            var producaoLeite = GetProducaoLeiteById(loteControleLeiteiroId, producaoLeiteId);
            DateTime dataControleLeiteiro = GetDataControleLeiteiro(loteControleLeiteiroId.ToString());
            var matriz = GetAnimalById(producaoLeite.IdMatriz);

            var command = new LightBaseCommand(@"delete from FCARNAUBA_LOTE_CONTROLE_LEITEIRO.Producao_de_Leite[" + producaoLeiteId + "] where id = " + loteControleLeiteiroId);
            command.Connection = _Connection;
            command.ExecuteNonQuery();
            
            if (!producaoLeite.SairControle)
            {
                AtualizaRemocaoControleLeiteiro(producaoLeite.IdMatriz, dataControleLeiteiro, matriz.Raca);
            }
        }

        private string BuildProducaoLeiteString(List<string> campos, int producaoLeiteId)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + ":@" + campo);
            }
            return "update FCARNAUBA_LOTE_CONTROLE_LEITEIRO set Producao_de_Leite[" + producaoLeiteId + "] = {" + String.Join(",", outCampos) +
                   "} where id = @id";
        }

        private string BuildFinanceiroString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_FINANCEIRO set " + String.Join(",", outCampos) + " where id = @id";
        }

        private string BuildEmpresaString(List<string> campos)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + "=@" + campo);
            }
            return "update FCARNAUBA_EMPRESA set " + String.Join(",", outCampos) + " where id = @id";
        }

        private Historico DataRowToHistorico(DataRow dRow)
        {
            Historico retVal = new Historico();

            if (dRow["MA_strMovimento"] != DBNull.Value)
            {
                retVal.Movimento = (string)dRow["MA_strMovimento"];
            }

            if (dRow["MA_strNomeQAQ"] != DBNull.Value)
            {
                retVal.NomeQAQ = (string)dRow["MA_strNomeQAQ"];
            }

            if (dRow["MA_dtDataManejo"] != DBNull.Value)
            {
                retVal.DataManejo = (DateTime)dRow["MA_dtDataManejo"];
            }

            if (dRow["MA_docObservacao"] != DBNull.Value)
            {
                retVal.Observacao = (string)dRow["MA_docObservacao"];
            }

            return retVal;
        }

        private Matriz DataRowToMatriz(DataRow dRow)
        {
            Matriz retVal = new Matriz();

            if (dRow["MAT_FK_strIdMatriz"] != DBNull.Value)
            {
                retVal.IdMatriz = (string)dRow["MAT_FK_strIdMatriz"];
                retVal.NomeMatriz = GetNomeAnimal(retVal.IdMatriz);
            }

            if (dRow["MAT_vfCoberturaEfetiva"] != DBNull.Value)
            {
                retVal.CdcEfetiva = (bool)dRow["MAT_vfCoberturaEfetiva"];
                if (retVal.CdcEfetiva)
                {
                    retVal.CdcEfetivaStr = "SIM";
                }
                else
                {
                    retVal.CdcEfetivaStr = "NÃO";
                }
            }
            else
            {
                retVal.CdcEfetivaStr = "NÃO";
                retVal.CdcEfetiva = false;
            }

            return retVal;
        }

        public Matriz GetMatrizById(int loteId, int matInd)
        {
            var matrizList = GetMatrizes(loteId);
            return matrizList[matInd];
        }

        private ProducaoLeite DataRowToProducaoLeiteCompleto(DataRow dRow)
        {
            ProducaoLeite retVal = new ProducaoLeite();

            if (dRow["FK_PL_strIdMatriz"] != DBNull.Value)
            {
                retVal.IdMatriz = (string)dRow["FK_PL_strIdMatriz"];
                retVal.NomeMatriz = GetAnimalById(retVal.IdMatriz).Nome;
                retVal.RgdMatriz = GetAnimalById(retVal.IdMatriz).Rgd;
            }

            if (dRow["PL_intDiasLactacao"] != DBNull.Value) retVal.DiasLactacao = (int)dRow["PL_intDiasLactacao"];

            if (dRow["PL_decEsgota"] != DBNull.Value) retVal.Esgota = (double)dRow["PL_decEsgota"];

            if (dRow["PL_dec1Ordenha"] != DBNull.Value) retVal.POrdenha = (double)dRow["PL_dec1Ordenha"];

            if (dRow["PL_dec2Ordenha"] != DBNull.Value) retVal.SOrdenha = (double)dRow["PL_dec2Ordenha"];

            if (dRow["PL_dec3Ordenha"] != DBNull.Value) retVal.TOrdenha = (double)dRow["PL_dec3Ordenha"];

            if (dRow["PL_decTotal"] != DBNull.Value) retVal.Total = (double)dRow["PL_decTotal"];

            if (dRow["PL_vfBezerrosPe"] != DBNull.Value)
            {
                retVal.BezerrosPe = (bool)dRow["PL_vfBezerrosPe"];
            }

            if (dRow["PL_intTetosFuncionais"] != DBNull.Value) retVal.TetosFuncionais = (int)dRow["PL_intTetosFuncionais"];

            if (dRow["PL_docObs"] != DBNull.Value)
            {
                retVal.Obs = (string)dRow["PL_docObs"];
            }

            if (dRow["PL_strRegimeAlimentar"] != DBNull.Value)
            {
                retVal.RegimeAlimentar = (string)dRow["PL_strRegimeAlimentar"];
            }

            if (dRow["PL_dtDataEntradaControle"] != DBNull.Value)
            {
                retVal.DataEntradaControle = (DateTime)dRow["PL_dtDataEntradaControle"];
            }

            if (dRow["PL_dtDataSaidaControle"] != DBNull.Value)
            {
                retVal.DataSaidaControle = (DateTime)dRow["PL_dtDataSaidaControle"];
            }

            if (dRow["FK_PL_strIdCria"] != DBNull.Value)
            {
                retVal.IdCria = (string)dRow["FK_PL_strIdCria"];
                retVal.NomeCria = GetAnimalById(retVal.IdCria).NomeCompleto;
            }

            if (dRow["PL_vfReceptora"] != DBNull.Value)
            {
                retVal.Receptora = (bool)dRow["PL_vfReceptora"];
            }

            if (dRow["PL_decGord1Ordenha"] != DBNull.Value) retVal.GordPOrdenha = (double)dRow["PL_decGord1Ordenha"];

            if (dRow["PL_decGord2Ordenha"] != DBNull.Value) retVal.GordSOrdenha = (double)dRow["PL_decGord2Ordenha"];

            if (dRow["PL_decGord3Ordenha"] != DBNull.Value) retVal.GordTOrdenha = (double)dRow["PL_decGord3Ordenha"];

            if (dRow["PL_decProt1Ordenha"] != DBNull.Value) retVal.ProtPOrdenha = (double)dRow["PL_decProt1Ordenha"];

            if (dRow["PL_decProt2Ordenha"] != DBNull.Value) retVal.ProtSOrdenha = (double)dRow["PL_decProt2Ordenha"];

            if (dRow["PL_decProt3Ordenha"] != DBNull.Value) retVal.ProtTOrdenha = (double)dRow["PL_decProt3Ordenha"];

            if (dRow["PL_vfSairControle"] != DBNull.Value)
            {
                retVal.SairControle = (bool)dRow["PL_vfSairControle"];
            }

            if (dRow["PL_vfSairControle"] != DBNull.Value)
            {
                retVal.SairControle = (bool)dRow["PL_vfSairControle"];
                if (retVal.SairControle)
                {
                    retVal.SairControleSr = "SIM";
                }
                else
                {
                    retVal.SairControleSr = "NÃO";
                }
            }
            else
            {
                retVal.SairControleSr = "NÃO";
            }

            if (dRow["PL_strMotivo"] != DBNull.Value)
            {
                retVal.Motivo = (string)dRow["PL_strMotivo"];
            }

            return retVal;
        }


        private Mensuracao DataRowToMensuracao(DataRow dRow)
        {
            Mensuracao retVal = new Mensuracao();

            if (dRow["FK_CP_strIdAnimal"] != DBNull.Value)
            {
                retVal.IdAnimal = (string)dRow["FK_CP_strIdAnimal"];
                retVal.NomeAnimal = GetAnimalById(retVal.IdAnimal).NomeRg;
            }

            if (dRow["CP_dtDataEntradaControle"] != DBNull.Value)
            {
                retVal.DataEntradaControle = (DateTime)dRow["CP_dtDataEntradaControle"];
            }

            if (dRow["CP_dtDataSaidaControle"] != DBNull.Value)
            {
                retVal.DataSaidaControle = (DateTime)dRow["CP_dtDataSaidaControle"];
            }

            if (dRow["CP_vfSairControle"] != DBNull.Value)
            {
                retVal.SairControle = (bool)dRow["CP_vfSairControle"];
            }

            if (dRow["CP_vfSairControle"] != DBNull.Value)
            {
                retVal.SairControle = (bool)dRow["CP_vfSairControle"];
                if (retVal.SairControle)
                {
                    retVal.SairControleSr = "SIM";
                }
                else
                {
                    retVal.SairControleSr = "NÃO";
                }
            }
            else
            {
                retVal.SairControleSr = "NÃO";
            }

            if (dRow["CP_strMotivo"] != DBNull.Value)
            {
                retVal.Motivo = (string)dRow["CP_strMotivo"];
            }

            if (dRow["CP_decPeso"] != DBNull.Value) retVal.Peso = (double)dRow["CP_decPeso"];

            if (dRow["CP_strRegimeAlimentar"] != DBNull.Value) retVal.RegimeAlimentar = (string)dRow["CP_strRegimeAlimentar"];

            if (dRow["CP_decCe"] != DBNull.Value) retVal.CEscrotal = (double)dRow["CP_decCe"];

            if (dRow["CP_decAa"] != DBNull.Value) retVal.AAnterior = (double)dRow["CP_decAa"];

            if (dRow["CP_decAp"] != DBNull.Value) retVal.APosterior = (double)dRow["CP_decAp"];

            if (dRow["CP_decLg"] != DBNull.Value) retVal.LGarupa = (double)dRow["CP_decLg"];

            if (dRow["CP_decCg"] != DBNull.Value) retVal.CGarupa = (double)dRow["CP_decCg"];

            if (dRow["CP_decCc"] != DBNull.Value) retVal.CCorporal = (double)dRow["CP_decCc"];

            if (dRow["CP_decPt"] != DBNull.Value) retVal.PToracico = (double)dRow["CP_decPt"];

            if (dRow["CP_strCaracterizacaoRacial"] != DBNull.Value)
            {
                retVal.CaracterizacaoRacial = (string)dRow["CP_strCaracterizacaoRacial"];
            }

            if (dRow["CP_strClassificacaoUbere"] != DBNull.Value)
            {
                retVal.ClassificacaoUbere = (string)dRow["CP_strClassificacaoUbere"];
            }

            if (dRow["CP_strCondicaoCriacao"] != DBNull.Value)
            {
                retVal.CondicaoCriacao = (string)dRow["CP_strCondicaoCriacao"];
            }

            if (dRow["CP_dtDataDesmame"] != DBNull.Value)
            {
                retVal.DataDesmame = (DateTime)dRow["CP_dtDataDesmame"];
            }

            if (dRow["CP_dtDataDiagnostico"] != DBNull.Value)
            {
                retVal.DataDiagnosticoPrenhez = (DateTime)dRow["CP_dtDataDiagnostico"];
            }

            if (dRow["CP_dtDataParto"] != DBNull.Value)
            {
                retVal.DataParto = (DateTime)dRow["CP_dtDataParto"];
            }

            if (dRow["CP_dtDataEntradaControleLeiteiro"] != DBNull.Value)
            {
                retVal.DataEntradaControleLeiteiro = (DateTime)dRow["CP_dtDataEntradaControleLeiteiro"];
            }

            if (dRow["CP_dtDataEncerramentoLactacao"] != DBNull.Value)
            {
                retVal.DataEncerramentoLactacao = (DateTime)dRow["CP_dtDataEncerramentoLactacao"];
            }

            if (dRow["CP_decPesoMaeDesmame"] != DBNull.Value) retVal.PesoMaeDesmame = (double)dRow["CP_decPesoMaeDesmame"];

            return retVal;
        }

        private RMensuracao DataRowToRMensuracao(DataRow dRow)
        {
            RMensuracao retVal = new RMensuracao();

            if (dRow["FK_CP_strIdAnimal"] != DBNull.Value)
            {
                retVal.IdAnimal = (string)dRow["FK_CP_strIdAnimal"];
                retVal.NomeAnimal = GetAnimalById(retVal.IdAnimal).Nome;
                retVal.RgnAnimal = GetAnimalById(retVal.IdAnimal).Rgn;
            }

            if (dRow["CP_dtDataEntradaControle"] != DBNull.Value)
            {
                retVal.DataEntradaControle = (DateTime)dRow["CP_dtDataEntradaControle"];
            }

            if (dRow["CP_dtDataSaidaControle"] != DBNull.Value)
            {
                retVal.DataSaidaControle = (DateTime)dRow["CP_dtDataSaidaControle"];
            }

            if (dRow["CP_vfSairControle"] != DBNull.Value)
            {
                retVal.SairControle = (bool)dRow["CP_vfSairControle"];
            }

            if (dRow["CP_vfSairControle"] != DBNull.Value)
            {
                retVal.SairControle = (bool)dRow["CP_vfSairControle"];
                if (retVal.SairControle)
                {
                    retVal.SairControleStr = "SIM";
                }
                else
                {
                    retVal.SairControleStr = "NÃO";
                }
            }
            else
            {
                retVal.SairControleStr = "NÃO";
            }

            if (dRow["CP_strMotivo"] != DBNull.Value)
            {
                retVal.Motivo = (string)dRow["CP_strMotivo"];
            }

            if (dRow["CP_decPeso"] != DBNull.Value) retVal.Peso = (double)dRow["CP_decPeso"];

            if (dRow["CP_strRegimeAlimentar"] != DBNull.Value) retVal.RA = (string)dRow["CP_strRegimeAlimentar"];

            if (dRow["CP_decCc"] != DBNull.Value) retVal.CC = (double)dRow["CP_decCc"];

            if (dRow["CP_decCe"] != DBNull.Value) retVal.CE = (double)dRow["CP_decCe"];

            if (dRow["CP_decAa"] != DBNull.Value) retVal.AA = (double)dRow["CP_decAa"];

            if (dRow["CP_decAp"] != DBNull.Value) retVal.AP = (double)dRow["CP_decAp"];

            if (dRow["CP_decLg"] != DBNull.Value) retVal.LG = (double)dRow["CP_decLg"];

            if (dRow["CP_decCg"] != DBNull.Value) retVal.CG = (double)dRow["CP_decCg"];

            if (dRow["CP_decPt"] != DBNull.Value) retVal.PT = (double)dRow["CP_decPt"];

            if (dRow["CP_decPesoMaeDesmame"] != DBNull.Value) retVal.PesoMaeDesmame = (double)dRow["CP_decPesoMaeDesmame"];

            return retVal;
        }

        private RAnimaisLactacaoAno DataRowToRAnimaisLactacaoAno(DataRow dRow)
        {
            RAnimaisLactacaoAno retVal = new RAnimaisLactacaoAno();

            if (dRow["PL_intDiasLactacao"] != DBNull.Value) retVal.DiasLactacao = (int)dRow["PL_intDiasLactacao"];

            if (dRow["PL_dec1Ordenha"] != DBNull.Value) retVal.POrdenha = (double)dRow["PL_dec1Ordenha"];

            if (dRow["PL_decTotal"] != DBNull.Value) retVal.Total = (double)dRow["PL_decTotal"];

            if (dRow["PL_vfSairControle"] != DBNull.Value)
            {
                retVal.SairControle = (bool)dRow["PL_vfSairControle"];
                if (retVal.SairControle)
                {
                    retVal.SairControleStr = "SIM";
                }
                else
                {
                    retVal.SairControleStr = "NÃO";
                }
            }
            else
            {
                retVal.SairControleStr = "NÃO";
            }



            return retVal;
        }

        public List<ResultadoBuscaAnimal> ConsultaAnimal(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametro(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);
            filter = AddParametro(filter, "strTipoBetaCaseina", parametrosBuscaEmAnimais.BetaCaseina);
            filter = AddParametro(filter, "strTipoKappaCaseina", parametrosBuscaEmAnimais.KappaCaseina);
            filter = AddParametro(filter, "MA_strMovimento", parametrosBuscaEmAnimais.Movimento);
            filter = AddParametro(filter, "MA_docObservacao", parametrosBuscaEmAnimais.Observacao);

            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animais = new List<ResultadoBuscaAnimal>();

            while (qReader.Read())
            {

                var retVal = new ResultadoBuscaAnimal();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeFazenda"] != DBNull.Value) retVal.NomeFazenda = qReader["strNomeFazenda"].ToString();
                if (qReader["strNome"] != DBNull.Value) retVal.Nome = qReader["strNome"].ToString();
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["strSexo"] != DBNull.Value) retVal.Sexo = qReader["strSexo"].ToString();
                if (qReader["strRgn"] != DBNull.Value) retVal.Rgn = qReader["strRgn"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                if (qReader["strRgd"] != DBNull.Value) retVal.Rgd = qReader["strRgd"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["decPn"] != DBNull.Value) retVal.Pn = (double)qReader["decPn"];

                if (qReader["strLaudoDna"] != DBNull.Value)
                {
                    string fileNameFinal = qReader["strLaudoDna"].ToString();
                    string fullPathFinal = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal;
                    retVal.LaudoDna = new Arquivo(fileNameFinal, null);
                }

                if (qReader["docObservacoes"] != DBNull.Value) retVal.Observacoes = qReader["docObservacoes"].ToString();

                if (qReader["strFoto"] != DBNull.Value)
                {
                    string fileNameFinal2 = qReader["strFoto"].ToString();
                    string fullPathFinal2 = FCarnaubaSettings.RepositorioAnimalPrincipal + "/" + fileNameFinal2;
                    retVal.Foto = new Arquivo(fileNameFinal2, null);
                }

                if (qReader["dtDataUltimoParto"] != DBNull.Value) retVal.DataUltimoParto = (DateTime)qReader["dtDataUltimoParto"];
                if (qReader["strRgdSerie"] != DBNull.Value) retVal.RgdSerie = qReader["strRgdSerie"].ToString();
                if (qReader["intRgdNumero"] != DBNull.Value) retVal.RgdNumero = Convert.ToInt64(qReader["intRgdNumero"]);
                if (qReader["strRgnSerie"] != DBNull.Value) retVal.RgnSerie = qReader["strRgnSerie"].ToString();
                if (qReader["intRgnNumero"] != DBNull.Value) retVal.RgnNumero = Convert.ToInt64(qReader["intRgnNumero"]);
                if (qReader["strId"] != DBNull.Value) retVal.StrId = qReader["strId"].ToString();
                if (qReader["strPaiId"] != DBNull.Value)
                {
                    retVal.StrPaiId = qReader["strPaiId"].ToString();
                    Animal pai = GetPaiMae(retVal.StrPaiId);
                    retVal.NomeCompletoPai = pai.NomeCompleto;
                    retVal.NomePai = pai.Nome;
                    retVal.RgdPai = pai.Rgd;
                }
                if (qReader["strMaeId"] != DBNull.Value)
                {
                    retVal.StrMaeId = qReader["strMaeId"].ToString();
                    Animal mae = GetPaiMae(retVal.StrMaeId);
                    retVal.NomeCompletoMae = mae.NomeCompleto;
                    retVal.NomeMae = mae.Nome;
                    retVal.RgdMae = mae.Rgd;
                }
                if (qReader["dtDataCdc"] != DBNull.Value) retVal.DataCdc = (DateTime)qReader["dtDataCdc"];
                if (qReader["strCria"] != DBNull.Value) retVal.NCria = qReader["strCria"].ToString();
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];

                retVal.NumeroFilhos = 0;

                animais.Add(retVal);
            }
            qReader.Close();

            return animais.ToList();
        }

        public Animal[] ConsultaDdlAnimal(CriterioPesquisaAnimal criterio)
        {
            try
            {
                var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + criterio.Filter + " order by strNomeCompleto");
                command.Connection = _Connection;
                LightBaseDataReader qReader = command.ExecuteReader();

                List<Animal> animais = new List<Animal>();

                while (qReader.Read())
                {
                    Animal animal = new Animal();
                    animal.Id = Convert.ToInt32(qReader["id"]);
                    animal.Rgd = Convert.ToString(qReader["strRgd"]);
                    animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();

                    animais.Add(animal);
                }
                qReader.Close();
                return animais.ToArray();
            }
            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }
            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }


        public List<RRankingPeso> ConsultaRankingPeso(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter + " order by decPn desc");

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsPeso = new List<RRankingPeso>();

            while (qReader.Read())
            {

                var retVal = new RRankingPeso();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["strSexo"] != DBNull.Value) retVal.Sexo = qReader["strSexo"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["decPn"] != DBNull.Value) retVal.Pn = (double)qReader["decPn"];


                rankingsPeso.Add(retVal);


            }
            qReader.Close();

            return rankingsPeso.ToList();
        }

        public List<RRankingFilhos> ConsultaRankingFilhos(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsFilhos = new List<RRankingFilhos>();

            while (qReader.Read())
            {

                var retVal = new RRankingFilhos();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["strSexo"] != DBNull.Value) retVal.Sexo = qReader["strSexo"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strPaiId"] != DBNull.Value) retVal.StrPaiId = qReader["strPaiId"].ToString();
                if (qReader["strMaeId"] != DBNull.Value) retVal.StrMaeId = qReader["strMaeId"].ToString();

                if (retVal.Sexo == "Macho")
                {
                    if (!String.IsNullOrEmpty(retVal.StrPaiId))
                    {
                        retVal.NumeroFilhos = QuantidadeDeFilhosPai(retVal.Id.ToString());
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(retVal.StrMaeId))
                    {
                        retVal.NumeroFilhos = QuantidadeDeFilhosMae(retVal.Id.ToString());
                    }
                }


                rankingsFilhos.Add(retVal);


            }
            qReader.Close();

            var orderRankingsFilhos = rankingsFilhos.OrderByDescending(s => s.NumeroFilhos);

            return orderRankingsFilhos.ToList();
        }

        public List<RAnimaisAno> ConsultaAnimaisAno(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter + " order by dtDataNascimento");

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisAno = new List<RAnimaisAno>();

            while (qReader.Read())
            {

                var retVal = new RAnimaisAno();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strNome"] != DBNull.Value) retVal.Nome = qReader["strNome"].ToString();
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value)
                {
                    retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                    retVal.Ano = retVal.DataNascimento.Year;
                }
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();



                animaisAno.Add(retVal);


            }
            qReader.Close();

            return animaisAno.ToList();
        }


        public List<RAnimaisLactacaoAno> ConsultaAnimaisLactacaoAno(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "id", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisLactacaoAno = new List<RAnimaisLactacaoAno>();

            while (qReader.Read())
            {

                var retVal = new RAnimaisLactacaoAno();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["dtDataControle"] != DBNull.Value)
                {
                    retVal.DataControle = (DateTime)qReader["dtDataControle"];
                    retVal.Ano = retVal.DataControle.Year;
                }
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    RAnimaisLactacaoAno animalLactacaoAno = DataRowToRAnimaisLactacaoAno(rowProducoesLeite);

                    if (!String.IsNullOrEmpty(retVal.DataControle.ToString()))
                    {

                        animalLactacaoAno.DataControle = retVal.DataControle;
                        animalLactacaoAno.Ano = retVal.Ano;
                    }

                    if (animalLactacaoAno.SairControleStr == "SIM")
                    {
                        animaisLactacaoAno.Add(animalLactacaoAno);
                    }
                }


            }
            qReader.Close();

            var orderanimaisLactacaoAno = animaisLactacaoAno.OrderBy(s => s.DataControle);

            return orderanimaisLactacaoAno.ToList();
        }


        public void AdicionaHistorico(string strId, Historico historico)
        {
            var command =
                new LightBaseCommand(@"insert into FCARNAUBA_ANIMAIS Historico values ({{@MA_strMovimento,
                                                                                       @MA_strNomeQAQ,
                                                                                       @MA_dtDataManejo,
                                                                                       @MA_docObservacao}}) parent strId = @strId");

            ((LightBaseParameter)command.Parameters["MA_strMovimento"]).Value = historico.Movimento;
            ((LightBaseParameter)command.Parameters["MA_strNomeQAQ"]).Value = historico.NomeQAQ;
            ((LightBaseParameter)command.Parameters["MA_dtDataManejo"]).Value = historico.DataManejo;
            ((LightBaseParameter)command.Parameters["MA_docObservacao"]).Value = historico.Observacao;

            ((LightBaseParameter)command.Parameters["strId"]).Value = strId;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public void SalvaHistorico(string strId, Historico historico, int row)
        {


            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("update FCARNAUBA_ANIMAIS set Historico[" + sRow +
                                     "].MA_strMovimento = @MA_strMovimento," + Environment.NewLine +
                                     "Historico[" + sRow + "].MA_strNomeQAQ = @MA_strNomeQAQ," +
                                     Environment.NewLine +
                                     "Historico[" + sRow + "].MA_dtDataManejo = @MA_dtDataManejo," +
                                     Environment.NewLine +
                                     "Historico[" + sRow +
                                     "].MA_docObservacao = @MA_docObservacao where strId = @strId");

            ((LightBaseParameter)command.Parameters["MA_strMovimento"]).Value = historico.Movimento;
            ((LightBaseParameter)command.Parameters["MA_strNomeQAQ"]).Value = historico.NomeQAQ;
            ((LightBaseParameter)command.Parameters["MA_dtDataManejo"]).Value = historico.DataManejo;
            ((LightBaseParameter)command.Parameters["MA_docObservacao"]).Value = historico.Observacao;
            ((LightBaseParameter)command.Parameters["strId"]).Value = strId;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public void RemoveHistorico(int id, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("delete from FCARNAUBA_ANIMAIS.Historico[" + sRow +
                                         "] where strId = @strId");
            ((LightBaseParameter)command.Parameters["strId"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }


        public List<Historico> GetHistorico(int animalID)
        {
            var command = new LightBaseCommand(@"select Historico from FCARNAUBA_ANIMAIS where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = animalID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Historico>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Historico"];
                int h = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    Historico historico = new Historico();
                    historico.Id = h;
                    if (dRow["MA_strMovimento"] != DBNull.Value) historico.Movimento = (string)dRow["MA_strMovimento"];
                    if (dRow["MA_dtDataManejo"] != DBNull.Value) historico.DataManejo = (DateTime)dRow["MA_dtDataManejo"];
                    if (dRow["MA_docObservacao"] != DBNull.Value) historico.Observacao = (string)dRow["MA_docObservacao"];

                    h++;
                    retVal.Add(historico);
                }

            }

            qReader.Close();
            return retVal;
        }

        public Historico GetHistoricoById(int animalID, int historicoId)
        {
            var historicoList = GetHistorico(animalID);
            return historicoList[historicoId];
        }


        public void AdicionaMensuracaoAnimal(string id, Mensuracao mensuracao)
        {
            var command =
                new LightBaseCommand(@"insert into FCARNAUBA_ANIMAIS Mensuracoes values ({{@CP_dtDataPesagem,
                                                                                       @CP_decPesoFinal,
                                                                                       @CP_strRegimeAlimentar,
                                                                                       @CP_decCe,
                                                                                       @CP_decAa,
                                                                                       @CP_decAp,
                                                                                       @CP_decLg,
                                                                                       @CP_decCg,
                                                                                       @CP_decCc,
                                                                                       @CP_decPt,
                                                                                       @CP_strCaracterizacaoRacial,
                                                                                       @CP_strClassificacaoUbere}}) parent id = @id");

            ((LightBaseParameter)command.Parameters["CP_dtDataPesagem"]).Value = mensuracao.DataPesagem;
            ((LightBaseParameter)command.Parameters["CP_decPesoFinal"]).Value = mensuracao.Peso;
            ((LightBaseParameter)command.Parameters["CP_strRegimeAlimentar"]).Value = mensuracao.RegimeAlimentar;
            ((LightBaseParameter)command.Parameters["CP_decCe"]).Value = mensuracao.CEscrotal;
            ((LightBaseParameter)command.Parameters["CP_decAa"]).Value = mensuracao.AAnterior;
            ((LightBaseParameter)command.Parameters["CP_decAp"]).Value = mensuracao.APosterior;
            ((LightBaseParameter)command.Parameters["CP_decLg"]).Value = mensuracao.LGarupa;
            ((LightBaseParameter)command.Parameters["CP_decCg"]).Value = mensuracao.CGarupa;
            ((LightBaseParameter)command.Parameters["CP_decCc"]).Value = mensuracao.CCorporal;
            ((LightBaseParameter)command.Parameters["CP_decPt"]).Value = mensuracao.PToracico;
            ((LightBaseParameter)command.Parameters["CP_strCaracterizacaoRacial"]).Value = mensuracao.CaracterizacaoRacial;
            ((LightBaseParameter)command.Parameters["CP_strClassificacaoUbere"]).Value = mensuracao.ClassificacaoUbere;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public void SalvaMensuracaoAnimal(int id, Mensuracao mensuracao, int row)
        {


            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("update FCARNAUBA_ANIMAIS set Mensuracoes[" + sRow +
                                     "].CP_dtDataPesagem = @CP_dtDataPesagem," + Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decPesoFinal = @CP_decPesoFinal," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_strRegimeAlimentar = @CP_strRegimeAlimentar," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decCe = @CP_decCe," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decAa = @CP_decAa," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decAp = @CP_decAp," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decLg = @CP_decLg," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decCg = @CP_decCg," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decCc = @CP_decCc," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decPt = @CP_decPt," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_strCaracterizacaoRacial = @CP_strCaracterizacaoRacial," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow +
                                     "].CP_strClassificacaoUbere = @CP_strClassificacaoUbere where id = @id");

            ((LightBaseParameter)command.Parameters["CP_dtDataPesagem"]).Value = mensuracao.DataPesagem;
            ((LightBaseParameter)command.Parameters["CP_decPesoFinal"]).Value = mensuracao.Peso;
            ((LightBaseParameter)command.Parameters["CP_strRegimeAlimentar"]).Value = mensuracao.RegimeAlimentar;
            ((LightBaseParameter)command.Parameters["CP_decCe"]).Value = mensuracao.CEscrotal;
            ((LightBaseParameter)command.Parameters["CP_decAa"]).Value = mensuracao.AAnterior;
            ((LightBaseParameter)command.Parameters["CP_decAp"]).Value = mensuracao.APosterior;
            ((LightBaseParameter)command.Parameters["CP_decLg"]).Value = mensuracao.LGarupa;
            ((LightBaseParameter)command.Parameters["CP_decCg"]).Value = mensuracao.CGarupa;
            ((LightBaseParameter)command.Parameters["CP_decCc"]).Value = mensuracao.CCorporal;
            ((LightBaseParameter)command.Parameters["CP_decPt"]).Value = mensuracao.PToracico;
            ((LightBaseParameter)command.Parameters["CP_strCaracterizacaoRacial"]).Value = mensuracao.CaracterizacaoRacial;
            ((LightBaseParameter)command.Parameters["CP_strClassificacaoUbere"]).Value = mensuracao.ClassificacaoUbere;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }


        public Mensuracao GetMensuracaoAnimalById(int animalID, int mensuracaoAnimalId)
        {
            var mensuracaoAnimalList = GetMensuracaoAnimal(animalID);
            return mensuracaoAnimalList[mensuracaoAnimalId];
        }

        public List<Mensuracao> GetMensuracaoAnimal(int animalID)
        {
            var command = new LightBaseCommand(@"select Mensuracoes from FCARNAUBA_ANIMAIS where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = animalID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Mensuracao>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Mensuracoes"];
                int m = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    var mensuracao = DataRowToMensuracaoAnimal(dRow);
                    mensuracao.Id = m;
                    retVal.Add(mensuracao);
                    m++;
                }


            }

            qReader.Close();
            return retVal;
        }

        private Mensuracao DataRowToMensuracaoAnimal(DataRow dRow)
        {
            Mensuracao retVal = new Mensuracao();

            if (dRow["CP_dtDataPesagem"] != DBNull.Value) retVal.DataPesagem = (DateTime)dRow["CP_dtDataPesagem"];

            if (dRow["CP_decPesoFinal"] != DBNull.Value) retVal.Peso = (double)dRow["CP_decPesoFinal"];

            if (dRow["CP_strRegimeAlimentar"] != DBNull.Value) retVal.RegimeAlimentar = (string)dRow["CP_strRegimeAlimentar"];

            if (dRow["CP_decCe"] != DBNull.Value) retVal.CEscrotal = (double)dRow["CP_decCe"];

            if (dRow["CP_decAa"] != DBNull.Value) retVal.AAnterior = (double)dRow["CP_decAa"];

            if (dRow["CP_decAp"] != DBNull.Value) retVal.APosterior = (double)dRow["CP_decAp"];

            if (dRow["CP_decLg"] != DBNull.Value) retVal.LGarupa = (double)dRow["CP_decLg"];

            if (dRow["CP_decCg"] != DBNull.Value) retVal.CGarupa = (double)dRow["CP_decCg"];

            if (dRow["CP_decCc"] != DBNull.Value) retVal.CCorporal = (double)dRow["CP_decCc"];

            if (dRow["CP_decPt"] != DBNull.Value) retVal.PToracico = (double)dRow["CP_decPt"];

            if (dRow["CP_strCaracterizacaoRacial"] != DBNull.Value)
            {
                retVal.CaracterizacaoRacial = (string)dRow["CP_strCaracterizacaoRacial"];
            }

            if (dRow["CP_strClassificacaoUbere"] != DBNull.Value)
            {
                retVal.ClassificacaoUbere = (string)dRow["CP_strClassificacaoUbere"];
            }

            return retVal;
        }

        public void RemoveMensuracaoAnimal(int id, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("delete from FCARNAUBA_ANIMAIS.Mensuracoes[" + sRow +
                                         "] where strId = @strId");
            ((LightBaseParameter)command.Parameters["strId"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public void AdicionaMensuracao(string id, Mensuracao mensuracao)
        {
            var command =
                new LightBaseCommand(@"insert into FCARNAUBA_LOTE_CONTROLE_PONDERAL Mensuracoes values ({{@FK_CP_strIdAnimal,
                                                                                       @CP_decPeso,
                                                                                       @CP_strRegimeAlimentar,
                                                                                       @CP_decCe,
                                                                                       @CP_decAa,
                                                                                       @CP_decAp,
                                                                                       @CP_decLg,
                                                                                       @CP_decCg,
                                                                                       @CP_decCc,
                                                                                       @CP_decPt,
                                                                                       @CP_strCaracterizacaoRacial,
                                                                                       @CP_strClassificacaoUbere,
                                                                                       @CP_dtDataEntradaControle,
                                                                                       @CP_dtDataSaidaControle,
                                                                                       @CP_vfSairControle,
                                                                                       @CP_strMotivo,
                                                                                       @CP_strCondicaoCriacao,
                                                                                       @CP_dtDataDesmame,
                                                                                       @CP_dtDataDiagnostico,
                                                                                       @CP_dtDataParto,
                                                                                       @CP_dtDataEntradaControleLeiteiro,
                                                                                       @CP_dtDataEncerramentoLactacao,
                                                                                       @CP_decPesoMaeDesmame}}) parent id = @id");

            ((LightBaseParameter)command.Parameters["FK_CP_strIdAnimal"]).Value = mensuracao.IdAnimal;
            ((LightBaseParameter)command.Parameters["CP_decPeso"]).Value = mensuracao.Peso;
            ((LightBaseParameter)command.Parameters["CP_strRegimeAlimentar"]).Value = mensuracao.RegimeAlimentar;
            ((LightBaseParameter)command.Parameters["CP_decCe"]).Value = mensuracao.CEscrotal;
            ((LightBaseParameter)command.Parameters["CP_decAa"]).Value = mensuracao.AAnterior;
            ((LightBaseParameter)command.Parameters["CP_decAp"]).Value = mensuracao.APosterior;
            ((LightBaseParameter)command.Parameters["CP_decLg"]).Value = mensuracao.LGarupa;
            ((LightBaseParameter)command.Parameters["CP_decCg"]).Value = mensuracao.CGarupa;
            ((LightBaseParameter)command.Parameters["CP_decCc"]).Value = mensuracao.CCorporal;
            ((LightBaseParameter)command.Parameters["CP_decPt"]).Value = mensuracao.PToracico;
            ((LightBaseParameter)command.Parameters["CP_dtDataEntradaControle"]).Value = mensuracao.DataEntradaControle;
            ((LightBaseParameter)command.Parameters["CP_dtDataSaidaControle"]).Value = mensuracao.DataSaidaControle;
            ((LightBaseParameter)command.Parameters["CP_vfSairControle"]).Value = mensuracao.SairControle;
            ((LightBaseParameter)command.Parameters["CP_strMotivo"]).Value = mensuracao.Motivo;
            ((LightBaseParameter)command.Parameters["CP_strCaracterizacaoRacial"]).Value = mensuracao.CaracterizacaoRacial;
            ((LightBaseParameter)command.Parameters["CP_strClassificacaoUbere"]).Value = mensuracao.ClassificacaoUbere;
            ((LightBaseParameter)command.Parameters["CP_strCondicaoCriacao"]).Value = mensuracao.CondicaoCriacao;
            ((LightBaseParameter)command.Parameters["CP_dtDataDesmame"]).Value = mensuracao.DataDesmame;
            ((LightBaseParameter)command.Parameters["CP_dtDataDiagnostico"]).Value = mensuracao.DataDiagnosticoPrenhez;
            ((LightBaseParameter)command.Parameters["CP_dtDataParto"]).Value = mensuracao.DataParto;
            ((LightBaseParameter)command.Parameters["CP_dtDataEntradaControleLeiteiro"]).Value = mensuracao.DataEntradaControleLeiteiro;
            ((LightBaseParameter)command.Parameters["CP_dtDataEncerramentoLactacao"]).Value = mensuracao.DataEncerramentoLactacao;
            ((LightBaseParameter)command.Parameters["CP_decPesoMaeDesmame"]).Value = mensuracao.PesoMaeDesmame;


            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }


        public void SalvaMensuracao(int id, Mensuracao mensuracao, int row)
        {


            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("update FCARNAUBA_LOTE_CONTROLE_PONDERAL set Mensuracoes[" + sRow +
                                     "].FK_CP_strIdAnimal = @FK_CP_strIdAnimal," + Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decPeso = @CP_decPeso," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_strRegimeAlimentar = @CP_strRegimeAlimentar," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decCe = @CP_decCe," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decAa = @CP_decAa," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decAp = @CP_decAp," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decLg = @CP_decLg," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decCg = @CP_decCg," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decCc = @CP_decCc," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_decPt = @CP_decPt," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_strCaracterizacaoRacial = @CP_strCaracterizacaoRacial," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_strClassificacaoUbere = @CP_strClassificacaoUbere," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_dtDataEntradaControle = @CP_dtDataEntradaControle," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_dtDataSaidaControle = @CP_dtDataSaidaControle," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_vfSairControle = @CP_vfSairControle," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_strMotivo = @CP_strMotivo," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_strCondicaoCriacao = @CP_strCondicaoCriacao," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_dtDataDesmame = @CP_dtDataDesmame," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_dtDataDiagnostico = @CP_dtDataDiagnostico," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_dtDataParto = @CP_dtDataParto," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_dtDataEntradaControleLeiteiro = @CP_dtDataEntradaControleLeiteiro," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow + "].CP_dtDataEncerramentoLactacao = @CP_dtDataEncerramentoLactacao," +
                                     Environment.NewLine +
                                     "Mensuracoes[" + sRow +
                                     "].CP_decPesoMaeDesmame = @CP_decPesoMaeDesmame where id = @id");

            ((LightBaseParameter)command.Parameters["FK_CP_strIdAnimal"]).Value = mensuracao.IdAnimal;
            ((LightBaseParameter)command.Parameters["CP_decPeso"]).Value = mensuracao.Peso;
            ((LightBaseParameter)command.Parameters["CP_strRegimeAlimentar"]).Value = mensuracao.RegimeAlimentar;
            ((LightBaseParameter)command.Parameters["CP_decCe"]).Value = mensuracao.CEscrotal;
            ((LightBaseParameter)command.Parameters["CP_decAa"]).Value = mensuracao.AAnterior;
            ((LightBaseParameter)command.Parameters["CP_decAp"]).Value = mensuracao.APosterior;
            ((LightBaseParameter)command.Parameters["CP_decLg"]).Value = mensuracao.LGarupa;
            ((LightBaseParameter)command.Parameters["CP_decCg"]).Value = mensuracao.CGarupa;
            ((LightBaseParameter)command.Parameters["CP_decCc"]).Value = mensuracao.CCorporal;
            ((LightBaseParameter)command.Parameters["CP_decPt"]).Value = mensuracao.PToracico;

            ((LightBaseParameter)command.Parameters["CP_strCaracterizacaoRacial"]).Value = mensuracao.CaracterizacaoRacial;
            ((LightBaseParameter)command.Parameters["CP_strClassificacaoUbere"]).Value = mensuracao.ClassificacaoUbere;
            ((LightBaseParameter)command.Parameters["CP_dtDataEntradaControle"]).Value = mensuracao.DataEntradaControle;
            ((LightBaseParameter)command.Parameters["CP_dtDataSaidaControle"]).Value = mensuracao.DataSaidaControle;
            ((LightBaseParameter)command.Parameters["CP_vfSairControle"]).Value = mensuracao.SairControle;
            ((LightBaseParameter)command.Parameters["CP_strMotivo"]).Value = mensuracao.Motivo;

            ((LightBaseParameter)command.Parameters["CP_strCondicaoCriacao"]).Value = mensuracao.CondicaoCriacao;
            ((LightBaseParameter)command.Parameters["CP_dtDataDesmame"]).Value = mensuracao.DataDesmame;
            ((LightBaseParameter)command.Parameters["CP_dtDataDiagnostico"]).Value = mensuracao.DataDiagnosticoPrenhez;
            ((LightBaseParameter)command.Parameters["CP_dtDataParto"]).Value = mensuracao.DataParto;
            ((LightBaseParameter)command.Parameters["CP_dtDataEntradaControleLeiteiro"]).Value = mensuracao.DataEntradaControleLeiteiro;
            ((LightBaseParameter)command.Parameters["CP_dtDataEncerramentoLactacao"]).Value = mensuracao.DataEncerramentoLactacao;
            ((LightBaseParameter)command.Parameters["CP_decPesoMaeDesmame"]).Value = mensuracao.PesoMaeDesmame;

            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }


        public void RemoveMensuracao(int id, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("delete from FCARNAUBA_LOTE_CONTROLE_PONDERAL.Mensuracoes[" + sRow +
                                         "] where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public List<Mensuracao> GetMensuracao(int loteControlePonderalID)
        {
            var command = new LightBaseCommand(@"select Mensuracoes from FCARNAUBA_LOTE_CONTROLE_PONDERAL where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = loteControlePonderalID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Mensuracao>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Mensuracoes"];
                int m = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    var mensuracao = DataRowToMensuracao(dRow);
                    mensuracao.Id = m;
                    retVal.Add(mensuracao);
                    m++;
                }


            }

            qReader.Close();
            return retVal;
        }



        public List<RMensuracao> GetMensuracoesPes(int loteControlePonderalID)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = loteControlePonderalID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControlePonderal();
            var mensuracoes = new List<RMensuracao>();

            while (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strIdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];

                var res = (DataTable)qReader["Mensuracoes"];
                int m = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    var mensuracao = DataRowToRMensuracao(dRow);
                    mensuracao.Id = m;

                    mensuracao.SLote = retVal.SLote;
                    mensuracao.Raca = retVal.Raca;
                    mensuracao.Fazenda = retVal.NomePropriedade;
                    mensuracao.DataControle = retVal.DataControle;

                    mensuracoes.Add(mensuracao);
                    m++;
                }


            }

            qReader.Close();
            return mensuracoes;
        }

        public double[] GetGmds(string animalId)
        {
            int i = 1;
            double pesoAnterior = 0;
            double pesoAtual = 0;
            DateTime dataControle = new DateTime(1, 1, 1);
            DateTime dataAnterior = new DateTime(1, 1, 1);
            DateTime dataAtual = new DateTime(1, 1, 1);
            string animalIdTemp = "";
            double gmd = 0;

            var gmds = new List<double>();

            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where FK_CP_strIdAnimal = @FK_CP_strIdAnimal order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_CP_strIdAnimal"]).Value = animalId;
            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {
                if (qReader["dtDataControle"] != DBNull.Value) dataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableMensuracoes = (DataTable)qReader["Mensuracoes"];

                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    if (rowMensuracao["FK_CP_strIdAnimal"] != DBNull.Value)
                    {
                        animalIdTemp = (string)rowMensuracao["FK_CP_strIdAnimal"];
                    }

                    if (animalIdTemp == animalId)
                    {
                        if (rowMensuracao["FK_CP_strIdAnimal"] != DBNull.Value)
                        {
                            pesoAtual = (double)rowMensuracao["CP_decPeso"];
                        }
                        else
                        {
                            pesoAtual = 0;
                        }

                        dataAtual = dataControle;

                        if (i == 1)
                        {
                            gmd = 0;
                        }
                        else
                        {
                            TimeSpan dataDif = dataAtual - dataAnterior;
                            int diasDif = dataDif.Days;

                            if (diasDif > 0)
                            {
                                gmd = (pesoAtual - pesoAnterior) / diasDif;
                            }
                            else
                            {
                                gmd = 0;
                            }
                        }

                        i++;

                        pesoAnterior = pesoAtual;
                        dataAnterior = dataAtual;

                        gmds.Add(gmd);

                    }
                }
            }

            return gmds.ToArray();
        }

        public double[] GetTodosGmds(DateTime dataInicio, DateTime dataFim, string idPropriedade)
        {
            var idsAnimais = new List<string>();
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where strIdPropriedade = @strIdPropriedade and dtDataControle >= @dtDataInicio and dtDataControle <= @dtDataFim order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = idPropriedade;
            ((LightBaseParameter)command.Parameters["dtDataInicio"]).Value = dataInicio.ToString("dd/MM/yyyy");
            ((LightBaseParameter)command.Parameters["dtDataFim"]).Value = dataFim.ToString("dd/MM/yyyy");
            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {
                DataTable tableMensuracoes = (DataTable)qReader["Mensuracoes"];

                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    if (rowMensuracao["FK_CP_strIdAnimal"] != DBNull.Value)
                    {
                        string idAnimal = (string)rowMensuracao["FK_CP_strIdAnimal"];

                        var match = idsAnimais.FirstOrDefault(stringToCheck => stringToCheck.Contains(idAnimal));

                        if (match == null)
                        {
                            idsAnimais.Add(idAnimal);
                        }
                    }
                }
            }

            qReader.Close();

            var gmds = new List<double>();

            foreach (string idsAnimal in idsAnimais)
            {
                var gmdsTemp = GetGmds(idsAnimal);
                var gmdMedia = Math.Round(gmdsTemp.Average(), 2);
                gmds.Add(gmdMedia);

            }

            return gmds.ToArray();
        }

        public List<RMensuracao> GetMensuracoes(DateTime dataControle, string animalId)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where FK_CP_strIdAnimal = @FK_CP_strIdAnimal order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_CP_strIdAnimal"]).Value = animalId;
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControlePonderal();
            var mensuracao = new RMensuracao();
            var mensuracoes = new List<RMensuracao>();
            bool sairControle = false;
            int i = 1;
            double pesoAnterior = 0;
            double pesoAtual = 0;
            DateTime dataAnterior = new DateTime(1, 1, 1);
            DateTime dataAtual = new DateTime(1, 1, 1);
            string animalIdTemp = "";

            while (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strIdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableMensuracoes = (DataTable)qReader["Mensuracoes"];

                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    if (rowMensuracao["FK_CP_strIdAnimal"] != DBNull.Value)
                    {
                        animalIdTemp = (string)rowMensuracao["FK_CP_strIdAnimal"];
                    }

                    if (animalIdTemp == animalId)
                    {
                        RMensuracao rMensuracao = DataRowToRMensuracao(rowMensuracao);

                        rMensuracao.RgnAnimal = GetAnimalById(animalId).Rgn;

                        rMensuracao.DataControle = retVal.DataControle;

                        pesoAtual = rMensuracao.Peso;
                        dataAtual = rMensuracao.DataControle;

                        rMensuracao.DataNascimentoAnimal = GetDataNascimentoCria(rMensuracao.IdAnimal);

                        rMensuracao.IdadeMeses = GetQtdMoth(rMensuracao.DataNascimentoAnimal, retVal.DataControle);

                        rMensuracao.Controle = i;
                        rMensuracao.Raca = retVal.Raca;
                        rMensuracao.Fazenda = retVal.NomePropriedade;
                        rMensuracao.DataControle = retVal.DataControle;

                        if (i == 1)
                        {
                            rMensuracao.GMD = 0;
                        }
                        else
                        {
                            TimeSpan dataDif = dataAtual - dataAnterior;
                            int diasDif = dataDif.Days;

                            if (diasDif > 0)
                            {
                                rMensuracao.GMD = (pesoAtual - pesoAnterior) / diasDif;
                            }
                            else
                            {
                                rMensuracao.GMD = 0;
                            }
                        }

                        i++;

                        pesoAnterior = pesoAtual;
                        dataAnterior = dataAtual;

                        mensuracoes.Add(rMensuracao);

                        if (rMensuracao.SairControle)
                        {

                            mensuracoes[0].DataSaidaControle = retVal.DataControle;

                            sairControle = true;
                            break;
                        }

                    }

                }

                if (sairControle)
                    sairControle = false;
            }

            return mensuracoes;
        }


        public double[] GetPesos(string animalId)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where FK_CP_strIdAnimal = @FK_CP_strIdAnimal order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_CP_strIdAnimal"]).Value = animalId;
            var qReader = command.ExecuteReader();

            var pesos = new List<double>();
            string animalIdTemp = "";
            double peso = 0;

            while (qReader.Read())
            {
                DataTable tableMensuracoes = (DataTable)qReader["Mensuracoes"];

                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    if (rowMensuracao["FK_CP_strIdAnimal"] != DBNull.Value)
                    {
                        animalIdTemp = (string)rowMensuracao["FK_CP_strIdAnimal"];
                    }

                    if (animalIdTemp == animalId)
                    {

                        if (rowMensuracao["CP_decPeso"] != DBNull.Value) peso = (double)rowMensuracao["CP_decPeso"];

                        pesos.Add(peso);

                        break;
                    }
                }
            }

            return pesos.ToArray();
        }

        public List<RPeso> GetPesos(ParametrosDeBuscaEmLotesPonderais parametros)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where strIdPropriedade = @strIdPropriedade and strRaca = @strRaca and dtDataControle >= @dtDataInicio and dtDataControle <= @dtDataFim order by dtDataControle");
            command.Connection = _Connection;

            if (parametros.IdPropriedade != null)
                ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = parametros.IdPropriedade;

            if (parametros.Raca != null)
                ((LightBaseParameter)command.Parameters["strRaca"]).Value = parametros.Raca;

            if (parametros.DataControleInicial != null)
                ((LightBaseParameter)command.Parameters["dtDataInicio"]).Value = parametros.DataControleInicial.Value.ToShortDateString();

            if (parametros.DataControleFinal != null)
                ((LightBaseParameter)command.Parameters["dtDataFim"]).Value = parametros.DataControleFinal.Value.ToShortDateString();

            var qReader = command.ExecuteReader();

            var pesos = new List<RPeso>();

            while (qReader.Read())
            {
                var retVal = new LotePonderal();

                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }

                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableMensuracoes = (DataTable)qReader["Mensuracoes"];

                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {

                    RMensuracao rMensuracao = DataRowToRMensuracao(rowMensuracao);
                    var animal = GetAnimalById(rMensuracao.IdAnimal);

                    RPeso peso = new RPeso();
                    peso.Nome = rMensuracao.NomeAnimal;
                    peso.NomePropriedade = retVal.NomePropriedade;
                    peso.DataControle = retVal.DataControle;
                    peso.Peso = rMensuracao.Peso;
                    peso.Raca = animal.Raca;
                    peso.Sexo = animal.Sexo;
                    peso.Periodo = "Período: " + parametros.DataControleInicial.Value.ToShortDateString() + " a " + parametros.DataControleFinal.Value.ToShortDateString();

                    pesos.Add(peso);


                }
            }

            return pesos;
        }

        public double[] GetProducoes(string matrizId)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where  FK_PL_strIdMatriz = @ FK_PL_strIdMatriz order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters[" FK_PL_strIdMatriz"]).Value = matrizId;
            var qReader = command.ExecuteReader();

            var producoes = new List<double>();
            string matrizIdTemp = "";
            double producao = 0;

            while (qReader.Read())
            {
                DataTable tableProducoes = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducao in tableProducoes.Rows)
                {
                    if (rowProducao["FK_PL_strIdMatriz"] != DBNull.Value)
                    {
                        matrizIdTemp = (string)rowProducao["FK_CP_strIdAnimal"];
                    }

                    if (matrizIdTemp == matrizId)
                    {

                        if (rowProducao["PL_decTotal"] != DBNull.Value) producao = (double)rowProducao["PL_decTotal"];

                        producoes.Add(producao);

                        break;
                    }
                }
            }

            return producoes.ToArray();
        }


        public Mensuracao GetMensuracaoById(int loteControlePonderalID, int mensuracaoId)
        {
            var mensuracaoList = GetMensuracao(loteControlePonderalID);
            return mensuracaoList[mensuracaoId];
        }




        public long UltimoLotePonderal(string animalId)
        {
            var command = new LightBaseCommand(@"select id, Mensuracoes from FCARNAUBA_LOTE_CONTROLE_PONDERAL where FK_CP_strIdAnimal = @FK_CP_strIdAnimal");
            ((LightBaseParameter)command.Parameters["FK_CP_strIdAnimal"]).Value = animalId;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            long retVal = 0;
            bool ultimoLote = false;

            while (qReader.Read())
            {
                var res = qReader["Mensuracoes"] as DataTable;
                if (res == null) break;
                foreach (DataRow dRow in res.Rows)
                {

                    if (dRow["CP_vfSairControle"] != DBNull.Value) ultimoLote = (bool)dRow["CP_vfSairControle"];

                    if (ultimoLote)
                    {
                        if (qReader["id"] != DBNull.Value) retVal = Convert.ToInt64(qReader["id"]);
                        break;
                    }

                }

                if (ultimoLote)
                    break;

            }
            qReader.Close();
            return retVal;
        }


        public long AdicionaEstruturaPropriedade(EstruturaPropriedade estruturaPropriedade)
        {

            var command = new LightBaseCommand(@"insert into FCARNAUBA_PROPRIEDADE
                diretorio,
                strNome,
                strLocalizacao,
                strRegistroOficial,
                decArea,
                decAreaUtilizada,
                decAreaPreservacao,
                docAtividades,
                strMunicipio,
                strUf,
                strUsuario,
                dtDataUsuario,
                dtData
                 
                values
                
                (@diretorio,
                @strNome,
                @strLocalizacao,
                @strRegistroOficial,
                @decArea,
                @decAreaUtilizada,
                @decAreaPreservacao,
                @docAtividades,
                @strMunicipio,
                @strUf,
                @strUsuario,
                @dtDataUsuario,
                @dtData)", _Connection);
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = estruturaPropriedade.Diretorio;
            ((LightBaseParameter)command.Parameters["strNome"]).Value = estruturaPropriedade.NomePropriedade;
            ((LightBaseParameter)command.Parameters["strLocalizacao"]).Value = estruturaPropriedade.Localizacao;
            ((LightBaseParameter)command.Parameters["strRegistroOficial"]).Value = estruturaPropriedade.RegistroOficial;
            ((LightBaseParameter)command.Parameters["decArea"]).Value = estruturaPropriedade.Area;
            ((LightBaseParameter)command.Parameters["decAreaUtilizada"]).Value = estruturaPropriedade.AreaUtilizada;
            ((LightBaseParameter)command.Parameters["decAreaPreservacao"]).Value = estruturaPropriedade.Reserva;
            ((LightBaseParameter)command.Parameters["docAtividades"]).Value = estruturaPropriedade.Atividades;
            ((LightBaseParameter)command.Parameters["strMunicipio"]).Value = estruturaPropriedade.Municipio;
            ((LightBaseParameter)command.Parameters["strUf"]).Value = estruturaPropriedade.Uf;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = estruturaPropriedade.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = estruturaPropriedade.DataUsuario;
            ((LightBaseParameter)command.Parameters["dtData"]).Value = estruturaPropriedade.Data;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

            const string idRetrievingCommand = "@@Id";
            LightBaseCommand lastIdCommand = new LightBaseCommand(idRetrievingCommand, _Connection);
            long ultimoId = Convert.ToInt32(lastIdCommand.ExecuteScalar());

            return ultimoId;

        }

        public EstruturaPropriedade GetEstruturaPropriedadeById(string id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_PROPRIEDADE where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new EstruturaPropriedade();

            if (qReader.Read())
            {

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strNome"] != DBNull.Value)
                {
                    retVal.NomePropriedade = qReader["strNome"].ToString();
                }
                if (qReader["dtData"] != DBNull.Value) retVal.Data = (DateTime)qReader["dtData"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strLocalizacao"] != DBNull.Value) retVal.Localizacao = qReader["strLocalizacao"].ToString();
                if (qReader["strRegistroOficial"] != DBNull.Value) retVal.RegistroOficial = qReader["strRegistroOficial"].ToString();

                if (qReader["decArea"] != DBNull.Value) retVal.Area = (double)qReader["decArea"];
                if (qReader["decAreaUtilizada"] != DBNull.Value) retVal.AreaUtilizada = (double)qReader["decAreaUtilizada"];
                if (qReader["decAreaPreservacao"] != DBNull.Value) retVal.Reserva = (double)qReader["decAreaPreservacao"];
                if (qReader["docAtividades"] != DBNull.Value) retVal.Atividades = qReader["docAtividades"].ToString();
                if (qReader["strMunicipio"] != DBNull.Value) retVal.Municipio = qReader["strMunicipio"].ToString();
                if (qReader["strUf"] != DBNull.Value) retVal.Uf = qReader["strUf"].ToString();
                if (qReader["strId"] != DBNull.Value) retVal.StrId = qReader["strId"].ToString();

                DataTable tablePastagens = (DataTable)qReader["Pastagens"];
                double areaTotalAcumuladaPastagens = 0;
                int i = 0;
                foreach (DataRow rowPastagem in tablePastagens.Rows)
                {
                    Pastagem pastagem = new Pastagem();
                    pastagem.Id = i;
                    if (rowPastagem["PAS_strTipo"] != DBNull.Value) pastagem.Tipo = (string)rowPastagem["PAS_strTipo"];
                    if (rowPastagem["PAS_dtDataPastagem"] != DBNull.Value) pastagem.Data = (DateTime)rowPastagem["PAS_dtDataPastagem"];
                    if (rowPastagem["PAS_decAreaTipo"] != DBNull.Value)
                    {
                        pastagem.Area = (double)rowPastagem["PAS_decAreaTipo"];
                        areaTotalAcumuladaPastagens = areaTotalAcumuladaPastagens + pastagem.Area;
                    }
                    pastagem.AreaTotalAcumulada = areaTotalAcumuladaPastagens;

                    retVal.Pastagens.Add(pastagem);
                    i++;
                }

                DataTable tableAgriculturas = (DataTable)qReader["Agriculturas"];
                double areaTotalAcumuladaAgriculturas = 0;
                int j = 0;
                foreach (DataRow rowAgricultura in tableAgriculturas.Rows)
                {
                    Agricultura agricultura = new Agricultura();
                    agricultura.Id = j;
                    if (rowAgricultura["AGR_strTipo"] != DBNull.Value) agricultura.Tipo = (string)rowAgricultura["AGR_strTipo"];
                    if (rowAgricultura["AGR_dtDataAgricultura"] != DBNull.Value) agricultura.Data = (DateTime)rowAgricultura["AGR_dtDataAgricultura"];
                    if (rowAgricultura["AGR_decAreaTipo"] != DBNull.Value)
                    {
                        agricultura.Area = (double)rowAgricultura["AGR_decAreaTipo"];
                        areaTotalAcumuladaAgriculturas = areaTotalAcumuladaAgriculturas + agricultura.Area;
                    }
                    agricultura.AreaTotalAcumulada = areaTotalAcumuladaAgriculturas;

                    retVal.Agriculturas.Add(agricultura);
                    j++;
                }

                DataTable tableBenfeitorias = (DataTable)qReader["Benfeitorias"];
                double areaTotalAcumuladaBenfeitorias = 0;
                int k = 0;
                foreach (DataRow rowBenfeitoria in tableBenfeitorias.Rows)
                {
                    Benfeitoria benfeitoria = new Benfeitoria();
                    benfeitoria.Id = k;
                    if (rowBenfeitoria["BEN_strTipo"] != DBNull.Value) benfeitoria.Tipo = (string)rowBenfeitoria["BEN_strTipo"];
                    if (rowBenfeitoria["BEN_dtDataBenfeitoria"] != DBNull.Value) benfeitoria.Data = (DateTime)rowBenfeitoria["BEN_dtDataBenfeitoria"];
                    if (rowBenfeitoria["BEN_decAreaTipo"] != DBNull.Value)
                    {
                        benfeitoria.Area = (double)rowBenfeitoria["BEN_decAreaTipo"];
                        areaTotalAcumuladaBenfeitorias = areaTotalAcumuladaBenfeitorias + benfeitoria.Area;
                    }
                    benfeitoria.AreaTotalAcumulada = areaTotalAcumuladaBenfeitorias;

                    retVal.Benfeitorias.Add(benfeitoria);
                    k++;
                }

                DataTable tableArrendamentos = (DataTable)qReader["Arrendamentos"];
                double areaTotalAcumuladaArrendamentos = 0;
                int l = 0;
                foreach (DataRow rowArrendamento in tableArrendamentos.Rows)
                {
                    Arrendamento arrendamento = new Arrendamento();
                    arrendamento.Id = l;
                    if (rowArrendamento["ARR_strTipo"] != DBNull.Value) arrendamento.Tipo = (string)rowArrendamento["ARR_strTipo"];
                    if (rowArrendamento["ARR_dtDataArrendamento"] != DBNull.Value) arrendamento.Data = (DateTime)rowArrendamento["ARR_dtDataArrendamento"];
                    if (rowArrendamento["ARR_decAreaTipo"] != DBNull.Value)
                    {
                        arrendamento.Area = (double)rowArrendamento["ARR_decAreaTipo"];
                        areaTotalAcumuladaArrendamentos = areaTotalAcumuladaArrendamentos + arrendamento.Area;
                    }
                    arrendamento.AreaTotalAcumulada = areaTotalAcumuladaArrendamentos;

                    retVal.Arrendamentos.Add(arrendamento);
                    l++;
                }

                DataTable tableOutras = (DataTable)qReader["Outras"];
                double areaTotalAcumuladaOutras = 0;
                int m = 0;
                foreach (DataRow rowOutra in tableOutras.Rows)
                {
                    Outra outra = new Outra();
                    outra.Id = m;
                    if (rowOutra["OUT_strTipo"] != DBNull.Value) outra.Tipo = (string)rowOutra["OUT_strTipo"];
                    if (rowOutra["OUT_dtDataOutrasAreas"] != DBNull.Value) outra.Data = (DateTime)rowOutra["OUT_dtDataOutrasAreas"];
                    if (rowOutra["OUT_decAreaTipo"] != DBNull.Value)
                    {
                        outra.Area = (double)rowOutra["OUT_decAreaTipo"];
                        areaTotalAcumuladaOutras = areaTotalAcumuladaOutras + outra.Area;
                    }
                    outra.AreaTotalAcumulada = areaTotalAcumuladaOutras;

                    retVal.Outras.Add(outra);
                    m++;
                }


                int linhasA = GetAgriculturas(Convert.ToInt32(retVal.Id)).Count;
                if (linhasA > 0)
                {
                    var agr = GetAgriculturaById(Convert.ToInt32(retVal.Id), linhasA - 1).AreaTotalAcumulada;
                    retVal.TotalAgricultura = agr;
                }
                else
                {
                    retVal.TotalAgricultura = 0;
                }

                int linhasP = GetPastagem(Convert.ToInt32(retVal.Id)).Count;
                if (linhasP > 0)
                {
                    var pas = GetPastagemById(Convert.ToInt32(retVal.Id), linhasP - 1).AreaTotalAcumulada;
                    retVal.TotalPastagens = pas;
                }
                else
                {
                    retVal.TotalPastagens = 0;
                }


                int linhasB = GetBenfeitorias(Convert.ToInt32(retVal.Id)).Count;
                if (linhasB > 0)
                {
                    var ben = GetBenfeitoriaById(Convert.ToInt32(retVal.Id), linhasB - 1).AreaTotalAcumulada;
                    retVal.TotalBenfeitoria = ben;
                }
                else
                {
                    retVal.TotalBenfeitoria = 0;
                }

                int linhasArr = GetArrendamentos(Convert.ToInt32(retVal.Id)).Count;
                if (linhasArr > 0)
                {
                    var arr = GetArrendamentoById(Convert.ToInt32(retVal.Id), linhasArr - 1).AreaTotalAcumulada;
                    retVal.TotalArrendamento = arr;
                }
                else
                {
                    retVal.TotalArrendamento = 0;
                }

                int linhasO = GetOutras(Convert.ToInt32(retVal.Id)).Count;
                if (linhasO > 0)
                {
                    var outr = GetOutraById(Convert.ToInt32(retVal.Id), linhasO - 1).AreaTotalAcumulada;
                    retVal.TotalOutras = outr;
                }
                else
                {
                    retVal.TotalOutras = 0;
                }

            }


            qReader.Close();
            return retVal;
        }

        public EstruturaPropriedade GetEstruturaPropriedade(string id, DateTime dataInicio, DateTime dataFim)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_PROPRIEDADE where id=@id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new EstruturaPropriedade();

            if (qReader.Read())
            {

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strNome"] != DBNull.Value)
                {
                    retVal.NomePropriedade = qReader["strNome"].ToString();
                }
                if (qReader["dtData"] != DBNull.Value) retVal.Data = (DateTime)qReader["dtData"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strLocalizacao"] != DBNull.Value) retVal.Localizacao = qReader["strLocalizacao"].ToString();
                if (qReader["strRegistroOficial"] != DBNull.Value) retVal.RegistroOficial = qReader["strRegistroOficial"].ToString();

                if (qReader["decArea"] != DBNull.Value) retVal.Area = (double)qReader["decArea"];
                if (qReader["decAreaUtilizada"] != DBNull.Value) retVal.AreaUtilizada = (double)qReader["decAreaUtilizada"];
                if (qReader["decAreaPreservacao"] != DBNull.Value) retVal.Reserva = (double)qReader["decAreaPreservacao"];
                if (qReader["docAtividades"] != DBNull.Value) retVal.Atividades = qReader["docAtividades"].ToString();
                if (qReader["strMunicipio"] != DBNull.Value) retVal.Municipio = qReader["strMunicipio"].ToString();
                if (qReader["strUf"] != DBNull.Value) retVal.Uf = qReader["strUf"].ToString();
                if (qReader["strId"] != DBNull.Value) retVal.StrId = qReader["strId"].ToString();

                DataTable tablePastagens = (DataTable)qReader["Pastagens"];
                double areaTotalAcumuladaPastagens = 0;
                int i = 0;
                foreach (DataRow rowPastagem in tablePastagens.Rows)
                {
                    Pastagem pastagem = new Pastagem();
                    pastagem.Id = i;
                    if (rowPastagem["PAS_strTipo"] != DBNull.Value) pastagem.Tipo = (string)rowPastagem["PAS_strTipo"];
                    if (rowPastagem["PAS_dtDataPastagem"] != DBNull.Value) pastagem.Data = (DateTime)rowPastagem["PAS_dtDataPastagem"];
                    if (rowPastagem["PAS_decAreaTipo"] != DBNull.Value)
                    {
                        pastagem.Area = (double)rowPastagem["PAS_decAreaTipo"];
                        areaTotalAcumuladaPastagens = areaTotalAcumuladaPastagens + pastagem.Area;
                    }
                    pastagem.AreaTotalAcumulada = areaTotalAcumuladaPastagens;

                    retVal.Pastagens.Add(pastagem);
                    i++;
                }

                DataTable tableAgriculturas = (DataTable)qReader["Agriculturas"];
                double areaTotalAcumuladaAgriculturas = 0;
                int j = 0;
                foreach (DataRow rowAgricultura in tableAgriculturas.Rows)
                {
                    Agricultura agricultura = new Agricultura();
                    agricultura.Id = j;
                    if (rowAgricultura["AGR_strTipo"] != DBNull.Value) agricultura.Tipo = (string)rowAgricultura["AGR_strTipo"];
                    if (rowAgricultura["AGR_dtDataAgricultura"] != DBNull.Value) agricultura.Data = (DateTime)rowAgricultura["AGR_dtDataAgricultura"];
                    if (rowAgricultura["AGR_decAreaTipo"] != DBNull.Value)
                    {
                        agricultura.Area = (double)rowAgricultura["AGR_decAreaTipo"];
                        areaTotalAcumuladaAgriculturas = areaTotalAcumuladaAgriculturas + agricultura.Area;
                    }
                    agricultura.AreaTotalAcumulada = areaTotalAcumuladaAgriculturas;

                    retVal.Agriculturas.Add(agricultura);
                    j++;
                }


                DataTable tableBenfeitorias = (DataTable)qReader["Benfeitorias"];
                double areaTotalAcumuladaBenfeitorias = 0;
                int k = 0;
                foreach (DataRow rowBenfeitoria in tableBenfeitorias.Rows)
                {
                    Benfeitoria benfeitoria = new Benfeitoria();
                    benfeitoria.Id = k;
                    if (rowBenfeitoria["BEN_strTipo"] != DBNull.Value) benfeitoria.Tipo = (string)rowBenfeitoria["BEN_strTipo"];
                    if (rowBenfeitoria["BEN_dtDataBenfeitoria"] != DBNull.Value) benfeitoria.Data = (DateTime)rowBenfeitoria["BEN_dtDataBenfeitoria"];
                    if (rowBenfeitoria["BEN_decAreaTipo"] != DBNull.Value)
                    {
                        benfeitoria.Area = (double)rowBenfeitoria["BEN_decAreaTipo"];
                        areaTotalAcumuladaBenfeitorias = areaTotalAcumuladaBenfeitorias + benfeitoria.Area;
                    }
                    benfeitoria.AreaTotalAcumulada = areaTotalAcumuladaBenfeitorias;

                    retVal.Benfeitorias.Add(benfeitoria);
                    k++;
                }

                DataTable tableArrendamentos = (DataTable)qReader["Arrendamentos"];
                double areaTotalAcumuladaArrendamentos = 0;
                int l = 0;
                foreach (DataRow rowArrendamento in tableArrendamentos.Rows)
                {
                    Arrendamento arrendamento = new Arrendamento();
                    arrendamento.Id = l;
                    if (rowArrendamento["ARR_strTipo"] != DBNull.Value) arrendamento.Tipo = (string)rowArrendamento["ARR_strTipo"];
                    if (rowArrendamento["ARR_dtDataArrendamento"] != DBNull.Value) arrendamento.Data = (DateTime)rowArrendamento["ARR_dtDataArrendamento"];
                    if (rowArrendamento["ARR_decAreaTipo"] != DBNull.Value)
                    {
                        arrendamento.Area = (double)rowArrendamento["ARR_decAreaTipo"];
                        areaTotalAcumuladaArrendamentos = areaTotalAcumuladaArrendamentos + arrendamento.Area;
                    }
                    arrendamento.AreaTotalAcumulada = areaTotalAcumuladaArrendamentos;

                    retVal.Arrendamentos.Add(arrendamento);
                    l++;
                }


                DataTable tableOutras = (DataTable)qReader["Outras"];
                double areaTotalAcumuladaOutras = 0;
                int m = 0;
                foreach (DataRow rowOutra in tableOutras.Rows)
                {
                    Outra outra = new Outra();
                    outra.Id = m;
                    if (rowOutra["OUT_strTipo"] != DBNull.Value) outra.Tipo = (string)rowOutra["OUT_strTipo"];
                    if (rowOutra["OUT_dtDataOutrasAreas"] != DBNull.Value) outra.Data = (DateTime)rowOutra["OUT_dtDataOutrasAreas"];
                    if (rowOutra["OUT_decAreaTipo"] != DBNull.Value)
                    {
                        outra.Area = (double)rowOutra["OUT_decAreaTipo"];
                        areaTotalAcumuladaOutras = areaTotalAcumuladaOutras + outra.Area;
                    }
                    outra.AreaTotalAcumulada = areaTotalAcumuladaOutras;

                    retVal.Outras.Add(outra);
                    m++;
                }

            }


            qReader.Close();
            return retVal;
        }

        public List<ResultadoBuscaEstruturaPropriedade> ConsultaEstruturaPropriedade(ParametrosDeBuscaEmEstruturaPropriedades parametrosBuscaEmEstruturaPropriedades)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmEstruturaPropriedades.TodosOsCampos);
            if (parametrosBuscaEmEstruturaPropriedades.Id > 0)
            {
                filter = AddParametro(filter, "id", parametrosBuscaEmEstruturaPropriedades.Id.ToString());
            }

            filter = AddParametro(filter, "strNome", parametrosBuscaEmEstruturaPropriedades.NomePropriedade);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_PROPRIEDADE " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var estruturasPropriedades = new List<ResultadoBuscaEstruturaPropriedade>();

            while (qReader.Read())
            {

                var retVal = new ResultadoBuscaEstruturaPropriedade();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["dtData"] != DBNull.Value) retVal.Data = (DateTime)qReader["dtData"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strNome"] != DBNull.Value)
                {
                    retVal.NomePropriedade = qReader["strNome"].ToString();
                }

                int linhasA = GetAgriculturas(Convert.ToInt32(retVal.Id)).Count;
                if (linhasA > 0)
                {
                    var agr = GetAgriculturaById(Convert.ToInt32(retVal.Id), linhasA - 1).AreaTotalAcumulada;
                    retVal.TotalAgricultura = agr;
                }
                else
                {
                    retVal.TotalAgricultura = 0;
                }

                int linhasP = GetPastagem(Convert.ToInt32(retVal.Id)).Count;
                if (linhasP > 0)
                {
                    var pas = GetPastagemById(Convert.ToInt32(retVal.Id), linhasP - 1).AreaTotalAcumulada;
                    retVal.TotalPastagens = pas;
                }
                else
                {
                    retVal.TotalPastagens = 0;
                }

                int linhasB = GetBenfeitorias(Convert.ToInt32(retVal.Id)).Count;
                if (linhasB > 0)
                {
                    var ben = GetBenfeitoriaById(Convert.ToInt32(retVal.Id), linhasB - 1).AreaTotalAcumulada;
                    retVal.Benfeitorias = ben;
                }
                else
                {
                    retVal.Benfeitorias = 0;
                }


                int linhasArr = GetArrendamentos(Convert.ToInt32(retVal.Id)).Count;
                if (linhasArr > 0)
                {
                    var arr = GetArrendamentoById(Convert.ToInt32(retVal.Id), linhasArr - 1).AreaTotalAcumulada;
                    retVal.Arrendamentos = arr;
                }
                else
                {
                    retVal.Arrendamentos = 0;
                }


                int linhasO = GetOutras(Convert.ToInt32(retVal.Id)).Count;
                if (linhasO > 0)
                {
                    var outr = GetOutraById(Convert.ToInt32(retVal.Id), linhasO - 1).AreaTotalAcumulada;
                    retVal.Outras = outr;
                }
                else
                {
                    retVal.Outras = 0;
                }

                if (qReader["decArea"] != DBNull.Value) retVal.Area = (double)qReader["decArea"];
                if (qReader["decAreaUtilizada"] != DBNull.Value) retVal.AreaUtilizada = (double)qReader["decAreaUtilizada"];
                if (qReader["decAreaPreservacao"] != DBNull.Value) retVal.Reserva = (double)qReader["decAreaPreservacao"];

                estruturasPropriedades.Add(retVal);
            }
            qReader.Close();

            var orderEstruturasPropriedades = estruturasPropriedades.OrderBy(s => s.Data);

            return orderEstruturasPropriedades.ToList();
        }


        public void RemoveEstruturaPropriedade(string id)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_PROPRIEDADE where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }

        public void SalvaEstruturaPropriedade(EstruturaPropriedade estruturaPropriedade)
        {

            List<string> campos = new List<string>()
                                      {
                                          "diretorio",
                                          "strNome",
                                          "strLocalizacao",
                                          "strRegistroOficial",
                                          "decArea",
                                          "decAreaUtilizada",
                                          "decAreaPreservacao",
                                          "docAtividades",
                                          "strMunicipio",
                                          "strUf",
                                          "strUsuario",
                                          "dtDataUsuario",
                                          "strId",
                                          "dtData"
                                      };

            var command = new LightBaseCommand(BuildEstruturaPropriedadeString(campos));
            ((LightBaseParameter)command.Parameters["id"]).Value = estruturaPropriedade.Id;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = estruturaPropriedade.Diretorio;
            ((LightBaseParameter)command.Parameters["strNome"]).Value = estruturaPropriedade.NomePropriedade;
            ((LightBaseParameter)command.Parameters["strLocalizacao"]).Value = estruturaPropriedade.Localizacao;
            ((LightBaseParameter)command.Parameters["strRegistroOficial"]).Value = estruturaPropriedade.RegistroOficial;
            ((LightBaseParameter)command.Parameters["decArea"]).Value = estruturaPropriedade.Area;
            ((LightBaseParameter)command.Parameters["decAreaUtilizada"]).Value = estruturaPropriedade.AreaUtilizada;
            ((LightBaseParameter)command.Parameters["decAreaPreservacao"]).Value = estruturaPropriedade.Reserva;
            ((LightBaseParameter)command.Parameters["docAtividades"]).Value = estruturaPropriedade.Atividades;
            ((LightBaseParameter)command.Parameters["strMunicipio"]).Value = estruturaPropriedade.Municipio;
            ((LightBaseParameter)command.Parameters["strUf"]).Value = estruturaPropriedade.Uf;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = estruturaPropriedade.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = estruturaPropriedade.DataUsuario;
            ((LightBaseParameter)command.Parameters["strId"]).Value = estruturaPropriedade.StrId;
            ((LightBaseParameter)command.Parameters["dtData"]).Value = estruturaPropriedade.Data;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public void AdicionaPastagem(string id, Pastagem pastagem)
        {
            var command =
                new LightBaseCommand(@"insert into FCARNAUBA_PROPRIEDADE Pastagens values ({{@PAS_strTipo, @PAS_decAreaTipo, @PAS_dtDataPastagem}}) parent id = @id");

            ((LightBaseParameter)command.Parameters["PAS_strTipo"]).Value = pastagem.Tipo;
            ((LightBaseParameter)command.Parameters["PAS_decAreaTipo"]).Value = pastagem.Area;
            ((LightBaseParameter)command.Parameters["PAS_dtDataPastagem"]).Value = pastagem.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public List<Pastagem> GetPastagem(int estrututuraPropriedadeID)
        {
            double areaTotalAcumuladaPastagens = 0;
            var command = new LightBaseCommand(@"select Pastagens from FCARNAUBA_PROPRIEDADE where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = estrututuraPropriedadeID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Pastagem>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Pastagens"];
                int p = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    Pastagem pastagem = new Pastagem();
                    pastagem.Id = p;
                    if (dRow["PAS_strTipo"] != DBNull.Value) pastagem.Tipo = (string)dRow["PAS_strTipo"];
                    if (dRow["PAS_decAreaTipo"] != DBNull.Value)
                    {
                        pastagem.Area = (double)dRow["PAS_decAreaTipo"];
                        areaTotalAcumuladaPastagens = areaTotalAcumuladaPastagens + pastagem.Area;
                    }
                    if (dRow["PAS_dtDataPastagem"] != DBNull.Value) pastagem.Data = (DateTime)dRow["PAS_dtDataPastagem"];

                    pastagem.AreaTotalAcumulada = areaTotalAcumuladaPastagens;
                    p++;
                    retVal.Add(pastagem);
                }

            }

            qReader.Close();
            return retVal;
        }

        public Pastagem GetPastagemById(int estrututuraPropriedadeID, int pastagemId)
        {
            var pastagemList = GetPastagem(estrututuraPropriedadeID);
            return pastagemList[pastagemId];
        }


        public void SalvaPastagem(int id, Pastagem pastagem, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("update FCARNAUBA_PROPRIEDADE set Pastagens[" + sRow +
                                     "].PAS_strTipo = @PAS_strTipo," + Environment.NewLine +
                                     "Pastagens[" + sRow +
                                     "].PAS_decAreaTipo = @PAS_decAreaTipo," + Environment.NewLine +
                                     "Pastagens[" + sRow +
                                     "].PAS_dtDataPastagem = @PAS_dtDataPastagem where id = @id");

            ((LightBaseParameter)command.Parameters["PAS_strTipo"]).Value = pastagem.Tipo;
            ((LightBaseParameter)command.Parameters["PAS_decAreaTipo"]).Value = pastagem.Area;
            ((LightBaseParameter)command.Parameters["PAS_dtDataPastagem"]).Value = pastagem.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }


        public void RemovePastagem(int id, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("delete from FCARNAUBA_PROPRIEDADE.Pastagens[" + sRow +
                                         "] where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }


        public void AdicionaAgricultura(string id, Agricultura agricultura)
        {
            var command =
                new LightBaseCommand(@"insert into FCARNAUBA_PROPRIEDADE Agriculturas values ({{@AGR_strTipo, @AGR_decAreaTipo, @AGR_dtDataAgricultura}}) parent id = @id");

            ((LightBaseParameter)command.Parameters["AGR_strTipo"]).Value = agricultura.Tipo;
            ((LightBaseParameter)command.Parameters["AGR_decAreaTipo"]).Value = agricultura.Area;
            ((LightBaseParameter)command.Parameters["AGR_dtDataAgricultura"]).Value = agricultura.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public List<Agricultura> GetAgriculturas(int estrututuraPropriedadeID)
        {
            double areaTotalAcumuladaAgriculturas = 0;
            var command = new LightBaseCommand(@"select Agriculturas from FCARNAUBA_PROPRIEDADE where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = estrututuraPropriedadeID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Agricultura>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Agriculturas"];
                int a = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    Agricultura agricultura = new Agricultura();
                    agricultura.Id = a;
                    if (dRow["AGR_strTipo"] != DBNull.Value) agricultura.Tipo = (string)dRow["AGR_strTipo"];
                    if (dRow["AGR_dtDataAgricultura"] != DBNull.Value) agricultura.Data = (DateTime)dRow["AGR_dtDataAgricultura"];
                    if (dRow["AGR_decAreaTipo"] != DBNull.Value)
                    {
                        agricultura.Area = (double)dRow["AGR_decAreaTipo"];
                        areaTotalAcumuladaAgriculturas = areaTotalAcumuladaAgriculturas + agricultura.Area;
                    }
                    agricultura.AreaTotalAcumulada = areaTotalAcumuladaAgriculturas;
                    a++;
                    retVal.Add(agricultura);
                }

            }

            qReader.Close();
            return retVal;
        }

        public Agricultura GetAgriculturaById(int estrututuraPropriedadeID, int agriculturaId)
        {
            var agriculturaList = GetAgriculturas(estrututuraPropriedadeID);
            return agriculturaList[agriculturaId];
        }


        public void SalvaAgricultura(int id, Agricultura agricultura, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("update FCARNAUBA_PROPRIEDADE set Agriculturas[" + sRow +
                                     "].AGR_strTipo = @AGR_strTipo," + Environment.NewLine +
                                     "Agriculturas[" + sRow +
                                     "].AGR_decAreaTipo = @AGR_decAreaTipo," + Environment.NewLine +
                                     "Agriculturas[" + sRow +
                                     "].AGR_dtDataAgricultura = @AGR_dtDataAgricultura where id = @id");

            ((LightBaseParameter)command.Parameters["AGR_strTipo"]).Value = agricultura.Tipo;
            ((LightBaseParameter)command.Parameters["AGR_decAreaTipo"]).Value = agricultura.Area;
            ((LightBaseParameter)command.Parameters["AGR_dtDataAgricultura"]).Value = agricultura.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public void RemoveAgricultura(int id, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("delete from FCARNAUBA_PROPRIEDADE.Agriculturas[" + sRow +
                                         "] where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }


        public void AdicionaBenfeitoria(string id, Benfeitoria benfeitoria)
        {
            var command =
                new LightBaseCommand(@"insert into FCARNAUBA_PROPRIEDADE Benfeitorias values ({{@BEN_strTipo, @BEN_decAreaTipo, @BEN_dtDataBenfeitoria}}) parent id = @id");

            ((LightBaseParameter)command.Parameters["BEN_strTipo"]).Value = benfeitoria.Tipo;
            ((LightBaseParameter)command.Parameters["BEN_decAreaTipo"]).Value = benfeitoria.Area;
            ((LightBaseParameter)command.Parameters["BEN_dtDataBenfeitoria"]).Value = benfeitoria.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public List<Benfeitoria> GetBenfeitorias(int estrututuraPropriedadeID)
        {
            double areaTotalAcumuladaBenfeitorias = 0;
            var command = new LightBaseCommand(@"select Benfeitorias from FCARNAUBA_PROPRIEDADE where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = estrututuraPropriedadeID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Benfeitoria>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Benfeitorias"];
                int a = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    Benfeitoria benfeitoria = new Benfeitoria();
                    benfeitoria.Id = a;
                    if (dRow["BEN_strTipo"] != DBNull.Value) benfeitoria.Tipo = (string)dRow["BEN_strTipo"];
                    if (dRow["BEN_dtDataBenfeitoria"] != DBNull.Value) benfeitoria.Data = (DateTime)dRow["BEN_dtDataBenfeitoria"];
                    if (dRow["BEN_decAreaTipo"] != DBNull.Value)
                    {
                        benfeitoria.Area = (double)dRow["BEN_decAreaTipo"];
                        areaTotalAcumuladaBenfeitorias = areaTotalAcumuladaBenfeitorias + benfeitoria.Area;
                    }
                    benfeitoria.AreaTotalAcumulada = areaTotalAcumuladaBenfeitorias;
                    a++;
                    retVal.Add(benfeitoria);
                }

            }

            qReader.Close();
            return retVal;
        }

        public Benfeitoria GetBenfeitoriaById(int estrututuraPropriedadeID, int benfeitoriaId)
        {
            var benfeitoriaList = GetBenfeitorias(estrututuraPropriedadeID);
            return benfeitoriaList[benfeitoriaId];
        }

        public void SalvaBenfeitoria(int id, Benfeitoria benfeitoria, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("update FCARNAUBA_PROPRIEDADE set Benfeitorias[" + sRow +
                                     "].BEN_strTipo = @BEN_strTipo," + Environment.NewLine +
                                     "Benfeitorias[" + sRow +
                                     "].BEN_decAreaTipo = @BEN_decAreaTipo," + Environment.NewLine +
                                     "Benfeitorias[" + sRow +
                                     "].BEN_dtDataBenfeitoria = @BEN_dtDataBenfeitoria where id = @id");

            ((LightBaseParameter)command.Parameters["BEN_strTipo"]).Value = benfeitoria.Tipo;
            ((LightBaseParameter)command.Parameters["BEN_decAreaTipo"]).Value = benfeitoria.Area;
            ((LightBaseParameter)command.Parameters["BEN_dtDataBenfeitoria"]).Value = benfeitoria.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public void RemoveBenfeitoria(int id, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("delete from FCARNAUBA_PROPRIEDADE.Benfeitorias[" + sRow +
                                         "] where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public void AdicionaArrendamento(string id, Arrendamento arrendamento)
        {
            var command =
                new LightBaseCommand(@"insert into FCARNAUBA_PROPRIEDADE Arrendamentos values ({{@ARR_strTipo, @ARR_decAreaTipo, @ARR_dtDataArrendamento}}) parent id = @id");

            ((LightBaseParameter)command.Parameters["ARR_strTipo"]).Value = arrendamento.Tipo;
            ((LightBaseParameter)command.Parameters["ARR_decAreaTipo"]).Value = arrendamento.Area;
            ((LightBaseParameter)command.Parameters["ARR_dtDataArrendamento"]).Value = arrendamento.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public List<Arrendamento> GetArrendamentos(int estrututuraPropriedadeID)
        {
            double areaTotalAcumuladaArrendamentos = 0;
            var command = new LightBaseCommand(@"select Arrendamentos from FCARNAUBA_PROPRIEDADE where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = estrututuraPropriedadeID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Arrendamento>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Arrendamentos"];
                int a = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    Arrendamento arrendamento = new Arrendamento();
                    arrendamento.Id = a;
                    if (dRow["ARR_strTipo"] != DBNull.Value) arrendamento.Tipo = (string)dRow["ARR_strTipo"];
                    if (dRow["ARR_dtDataArrendamento"] != DBNull.Value) arrendamento.Data = (DateTime)dRow["ARR_dtDataArrendamento"];
                    if (dRow["ARR_decAreaTipo"] != DBNull.Value)
                    {
                        arrendamento.Area = (double)dRow["ARR_decAreaTipo"];
                        areaTotalAcumuladaArrendamentos = areaTotalAcumuladaArrendamentos + arrendamento.Area;
                    }
                    arrendamento.AreaTotalAcumulada = areaTotalAcumuladaArrendamentos;
                    a++;
                    retVal.Add(arrendamento);
                }

            }

            qReader.Close();
            return retVal;
        }

        public Arrendamento GetArrendamentoById(int estrututuraPropriedadeID, int arrendamentoId)
        {
            var arrendamentoList = GetArrendamentos(estrututuraPropriedadeID);
            return arrendamentoList[arrendamentoId];
        }


        public void SalvaArrendamento(int id, Arrendamento arrendamento, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("update FCARNAUBA_PROPRIEDADE set Arrendamentos[" + sRow +
                                     "].ARR_strTipo = @ARR_strTipo," + Environment.NewLine +
                                     "Arrendamentos[" + sRow +
                                     "].ARR_decAreaTipo = @ARR_decAreaTipo," + Environment.NewLine +
                                     "Arrendamentos[" + sRow +
                                     "].ARR_dtDataArrendamento = @ARR_dtDataArrendamento where id = @id");

            ((LightBaseParameter)command.Parameters["ARR_strTipo"]).Value = arrendamento.Tipo;
            ((LightBaseParameter)command.Parameters["ARR_decAreaTipo"]).Value = arrendamento.Area;
            ((LightBaseParameter)command.Parameters["ARR_dtDataArrendamento"]).Value = arrendamento.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public void RemoveArrendamento(int id, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("delete from FCARNAUBA_PROPRIEDADE.Arrendamentos[" + sRow +
                                         "] where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public void AdicionaOutra(string id, Outra outra)
        {
            var command =
                new LightBaseCommand(@"insert into FCARNAUBA_PROPRIEDADE Outras values ({{@OUT_strTipo, @OUT_decAreaTipo, @OUT_dtDataOutrasAreas}}) parent id = @id");

            ((LightBaseParameter)command.Parameters["OUT_strTipo"]).Value = outra.Tipo;
            ((LightBaseParameter)command.Parameters["OUT_decAreaTipo"]).Value = outra.Area;
            ((LightBaseParameter)command.Parameters["OUT_dtDataOutrasAreas"]).Value = outra.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public List<Outra> GetOutras(int estrututuraPropriedadeID)
        {
            double areaTotalAcumuladaOutras = 0;
            var command = new LightBaseCommand(@"select Outras from FCARNAUBA_PROPRIEDADE where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = estrututuraPropriedadeID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Outra>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Outras"];
                int a = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    Outra outra = new Outra();
                    outra.Id = a;
                    if (dRow["OUT_strTipo"] != DBNull.Value) outra.Tipo = (string)dRow["OUT_strTipo"];
                    if (dRow["OUT_dtDataOutrasAreas"] != DBNull.Value) outra.Data = (DateTime)dRow["OUT_dtDataOutrasAreas"];
                    if (dRow["OUT_decAreaTipo"] != DBNull.Value)
                    {
                        outra.Area = (double)dRow["OUT_decAreaTipo"];
                        areaTotalAcumuladaOutras = areaTotalAcumuladaOutras + outra.Area;
                    }
                    outra.AreaTotalAcumulada = areaTotalAcumuladaOutras;
                    a++;
                    retVal.Add(outra);
                }

            }

            qReader.Close();
            return retVal;
        }

        public Outra GetOutraById(int estrututuraPropriedadeID, int outraId)
        {
            var outraList = GetOutras(estrututuraPropriedadeID);
            return outraList[outraId];
        }

        public int QuantidadeDeFilhosPai(string paiId)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_ANIMAIS where strPaiId = @strPaiId");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strPaiId"]).Value = paiId;
            var qReader = command.ExecuteReader();
            qReader.Read();
            return (int)qReader[0];
        }

        public void SalvaOutra(int id, Outra outra, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("update FCARNAUBA_PROPRIEDADE set Outras[" + sRow +
                                     "].OUT_strTipo = @OUT_strTipo," + Environment.NewLine +
                                     "Outras[" + sRow +
                                     "].OUT_decAreaTipo = @OUT_decAreaTipo," + Environment.NewLine +
                                     "Outras[" + sRow +
                                     "].OUT_dtDataOutrasAreas = @OUT_dtDataOutrasAreas where id = @id");

            ((LightBaseParameter)command.Parameters["OUT_strTipo"]).Value = outra.Tipo;
            ((LightBaseParameter)command.Parameters["OUT_decAreaTipo"]).Value = outra.Area;
            ((LightBaseParameter)command.Parameters["OUT_dtDataOutrasAreas"]).Value = outra.Data;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public void RemoveOutra(int id, int row)
        {
            string sRow = Convert.ToString(row);
            var command =
                new LightBaseCommand("delete from FCARNAUBA_PROPRIEDADE.Outras[" + sRow +
                                         "] where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public int QuantidadeDeFilhosMae(string maeId)
        {
            int filhos = 0;
            var command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strMaeId = @strMaeId");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {
                bool fiv = false;
                if (qReader["vfFiv"] != DBNull.Value) fiv = (bool)qReader["vfFiv"];

                if (!fiv)
                {
                    filhos++;
                }
            }

            return filhos;
        }

        //Não está fazendo a pesquisa pelo boolean (multivalorado)
        //public int QuantidadeLactacaoControlada(string maeId)
        //{
        //    var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz = @FK_PL_strIdMatriz and PL_vfSairControle = @PL_vfSairControle");
        //    command.Connection = _Connection;
        //    ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = maeId;
        //    ((LightBaseParameter)command.Parameters["PL_vfSairControle"]).Value = 1;
        //    var qReader = command.ExecuteReader();
        //    qReader.Read();
        //    return (int)qReader[0];
        //}

        public int QuantidadeLactacaoControlada(string maeId)
        {
            int quantidade = 0;
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz = @FK_PL_strIdMatriz");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = maeId;
            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {
                bool sairControle = false;
                string maeIdCorr = "";
                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["PL_vfSairControle"] != DBNull.Value) sairControle = (bool)rowProducoesLeite["PL_vfSairControle"];
                    if (rowProducoesLeite["FK_PL_strIdMatriz"] != DBNull.Value) maeIdCorr = (string)rowProducoesLeite["FK_PL_strIdMatriz"];

                    if (maeIdCorr == maeId)
                    {
                        if (sairControle)
                            quantidade++;

                        break;
                    }

                }
            }

            return quantidade;
        }

        public int QuantidadeDeMedicoesPluviometricas(DateTime data, string propriedade)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_CONT_PLUVIOMETRICO where dtData = @dtData and diretorio = @diretorio");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["dtData"]).Value = data;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = propriedade;
            var qReader = command.ExecuteReader();
            qReader.Read();
            return (int)qReader[0];
        }

        public Animal GetPaiMae(string id)
        {
            Animal animal = new Animal();
            var command = new LightBaseCommand(@"select strNome, strNomeCompleto, strRgd from FCARNAUBA_ANIMAIS where strId = @strId");
            ((LightBaseParameter)command.Parameters["strId"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {
                animal.Nome = (string)qReader["strNome"];
                animal.NomeCompleto = (string)qReader["strNomeCompleto"];
                if (qReader["strRgd"] != DBNull.Value) animal.Rgd = (string)qReader["strRgd"];
            }
            qReader.Close();
            return animal;
        }

        public RMachoFemea GetMachoFemea(string strId)
        {
            RMachoFemea retVal = new RMachoFemea();
            var command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strId = @strId");
            ((LightBaseParameter)command.Parameters["strId"]).Value = strId;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {
                if (qReader["strId"] != DBNull.Value) retVal.Id = Convert.ToInt32(qReader["strId"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = (string)qReader["strNomeCompleto"];
                if (qReader["strNome"] != DBNull.Value) retVal.Nome = (string)qReader["strNome"];
                if (qReader["strRgd"] != DBNull.Value) retVal.Rgd = (string)qReader["strRgd"];
                if (qReader["strPaiId"] != DBNull.Value)
                {
                    retVal.PaiId = (string)qReader["strPaiId"];
                    var pai = GetPaiMae(retVal.PaiId);
                    retVal.NomeCompletoPai = pai.NomeCompleto;
                    retVal.NomePai = pai.Nome;
                    retVal.RgdPai = pai.Rgd;
                }
                if (qReader["strMaeId"] != DBNull.Value)
                {
                    retVal.MaeId = (string)qReader["strMaeId"];
                    var mae = GetPaiMae(retVal.MaeId);
                    retVal.NomeCompletoMae = mae.NomeCompleto;
                    retVal.NomeMae = mae.Nome;
                    retVal.RgdMae = mae.Rgd;

                }
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];

                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = (string)qReader["strRaca"];
                if (qReader["strSexo"] != DBNull.Value) retVal.Sexo = (string)qReader["strSexo"];

            }
            qReader.Close();

            if (retVal.Sexo == "Fêmea")
            {
                retVal.Crias = GetCriasMae(retVal);
            }
            else
            {
                retVal.Crias = GetCriasPai(retVal);
            }

            return retVal;
        }

        public List<RCria> GetCriasMae(RMachoFemea machoFemea)
        {
            LightBaseCommand command;
            List<RCria> retVal = new List<RCria>();

            command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strMaeId = @strMaeId or (strReceptoraId = @strMaeId and vfFiv = @vfFiv) order by vfFiv, dtDataNascimento");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = machoFemea.Id;
            ((LightBaseParameter)command.Parameters["vfFiv"]).Value = true;


            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            int i = 1;
            int j = 1;
            int numeroCrias = NumeroCriasMae(machoFemea.Id.ToString());
            DateTime[] datasNascCrias = new DateTime[numeroCrias];
            while (qReader.Read())
            {
                bool fiv = false;
                string receptora = null;

                if (qReader["vfFiv"] != DBNull.Value) fiv = (bool)qReader["vfFiv"];
                if (qReader["strReceptoraId"] != DBNull.Value) receptora = (string)qReader["strReceptoraId"];

                if (!fiv || (fiv && receptora == machoFemea.Id.ToString()))
                {

                    RCria cria = new RCria();
                    cria.NCria = i + "ª";
                    if (qReader["strId"] != DBNull.Value) cria.Id = Convert.ToInt32(qReader["strId"]);
                    if (qReader["strSexo"] != DBNull.Value) cria.Sexo = (string)qReader["strSexo"];
                    if (qReader["dtDataNascimento"] != DBNull.Value) cria.DataNascimento = (DateTime)qReader["dtDataNascimento"];

                    datasNascCrias[i - 1] = cria.DataNascimento;

                    if (qReader["decPn"] != DBNull.Value) cria.Pn = (double)qReader["decPn"];
                    if (qReader["strPaiId"] != DBNull.Value)
                    {
                        cria.PaiId = (string)qReader["strPaiId"];
                        var pai = GetPaiMae(cria.PaiId);
                        cria.NomePai = pai.Nome;
                        cria.NomeCompletoPai = pai.NomeCompleto;
                        cria.RgdPai = pai.Rgd;

                    }

                    if (qReader["strMaeId"] != DBNull.Value)
                    {
                        cria.MaeId = (string)qReader["strMaeId"];
                        var mae = GetPaiMae(cria.MaeId);
                        cria.NomeMae = mae.Nome;
                        cria.NomeCompletoMae = mae.NomeCompleto;
                        cria.RgdMae = mae.Rgd;

                    }

                    if (qReader["strNome"] != DBNull.Value) cria.Nome = (string)qReader["strNome"];
                    if (qReader["strRgd"] != DBNull.Value) cria.Rgd = (string)qReader["strRgd"];


                    if (cria.NCria == "1ª")
                    {
                        cria.IppIep = CalculaIpp(machoFemea.DataNascimento, cria.DataNascimento);
                    }
                    else
                    {
                        cria.IppIep = CalculaIep(datasNascCrias[i - 2], datasNascCrias[i - 1]);
                    }

                    cria.Er = CalculaEr(numeroCrias, CalculaIdadeUltimoParto(machoFemea.DataNascimento, cria.DataNascimento));

                    cria.KgIep = 0;

                    cria.PMedia = Math.Round(GetProducaoMedia(cria.Id.ToString()), 2);
                    cria.PMaxima = Math.Round(GetProducaoMaxima(cria.Id.ToString()), 2);

                    double[] pesos = GetPesos(cria.Id.ToString());

                    if (pesos.Length > 0)
                    {
                        cria.PInicial = Math.Round(pesos.Min(), 2);
                        cria.PFinal = Math.Round(pesos.Max(), 2);
                    }
                    else
                    {

                        cria.PInicial = 0;
                        cria.PFinal = 0;
                    }

                    i++;
                    retVal.Add(cria);

                }
                else
                {
                    RCria cria = new RCria();
                    cria.NCria = j + "ª FIV";
                    if (qReader["strId"] != DBNull.Value) cria.Id = Convert.ToInt32(qReader["strId"]);
                    if (qReader["dtDataNascimento"] != DBNull.Value) cria.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                    if (qReader["strSexo"] != DBNull.Value) cria.Sexo = (string)qReader["strSexo"];

                    if (qReader["decPn"] != DBNull.Value) cria.Pn = (double)qReader["decPn"];
                    if (qReader["strPaiId"] != DBNull.Value)
                    {
                        cria.PaiId = (string)qReader["strPaiId"];
                        var pai = GetPaiMae(cria.PaiId);
                        cria.NomePai = pai.Nome;
                        cria.NomeCompletoPai = pai.NomeCompleto;
                        cria.RgdPai = pai.Rgd;

                    }

                    if (qReader["strMaeId"] != DBNull.Value)
                    {
                        cria.MaeId = (string)qReader["strMaeId"];
                        var mae = GetPaiMae(cria.MaeId);
                        cria.NomeMae = mae.Nome;
                        cria.NomeCompletoMae = mae.NomeCompleto;
                        cria.RgdMae = mae.Rgd;

                    }

                    if (qReader["strNome"] != DBNull.Value) cria.Nome = (string)qReader["strNome"];
                    if (qReader["strRgd"] != DBNull.Value) cria.Rgd = (string)qReader["strRgd"];



                    cria.IppIep = 0;


                    cria.Er = 0;

                    cria.KgIep = 0;

                    cria.PMedia = 0;
                    cria.PMaxima = 0;
                    cria.PInicial = 0;
                    cria.PFinal = 0;
                    j++;
                    retVal.Add(cria);


                }


            }
            qReader.Close();

            return retVal;
        }

        public int NumeroCriasMae(string maeId)
        {
            LightBaseCommand command;

            command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strMaeId = @strMaeId or (strReceptoraId = @strMaeId and vfFiv = @vfFiv) order by dtDataNascimento");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            ((LightBaseParameter)command.Parameters["vfFiv"]).Value = true;


            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            int i = 0;

            while (qReader.Read())
            {
                bool fiv = false;
                string receptora = null;

                if (qReader["vfFiv"] != DBNull.Value) fiv = (bool)qReader["vfFiv"];
                if (qReader["strReceptoraId"] != DBNull.Value) receptora = (string)qReader["strReceptoraId"];

                if (!fiv || (fiv && receptora == maeId))
                {
                    i++;
                }

            }
            qReader.Close();

            return i;
        }


        public int NumeroCriasMae(string maeId, DateTime dataInicio, DateTime dataFim)
        {
            LightBaseCommand command;

            command = new LightBaseCommand(@"select count(*) from FCARNAUBA_ANIMAIS where strMaeId = @strMaeId and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim  order by dtDataNascimento");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            ((LightBaseParameter)command.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command.Parameters["dtDataFim"]).Value = dataFim;

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant;
        }

        public List<RCria> GetCriasPai(RMachoFemea machoFemea)
        {
            LightBaseCommand command;
            List<RCria> retVal = new List<RCria>();

            command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strPaiId = @strPaiId order by dtDataNascimento");
            ((LightBaseParameter)command.Parameters["strPaiId"]).Value = machoFemea.Id;

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            int i = 1;
            int numeroCrias = qReader.Count;
            DateTime[] datasNascCrias = new DateTime[numeroCrias];
            while (qReader.Read())
            {

                RCria cria = new RCria();
                cria.NCria = i + "ª";
                if (qReader["strId"] != DBNull.Value) cria.Id = Convert.ToInt32(qReader["strId"]);
                if (qReader["strSexo"] != DBNull.Value) cria.Sexo = (string)qReader["strSexo"];
                if (qReader["dtDataNascimento"] != DBNull.Value) cria.DataNascimento = (DateTime)qReader["dtDataNascimento"];

                datasNascCrias[i - 1] = cria.DataNascimento;

                if (qReader["decPn"] != DBNull.Value) cria.Pn = (double)qReader["decPn"];

                if (qReader["strPaiId"] != DBNull.Value)
                {
                    cria.PaiId = (string)qReader["strPaiId"];
                    var pai = GetPaiMae(cria.PaiId);
                    cria.NomePai = pai.Nome;
                    cria.NomeCompletoPai = pai.NomeCompleto;
                    cria.RgdPai = pai.Rgd;

                }

                if (qReader["strMaeId"] != DBNull.Value)
                {
                    cria.MaeId = (string)qReader["strMaeId"];
                    var mae = GetPaiMae(cria.MaeId);
                    cria.NomeMae = mae.Nome;
                    cria.NomeCompletoMae = mae.NomeCompleto;
                    cria.RgdMae = mae.Rgd;

                }

                if (qReader["strNome"] != DBNull.Value) cria.Nome = (string)qReader["strNome"];
                if (qReader["strRgd"] != DBNull.Value) cria.Rgd = (string)qReader["strRgd"];



                if (cria.Sexo == "Fêmea")
                {
                    cria.ErMedio = 0;
                    cria.KgIepMedio = 0;
                    cria.IppIepMedio = 0;
                }
                else
                {
                    cria.IppIepMedio = 0;
                    cria.ErMedio = 0;
                    cria.KgIepMedio = 0;
                }

                cria.PMedia = 0;
                cria.PMaxima = 0;

                double[] gmds = GetGmds(cria.Id.ToString());

                if (gmds.Length > 0)
                {
                    cria.Gmd = Math.Round(gmds.Average(), 2);
                }
                else
                {

                    cria.Gmd = 0;
                }

                double[] pesos = GetPesos(cria.Id.ToString());

                if (pesos.Length > 0)
                {
                    cria.PInicial = Math.Round(pesos.Min(), 2);
                    cria.PFinal = Math.Round(pesos.Max(), 2);
                }
                else
                {

                    cria.PInicial = 0;
                    cria.PFinal = 0;
                }

                i++;
                retVal.Add(cria);

            }
            qReader.Close();

            return retVal;
        }


        public List<RCria> GetCriasCria(RCria criaCria)
        {
            LightBaseCommand command;
            List<RCria> retVal = new List<RCria>();

            if (criaCria.Sexo == "Macho")
            {

                command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strPaiId = @strPaiId order by dtDataNascimento");
                ((LightBaseParameter)command.Parameters["strPaiId"]).Value = criaCria.Id;
            }
            else
            {
                command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strMaeId = @strMaeId order by dtDataNascimento");
                ((LightBaseParameter)command.Parameters["strMaeId"]).Value = criaCria.Id;
            }



            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            int i = 1;
            int numeroCrias = qReader.Count;
            DateTime[] datasNascCrias = new DateTime[numeroCrias];
            while (qReader.Read())
            {

                RCria cria = new RCria();
                cria.NCria = i + "ª";
                if (qReader["strSexo"] != DBNull.Value) cria.Sexo = (string)qReader["strSexo"];
                if (qReader["dtDataNascimento"] != DBNull.Value) cria.DataNascimento = (DateTime)qReader["dtDataNascimento"];

                datasNascCrias[i - 1] = cria.DataNascimento;

                if (qReader["decPn"] != DBNull.Value) cria.Pn = (double)qReader["decPn"];
                if (qReader["strPaiId"] != DBNull.Value)
                {
                    cria.PaiId = (string)qReader["strPaiId"];
                    cria.NomeCompletoPai = GetPaiMae(cria.PaiId).NomeCompleto;

                }

                if (qReader["strNome"] != DBNull.Value) cria.Nome = (string)qReader["strNome"];
                if (qReader["strRgd"] != DBNull.Value) cria.Rgd = (string)qReader["strRgd"];

                if (cria.NCria == "1ª")
                {
                    cria.IppIep = CalculaIpp(criaCria.DataNascimento, cria.DataNascimento);
                }
                else
                {
                    cria.IppIep = CalculaIep(datasNascCrias[i - 2], datasNascCrias[i - 1]);
                }

                cria.Er = CalculaEr(numeroCrias, CalculaIdadeUltimoParto(criaCria.DataNascimento, cria.DataNascimento));

                cria.KgIep = 0;
                cria.PMedia = 0;
                cria.PMaxima = 0;
                cria.PInicial = 0;
                cria.PFinal = 0;
                i++;
                retVal.Add(cria);

            }
            qReader.Close();

            return retVal;
        }


        public int CalculaIpp(DateTime dataNascMae, DateTime dataNascCria)
        {
            return (int)dataNascCria.Subtract(dataNascMae).TotalDays;
        }

        public int CalculaIep(DateTime dataNascPCria, DateTime dataNascUCria)
        {
            return (int)dataNascUCria.Subtract(dataNascPCria).TotalDays;
        }

        public double CalculaEr(int nPartos, int idadeUltimoParto)
        {
            if (idadeUltimoParto > 0)
            {
                return (nPartos * 465 * 100) / idadeUltimoParto;
            }
            else
            {
                return 0;
            }
        }

        public int CalculaIdadeUltimoParto(DateTime dataNascMae, DateTime dataNascCria)
        {
            return (int)dataNascCria.Subtract(dataNascMae).TotalDays;
        }


        public double CalculaIppIepMedio(RCria criaCria)
        {
            int i = 1;
            var crias = GetCriasCria(criaCria);
            var numeroCrias = crias.Count;
            long totalIppIep = 0;
            double mediaIppIep = 0;

            foreach (RCria cria in crias)
            {
                totalIppIep = totalIppIep + cria.IppIep;
            }

            if (numeroCrias > 0)
            {
                mediaIppIep = totalIppIep / numeroCrias;
            }
            else
            {
                mediaIppIep = 0;
            }

            return mediaIppIep;
        }


        public double CalculaErMedio(RCria cria)
        {
            return 0;
        }

        public double CalculaIppIep(int nPartos, int idadeUltimoParto)
        {
            return 0;
        }

        public List<Animal> GetAnimais()
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS order by strNome");
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetAnimaisComVazio()
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS order by strNome");
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetAnimais(string raca)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strRaca=@strRaca order by strNome");
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            
            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetAnimais(string raca, string nomeOuRgd)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strRaca=@strRaca and ( strNomeCompleto=@strNomeOuRgd or strRgd=@strNomeOuRgd) order by strNome");
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command.Parameters["strNomeOuRgd"]).Value = nomeOuRgd;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }


        public List<Animal> GetPais()
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strSexo=@strSexo order by strNome");
            ((LightBaseParameter)command.Parameters["strSexo"]).Value = "Macho";
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetPais(string raca)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strSexo=@strSexo and strRaca=@strRaca order by strNome");
            ((LightBaseParameter)command.Parameters["strSexo"]).Value = "Macho";
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetPais(string raca, string nomeOuRgd)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strSexo=@strSexo and strRaca=@strRaca and ( strNomeCompleto=@strNomeOuRgd or strRgd=@strNomeOuRgd) order by strNome");
            ((LightBaseParameter)command.Parameters["strSexo"]).Value = "Macho";
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command.Parameters["strNomeOuRgd"]).Value = nomeOuRgd;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public ItemDescFinanceiro[] GetDescricoesFinanceiro()
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_ITEMDESC_FINANCEIRO order by strDescricao");
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var itens = new List<ItemDescFinanceiro>();
            ItemDescFinanceiro itemlVazio = new ItemDescFinanceiro();
            itemlVazio.Id = 0;
            itemlVazio.Descricao = "";
            itemlVazio.IdGrupo = 0;

            itens.Add(itemlVazio);

            while (qReader.Read())
            {
                ItemDescFinanceiro item = new ItemDescFinanceiro();
                item.Id = Convert.ToInt32(qReader["id"].ToString());
                item.Descricao = qReader["strDescricao"].ToString();
                item.IdGrupo = Convert.ToInt32(qReader["FK_intID_Grupo"].ToString());
                itens.Add(item);
            }
            qReader.Close();
            return itens.ToArray();
        }

        public int GetIdGrupoDescricaoFinanceiro(string descricao)
        {
            int idGrupo = 0;
            var command = new LightBaseCommand(@"select FK_intID_Grupo from FCARNAUBA_ITEMDESC_FINANCEIRO where strDescricao=@strDescricao");
            ((LightBaseParameter)command.Parameters["strDescricao"]).Value = descricao;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            if (qReader.Read())
            {

                idGrupo = Convert.ToInt32(qReader["FK_intID_Grupo"].ToString());

            }
            qReader.Close();
            return idGrupo;
        }

        public string GetDescricaoIdGrupo(int idGrupo)
        {
            string descricao = "";
            var command = new LightBaseCommand(@"select strDescricao_Grupo from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo=@kintID_Grupo");
            ((LightBaseParameter)command.Parameters["kintID_Grupo"]).Value = idGrupo;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            if (qReader.Read())
            {

                descricao = qReader["strDescricao_Grupo"].ToString();

            }
            qReader.Close();
            return descricao;
        }

        public int GetEntradaDesembolsoIdGrupo(int idGrupo)
        {
            int entradaDesembolso = 0;
            var command = new LightBaseCommand(@"select intEntradaDesembolso from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo=@kintID_Grupo");
            ((LightBaseParameter)command.Parameters["kintID_Grupo"]).Value = idGrupo;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            if (qReader.Read())
            {

                entradaDesembolso = Convert.ToInt32(qReader["intEntradaDesembolso"]);

            }
            qReader.Close();
            return entradaDesembolso;
        }

        public List<Animal> GetMaes()
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strSexo=@strSexo order by strNome");
            ((LightBaseParameter)command.Parameters["strSexo"]).Value = "Fêmea";
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetMaes(string raca)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strSexo=@strSexo and strRaca=@strRaca order by strNome");
            ((LightBaseParameter)command.Parameters["strSexo"]).Value = "Fêmea";
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetMaes(string raca, string nomeOuRgd)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strSexo=@strSexo and strRaca=@strRaca and ( strNomeCompleto=@strNomeOuRgd or strRgd=@strNomeOuRgd) order by strNome");
            ((LightBaseParameter)command.Parameters["strSexo"]).Value = "Fêmea";
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command.Parameters["strNomeOuRgd"]).Value = nomeOuRgd;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public long GetIpp(string maeId, DateTime dataNascimento)
        {
            LightBaseCommand command;
            long ipp = 0;

            command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strMaeId = @strMaeId or (strReceptoraId = @strReceptoraId and vfFiv = @vfFiv) order by dtDataNascimento");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            ((LightBaseParameter)command.Parameters["strReceptoraId"]).Value = maeId;
            ((LightBaseParameter)command.Parameters["vfFiv"]).Value = true;

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {
                bool fiv = false;
                string receptora = null;

                if (qReader["vfFiv"] != DBNull.Value) fiv = (bool)qReader["vfFiv"];
                if (qReader["strReceptoraId"] != DBNull.Value) receptora = (string)qReader["strReceptoraId"];

                if (!fiv || (fiv && receptora == maeId))
                {

                    RCria cria = new RCria();
                    cria.NCria = "1ª";
                    if (qReader["strId"] != DBNull.Value) cria.Id = Convert.ToInt32(qReader["strId"]);
                    if (qReader["strSexo"] != DBNull.Value) cria.Sexo = (string)qReader["strSexo"];
                    if (qReader["dtDataNascimento"] != DBNull.Value) cria.DataNascimento = (DateTime)qReader["dtDataNascimento"];


                    ipp = CalculaIpp(dataNascimento, cria.DataNascimento);

                    break;

                }


            }
            qReader.Close();

            return ipp;
        }

        public long GetIep(string maeId)
        {
            long iep = 0;
            LightBaseCommand command;


            command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strMaeId = @strMaeId or (strReceptoraId = @strMaeId and vfFiv = @vfFiv) order by dtDataNascimento");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            ((LightBaseParameter)command.Parameters["vfFiv"]).Value = true;

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            int i = 1;
            int numeroCrias = qReader.Count;
            DateTime[] datasNascCrias = new DateTime[numeroCrias];
            while (qReader.Read())
            {
                bool fiv = false;
                string receptora = null;

                if (qReader["vfFiv"] != DBNull.Value) fiv = (bool)qReader["vfFiv"];
                if (qReader["strReceptoraId"] != DBNull.Value) receptora = (string)qReader["strReceptoraId"];

                if (!fiv || (fiv && receptora == maeId))
                {

                    RCria cria = new RCria();
                    cria.NCria = i + "ª";
                    if (qReader["strId"] != DBNull.Value) cria.Id = Convert.ToInt32(qReader["strId"]);
                    if (qReader["strSexo"] != DBNull.Value) cria.Sexo = (string)qReader["strSexo"];
                    if (qReader["dtDataNascimento"] != DBNull.Value) cria.DataNascimento = (DateTime)qReader["dtDataNascimento"];

                    datasNascCrias[i - 1] = cria.DataNascimento;

                    i++;
                }
            }

            if (i >= 3)
                iep = CalculaIep(datasNascCrias[i - 3], datasNascCrias[i - 2]);

            qReader.Close();



            return iep;
        }

        public double GetEr(string maeId, DateTime dataNascimento)
        {
            LightBaseCommand command;
            double er = 0;

            command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strMaeId = @strMaeId or (strReceptoraId = @strMaeId and vfFiv = @vfFiv) order by dtDataNascimento");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            ((LightBaseParameter)command.Parameters["vfFiv"]).Value = true;

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            int numeroCrias = NumeroCriasMae(maeId);

            while (qReader.Read())
            {
                bool fiv = false;
                string receptora = null;

                if (qReader["vfFiv"] != DBNull.Value) fiv = (bool)qReader["vfFiv"];
                if (qReader["strReceptoraId"] != DBNull.Value) receptora = (string)qReader["strReceptoraId"];

                if (!fiv || (fiv && receptora == maeId))
                {

                    RCria cria = new RCria();
                    if (qReader["strId"] != DBNull.Value) cria.Id = Convert.ToInt32(qReader["strId"]);
                    if (qReader["strSexo"] != DBNull.Value) cria.Sexo = (string)qReader["strSexo"];
                    if (qReader["dtDataNascimento"] != DBNull.Value) cria.DataNascimento = (DateTime)qReader["dtDataNascimento"];


                    cria.Er = CalculaEr(numeroCrias, CalculaIdadeUltimoParto(dataNascimento, cria.DataNascimento));
                    er = cria.Er;
                }

            }
            qReader.Close();

            return er;
        }

        public List<RRankingIppIepEr> ConsultaRankingIpp(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            filter = AddParametro(filter, "strSexo", "Fêmea");


            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsIpp = new List<RRankingIppIepEr>();

            while (qReader.Read())
            {

                var retVal = new RRankingIppIepEr();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();




                retVal.Ipp = GetIpp(retVal.Id.ToString(), retVal.DataNascimento);


                rankingsIpp.Add(retVal);


            }
            qReader.Close();

            var orderRankingsIpp = rankingsIpp.OrderByDescending(s => s.Ipp);

            return orderRankingsIpp.ToList();
        }


        public List<RRankingIppIepEr> ConsultaRankingIep(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            filter = AddParametro(filter, "strSexo", "Fêmea");


            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsIep = new List<RRankingIppIepEr>();

            while (qReader.Read())
            {

                var retVal = new RRankingIppIepEr();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                retVal.Iep = GetIep(retVal.Id.ToString());


                rankingsIep.Add(retVal);


            }
            qReader.Close();

            var orderRankingsIep = rankingsIep.OrderByDescending(s => s.Iep);

            return orderRankingsIep.ToList();
        }

        public List<RRankingIppIepEr> ConsultaRankingAcumuladaTotal(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");


            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            filter = AddParametro(filter, "strSexo", "Fêmea");


            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsAcumuladaTotal = new List<RRankingIppIepEr>();

            while (qReader.Read())
            {

                var retVal = new RRankingIppIepEr();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["strNome"] != DBNull.Value) retVal.Nome = qReader["strNome"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                var producao = GetProducaoAcumulada(retVal.Id.ToString());

                retVal.AcumuladaTotal = producao.Acumulada;

                rankingsAcumuladaTotal.Add(retVal);


            }
            qReader.Close();

            var orderRankingsAcumuladaTotal = rankingsAcumuladaTotal.OrderByDescending(s => s.AcumuladaTotal);

            return orderRankingsAcumuladaTotal.ToList();
        }

        public List<RRankingIppIepEr> ConsultaRankingKgIep(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            filter = AddParametro(filter, "strSexo", "Fêmea");


            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsIep = new List<RRankingIppIepEr>();

            while (qReader.Read())
            {

                var retVal = new RRankingIppIepEr();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                retVal.Iep = GetIep(retVal.Id.ToString());

                var producao = GetProducaoAcumulada(retVal.Id.ToString());

                if (producao != null && retVal.Iep > 0)
                {

                    retVal.KgIep = producao.Acumulada / retVal.Iep;
                }
                else
                {
                    retVal.KgIep = 0;
                }


                rankingsIep.Add(retVal);


            }
            qReader.Close();

            var orderRankingsKgIep = rankingsIep.OrderByDescending(s => s.KgIep);

            return orderRankingsKgIep.ToList();
        }

        public List<RRankingIppIepEr> ConsultaRankingEr(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", parametrosBuscaEmAnimais.Sexo);


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            filter = AddParametro(filter, "strSexo", "Fêmea");


            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsEr = new List<RRankingIppIepEr>();

            while (qReader.Read())
            {

                var retVal = new RRankingIppIepEr();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["dtDataNascimento"] != DBNull.Value) retVal.DataNascimento = (DateTime)qReader["dtDataNascimento"];
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                retVal.Er = GetEr(retVal.Id.ToString(), retVal.DataNascimento);


                rankingsEr.Add(retVal);


            }
            qReader.Close();

            var orderRankingsEr = rankingsEr.OrderByDescending(s => s.Er);

            return orderRankingsEr.ToList();
        }

        public double GetProducaoMedia(string criaId)
        {
            string filter = "";

            filter = AddParametro(filter, "FK_PL_strIdCria", criaId);


            var command = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisLactacaoAno = new List<RAnimaisLactacaoAno>();
            int pesagens = 0;
            double total = 0;
            double acumulada = 0;
            double media = 0;
            while (qReader.Read())
            {

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                    {
                        total = (double)rowProducoesLeite["PL_decTotal"];
                        acumulada = acumulada + total;
                    }
                    pesagens++;
                }


            }
            qReader.Close();

            if (pesagens > 0)
            {
                media = acumulada / pesagens;
            }
            else
            {
                media = 0;
            }

            return media;
        }

        public double GetProducaoMaxima(string criaId)
        {
            string filter = "";

            filter = AddParametro(filter, "FK_PL_strIdCria", criaId);


            var command = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisLactacaoAno = new List<RAnimaisLactacaoAno>();

            double atual = 0;
            double max = 0;

            while (qReader.Read())
            {

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                    {
                        atual = (double)rowProducoesLeite["PL_decTotal"];

                        if (atual > max)
                        {
                            max = atual;
                        }

                    }

                }
            }
            qReader.Close();


            return max;
        }

        public double GetProducaoMediaMatriz(string matrizId, int ano, string idFazenda, string raca)
        {
            string filter = "";

            DateTime dataInicio = new DateTime(ano, 01, 01);
            DateTime dataFim = new DateTime(ano, 12, 31);

            filter = AddParametro(filter, "FK_PL_strIdMatriz", matrizId);
            filter = AddParametro(filter, "strIdPropriedade", idFazenda);
            filter = AddParametro(filter, "strRaca", raca);
            filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
            filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

            var command = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisLactacaoAno = new List<RAnimaisLactacaoAno>();
            int pesagens = 0;
            double total = 0;
            double acumulada = 0;
            double media = 0;
            while (qReader.Read())
            {

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                    {
                        total = (double)rowProducoesLeite["PL_decTotal"];
                        acumulada = acumulada + total;
                    }
                    pesagens++;
                }


            }
            qReader.Close();

            if (pesagens > 0)
            {
                media = acumulada / pesagens;
            }
            else
            {
                media = 0;
            }

            return media;
        }

        public double GetGorduraMediaMatriz(string matrizId, int ano, string idFazenda, string raca)
        {
            string filter = "";

            DateTime dataInicio = new DateTime(ano, 01, 01);
            DateTime dataFim = new DateTime(ano, 12, 31);

            filter = AddParametro(filter, "FK_PL_strIdMatriz", matrizId);
            filter = AddParametro(filter, "strIdPropriedade", idFazenda);
            filter = AddParametro(filter, "strRaca", raca);
            filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
            filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

            var command = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisLactacaoAno = new List<RAnimaisLactacaoAno>();
            int pesagens = 0;
            double gordura1 = 0;
            double gordura2 = 0;
            double gordura3 = 0;
            double gordMediaTotal = 0;
            double acumulada = 0;
            double media = 0;
            while (qReader.Read())
            {

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["PL_decGord1Ordenha"] != DBNull.Value)
                    {
                        gordura1 = (double)rowProducoesLeite["PL_decGord1Ordenha"];
                    }

                    if (rowProducoesLeite["PL_decGord2Ordenha"] != DBNull.Value)
                    {
                        gordura2 = (double)rowProducoesLeite["PL_decGord2Ordenha"];
                    }

                    if (rowProducoesLeite["PL_decGord3Ordenha"] != DBNull.Value)
                    {
                        gordura3 = (double)rowProducoesLeite["PL_decGord3Ordenha"];
                    }

                    gordMediaTotal = (gordura1 + gordura2 + gordura3) / 3;

                    acumulada = acumulada + gordMediaTotal;

                    pesagens++;
                }


            }
            qReader.Close();

            if (pesagens > 0)
            {
                media = acumulada / pesagens;
            }
            else
            {
                media = 0;
            }

            return media;
        }

        public double GetProteinaMediaMatriz(string matrizId, int ano, string idFazenda, string raca)
        {
            string filter = "";

            DateTime dataInicio = new DateTime(ano, 01, 01);
            DateTime dataFim = new DateTime(ano, 12, 31);

            filter = AddParametro(filter, "FK_PL_strIdMatriz", matrizId);
            filter = AddParametro(filter, "strIdPropriedade", idFazenda);
            filter = AddParametro(filter, "strRaca", raca);
            filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
            filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

            var command = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisLactacaoAno = new List<RAnimaisLactacaoAno>();
            int pesagens = 0;
            double proteina1 = 0;
            double proteina2 = 0;
            double proteina3 = 0;
            double protMediaTotal = 0;
            double acumulada = 0;
            double media = 0;
            while (qReader.Read())
            {

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["PL_decProt1Ordenha"] != DBNull.Value)
                    {
                        proteina1 = (double)rowProducoesLeite["PL_decProt1Ordenha"];
                    }

                    if (rowProducoesLeite["PL_decProt2Ordenha"] != DBNull.Value)
                    {
                        proteina2 = (double)rowProducoesLeite["PL_decProt2Ordenha"];
                    }

                    if (rowProducoesLeite["PL_decProt3Ordenha"] != DBNull.Value)
                    {
                        proteina3 = (double)rowProducoesLeite["PL_decProt3Ordenha"];
                    }

                    protMediaTotal = (proteina1 + proteina2 + proteina3) / 3;

                    acumulada = acumulada + protMediaTotal;

                    pesagens++;
                }


            }
            qReader.Close();

            if (pesagens > 0)
            {
                media = acumulada / pesagens;
            }
            else
            {
                media = 0;
            }

            return media;
        }

        public ProducaoLeite GetGordProtMediaMatriz(string matrizId, int ano, string idFazenda, string raca)
        {
            string filter = "";

            DateTime dataInicio = new DateTime(ano, 01, 01);
            DateTime dataFim = new DateTime(ano, 12, 31);

            filter = AddParametro(filter, "FK_PL_strIdMatriz", matrizId);
            filter = AddParametro(filter, "strIdPropriedade", idFazenda);
            filter = AddParametro(filter, "strRaca", raca);
            filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
            filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

            var command = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisLactacaoAno = new List<RAnimaisLactacaoAno>();
            int pesagens = 0;
            double gordura1 = 0;
            double gordura2 = 0;
            double gordura3 = 0;
            double gordMediaTotal = 0;
            double gordAcumulada = 0;
            double gordMedia = 0;

            double proteina1 = 0;
            double proteina2 = 0;
            double proteina3 = 0;
            double protMediaTotal = 0;
            double protAcumulada = 0;
            double protMedia = 0;

            while (qReader.Read())
            {

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["PL_decGord1Ordenha"] != DBNull.Value)
                    {
                        gordura1 = (double)rowProducoesLeite["PL_decGord1Ordenha"];

                    }

                    if (rowProducoesLeite["PL_decGord2Ordenha"] != DBNull.Value)
                    {
                        gordura2 = (double)rowProducoesLeite["PL_decGord2Ordenha"];

                    }

                    if (rowProducoesLeite["PL_decGord3Ordenha"] != DBNull.Value)
                    {
                        gordura3 = (double)rowProducoesLeite["PL_decGord3Ordenha"];

                    }

                    gordMediaTotal = (gordura1 + gordura2 + gordura3) / 3;

                    gordAcumulada = gordAcumulada + gordMediaTotal;


                    if (rowProducoesLeite["PL_decProt1Ordenha"] != DBNull.Value)
                    {
                        proteina1 = (double)rowProducoesLeite["PL_decProt1Ordenha"];

                    }

                    if (rowProducoesLeite["PL_decProt2Ordenha"] != DBNull.Value)
                    {
                        proteina2 = (double)rowProducoesLeite["PL_decProt2Ordenha"];

                    }

                    if (rowProducoesLeite["PL_decProt3Ordenha"] != DBNull.Value)
                    {
                        proteina3 = (double)rowProducoesLeite["PL_decProt3Ordenha"];

                    }

                    protMediaTotal = (proteina1 + proteina2 + proteina3) / 3;

                    protAcumulada = protAcumulada + protMediaTotal;


                    pesagens++;
                }


            }
            qReader.Close();

            if (pesagens > 0)
            {
                gordMedia = gordAcumulada / pesagens;
                protMedia = protAcumulada / pesagens;
            }
            else
            {
                gordMedia = 0;
                protMedia = 0;
            }

            var prod = new ProducaoLeite();
            prod.GordMedia = gordMedia;
            prod.ProtMedia = protMedia;

            return prod;
        }

        public double GetProducaoMaximaMatriz(string matrizId, int ano, string idFazenda, string raca)
        {
            string filter = "";

            DateTime dataInicio = new DateTime(ano, 01, 01);
            DateTime dataFim = new DateTime(ano, 12, 31);

            filter = AddParametro(filter, "FK_PL_strIdMatriz", matrizId);
            filter = AddParametro(filter, "strIdPropriedade", idFazenda);
            filter = AddParametro(filter, "strRaca", raca);
            filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
            filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

            var command = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var animaisLactacaoAno = new List<RAnimaisLactacaoAno>();

            double atual = 0;
            double max = 0;

            while (qReader.Read())
            {

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                    {
                        atual = (double)rowProducoesLeite["PL_decTotal"];

                        if (atual > max)
                        {
                            max = atual;
                        }

                    }

                }
            }
            qReader.Close();


            return max;
        }

        public List<Noh> GetListPais(string strPaiId)
        {
            List<Noh> ListPais = new List<Noh>();
            var command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strId = @strId");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strId"]).Value = strPaiId;
            var qReader = command.ExecuteReader();

            if (qReader.Read())
            {
                if (qReader["strPaiId"] != DBNull.Value)
                {
                    Noh noh = new Noh();
                    noh.StrId = Convert.ToString(qReader["strPaiId"]);
                    noh.CaminhoPai = noh.StrId;
                    noh.IdPai = GetIdPai(noh.StrId);
                    noh.IdMae = GetIdMae(noh.StrId);

                    ListPais.Add(noh);
                }


            }

            return ListPais;
        }

        public List<Noh> GetListMaes(string strMaeId)
        {
            List<Noh> ListMaes = new List<Noh>();
            var command = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strId = @strId");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strId"]).Value = strMaeId;
            var qReader = command.ExecuteReader();

            if (qReader.Read())
            {


                if (qReader["strMaeId"] != DBNull.Value)
                {
                    Noh noh = new Noh();
                    noh.StrId = Convert.ToString(qReader["strMaeId"]);
                    noh.CaminhoMae = noh.StrId;
                    noh.IdPai = GetIdPai(noh.StrId);
                    noh.IdMae = GetIdMae(noh.StrId);

                    ListMaes.Add(noh);

                }
            }

            return ListMaes;
        }

        public string GetIdPai(string id)
        {
            string pai = null;
            var command = new LightBaseCommand(@"select strPaiId from FCARNAUBA_ANIMAIS where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["strPaiId"] != DBNull.Value) pai = (string)qReader["strPaiId"];
            }
            qReader.Close();
            return pai;
        }

        public string GetIdMae(string id)
        {
            string pai = null;
            var command = new LightBaseCommand(@"select strMaeId from FCARNAUBA_ANIMAIS where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["strMaeId"] != DBNull.Value) pai = (string)qReader["strMaeId"];
            }
            qReader.Close();
            return pai;
        }

        public string GetIdPropriedade(string nomePropriedade)
        {
            string idPropriedade = null;
            var command = new LightBaseCommand(@"select id from FCARNAUBA_PROPRIEDADE where strNome = @strNome");
            ((LightBaseParameter)command.Parameters["strNome"]).Value = nomePropriedade;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["id"] != DBNull.Value) idPropriedade = Convert.ToString(qReader["id"]);
            }
            qReader.Close();
            return idPropriedade;
        }

        public List<RPluviometria> GetPluviometrias(int ano, string propriedade)
        {
            DateTime dataInicial = new DateTime(ano, 1, 1);
            DateTime dataFinal = new DateTime(ano, 12, 31);
            string filter = "";
            filter = AddParametro(filter, "diretorio", propriedade);
            filter = AddParametroData(filter, "dtData", dataInicial.ToShortDateString(), ">=");
            filter = AddParametroData(filter, "dtData", dataFinal.ToShortDateString(), "<=");

            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + filter);
            List<RPluviometria> ListPluviometrias = new List<RPluviometria>();

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {

                RPluviometria pluviometria = new RPluviometria();
                var dia = Convert.ToDateTime(qReader["dtData"]).Day;
                var mes = Convert.ToDateTime(qReader["dtData"]).Month;

                var culturePt = "pt-BR";
                var mesPt = CultureInfo.CreateSpecificCulture(culturePt).DateTimeFormat.GetAbbreviatedMonthName(mes);

                var pluv = Convert.ToDouble(qReader["decPluviometria"]);
                var pluviometro = Convert.ToString(qReader["strPluviometro"]);
                if (qReader["dtData"] != DBNull.Value) pluviometria.Data = (DateTime)qReader["dtData"];
                pluviometria.Dia = dia;
                pluviometria.Mes = mes;
                pluviometria.Ano = ano;
                pluviometria.Propriedade = propriedade;
                pluviometria.SMes = mesPt.ToString();

                if (QuantidadeDeMedicoesPluviometricas(pluviometria.Data, pluviometria.Propriedade) <= 1)
                {
                    pluviometria.Pluviometria = pluv;
                }
                else
                {
                    pluviometria.Pluviometria = GetMediaPluviometria(pluviometria.Data, pluviometria.Propriedade);
                }


                pluviometria.Pluviometro = pluviometro;

                ListPluviometrias.Add(pluviometria);

            }

            var DistinctItems = new List<RPluviometria>(ListPluviometrias.GroupBy(x => x.Data).Select(y => y.First()));

            return DistinctItems;
        }

        public List<RPluviometria> GetPluviometriasDet(int ano, string propriedade)
        {
            DateTime dataInicial = new DateTime(ano, 1, 1);
            DateTime dataFinal = new DateTime(ano, 12, 31);
            string filter = "";
            filter = AddParametro(filter, "diretorio", propriedade);
            filter = AddParametroData(filter, "dtData", dataInicial.ToShortDateString(), ">=");
            filter = AddParametroData(filter, "dtData", dataFinal.ToShortDateString(), "<=");

            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + filter);
            List<RPluviometria> ListPluviometrias = new List<RPluviometria>();

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {

                RPluviometria pluviometria = new RPluviometria();
                var dia = Convert.ToDateTime(qReader["dtData"]).Day;
                var mes = Convert.ToDateTime(qReader["dtData"]).Month;

                var culturePt = "pt-BR";
                var mesPt = CultureInfo.CreateSpecificCulture(culturePt).DateTimeFormat.GetAbbreviatedMonthName(mes);

                var pluv = Convert.ToDouble(qReader["decPluviometria"]);
                var pluviometro = Convert.ToString(qReader["strPluviometro"]);
                if (qReader["dtData"] != DBNull.Value) pluviometria.Data = (DateTime)qReader["dtData"];
                pluviometria.Dia = dia;
                pluviometria.Mes = mes;
                pluviometria.Ano = ano;
                pluviometria.Propriedade = propriedade;
                pluviometria.SMes = mesPt.ToString();

                if (QuantidadeDeMedicoesPluviometricas(pluviometria.Data, pluviometria.Propriedade) <= 1)
                {
                    pluviometria.Pluviometria = pluv;
                }
                else
                {
                    pluviometria.Pluviometria = GetMediaPluviometria(pluviometria.Data, pluviometria.Propriedade);
                }


                pluviometria.Pluviometro = pluviometro;

                pluviometria.Dias = 10;

                ListPluviometrias.Add(pluviometria);

            }

            var DistinctItems = new List<RPluviometria>(ListPluviometrias.GroupBy(x => x.Data).Select(y => y.First()));

            return DistinctItems;
        }

        public List<RPluviometria> GetPluviometriasDet(DateTime dataInicial, DateTime dataFinal, string propriedade)
        {
            string filter = "";
            filter = AddParametro(filter, "diretorio", propriedade);
            filter = AddParametroData(filter, "dtData", dataInicial.ToShortDateString(), ">=");
            filter = AddParametroData(filter, "dtData", dataFinal.ToShortDateString(), "<=");

            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + filter);
            List<RPluviometria> ListPluviometrias = new List<RPluviometria>();

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {

                RPluviometria pluviometria = new RPluviometria();
                var dia = Convert.ToDateTime(qReader["dtData"]).Day;
                var mes = Convert.ToDateTime(qReader["dtData"]).Month;
                var ano = Convert.ToDateTime(qReader["dtData"]).Year;

                var culturePt = "pt-BR";
                var mesPt = CultureInfo.CreateSpecificCulture(culturePt).DateTimeFormat.GetAbbreviatedMonthName(mes);

                var pluv = Convert.ToDouble(qReader["decPluviometria"]);
                var pluviometro = Convert.ToString(qReader["strPluviometro"]);
                if (qReader["dtData"] != DBNull.Value) pluviometria.Data = (DateTime)qReader["dtData"];
                pluviometria.Dia = dia;
                pluviometria.Mes = mes;
                pluviometria.Ano = ano;
                pluviometria.Propriedade = propriedade;
                pluviometria.SMes = mesPt.ToString();

                if (QuantidadeDeMedicoesPluviometricas(pluviometria.Data, pluviometria.Propriedade) <= 1)
                {
                    pluviometria.Pluviometria = pluv;
                }
                else
                {
                    pluviometria.Pluviometria = GetMediaPluviometria(pluviometria.Data, pluviometria.Propriedade);
                }


                pluviometria.Pluviometro = pluviometro;

                pluviometria.Dias = 10;

                ListPluviometrias.Add(pluviometria);

            }

            var DistinctItems = new List<RPluviometria>(ListPluviometrias.GroupBy(x => x.Data).Select(y => y.First()));

            return DistinctItems;
        }

        public List<RPluviometria> GetPluviometrias(DateTime dataInicial, DateTime dataFinal, string propriedade)
        {
            string filter = "";
            filter = AddParametro(filter, "diretorio", propriedade);
            filter = AddParametroData(filter, "dtData", dataInicial.ToShortDateString(), ">=");
            filter = AddParametroData(filter, "dtData", dataFinal.ToShortDateString(), "<=");

            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + filter);
            List<RPluviometria> ListPluviometrias = new List<RPluviometria>();

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {

                RPluviometria pluviometria = new RPluviometria();
                var dia = Convert.ToDateTime(qReader["dtData"]).Day;
                var mes = Convert.ToDateTime(qReader["dtData"]).Month;

                var culturePt = "pt-BR";
                var mesPt = CultureInfo.CreateSpecificCulture(culturePt).DateTimeFormat.GetAbbreviatedMonthName(mes);

                var pluv = Convert.ToDouble(qReader["decPluviometria"]);
                var pluviometro = Convert.ToString(qReader["strPluviometro"]);
                if (qReader["dtData"] != DBNull.Value) pluviometria.Data = (DateTime)qReader["dtData"];
                pluviometria.Dia = dia;
                pluviometria.Mes = mes;
                pluviometria.Ano = dataInicial.Year;
                pluviometria.Propriedade = propriedade;
                pluviometria.SMes = mesPt.ToString();

                if (QuantidadeDeMedicoesPluviometricas(pluviometria.Data, pluviometria.Propriedade) <= 1)
                {
                    pluviometria.Pluviometria = pluv;
                }
                else
                {
                    pluviometria.Pluviometria = GetMediaPluviometria(pluviometria.Data, pluviometria.Propriedade);
                }


                pluviometria.Pluviometro = pluviometro;

                ListPluviometrias.Add(pluviometria);

            }

            var DistinctItems = new List<RPluviometria>(ListPluviometrias.GroupBy(x => x.Data).Select(y => y.First()));

            return DistinctItems;
        }


        public List<RPluviometria> GetPluviometrias2(string propriedade)
        {
            string filter = "";
            filter = AddParametro(filter, "diretorio", propriedade);

            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + filter);
            List<RPluviometria> ListPluviometrias = new List<RPluviometria>();

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {

                RPluviometria pluviometria = new RPluviometria();

                var culturePt = "pt-BR";

                var pluv = Convert.ToDouble(qReader["decPluviometria"]);
                var pluviometro = Convert.ToString(qReader["strPluviometro"]);
                if (qReader["dtData"] != DBNull.Value)
                {
                    pluviometria.Data = (DateTime)qReader["dtData"];
                    pluviometria.Ano = pluviometria.Data.Year;
                }
                pluviometria.Propriedade = propriedade;

                if (QuantidadeDeMedicoesPluviometricas(pluviometria.Data, pluviometria.Propriedade) <= 1)
                {
                    pluviometria.Pluviometria = pluv;
                }
                else
                {
                    pluviometria.Pluviometria = GetMediaPluviometria(pluviometria.Data, pluviometria.Propriedade);
                }


                pluviometria.Pluviometro = pluviometro;

                ListPluviometrias.Add(pluviometria);

            }

            var DistinctItems = new List<RPluviometria>(ListPluviometrias.GroupBy(x => x.Data).Select(y => y.First()));

            var orderDistinctItems = DistinctItems.OrderBy(s => s.Ano);

            return orderDistinctItems.ToList();
        }

        public List<RPluviometria> GetPluviometrias(string propriedade)
        {
            int anoAnterior = 0;
            int anoAtual = 0;
            int DiasCChuva = 0;
            int DiasME5 = 0;
            int DiasE510 = 0;
            int DiasMA10 = 0;
            double pluviometria;
            double anual = 0;
            string filter = "";
            filter = AddParametro(filter, "diretorio", propriedade);
            DateTime data = new DateTime();

            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + filter + " order by dtData");
            List<RPluviometria> ListPluviometrias = new List<RPluviometria>();

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {
                if (qReader["dtData"] != DBNull.Value)
                {
                    data = (DateTime)qReader["dtData"];
                    anoAtual = data.Year;
                }

                if ((anoAnterior != anoAtual) && (anoAnterior > 0))
                {

                    RPluviometria rPluviometria = new RPluviometria();

                    rPluviometria.Propriedade = propriedade.Replace("FAZENDA ", "");
                    rPluviometria.Ano = anoAnterior;
                    rPluviometria.DiasCChuva = DiasCChuva;
                    rPluviometria.DiasME5 = DiasME5;
                    rPluviometria.DiasE510 = DiasE510;
                    rPluviometria.DiasMA10 = DiasMA10;
                    rPluviometria.PluviometriaAnual = anual;
                    ListPluviometrias.Add(rPluviometria);
                    DiasCChuva = 0;
                    DiasME5 = 0;
                    DiasE510 = 0;
                    DiasMA10 = 0;
                    anual = 0;
                }

                var pluv = Convert.ToDouble(qReader["decPluviometria"]);



                if (QuantidadeDeMedicoesPluviometricas(data, propriedade) <= 1)
                {
                    pluviometria = pluv;
                }
                else
                {
                    pluviometria = GetMediaPluviometria(data, propriedade);
                }

                if (pluviometria > 0)
                {
                    DiasCChuva++;
                    anual = anual + pluviometria;
                }

                if (pluviometria < 5)
                {
                    DiasME5++;
                }
                else if (pluviometria >= 5 && pluviometria <= 10)
                {
                    DiasE510++;
                }
                else
                {
                    DiasMA10++;
                }

                anoAnterior = anoAtual;


            }

            RPluviometria rPluviometriaF = new RPluviometria();

            rPluviometriaF.Propriedade = propriedade.Replace("FAZENDA ", "");
            rPluviometriaF.Ano = anoAtual;
            rPluviometriaF.DiasCChuva = DiasCChuva;
            rPluviometriaF.DiasME5 = DiasME5;
            rPluviometriaF.DiasE510 = DiasE510;
            rPluviometriaF.DiasMA10 = DiasMA10;
            rPluviometriaF.PluviometriaAnual = anual;
            ListPluviometrias.Add(rPluviometriaF);

            return ListPluviometrias;
        }

        public List<RPluviometria> GetTodasPluviometrias(DateTime dataInicial, DateTime dataFinal, string propriedade)
        {
            int anoAnterior = 0;
            int anoAtual = 0;
            int DiasCChuva = 0;
            int DiasME5 = 0;
            int DiasE510 = 0;
            int DiasMA10 = 0;
            double pluviometria;
            double anual = 0;
            string filter = "";
            filter = AddParametro(filter, "diretorio", propriedade);
            filter = AddParametroData(filter, "dtData", dataInicial.ToShortDateString(), ">=");
            filter = AddParametroData(filter, "dtData", dataFinal.ToShortDateString(), "<=");
            DateTime data = new DateTime();

            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + filter + " order by dtData");
            List<RPluviometria> ListPluviometrias = new List<RPluviometria>();

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            while (qReader.Read())
            {
                if (qReader["dtData"] != DBNull.Value)
                {
                    data = (DateTime)qReader["dtData"];
                    anoAtual = data.Year;
                }

                if ((anoAnterior != anoAtual) && (anoAnterior > 0))
                {

                    RPluviometria rPluviometria = new RPluviometria();

                    rPluviometria.Propriedade = propriedade.Replace("FAZENDA ", "");
                    rPluviometria.Ano = anoAnterior;
                    rPluviometria.DiasCChuva = DiasCChuva;
                    rPluviometria.DiasME5 = DiasME5;
                    rPluviometria.DiasE510 = DiasE510;
                    rPluviometria.DiasMA10 = DiasMA10;
                    rPluviometria.PluviometriaAnual = anual;
                    if (rPluviometria.Ano > 0)
                        ListPluviometrias.Add(rPluviometria);
                    DiasCChuva = 0;
                    DiasME5 = 0;
                    DiasE510 = 0;
                    DiasMA10 = 0;
                    anual = 0;
                }

                var pluv = Convert.ToDouble(qReader["decPluviometria"]);



                if (QuantidadeDeMedicoesPluviometricas(data, propriedade) <= 1)
                {
                    pluviometria = pluv;
                }
                else
                {
                    pluviometria = GetMediaPluviometria(data, propriedade);
                }

                if (pluviometria > 0)
                {
                    DiasCChuva++;
                    anual = anual + pluviometria;
                }

                if (pluviometria < 5)
                {
                    DiasME5++;
                }
                else if (pluviometria >= 5 && pluviometria <= 10)
                {
                    DiasE510++;
                }
                else
                {
                    DiasMA10++;
                }

                anoAnterior = anoAtual;


            }

            RPluviometria rPluviometriaF = new RPluviometria();

            rPluviometriaF.Propriedade = propriedade.Replace("FAZENDA ", "");
            rPluviometriaF.Ano = anoAtual;
            rPluviometriaF.DiasCChuva = DiasCChuva;
            rPluviometriaF.DiasME5 = DiasME5;
            rPluviometriaF.DiasE510 = DiasE510;
            rPluviometriaF.DiasMA10 = DiasMA10;
            rPluviometriaF.PluviometriaAnual = anual;
            if (rPluviometriaF.Ano > 0)
                ListPluviometrias.Add(rPluviometriaF);

            return ListPluviometrias;
        }

        public List<RPluviometria> GetTodasPluviometrias(DateTime dataInicial, DateTime dataFinal)
        {
            List<RPluviometria> ListPluviometrias = new List<RPluviometria>();
            List<RPluviometria> ListPluviometriasConcat = new List<RPluviometria>();
            var propriedades = new List<Propriedade>();
            propriedades = GetPropriedades();
            propriedades.RemoveAt(0);

            foreach (Propriedade propriedade in propriedades)
            {
                var ListPluviometriasAtual = GetTodasPluviometrias(dataInicial, dataFinal, propriedade.Nome);

                ListPluviometriasConcat = new List<RPluviometria>(ListPluviometrias.Count + ListPluviometriasAtual.Count);
                ListPluviometriasConcat.AddRange(ListPluviometrias);
                ListPluviometriasConcat.AddRange(ListPluviometriasAtual);

                ListPluviometrias = ListPluviometriasConcat;

            }

            return ListPluviometrias;
        }

        public List<Propriedade> GetPropriedades()
        {
            var command = new LightBaseCommand(@"select id, strNome from FCARNAUBA_PROPRIEDADE order by id");

            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var propriedades = new List<Propriedade>();
            Propriedade propriedadeVazio = new Propriedade();
            propriedadeVazio.Id = 0;
            propriedadeVazio.Nome = "";
            propriedades.Add(propriedadeVazio);

            while (qReader.Read())
            {
                Propriedade propriedade = new Propriedade();
                propriedade.Id = Convert.ToInt32(qReader["id"].ToString());
                propriedade.Nome = qReader["strNome"].ToString();
                propriedades.Add(propriedade);
            }
            qReader.Close();
            return propriedades;
        }

        public Propriedade[] ObtemPropriedades()
        {
            var command = new LightBaseCommand(@"select id, strNome from FCARNAUBA_PROPRIEDADE order by id");

            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var propriedades = new List<Propriedade>();
            
            while (qReader.Read())
            {
                Propriedade propriedade = new Propriedade();
                propriedade.Id = Convert.ToInt32(qReader["id"].ToString());
                propriedade.Nome = qReader["strNome"].ToString();
                propriedade.Nome = propriedade.Nome.Replace("FAZENDA ", "");
                propriedades.Add(propriedade);
            }
            qReader.Close();
            return propriedades.ToArray();
        }

        public Propriedade ObtemPropriedade(int idPropriedade)
        {
            var command = new LightBaseCommand(@"select id, strNome from FCARNAUBA_PROPRIEDADE where id=@id");
            ((LightBaseParameter)command.Parameters["id"]).Value = idPropriedade;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            Propriedade propriedade = new Propriedade();

            if (qReader.Read())
            {
                propriedade = new Propriedade();
                propriedade.Id = Convert.ToInt32(qReader["id"].ToString());
                propriedade.Nome = qReader["strNome"].ToString();
                propriedade.Nome = propriedade.Nome.Replace("FAZENDA ", "");
            }
            qReader.Close();
            return propriedade;
        }

        public Propriedade ObtemPropriedadeCompleta(int idPropriedade)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_PROPRIEDADE where id=@id");
            ((LightBaseParameter)command.Parameters["id"]).Value = idPropriedade;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            Propriedade propriedade = new Propriedade();

            if (qReader.Read())
            {
                propriedade = new Propriedade();
                propriedade.Id = Convert.ToInt32(qReader["id"].ToString());
                propriedade.Nome = qReader["strNome"].ToString();
                if (qReader["strLocalizacao"] != DBNull.Value) propriedade.Localizacao = qReader["strLocalizacao"].ToString();
                if (qReader["decArea"] != DBNull.Value) propriedade.Area = (double)qReader["decArea"];
                if (qReader["decAreaUtilizada"] != DBNull.Value) propriedade.AreaUtilizada = (double)qReader["decAreaUtilizada"];
                if (qReader["decAreaPreservacao"] != DBNull.Value) propriedade.AreaPreservacao = (double)qReader["decAreaPreservacao"];
                if (qReader["strMunicipio"] != DBNull.Value) propriedade.Municipio = qReader["strMunicipio"].ToString();
                if (qReader["strUf"] != DBNull.Value) propriedade.Uf = qReader["strUf"].ToString();
            }
            qReader.Close();
            return propriedade;
        }

        public Propriedade[] ObtemPropriedadesComp()
        {
            var command = new LightBaseCommand(@"select id, strNome, strIdsPropriedades from FCARNAUBA_PROPRIEDADE_COMP order by id");

            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var propriedades = new List<Propriedade>();

            while (qReader.Read())
            {
                Propriedade propriedade = new Propriedade();
                propriedade.Id = Convert.ToInt32(qReader["id"].ToString());
                propriedade.Nome = qReader["strNome"].ToString();
                propriedade.IdsPropriedades = qReader["strIdsPropriedades"].ToString();
                propriedades.Add(propriedade);
            }
            qReader.Close();
            return propriedades.ToArray();
        }

        public Propriedade ObtemPropriedadeComp(string idsPropriedadesComp)
        {
            var command = new LightBaseCommand(@"select id, strNome, strIdsPropriedades from FCARNAUBA_PROPRIEDADE_COMP where strIdsPropriedades=@strIdsPropriedades");
            ((LightBaseParameter)command.Parameters["strIdsPropriedades"]).Value = idsPropriedadesComp;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            Propriedade propriedade = new Propriedade();

            if (qReader.Read())
            {
                propriedade = new Propriedade();
                propriedade.Id = Convert.ToInt32(qReader["id"].ToString());
                propriedade.Nome = qReader["strNome"].ToString();
            }
            qReader.Close();
            return propriedade;
        }

        public Empresa ObtemEmpresa(int idEmpresa)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_EMPRESA where id=@id");
            ((LightBaseParameter)command.Parameters["id"]).Value = idEmpresa;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            Empresa empresa = new Empresa();

            if (qReader.Read())
            {
                empresa = new Empresa();
                empresa.IdEmpresa = Convert.ToInt32(qReader["id"].ToString());
                empresa.RazaoSocial = qReader["strRazaoSocial"].ToString();
                empresa.CnpjCpf = qReader["strCnpjCpf"].ToString();
                empresa.Endereco = qReader["strEndereco"].ToString();
                empresa.Municipio = qReader["strMunicipio"].ToString();
                empresa.Uf = qReader["strUf"].ToString();
                empresa.Telefones = qReader["strTelefones"].ToString();
                empresa.Email = qReader["strEmail"].ToString();
            }
            qReader.Close();
            return empresa;
        }

        public List<CentroCusto> GetCentrosCusto()
        {
            var command = new LightBaseCommand(@"select id, strDescricao from FCARNAUBA_CENTRO_CUSTO order by strDescricao");

            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var centrosCusto = new List<CentroCusto>();
            CentroCusto centroCustoVazio = new CentroCusto();
            centroCustoVazio.Id = 0;
            centroCustoVazio.Descricao = "";
            centrosCusto.Add(centroCustoVazio);

            while (qReader.Read())
            {
                CentroCusto centroCusto = new CentroCusto();
                centroCusto.Id = Convert.ToInt32(qReader["id"].ToString());
                centroCusto.Descricao = qReader["strDescricao"].ToString();
                centrosCusto.Add(centroCusto);
            }
            qReader.Close();
            return centrosCusto;
        }


        public List<string> GetSimplesPropriedades()
        {
            return GetSimpleStrTable("FCARNAUBA_PROPRIEDADE", "strNome");
        }

        public List<string> GetSimplesUfs()
        {
            return GetSimpleStrTable("CADUFS", "siglauf");
        }

        private List<string> GetSimpleStrTable(string tablename, string fieldname)
        {
            var command = new LightBaseCommand(@"select * from " + tablename + " order by " + fieldname);
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var retVal = new List<string>();
            while (qReader.Read())
            {
                retVal.Add(qReader[fieldname].ToString());
            }
            qReader.Close();
            return retVal;
        }

        private List<string> GetSimpleStrTableWhere(string tablename, string fieldname, string equals)
        {
            var command =
                new LightBaseCommand(@"select * from " + tablename + " where " + fieldname + " = @equals" + " order by " +
                                     fieldname);
            ((LightBaseParameter)command.Parameters["equals"]).Value = equals;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var retVal = new List<string>();
            while (qReader.Read())
            {
                retVal.Add(qReader[fieldname].ToString());
            }
            qReader.Close();
            return retVal;
        }

        public RLoteControleLeiteiro GetLoteControleLeiteiroData(DateTime dataControleLeiteiro)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where dtDataControle = @dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControleLeiteiro.ToString("dd/MM/yyyy");
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControleLeiteiro();

            if (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strIdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }
                if (qReader["strLoteDataPropriedade"] != DBNull.Value) retVal.LoteDataPropriedade = qReader["strLoteDataPropriedade"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                if (qReader["strCategoria"] != DBNull.Value) retVal.Categoria = qReader["strCategoria"].ToString();
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();
                if (qReader["strControlador"] != DBNull.Value) retVal.Controlador = qReader["str3Ordenha"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducaoLeite in tableProducoesLeite.Rows)
                {
                    RProducaoLeite producaoLeite = DataRowToRProducaoLeite(rowProducaoLeite);
                    retVal.ProducoesLeite.Add(producaoLeite);
                }
            }

            return retVal;
        }


        public List<RProducaoLeite> GetControleProducaoLeiteiroData(long idLote)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = idLote;
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControleLeiteiro();
            var producao = new RProducaoLeite();
            var producoes = new List<RProducaoLeite>();

            if (qReader.Read())
            {
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strIdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducaoLeite in tableProducoesLeite.Rows)
                {
                    RProducaoLeite producaoLeite = DataRowToRProducaoLeite(rowProducaoLeite);

                    //Controle Leiteiro
                    producaoLeite.SLote = retVal.SLote;
                    producaoLeite.Raca = retVal.Raca;
                    producaoLeite.Fazenda = retVal.NomePropriedade;
                    producaoLeite.DataControle = retVal.DataControle;
                    producaoLeite.HoraPOrdenha = retVal.POrdenha;
                    producaoLeite.HoraSOrdenha = retVal.SOrdenha;
                    producaoLeite.HoraTOrdenha = retVal.TOrdenha;

                    if (producaoLeite.POrdenha >= 100)
                    {
                        producaoLeite.ProducaoAnterior = 0;
                        producaoLeite.ProducaoAcumulada = producaoLeite.POrdenha;
                    }
                    else
                    {
                        var prod = GetProducaoAcumulada(retVal.DataControle, producaoLeite.DiasLactacao, idLote.ToString(), producaoLeite.IdMatriz);
                        producaoLeite.ProducaoAnterior = prod.Anterior;
                        producaoLeite.ProducaoAcumulada = prod.Acumulada;
                    }


                    producoes.Add(producaoLeite);
                }
            }

            return producoes;
        }

        public List<RProducaoLeite> GetControleProducaoLeiteiroTotal(DateTime dataInicio, DateTime dataFim, string idPropriedade)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where dtDataControle >= @dtDataInicio and dtDataControle <= @dtDataFim and strIdPropriedade=@strIdPropriedade order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = idPropriedade;
            ((LightBaseParameter)command.Parameters["dtDataInicio"]).Value = dataInicio.ToString("dd/MM/yyyy");
            ((LightBaseParameter)command.Parameters["dtDataFim"]).Value = dataFim.ToString("dd/MM/yyyy");
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControleLeiteiro();
            var producao = new RProducaoLeite();
            var producoes = new List<RProducaoLeite>();

            if (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strIdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducaoLeite in tableProducoesLeite.Rows)
                {
                    RProducaoLeite producaoLeite = DataRowToRProducaoLeite(rowProducaoLeite);

                    if (producaoLeite.SairControle)
                    {

                        //Controle Leiteiro
                        producaoLeite.SLote = retVal.SLote;
                        producaoLeite.Raca = retVal.Raca;
                        producaoLeite.Fazenda = retVal.NomePropriedade;
                        producaoLeite.DataControle = retVal.DataControle;
                        producaoLeite.HoraPOrdenha = retVal.POrdenha;
                        producaoLeite.HoraSOrdenha = retVal.SOrdenha;
                        producaoLeite.HoraTOrdenha = retVal.TOrdenha;

                        if (producaoLeite.POrdenha >= 100)
                        {
                            producaoLeite.ProducaoAnterior = 0;
                            producaoLeite.ProducaoAcumulada = producaoLeite.POrdenha;
                        }
                        else
                        {
                            var prod = GetProducaoAcumulada(retVal.DataControle, producaoLeite.DiasLactacao, retVal.Id.ToString(), producaoLeite.IdMatriz);
                            producaoLeite.ProducaoAnterior = prod.Anterior;
                            producaoLeite.ProducaoAcumulada = prod.Acumulada;
                        }

                        producoes.Add(producaoLeite);
                    }

                }
            }

            return producoes;
        }


        public List<RProducaoLeite> GetControleProducaoLeiteiroDataPes(long idLote)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = idLote;
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControleLeiteiro();
            var producao = new RProducaoLeite();
            var producoes = new List<RProducaoLeite>();

            if (qReader.Read())
            {
                if (qReader["strLote"] != DBNull.Value) retVal.SLote = qReader["strLote"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strIdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducaoLeite in tableProducoesLeite.Rows)
                {
                    RProducaoLeite producaoLeite = DataRowToRProducaoLeite(rowProducaoLeite);

                    //Controle Leiteiro
                    producaoLeite.SLote = retVal.SLote;
                    producaoLeite.Raca = retVal.Raca;
                    producaoLeite.Fazenda = retVal.NomePropriedade;
                    producaoLeite.DataControle = retVal.DataControle;
                    producaoLeite.HoraPOrdenha = retVal.POrdenha;
                    producaoLeite.HoraSOrdenha = retVal.SOrdenha;
                    producaoLeite.HoraTOrdenha = retVal.TOrdenha;

                    if (producaoLeite.POrdenha >= 100)
                    {
                        producaoLeite.ProducaoAnterior = 0;
                        producaoLeite.ProducaoAcumulada = producaoLeite.POrdenha;
                    }
                    else
                    {
                        var prod = GetProducaoAcumulada(retVal.DataControle, producaoLeite.DiasLactacao, idLote.ToString(), producaoLeite.IdMatriz);
                        producaoLeite.ProducaoAnterior = prod.Anterior;
                        producaoLeite.ProducaoAcumulada = prod.Acumulada;
                    }


                    producoes.Add(producaoLeite);
                }
            }



            return producoes;
        }


        private RProducaoLeite DataRowToRProducaoLeite(DataRow dRow)
        {
            RProducaoLeite retVal = new RProducaoLeite();

            if (dRow["FK_PL_strIdMatriz"] != DBNull.Value)
            {
                retVal.IdMatriz = (string)dRow["FK_PL_strIdMatriz"];
                retVal.NomeMatriz = GetAnimalById(retVal.IdMatriz).NomeRg;
                retVal.RgdMatriz = GetAnimalById(retVal.IdMatriz).Rgd;
            }

            if (dRow["PL_intDiasLactacao"] != DBNull.Value)
            {
                retVal.DiasLactacao = (int)dRow["PL_intDiasLactacao"];
                retVal.DiasLactacaoReal = retVal.DiasLactacao;
            }
            if (dRow["PL_decEsgota"] != DBNull.Value) retVal.Esgota = (double)dRow["PL_decEsgota"];
            if (dRow["PL_dec1Ordenha"] != DBNull.Value) retVal.POrdenha = (double)dRow["PL_dec1Ordenha"];
            if (dRow["PL_dec2Ordenha"] != DBNull.Value) retVal.SOrdenha = (double)dRow["PL_dec2Ordenha"];
            if (dRow["PL_dec3Ordenha"] != DBNull.Value) retVal.TOrdenha = (double)dRow["PL_dec3Ordenha"];
            if (dRow["PL_decTotal"] != DBNull.Value) retVal.Total = (double)dRow["PL_decTotal"];
            if (dRow["PL_vfBezerrosPe"] != DBNull.Value) retVal.BezerrosAoPe = (bool)dRow["PL_vfBezerrosPe"];
            if (dRow["PL_intTetosFuncionais"] != DBNull.Value) retVal.TetosFuncionais = (int)dRow["PL_intTetosFuncionais"];
            if (dRow["PL_docObs"] != DBNull.Value) retVal.Obs = (string)dRow["PL_docObs"];
            if (dRow["PL_strRegimeAlimentar"] != DBNull.Value) retVal.RegimeAlimentar = (string)dRow["PL_strRegimeAlimentar"];
            if (dRow["PL_dtDataEntradaControle"] != DBNull.Value) retVal.DataEntradaControle = (DateTime)dRow["PL_dtDataEntradaControle"];
            if (dRow["PL_dtDataSaidaControle"] != DBNull.Value) retVal.DataSaidaControle = (DateTime)dRow["PL_dtDataSaidaControle"];
            if (dRow["FK_PL_strIdCria"] != DBNull.Value) retVal.IdCria = (string)dRow["FK_PL_strIdCria"];
            if (dRow["PL_vfReceptora"] != DBNull.Value) retVal.Receptora = (bool)dRow["PL_vfReceptora"];
            if (dRow["PL_decGord1Ordenha"] != DBNull.Value) retVal.GordPOrdenha = (double)dRow["PL_decGord1Ordenha"];
            if (dRow["PL_decGord2Ordenha"] != DBNull.Value) retVal.GordSOrdenha = (double)dRow["PL_decGord2Ordenha"];
            if (dRow["PL_decGord3Ordenha"] != DBNull.Value) retVal.GordTOrdenha = (double)dRow["PL_decGord3Ordenha"];
            if (dRow["PL_decProt1Ordenha"] != DBNull.Value) retVal.ProtPOrdenha = (double)dRow["PL_decProt1Ordenha"];
            if (dRow["PL_decProt2Ordenha"] != DBNull.Value) retVal.ProtSOrdenha = (double)dRow["PL_decProt2Ordenha"];
            if (dRow["PL_decProt3Ordenha"] != DBNull.Value) retVal.ProtTOrdenha = (double)dRow["PL_decProt3Ordenha"];
            if (dRow["PL_vfSairControle"] != DBNull.Value)
            {
                retVal.SairControle = (bool)dRow["PL_vfSairControle"];
                if (retVal.SairControle)
                {
                    retVal.SairControleStr = "Sim";
                }
                else
                {
                    retVal.SairControleStr = "Não";
                }
            }
            else
            {
                retVal.SairControle = false;
                retVal.SairControleStr = "Não";
            }
            if (dRow["PL_strMotivo"] != DBNull.Value) retVal.Motivo = (string)dRow["PL_strMotivo"];

            return retVal;
        }


        private RProducaoReal DataRowToRProducaoReal(DataRow dRow)
        {
            RProducaoReal retVal = new RProducaoReal();

            if (dRow["FK_PL_strIdMatriz"] != DBNull.Value)
            {
                retVal.IdMatriz = (string)dRow["FK_PL_strIdMatriz"];
            }

            if (dRow["PL_intDiasLactacao"] != DBNull.Value)
            {
                retVal.DiasLactacao = (int)dRow["PL_intDiasLactacao"];
                retVal.DiasLactacaoReal = retVal.DiasLactacao;
            }
            if (dRow["PL_decEsgota"] != DBNull.Value) retVal.Esgota = (double)dRow["PL_decEsgota"];
            if (dRow["PL_dec1Ordenha"] != DBNull.Value) retVal.POrdenha = (double)dRow["PL_dec1Ordenha"];
            if (dRow["PL_dec2Ordenha"] != DBNull.Value) retVal.SOrdenha = (double)dRow["PL_dec2Ordenha"];
            if (dRow["PL_dec3Ordenha"] != DBNull.Value) retVal.TOrdenha = (double)dRow["PL_dec3Ordenha"];
            if (dRow["PL_decTotal"] != DBNull.Value) retVal.Total = (double)dRow["PL_decTotal"];
            if (dRow["PL_vfBezerrosPe"] != DBNull.Value) retVal.BezerrosAoPe = (bool)dRow["PL_vfBezerrosPe"];
            if (dRow["PL_intTetosFuncionais"] != DBNull.Value) retVal.TetosFuncionais = (int)dRow["PL_intTetosFuncionais"];
            if (dRow["PL_docObs"] != DBNull.Value) retVal.Obs = (string)dRow["PL_docObs"];
            if (dRow["PL_strRegimeAlimentar"] != DBNull.Value) retVal.RegimeAlimentar = (string)dRow["PL_strRegimeAlimentar"];
            if (dRow["PL_dtDataEntradaControle"] != DBNull.Value) retVal.DataEntradaControle = (DateTime)dRow["PL_dtDataEntradaControle"];
            if (dRow["PL_dtDataSaidaControle"] != DBNull.Value) retVal.DataSaidaControle = (DateTime)dRow["PL_dtDataSaidaControle"];
            if (dRow["FK_PL_strIdCria"] != DBNull.Value) retVal.IdCria = (string)dRow["FK_PL_strIdCria"];
            if (dRow["PL_vfReceptora"] != DBNull.Value) retVal.Receptora = (bool)dRow["PL_vfReceptora"];
            if (dRow["PL_decGord1Ordenha"] != DBNull.Value) retVal.GordPOrdenha = (double)dRow["PL_decGord1Ordenha"];
            if (dRow["PL_decGord2Ordenha"] != DBNull.Value) retVal.GordSOrdenha = (double)dRow["PL_decGord2Ordenha"];
            if (dRow["PL_decGord3Ordenha"] != DBNull.Value) retVal.GordTOrdenha = (double)dRow["PL_decGord3Ordenha"];
            if (dRow["PL_decProt1Ordenha"] != DBNull.Value) retVal.ProtPOrdenha = (double)dRow["PL_decProt1Ordenha"];
            if (dRow["PL_decProt2Ordenha"] != DBNull.Value) retVal.ProtSOrdenha = (double)dRow["PL_decProt2Ordenha"];
            if (dRow["PL_decProt3Ordenha"] != DBNull.Value) retVal.ProtTOrdenha = (double)dRow["PL_decProt3Ordenha"];
            if (dRow["PL_vfSairControle"] != DBNull.Value) retVal.SairControle = (bool)dRow["PL_vfSairControle"];
            if (dRow["PL_strMotivo"] != DBNull.Value) retVal.Motivo = (string)dRow["PL_strMotivo"];

            retVal.NomeMatriz = GetAnimalById(retVal.IdMatriz).Nome;
            retVal.RgdMatriz = GetAnimalById(retVal.IdMatriz).Rgd;

            return retVal;
        }

        private ProducaoLeite DataRowToProducaoLeite(DataRow dRow)
        {
            ProducaoLeite retVal = new ProducaoLeite();

            if (dRow["FK_PL_strIdMatriz"] != DBNull.Value)
            {
                retVal.IdMatriz = (string)dRow["FK_PL_strIdMatriz"];
            }

            if (dRow["PL_intDiasLactacao"] != DBNull.Value) retVal.DiasLactacao = (int)dRow["PL_intDiasLactacao"];
            if (dRow["PL_decEsgota"] != DBNull.Value) retVal.Esgota = (double)dRow["PL_decEsgota"];
            if (dRow["PL_dec1Ordenha"] != DBNull.Value) retVal.POrdenha = (double)dRow["PL_dec1Ordenha"];
            if (dRow["PL_dec2Ordenha"] != DBNull.Value) retVal.SOrdenha = (double)dRow["PL_dec2Ordenha"];
            if (dRow["PL_dec3Ordenha"] != DBNull.Value) retVal.TOrdenha = (double)dRow["PL_dec3Ordenha"];
            if (dRow["PL_decTotal"] != DBNull.Value) retVal.Total = (double)dRow["PL_decTotal"];
            if (dRow["PL_vfBezerrosPe"] != DBNull.Value) retVal.BezerrosPe = (bool)dRow["PL_vfBezerrosPe"];
            if (dRow["PL_intTetosFuncionais"] != DBNull.Value) retVal.TetosFuncionais = (int)dRow["PL_intTetosFuncionais"];
            if (dRow["PL_docObs"] != DBNull.Value) retVal.Obs = (string)dRow["PL_docObs"];
            if (dRow["PL_strRegimeAlimentar"] != DBNull.Value) retVal.RegimeAlimentar = (string)dRow["PL_strRegimeAlimentar"];
            if (dRow["PL_dtDataEntradaControle"] != DBNull.Value) retVal.DataEntradaControle = (DateTime)dRow["PL_dtDataEntradaControle"];
            if (dRow["PL_dtDataSaidaControle"] != DBNull.Value) retVal.DataSaidaControle = (DateTime)dRow["PL_dtDataSaidaControle"];
            if (dRow["FK_PL_strIdCria"] != DBNull.Value) retVal.IdCria = (string)dRow["FK_PL_strIdCria"];
            if (dRow["PL_vfReceptora"] != DBNull.Value) retVal.Receptora = (bool)dRow["PL_vfReceptora"];
            if (dRow["PL_decGord1Ordenha"] != DBNull.Value) retVal.GordPOrdenha = (double)dRow["PL_decGord1Ordenha"];
            if (dRow["PL_decGord2Ordenha"] != DBNull.Value) retVal.GordSOrdenha = (double)dRow["PL_decGord2Ordenha"];
            if (dRow["PL_decGord3Ordenha"] != DBNull.Value) retVal.GordTOrdenha = (double)dRow["PL_decGord3Ordenha"];
            if (dRow["PL_decProt1Ordenha"] != DBNull.Value) retVal.ProtPOrdenha = (double)dRow["PL_decProt1Ordenha"];
            if (dRow["PL_decProt2Ordenha"] != DBNull.Value) retVal.ProtSOrdenha = (double)dRow["PL_decProt2Ordenha"];
            if (dRow["PL_decProt3Ordenha"] != DBNull.Value) retVal.ProtTOrdenha = (double)dRow["PL_decProt3Ordenha"];
            if (dRow["PL_vfSairControle"] != DBNull.Value) retVal.SairControle = (bool)dRow["PL_vfSairControle"];
            if (dRow["PL_strMotivo"] != DBNull.Value) retVal.Motivo = (string)dRow["PL_strMotivo"];

            retVal.NomeMatriz = GetAnimalById(retVal.IdMatriz).NomeRg;
            if (!String.IsNullOrEmpty(retVal.IdCria))
            {
                retVal.NomeCria = GetAnimalById(retVal.IdCria).NomeRg;
            }

            if (retVal.SairControle)
            {
                retVal.SairControleSr = "Sim";
            }
            else
            {
                retVal.SairControleSr = "Não";
            }

            if (retVal.BezerrosPe)
            {
                retVal.BezerrosPeSr = "Sim";
            }
            else
            {
                retVal.BezerrosPeSr = "Não";
            }

            return retVal;
        }

        private Compra DataRowToCompra(DataRow dRow)
        {
            Compra retVal = new Compra();

            if (dRow["FK_COM_strIdAnimal"] != DBNull.Value)
            {
                retVal.IdAnimal = (string)dRow["FK_COM_strIdAnimal"];
            }

            retVal.NomeAnimal = GetAnimalById(retVal.IdAnimal).NomeCompleto;

            if (dRow["COM_strEvento"] != DBNull.Value) retVal.Evento = (string)dRow["COM_strEvento"];
            if (dRow["COM_docDescricao"] != DBNull.Value) retVal.Descricao = (string)dRow["COM_docDescricao"];
            if (dRow["COM_moeValor"] != DBNull.Value) retVal.Valor = (double)dRow["COM_moeValor"];

            return retVal;
        }

        private Documento DataRowToDocumento(DataRow dRow)
        {
            Documento retVal = new Documento();

            if (dRow["DOC_strDescricao"] != DBNull.Value) retVal.Descricao = (string)dRow["DOC_strDescricao"];
            if (dRow["DOC_dtDataAnexo"] != DBNull.Value) retVal.DataDocumento = (DateTime)dRow["DOC_dtDataAnexo"];

            if (dRow["DOC_strPDFDocumento"] != DBNull.Value)
            {
                string fileNameInicial = dRow["DOC_strPDFDocumento"].ToString();
                var origName = GetNomeArquivoOriginal(fileNameInicial);
                retVal.PDFDocumento = new Arquivo(origName, fileNameInicial, null);
            }

            return retVal;
        }

        private Parcela DataRowToParcela(DataRow dRow)
        {
            Parcela retVal = new Parcela();

            if (dRow["PAR_intNParcela"] != DBNull.Value)
            {
                retVal.NParcela = (int)dRow["PAR_intNParcela"];
            }

            if (dRow["PAR_dtDataParcela"] != DBNull.Value) retVal.Data = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dRow["PAR_dtDataParcela"]));
            if (dRow["PAR_moeValorInicial"] != DBNull.Value) retVal.ValorInicial = (double)dRow["PAR_moeValorInicial"];
            if (dRow["PAR_moeValorPago"] != DBNull.Value) retVal.ValorPago = (double)dRow["PAR_moeValorPago"];
            if (dRow["PAR_dtDataPagamento"] != DBNull.Value)
            {
                retVal.DataPagamento = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dRow["PAR_dtDataPagamento"]));
                retVal.DataPagamentoDt = Convert.ToDateTime(dRow["PAR_dtDataPagamento"]);
            }

            return retVal;
        }

        public Lote GetLote(long id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            Lote lote = new Lote();
            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) lote.Id = Convert.ToInt32(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) lote.SLote = Convert.ToString(qReader["strLote"]);
                if (qReader["strRaca"] != DBNull.Value) lote.Raca = Convert.ToString(qReader["strRaca"]);
                if (qReader["strIdPropriedade"] != DBNull.Value) lote.IdPropriedade = Convert.ToString(qReader["strIdPropriedade"]);
            }
            qReader.Close();
            return lote;
        }

        public List<Lote> GetLotes()
        {
            var command = new LightBaseCommand(@"select id, strLote, dtDataControle, strIdPropriedade from FCARNAUBA_LOTE_CONTROLE_LEITEIRO order by dtDataControle desc");

            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var lotes = new List<Lote>();

            while (qReader.Read())
            {

                Lote lote = new Lote();
                if (qReader["id"] != DBNull.Value) lote.Id = Convert.ToInt32(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) lote.SLote = Convert.ToString(qReader["strLote"]);
                if (qReader["dtDataControle"] != DBNull.Value) lote.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strIdPropriedade"] != DBNull.Value) lote.IdPropriedade = Convert.ToString(qReader["strIdPropriedade"]);
                var nomePropriedade = GetNomePropriedade(lote.IdPropriedade);
                DateTime dataControle = (DateTime)qReader["dtDataControle"];
                lote.LoteDataPropriedade = lote.SLote + " - " + dataControle.ToString("dd/MM/yyyy") + " - " + nomePropriedade;
                lotes.Add(lote);

            }
            qReader.Close();
            return lotes;
        }

        public List<Lote> GetLotesParaPesagens()
        {
            var command = new LightBaseCommand(@"select id, strLote, dtDataControle, strIdPropriedade from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where vfLiberarLotePesagem = @vfLiberarLotePesagem order by dtDataControle desc");
            ((LightBaseParameter)command.Parameters["vfLiberarLotePesagem"]).Value = 1;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var lotes = new List<Lote>();
            Lote loteVazio = new Lote();
            loteVazio.Id = 0;
            loteVazio.LoteDataPropriedade = "Selecione o Lote";
            lotes.Add(loteVazio);
            while (qReader.Read())
            {

                Lote lote = new Lote();
                if (qReader["id"] != DBNull.Value) lote.Id = Convert.ToInt32(qReader["id"]);
                if (qReader["strLote"] != DBNull.Value) lote.SLote = Convert.ToString(qReader["strLote"]);
                if (qReader["dtDataControle"] != DBNull.Value) lote.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["strIdPropriedade"] != DBNull.Value) lote.IdPropriedade = Convert.ToString(qReader["strIdPropriedade"]);
                var nomePropriedade = GetNomePropriedade(lote.IdPropriedade);
                DateTime dataControle = (DateTime)qReader["dtDataControle"];
                lote.LoteDataPropriedade = lote.SLote + " - " + dataControle.ToString("dd/MM/yyyy") + " - " + nomePropriedade.Replace("FAZENDA ","");
                lotes.Add(lote);

            }
            qReader.Close();
            return lotes;
        }

        public Producao GetProducaoAcumulada(DateTime dataControleLeiteiro, string loteId, string matrizId)
        {
            DateTime dataControle;

            var lote = GetLote(Convert.ToInt32(loteId));

            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where dtDataControle <= @dtDataControle and FK_PL_strIdMatriz=@FK_PL_strIdMatriz and strRaca=@strRaca order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControleLeiteiro.ToString("dd/MM/yyyy");
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = lote.Raca;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;

            var qReader = command.ExecuteReader();

            double total = 0;
            double anterior1 = 0;
            double anterior2 = 0;
            double acumulada = 0;
            int pesagens = 0;
            string matriz = "";
            int diasLacAnterior = 0;
            int diasLacAtual = 0;
            double pesoAnterior = 0;
            double pesoAtual = 0;
            int difLac = 0;

            DateTime dataNascMatriz = GetDataNascimentoCria(matrizId);
            int idadeAnos = 0;
            double fatorCorrecao = 0;

            while (qReader.Read())
            {
                if (qReader["dtDataControle"] != DBNull.Value)
                {
                    dataControle = (DateTime)qReader["dtDataControle"];
                    idadeAnos = calculaIdadeAnos(dataNascMatriz, dataControle);
                }


                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["FK_PL_strIdMatriz"] != DBNull.Value) matriz = (string)rowProducoesLeite["FK_PL_strIdMatriz"];

                    if (matriz == matrizId)
                    {
                        pesagens++;

                        if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                        {
                            if (rowProducoesLeite["PL_intDiasLactacao"] != DBNull.Value) diasLacAtual = (int)rowProducoesLeite["PL_intDiasLactacao"];
                            pesoAtual = (double)rowProducoesLeite["PL_decTotal"];
                            fatorCorrecao = GetFatorCorrecao(diasLacAtual, idadeAnos);
                            anterior2 = anterior1;
                            difLac = diasLacAtual - diasLacAnterior;
                            total = (double)rowProducoesLeite["PL_decTotal"];

                            if (total < 100)
                            {

                                if (difLac > 0)
                                {
                                    if (pesagens == 1)
                                    {
                                        acumulada = total * fatorCorrecao * difLac;
                                    }
                                    else
                                    {
                                        acumulada = acumulada + ((pesoAnterior + pesoAtual) / 2 * difLac);
                                    }
                                }

                            }
                            else
                            {
                                acumulada = total;
                            }


                            diasLacAnterior = diasLacAtual;
                            pesoAnterior = pesoAtual;

                            anterior1 = total;


                        }

                        break;
                    }

                }

                if ((difLac < 0) || total >= 100)
                    break;

            }
            qReader.Close();

            Producao producao = new Producao();
            producao.Acumulada = acumulada;
            producao.Anterior = anterior2;

            return producao;
        }

        public Producao GetProducaoAcumulada(DateTime dataControleLeiteiro, int diasLactacao, string loteId, string matrizId)
        {
            DateTime dataControle;
            DateTime dataLimite = dataControleLeiteiro.AddDays(-diasLactacao);

            var lote = GetLote(Convert.ToInt32(loteId));

            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where dtDataControle <= @dtDataControle and dtDataControle >= @dtDataLimite and FK_PL_strIdMatriz=@FK_PL_strIdMatriz and strRaca=@strRaca order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControleLeiteiro.ToString("dd/MM/yyyy");
            ((LightBaseParameter)command.Parameters["dtDataLimite"]).Value = dataLimite.ToString("dd/MM/yyyy");
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = lote.Raca;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;

            var qReader = command.ExecuteReader();

            double total = 0;
            double anterior1 = 0;
            double anterior2 = 0;
            double acumulada = 0;
            int pesagens = 0;
            string matriz = "";
            int diasLacAnterior = 0;
            int diasLacAtual = 0;
            double pesoAnterior = 0;
            double pesoAtual = 0;
            int difLac = 0;

            DateTime dataNascMatriz = GetDataNascimentoCria(matrizId);
            int idadeAnos = 0;
            double fatorCorrecao = 0;

            while (qReader.Read())
            {
                if (qReader["dtDataControle"] != DBNull.Value)
                {
                    dataControle = (DateTime)qReader["dtDataControle"];
                    idadeAnos = calculaIdadeAnos(dataNascMatriz, dataControle);
                }


                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["FK_PL_strIdMatriz"] != DBNull.Value) matriz = (string)rowProducoesLeite["FK_PL_strIdMatriz"];

                    if (matriz == matrizId)
                    {
                        pesagens++;

                        if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                        {
                            if (rowProducoesLeite["PL_intDiasLactacao"] != DBNull.Value) diasLacAtual = (int)rowProducoesLeite["PL_intDiasLactacao"];
                            pesoAtual = (double)rowProducoesLeite["PL_decTotal"];
                            fatorCorrecao = GetFatorCorrecao(diasLacAtual, idadeAnos);
                            anterior2 = anterior1;
                            difLac = diasLacAtual - diasLacAnterior;
                            total = (double)rowProducoesLeite["PL_decTotal"];

                            if (total < 100)
                            {

                                if (difLac > 0)
                                {
                                    if (pesagens == 1)
                                    {
                                        acumulada = total * fatorCorrecao * difLac;
                                    }
                                    else
                                    {
                                        acumulada = acumulada + ((pesoAnterior + pesoAtual) / 2 * difLac);
                                    }
                                }

                            }
                            else
                            {
                                acumulada = total;
                            }


                            diasLacAnterior = diasLacAtual;
                            pesoAnterior = pesoAtual;

                            anterior1 = total;


                        }

                        break;
                    }

                }

                if ((difLac < 0) || total >= 100)
                    break;

            }
            qReader.Close();

            Producao producao = new Producao();
            producao.Acumulada = acumulada;
            producao.Anterior = anterior2;

            return producao;
        }

        public Producao GetProducaoAcumulada(string matrizId)
        {
            DateTime dataControle;

            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz=@FK_PL_strIdMatriz order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;

            var qReader = command.ExecuteReader();

            double total = 0;
            double anterior1 = 0;
            double anterior2 = 0;
            double acumulada = 0;
            int pesagens = 0;
            string matriz = "";
            int diasLacAnterior = 0;
            int diasLacAtual = 0;
            double pesoAnterior = 0;
            double pesoAtual = 0;
            int difLac = 0;

            DateTime dataNascMatriz = GetDataNascimentoCria(matrizId);
            int idadeAnos = 0;
            double fatorCorrecao = 0;

            while (qReader.Read())
            {
                if (qReader["dtDataControle"] != DBNull.Value)
                {
                    dataControle = (DateTime)qReader["dtDataControle"];
                    idadeAnos = calculaIdadeAnos(dataNascMatriz, dataControle);
                }


                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["FK_PL_strIdMatriz"] != DBNull.Value) matriz = (string)rowProducoesLeite["FK_PL_strIdMatriz"];

                    if (matriz == matrizId)
                    {
                        pesagens++;

                        if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                        {
                            if (rowProducoesLeite["PL_intDiasLactacao"] != DBNull.Value) diasLacAtual = (int)rowProducoesLeite["PL_intDiasLactacao"];
                            pesoAtual = (double)rowProducoesLeite["PL_decTotal"];
                            fatorCorrecao = GetFatorCorrecao(diasLacAtual, idadeAnos);
                            anterior2 = anterior1;
                            difLac = diasLacAtual - diasLacAnterior;
                            total = (double)rowProducoesLeite["PL_decTotal"];

                            if (total < 100)
                            {

                                if (difLac > 0)
                                {
                                    if (pesagens == 1)
                                    {
                                        acumulada = total * fatorCorrecao * difLac;
                                    }
                                    else
                                    {

                                        acumulada = acumulada + ((pesoAnterior + pesoAtual) / 2 * difLac);
                                    }
                                }

                            }
                            else
                            {
                                acumulada = total;
                            }


                            diasLacAnterior = diasLacAtual;
                            pesoAnterior = pesoAtual;

                            anterior1 = total;


                        }

                        break;
                    }

                }

                if (difLac < 0)
                {
                    anterior1 = 0;
                    anterior2 = 0;
                    diasLacAnterior = 0;
                    diasLacAtual = 0;
                    pesoAnterior = 0;
                    pesoAtual = 0;
                    difLac = 0;
                }

            }
            qReader.Close();

            Producao producao = new Producao();
            producao.Acumulada = acumulada;
            producao.Anterior = anterior2;

            return producao;
        }

        public string GetNomePropriedade(string id)
        {
            string propriedade = null;
            var command = new LightBaseCommand(@"select strNome from FCARNAUBA_PROPRIEDADE where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["strNome"] != DBNull.Value) propriedade = (string)qReader["strNome"];
            }
            qReader.Close();
            return propriedade;
        }

        public string GetNomeEmpresa(string id)
        {
            string empresa = null;
            var command = new LightBaseCommand(@"select strRazaoSocial from FCARNAUBA_EMPRESA where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["strRazaoSocial"] != DBNull.Value) empresa = (string)qReader["strRazaoSocial"];
            }
            qReader.Close();
            return empresa;
        }

        public string GetNomeGrupo(string id)
        {
            string grupo = null;
            var command = new LightBaseCommand(@"select strDescricao_Grupo from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo");
            ((LightBaseParameter)command.Parameters["kintID_Grupo"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["strDescricao_Grupo"] != DBNull.Value) grupo = (string)qReader["strDescricao_Grupo"];
            }
            qReader.Close();
            return grupo;
        }

        public string GetNomePropriedadeComp(string id)
        {
            string propriedade = null;
            var command = new LightBaseCommand(@"select strNome from FCARNAUBA_PROPRIEDADE_COMP where strIdsPropriedades = @strIdsPropriedades");
            ((LightBaseParameter)command.Parameters["strIdsPropriedades"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {
                if (qReader["strNome"] != DBNull.Value) propriedade = (string)qReader["strNome"];
            }
            qReader.Close();
            return propriedade;
        }

        public string GetDescricaoCentroCusto(string id)
        {
            string centroCusto = null;
            var command = new LightBaseCommand(@"select strDescricao from FCARNAUBA_CENTRO_CUSTO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["strDescricao"] != DBNull.Value) centroCusto = (string)qReader["strDescricao"];
            }
            qReader.Close();
            return centroCusto;
        }

        public string GetNomeAnimal(string id)
        {
            string nome = null;
            var command = new LightBaseCommand(@"select strNomeCompleto from FCARNAUBA_ANIMAIS where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["strNomeCompleto"] != DBNull.Value) nome = (string)qReader["strNomeCompleto"];
            }
            qReader.Close();
            return nome;
        }

        public bool LoteExists(string lote)
        {
            return GetSimpleStrTableWhere("FCARNAUBA_LOTE_CONTROLE_LEITEIRO", "strLote", lote).Count != 0;
        }

        public bool LoteExists(string lote, DateTime dataControle)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where strLote=@strLote and dtDataControle=@dtDataControle");
            ((LightBaseParameter)command.Parameters["strLote"]).Value = lote;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControle;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool LotePonderalExists(string lotePonderal, DateTime dataControle)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_LOTE_CONTROLE_PONDERAL where strLote=@strLote and dtDataControle=@dtDataControle");
            ((LightBaseParameter)command.Parameters["strLote"]).Value = lotePonderal;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControle;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool EstruturaPropriedadeExists(string idPropriedade, DateTime data)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_PROPRIEDADES_ESTRUTURA where strIdPropriedade=@strIdPropriedade and dtData=@dtData");
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = idPropriedade;
            ((LightBaseParameter)command.Parameters["dtData"]).Value = data;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool CdcExists(string cdc)
        {
            return GetSimpleStrTableWhere("FCARNAUBA_CDC", "intCdc", cdc).Count != 0;
        }

        public bool CnpjCpfExists(string cnpjCpf)
        {
            return GetSimpleStrTableWhere("FCARNAUBA_EMPRESA", "strCnpjCpf", cnpjCpf).Count != 0;
        }

        public bool MatrizControleLeiteiroExists(string id, string idMatriz)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz=@FK_PL_strIdMatriz and id=@id");
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = idMatriz;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool MatrizCdcExists(string id, string idMatriz)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_CDC where MAT_FK_strIdMatriz=@MAT_FK_strIdMatriz and id=@id");
            ((LightBaseParameter)command.Parameters["MAT_FK_strIdMatriz"]).Value = idMatriz;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool AnimalLotePonderalExists(string id, string idAnimal)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_LOTE_CONTROLE_PONDERAL where FK_CP_strIdAnimal=@FK_CP_strIdAnimal and id=@id");
            ((LightBaseParameter)command.Parameters["FK_CP_strIdAnimal"]).Value = idAnimal;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool PastagemEstruturaPropriedadeExists(string id, string tipo)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_PROPRIEDADE where PAS_strTipo=@PAS_strTipo and id=@id");
            ((LightBaseParameter)command.Parameters["PAS_strTipo"]).Value = tipo;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool AgriculturaEstruturaPropriedadeExists(string id, string tipo)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_PROPRIEDADE where AGR_strTipo=@AGR_strTipo and id=@id");
            ((LightBaseParameter)command.Parameters["AGR_strTipo"]).Value = tipo;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool BenfeitoriaEstruturaPropriedadeExists(string id, string tipo)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_PROPRIEDADE where BEN_strTipo=@BEN_strTipo and id=@id");
            ((LightBaseParameter)command.Parameters["BEN_strTipo"]).Value = tipo;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool OutraEstruturaPropriedadeExists(string id, string tipo)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_PROPRIEDADE where OUT_strTipo=@OUT_strTipo and id=@id");
            ((LightBaseParameter)command.Parameters["OUT_strTipo"]).Value = tipo;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool ArrendamentoEstruturaPropriedadeExists(string id, string tipo)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_PROPRIEDADE where ARR_strTipo=@ARR_strTipo and id=@id");
            ((LightBaseParameter)command.Parameters["ARR_strTipo"]).Value = tipo;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool PluviometriaExists(string diretorio, DateTime data, string pluviometro)
        {
            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_CONT_PLUVIOMETRICO where diretorio=@diretorio and dtData=@dtData and strPluviometro=@strPluviometro");
            ((LightBaseParameter)command.Parameters["dtData"]).Value = data;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = diretorio;
            ((LightBaseParameter)command.Parameters["strPluviometro"]).Value = pluviometro;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool AnimalCompraExists(string id, string idAnimal)
        {
            var command = new LightBaseCommand(@"select count(*) from  FCARNAUBA_FINANCEIRO where FK_COM_strIdAnimal=@FK_COM_strIdAnimal and id=@id");
            ((LightBaseParameter)command.Parameters["FK_COM_strIdAnimal"]).Value = idAnimal;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public bool NParcelaExists(string id, string NParcela)
        {
            var command = new LightBaseCommand(@"select count(*) from  FCARNAUBA_FINANCEIRO where PAR_intNParcela=@PAR_intNParcela and id=@id");
            ((LightBaseParameter)command.Parameters["PAR_intNParcela"]).Value = NParcela;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant != 0;
        }

        public int QuantidadeDeContratos()
        {

            var command = new LightBaseCommand(@"select count(*) from SIGO_CONTRATOS");
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            var quant = (int)qReader[0];
            qReader.Close();
            return quant;
        }

        public List<Animal> GetCrias(string maeId)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca from FCARNAUBA_ANIMAIS where strMaeId=@strMaeId order by strNome");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "Sem Crias";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetCriasControleLeiteiro(string maeId)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca, dtDataNascimento from FCARNAUBA_ANIMAIS where strMaeId=@strMaeId order by strNome");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "Sem Crias";
            animais.Add(animalVazio);

            while (qReader.Read())
            {
                var dataTemp = qReader["dtDataNascimento"].ToString();
                var dataNascimentoCria = Convert.ToDateTime(dataTemp);
                //var dataInicioControle = dataNascimentoCria.AddDays(7);
                var dataInicioControle = dataNascimentoCria;

                Animal animal = new Animal();
                animal.Id = Convert.ToInt32(qReader["id"].ToString());
                animal.NomeCompleto = dataInicioControle.ToString("dd/MM/yyyy") + " - " + qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                animais.Add(animal);
            }
            qReader.Close();
            return animais;
        }

        public List<Animal> GetCriasControleLeiteiro(string maeId, DateTime dataControle)
        {
            var command = new LightBaseCommand(@"select id, strNome, strRgd, strRaca, dtDataNascimento from FCARNAUBA_ANIMAIS where strMaeId=@strMaeId order by strNome");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = maeId;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var animais = new List<Animal>();
            Animal animalVazio = new Animal();
            animalVazio.Id = 0;
            animalVazio.NomeCompleto = "Sem Crias";
            animais.Add(animalVazio);

            while (qReader.Read())
            {

                var dataTemp = qReader["dtDataNascimento"].ToString();
                var dataNascimentoCria = Convert.ToDateTime(dataTemp);
                //var dataInicioControle = dataNascimentoCria.AddDays(7);
                var dataInicioControle = dataNascimentoCria;

                if (dataControle >= dataNascimentoCria && dataControle <= dataNascimentoCria.AddYears(1))
                {

                    Animal animal = new Animal();
                    animal.Id = Convert.ToInt32(qReader["id"].ToString());
                    animal.NomeCompleto = dataInicioControle.ToString("dd/MM/yyyy") + " - " + qReader["strNome"].ToString() + " - " + qReader["strRgd"].ToString() + " - " + qReader["strRaca"].ToString();
                    animais.Add(animal);
                }
            }
            qReader.Close();
            return animais;
        }

        public List<ProducaoLeite> GetProducaoLeite(int loteControleLeiteiroID)
        {
            var command = new LightBaseCommand(@"select Producao_de_Leite from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = loteControleLeiteiroID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<ProducaoLeite>();

            while (qReader.Read())
            {
                var res = (DataTable)qReader["Producao_de_Leite"];
                int p = 0;

                foreach (DataRow dRow in res.Rows)
                {
                    var producaoLeite = DataRowToProducaoLeite(dRow);
                    producaoLeite.Id = p;
                    retVal.Add(producaoLeite);
                    p++;
                }


            }

            qReader.Close();
            return retVal;
        }

        public ProducaoLeite GetProducaoLeiteById(int loteControleLeiteiroID, int producaoId)
        {
            var producaoLeiteList = GetProducaoLeite(loteControleLeiteiroID);
            return producaoLeiteList[producaoId];
        }

        public DateTime GetDataNascimentoCria(string id)
        {
            string dataTemp = null;
            var command = new LightBaseCommand(@"select dtDataNascimento from FCARNAUBA_ANIMAIS where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["dtDataNascimento"] != DBNull.Value)
                {
                    dataTemp = qReader["dtDataNascimento"].ToString();
                }
            }
            qReader.Close();

            var dataNascimentoCria = Convert.ToDateTime(dataTemp);

            return dataNascimentoCria;
        }

        public DateTime GetDataCobertura(string id)
        {
            string dataTemp = null;
            var command = new LightBaseCommand(@"select dtDataCobertura from FCARNAUBA_CDC where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["dtDataCobertura"] != DBNull.Value)
                {
                    dataTemp = qReader["dtDataCobertura"].ToString();
                }
            }
            qReader.Close();

            var dataCobertura = Convert.ToDateTime(dataTemp);

            return dataCobertura;
        }

        public string GetUltimaDataCDC(string idPai)
        {
            string dataTemp = null;
            var command = new LightBaseCommand(@"select dtDataCobertura from FCARNAUBA_CDC where FK_strIdTouro = @FK_strIdTouro");
            ((LightBaseParameter)command.Parameters["FK_strIdTouro"]).Value = idPai;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["dtDataCobertura"] != DBNull.Value)
                {
                    dataTemp = qReader["dtDataCobertura"].ToString();
                }
            }
            qReader.Close();

            var dataCobertura = dataTemp;

            return dataCobertura;
        }

        public string GetUltimoCDC(string idPai)
        {
            string cdcTemp = null;
            var command = new LightBaseCommand(@"select dtDataCobertura, intCdc from FCARNAUBA_CDC where FK_strIdTouro = @FK_strIdTouro");
            ((LightBaseParameter)command.Parameters["FK_strIdTouro"]).Value = idPai;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["intCdc"] != DBNull.Value)
                {
                    cdcTemp = qReader["intCdc"].ToString();
                }
            }
            qReader.Close();

            var cdc = cdcTemp;

            return cdc;
        }

        public DateTime GetDataControleLeiteiro(string id)
        {
            string dataTemp = null;
            var command = new LightBaseCommand(@"select dtDataControle from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["dtDataControle"] != DBNull.Value)
                {
                    dataTemp = qReader["dtDataControle"].ToString();
                }
            }
            qReader.Close();

            var dataControle = Convert.ToDateTime(dataTemp);

            return dataControle;
        }


        public DateTime? GetDataEntradaControle(string idLote, string idMatriz, string idCria)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = idLote;
            var qReader = command.ExecuteReader();
            DateTime? dataEntradaControle = null;

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                DataTable tableProducoes = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducao in tableProducoes.Rows)
                {
                    string matrizCorr = (String)rowProducao["FK_PL_strIdMatriz"];
                    string criaCorr = "";
                    if (rowProducao["FK_PL_strIdCria"] != DBNull.Value) criaCorr = (String)rowProducao["FK_PL_strIdCria"];
                    if (idMatriz == matrizCorr && idCria == criaCorr)
                    {

                        if (rowProducao["PL_dtDataEntradaControle"] != DBNull.Value)
                        {
                            dataEntradaControle = (DateTime)rowProducao["PL_dtDataEntradaControle"];
                        }
                        break;
                    }
                }

            }


            qReader.Close();
            return dataEntradaControle;
        }

        public bool GetSairControle(string idLote, string idMatriz)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = idLote;
            var qReader = command.ExecuteReader();
            bool sairControle = false;

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                DataTable tableProducoes = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducao in tableProducoes.Rows)
                {
                    string matrizCorr = (String)rowProducao["FK_PL_strIdMatriz"];

                    if (idMatriz == matrizCorr)
                    {

                        if (rowProducao["PL_vfSairControle"] != DBNull.Value)
                        {
                            sairControle = (bool)rowProducao["PL_vfSairControle"];
                        }
                        break;
                    }
                }

            }


            qReader.Close();
            return sairControle;
        }

        public void AdicionaControlePluviometrico(ControlePluviometrico controlePluviometrico)
        {
            var command = new LightBaseCommand(@"insert into FCARNAUBA_CONT_PLUVIOMETRICO
                diretorio,
                dtData,
                decPluviometria,
                FK_IdPropriedade,
                strUsuario,
                dtDataUsuario,
                strPluviometro
                 
                values
                
                (@diretorio,
                @dtData,
                @decPluviometria,
                @FK_IdPropriedade,
                @strUsuario,
                @dtDataUsuario,
                @strPluviometro)", _Connection);
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = controlePluviometrico.Diretorio;
            ((LightBaseParameter)command.Parameters["dtData"]).Value = controlePluviometrico.Data;
            ((LightBaseParameter)command.Parameters["decPluviometria"]).Value = controlePluviometrico.Pluviometria;
            ((LightBaseParameter)command.Parameters["FK_IdPropriedade"]).Value = controlePluviometrico.IdPropriedade;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = controlePluviometrico.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = controlePluviometrico.DataUsuario;
            ((LightBaseParameter)command.Parameters["strPluviometro"]).Value = controlePluviometrico.Pluviometro;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public ControlePluviometrico GetControlePluviometricoById(string id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_CONT_PLUVIOMETRICO where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new ControlePluviometrico();

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["diretorio"] != DBNull.Value) retVal.Diretorio = qReader["diretorio"].ToString();
                if (qReader["dtData"] != DBNull.Value) retVal.Data = (DateTime)qReader["dtData"];
                if (qReader["decPluviometria"] != DBNull.Value) retVal.Pluviometria = Convert.ToDouble(qReader["decPluviometria"]);
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strPluviometro"] != DBNull.Value) retVal.Pluviometro = qReader["strPluviometro"].ToString();

            }


            qReader.Close();
            return retVal;
        }

        public void RemoveControlePluviometrico(string id)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_CONT_PLUVIOMETRICO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }

        public void SalvaControlePluviometrico(ControlePluviometrico controlePluviometrico)
        {

            List<string> campos = new List<string>()
                                      {
                                            "diretorio",
                                            "dtData",
                                            "decPluviometria",
                                            "FK_IdPropriedade",
                                            "strUsuario",
                                            "dtDataUsuario",
                                            "strPluviometro"
                                      };

            var command = new LightBaseCommand(BuildControlePluviometricoString(campos));
            ((LightBaseParameter)command.Parameters["id"]).Value = controlePluviometrico.Id;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = controlePluviometrico.Diretorio;
            ((LightBaseParameter)command.Parameters["dtData"]).Value = controlePluviometrico.Data;
            ((LightBaseParameter)command.Parameters["decPluviometria"]).Value = controlePluviometrico.Pluviometria;
            ((LightBaseParameter)command.Parameters["FK_IdPropriedade"]).Value = controlePluviometrico.IdPropriedade;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = controlePluviometrico.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = controlePluviometrico.DataUsuario;
            ((LightBaseParameter)command.Parameters["strPluviometro"]).Value = controlePluviometrico.Pluviometro;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public List<ResultadoBuscaControlePluviometrico> ConsultaControlePluviometrico(ParametrosDeBuscaEmControlePluviometrico parametrosBuscaEmControlePluviometrico)
        {
            string filter = "";

            if (!String.IsNullOrEmpty(parametrosBuscaEmControlePluviometrico.TodosOsCampos))
            {

                if (parametrosBuscaEmControlePluviometrico.TodosOsCampos.Contains("/"))
                {
                    filter = AddParametroData(filter, "dtData", parametrosBuscaEmControlePluviometrico.TodosOsCampos, "");
                }
                else
                {
                    filter = AddParametro(filter, parametrosBuscaEmControlePluviometrico.TodosOsCampos);
                }

            }

            if (parametrosBuscaEmControlePluviometrico.Id > 0)
            {
                filter = AddParametro(filter, "id", parametrosBuscaEmControlePluviometrico.Id.ToString());
            }

            filter = AddParametro(filter, "diretorio", parametrosBuscaEmControlePluviometrico.Diretorio);

            if (parametrosBuscaEmControlePluviometrico.Data != null)
                filter = AddParametroData(filter, "dtData", parametrosBuscaEmControlePluviometrico.Data.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmControlePluviometrico.DataInicial != null)
                filter = AddParametroData(filter, "dtData", parametrosBuscaEmControlePluviometrico.DataInicial.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmControlePluviometrico.DataFinal != null)
                filter = AddParametroData(filter, "dtData", parametrosBuscaEmControlePluviometrico.DataFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "FK_IdPropriedade", parametrosBuscaEmControlePluviometrico.IdPropriedade);

            filter = AddParametro(filter, "strPluviometro", parametrosBuscaEmControlePluviometrico.Pluviometro);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var controles = new List<ResultadoBuscaControlePluviometrico>();

            while (qReader.Read())
            {

                var retVal = new ResultadoBuscaControlePluviometrico();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["diretorio"] != DBNull.Value) retVal.Diretorio = qReader["diretorio"].ToString();
                if (qReader["dtData"] != DBNull.Value) retVal.Data = (DateTime)qReader["dtData"];
                if (qReader["decPluviometria"] != DBNull.Value) retVal.Pluviometria = Convert.ToDouble(qReader["decPluviometria"]);
                if (qReader["FK_IdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["FK_IdPropriedade"].ToString();
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strPluviometro"] != DBNull.Value) retVal.Pluviometro = qReader["strPluviometro"].ToString();

                controles.Add(retVal);
            }
            qReader.Close();

            var orderControles = controles.OrderByDescending(s => s.Data);

            return orderControles.ToList();
        }



        public void AdicionaFluxoCaixa(FluxoCaixa fluxoCaixa)
        {
            var command = new LightBaseCommand(@"insert into FCARNAUBA_FLUXO_CAIXA
                diretorio,
                strTipo,
                strDescricao,
                moeValor,
                dtData,
                FK_IdPropriedade,
                strUsuario,
                dtDataUsuario,
                FK_IdCentroCusto
                 
                values
                
                (@diretorio,
                @strTipo,
                @strDescricao,
                @moeValor,
                @dtData,
                @FK_IdPropriedade,
                @strUsuario,
                dtDataUsuario,
                @FK_IdCentroCusto)", _Connection);
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = fluxoCaixa.Diretorio;
            ((LightBaseParameter)command.Parameters["dtData"]).Value = fluxoCaixa.Data;
            ((LightBaseParameter)command.Parameters["FK_IdPropriedade"]).Value = fluxoCaixa.IdPropriedade;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = fluxoCaixa.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = fluxoCaixa.DataUsuario;
            ((LightBaseParameter)command.Parameters["strTipo"]).Value = fluxoCaixa.Tipo;
            ((LightBaseParameter)command.Parameters["strDescricao"]).Value = fluxoCaixa.Descricao;
            ((LightBaseParameter)command.Parameters["moeValor"]).Value = fluxoCaixa.Valor;
            ((LightBaseParameter)command.Parameters["FK_IdCentroCusto"]).Value = fluxoCaixa.IdCentroCusto;

            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public FluxoCaixa GetFluxoCaixaById(string id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_FLUXO_CAIXA where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new FluxoCaixa();

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["diretorio"] != DBNull.Value) retVal.Diretorio = qReader["diretorio"].ToString();
                if (qReader["dtData"] != DBNull.Value) retVal.Data = (DateTime)qReader["dtData"];
                if (qReader["moeValor"] != DBNull.Value) retVal.Valor = Convert.ToDouble(qReader["moeValor"]);

                if (qReader["FK_IdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["FK_IdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }

                if (qReader["FK_IdCentroCusto"] != DBNull.Value)
                {
                    retVal.IdCentroCusto = qReader["FK_IdCentroCusto"].ToString();
                    retVal.DescricaoCentroCusto = GetDescricaoCentroCusto(retVal.IdCentroCusto);
                }

                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strTipo"] != DBNull.Value) retVal.Tipo = qReader["strTipo"].ToString();
                if (qReader["strDescricao"] != DBNull.Value) retVal.Descricao = qReader["strDescricao"].ToString();

            }


            qReader.Close();
            return retVal;
        }

        public void RemoveFluxoCaixa(string id)
        {
            OpenConnection();
            var command = new LightBaseCommand(@"delete from FCARNAUBA_FLUXO_CAIXA where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            command.Connection = _Connection;
            command.ExecuteNonQuery();

            CloseConnection();

        }


        public void SalvaFluxoCaixa(FluxoCaixa fluxoCaixa)
        {

            List<string> campos = new List<string>()
                                      {
                                            "diretorio",
                                            "strTipo",
                                            "strDescricao",
                                            "moeValor",
                                            "dtData",
                                            "FK_IdPropriedade",
                                            "strUsuario",
                                            "dtDataUsuario",
                                            "FK_IdCentroCusto"
                                      };

            var command = new LightBaseCommand(BuildFluxoCaixaString(campos));
            ((LightBaseParameter)command.Parameters["id"]).Value = fluxoCaixa.Id;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = fluxoCaixa.Diretorio;
            ((LightBaseParameter)command.Parameters["dtData"]).Value = fluxoCaixa.Data;
            ((LightBaseParameter)command.Parameters["FK_IdPropriedade"]).Value = fluxoCaixa.IdPropriedade;
            ((LightBaseParameter)command.Parameters["strUsuario"]).Value = fluxoCaixa.Usuario;
            ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = fluxoCaixa.DataUsuario;
            ((LightBaseParameter)command.Parameters["strTipo"]).Value = fluxoCaixa.Tipo;
            ((LightBaseParameter)command.Parameters["strDescricao"]).Value = fluxoCaixa.Descricao;
            ((LightBaseParameter)command.Parameters["moeValor"]).Value = fluxoCaixa.Valor;
            ((LightBaseParameter)command.Parameters["FK_IdCentroCusto"]).Value = fluxoCaixa.IdCentroCusto;


            command.Connection = _Connection;
            command.ExecuteNonQuery();

        }

        public List<ResultadoBuscaFluxoCaixa> ConsultaFluxoCaixa(ParametrosDeBuscaEmFluxoCaixa parametrosBuscaEmFluxoCaixa)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmFluxoCaixa.TodosOsCampos);
            if (parametrosBuscaEmFluxoCaixa.Id > 0)
            {
                filter = AddParametro(filter, "id", parametrosBuscaEmFluxoCaixa.Id.ToString());
            }

            filter = AddParametro(filter, "diretorio", parametrosBuscaEmFluxoCaixa.Diretorio);

            if (parametrosBuscaEmFluxoCaixa.Data != null)
                filter = AddParametroData(filter, "dtData", parametrosBuscaEmFluxoCaixa.Data.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmFluxoCaixa.DataInicial != null)
                filter = AddParametroData(filter, "dtData", parametrosBuscaEmFluxoCaixa.DataInicial.Value.ToShortDateString(), ">=");
            if (parametrosBuscaEmFluxoCaixa.DataFinal != null)
                filter = AddParametroData(filter, "dtData", parametrosBuscaEmFluxoCaixa.DataFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "FK_IdPropriedade", parametrosBuscaEmFluxoCaixa.IdPropriedade);

            if (parametrosBuscaEmFluxoCaixa.Valor > 0)
                filter = AddParametroData(filter, "moeValor", parametrosBuscaEmFluxoCaixa.Valor.ToString(), ">=");
            if (parametrosBuscaEmFluxoCaixa.ValorInicial > 0)
                filter = AddParametroData(filter, "moeValor", parametrosBuscaEmFluxoCaixa.ValorInicial.ToString(), ">=");
            if (parametrosBuscaEmFluxoCaixa.ValorFinal > 0)
                filter = AddParametroData(filter, "moeValor", parametrosBuscaEmFluxoCaixa.ValorFinal.ToString(), "<=");

            filter = AddParametro(filter, "strTipo", parametrosBuscaEmFluxoCaixa.Tipo);
            filter = AddParametro(filter, "strDescricao", parametrosBuscaEmFluxoCaixa.Descricao);


            var command = new LightBaseCommand("textsearch in FCARNAUBA_FLUXO_CAIXA " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var fluxos = new List<ResultadoBuscaFluxoCaixa>();

            while (qReader.Read())
            {

                var retVal = new ResultadoBuscaFluxoCaixa();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["diretorio"] != DBNull.Value) retVal.Diretorio = qReader["diretorio"].ToString();
                if (qReader["dtData"] != DBNull.Value) retVal.Data = (DateTime)qReader["dtData"];
                if (qReader["moeValor"] != DBNull.Value) retVal.Valor = Convert.ToDouble(qReader["moeValor"]);
                if (qReader["FK_IdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["FK_IdPropriedade"].ToString();
                if (qReader["strUsuario"] != DBNull.Value) retVal.Usuario = qReader["strUsuario"].ToString();
                if (qReader["dtDataUsuario"] != DBNull.Value) retVal.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                if (qReader["strTipo"] != DBNull.Value) retVal.Tipo = qReader["strTipo"].ToString();
                if (qReader["strDescricao"] != DBNull.Value) retVal.Descricao = qReader["strDescricao"].ToString();

                if (qReader["FK_IdPropriedade"] != DBNull.Value)
                {
                    retVal.IdPropriedade = qReader["FK_IdPropriedade"].ToString();
                    retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                }

                if (qReader["FK_IdCentroCusto"] != DBNull.Value)
                {
                    retVal.IdCentroCusto = qReader["FK_IdCentroCusto"].ToString();
                    retVal.DescricaoCentroCusto = GetDescricaoCentroCusto(retVal.IdCentroCusto);
                }

                fluxos.Add(retVal);
            }
            qReader.Close();

            var orderFluxos = fluxos.OrderByDescending(s => s.Data);

            return orderFluxos.ToList();
        }

        public double GetProducaoDiariaMediaMatriz(string matrizId)
        {
            DateTime dataControle;

            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz=@FK_PL_strIdMatriz order by dtDataControle");

            command.Connection = _Connection;

            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;
            var qReader = command.ExecuteReader();

            double total = 0;
            string matriz = "";
            int i = 0;

            while (qReader.Read())
            {
                if (qReader["dtDataControle"] != DBNull.Value) dataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["FK_PL_strIdMatriz"] != DBNull.Value) matriz = (string)rowProducoesLeite["FK_PL_strIdMatriz"];

                    if (matriz == matrizId)
                    {

                        if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                        {
                            total = total + (double)rowProducoesLeite["PL_decTotal"];
                            i++; ;

                        }

                        break;
                    }

                }

            }
            qReader.Close();

            if (i > 0)
            {
                return total / i;
            }
            else
            {
                return 0;
            }
        }


        public List<RAnimaisProducaoLeite> ConsultaProducaoDiariaMedia(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {

            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", "Fêmea");


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsProducaoDiariaMedia = new List<RAnimaisProducaoLeite>();

            while (qReader.Read())
            {

                var retVal = new RAnimaisProducaoLeite();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                var producaoDiariaMedia = GetProducaoDiariaMediaMatriz(retVal.Id.ToString());

                retVal.ProducaoDiariaMedia = producaoDiariaMedia;


                rankingsProducaoDiariaMedia.Add(retVal);


            }
            qReader.Close();

            var orderRankingsProducaoDiariaMedia = rankingsProducaoDiariaMedia.OrderByDescending(s => s.ProducaoDiariaMedia);

            return orderRankingsProducaoDiariaMedia.ToList();
        }


        public double GetProducaoDiariaMaximaMatriz(string matrizId)
        {
            DateTime dataControle;

            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz=@FK_PL_strIdMatriz order by dtDataControle");

            command.Connection = _Connection;

            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;
            var qReader = command.ExecuteReader();

            double total = 0;
            double totalAnterior = 0;
            double totalMax = 0;
            string matriz = "";

            while (qReader.Read())
            {
                if (qReader["dtDataControle"] != DBNull.Value) dataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["FK_PL_strIdMatriz"] != DBNull.Value) matriz = (string)rowProducoesLeite["FK_PL_strIdMatriz"];

                    if (matriz == matrizId)
                    {

                        if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                        {
                            total = (double)rowProducoesLeite["PL_decTotal"];

                            if (total > totalAnterior)
                            {
                                totalMax = total;
                            }
                            else
                            {
                                totalMax = totalAnterior;
                            }

                            totalAnterior = total;


                        }

                        break;
                    }

                }

            }
            qReader.Close();

            return totalMax;
        }


        public List<RAnimaisProducaoLeite> ConsultaProducaoDiariaMaxima(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", "Fêmea");


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsProducaoDiariaMaxima = new List<RAnimaisProducaoLeite>();

            while (qReader.Read())
            {

                var retVal = new RAnimaisProducaoLeite();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                var producaoDiariaMaxima = GetProducaoDiariaMaximaMatriz(retVal.Id.ToString());

                retVal.ProducaoDiariaMaxima = producaoDiariaMaxima;


                rankingsProducaoDiariaMaxima.Add(retVal);


            }
            qReader.Close();

            var orderRankingsProducaoDiariaMaxima = rankingsProducaoDiariaMaxima.OrderByDescending(s => s.ProducaoDiariaMaxima);

            return orderRankingsProducaoDiariaMaxima.ToList();
        }

        public Producao GetProducaoAcumuladaMatriz(string matrizId)
        {
            DateTime dataControle;

            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz=@FK_PL_strIdMatriz order by dtDataControle");

            command.Connection = _Connection;

            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;
            var qReader = command.ExecuteReader();

            double total = 0;
            double anterior1 = 0;
            double anterior2 = 0;
            double acumulada = 0;
            int pesagens = 0;
            string matriz = "";
            int diasLacAnterior = 0;
            int diasLacAtual = 0;
            double pesoAnterior = 0;
            double pesoAtual = 0;
            int difLac = 0;

            DateTime dataNascMatriz = GetDataNascimentoCria(matrizId);
            int idadeAnos = 0;
            double fatorCorrecao = 0;

            while (qReader.Read())
            {
                if (qReader["dtDataControle"] != DBNull.Value)
                {
                    dataControle = (DateTime)qReader["dtDataControle"];
                    idadeAnos = calculaIdadeAnos(dataNascMatriz, dataControle);
                }

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["FK_PL_strIdMatriz"] != DBNull.Value) matriz = (string)rowProducoesLeite["FK_PL_strIdMatriz"];

                    if (matriz == matrizId)
                    {
                        pesagens++;

                        if (rowProducoesLeite["PL_decTotal"] != DBNull.Value)
                        {
                            if (rowProducoesLeite["PL_intDiasLactacao"] != DBNull.Value) diasLacAtual = (int)rowProducoesLeite["PL_intDiasLactacao"];
                            pesoAtual = (double)rowProducoesLeite["PL_decTotal"];
                            fatorCorrecao = GetFatorCorrecao(diasLacAtual, idadeAnos);
                            anterior2 = anterior1;
                            difLac = diasLacAtual - diasLacAnterior;
                            total = (double)rowProducoesLeite["PL_decTotal"];

                            if (difLac > 0)
                            {
                                if (pesagens == 1)
                                {
                                    acumulada = total * fatorCorrecao * difLac;
                                }
                                else
                                {
                                    
                                    acumulada = acumulada + ((pesoAnterior + pesoAtual) / 2 * difLac);
                                }
                            }
                            else
                            {
                                acumulada = acumulada + (total * fatorCorrecao * diasLacAtual);
                                pesagens = 1;
                            }

                            diasLacAnterior = diasLacAtual;
                            pesoAnterior = pesoAtual;

                            anterior1 = total;


                        }

                        break;
                    }

                }

            }
            qReader.Close();

            Producao producao = new Producao();
            producao.Acumulada = acumulada;

            return producao;
        }

        public List<RAnimaisProducaoLeite> ConsultaProducaoAcumulada(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            string filter = "";
            filter = AddParametro(filter, parametrosBuscaEmAnimais.TodosOsCampos);

            filter = AddParametro(filter, "strId", parametrosBuscaEmAnimais.StrId);

            filter = AddParametro(filter, "strNomeFazenda", parametrosBuscaEmAnimais.NomeFazenda);

            filter = AddParametro(filter, "strRaca", parametrosBuscaEmAnimais.Raca);

            if (parametrosBuscaEmAnimais.NumeroOrdem > 0)
                filter = AddParametro(filter, "intNumeroOrdem", parametrosBuscaEmAnimais.NumeroOrdem.ToString());

            if (!String.IsNullOrEmpty(parametrosBuscaEmAnimais.Nome))
            {
                filter = AddParametroTextual(filter, "strNome", parametrosBuscaEmAnimais.Nome);
            }

            filter = AddParametro(filter, "strNomeCompleto", parametrosBuscaEmAnimais.NomeCompleto);

            filter = AddParametro(filter, "strSexo", "Fêmea");


            if (parametrosBuscaEmAnimais.DataNascimento != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimento.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoInicial != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoInicial.Value.ToShortDateString(), ">=");

            if (parametrosBuscaEmAnimais.DataNascimentoFinal != null)
                filter = AddParametroData(filter, "dtDataNascimento", parametrosBuscaEmAnimais.DataNascimentoFinal.Value.ToShortDateString(), "<=");

            filter = AddParametro(filter, "strRgd", parametrosBuscaEmAnimais.Rgd);

            filter = AddParametro(filter, "strPaiId", parametrosBuscaEmAnimais.StrPaiId);

            filter = AddParametro(filter, "strMaeId", parametrosBuscaEmAnimais.StrMaeId);

            var command = new LightBaseCommand("textsearch in FCARNAUBA_ANIMAIS " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var rankingsProducaoAcumulada = new List<RAnimaisProducaoLeite>();

            while (qReader.Read())
            {

                var retVal = new RAnimaisProducaoLeite();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strNomeCompleto"] != DBNull.Value) retVal.NomeCompleto = qReader["strNomeCompleto"].ToString();
                if (qReader["intNumeroOrdem"] != DBNull.Value) retVal.NumeroOrdem = Convert.ToInt64(qReader["intNumeroOrdem"]);
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();

                var producao = GetProducaoAcumuladaMatriz(retVal.Id.ToString());

                retVal.Acumulado = producao.Acumulada;


                rankingsProducaoAcumulada.Add(retVal);


            }
            qReader.Close();

            var orderRankingsProducaoAcumulada = rankingsProducaoAcumulada.OrderByDescending(s => s.ProducaoDiariaMaxima);

            return orderRankingsProducaoAcumulada.ToList();
        }

        public long UltimoLote(string matrizId)
        {
            var command = new LightBaseCommand(@"select id, Producao_de_Leite from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz = @FK_PL_strIdMatriz");
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            long retVal = 0;
            bool ultimoLote = false;

            while (qReader.Read())
            {
                var res = qReader["Producao_de_Leite"] as DataTable;
                if (res == null) break;
                foreach (DataRow dRow in res.Rows)
                {

                    if (dRow["PL_vfSairControle"] != DBNull.Value) ultimoLote = (bool)dRow["PL_vfSairControle"];

                    if (ultimoLote)
                    {
                        if (qReader["id"] != DBNull.Value) retVal = Convert.ToInt64(qReader["id"]);
                        break;
                    }

                }

                if (ultimoLote)
                    break;

            }
            qReader.Close();
            return retVal;
        }


        public List<RProducaoLeite> GetPesagens(DateTime dataControle, string matrizId)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz = @FK_PL_strIdMatriz order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControleLeiteiro();
            var producao = new RProducaoLeite();
            var producoes = new List<RProducaoLeite>();
            int diasTemp = 0;
            double acumulTemp = 0;
            double mediaTemp = 0;
            bool sairControle = false;
            int i = 1;
            int lactacaoControlada = QuantidadeLactacaoControlada(matrizId);
            int lactacaoControladaInv = 1;
            string matrizIdTemp = "";
            bool entrou = false;

            while (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strIdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducaoLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducaoLeite["FK_PL_strIdMatriz"] != DBNull.Value)
                    {
                        matrizIdTemp = (string)rowProducaoLeite["FK_PL_strIdMatriz"];
                    }

                    if (matrizIdTemp == matrizId)
                    {
                        RProducaoLeite producaoLeite = DataRowToRProducaoLeite(rowProducaoLeite);

                        //Controle Leiteiro
                        producaoLeite.DataNascimentoMatriz = GetDataNascimentoCria(producaoLeite.IdMatriz);

                        producaoLeite.Controle = i;
                        producaoLeite.LactacaoControlada = lactacaoControlada;
                        producaoLeite.LactacaoControladaInv = lactacaoControladaInv;
                        producaoLeite.Raca = retVal.Raca;
                        producaoLeite.Fazenda = retVal.NomePropriedade;
                        producaoLeite.DataControle = retVal.DataControle;
                        producaoLeite.HoraPOrdenha = retVal.POrdenha;
                        producaoLeite.HoraSOrdenha = retVal.SOrdenha;
                        producaoLeite.HoraTOrdenha = retVal.TOrdenha;

                        if (producaoLeite.POrdenha >= 100)
                        {
                            producaoLeite.ProducaoAnterior = 0;
                            producaoLeite.ProducaoAcumulada = producaoLeite.POrdenha;
                        }
                        else
                        {
                            var prod = GetProducaoAcumulada(retVal.DataControle, producaoLeite.DiasLactacao, retVal.Id.ToString(), producaoLeite.IdMatriz);
                            producaoLeite.ProducaoAnterior = prod.Anterior;
                            producaoLeite.ProducaoAcumulada = prod.Acumulada;
                        }

                        if (producaoLeite.DiasLactacao > 0)
                        {
                            producaoLeite.Media = producaoLeite.ProducaoAcumulada / producaoLeite.DiasLactacao;
                        }
                        else
                        {
                            producaoLeite.Media = 0;
                        }

                        if (producaoLeite.Total > 0)
                        {
                            diasTemp = producaoLeite.DiasLactacao;
                            acumulTemp = producaoLeite.ProducaoAcumulada;
                            mediaTemp = producaoLeite.Media;

                            if (producaoLeite.LactacaoControlada == 1)
                            {
                                if (!String.IsNullOrEmpty(producaoLeite.IdCria) && retVal.DataControle.Year != 1)
                                {
                                    if ((producoes.Count > 0) && !entrou)
                                    {
                                        entrou = true;
                                        producaoLeite.DataNascimentoCria = retVal.DataControle.AddDays(-producaoLeite.DiasLactacao).AddDays(-7);
                                        producoes[0].DataNascimentoCria = producaoLeite.DataNascimentoCria;
                                        producaoLeite.IdadeParto = calculaIdade(producaoLeite.DataNascimentoMatriz, producaoLeite.DataNascimentoCria);
                                        producoes[0].IdadeParto = producaoLeite.IdadeParto;
                                    }
                                }
                                i++;
                            }

                            if (!producaoLeite.SairControle)
                            {
                                producoes.Add(producaoLeite);
                            }


                        }


                        if (producaoLeite.SairControle)
                        {
                            producaoLeite.DiasLactacao = diasTemp;
                            producaoLeite.ProducaoAcumulada = acumulTemp;
                            producaoLeite.Media = mediaTemp;
                            producoes.Add(producaoLeite);
                            producoes[0].DataSaidaControle = retVal.DataControle;
                            producoes[0].OrdemParto = QuantidadeDeFilhosMae(producaoLeite.IdMatriz);
                            
                            lactacaoControlada--;
                            lactacaoControladaInv++;

                            TimeSpan dataVida = Convert.ToDateTime(retVal.DataControle) - Convert.ToDateTime(producaoLeite.DataNascimentoMatriz);

                            int diasVida = dataVida.Days;

                            double producaoAcumuladaTotal = GetProducaoAcumuladaMatriz(matrizId).Acumulada;

                            if (diasVida > 0)
                            {
                                producoes[0].MediaTotal = producaoAcumuladaTotal / diasVida;
                            }
                            else
                            {
                                producoes[0].MediaTotal = 0;
                            }

                            sairControle = true;
                            break;
                        }


                    }


                }

                if (sairControle)
                    sairControle = false;
            }

            return producoes;
        }

        public List<RProducaoLeite> GetPesagensRebanho(ParametrosDeBuscaEmLotes parametros)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataControle >= @dtDataInicio and dtDataControle <= @dtDataFim");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = parametros.Raca;
            ((LightBaseParameter)command.Parameters["strIdPropriedade"]).Value = parametros.IdPropriedade;
            ((LightBaseParameter)command.Parameters["dtDataInicio"]).Value = parametros.DataControleInicial;
            ((LightBaseParameter)command.Parameters["dtDataFim"]).Value = parametros.DataControleFinal;
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControleLeiteiro();
            var producao = new RProducaoLeite();
            var producoes = new List<RProducaoLeite>();
            int diasTemp = 0;
            double acumulTemp = 0;
            double mediaTemp = 0;

            while (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strIdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducaoLeite in tableProducoesLeite.Rows)
                {

                    RProducaoLeite producaoLeite = DataRowToRProducaoLeite(rowProducaoLeite);


                    if (producaoLeite.SairControle)
                    {
                        //Controle Leiteiro
                        producaoLeite.Raca = retVal.Raca;
                        producaoLeite.Fazenda = retVal.NomePropriedade;
                        producaoLeite.DataControle = retVal.DataControle;

                        if (producaoLeite.POrdenha >= 100)
                        {

                            producaoLeite.ProducaoAcumulada = producaoLeite.POrdenha;
                        }
                        else
                        {
                            var prod = GetProducaoAcumulada(retVal.DataControle, producaoLeite.DiasLactacao, retVal.Id.ToString(), producaoLeite.IdMatriz);
                            producaoLeite.ProducaoAcumulada = prod.Acumulada;
                        }

                        if (producaoLeite.DiasLactacao > 0)
                        {
                            producaoLeite.Media = producaoLeite.ProducaoAcumulada / producaoLeite.DiasLactacao;
                        }
                        else
                        {
                            producaoLeite.Media = 0;
                        }

                        if (producaoLeite.Total > 0)
                        {
                            diasTemp = producaoLeite.DiasLactacao;
                            acumulTemp = producaoLeite.ProducaoAcumulada;
                            mediaTemp = producaoLeite.Media;

                        }


                        producaoLeite.Periodo = "Período: " + parametros.DataControleInicial.Value.ToShortDateString() + " a " + parametros.DataControleFinal.Value.ToShortDateString();
                        producaoLeite.DiasLactacao = diasTemp;
                        producaoLeite.ProducaoAcumulada = acumulTemp;
                        producaoLeite.Media = mediaTemp;
                        producoes.Add(producaoLeite);

                    }

                }


            }

            var orderProducoes = producoes.OrderBy(s => s.NomeMatriz).ThenBy(t => t.DataControle);

            return orderProducoes.ToList(); ;
        }

        public List<RProducaoReal> GetProducaoReal(DateTime dataControle, string matrizId)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz = @FK_PL_strIdMatriz order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = matrizId;
            var qReader = command.ExecuteReader();

            var retVal = new RLoteControleLeiteiro();
            var producao = new RProducaoReal();
            var producoes = new List<RProducaoReal>();
            var producaoReal = new List<RProducaoReal>();
            bool sairControle = false;
            int i = 1;
            while (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["strRaca"] != DBNull.Value) retVal.Raca = qReader["strRaca"].ToString();
                if (qReader["strIdPropriedade"] != DBNull.Value) retVal.IdPropriedade = qReader["strIdPropriedade"].ToString();
                retVal.NomePropriedade = GetNomePropriedade(retVal.IdPropriedade);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];
                if (qReader["str1Ordenha"] != DBNull.Value) retVal.POrdenha = qReader["str1Ordenha"].ToString();
                if (qReader["str2Ordenha"] != DBNull.Value) retVal.SOrdenha = qReader["str2Ordenha"].ToString();
                if (qReader["str3Ordenha"] != DBNull.Value) retVal.TOrdenha = qReader["str3Ordenha"].ToString();

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducaoLeite in tableProducoesLeite.Rows)
                {
                    RProducaoReal producaoLeite = DataRowToRProducaoReal(rowProducaoLeite);

                    if (producaoLeite.IdMatriz == matrizId)
                    {

                        //Controle Leiteiro
                        producaoLeite.Controle = i;
                        producaoLeite.Raca = retVal.Raca;
                        producaoLeite.Fazenda = retVal.NomePropriedade;
                        producaoLeite.DataControle = retVal.DataControle;
                        producaoLeite.HoraPOrdenha = retVal.POrdenha;
                        producaoLeite.HoraSOrdenha = retVal.SOrdenha;
                        producaoLeite.HoraTOrdenha = retVal.TOrdenha;

                        if (producaoLeite.POrdenha >= 100)
                        {
                            producaoLeite.ProducaoAnterior = 0;
                            producaoLeite.ProducaoAcumuladaReal = producaoLeite.POrdenha;
                        }
                        else
                        {
                            var prod = GetProducaoAcumulada(retVal.DataControle, retVal.Id.ToString(), producaoLeite.IdMatriz);
                            producaoLeite.ProducaoAnterior = prod.Anterior;
                            producaoLeite.ProducaoAcumuladaReal = prod.Acumulada;
                        }

                        if (producaoLeite.DiasLactacaoReal > 0)
                        {
                            producaoLeite.MediaReal = producaoLeite.ProducaoAcumuladaReal / producaoLeite.DiasLactacaoReal;
                        }
                        else
                        {
                            producaoLeite.MediaReal = 0;
                        }

                    }

                    if (producaoLeite.SairControle)
                    {
                        producaoReal.Add(producaoLeite);
                        sairControle = true;
                        break;
                    }

                }

                if (sairControle)
                    break;
            }


            return producaoReal;
        }

        string calculaIdade(DateTime dNascimento, DateTime dAtual)
        {
            int idDias = 0, idMeses = 0, idAnos = 0;
            DateTime dNascimentoCorrente = DateTime.Parse(dNascimento.Day.ToString() + "/" +
            dNascimento.Month.ToString() + "/" + (dAtual.Year - 1).ToString());
            string ta = "", tm = "", td = "";
            if (dAtual < dNascimento)
            {
                return "Data de nascimento inválida ";
            }
            idAnos = dAtual.Year - dNascimento.Year;
            if (dAtual.Month < dNascimento.Month || (dAtual.Month ==
            dNascimento.Month && dAtual.Day < dNascimento.Day))
            {
                idAnos--;
            }
            idMeses = calculaMeses(dAtual, dNascimento);
            idDias = calculaDias(dAtual, dNascimento);
            if (idAnos > 1)
                ta = idAnos + " anos ";
            else if (idAnos == 1)
                ta = idAnos + "ano";
            if (idMeses > 1)
                tm = idMeses + " meses ";
            else if (idMeses == 1)
                tm = idMeses + " mês ";
            if (idDias > 1)
                td = idDias + " dias ";
            else if (idDias == 1)
                td = idDias + " dia ";
            return ta + tm + td;
        }

        public int calculaIdadeAnos(DateTime dNascimento, DateTime dAtual)
        {
            int idAnos = 0;
            if (dAtual < dNascimento)
            {
                return 0;
            }
            idAnos = dAtual.Year - dNascimento.Year;
            if (dAtual.Month < dNascimento.Month || (dAtual.Month ==
            dNascimento.Month && dAtual.Day < dNascimento.Day))
            {
                idAnos--;
            }

            return idAnos;
        }


        int calculaDias(DateTime dataAtual, DateTime dataOriginal)
        {
            int numeroDias = 0;
            if ((dataAtual.Month > dataOriginal.Month || dataAtual.Month <
            dataOriginal.Month) && (dataAtual.Day > dataOriginal.Day))
            {
                DateTime dUltima = DateTime.Parse(dataOriginal.Day.ToString() + "/" +
                (dataAtual.Month).ToString() + "/" + (dataAtual.Year).ToString());
                numeroDias = (dataAtual - dUltima).Days;
            }
            else if ((dataAtual.Month > dataOriginal.Month || dataAtual.Month <
            dataOriginal.Month) && (dataAtual.Day < dataOriginal.Day))
            {
                DateTime dUltima = DateTime.Parse(dataOriginal.Day.ToString() + "/" +
                (dataAtual.Month - 1).ToString() + "/" + (dataAtual.Year).ToString());
                numeroDias = (dataAtual - dUltima).Days;
            }
            else if (dataOriginal.Month == dataAtual.Month)
            {
                DateTime dUltima = DateTime.Parse(dataOriginal.Day.ToString() + "/" +
                (dataAtual.Month).ToString() + "/" + (dataAtual.Year).ToString());
                numeroDias = (dataAtual - dUltima).Days;
            }
            return numeroDias;
        }

        int calculaMeses(DateTime dataAtual, DateTime dataOriginal)
        {
            int numeroMeses = 0;
            if ((dataAtual.Month > dataOriginal.Month))
            {
                numeroMeses = dataAtual.Month - dataOriginal.Month;
            }
            else if ((dataAtual.Month < dataOriginal.Month))
            {
                if (dataAtual.Day > dataOriginal.Day)
                {
                    numeroMeses = (12 - dataOriginal.Month) + (dataAtual.Month);
                }
                else if (dataAtual.Day < dataOriginal.Day)
                {
                    numeroMeses = (12 - dataOriginal.Month) + (dataAtual.Month - 1);
                }
            }
            return numeroMeses;
        }

        int GetQtdMoth(DateTime inicio, DateTime fim)
        {
            int years = fim.Year - inicio.Year;
            if (years < 0)
                return 0;
            if (years == 0)
                return fim.Month - inicio.Month;
            int meses = 12 - inicio.Month;
            return (years - 1) * 12 + meses + fim.Month;
        }

        public void AtualizaDiasLactacao()
        {
            LightBaseCommand command;

            command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO");
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            int idLote = 0;
            int DiasAtual = 0;

            while (qReader.Read())
            {
                if (qReader["id"] != DBNull.Value)
                    idLote = Convert.ToInt32(qReader["id"]);

                DataTable tableProducaoLeite = (DataTable)qReader["Producao_de_Leite"];
                int i = 0;
                foreach (DataRow rowProducaoLeite in tableProducaoLeite.Rows)
                {
                    if (rowProducaoLeite["PL_intDiasLactacao"] != DBNull.Value)
                    {
                        if (rowProducaoLeite["FK_PL_strIdCria"] != DBNull.Value && (string)rowProducaoLeite["FK_PL_strIdCria"] != "0")
                        {

                            DiasAtual = (int)rowProducaoLeite["PL_intDiasLactacao"];
                            DiasAtual = DiasAtual + 7;

                            AtualizaDiasLactacaoRow(idLote, DiasAtual, i);
                        }

                    }

                    i++;
                }
            }
        }

        public void AtualizaDiasLactacaoRow(int idLote, int diasAtual, int row)
        {
            string sRow = Convert.ToString(row);
            var command = new LightBaseCommand("update FCARNAUBA_LOTE_CONTROLE_LEITEIRO set Producao_de_Leite[" + sRow + "].PL_intDiasLactacao = @PL_intDiasLactacao where id = @id");

            ((LightBaseParameter)command.Parameters["PL_intDiasLactacao"]).Value = diasAtual;
            ((LightBaseParameter)command.Parameters["id"]).Value = idLote;

            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public double GetFatorCorrecao(int diasLactacao, int anos)
        {

            double fatorCorrecao = 0;
            var command = new LightBaseCommand(@"select * from FCARNAUBA_FATOR_CORRECAO where intIntervaloPartoEntrada = @intIntervaloPartoEntrada");
            ((LightBaseParameter)command.Parameters["intIntervaloPartoEntrada"]).Value = diasLactacao;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {
                if (anos <= 3)
                    if (qReader["decAte3"] != DBNull.Value) fatorCorrecao = (double)qReader["decAte3"];
                if (anos > 3 && anos <= 4)
                    if (qReader["decAte4"] != DBNull.Value) fatorCorrecao = (double)qReader["decAte4"];
                if (anos > 4 && anos <= 5)
                    if (qReader["decAte5"] != DBNull.Value) fatorCorrecao = (double)qReader["decAte5"];
                if (anos > 6 && anos <= 6)
                    if (qReader["decAte6"] != DBNull.Value) fatorCorrecao = (double)qReader["decAte6"];
                if (anos > 6)
                    if (qReader["decMaior6"] != DBNull.Value) fatorCorrecao = (double)qReader["decMaior6"];

            }

            qReader.Close();

            fatorCorrecao = Math.Round(fatorCorrecao, 3);

            return fatorCorrecao;
        }

        public int GetNumeroCio(string idMatriz, DateTime dataCobertura)
        {
            string dataInicioStr = "";

            if (dataCobertura.Month >= 5)
            {
                dataInicioStr = "01/05/" + dataCobertura.Year;
            }
            else
            {
                dataInicioStr = "01/05/" + (dataCobertura.Year - 1);
            }

            DateTime dataInicio = Convert.ToDateTime(dataInicioStr);

            var command = new LightBaseCommand(@"select * from FCARNAUBA_CDC where MAT_FK_strIdMatriz = @MAT_FK_strIdMatriz and dtDataCobertura>=@dtDataInicio and dtDataCobertura<=@dtDataCobertura order by dtDataCobertura");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["MAT_FK_strIdMatriz"]).Value = idMatriz;
            ((LightBaseParameter)command.Parameters["dtDataCobertura"]).Value = dataCobertura;
            ((LightBaseParameter)command.Parameters["dtDataInicio"]).Value = dataInicio;
            var qReader = command.ExecuteReader();

            string idMatrizCorr = "";
            int i = 0;
            while (qReader.Read())
            {
                DataTable tableMatrizes = (DataTable)qReader["Matrizes"];

                foreach (DataRow rowMatriz in tableMatrizes.Rows)
                {
                    if (rowMatriz["MAT_FK_strIdMatriz"] != DBNull.Value)
                    {
                        idMatrizCorr = (string)rowMatriz["MAT_FK_strIdMatriz"];
                    }

                    if (idMatrizCorr == idMatriz)
                    {
                        i++;
                    }

                }
            }

            return i;
        }

        public void AtualizaRemocaoControleLeiteiro(string idMatriz, DateTime dataControle, string raca)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where FK_PL_strIdMatriz = @FK_PL_strIdMatriz and strRaca = @strRaca and dtDataControle>@dtDataControle order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["FK_PL_strIdMatriz"]).Value = idMatriz;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControle;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            var qReader = command.ExecuteReader();

            string idMatrizCorr = "";
            int diasLacAnterior = 0;
            int diasLacAtual = 0;
            int j = 0;
            int idLoteControleLeiteiro = 0;
            while (qReader.Read())
            {
                int i = 0;

                if (qReader["id"] != DBNull.Value) idLoteControleLeiteiro = Convert.ToInt32(qReader["id"]);

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];

                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    if (rowProducoesLeite["FK_PL_strIdMatriz"] != DBNull.Value)
                    {
                        idMatrizCorr = (string)rowProducoesLeite["FK_PL_strIdMatriz"];
                    }

                    if (idMatrizCorr == idMatriz)
                    {
                        if (rowProducoesLeite["PL_intDiasLactacao"] != DBNull.Value) diasLacAtual = (int)rowProducoesLeite["PL_intDiasLactacao"];

                        if (diasLacAnterior < diasLacAtual)
                        {

                            RemoveProducaoLeite(idLoteControleLeiteiro, i);

                            j++;
                        }

                        break;
                    }

                    i++;

                }

                if (diasLacAnterior >= diasLacAtual && j != 0)
                {
                    break;
                }

                diasLacAnterior = diasLacAtual;
            }
        }


        public void AtualizaAdicaoControleLeiteiro(ProducaoLeite prodLeite, DateTime dataControle, string raca)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_LEITEIRO where strRaca = @strRaca and dtDataControle>@dtDataControle order by dtDataControle");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["dtDataControle"]).Value = dataControle;
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            var qReader = command.ExecuteReader();

            int idLoteControleLeiteiro = 0;
            DateTime dataAtual = dataControle;
            DateTime dataAnterior = dataControle;
            while (qReader.Read())
            {

                if (qReader["id"] != DBNull.Value) idLoteControleLeiteiro = Convert.ToInt32(qReader["id"]);
                if (qReader["dtDataControle"] != DBNull.Value) dataAtual = Convert.ToDateTime(qReader["dtDataControle"]);

                TimeSpan date = Convert.ToDateTime(dataAtual) - Convert.ToDateTime(dataAnterior);
                int totalDias = date.Days;

                if (totalDias > 200)
                {
                    break;
                }
                else
                {
                    if (!MatrizControleLeiteiroExists(idLoteControleLeiteiro.ToString(), prodLeite.IdMatriz))
                    {
                        ProducaoLeite producaoLeite = new ProducaoLeite();
                        producaoLeite.IdMatriz = prodLeite.IdMatriz;
                        producaoLeite.IdCria = prodLeite.IdCria;


                        int diasLactacao = 0;
                        DateTime? dataEntradaControle = GetDataEntradaControle(idLoteControleLeiteiro.ToString(), producaoLeite.IdMatriz, producaoLeite.IdCria);

                        if (dataEntradaControle != null)
                        {

                            TimeSpan diferenca = (TimeSpan)(dataAtual - dataEntradaControle);
                            if (diferenca.TotalDays > 0)
                            {
                                diasLactacao = (int)diferenca.TotalDays;
                            }

                        }
                        else
                        {
                            if (producaoLeite.IdCria != "0")
                            {
                                DateTime dataEntradaControleCria = GetDataNascimentoCria(producaoLeite.IdCria);

                                TimeSpan diferenca = (TimeSpan)(dataAtual - dataEntradaControleCria);
                                if (diferenca.TotalDays > 0)
                                {
                                    diasLactacao = (int)diferenca.TotalDays;
                                }
                            }
                        }

                        producaoLeite.DiasLactacao = diasLactacao;

                        if (producaoLeite.DiasLactacao <= 300)
                        {

                            AdicionaProducaoLeite(idLoteControleLeiteiro, producaoLeite);
                        }
                    }
                }

                dataAnterior = dataAtual;
            }
        }

        public bool TemCriaPeriodo2(string idMatriz, DateTime dataCobertura)
        {
            bool temCriaPeriodo = false;
            string dataInicioStr = "";
            string dataFimStr = "";

            if (dataCobertura.Month >= 5)
            {
                dataInicioStr = "01/05/" + (dataCobertura.Year - 1);
                dataFimStr = "30/04/" + (dataCobertura.Year);
            }
            else
            {
                dataInicioStr = "01/05/" + (dataCobertura.Year - 2);
                dataFimStr = "30/04/" + (dataCobertura.Year - 1);
            }

            DateTime dataInicio = Convert.ToDateTime(dataInicioStr);
            DateTime dataFim = Convert.ToDateTime(dataInicioStr);

            var command = new LightBaseCommand(@"select * from FCARNAUBA_CDC where MAT_FK_strIdMatriz = @MAT_FK_strIdMatriz and dtDataCobertura>=@dtDataInicio and dtDataCobertura<=@dtDataFim order by dtDataCobertura");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["MAT_FK_strIdMatriz"]).Value = idMatriz;
            ((LightBaseParameter)command.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command.Parameters["dtDataFim"]).Value = dataCobertura;
            var qReader = command.ExecuteReader();

            string idMatrizCorr = "";
            int i = 0;
            while (qReader.Read())
            {
                DataTable tableMatrizes = (DataTable)qReader["Matrizes"];

                foreach (DataRow rowMatriz in tableMatrizes.Rows)
                {
                    if (rowMatriz["MAT_FK_strIdMatriz"] != DBNull.Value)
                    {
                        idMatrizCorr = (string)rowMatriz["MAT_FK_strIdMatriz"];
                    }

                    if (idMatrizCorr == idMatriz)
                    {
                        i++;
                    }

                }
            }

            if (i > 0)
                temCriaPeriodo = true;

            return temCriaPeriodo;
        }


        public bool TemCriasAnoPecuarioAnterior(string idMatriz, DateTime dataCobertura)
        {
            int nCriasPeriodo = 0;
            bool temCriaPeriodo = false;
            string dataInicioStr = "";
            string dataFimStr = "";

            if (dataCobertura.Month >= 5)
            {
                dataInicioStr = "01/05/" + (dataCobertura.Year - 1);
                dataFimStr = "30/04/" + (dataCobertura.Year);
            }
            else
            {
                dataInicioStr = "01/05/" + (dataCobertura.Year - 2);
                dataFimStr = "30/04/" + (dataCobertura.Year - 1);
            }

            DateTime dataInicio = Convert.ToDateTime(dataInicioStr);
            DateTime dataFim = Convert.ToDateTime(dataFimStr);

            var command = new LightBaseCommand(@"select count(*) from FCARNAUBA_ANIMAIS where strMaeId=@strMaeId and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim order by dtDataNascimento");
            ((LightBaseParameter)command.Parameters["strMaeId"]).Value = idMatriz;
            ((LightBaseParameter)command.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command.Parameters["dtDataFim"]).Value = dataFim;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            qReader.Read();
            nCriasPeriodo = (int)qReader[0];
            qReader.Close();

            if (nCriasPeriodo > 0)
                temCriaPeriodo = true;

            return temCriaPeriodo;
        }

        public double GetMediaPluviometria(DateTime data, string propriedade)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_CONT_PLUVIOMETRICO where dtData=@dtData and diretorio=@diretorio");

            command.Connection = _Connection;

            ((LightBaseParameter)command.Parameters["dtData"]).Value = data;
            ((LightBaseParameter)command.Parameters["diretorio"]).Value = propriedade;
            var qReader = command.ExecuteReader();

            double total = 0;
            int i = 0;

            while (qReader.Read())
            {
                if (qReader["decPluviometria"] != DBNull.Value)
                {
                    total = total + (double)qReader["decPluviometria"];
                    i++;

                }

            }
            qReader.Close();

            if (i > 0)
            {
                return total / i;
            }
            else
            {
                return 0;
            }
        }

        public List<IndiceZootecnico> LotacaoMediaAnual(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double cabecas = 0;
            double lotacaoMediaAnual = 0;
            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento<=@dtDataInicio");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                bool adicionar = true;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Inativo" || movimento == "Vendido" || movimento == "Morto" || movimento == "Abatido") && dataMovimento < dataInicio)
                        {
                            adicionar = false;
                            break;
                        }
                    }
                }
                if (adicionar)
                    cabecas++;
            }

            qReader1.Close();

            var command2 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento > @dtDataInicio and dtDataNascimento <= @dtDataFim");
            command2.Connection = _Connection;
            ((LightBaseParameter)command2.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command2.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command2.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command2.Parameters["dtDataFim"]).Value = dataFim;
            var qReader2 = command2.ExecuteReader();

            while (qReader2.Read())
            {
                bool adicionar = true;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader2["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Inativo" || movimento == "Vendido" || movimento == "Morto" || movimento == "Abatido") && dataMovimento < dataFim)
                        {
                            adicionar = false;
                            break;
                        }
                    }
                }
                if (adicionar)
                    cabecas++;
            }

            qReader2.Close();

            var command3 = new LightBaseCommand(@"select * from FCARNAUBA_PROPRIEDADES_ESTRUTURA where dtData>=@dtDataInicio and dtData<=@dtDataFim and strIdPropriedade=@strIdPropriedade");

            command3.Connection = _Connection;

            ((LightBaseParameter)command3.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command3.Parameters["dtDataFim"]).Value = dataFim;
            ((LightBaseParameter)command3.Parameters["strIdPropriedade"]).Value = idFazenda;
            var qReader3 = command3.ExecuteReader();

            double total = 0;
            double media = 0;
            int i = 0;

            while (qReader3.Read())
            {
                DataTable tablePastagens = (DataTable)qReader3["Pastagens"];
                foreach (DataRow rowPastagem in tablePastagens.Rows)
                {
                    if (rowPastagem["PAS_decAreaTipo"] != DBNull.Value)
                    {
                        total = total + (double)rowPastagem["PAS_decAreaTipo"];
                        i++;

                    }

                }

            }
            qReader3.Close();

            if (i > 0)
            {
                media = total / i;
            }
            else
            {
                media = 0;
            }

            if (media > 0)
            {
                lotacaoMediaAnual = cabecas / media;
            }
            else
            {
                lotacaoMediaAnual = 0;
            }

            lotacaoMediaAnual = Math.Round(lotacaoMediaAnual, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "LOTAÇÃO MÉDIA ANUAL (LMA) em cab/ha";
            indiceZootecnico.Valor = lotacaoMediaAnual;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TaxaDesfrute(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double inicial = 0;
            double final = 0;
            double compras = 0;
            double vendas = 0;
            double taxaDesfrute = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento<=@dtDataInicio");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                bool adicionarInicial = true;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Inativo" || movimento == "Vendido" || movimento == "Morto" || movimento == "Abatido") && dataMovimento <= dataInicio)
                        {
                            adicionarInicial = false;
                            break;
                        }
                    }
                }
                if (adicionarInicial)
                    inicial++;
            }

            qReader1.Close();

            var command2 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento<=@dtDataFim");
            command2.Connection = _Connection;
            ((LightBaseParameter)command2.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command2.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command2.Parameters["dtDataFim"]).Value = dataFim;
            var qReader2 = command2.ExecuteReader();

            while (qReader2.Read())
            {
                bool adicionarFinal = true;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader2["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Inativo" || movimento == "Vendido" || movimento == "Morto" || movimento == "Abatido") && dataMovimento <= dataFim)
                        {
                            adicionarFinal = false;
                            break;
                        }
                    }
                }
                if (adicionarFinal)
                    final++;
            }

            qReader2.Close();


            var command3 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim");
            command3.Connection = _Connection;
            ((LightBaseParameter)command3.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command3.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command3.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command3.Parameters["dtDataFim"]).Value = dataFim;
            var qReader3 = command3.ExecuteReader();

            while (qReader3.Read())
            {
                bool adicionarCompra = false;
                bool adicionarVenda = false;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader3["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if (movimento == "Adquirido" && dataMovimento >= dataInicio && dataMovimento <= dataFim)
                        {
                            adicionarCompra = true;
                        }

                        if (movimento == "Vendido" && dataMovimento >= dataInicio && dataMovimento <= dataFim)
                        {
                            adicionarVenda = true;
                        }
                    }
                }
                if (adicionarCompra)
                    compras++;

                if (adicionarVenda)
                    vendas++;
            }

            qReader3.Close();

            if (inicial > 0)
            {
                taxaDesfrute = (((inicial - final - compras + vendas) / inicial) * 100);
            }
            else
            {
                taxaDesfrute = 0;
            }

            taxaDesfrute = Math.Round(taxaDesfrute, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE DESFRUTE (TD %)";
            indiceZootecnico.Valor = taxaDesfrute;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> ProducaoCarne(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double inicial = 0;
            double final = 0;
            double compras = 0;
            double vendas = 0;
            double producaoCarne = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento<=@dtDataInicio");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                bool adicionarInicial = true;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Inativo" || movimento == "Vendido" || movimento == "Morto" || movimento == "Abatido") && dataMovimento <= dataInicio)
                        {
                            adicionarInicial = false;
                            break;
                        }
                    }
                }
                if (adicionarInicial)
                    inicial++;
            }

            qReader1.Close();

            var command2 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento<=@dtDataFim");
            command2.Connection = _Connection;
            ((LightBaseParameter)command2.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command2.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command2.Parameters["dtDataFim"]).Value = dataFim;
            var qReader2 = command2.ExecuteReader();

            while (qReader2.Read())
            {
                bool adicionarFinal = true;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader2["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Inativo" || movimento == "Vendido" || movimento == "Morto" || movimento == "Abatido") && dataMovimento <= dataFim)
                        {
                            adicionarFinal = false;
                            break;
                        }
                    }
                }
                if (adicionarFinal)
                    final++;
            }

            qReader2.Close();


            var command3 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim");
            command3.Connection = _Connection;
            ((LightBaseParameter)command3.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command3.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command3.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command3.Parameters["dtDataFim"]).Value = dataFim;
            var qReader3 = command3.ExecuteReader();

            while (qReader3.Read())
            {
                bool adicionarCompra = false;
                bool adicionarVenda = false;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader3["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if (movimento == "Adquirido" && dataMovimento >= dataInicio && dataMovimento <= dataFim)
                        {
                            adicionarCompra = true;
                        }

                        if (movimento == "Vendido" && dataMovimento >= dataInicio && dataMovimento <= dataFim)
                        {
                            adicionarVenda = true;
                        }
                    }
                }
                if (adicionarCompra)
                    compras++;

                if (adicionarVenda)
                    vendas++;
            }


            var command4 = new LightBaseCommand(@"select * from FCARNAUBA_PROPRIEDADES_ESTRUTURA where dtData>=@dtDataInicio and dtData<=@dtDataFim and strIdPropriedade=@strIdPropriedade");

            command4.Connection = _Connection;

            ((LightBaseParameter)command4.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command4.Parameters["dtDataFim"]).Value = dataFim;
            ((LightBaseParameter)command4.Parameters["strIdPropriedade"]).Value = idFazenda;
            var qReader4 = command4.ExecuteReader();

            double areaTotal = 0;
            int i = 0;

            while (qReader4.Read())
            {
                DataTable tablePastagens = (DataTable)qReader4["Pastagens"];
                foreach (DataRow rowPastagem in tablePastagens.Rows)
                {
                    if (rowPastagem["PAS_decAreaTipo"] != DBNull.Value)
                    {
                        areaTotal = areaTotal + (double)rowPastagem["PAS_decAreaTipo"];
                        i++;

                    }

                }

            }
            qReader4.Close();



            if (areaTotal > 0)
            {
                producaoCarne = ((inicial - final - compras + vendas) / areaTotal);
            }
            else
            {
                producaoCarne = 0;
            }

            producaoCarne = Math.Round(producaoCarne, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "PRODUÇÃO DE CARNE/ha";
            indiceZootecnico.Valor = producaoCarne;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;

        }

        public List<IndiceZootecnico> MatrizesNBezerros(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda, double nBezerros)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double matrizes = 0;
            double filhos = 0;
            int idAnimal = 0;
            double matrizesNecessarias = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and strSexo = @strSexo and dtDataNascimento <= @dtDataInicio");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["strSexo"]).Value = "Fêmea";
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio.AddDays(-365);
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                idAnimal = Convert.ToInt32(qReader1["id"]);
                int filhosTemp = NumeroCriasMae(idAnimal.ToString(), dataInicio, dataFim);

                if (filhosTemp > 0)
                {
                    filhos = filhos + filhosTemp;
                    matrizes++;
                }
            }

            qReader1.Close();

            if (filhos > 0)
            {
                matrizesNecessarias = ((matrizes * nBezerros)/filhos);
            }

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "MATRIZES NECESSÁRIAS PARA PRODUZIR " + Convert.ToInt32(nBezerros) + " BEZERROS/TEMPO";
            indiceZootecnico.Valor = matrizesNecessarias;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TaxaVendas(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double cabecas = 0;
            double vendidas = 0;
            double taxaVendas = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento<=@dtDataInicio");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                bool adicionar = true;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Inativo" || movimento == "Vendido" || movimento == "Morto" || movimento == "Abatido") && dataMovimento < dataInicio)
                        {
                            adicionar = false;
                            break;
                        }
                    }
                }
                if (adicionar)
                    cabecas++;
            }

            qReader1.Close();

            var command2 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento > @dtDataInicio and dtDataNascimento <= @dtDataFim");
            command2.Connection = _Connection;
            ((LightBaseParameter)command2.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command2.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command2.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command2.Parameters["dtDataFim"]).Value = dataFim;
            var qReader2 = command2.ExecuteReader();

            while (qReader2.Read())
            {
                bool adicionar = true;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader2["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Inativo" || movimento == "Vendido" || movimento == "Morto" || movimento == "Abatido") && dataMovimento < dataFim)
                        {
                            adicionar = false;
                            break;
                        }
                    }
                }
                if (adicionar)
                    cabecas++;
            }

            qReader2.Close();

            var command3 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and MA_strMovimento=@MA_strMovimento and MA_dtDataManejo > @dtDataInicio and MA_dtDataManejo <= @dtDataFim");
            command3.Connection = _Connection;
            ((LightBaseParameter)command3.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command3.Parameters["MA_strMovimento"]).Value = "Vendido";
            ((LightBaseParameter)command3.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command3.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command3.Parameters["dtDataFim"]).Value = dataFim;
            var qReader3 = command2.ExecuteReader();
            qReader3.Read();

            while (qReader3.Read())
            {
                bool adicionar = false;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader3["Historico"];

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        if ((movimento == "Vendido") && dataMovimento >= dataInicio && dataMovimento <= dataFim)
                        {
                            adicionar = true;
                            break;
                        }
                    }
                }
                if (adicionar)
                    vendidas++;
            }

            qReader3.Close();

            if (cabecas > 0)
            {
                taxaVendas = (vendidas / cabecas) * 100;
            }
            else
            {
                taxaVendas = 0;
            }

            taxaVendas = Math.Round(taxaVendas, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE VENDAS (TV %)";
            indiceZootecnico.Valor = taxaVendas;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TaxaMortalidade(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda, string categoria)
        {
            if (categoria == "BEZERROS")
            {
                return TaxaMortalidadeBezerros(dataInicio, dataFim, raca, idFazenda);
            }
            else if (categoria == "JOVENS")
            {
                return TaxaMortalidadeJovens(dataInicio, dataFim, raca, idFazenda);
            }
            else
            {
                return TaxaMortalidadeAdultos(dataInicio, dataFim, raca, idFazenda);
            }

        }

        public List<IndiceZootecnico> TaxaMortalidadeBezerros(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double mortos = 0;
            double nascidos = 0;
            double taxaMortalidadeBezerros = 0;
            long idAnimal = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            //((LightBaseParameter)command1.Parameters["MA_strMovimento"]).Value = "Morto";
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                bool adicionar = false;
                string movimento = "";
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];

                idAnimal = Convert.ToInt64(qReader1["id"]);

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];

                        if ((movimento == "Morto") && !TemDesmame(idAnimal.ToString()))
                        {
                            adicionar = true;
                            break;
                        }
                    }
                }

                if (adicionar)
                    mortos++;

            nascidos++;

            }

            qReader1.Close();

            if (nascidos > 0)
            {
                taxaMortalidadeBezerros = (mortos / nascidos) * 100;
            }
            else
            {
                taxaMortalidadeBezerros = 0;
            }

            taxaMortalidadeBezerros = Math.Round(taxaMortalidadeBezerros, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE MORTALIDADE DE BEZERROS (TMB %)";
            indiceZootecnico.Valor = taxaMortalidadeBezerros;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public bool TemDesmame(string idAnimal)
        {
            bool temDesmame = false;
            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where FK_CP_strIdAnimal = @FK_CP_strIdAnimal");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["FK_CP_strIdAnimal"]).Value = idAnimal;
            var qReader1 = command1.ExecuteReader();
            var retVal = new List<Mensuracao>();
            while (qReader1.Read())
            {
                DataTable tableMensuracoes = (DataTable)qReader1["Mensuracoes"];

                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    var curMen = DataRowToMensuracao(rowMensuracao);

                    if (curMen.CondicaoCriacao == "DESMAMADO" && curMen.IdAnimal == idAnimal)
                    {
                        temDesmame = true;
                        break;
                    }
                }

                if (temDesmame)
                    break;
            }

            qReader1.Close();

            return temDesmame;

        }

        public List<IndiceZootecnico> TaxaMortalidadeJovens(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double mortos = 0;
            double nascidos = 0;
            double taxaMortalidadeJovens = 0;
            long idAnimal = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            //((LightBaseParameter)command1.Parameters["MA_strMovimento"]).Value = "Morto";
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                bool adicionar = false;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DateTime dataNascimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];

                idAnimal = Convert.ToInt64(qReader1["id"]);
                if (qReader1["dtDataNascimento"] != DBNull.Value) dataNascimento = Convert.ToDateTime(qReader1["dtDataNascimento"]);

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        TimeSpan diferenca = (TimeSpan)(dataMovimento - dataNascimento);

                        if ((movimento == "Morto") && diferenca.TotalDays < 450 && TemDesmame(idAnimal.ToString()))
                        {
                            adicionar = true;
                            break;
                        }
                    }
                }

                if (adicionar)
                    mortos++;

            nascidos++;
            }

            qReader1.Close();

            if (nascidos > 0)
            {
                taxaMortalidadeJovens = (mortos / nascidos) * 100;
            }
            else
            {
                taxaMortalidadeJovens = 0;
            }

            taxaMortalidadeJovens = Math.Round(taxaMortalidadeJovens, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE MORTALIDADE DE ANIMAIS JOVENS (TMJ %)";
            indiceZootecnico.Valor = taxaMortalidadeJovens;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }


        public List<IndiceZootecnico> TaxaMortalidadeAdultos(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double mortos = 0;
            double nascidos = 0;
            double taxaMortalidadeAdultos = 0;
            long idAnimal = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            //((LightBaseParameter)command1.Parameters["MA_strMovimento"]).Value = "Morto";
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                bool adicionar = false;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DateTime dataNascimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];

                idAnimal = Convert.ToInt64(qReader1["id"]);
                if (qReader1["dtDataNascimento"] != DBNull.Value) dataNascimento = Convert.ToDateTime(qReader1["dtDataNascimento"]);

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];
                    }
                    if (rowHistorico["MA_dtDataManejo"] != DBNull.Value)
                    {
                        dataMovimento = (DateTime)rowHistorico["MA_dtDataManejo"];

                        TimeSpan diferenca = (TimeSpan)(dataMovimento - dataNascimento);

                        if ((movimento == "Morto") && diferenca.TotalDays >= 450 && TemDesmame(idAnimal.ToString()))
                        {
                            adicionar = true;
                            break;
                        }
                    }
                }

                if (adicionar)
                    mortos++;

                nascidos++;
            }

            qReader1.Close();

            if (nascidos > 0)
            {
                taxaMortalidadeAdultos = (mortos / nascidos) * 100;
            }
            else
            {
                taxaMortalidadeAdultos = 0;
            }

            taxaMortalidadeAdultos = Math.Round(taxaMortalidadeAdultos, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE MORTALIDADE DE ANIMAIS ADULTOS (TMA %)";
            indiceZootecnico.Valor = taxaMortalidadeAdultos;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TaxaAbate(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double inicio = 0;
            double abatidos = 0;
            long idAnimal = 0;
            double taxaAbate = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            //((LightBaseParameter)command1.Parameters["MA_strMovimento"]).Value = "Morto";
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                bool adicionar = false;
                string movimento = "";
                DateTime dataMovimento = new DateTime(01, 01, 01);
                DateTime dataNascimento = new DateTime(01, 01, 01);
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];

                idAnimal = Convert.ToInt64(qReader1["id"]);

                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    if (rowHistorico["MA_strMovimento"] != DBNull.Value)
                    {
                        movimento = (string)rowHistorico["MA_strMovimento"];

                        if (movimento == "Abatido")
                        {
                            adicionar = true;
                            break;
                        }
                    }
                }

                if (adicionar)
                    abatidos++;

                inicio++;
            }

            qReader1.Close();

            if (inicio > 0)
            {
                taxaAbate = (abatidos / inicio) * 100;
            }
            else
            {
                taxaAbate = 0;
            }

            taxaAbate = Math.Round(taxaAbate, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE ABATE (TA %)";
            indiceZootecnico.Valor = taxaAbate;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();
            indiceZootecnico.AnoPecuario = dataInicio.Year + "-" + dataFim.Year;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> IndiceFertilidade(string anoPecuario, string raca, string idFazenda)
        {
            string[] anos = anoPecuario.Split('-');
            int anoInicial = Convert.ToInt32(anos[0]);
            int anoFinal = Convert.ToInt32(anos[1]);

            DateTime dataInicio = new DateTime(anoInicial, 05, 01);
            DateTime dataFim = new DateTime(anoFinal, 04, 30);

            string nomeFazenda = GetNomePropriedade(idFazenda);
            int femeasPrenhas = 0;
            int femeasEmCobertura = 0;
            double indiceFertilidade = 0;
            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_CDC where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataCobertura>=@dtDataInicio and dtDataCobertura<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strIdPropriedade"]).Value = idFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();
            var retVal = new List<Matriz>();
            while (qReader1.Read())
            {
                DataTable tableMatrizes = (DataTable)qReader1["Matrizes"];
                int i = 0;
                foreach (DataRow rowMatriz in tableMatrizes.Rows)
                {
                    var curMat = DataRowToMatriz(rowMatriz);
                    curMat.Id = i;
                    curMat.NomeMatriz = GetNomeAnimal(curMat.IdMatriz);
                    if (curMat.CdcEfetiva)
                        femeasPrenhas++;

                    i++;
                    retVal.Add(curMat);
                }
            }

            qReader1.Close();

            List<Matriz> distinctMatriz = retVal
                .GroupBy(p => p.IdMatriz)
                .Select(g => g.First())
                .ToList();

            femeasEmCobertura = distinctMatriz.Count;

            if (femeasEmCobertura > 0)
            {
                indiceFertilidade = (femeasPrenhas / femeasEmCobertura) * 100;
            }
            else
            {
                indiceFertilidade = 0;
            }

            indiceFertilidade = Math.Round(indiceFertilidade, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "ÍNDICE DE FERTILIDADE (IF)";
            indiceZootecnico.Valor = indiceFertilidade;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.AnoPecuario = anoPecuario;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> IndiceFertilidade(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double femeasPrenhas = 0;
            double femeasEmCobertura = 0;
            double indiceFertilidade = 0;
            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_CDC where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataCobertura>=@dtDataInicio and dtDataCobertura<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strIdPropriedade"]).Value = idFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();
            var retVal = new List<Matriz>();
            while (qReader1.Read())
            {
                DataTable tableMatrizes = (DataTable)qReader1["Matrizes"];
                int i = 0;
                foreach (DataRow rowMatriz in tableMatrizes.Rows)
                {
                    var curMat = DataRowToMatriz(rowMatriz);
                    curMat.Id = i;
                    curMat.NomeMatriz = GetNomeAnimal(curMat.IdMatriz);
                    if (curMat.CdcEfetiva)
                        femeasPrenhas++;

                    i++;
                    retVal.Add(curMat);
                }
            }

            qReader1.Close();

            List<Matriz> distinctMatriz = retVal
                .GroupBy(p => p.IdMatriz)
                .Select(g => g.First())
                .ToList();

            femeasEmCobertura = distinctMatriz.Count;

            if (femeasEmCobertura > 0)
            {
                indiceFertilidade = (femeasPrenhas / femeasEmCobertura) * 100;
            }
            else
            {
                indiceFertilidade = 0;
            }

            indiceFertilidade = Math.Round(indiceFertilidade, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "ÍNDICE DE FERTILIDADE (IF)";
            indiceZootecnico.Valor = indiceFertilidade;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TaxaNatalidade(string anoPecuario, string raca, string idFazenda)
        {
            string[] anos = anoPecuario.Split('-');
            int anoInicial = Convert.ToInt32(anos[0]);
            int anoFinal = Convert.ToInt32(anos[1]);

            DateTime dataInicio = new DateTime(anoInicial, 05, 01);
            DateTime dataFim = new DateTime(anoFinal, 04, 30);

            string nomeFazenda = GetNomePropriedade(idFazenda);
            double bezerrosNascidos = 0;
            double femeasEmCobertura = 0;
            double taxaNatalidade = 0;

            var command1 = new LightBaseCommand(@"select count(*) from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();
            qReader1.Read();

            bezerrosNascidos = (int)qReader1[0];

            qReader1.Close();

            var command2 = new LightBaseCommand(@"select * from FCARNAUBA_CDC where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataCobertura>=@dtDataInicio and dtDataCobertura<=@dtDataFim");
            command2.Connection = _Connection;
            ((LightBaseParameter)command2.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command2.Parameters["strIdPropriedade"]).Value = idFazenda;
            ((LightBaseParameter)command2.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command2.Parameters["dtDataFim"]).Value = dataFim;
            var qReader2 = command2.ExecuteReader();
            var retVal = new List<Matriz>();
            while (qReader2.Read())
            {
                DataTable tableMatrizes = (DataTable)qReader2["Matrizes"];
                int i = 0;
                foreach (DataRow rowMatriz in tableMatrizes.Rows)
                {
                    var curMat = DataRowToMatriz(rowMatriz);
                    curMat.Id = i;
                    curMat.NomeMatriz = GetNomeAnimal(curMat.IdMatriz);
                    i++;
                    retVal.Add(curMat);
                }
            }

            qReader2.Close();

            List<Matriz> distinctMatriz = retVal
                .GroupBy(p => p.IdMatriz)
                .Select(g => g.First())
                .ToList();

            femeasEmCobertura = distinctMatriz.Count;

            if (femeasEmCobertura > 0)
            {
                taxaNatalidade = (bezerrosNascidos / femeasEmCobertura) * 100;
            }
            else
            {
                taxaNatalidade = 0;
            }

            taxaNatalidade = Math.Round(taxaNatalidade, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE NATALIDADE (TN)";
            indiceZootecnico.Valor = taxaNatalidade;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.AnoPecuario = anoPecuario;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TaxaMortalidadeIntrauterina(string anoPecuario, string raca, string idFazenda)
        {
            string[] anos = anoPecuario.Split('-');
            int anoInicial = Convert.ToInt32(anos[0]);
            int anoFinal = Convert.ToInt32(anos[1]);

            DateTime dataInicio = new DateTime(anoInicial, 05, 01);
            DateTime dataFim = new DateTime(anoFinal, 04, 30);

            string nomeFazenda = GetNomePropriedade(idFazenda);
            double femeasQuePariram = 0;
            double femeasPrenhas = 0;
            double taxaMortalidadeIntrauterina = 0;

            var command1 = new LightBaseCommand(@"select dtDataNascimento, strMaeId from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento>=@dtDataInicio and dtDataNascimento<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();
            qReader1.Read();

            var animais = new List<Animal>();

            while (qReader1.Read())
            {
                Animal animal = new Animal();
                if (qReader1["dtDataNascimento"] != DBNull.Value) animal.DataNascimento = (DateTime)qReader1["dtDataNascimento"];
                if (qReader1["strMaeId"] != DBNull.Value) animal.StrMaeId = qReader1["strMaeId"].ToString();
                animais.Add(animal);

            }

            List<Animal> distinctAnimal = animais
                .GroupBy(p => new { p.DataNascimento, p.StrMaeId })
                .Select(g => g.First())
                .ToList();

            femeasQuePariram = distinctAnimal.Count;

            qReader1.Close();

            var command2 = new LightBaseCommand(@"select * from FCARNAUBA_CDC where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataCobertura>=@dtDataInicio and dtDataCobertura<=@dtDataFim");
            command2.Connection = _Connection;
            ((LightBaseParameter)command2.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command2.Parameters["strIdPropriedade"]).Value = idFazenda;
            ((LightBaseParameter)command2.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command2.Parameters["dtDataFim"]).Value = dataFim;
            var qReader2 = command2.ExecuteReader();
            var retVal = new List<Matriz>();
            while (qReader2.Read())
            {
                DataTable tableMatrizes = (DataTable)qReader2["Matrizes"];
                int i = 0;
                foreach (DataRow rowMatriz in tableMatrizes.Rows)
                {
                    var curMat = DataRowToMatriz(rowMatriz);
                    curMat.Id = i;
                    curMat.NomeMatriz = GetNomeAnimal(curMat.IdMatriz);
                    if (curMat.CdcEfetiva)
                        femeasPrenhas++;
                }
            }

            qReader2.Close();

            if (femeasPrenhas > 0)
            {
                taxaMortalidadeIntrauterina = (femeasPrenhas - (femeasQuePariram / femeasPrenhas)) * 100;
            }
            else
            {
                taxaMortalidadeIntrauterina = 0;
            }

            taxaMortalidadeIntrauterina = Math.Round(taxaMortalidadeIntrauterina, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE MORTALIDADE INTRAUTERINA (IMU)";
            indiceZootecnico.Valor = taxaMortalidadeIntrauterina;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.AnoPecuario = anoPecuario;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TaxaDesmame(string anoPecuario, string raca, string idFazenda)
        {
            string[] anos = anoPecuario.Split('-');
            int anoInicial = Convert.ToInt32(anos[0]);
            int anoFinal = Convert.ToInt32(anos[1]);

            DateTime dataInicio = new DateTime(anoInicial, 05, 01);
            DateTime dataFim = new DateTime(anoFinal, 04, 30);

            string nomeFazenda = GetNomePropriedade(idFazenda);
            double bezerrosDesmamados = 0;
            double femeasEmCobertura = 0;
            double taxaDesmame = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataControle>=@dtDataInicio and dtDataControle<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strIdPropriedade"]).Value = idFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();
            var retVal = new List<Mensuracao>();
            while (qReader1.Read())
            {
                DataTable tableMensuracoes = (DataTable)qReader1["Mensuracoes"];

                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    var curMen = DataRowToMensuracao(rowMensuracao);

                    if (curMen.CondicaoCriacao == "DESMAMADO")
                        retVal.Add(curMen);
                }
            }

            qReader1.Close();

            List<Mensuracao> distinctMensuracao = retVal
                .GroupBy(p => new { p.DataDesmame, p.IdAnimal })
                .Select(g => g.First())
                .ToList();

            bezerrosDesmamados = distinctMensuracao.Count;

            var command2 = new LightBaseCommand(@"select * from FCARNAUBA_CDC where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataCobertura>=@dtDataInicio and dtDataCobertura<=@dtDataFim");
            command2.Connection = _Connection;
            ((LightBaseParameter)command2.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command2.Parameters["strIdPropriedade"]).Value = idFazenda;
            ((LightBaseParameter)command2.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command2.Parameters["dtDataFim"]).Value = dataFim;
            var qReader2 = command2.ExecuteReader();
            var retVal2 = new List<Matriz>();
            while (qReader2.Read())
            {
                DataTable tableMatrizes = (DataTable)qReader2["Matrizes"];
                int i = 0;
                foreach (DataRow rowMatriz in tableMatrizes.Rows)
                {
                    var curMat = DataRowToMatriz(rowMatriz);
                    curMat.Id = i;
                    i++;
                    retVal2.Add(curMat);
                }
            }

            qReader2.Close();

            List<Matriz> distinctMatriz = retVal2
                .GroupBy(p => p.IdMatriz)
                .Select(g => g.First())
                .ToList();

            femeasEmCobertura = distinctMatriz.Count;

            if (femeasEmCobertura > 0)
            {
                taxaDesmame = (bezerrosDesmamados / femeasEmCobertura) * 100;
            }
            else
            {
                taxaDesmame = 0;
            }

            taxaDesmame = Math.Round(taxaDesmame, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE DESMAME (TD)";
            indiceZootecnico.Valor = taxaDesmame;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.AnoPecuario = anoPecuario;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> RelacaoDesmama(string anoPecuario, string raca, string idFazenda)
        {
            string[] anos = anoPecuario.Split('-');
            int anoInicial = Convert.ToInt32(anos[0]);
            int anoFinal = Convert.ToInt32(anos[1]);

            DateTime dataInicio = new DateTime(anoInicial, 05, 01);
            DateTime dataFim = new DateTime(anoFinal, 04, 30);

            string nomeFazenda = GetNomePropriedade(idFazenda);
            double somaRelacoes = 0;
            double mediaRelacoes = 0;

            var command1 = new LightBaseCommand(@"select * from FCARNAUBA_LOTE_CONTROLE_PONDERAL where strRaca = @strRaca and strIdPropriedade = @strIdPropriedade and dtDataControle>=@dtDataInicio and dtDataControle<=@dtDataFim");
            command1.Connection = _Connection;
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strIdPropriedade"]).Value = idFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            ((LightBaseParameter)command1.Parameters["dtDataFim"]).Value = dataFim;
            var qReader1 = command1.ExecuteReader();
            var retVal = new List<Mensuracao>();
            while (qReader1.Read())
            {
                DataTable tableMensuracoes = (DataTable)qReader1["Mensuracoes"];

                foreach (DataRow rowMensuracao in tableMensuracoes.Rows)
                {
                    var curMen = DataRowToMensuracao(rowMensuracao);

                    if (curMen.CondicaoCriacao == "DESMAMADO")
                    {
                        if (curMen.PesoMaeDesmame > 0)
                        {
                            curMen.RelacaoDesmama = (curMen.Peso / curMen.PesoMaeDesmame) * 100;
                        }
                        else
                        {
                            curMen.RelacaoDesmama = 0;
                        }

                        retVal.Add(curMen);
                    }
                }
            }

            qReader1.Close();


            foreach (Mensuracao mensuracao in retVal)
            {

                somaRelacoes = somaRelacoes + mensuracao.RelacaoDesmama;

            } 

            if (retVal.Count > 0)
            {
                double relacoes = retVal.Count;
                mediaRelacoes = somaRelacoes / relacoes;
            }

            mediaRelacoes = Math.Round(mediaRelacoes, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "RELAÇÃO DE DESMAMA (%)";
            indiceZootecnico.Valor = mediaRelacoes;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.AnoPecuario = anoPecuario;

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TaxaCrescimentoVegetativo(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            double taxaCrescimentoVegetativo = 0.05;
            string nomeFazenda = GetNomePropriedade(idFazenda);
            double nCabecasInicio = 0;
            double nCabecasFim = 0;

            var command1 = new LightBaseCommand(@"select id, Historico from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento<=@dtDataInicio");
            ((LightBaseParameter)command1.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command1.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command1.Parameters["dtDataInicio"]).Value = dataInicio;
            command1.Connection = _Connection;
            var qReader1 = command1.ExecuteReader();

            while (qReader1.Read())
            {
                DataTable tableHistoricos = (DataTable)qReader1["Historico"];
                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    Historico historico = DataRowToHistorico(rowHistorico);

                    if (historico.Movimento == "adquirido")
                        break;

                }

                nCabecasInicio++;

            }
            qReader1.Close();

            var command2 = new LightBaseCommand(@"select id, Historico from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda and dtDataNascimento<=@dtDataFim");
            ((LightBaseParameter)command2.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command2.Parameters["strNomeFazenda"]).Value = nomeFazenda;
            ((LightBaseParameter)command2.Parameters["dtDataFim"]).Value = dataFim;
            command2.Connection = _Connection;
            var qReader2 = command2.ExecuteReader();

            while (qReader2.Read())
            {
                DataTable tableHistoricos = (DataTable)qReader2["Historico"];
                foreach (DataRow rowHistorico in tableHistoricos.Rows)
                {
                    Historico historico = DataRowToHistorico(rowHistorico);

                    if (historico.Movimento == "adquirido")
                        break;
                }

                nCabecasFim++;

            }

            if (nCabecasInicio > 0)
            {
                taxaCrescimentoVegetativo = (((nCabecasFim - nCabecasInicio) / nCabecasInicio) * 100);
            }
            else
            {
                taxaCrescimentoVegetativo = 0.00;
            }

            taxaCrescimentoVegetativo = Math.Round(taxaCrescimentoVegetativo, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "TAXA DE CRESCIMENTO VEGETATIVO (TCV%)";
            indiceZootecnico.Valor = taxaCrescimentoVegetativo;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Raca = raca;
            indiceZootecnico.Propriedade = nomeFazenda;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> RQMedio(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            double rendQueijeiroTotal = 0;
            double rendQueijeiroMedia = 0;

            string filter = "";

            filter = AddParametro(filter, "PL_vfSairControle", "1");
            filter = AddParametro(filter, "strIdPropriedade", idFazenda);
            filter = AddParametro(filter, "strRaca", raca);
            filter = AddParametroData(filter, "dtDataControle", dataInicio.ToString("dd/MM/yyyy"), ">=");
            filter = AddParametroData(filter, "dtDataControle", dataFim.ToString("dd/MM/yyyy"), "<=");

            var command = new LightBaseCommand("textsearch filterchildren in FCARNAUBA_LOTE_CONTROLE_LEITEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            List<ProducaoLeite> producoesLeite = new List<ProducaoLeite>();
            int i = 0;
            while (qReader.Read())
            {
                var retVal = new Lote();

                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.Id = Convert.ToInt64(qReader["id"]);
                if (qReader["dtDataControle"] != DBNull.Value) retVal.DataControle = (DateTime)qReader["dtDataControle"];

                DataTable tableProducoesLeite = (DataTable)qReader["Producao_de_Leite"];
                
                foreach (DataRow rowProducoesLeite in tableProducoesLeite.Rows)
                {
                    ProducaoLeite producaoLeite = DataRowToProducaoLeiteCompleto(rowProducoesLeite);

                    if (producaoLeite.SairControleSr == "SIM")
                    {
                        producaoLeite.Id = i;
                        var prodLeite = GetGordProtMediaMatriz(producaoLeite.IdMatriz, dataInicio.Year, idFazenda, raca);
                        var prod = GetProducaoAcumulada(retVal.DataControle, retVal.Id.ToString(), producaoLeite.IdMatriz);
                        double rendQueijeiro = (((3.5 * prodLeite.ProtMedia) + (1.23 * prodLeite.GordMedia) - 0.88) / 100) * prod.Acumulada;

                        if (rendQueijeiro > 0)
                        {
                            rendQueijeiroTotal = rendQueijeiroTotal + rendQueijeiro;
                            i++;
                        }
                    }
                    
                }

            }
            qReader.Close();

            if (i > 0)
            {
                rendQueijeiroMedia = rendQueijeiroTotal / i;
            }
            else
            {
                rendQueijeiroMedia = 0;
            }

            rendQueijeiroMedia = Math.Round(rendQueijeiroMedia, 2);

            IndiceZootecnico indiceZootecnico = new IndiceZootecnico();
            indiceZootecnico.Indice = "RQ MÉDIO";
            indiceZootecnico.Valor = rendQueijeiroMedia;
            indiceZootecnico.ValorReferencia = 0;
            indiceZootecnico.Periodo = "Período: " + dataInicio.ToShortDateString() + " a " + dataFim.ToShortDateString();

            List<IndiceZootecnico> indices = new List<IndiceZootecnico>();

            indices.Add(indiceZootecnico);

            return indices;
        }

        public List<IndiceZootecnico> TodasTaxas(string anoPecuario, string raca, string idFazenda)
        {

            string[] anos = anoPecuario.Split('-');
            int anoInicial = Convert.ToInt32(anos[0]);
            int anoFinal = Convert.ToInt32(anos[1]);
            DateTime dataInicio = new DateTime(anoInicial, 05, 01);
            DateTime dataFim = new DateTime(anoFinal, 04, 30);
            string nomeFazenda = GetNomePropriedade(idFazenda);

            IndiceZootecnico lotacaoMediaAnual = LotacaoMediaAnual(dataInicio, dataFim, raca, idFazenda)[0];
            IndiceZootecnico taxaFertilidade = IndiceFertilidade(anoPecuario, raca, idFazenda)[0];
            IndiceZootecnico taxaNatalidade = TaxaNatalidade(anoPecuario, raca, idFazenda)[0];
            IndiceZootecnico taxaMortalidadeIntrauterina = TaxaMortalidadeIntrauterina(anoPecuario, raca, idFazenda)[0];
            IndiceZootecnico taxaDesmame = TaxaDesmame(anoPecuario, raca, idFazenda)[0];

            List<IndiceZootecnico> todosIndices = new List<IndiceZootecnico>();
            todosIndices.Add(lotacaoMediaAnual);
            todosIndices.Add(taxaFertilidade);
            todosIndices.Add(taxaNatalidade);
            todosIndices.Add(taxaMortalidadeIntrauterina);
            todosIndices.Add(taxaDesmame);

            return todosIndices;

        }

        public GrupoFinanceiro[] ObtemGrupos(string criterio)
        {
            LightBaseDataReader qReader = null;

            try
            {
                string filtro = "";
                if (!string.IsNullOrEmpty(criterio))
                {
                    filtro = "where strDescricao_Grupo = @strDescricao_Grupo";
                }

                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO " + filtro + " order by FK_intID_Grupo_Sup, kintID_Grupo");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo"]).Value = criterio;
                qReader = command.ExecuteReader();

                List<GrupoFinanceiro> grupos = new List<GrupoFinanceiro>();

                while (qReader.Read())
                {
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);
                    grupos.Add(grupo);
                }
                qReader.Close();

                MontaHierarquiaDePais(grupos);

                return grupos.ToArray();
            }
            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public GrupoFinanceiro[] ObtemFilhosDe(int idGrupoPai)
        {
            LightBaseDataReader qReader = null;

            try
            {
                string criterio = " = " + idGrupoPai;
                if (idGrupoPai == new GrupoFinanceiro().IdNulo)
                {
                    criterio = " = 0 ";
                }

                var command = new LightBaseCommand("select * from FCARNAUBA_GRUPO_FINANCEIRO where FK_intID_Grupo_Sup " + criterio + " order by FK_intID_Grupo_Sup, kintID_Grupo");
                command.Connection = _Connection;
                qReader = command.ExecuteReader();

                List<GrupoFinanceiro> grupos = new List<GrupoFinanceiro>();

                while (qReader.Read())
                {
                    GrupoFinanceiro grupo = new GrupoFinanceiro();

                    grupo.IdGrupo = Convert.ToInt32(qReader["kintID_Grupo"]);
                    grupo.Descricao = Convert.ToString(qReader["strDescricao_Grupo"]);
                    int idPai = Convert.ToInt32(qReader["kintID_Grupo"]);
                    if (idPai > 0)
                    {
                        grupo.IdGrupoPai = Convert.ToInt32(idPai);
                    }

                    grupos.Add(grupo);
                }

                return grupos.ToArray();
            }
            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public GrupoFinanceiro[] ObtemGrupo(long idGrupo, bool comHierarquia)
        {
            LightBaseDataReader qReader = null;
            try
            {

                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["kintID_Grupo"]).Value = idGrupo;
                qReader = command.ExecuteReader();

                List<GrupoFinanceiro> grupos = new List<GrupoFinanceiro>();

                if (qReader.Read())
                {
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);
                    qReader.Close();
                    grupos.Add(grupo);
                    if (comHierarquia)
                    {
                        MontaHierarquiaDePais(grupos);
                    }
                }

                return grupos.ToArray();
            }
            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }


        public List<RItemFinanceiro> ObtemItensFinanceiros(CriterioPesquisaFinanceiro criterio)
        {
            double entradas = 0;
            double desembolsos = 0;

            try
            {

                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO order by strOrdem");
                command.Connection = _Connection;
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.Descricao = grupo.Descricao;
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);
                    itens.Add(item);

                    if (grupo.Descricao == "1 - RECEITA")
                        entradas = item.ValorTotal;

                    if (grupo.Descricao == "2 - CUSTO")
                        desembolsos = item.ValorTotal;
                }
                qReader.Close();

                RItemFinanceiro balanco = new RItemFinanceiro();
                balanco.Descricao = "BALANÇO FINANCEIRO";
                balanco.ValorTotal = entradas - desembolsos; ;
                itens.Add(balanco);

                return itens;
            }
            finally
            {

            }
        }

        public InformacoesFinanceiras ObtemInformacoesFinanceiras(CriterioPesquisaFinanceiro criterio)
        {
            double entradas = 0;
            double desembolsos = 0;
            double custosFixos = 0;
            double custosVariaveis = 0;
            double custosAdministrativos = 0;
            double custosTributarios = 0;
            double custoAlimentar = 0;
            double vendaLeite = 0;

            try
            {

                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO order by strOrdem");
                command.Connection = _Connection;
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.Descricao = grupo.Descricao;
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);
                    itens.Add(item);

                    if (grupo.Descricao == "1 - RECEITA")
                        entradas = item.ValorTotal;

                    if (grupo.Descricao == "2 - CUSTO")
                        desembolsos = item.ValorTotal;

                    if (grupo.Descricao == "2.1.1 - CUSTOS FIXOS")
                        custosFixos = item.ValorTotal;

                    if (grupo.Descricao == "2.1.2 - CUSTOS VARIÁVEIS")
                        custosVariaveis = item.ValorTotal;

                    if (grupo.Descricao.Contains("DESPESAS ADMINISTRATIVAS"))
                        custosAdministrativos = custosAdministrativos + item.ValorTotal;

                    if (grupo.Descricao.Contains("IMPOSTOS"))
                        custosTributarios = custosTributarios + item.ValorTotal;

                    if (grupo.Descricao.Contains("RAÇÃO"))
                        custoAlimentar = custoAlimentar + item.ValorTotal;

                    if (grupo.Descricao.Contains("2.1 - BOVINO"))
                        vendaLeite = vendaLeite + item.ValorTotal;
                }
                qReader.Close();

                InformacoesFinanceiras informacoes = new InformacoesFinanceiras();
                informacoes.Entradas = entradas;
                informacoes.Desembolsos = desembolsos;
                informacoes.CustosFixos = custosFixos;
                informacoes.CustosVariaveis = custosVariaveis;
                informacoes.CustoAdmintrativo = custosAdministrativos;
                informacoes.CustoTributario = custosTributarios;
                informacoes.CustoAlimentar = custoAlimentar;
                informacoes.VendaLeite = vendaLeite;
                informacoes.CustoAlimentarHA = 0;

                return informacoes;
            }
            finally
            {

            }
        }

        public List<InformacoesFinanceiras> ObtemListaInformacoesFinanceiras(CriterioPesquisaFinanceiro criterio)
        {

            InformacoesFinanceiras informacao = ObtemInformacoesFinanceiras(criterio);
            List<InformacoesFinanceiras> informacoes = new List<InformacoesFinanceiras>();

            if (informacao != null)
            {
                informacao.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                informacao.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                informacoes.Add(informacao);

            }

            return informacoes;
        }


        public List<RItemFinanceiro> ObtemItensFinanceirosCompleto(CriterioPesquisaFinanceiro criterio)
        {
            double entradas = 0;
            double desembolsos = 0;

            try
            {

                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO order by strOrdem");
                command.Connection = _Connection;
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.Descricao = grupo.Descricao;
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);
                    itens.Add(item);

                    if (grupo.Descricao == "1 - RECEITA")
                        entradas = item.ValorTotal;

                    if (grupo.Descricao == "2 - CUSTO")
                        desembolsos = item.ValorTotal;

                    CriterioPesquisaFinanceiro criterioCompleto = criterio;
                    criterioCompleto.IdGrupo = grupo.IdGrupo;

                    List<ItemFinanceiro> itensCompleto = ConsultaFinanceiro(criterioCompleto).ToList();

                    foreach (ItemFinanceiro itemCompleto in itensCompleto)
                    {
                        RItemFinanceiro rItemCompleto = new RItemFinanceiro();
                        rItemCompleto.Descricao = itemCompleto.Descricao;
                        rItemCompleto.Quantidade = itemCompleto.Quantidade;
                        rItemCompleto.ValorUnitario = itemCompleto.ValorUnitario;
                        rItemCompleto.ValorTotal = itemCompleto.ValorTotal;
                        itens.Add(rItemCompleto);

                    }
                }
                qReader.Close();

                RItemFinanceiro balanco = new RItemFinanceiro();
                balanco.Descricao = "BALANÇO FINANCEIRO";
                balanco.ValorTotal = entradas - desembolsos; ;
                itens.Add(balanco);

                return itens;
            }
            finally
            {

            }
        }

        public List<RItemFinanceiro> ObtemCustosItensFinanceiros(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo1 or kintID_Grupo = @kintID_Grupo2 or kintID_Grupo = @kintID_Grupo3");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["kintID_Grupo1"]).Value = "29";
                ((LightBaseParameter)command.Parameters["kintID_Grupo2"]).Value = "54";
                ((LightBaseParameter)command.Parameters["kintID_Grupo3"]).Value = "71";
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    if (grupo.IdGrupo == 29)
                        item.Descricao = "CUSTEIO PRODUTIVO";

                    if (grupo.IdGrupo == 54)
                        item.Descricao = "CUSTEIO ADMINISTRATIVO";

                    if (grupo.IdGrupo == 71)
                        item.Descricao = "CUSTEIO TRIBUTÁRIO";

                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);

                    itens.Add(item);
                }
                qReader.Close();

                return itens;
            }
            finally
            {

            }
        }

        public List<RItemFinanceiro> ObtemCustosFixos(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo1");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["kintID_Grupo1"]).Value = "294";
                
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.Descricao = ObtemGrupo(grupo.IdGrupoPai.ToString()).Descricao;
                    item.Descricao = item.Descricao.Substring(8, item.Descricao.Length - 8);
                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);

                    itens.Add(item);
                }
                qReader.Close();

                return itens;
            }
            finally
            {

            }
        }

        public List<RItemFinanceiro> ObtemCustosVariaveis(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo1");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["kintID_Grupo1"]).Value = "419";
                
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.Descricao = ObtemGrupo(grupo.IdGrupoPai.ToString()).Descricao;
                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);

                    itens.Add(item);
                }
                qReader.Close();

                return itens;
            }
            finally
            {

            }
        }

        public List<RItemFinanceiro> ObtemVendasAnimais(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                string filter = "";
                filter = AddParametro(filter, criterio.TodosOsCampos);

                filter = AddParametro(filter, "vfVendaAnimais", "1");

                if (criterio.IdEmpresa != 0)
                    filter = AddParametro(filter, "FK_IdEmpresa", criterio.IdEmpresa.ToString());

                if (criterio.Data != null)
                    filter = AddParametroData(filter, "dtData", criterio.Data.Value.ToShortDateString(), ">=");

                if (criterio.DataInicio != null)
                    filter = AddParametroData(filter, "dtData", criterio.DataInicio.Value.ToShortDateString(), ">=");

                if (criterio.DataFim != null)
                    filter = AddParametroData(filter, "dtData", criterio.DataFim.Value.ToShortDateString(), "<=");

                var command = new LightBaseCommand("textsearch in FCARNAUBA_FINANCEIRO " + filter);
                command.Connection = _Connection;

                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    
                    string idEmpresa = Convert.ToString(qReader["FK_IdEmpresa"]);
                    string idsPropriedade = Convert.ToString(qReader["strPropriedadeComp"]);
                    item.Descricao = Convert.ToString(qReader["strDescricao"]);
                    item.NomePropriedade = GetNomePropriedadeComp(idsPropriedade);
                    item.Cliente = GetNomeEmpresa(idEmpresa);
                    item.Quantidade = Convert.ToInt32(qReader["intQuantidade"]);
                    item.Data = Convert.ToDateTime(qReader["dtData"]);
                    item.ValorTotal = Convert.ToDouble(qReader["moeValorTotal"]);
                    item.FormaPagamento = Convert.ToString(qReader["strFormaPagamento"]);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();

                    itens.Add(item);
                }
                qReader.Close();

                return itens;
            }
            finally
            {

            }
        }

        public List<RItemFinanceiro> ObtemBalancoFinanceiro(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo1 or kintID_Grupo = @kintID_Grupo2");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["kintID_Grupo1"]).Value = "1";
                ((LightBaseParameter)command.Parameters["kintID_Grupo2"]).Value = "292";
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.Id = grupo.IdGrupo;
                    item.Descricao = ObtemGrupo(grupo.IdGrupo.ToString()).Descricao;
                    item.Descricao = item.Descricao.Substring(4, item.Descricao.Length - 4);
                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);

                    itens.Add(item);
                }
                qReader.Close();

                return itens;
            }
            finally
            {

            }
        }

        public List<RItemFinanceiro> ObtemInvestimentos(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where ( strDescricao_Grupo = @strDescricao_Grupo1 or strDescricao_Grupo = @strDescricao_Grupo2 or strDescricao_Grupo = @strDescricao_Grupo3 or strDescricao_Grupo = @strDescricao_Grupo4 or strDescricao_Grupo = @strDescricao_Grupo5 or strDescricao_Grupo = @strDescricao_Grupo6 or strDescricao_Grupo = @strDescricao_Grupo7 or strDescricao_Grupo = @strDescricao_Grupo8 or strDescricao_Grupo = @strDescricao_Grupo9 or strDescricao_Grupo = @strDescricao_Grupo10) and strIds_Grupo_Sup = @strIds_Grupo_Sup");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo1"]).Value = "CLONE";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo2"]).Value = "COMPRA";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo3"]).Value = "CONSTRUÇÕES";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo4"]).Value = "EMBRIÕES";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo5"]).Value = "EQUIPAMENTOS";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo6"]).Value = "MÁQUINAS";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo7"]).Value = "PLANTIO";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo8"]).Value = "SÊMEN";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo9"]).Value = "TERRAS/ÁREAS";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo10"]).Value = "OUTROS";
                ((LightBaseParameter)command.Parameters["strIds_Grupo_Sup"]).Value = "292 532*";
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.Id = grupo.IdGrupo;
                    item.Descricao = grupo.Descricao;

                    string[] descricoes = item.Descricao.Split('-');
                    item.Descricao = descricoes[1];
                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);
                    if (item.ValorTotal > 0)
                    {
                        itens.Add(item);
                    }
                }
                qReader.Close();

                return itens;
            }
            finally
            {

            }
        }

        public List<RItemFinanceiro> ObtemCustos(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where ( strDescricao_Grupo = @strDescricao_Grupo1 or strDescricao_Grupo = @strDescricao_Grupo2 or strDescricao_Grupo = @strDescricao_Grupo3 or strDescricao_Grupo = @strDescricao_Grupo4 or strDescricao_Grupo = @strDescricao_Grupo5 or strDescricao_Grupo = @strDescricao_Grupo6 or strDescricao_Grupo = @strDescricao_Grupo7 or strDescricao_Grupo = @strDescricao_Grupo8 or strDescricao_Grupo = @strDescricao_Grupo9 or strDescricao_Grupo = @strDescricao_Grupo10 or strDescricao_Grupo = @strDescricao_Grupo11 or strDescricao_Grupo = @strDescricao_Grupo12 or strDescricao_Grupo = @strDescricao_Grupo13 or strDescricao_Grupo = @strDescricao_Grupo14 or strDescricao_Grupo = @strDescricao_Grupo15 or strDescricao_Grupo = @strDescricao_Grupo16 or strDescricao_Grupo = @strDescricao_Grupo17 or strDescricao_Grupo = @strDescricao_Grupo18 or strDescricao_Grupo = @strDescricao_Grupo19) and strIds_Grupo_Sup = @strIds_Grupo_Sup");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo1"]).Value = "ÁGUA";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo2"]).Value = "ALUGUEL";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo3"]).Value = "ARRENDAMENTO";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo4"]).Value = "DESPESAS*";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo5"]).Value = "IMPOSTOS";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo6"]).Value = "INTERNET";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo7"]).Value = "MÃO*";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo8"]).Value = "PRÓ-LABORE";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo9"]).Value = "SISTEMAS";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo10"]).Value = "TELEFONE";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo11"]).Value = "ADUBOS";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo12"]).Value = "BONIFICAÇÃO";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo13"]).Value = "COMISSÃO";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo14"]).Value = "DEFENSIVOS";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo15"]).Value = "FATURAMENTO";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo16"]).Value = "INOCULANTES";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo17"]).Value = "INSUMOS";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo18"]).Value = "RAÇÃO";
                ((LightBaseParameter)command.Parameters["strDescricao_Grupo19"]).Value = "SEMENTE";
                ((LightBaseParameter)command.Parameters["strIds_Grupo_Sup"]).Value = "292 293*";
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.Id = grupo.IdGrupo;
                    item.Descricao = grupo.Descricao;

                    string[] descricoes = item.Descricao.Split('-');
                    item.Descricao = descricoes[1];
                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);
                    if (item.ValorTotal > 0)
                    {
                        itens.Add(item);
                    }
                }
                qReader.Close();

                return itens;
            }
            finally
            {

            }
        }

        public List<RItemFinanceiro> ObtemCusteioTotalXInvestimentos(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo1 or kintID_Grupo = @kintID_Grupo2");
                command.Connection = _Connection;
                ((LightBaseParameter)command.Parameters["kintID_Grupo1"]).Value = "293";
                ((LightBaseParameter)command.Parameters["kintID_Grupo2"]).Value = "532";
                var qReader = command.ExecuteReader();

                List<RItemFinanceiro> itens = new List<RItemFinanceiro>();

                while (qReader.Read())
                {
                    RItemFinanceiro item = new RItemFinanceiro();
                    GrupoFinanceiro grupo = CriaGrupoDe(qReader);

                    item.Id = grupo.IdGrupo;
                    item.Descricao = ObtemGrupo(grupo.IdGrupo.ToString()).Descricao;
                    item.Descricao = item.Descricao.Substring(6, item.Descricao.Length - 6);
                    item.NomePropriedade = GetNomePropriedade(criterio.PropriedadeComp);
                    item.Periodo = criterio.DataInicio.Value.ToShortDateString() + " a " + criterio.DataFim.Value.ToShortDateString();
                    item.ValorTotal = GetTotalItensFinanceiro(criterio, grupo);

                    itens.Add(item);
                }
                qReader.Close();

                return itens;
            }
            finally
            {

            }
        }


        private void MontaHierarquiaDePais(IList<GrupoFinanceiro> grupos)
        {
            IDictionary<int, GrupoFinanceiro> gruposPais = new Dictionary<int, GrupoFinanceiro>();
            foreach (GrupoFinanceiro grupo in grupos)
            {
                AddPaiDe(grupo, gruposPais);
            }
            foreach (int idPai in gruposPais.Keys)
            {
                grupos.Insert(0, gruposPais[idPai]);
            }
        }

        private void AddPaiDe(GrupoFinanceiro grupo, IDictionary<int, GrupoFinanceiro> grupos)
        {
            if (grupos.ContainsKey(grupo.IdGrupoPai))
            {
                return;
            }


            var command = new LightBaseCommand(@"select * from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["kintID_Grupo"]).Value = grupo.IdGrupoPai;
            var qReader = command.ExecuteReader();

            if (qReader.Read())
            {
                GrupoFinanceiro grupoPai = CriaGrupoDe(qReader);
                grupos[grupoPai.IdGrupo] = grupoPai;
                qReader.Close();
                AddPaiDe(grupoPai, grupos);
            }
            else
            {
                qReader.Close();
            }
        }

        private static GrupoFinanceiro CriaGrupoDe(LightBaseDataReader readerGrupo)
        {
            GrupoFinanceiro grupo = new GrupoFinanceiro();

            grupo.IdGrupo = Convert.ToInt32(readerGrupo["kintID_Grupo"]);
            grupo.Descricao = Convert.ToString(readerGrupo["strDescricao_Grupo"]);
            if (readerGrupo["strIds_Grupo_Sup"] != DBNull.Value)
            {
                grupo.IdsGrupoSup = Convert.ToString(readerGrupo["strIds_Grupo_Sup"]);
            }
            else
            {
                grupo.IdsGrupoSup = "";
            }
            if (readerGrupo["vfEhUltimoNoh"] != DBNull.Value)
            {
                grupo.EhUltimoNoh = (bool)readerGrupo["vfEhUltimoNoh"];
            }
            else
            {
                grupo.EhUltimoNoh = false;
            }
            Object idGrupoPai = readerGrupo["FK_intID_Grupo_Sup"];
            if (idGrupoPai is int)
            {
                grupo.IdGrupoPai = Convert.ToInt32(idGrupoPai);
            }
            return grupo;
        }

        public GrupoFinanceiro ObtemGrupo(string id)
        {
            GrupoFinanceiro grupo = new GrupoFinanceiro();
            var command = new LightBaseCommand(@"select strDescricao_Grupo, strIds_Grupo_Sup from FCARNAUBA_GRUPO_FINANCEIRO where kintID_Grupo = @kintID_Grupo");
            ((LightBaseParameter)command.Parameters["kintID_Grupo"]).Value = id;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {
                grupo.IdGrupo = Convert.ToInt32(id);
                grupo.Descricao = (string)qReader["strDescricao_Grupo"];

                if (qReader["strIds_Grupo_Sup"] != DBNull.Value)
                {
                    grupo.IdsGrupoSup = (string)qReader["strIds_Grupo_Sup"];
                }

            }
            qReader.Close();
            return grupo;
        }

        public ItemFinanceiro[] ObtemFinanceiros(string criterio)
        {
            LightBaseDataReader qReader = null;

            try
            {
                var command = new LightBaseCommand("select * from FCARNAUBA_FINANCEIRO where id = " + criterio + " or strDescricao = " + criterio);
                command.Connection = _Connection;
                qReader = command.ExecuteReader();

                List<ItemFinanceiro> financeiros = new List<ItemFinanceiro>();

                while (qReader.Read())
                {
                    ItemFinanceiro financeiro = new ItemFinanceiro();

                    financeiro.IdFinanceiro = Convert.ToInt64(qReader["id"]);
                    financeiro.DataCadastro = Convert.ToDateTime(qReader["dtDataCadastro"]);
                    financeiro.Descricao = Convert.ToString(qReader["strDescricao"]);
                    financeiro.IdGrupo = Convert.ToInt32(qReader["FK_intID_Grupo"]);
                    financeiro.PropriedadeComp = Convert.ToString(qReader["strPropriedadeComp"]);
                    financeiro.IdEmpresa = Convert.ToInt32(qReader["FK_IdEmpresa"]);
                    financeiro.Quantidade = Convert.ToInt32(qReader["intQuantidade"]);
                    financeiro.ValorUnitario = Convert.ToDouble(qReader["moeValorUnitario"]);
                    financeiro.ValorTotal = Convert.ToDouble(qReader["moeValorTotal"]);
                    financeiro.Data = Convert.ToString(qReader["dtData"]);
                    financeiro.Documento = Convert.ToString(qReader["strDocumento"]);
                    financeiro.FormaPagamento = Convert.ToString(qReader["strFormaPagamento"]);

                    if (qReader["vfVendaAnimais"] != DBNull.Value)
                    {
                        financeiro.VendaAnimais = (bool)qReader["vfVendaAnimais"];
                        if (financeiro.VendaAnimais)
                        {
                            financeiro.VendaAnimaisStr = "Sim";
                        }
                        else
                        {
                            financeiro.VendaAnimaisStr = "Não";
                        }
                    }
                    else
                    {
                        financeiro.VendaAnimais = false;
                        financeiro.VendaAnimaisStr = "Não";
                    }

                    financeiros.Add(financeiro);
                }

                return financeiros.ToArray();
            }

            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }


            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public ItemFinanceiro[] ConsultaFinanceiro(CriterioPesquisaFinanceiro criterio)
        {
            try
            {
                var command = new LightBaseCommand("textsearch in FCARNAUBA_FINANCEIRO " + criterio.Filter + " order by dtData desc");
                command.Connection = _Connection;
                LightBaseDataReader qReader = command.ExecuteReader();

                List<ItemFinanceiro> financeiros = new List<ItemFinanceiro>();

                while (qReader.Read())
                {
                    ItemFinanceiro financeiro = new ItemFinanceiro();
                    financeiro.IdFinanceiro = Convert.ToInt32(qReader["id"]);
                    financeiro.DataCadastro = Convert.ToDateTime(qReader["dtDataCadastro"]);
                    financeiro.Descricao = Convert.ToString(qReader["strDescricao"]);
                    financeiro.PropriedadeComp = Convert.ToString(qReader["strPropriedadeComp"]);
                    financeiro.IdEmpresa = Convert.ToInt32(qReader["FK_IdEmpresa"]);
                    financeiro.IdGrupo = Convert.ToInt32(qReader["FK_intID_Grupo"]);
                    financeiro.DescricaoGrupo = "Descrição aqui";
                    financeiro.Quantidade = Convert.ToInt32(qReader["intQuantidade"]);
                    financeiro.ValorUnitario = Convert.ToDouble(qReader["moeValorUnitario"]);
                    financeiro.ValorTotal = Convert.ToDouble(qReader["moeValorTotal"]);
                    financeiro.ValorTotal = Math.Round(Convert.ToDouble(qReader["moeValorTotal"]), 2);
                    financeiro.Data = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(qReader["dtData"]));
                    financeiro.Documento = Convert.ToString(qReader["strDocumento"]);
                    financeiro.FormaPagamento = Convert.ToString(qReader["strFormaPagamento"]);

                    if (qReader["vfVendaAnimais"] != DBNull.Value)
                    {
                        financeiro.VendaAnimais = (bool)qReader["vfVendaAnimais"];
                        if (financeiro.VendaAnimais)
                        {
                            financeiro.VendaAnimaisStr = "Sim";
                        }
                        else
                        {
                            financeiro.VendaAnimaisStr = "Não";
                        }
                    }
                    else
                    {
                        financeiro.VendaAnimais = false;
                        financeiro.VendaAnimaisStr = "Não";
                    }


                    financeiros.Add(financeiro);
                }
                qReader.Close();
                return financeiros.ToArray();
            }
            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }
            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public ItemFinanceiro[] ConsultaFinanceiro(string idGrupo)
        {
            try
            {
                var command = new LightBaseCommand("select * from FCARNAUBA_FINANCEIRO where FK_intID_Grupo = " + idGrupo);
                command.Connection = _Connection;
                LightBaseDataReader qReader = command.ExecuteReader();

                List<ItemFinanceiro> financeiros = new List<ItemFinanceiro>();

                while (qReader.Read())
                {
                    ItemFinanceiro financeiro = new ItemFinanceiro();
                    financeiro.IdFinanceiro = Convert.ToInt32(qReader["id"]);
                    financeiro.DataCadastro = Convert.ToDateTime(qReader["dtDataCadastro"]);
                    financeiro.Descricao = Convert.ToString(qReader["strDescricao"]);
                    financeiro.PropriedadeComp = Convert.ToString(qReader["strPropriedadeComp"]);
                    financeiro.IdEmpresa = Convert.ToInt32(qReader["FK_IdEmpresa"]);
                    financeiro.IdGrupo = Convert.ToInt32(qReader["FK_intID_Grupo"]);
                    financeiro.DescricaoGrupo = "Descrição aqui";
                    financeiro.Quantidade = Convert.ToInt32(qReader["intQuantidade"]);
                    financeiro.ValorUnitario = Convert.ToDouble(qReader["moeValorUnitario"]);
                    financeiro.ValorTotal = Convert.ToDouble(qReader["moeValorTotal"]);
                    financeiro.ValorTotal = Math.Round(Convert.ToDouble(qReader["moeValorTotal"]), 2);
                    financeiro.Data = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(qReader["dtData"]));
                    financeiro.Documento = Convert.ToString(qReader["strDocumento"]);
                    financeiro.FormaPagamento = Convert.ToString(qReader["strFormaPagamento"]);

                    if (qReader["vfVendaAnimais"] != DBNull.Value)
                    {
                        financeiro.VendaAnimais = (bool)qReader["vfVendaAnimais"];
                        if (financeiro.VendaAnimais)
                        {
                            financeiro.VendaAnimaisStr = "Sim";
                        }
                        else
                        {
                            financeiro.VendaAnimaisStr = "Não";
                        }
                    }
                    else
                    {
                        financeiro.VendaAnimais = false;
                        financeiro.VendaAnimaisStr = "Não";
                    }


                    financeiros.Add(financeiro);
                }
                qReader.Close();
                return financeiros.ToArray();
            }
            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }
            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public long InserirFinanceiro(ItemFinanceiro financeiro)
        {
            try
            {
                if (String.IsNullOrEmpty(financeiro.Descricao))
                {
                    Exception exc = new Exception("Erro genérico");
                    throw exc;
                }

                var command = new LightBaseCommand(@"insert into FCARNAUBA_FINANCEIRO
                diretorio,
                strDescricao,
                FK_intID_Grupo,
                FK_IdPropriedade,
                intQuantidade,
                moeValorUnitario,
                moeValorTotal,
                dtDataCadastro,
                strUsuario,
                dtDataUsuario,
                dtData,
                strDocumento,
                FK_IdEmpresa,
                strPropriedadeComp,
                strFormaPagamento,
                vfVendaAnimais
                 
                values
                
                (@diretorio,
                @strDescricao,
                @FK_intID_Grupo,
                @FK_IdPropriedade,
                @intQuantidade,
                @moeValorUnitario,
                @moeValorTotal,
                @dtDataCadastro,
                @strUsuario,
                @dtDataUsuario,
                @dtData,
                @strDocumento,
                @FK_IdEmpresa,
                @strPropriedadeComp,
                @strFormaPagamento,
                @vfVendaAnimais)", _Connection);
                ((LightBaseParameter)command.Parameters["diretorio"]).Value = financeiro.Diretorio;
                ((LightBaseParameter)command.Parameters["strDescricao"]).Value = financeiro.Descricao;
                ((LightBaseParameter)command.Parameters["FK_intID_Grupo"]).Value = financeiro.IdGrupo;
                ((LightBaseParameter)command.Parameters["FK_IdPropriedade"]).Value = financeiro.IdPropriedade;
                ((LightBaseParameter)command.Parameters["FK_IdEmpresa"]).Value = financeiro.IdEmpresa;
                ((LightBaseParameter)command.Parameters["intQuantidade"]).Value = financeiro.Quantidade;
                ((LightBaseParameter)command.Parameters["moeValorUnitario"]).Value = financeiro.ValorUnitario;
                ((LightBaseParameter)command.Parameters["moeValorTotal"]).Value = financeiro.Quantidade * financeiro.ValorUnitario;
                ((LightBaseParameter)command.Parameters["dtDataCadastro"]).Value = DateTime.Today;
                ((LightBaseParameter)command.Parameters["strUsuario"]).Value = financeiro.Usuario;
                ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = financeiro.DataUsuario;
                ((LightBaseParameter)command.Parameters["dtData"]).Value = financeiro.Data;
                ((LightBaseParameter)command.Parameters["strDocumento"]).Value = financeiro.Documento;
                ((LightBaseParameter)command.Parameters["strPropriedadeComp"]).Value = financeiro.PropriedadeComp;
                ((LightBaseParameter)command.Parameters["strFormaPagamento"]).Value = financeiro.FormaPagamento;
                ((LightBaseParameter)command.Parameters["vfVendaAnimais"]).Value = financeiro.VendaAnimais;

                command.Connection = _Connection;
                command.ExecuteNonQuery();

                const string idRetrievingCommand = "@@Id";
                LightBaseCommand lastIdCommand = new LightBaseCommand(idRetrievingCommand, _Connection);
                long ultimoId = Convert.ToInt32(lastIdCommand.ExecuteScalar());

                return ultimoId;

            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }
        }

        public ItemFinanceiro GetFinanceiroById(string id)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_FINANCEIRO where id = @id");
            command.Connection = _Connection;
            ((LightBaseParameter)command.Parameters["id"]).Value = id;
            var qReader = command.ExecuteReader();
            var retVal = new ItemFinanceiro();

            if (qReader.Read())
            {
                CultureInfo cult = new CultureInfo("pt-BR");

                if (qReader["id"] != DBNull.Value) retVal.IdFinanceiro = Convert.ToInt64(qReader["id"]);
                if (qReader["diretorio"] != DBNull.Value) retVal.Diretorio = qReader["diretorio"].ToString();
                if (qReader["strDocumento"] != DBNull.Value) retVal.Documento = qReader["strDocumento"].ToString();
                if (qReader["dtData"] != DBNull.Value) retVal.Data = qReader["dtData"].ToString();
                if (qReader["strDescricao"] != DBNull.Value) retVal.Descricao = qReader["strDescricao"].ToString();
                if (qReader["moeValorUnitario"] != DBNull.Value) retVal.ValorUnitario = Convert.ToDouble(qReader["moeValorUnitario"]);
                if (qReader["moeValorTotal"] != DBNull.Value) retVal.ValorTotal = Convert.ToDouble(qReader["moeValorTotal"]);
                if (qReader["intQuantidade"] != DBNull.Value) retVal.Quantidade = Convert.ToInt32(qReader["intQuantidade"]);
                if (qReader["strFormaPagamento"] != DBNull.Value) retVal.FormaPagamento = qReader["strFormaPagamento"].ToString();

                if (qReader["vfVendaAnimais"] != DBNull.Value)
                {
                    retVal.VendaAnimais = (bool)qReader["vfVendaAnimais"];
                    if (retVal.VendaAnimais)
                    {
                        retVal.VendaAnimaisStr = "Sim";
                        retVal.TotalPago = GetTotalPago(id);
                    }
                    else
                    {
                        retVal.VendaAnimaisStr = "Não";
                    }
                }
                else
                {
                    retVal.VendaAnimais = false;
                    retVal.VendaAnimaisStr = "Não";
                }


                if (qReader["strPropriedadeComp"] != DBNull.Value)
                {
                    string idPropriedadeStr = qReader["strPropriedadeComp"].ToString();
                    retVal.DescricaoPropriedade = GetNomePropriedadeComp(idPropriedadeStr);
                }

                if (qReader["FK_IdEmpresa"] != DBNull.Value)
                {
                    retVal.IdEmpresa = Convert.ToInt32(qReader["FK_IdEmpresa"]);
                    retVal.DescricaoEmpresa = GetNomeEmpresa(retVal.IdEmpresa.ToString());
                }

                if (qReader["FK_intID_Grupo"] != DBNull.Value)
                {
                    retVal.IdGrupo = Convert.ToInt32(qReader["FK_intID_Grupo"]);
                    retVal.DescricaoGrupo = GetNomeGrupo(retVal.IdGrupo.ToString());
                }

            }


            qReader.Close();
            return retVal;
        }

        public void AlterarFinanceiro(ItemFinanceiro financeiro)
        {

            try
            {
                List<string> campos = new List<string>()
                                      {
                                            "diretorio",
                                            "strDescricao",
                                            "FK_intID_Grupo",
                                            "FK_IdPropriedade",
                                            "intQuantidade",
                                            "moeValorUnitario",
                                            "moeValorTotal",
                                            "dtData",
                                            "strDocumento",
                                            "FK_IdEmpresa",
                                            "strPropriedadeComp",
                                            "strFormaPagamento",
                                            "vfVendaAnimais"
                                      };

                var command = new LightBaseCommand(BuildFinanceiroString(campos));

                ((LightBaseParameter)command.Parameters["id"]).Value = financeiro.IdFinanceiro;
                ((LightBaseParameter)command.Parameters["diretorio"]).Value = financeiro.Diretorio;
                ((LightBaseParameter)command.Parameters["strDescricao"]).Value = financeiro.Descricao;
                ((LightBaseParameter)command.Parameters["FK_intID_Grupo"]).Value = financeiro.IdGrupo;
                ((LightBaseParameter)command.Parameters["FK_IdPropriedade"]).Value = financeiro.IdPropriedade;
                ((LightBaseParameter)command.Parameters["FK_IdEmpresa"]).Value = financeiro.IdEmpresa;
                ((LightBaseParameter)command.Parameters["intQuantidade"]).Value = financeiro.Quantidade;
                ((LightBaseParameter)command.Parameters["moeValorUnitario"]).Value = financeiro.ValorUnitario;
                ((LightBaseParameter)command.Parameters["moeValorTotal"]).Value = financeiro.Quantidade * financeiro.ValorUnitario;
                ((LightBaseParameter)command.Parameters["dtData"]).Value = financeiro.Data;
                ((LightBaseParameter)command.Parameters["strDocumento"]).Value = financeiro.Documento;
                ((LightBaseParameter)command.Parameters["strPropriedadeComp"]).Value = financeiro.PropriedadeComp;
                ((LightBaseParameter)command.Parameters["strFormaPagamento"]).Value = financeiro.FormaPagamento;
                ((LightBaseParameter)command.Parameters["vfVendaAnimais"]).Value = financeiro.VendaAnimais;


                command.Connection = _Connection;
                command.ExecuteNonQuery();


            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro na edição", exc);
                throw except;
            }
        }

        public void RemoverFinanceiro(long idFinanceiro)
        {

            try
            {
                var command = new LightBaseCommand(@"delete from FCARNAUBA_FINANCEIRO where id = @id");
                ((LightBaseParameter)command.Parameters["id"]).Value = idFinanceiro;
                command.Connection = _Connection;
                command.ExecuteNonQuery();
            }

            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }
        }

        public void ValidarFinanceiro(long idFinanceiro, string usuarioValidacao, DateTime usuarioDataValidacao)
        {
            var command = new LightBaseCommand(@"update FCARNAUBA_FINANCEIRO set strUsuarioValidacao=@strUsuarioValidacao, dtDataUsuarioValidacao=@dtDataUsuarioValidacao where id = @id");
            ((LightBaseParameter)command.Parameters["strUsuarioValidacao"]).Value = usuarioValidacao;
            ((LightBaseParameter)command.Parameters["dtDataUsuarioValidacao"]).Value = usuarioDataValidacao;
            ((LightBaseParameter)command.Parameters["id"]).Value = idFinanceiro;

            command.Connection = _Connection;
            command.ExecuteNonQuery();
        }

        public double GetTotalItensFinanceiro(CriterioPesquisaFinanceiro criterio, GrupoFinanceiro grupo)
        {
            string filter = "";
            double total = 0;


            if (!grupo.EhUltimoNoh)
            {

                if (grupo.IdsGrupoSup != "")
                {
                    filter = AddParametro(filter, "strIds_Grupo_Sup", grupo.IdsGrupoSup + " " + grupo.IdGrupo + "*");
                }
                else
                {
                    filter = AddParametro(filter, "strIds_Grupo_Sup", grupo.IdGrupo.ToString());
                }


                var command = new LightBaseCommand("textsearch in FCARNAUBA_GRUPO_FINANCEIRO " + filter);

                command.Connection = _Connection;

                var qReader = command.ExecuteReader();

                var grupos = new List<GrupoFinanceiro>();

                while (qReader.Read())
                {

                    var retVal = new GrupoFinanceiro();

                    if (qReader["kintID_Grupo"] != DBNull.Value) retVal.IdGrupo = Convert.ToInt32(qReader["kintID_Grupo"]);

                    total = total + GetTotalItemFinanceiro(retVal.IdGrupo.ToString(), criterio);

                }

                if (total == 0)
                {
                    total = GetTotalItensFinanceiroMeio(criterio, grupo);
                }

                qReader.Close();
            }

            else
            {
                total = GetTotalItemFinanceiro(grupo.IdGrupo.ToString(), criterio);

            }

            return total;
        }

        public double GetTotalItensFinanceiroMeio(CriterioPesquisaFinanceiro criterio, GrupoFinanceiro grupo)
        {
            string filter = "";
            double total = 0;

            filter = AddParametro(filter, "strIds_Grupo_Sup", grupo.IdGrupo.ToString());

            var command = new LightBaseCommand("textsearch in FCARNAUBA_GRUPO_FINANCEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var grupos = new List<GrupoFinanceiro>();

            while (qReader.Read())
            {

                var retVal = new GrupoFinanceiro();

                if (qReader["kintID_Grupo"] != DBNull.Value) retVal.IdGrupo = Convert.ToInt32(qReader["kintID_Grupo"]);
                {

                    total = total + GetTotalItemFinanceiro(retVal.IdGrupo.ToString(), criterio);

                }
            }

            qReader.Close();

            return total;
        }

        public double GetTotalItemFinanceiro(string idGrupo, CriterioPesquisaFinanceiro criterio)
        {

            string filter = "";
            double total = 0;

            filter = AddParametro(filter, "FK_intID_Grupo", idGrupo.ToString());

            if (criterio.Data != null)
                filter = AddParametroData(filter, "dtData", criterio.Data.Value.ToShortDateString(), ">=");
            if (criterio.DataInicio != null)
                filter = AddParametroData(filter, "dtData", criterio.DataInicio.Value.ToShortDateString(), ">=");
            if (criterio.DataFim != null)
                filter = AddParametroData(filter, "dtData", criterio.DataFim.Value.ToShortDateString(), "<=");

            filter = AddParametroTextual(filter, "strPropriedadeComp", criterio.PropriedadeComp);

            var command = new LightBaseCommand("textsearch in FCARNAUBA_FINANCEIRO " + filter);

            command.Connection = _Connection;

            var qReader = command.ExecuteReader();

            var financeiros = new List<ItemFinanceiro>();

            while (qReader.Read())
            {

                var retVal = new ItemFinanceiro();

                if (qReader["moeValorTotal"] != DBNull.Value)
                {
                    retVal.ValorTotal = Convert.ToDouble(qReader["moeValorTotal"]);
                    total = total + retVal.ValorTotal;
                }

            }

            return total;
        }


        public Empresa[] ObtemEmpresas(string criterio)
        {
            LightBaseDataReader qReader = null;

            try
            {
                var command = new LightBaseCommand("select * from FCARNAUBA_EMPRESA where id = " + criterio + " or strRazaoSocial = " + criterio + " or strCnpjCpf = " + criterio + " order by strRazaoSocial");
                command.Connection = _Connection;
                qReader = command.ExecuteReader();

                List<Empresa> empresas = new List<Empresa>();

                while (qReader.Read())
                {
                    Empresa empresa = new Empresa();

                    empresa.IdEmpresa = Convert.ToInt64(qReader["id"]);
                    empresa.RazaoSocial = Convert.ToString(qReader["strRazaoSocial"]);
                    empresa.CnpjCpf = Convert.ToString(qReader["strCnpjCpf"]);
                    empresa.Endereco = Convert.ToString(qReader["strEndereco"]);
                    empresa.Municipio = Convert.ToString(qReader["strMunicipio"]);
                    empresa.Uf = Convert.ToString(qReader["strUf"]);
                    empresa.Telefones = Convert.ToString(qReader["strTelefones"]);
                    empresa.Tipo = Convert.ToString(qReader["strTipo"]);
                    empresa.Email = Convert.ToString(qReader["strEmail"]);

                    empresas.Add(empresa);
                }

                return empresas.ToArray();
            }

            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }


            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public Empresa[] ObtemEmpresas()
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_EMPRESA order by strRazaoSocial");

            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var empresas = new List<Empresa>();
            Empresa empresaVazio = new Empresa();
            empresaVazio.IdEmpresa = 0;
            empresaVazio.RazaoSocial = "";
            empresas.Add(empresaVazio);

            while (qReader.Read())
            {
                Empresa empresa = new Empresa();
                empresa.IdEmpresa = Convert.ToInt32(qReader["id"].ToString());
                empresa.RazaoSocial = qReader["strRazaoSocial"].ToString();
                empresa.Endereco = qReader["strEndereco"].ToString();
                empresa.Municipio = Convert.ToString(qReader["strMunicipio"]);
                empresa.Uf = Convert.ToString(qReader["strUf"]);
                empresa.Telefones = Convert.ToString(qReader["strTelefones"]);
                empresa.Tipo = Convert.ToString(qReader["strTipo"]);
                empresa.Email = Convert.ToString(qReader["strEmail"]);

                empresas.Add(empresa);
            }
            qReader.Close();
            return empresas.ToArray();
        }

        public Empresa[] ObtemClientes()
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_EMPRESA where strTipo=@strTipo order by strRazaoSocial");
            ((LightBaseParameter)command.Parameters["strTipo"]).Value = "Cliente";
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            var empresas = new List<Empresa>();
            Empresa empresaVazio = new Empresa();
            empresaVazio.IdEmpresa = 0;
            empresaVazio.RazaoSocial = "";
            empresas.Add(empresaVazio);

            while (qReader.Read())
            {
                Empresa empresa = new Empresa();
                empresa.IdEmpresa = Convert.ToInt32(qReader["id"].ToString());
                empresa.RazaoSocial = qReader["strRazaoSocial"].ToString();
                empresa.Endereco = qReader["strEndereco"].ToString();
                empresa.Municipio = Convert.ToString(qReader["strMunicipio"]);
                empresa.Uf = Convert.ToString(qReader["strUf"]);
                empresa.Telefones = Convert.ToString(qReader["strTelefones"]);
                empresa.Tipo = Convert.ToString(qReader["strTipo"]);
                empresa.Email = Convert.ToString(qReader["strEmail"]);

                empresas.Add(empresa);
            }
            qReader.Close();
            return empresas.ToArray();
        }

        public Empresa[] ConsultaEmpresa(CriterioPesquisaEmpresa criterio)
        {
            try
            {
                var command = new LightBaseCommand("textsearch in FCARNAUBA_EMPRESA " + criterio.Filter + " order by strRazaoSocial");
                command.Connection = _Connection;
                LightBaseDataReader qReader = command.ExecuteReader();

                List<Empresa> empresas = new List<Empresa>();

                while (qReader.Read())
                {
                    Empresa empresa = new Empresa();
                    empresa.IdEmpresa = Convert.ToInt32(qReader["id"]);
                    empresa.RazaoSocial = Convert.ToString(qReader["strRazaoSocial"]);
                    empresa.CnpjCpf = Convert.ToString(qReader["strCnpjCpf"]);
                    empresa.Endereco = Convert.ToString(qReader["strEndereco"]);
                    empresa.Municipio = Convert.ToString(qReader["strMunicipio"]);
                    empresa.Uf = Convert.ToString(qReader["strUf"]);
                    empresa.Telefones = Convert.ToString(qReader["strTelefones"]);
                    empresa.Tipo = Convert.ToString(qReader["strTipo"]);
                    empresa.Email = Convert.ToString(qReader["strEmail"]);

                    empresas.Add(empresa);
                }
                qReader.Close();
                return empresas.ToArray();
            }
            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }
            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public void InserirEmpresa(Empresa empresa)
        {
            try
            {
                if (CnpjCpfExists(empresa.CnpjCpf))
                {
                    // Para o caso de o nome do grupo não poder ser duplicado
                    throw new Exception("Não é possível inserir um CNPJ/CPF existente.");
                }
                else
                {

                    var command = new LightBaseCommand(@"insert into FCARNAUBA_EMPRESA
                    diretorio,
                    strRazaoSocial,
                    strCnpjCpf,
                    strUsuario,
                    dtDataUsuario,
                    strEndereco,
                    strMunicipio,
                    strUf,
                    strTelefones,
                    strTipo,
                    strEmail
                 
                    values
                
                    (@diretorio,
                    @strRazaoSocial,
                    @strCnpjCpf,
                    @strUsuario,
                    @dtDataUsuario,
                    @strEndereco,
                    @strMunicipio,
                    @strUf,
                    @strTelefones,
                    @strTipo,
                    @strEmail)", _Connection);
                    ((LightBaseParameter)command.Parameters["diretorio"]).Value = empresa.Diretorio;
                    ((LightBaseParameter)command.Parameters["strRazaoSocial"]).Value = empresa.RazaoSocial;
                    ((LightBaseParameter)command.Parameters["strCnpjCpf"]).Value = empresa.CnpjCpf;
                    ((LightBaseParameter)command.Parameters["strUsuario"]).Value = empresa.Usuario;
                    ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = empresa.DataUsuario;
                    ((LightBaseParameter)command.Parameters["strEndereco"]).Value = empresa.Endereco;
                    ((LightBaseParameter)command.Parameters["strMunicipio"]).Value = empresa.Municipio;
                    ((LightBaseParameter)command.Parameters["strUf"]).Value = empresa.Uf;
                    ((LightBaseParameter)command.Parameters["strTelefones"]).Value = empresa.Telefones;
                    ((LightBaseParameter)command.Parameters["strTipo"]).Value = empresa.Tipo;
                    ((LightBaseParameter)command.Parameters["strEmail"]).Value = empresa.Email;

                    command.Connection = _Connection;
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }
        }

        public void AlterarEmpresa(Empresa empresa)
        {

            try
            {
                Empresa empresaCorr = ObtemEmpresa(Convert.ToInt32(empresa.IdEmpresa));
                List<string> campos = new List<string>()
                                      {
                                            "diretorio",
                                            "strRazaoSocial",
                                            "strCnpjCpf",
                                            "strEndereco",
                                            "strMunicipio",
                                            "strUf",
                                            "strTelefones",
                                            "strTipo",
                                            "strEmail"
                                      };

                if ((CnpjCpfExists(empresa.CnpjCpf) && (empresa.RazaoSocial != empresaCorr.RazaoSocial || empresa.Endereco != empresaCorr.Endereco || empresa.Municipio != empresaCorr.Municipio || empresa.Uf != empresaCorr.Uf || empresa.Telefones != empresaCorr.Telefones || empresa.Email != empresaCorr.Email)) || !CnpjCpfExists(empresa.CnpjCpf))
                {
                    var command = new LightBaseCommand(BuildEmpresaString(campos));

                    ((LightBaseParameter)command.Parameters["id"]).Value = empresa.IdEmpresa;
                    ((LightBaseParameter)command.Parameters["diretorio"]).Value = empresa.Diretorio;
                    ((LightBaseParameter)command.Parameters["strRazaoSocial"]).Value = empresa.RazaoSocial;
                    ((LightBaseParameter)command.Parameters["strCnpjCpf"]).Value = empresa.CnpjCpf;
                    ((LightBaseParameter)command.Parameters["strEndereco"]).Value = empresa.Endereco;
                    ((LightBaseParameter)command.Parameters["strMunicipio"]).Value = empresa.Municipio;
                    ((LightBaseParameter)command.Parameters["strUf"]).Value = empresa.Uf;
                    ((LightBaseParameter)command.Parameters["strTelefones"]).Value = empresa.Telefones;
                    ((LightBaseParameter)command.Parameters["strTipo"]).Value = empresa.Tipo;
                    ((LightBaseParameter)command.Parameters["strEmail"]).Value = empresa.Email;

                    command.Connection = _Connection;
                    command.ExecuteNonQuery();
                }
                else
                {
                    // Para o caso de o CNPJ/CPF não poder ser duplicado
                    throw new Exception("Não é possível inserir um CNPJ/CPF existente.");

                }


            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro na edição", exc);
                throw except;
            }
        }

        public void RemoverEmpresa(long idEmpresa)
        {
            try
            {
                var command = new LightBaseCommand(@"delete from FCARNAUBA_EMPRESA where id = @id");
                ((LightBaseParameter)command.Parameters["id"]).Value = idEmpresa;
                command.Connection = _Connection;
                command.ExecuteNonQuery();

            }

            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }
        }


        public ControlePluviometrico[] ObtemPluviometrias(string criterio)
        {
            LightBaseDataReader qReader = null;

            try
            {
                var command = new LightBaseCommand("select * from FCARNAUBA_CONT_PLUVIOMETRICO where id = " + criterio + " or diretorio = " + criterio);
                command.Connection = _Connection;
                qReader = command.ExecuteReader();

                List<ControlePluviometrico> pluviometrias = new List<ControlePluviometrico>();

                while (qReader.Read())
                {
                    ControlePluviometrico pluviometria = new ControlePluviometrico();

                    CultureInfo cult = new CultureInfo("pt-BR");

                    if (qReader["id"] != DBNull.Value) pluviometria.Id = Convert.ToInt64(qReader["id"]);
                    if (qReader["diretorio"] != DBNull.Value) pluviometria.Diretorio = qReader["diretorio"].ToString();
                    if (qReader["dtData"] != DBNull.Value) pluviometria.DataStr = qReader["dtData"].ToString();
                    if (qReader["dtData"] != DBNull.Value) pluviometria.Data = (DateTime)qReader["dtData"];
                    if (qReader["decPluviometria"] != DBNull.Value) pluviometria.Pluviometria = Convert.ToDouble(qReader["decPluviometria"]);
                    if (qReader["strUsuario"] != DBNull.Value) pluviometria.Usuario = qReader["strUsuario"].ToString();
                    if (qReader["dtDataUsuario"] != DBNull.Value) pluviometria.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                    if (qReader["strPluviometro"] != DBNull.Value) pluviometria.Pluviometro = qReader["strPluviometro"].ToString();

                    pluviometrias.Add(pluviometria);
                }

                return pluviometrias.ToArray();
            }

            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }


            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public ControlePluviometrico[] ConsultaPluviometria(ParametrosDeBuscaEmControlePluviometrico criterio)
        {
            try
            {
                var command = new LightBaseCommand("textsearch in FCARNAUBA_CONT_PLUVIOMETRICO " + criterio.Filter + " order by dtData, strPluviometro");
                command.Connection = _Connection;
                LightBaseDataReader qReader = command.ExecuteReader();

                List<ControlePluviometrico> pluviometrias = new List<ControlePluviometrico>();

                while (qReader.Read())
                {
                    ControlePluviometrico pluviometria = new ControlePluviometrico();

                    if (qReader["id"] != DBNull.Value) pluviometria.Id = Convert.ToInt64(qReader["id"]);
                    if (qReader["diretorio"] != DBNull.Value) pluviometria.Diretorio = qReader["diretorio"].ToString();
                    if (qReader["dtData"] != DBNull.Value) pluviometria.DataStr = qReader["dtData"].ToString();
                    if (qReader["dtData"] != DBNull.Value) pluviometria.Data = (DateTime)qReader["dtData"];
                    if (qReader["decPluviometria"] != DBNull.Value) pluviometria.Pluviometria = Convert.ToDouble(qReader["decPluviometria"]);
                    if (qReader["strUsuario"] != DBNull.Value) pluviometria.Usuario = qReader["strUsuario"].ToString();
                    if (qReader["dtDataUsuario"] != DBNull.Value) pluviometria.DataUsuario = (DateTime)qReader["dtDataUsuario"];
                    if (qReader["strPluviometro"] != DBNull.Value) pluviometria.Pluviometro = qReader["strPluviometro"].ToString();


                    pluviometrias.Add(pluviometria);
                }
                qReader.Close();
                return pluviometrias.ToArray();
            }
            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }
            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public void InserirPluviometria(ControlePluviometrico pluviometria)
        {
            try
            {
                if (PluviometriaExists(pluviometria.Diretorio, pluviometria.Data, pluviometria.Pluviometro))
                {
                    // Para o caso da pluviometria não poder ser duplicado
                    throw new Exception("Não é possível inserir pluviometria existente.");
                }
                else
                {

                    var command = new LightBaseCommand(@"insert into FCARNAUBA_CONT_PLUVIOMETRICO
                diretorio,
                dtData,
                decPluviometria,
                strUsuario,
                dtDataUsuario,
                strPluviometro
                 
                values
                
                (@diretorio,
                @dtData,
                @decPluviometria,
                @strUsuario,
                @dtDataUsuario,
                @strPluviometro)", _Connection);
                    ((LightBaseParameter)command.Parameters["diretorio"]).Value = pluviometria.Diretorio;
                    ((LightBaseParameter)command.Parameters["dtData"]).Value = pluviometria.DataStr;
                    ((LightBaseParameter)command.Parameters["decPluviometria"]).Value = pluviometria.Pluviometria;
                    ((LightBaseParameter)command.Parameters["strUsuario"]).Value = pluviometria.Usuario;
                    ((LightBaseParameter)command.Parameters["dtDataUsuario"]).Value = pluviometria.DataUsuario;
                    ((LightBaseParameter)command.Parameters["strPluviometro"]).Value = pluviometria.Pluviometro;

                    command.Connection = _Connection;
                    command.ExecuteNonQuery();

                }


            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }
        }

        public void AlterarPluviometria(ControlePluviometrico pluviometria)
        {

            try
            {
                ControlePluviometrico pluviometriaCorr = GetControlePluviometricoById(pluviometria.Id.ToString());

                if (PluviometriaExists(pluviometriaCorr.Diretorio, Convert.ToDateTime(pluviometria.DataStr), pluviometria.Pluviometro) && (Convert.ToDateTime(pluviometria.DataStr) != pluviometriaCorr.Data || pluviometria.Pluviometro != pluviometriaCorr.Pluviometro))
                {
                    // Para o caso da pluviometria não poder ser duplicado
                    throw new Exception("Não é possível alterar para pluviometria já existente.");
                }
                else
                {

                    List<string> campos = new List<string>()
                                      {
                                            "dtData",
                                            "decPluviometria",
                                            "strPluviometro"
                                      };

                    var command = new LightBaseCommand(BuildControlePluviometricoString(campos));
                    ((LightBaseParameter)command.Parameters["id"]).Value = pluviometria.Id;
                    ((LightBaseParameter)command.Parameters["dtData"]).Value = pluviometria.DataStr;
                    ((LightBaseParameter)command.Parameters["decPluviometria"]).Value = pluviometria.Pluviometria;
                    ((LightBaseParameter)command.Parameters["strPluviometro"]).Value = pluviometria.Pluviometro;


                    command.Connection = _Connection;
                    command.ExecuteNonQuery();

                }


            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro na edição", exc);
                throw except;
            }
        }

        public void RemoverPluviometria(long idPluviometria)
        {

            try
            {
                var command = new LightBaseCommand(@"delete from FCARNAUBA_CONT_PLUVIOMETRICO where id = @id");
                ((LightBaseParameter)command.Parameters["id"]).Value = idPluviometria;
                command.Connection = _Connection;
                command.ExecuteNonQuery();

            }

            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }
        }

        public Compra[] ObtemCompras(CriterioPesquisaCompras criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_FINANCEIRO where id = @id");
                ((LightBaseParameter)command.Parameters["id"]).Value = criterio.IdFinanceiro;
                command.Connection = _Connection;
                var qReader = command.ExecuteReader();

                var retVal = new List<Compra>();

                if (qReader.Read())
                {
                    var res = (DataTable)qReader["Compras"];
                    int c = 0;
                    int id = Convert.ToInt32(qReader["id"]);

                    foreach (DataRow dRow in res.Rows)
                    {
                        var compra = DataRowToCompra(dRow);
                        compra.Id = c;
                        compra.FinanceiroId = id;
                        compra.CompraFinanceiroId = compra.FinanceiroId + " " + compra.Id;
                        retVal.Add(compra);
                        c++;
                    }
                }

                qReader.Close();
                return retVal.ToArray();
            }

            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }


            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public void InserirCompra(string itemFinanceiroId, Compra compra)
        {
            try
            {
                if (AnimalCompraExists(itemFinanceiroId, compra.IdAnimal) && compra.IdAnimal != "0")
                {
                    // Para o caso do animal não poder ser duplicado
                    throw new Exception("Não é possível inserir um Animal existente.");
                }
                else
                {
                    var command = new LightBaseCommand(@"insert into FCARNAUBA_FINANCEIRO Compras values ({{@COM_strEvento, @FK_COM_strIdAnimal, @COM_docDescricao, @COM_moeValor}}) parent id = @id");

                    ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroId;
                    ((LightBaseParameter)command.Parameters["COM_strEvento"]).Value = compra.Evento;
                    ((LightBaseParameter)command.Parameters["FK_COM_strIdAnimal"]).Value = compra.IdAnimal;
                    ((LightBaseParameter)command.Parameters["COM_docDescricao"]).Value = compra.Descricao;
                    ((LightBaseParameter)command.Parameters["COM_moeValor"]).Value = compra.Valor;

                    command.Connection = _Connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }

        }

        public void AlterarCompra(int itemFinanceiroId, int compraId, Compra compra)
        {
            try
            {
                Compra compraCorr = GetCompraById(itemFinanceiroId, compraId);

                List<string> campos = new List<string>()
                                      { "COM_strEvento",
                                        "FK_COM_strIdAnimal",
                                        "COM_docDescricao",
                                        "COM_moeValor",
                                      };


                if ((AnimalCompraExists(itemFinanceiroId.ToString(), compra.IdAnimal) && (compra.Evento != compraCorr.Evento || compra.Descricao != compraCorr.Descricao || compra.Valor != compraCorr.Valor)) || !AnimalCompraExists(itemFinanceiroId.ToString(), compra.IdAnimal) || compra.IdAnimal != "0")
                {

                    var command = new LightBaseCommand(BuildCompraString(campos, compraId));
                    ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroId;
                    ((LightBaseParameter)command.Parameters["COM_strEvento"]).Value = compra.Evento;
                    ((LightBaseParameter)command.Parameters["FK_COM_strIdAnimal"]).Value = compra.IdAnimal;
                    ((LightBaseParameter)command.Parameters["COM_docDescricao"]).Value = compra.Descricao;
                    ((LightBaseParameter)command.Parameters["COM_moeValor"]).Value = compra.Valor;

                    command.Connection = _Connection;
                    command.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("Não é possível inserir para um Animal existente.");
                }
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro na edição", exc);
                throw except;
            }
        }

        private string BuildCompraString(List<string> campos, int compraId)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + ":@" + campo);
            }
            return "update FCARNAUBA_FINANCEIRO set Compras[" + compraId + "] = {" + String.Join(",", outCampos) +
                   "} where id = @id";
        }


        public List<Compra> GetCompra(int itemFinanceiroID)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_FINANCEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Compra>();

            if (qReader.Read())
            {
                var res = (DataTable)qReader["Compras"];
                int c = 0;
                int id = Convert.ToInt32(qReader["id"]);

                foreach (DataRow dRow in res.Rows)
                {
                    var compra = DataRowToCompra(dRow);
                    compra.Id = c;
                    compra.FinanceiroId = id;
                    compra.CompraFinanceiroId = compra.FinanceiroId + " " + compra.Id;
                    retVal.Add(compra);
                    c++;
                }


            }

            qReader.Close();
            return retVal;
        }

        public Compra GetCompraById(int itemFinanceiroID, int compraId)
        {
            var compraList = GetCompra(itemFinanceiroID);
            return compraList[compraId];
        }

        public void RemoverCompra(int itemFinanceiroId, int compraId)
        {
            try
            {

                var command = new LightBaseCommand(@"delete from FCARNAUBA_FINANCEIRO.Compras[" + compraId + "] where id = " + itemFinanceiroId);
                command.Connection = _Connection;
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }

        }

        public Documento[] ObtemDocumentos(CriterioPesquisaDocumentos criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_FINANCEIRO where id = @id");
                ((LightBaseParameter)command.Parameters["id"]).Value = criterio.IdFinanceiro;
                command.Connection = _Connection;
                var qReader = command.ExecuteReader();

                var retVal = new List<Documento>();

                if (qReader.Read())
                {
                    var res = (DataTable)qReader["Documentos"];
                    int d = 0;
                    int id = Convert.ToInt32(qReader["id"]);

                    foreach (DataRow dRow in res.Rows)
                    {
                        var documento = DataRowToDocumento(dRow);
                        documento.Id = d;
                        documento.FinanceiroId = id;
                        documento.DocumentoFinanceiroId = documento.FinanceiroId + " " + documento.Id;

                        retVal.Add(documento);
                        d++;
                    }
                }

                qReader.Close();
                return retVal.ToArray();
            }

            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }


            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public void InserirDocumento(string itemFinanceiroId, Documento documento)
        {
            try
            {
                var command = new LightBaseCommand(@"insert into FCARNAUBA_FINANCEIRO Documentos values ({{@DOC_strDescricao, @DOC_dtDataAnexo, @DOC_strPDFDocumento}}) parent id = @id");

                ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroId;
                ((LightBaseParameter)command.Parameters["DOC_strDescricao"]).Value = documento.Descricao;
                ((LightBaseParameter)command.Parameters["DOC_dtDataAnexo"]).Value = DateTime.Today;

                if (documento.HasPDF())
                {
                    ((LightBaseParameter)command.Parameters["DOC_strPDFDocumento"]).Value = documento.PDFDocumento.FileName;
                    SalvaMapeiaArquivoGerado(documento.PDFDocumento);
                }

                command.Connection = _Connection;
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }

        }

        public void AlterarDocumento(int itemFinanceiroId, int documentoId, Documento documento)
        {
            try
            {
                List<string> campos = new List<string>()
                                      { "DOC_strDescricao",
                                        "DOC_dtDataAnexo",
                                      };

                if (documento.HasPDF())
                {
                    campos.Add("DOC_strPDFDocumento");
                }

                var command = new LightBaseCommand(BuildDocumentoString(campos, documentoId));
                ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroId;
                ((LightBaseParameter)command.Parameters["DOC_strDescricao"]).Value = documento.Descricao;
                ((LightBaseParameter)command.Parameters["DOC_dtDataAnexo"]).Value = DateTime.Today;

                if (documento.HasPDF())
                {
                    ((LightBaseParameter)command.Parameters["DOC_strPDFDocumento"]).Value = documento.PDFDocumento.FileName;
                    SalvaMapeiaArquivoGerado(documento.PDFDocumento);
                }

                command.Connection = _Connection;
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro na edição", exc);
                throw except;
            }
        }

        private string BuildDocumentoString(List<string> campos, int documentoId)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + ":@" + campo);
            }
            return "update FCARNAUBA_FINANCEIRO set Documentos[" + documentoId + "] = {" + String.Join(",", outCampos) +
                   "} where id = @id";
        }

        public List<Documento> GetDocumento(int itemFinanceiroID)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_FINANCEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Documento>();

            if (qReader.Read())
            {
                var res = (DataTable)qReader["Documentos"];
                int d = 0;
                int id = Convert.ToInt32(qReader["id"]);

                foreach (DataRow dRow in res.Rows)
                {
                    var documento = DataRowToDocumento(dRow);
                    documento.Id = d;
                    documento.FinanceiroId = id;
                    documento.DocumentoFinanceiroId = documento.FinanceiroId + " " + documento.Id;

                    retVal.Add(documento);
                    d++;
                }


            }

            qReader.Close();
            return retVal;
        }

        public Documento GetDocumentoById(int itemFinanceiroID, int documentoId)
        {
            var documentoList = GetDocumento(itemFinanceiroID);
            return documentoList[documentoId];
        }

        public string GetNomeArquivoOriginal(string nomeArquivoGerado)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_ARQUIVOS where strArquivoGerado = @strArquivoGerado");
            ((LightBaseParameter)command.Parameters["strArquivoGerado"]).Value = nomeArquivoGerado;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            string retVal = nomeArquivoGerado;
            while (qReader.Read())
            {
                retVal = qReader["strNomeOriginal"].ToString();
            }
            qReader.Close();
            return retVal;
        }

        public void RemoverDocumento(int itemFinanceiroId, int documentoId)
        {
            try
            {

                var command = new LightBaseCommand(@"delete from FCARNAUBA_FINANCEIRO.Documentos[" + documentoId + "] where id = " + itemFinanceiroId);
                command.Connection = _Connection;
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }

        }


        public void InserirParcela(string itemFinanceiroId, Parcela parcela)
        {
            try
            {
                if (NParcelaExists(itemFinanceiroId, parcela.NParcela.ToString()))
                {
                    // Para o caso da parcela não poder ser duplicada
                    throw new Exception("Não é possível inserir uma parcela existente.");
                }
                else
                {
                    var command = new LightBaseCommand(@"insert into FCARNAUBA_FINANCEIRO Parcelas values ({{@PAR_intNParcela, @PAR_dtDataParcela, @PAR_moeValorInicial, @PAR_moeValorPago, @PAR_dtDataPagamento}}) parent id = @id");

                    ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroId;
                    ((LightBaseParameter)command.Parameters["PAR_intNParcela"]).Value = parcela.NParcela;
                    ((LightBaseParameter)command.Parameters["PAR_dtDataParcela"]).Value = parcela.Data;
                    ((LightBaseParameter)command.Parameters["PAR_moeValorInicial"]).Value = parcela.ValorInicial;
                    ((LightBaseParameter)command.Parameters["PAR_moeValorPago"]).Value = parcela.ValorPago;
                    ((LightBaseParameter)command.Parameters["PAR_dtDataPagamento"]).Value = parcela.DataPagamento;

                    command.Connection = _Connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }

        }

        public void InserirParcelas(DateTime data, double valorTotal, int nParcelas, long itemFinanceiroId)
        {
            try
            {
                double valorInicialParcela = valorTotal / nParcelas;

                for (int ctr = 1; ctr <= nParcelas; ctr++)
                {
                    DateTime addMonthDate = data.AddMonths(ctr);

                    var command = new LightBaseCommand(@"insert into FCARNAUBA_FINANCEIRO Parcelas values ({{@PAR_intNParcela, @PAR_dtDataParcela, @PAR_moeValorInicial}}) parent id = @id");

                    ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroId;
                    ((LightBaseParameter)command.Parameters["PAR_intNParcela"]).Value = ctr;
                    ((LightBaseParameter)command.Parameters["PAR_dtDataParcela"]).Value = addMonthDate;
                    ((LightBaseParameter)command.Parameters["PAR_moeValorInicial"]).Value = valorInicialParcela;

                    command.Connection = _Connection;
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }

        }

        public void AlterarParcela(int itemFinanceiroId, int parcelaId, Parcela parcela)
        {
            try
            {
                Parcela parcelaCorr = GetParcelaById(itemFinanceiroId, parcelaId);

                List<string> campos = new List<string>()
                                      { "PAR_intNParcela",
                                        "PAR_dtDataParcela",
                                        "PAR_moeValorInicial",
                                        "PAR_moeValorPago",
                                        "PAR_dtDataPagamento",
                                      };


                if ((NParcelaExists(itemFinanceiroId.ToString(), parcela.NParcela.ToString()) && (parcela.Data != parcelaCorr.Data || parcela.DataPagamento != parcelaCorr.DataPagamento || parcela.ValorInicial != parcelaCorr.ValorInicial || parcela.ValorPago != parcelaCorr.ValorPago)) || !NParcelaExists(itemFinanceiroId.ToString(), parcela.NParcela.ToString()))
                {

                    var command = new LightBaseCommand(BuildParcelaString(campos, parcelaId));
                    ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroId;
                    ((LightBaseParameter)command.Parameters["PAR_intNParcela"]).Value = parcela.NParcela;
                    ((LightBaseParameter)command.Parameters["PAR_dtDataParcela"]).Value = parcela.Data;
                    ((LightBaseParameter)command.Parameters["PAR_moeValorInicial"]).Value = parcela.ValorInicial;
                    ((LightBaseParameter)command.Parameters["PAR_moeValorPago"]).Value = parcela.ValorPago;
                    ((LightBaseParameter)command.Parameters["PAR_dtDataPagamento"]).Value = parcela.DataPagamento;

                    command.Connection = _Connection;
                    command.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("Não é possível alterar para uma parcela existente.");
                }
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro na edição", exc);
                throw except;
            }
        }

        private string BuildParcelaString(List<string> campos, int parcelaId)
        {
            var outCampos = new List<string>();
            foreach (var campo in campos)
            {
                outCampos.Add(campo + ":@" + campo);
            }
            return "update FCARNAUBA_FINANCEIRO set Parcelas[" + parcelaId + "] = {" + String.Join(",", outCampos) +
                   "} where id = @id";
        }

        public List<Parcela> GetParcela(int itemFinanceiroID)
        {
            var command = new LightBaseCommand(@"select * from FCARNAUBA_FINANCEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = itemFinanceiroID;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();

            var retVal = new List<Parcela>();

            if (qReader.Read())
            {
                var res = (DataTable)qReader["Parcelas"];
                int p = 0;
                int id = Convert.ToInt32(qReader["id"]);

                foreach (DataRow dRow in res.Rows)
                {
                    var parcela = DataRowToParcela(dRow);
                    parcela.Id = p;
                    parcela.FinanceiroId = id;
                    parcela.ParcelaFinanceiroId = parcela.FinanceiroId + " " + parcela.Id;
                    retVal.Add(parcela);
                    p++;
                }


            }

            qReader.Close();
            return retVal;
        }

        public Parcela GetParcelaById(int itemFinanceiroID, int parcelaId)
        {
            var parcelaList = GetParcela(itemFinanceiroID);
            return parcelaList[parcelaId];
        }

        public Parcela[] ObtemParcelas(CriterioPesquisaParcelas criterio)
        {
            try
            {
                var command = new LightBaseCommand(@"select * from FCARNAUBA_FINANCEIRO where id = @id");
                ((LightBaseParameter)command.Parameters["id"]).Value = criterio.IdFinanceiro;
                command.Connection = _Connection;
                var qReader = command.ExecuteReader();

                var retVal = new List<Parcela>();

                if (qReader.Read())
                {
                    var res = (DataTable)qReader["Parcelas"];
                    int p = 0;
                    int id = Convert.ToInt32(qReader["id"]);

                    foreach (DataRow dRow in res.Rows)
                    {
                        var parcela = DataRowToParcela(dRow);
                        parcela.Id = p;
                        parcela.FinanceiroId = id;
                        parcela.ParcelaFinanceiroId = parcela.FinanceiroId + " " + parcela.Id;
                        retVal.Add(parcela);
                        p++;
                    }
                }

                qReader.Close();

                var orderParcelas = retVal.OrderBy(s => s.DataPagamentoDt);

                return orderParcelas.ToArray();
            }

            catch (Exception e)
            {
                Exception except = new Exception("Não foi possível consultar!");
                throw except;
            }


            finally
            {
                //if (!qReader.IsClosed)
                //{
                //    qReader.Close();
                //}
            }
        }

        public void RemoverParcela(int itemFinanceiroId, int parcelaId)
        {
            try
            {

                var command = new LightBaseCommand(@"delete from FCARNAUBA_FINANCEIRO.Parcelas[" + parcelaId + "] where id = " + itemFinanceiroId);
                command.Connection = _Connection;
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Exception except = new Exception("Erro genérico", exc);
                throw except;
            }

        }


        public InformacoesRebanho GetInformacoesRebanho(string raca)
        {
            var command = new LightBaseCommand(@"select Historico from FCARNAUBA_ANIMAIS where strRaca = @strRaca");
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            string retVal = "";
            int totalAnimais = 0;

            while (qReader.Read())
            {
                var res = qReader["Historico"] as DataTable;
                if (res == null)
                {
                    totalAnimais++;
                    break;
                }

                var linhas = res.Rows.Count;

                if (linhas > 0)
                {
                    DataRow dRow = res.Rows[linhas - 1];

                    if (dRow["MA_strMovimento"] != DBNull.Value)
                    {
                        retVal = (string)dRow["MA_strMovimento"];

                        if (retVal != "Morto" && retVal != "Inativo" && retVal != "Vendido" && retVal != "Abatido")
                        {
                            totalAnimais++;
                        }
                    }

                }

            }
            qReader.Close();

            InformacoesRebanho infornacoesRebanho = new InformacoesRebanho();
            infornacoesRebanho.Raca = "REBANHO " + raca;
            infornacoesRebanho.TotalAnimais = totalAnimais;

            return infornacoesRebanho;
        }

        public InformacoesRebanho GetInformacoesRebanho(string raca, string propriedade)
        {
            var command = new LightBaseCommand(@"select Historico from FCARNAUBA_ANIMAIS where strRaca = @strRaca and strNomeFazenda = @strNomeFazenda");
            ((LightBaseParameter)command.Parameters["strRaca"]).Value = raca;
            ((LightBaseParameter)command.Parameters["strNomeFazenda"]).Value = propriedade;
            command.Connection = _Connection;
            var qReader = command.ExecuteReader();
            string retVal = "";
            int totalAnimais = 0;

            while (qReader.Read())
            {
                var res = qReader["Historico"] as DataTable;
                if (res == null)
                {
                    totalAnimais++;
                    break;
                }

                var linhas = res.Rows.Count;

                if (linhas > 0)
                {
                    DataRow dRow = res.Rows[linhas - 1];

                    if (dRow["MA_strMovimento"] != DBNull.Value)
                    {
                        retVal = (string)dRow["MA_strMovimento"];

                        if (retVal != "Morto" && retVal != "Inativo" && retVal != "Vendido" && retVal != "Abatido")
                        {
                            totalAnimais++;
                        }
                    }

                }

            }
            qReader.Close();

            InformacoesRebanho infornacoesRebanho = new InformacoesRebanho();
            infornacoesRebanho.Raca = "REBANHO " + raca;
            infornacoesRebanho.TotalAnimais = totalAnimais;

            return infornacoesRebanho;
        }

        public bool ItemFinanceiroValidado(long idFinanceiro)
        {
            bool validado = false;
            var command = new LightBaseCommand(@"select strUsuarioValidacao from FCARNAUBA_FINANCEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = idFinanceiro;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            if (qReader.Read())
            {

                if (qReader["strUsuarioValidacao"] != DBNull.Value) validado = true;
            }
            qReader.Close();
            return validado;
        }

        public double GetTotalPago(string financeiroId)
        {
            var command = new LightBaseCommand(@"select Parcelas.PAR_moeValorPago from FCARNAUBA_FINANCEIRO where id = @id");
            ((LightBaseParameter)command.Parameters["id"]).Value = financeiroId;
            command.Connection = _Connection;

            var qReader = command.ExecuteReader();
            double totalPago = 0;

            try
            {
                if (qReader.Read())
                {
                    DataTable tableParcelas = (DataTable)qReader["Parcelas"];

                    foreach (DataRow parcelasRow in tableParcelas.Rows)
                    {

                        if ((parcelasRow["PAR_moeValorPago"] != DBNull.Value))
                        {
                            totalPago += (double)(parcelasRow["PAR_moeValorPago"]);
                        }


                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                qReader.Close();
            }


            return totalPago;
        }

    }
}
