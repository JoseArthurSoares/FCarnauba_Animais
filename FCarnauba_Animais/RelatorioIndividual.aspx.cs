using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FCarnauba_Animais.DataAccess;
using FCarnauba_Animais.util;
using LightInfocon.GoldenAccess.General;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;
using Image = iTextSharp.text.Image;
using ListItem = System.Web.UI.WebControls.ListItem;


namespace FCarnauba_Animais
{
    public partial class RelatorioIndividual : PaginaBase
    {
        private FCarnaubaFacade _fCarnaubaFacade = new FCarnaubaFacade();
        public string Id
        {
            get
            {
                string id = Request.Params["id"];
                return id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Id != "99999")
            {
                if (!IsPostBackOrCallBack())
                {
                    RMachoFemea machoFemea = _fCarnaubaFacade.GetMachoFemea(Id);
                    if (machoFemea.Sexo == "Fêmea")
                    {
                        GerarPdfFemea(machoFemea);
                    }
                    else
                    {
                        GerarPdfMacho(machoFemea);
                    }
                }
            }

        }

        public void GerarPdfFemea(RMachoFemea machoFemea)
        {

            Document doc = new Document(PageSize.A4.Rotate());
            doc.SetMargins(20, 20, 20, 20);
            doc.AddCreationDate();//adicionando as configuracoes
            var dataHoje = DateTime.Now;

            //caminho onde sera criado o pdf + nome desejado
            //OBS: o nome sempre deve ser terminado com .pdf
            string nome = machoFemea.Rgd.Trim() + dataHoje.Day + dataHoje.Month + dataHoje.Year + dataHoje.Hour + dataHoje.Minute + dataHoje.Second + ".pdf";
            string caminho = FCarnaubaSettings.DocumentosGerados + "//" + nome;


            //tambem criada acima.
            PdfWriter writer = PdfWriter.GetInstance(doc, new
            FileStream(caminho, FileMode.Create));

            //doc.Header = new HeaderFooter(new Phrase("PROCON Campina grande"), false);
            //doc.Header = new HeaderFooter(new Phrase("Cálculo da Multa"), false);

            doc.Open();

            //criando uma string vazia
            string dados = "";

            //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(@"C:\PROCONCG\ProconCGDOSIMETRIA\ProconCGDOSIMETRIA\Imagens\logoProcon.png");

            string imageURL = @"C:\FCarnaubaAnimais\img\logoFazeLisa.png";
            ////string imageURL = @"C:\FCarnauba_Animais\FCarnauba_Animais\img\logoFaze.png";

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);

            ////Resize image depend upon your need

            //jpg.ScaleToFit(500f, 428f);

            ////Give space before image

            jpg.SpacingBefore = 10f;

            ////Give some space after the image

            jpg.SpacingAfter = 1f;

            jpg.Alignment = Element.ALIGN_CENTER;

            doc.Add(jpg);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo4 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo4.Alignment = Element.ALIGN_CENTER;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo4.Add("PROCON Campina Grande");
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo4);

            //criando a variavel para paragrafo
            Paragraph paragrafo5 = new Paragraph(dados,
            new Font(Font.NORMAL, 14));
            //etipulando o alinhamneto
            paragrafo5.Alignment = Element.ALIGN_CENTER;
            //Alinhamento Justificado
            //adicioando texto
            paragrafo5.Add("FICHA INDIVIDUAL");
            //acidionado paragrafo ao documento
            doc.Add(paragrafo5);

            doc.Add(new Paragraph(" "));

            ////criando a variavel para paragrafo
            //Paragraph paragrafo1 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo1.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo1.Add("Nome: " + machoFemea.NomeCompleto);
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo1);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo3 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo3.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo3.Add("RGD: " + machoFemea.Rgd);
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo3);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo5 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo5.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo5.Add("Data Nascimento: " + machoFemea.DataNascimento);
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo5);

            //doc.Add(new Paragraph(" "));

            //header
            PdfPTable tableAnimal = new PdfPTable(8) { HorizontalAlignment = Element.ALIGN_CENTER, WidthPercentage = 100 };
            float[] anchoDeColumnasAnimal = new float[] { 10f, 7f, 6f, 6f, 5f, 6f, 5f, 5f };
            tableAnimal.SetWidths(anchoDeColumnasAnimal);
            Font fonteAnimal = new Font(Font.NORMAL, 10);

            Phrase nomeAnimal = new Phrase("NOME", fonteAnimal);
            var cellAnimal = new PdfPCell(nomeAnimal);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase rgdH1 = new Phrase("RGD", fonteAnimal);
            cellAnimal = new PdfPCell(rgdH1);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase dataNascimento = new Phrase("DATA NASC", fonteAnimal);
            cellAnimal = new PdfPCell(dataNascimento);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase pai = new Phrase("PAI", fonteAnimal);
            cellAnimal = new PdfPCell(pai);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase rgdPai = new Phrase("RGD", fonteAnimal);
            cellAnimal = new PdfPCell(rgdPai);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase mae = new Phrase("MÃE", fonteAnimal);
            cellAnimal = new PdfPCell(mae);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase rgdMae = new Phrase("RGD", fonteAnimal);
            cellAnimal = new PdfPCell(rgdMae);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase raca = new Phrase("RAÇA", fonteAnimal);
            cellAnimal = new PdfPCell(raca);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);


            //row
            nomeAnimal = new Phrase(machoFemea.Nome, fonteAnimal);
            cellAnimal = new PdfPCell(nomeAnimal);
            tableAnimal.AddCell(cellAnimal);

            Phrase rgd = new Phrase(machoFemea.Rgd, fonteAnimal);
            cellAnimal = new PdfPCell(rgd);
            tableAnimal.AddCell(cellAnimal);

            dataNascimento = new Phrase((machoFemea.DataNascimento.ToString("dd/MM/yyyy")), fonteAnimal);
            cellAnimal = new PdfPCell(dataNascimento);
            tableAnimal.AddCell(cellAnimal);

            pai = new Phrase(machoFemea.NomePai, fonteAnimal);
            cellAnimal = new PdfPCell(pai);
            tableAnimal.AddCell(cellAnimal);

            rgdPai = new Phrase(machoFemea.RgdPai, fonteAnimal);
            cellAnimal = new PdfPCell(rgdPai);
            tableAnimal.AddCell(cellAnimal);

            mae = new Phrase(machoFemea.NomeMae, fonteAnimal);
            cellAnimal = new PdfPCell(mae);
            tableAnimal.AddCell(cellAnimal);

            rgdMae = new Phrase(machoFemea.RgdMae, fonteAnimal);
            cellAnimal = new PdfPCell(rgdMae);
            tableAnimal.AddCell(cellAnimal);

            raca = new Phrase(machoFemea.Raca, fonteAnimal);
            cellAnimal = new PdfPCell(raca);
            tableAnimal.AddCell(cellAnimal);

            //Phrase sexo = new Phrase(cria.Sexo, fonte);
            //cell = new PdfPCell(sexo);
            //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            //table.AddCell(cell);

            //Phrase datanascimento = new Phrase((cria.DataNascimento.ToString("dd/MM/yyyy")), fonte);
            //cell = new PdfPCell(datanascimento);
            //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            //table.AddCell(cell);

            doc.Add(tableAnimal);

            var crias = machoFemea.Crias;

            PdfPTable table = new PdfPTable(15) { HorizontalAlignment = Element.ALIGN_CENTER, WidthPercentage = 100 };
            float[] anchoDeColumnas = new float[] { 3f, 2f, 5f, 2f, 8f, 5f, 8f, 5f, 4f, 3f, 3f, 3f, 3f, 3f, 3f };
            table.SetWidths(anchoDeColumnas);
            Font fonte = new Font(Font.NORMAL, 9);

            Phrase ncriah = new Phrase("Cria", fonte);
            var cellh = new PdfPCell(ncriah);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase sexoh = new Phrase("Sexo", fonte);
            cellh = new PdfPCell(sexoh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase datanascimentoh = new Phrase("Data Nasc.", fonte);
            cellh = new PdfPCell(datanascimentoh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            cellh.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cellh);

            Phrase pnh = new Phrase("P.N.", fonte);
            cellh = new PdfPCell(pnh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase paih = new Phrase("PAI", fonte);
            cellh = new PdfPCell(paih);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase rgPaih = new Phrase("RG", fonte);
            cellh = new PdfPCell(rgPaih);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase criah = new Phrase("NOME", fonte);
            cellh = new PdfPCell(criah);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase rgh = new Phrase("RG", fonte);
            cellh = new PdfPCell(rgh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase ippieph = new Phrase("IPP/IEP", fonte);
            cellh = new PdfPCell(ippieph);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase erh = new Phrase("E.R.", fonte);
            cellh = new PdfPCell(erh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase kgieph = new Phrase("KG/IEP", fonte);
            cellh = new PdfPCell(kgieph);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase pmedh = new Phrase("Méd.", fonte);
            cellh = new PdfPCell(pmedh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase pmaxh = new Phrase("Máx.", fonte);
            cellh = new PdfPCell(pmaxh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase pinicialh = new Phrase("Inic.", fonte);
            cellh = new PdfPCell(pinicialh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase pfinalh = new Phrase("Final", fonte);
            cellh = new PdfPCell(pfinalh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            foreach (var cria in crias)
            {


                Phrase ncria = new Phrase(cria.NCria, fonte);
                var cell = new PdfPCell(ncria);
                table.AddCell(cell);

                string ssexo = "";

                if (cria.Sexo == "Macho")
                {
                    ssexo = "M";
                }
                else
                {
                    ssexo = "F";
                }

                Phrase sexo = new Phrase(ssexo, fonte);
                cell = new PdfPCell(sexo);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase datanascimento = new Phrase((cria.DataNascimento.ToString("dd/MM/yyyy")), fonte);
                cell = new PdfPCell(datanascimento);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase pn = new Phrase(cria.Pn.ToString(), fonte);
                cell = new PdfPCell(pn);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase nomePai = new Phrase(cria.NomePai, fonte);
                cell = new PdfPCell(nomePai);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase rgdPai2 = new Phrase(cria.RgdPai, fonte);
                cell = new PdfPCell(rgdPai2);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase nomeCria = new Phrase(cria.Nome, fonte);
                cell = new PdfPCell(nomeCria);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase rgd2 = new Phrase(cria.Rgd, fonte);
                cell = new PdfPCell(rgd2);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var ippiepcorr = cria.IppIep.ToString();
                if (ippiepcorr == "0")
                    ippiepcorr = "-";
                Phrase ippiep = new Phrase(ippiepcorr, fonte);
                cell = new PdfPCell(ippiep);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var ercorr = cria.Er.ToString();
                if (ercorr == "0")
                    ercorr = "-";
                Phrase er = new Phrase(ercorr, fonte);
                cell = new PdfPCell(er);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var kgiepcorr = cria.KgIep.ToString();
                if (kgiepcorr == "0")
                    kgiepcorr = "-";
                Phrase kgiep = new Phrase(kgiepcorr, fonte);
                cell = new PdfPCell(kgiep);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var pmedcorr = cria.PMedia.ToString();
                if (pmedcorr == "0")
                    pmedcorr = "-";
                Phrase pmed = new Phrase(pmedcorr, fonte);
                cell = new PdfPCell(pmed);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var pmaxcorr = cria.PMaxima.ToString();
                if (pmaxcorr == "0")
                    pmaxcorr = "-";
                Phrase pmax = new Phrase(pmaxcorr, fonte);
                cell = new PdfPCell(pmax);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var pinicialcorr = cria.PInicial.ToString();
                if (pinicialcorr == "0")
                    pinicialcorr = "-";
                Phrase pinicial = new Phrase(pinicialcorr, fonte);
                cell = new PdfPCell(pinicial);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var pfinalcorr = cria.PFinal.ToString();
                if (pfinalcorr == "0")
                    pfinalcorr = "-";
                Phrase pfinal = new Phrase(pfinalcorr, fonte);
                cell = new PdfPCell(pfinal);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                //Phrase valorAtual = new Phrase((multa.ValorAtual.ToString("C")), fonte);
                //cell = new PdfPCell(valorAtual);
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //table.AddCell(cell);


            }

            doc.Add(table);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo2 = new Paragraph(dados,
            //new Font(Font.BOLD, 14));
            ////etipulando o alinhamneto
            //paragrafo2.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo2.Add("Valor da Infração: " + valor);
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo2);


            doc.Close();

            Response.Redirect("../DocumentosGerados/" + nome);


        }

        public void GerarPdfMacho(RMachoFemea machoFemea)
        {

            Document doc = new Document(PageSize.A4.Rotate());
            doc.SetMargins(40, 40, 40, 80);
            doc.AddCreationDate();//adicionando as configuracoes
            var dataHoje = DateTime.Now;

            //caminho onde sera criado o pdf + nome desejado
            //OBS: o nome sempre deve ser terminado com .pdf
            string nome = machoFemea.Rgd.Trim() + dataHoje.Day + dataHoje.Month + dataHoje.Year + dataHoje.Hour + dataHoje.Minute + dataHoje.Second + ".pdf";
            string caminho = FCarnaubaSettings.DocumentosGerados + "//" + nome;


            //tambem criada acima.
            PdfWriter writer = PdfWriter.GetInstance(doc, new
            FileStream(caminho, FileMode.Create));

            //doc.Header = new HeaderFooter(new Phrase("PROCON Campina grande"), false);
            //doc.Header = new HeaderFooter(new Phrase("Cálculo da Multa"), false);

            doc.Open();

            //criando uma string vazia
            string dados = "";

            string imageURL = @"C:\FCarnaubaAnimais\img\logoFazeLisa.png";
            ////string imageURL = @"C:\FCarnauba_Animais\FCarnauba_Animais\img\logoFaze.png";

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);

            ////Resize image depend upon your need

            //jpg.ScaleToFit(500f, 428f);

            ////Give space before image

            jpg.SpacingBefore = 10f;

            ////Give some space after the image

            jpg.SpacingAfter = 1f;

            jpg.Alignment = Element.ALIGN_CENTER;

            doc.Add(jpg);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo4 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo4.Alignment = Element.ALIGN_CENTER;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo4.Add("PROCON Campina Grande");
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo4);

            //criando a variavel para paragrafo
            Paragraph paragrafo5 = new Paragraph(dados,
            new Font(Font.NORMAL, 14));
            //etipulando o alinhamneto
            paragrafo5.Alignment = Element.ALIGN_CENTER;
            //Alinhamento Justificado
            //adicioando texto
            paragrafo5.Add("FICHA INDIVIDUAL");
            //acidionado paragrafo ao documento
            doc.Add(paragrafo5);

            doc.Add(new Paragraph(" "));

            ////criando a variavel para paragrafo
            //Paragraph paragrafo4 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo4.Alignment = Element.ALIGN_CENTER;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo4.Add("PROCON Campina Grande");
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo4);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo5 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo5.Alignment = Element.ALIGN_CENTER;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo5.Add("Cálculo da Multa");
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo5);

            doc.Add(new Paragraph(" "));

            ////criando a variavel para paragrafo
            //Paragraph paragrafo1 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo1.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo1.Add("Nome: " + machoFemea.NomeCompleto);
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo1);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo3 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo3.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo3.Add("RGD: " + machoFemea.Rgd);
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo3);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo5 = new Paragraph(dados,
            //new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo5.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo5.Add("Data Nascimento: " + machoFemea.DataNascimento);
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo5);

            //doc.Add(new Paragraph(" "));

            //header
            PdfPTable tableAnimal = new PdfPTable(8) { HorizontalAlignment = Element.ALIGN_CENTER, WidthPercentage = 100 };
            float[] anchoDeColumnasAnimal = new float[] { 10f, 7f, 6f, 6f, 5f, 6f, 5f, 5f };
            tableAnimal.SetWidths(anchoDeColumnasAnimal);
            Font fonteAnimal = new Font(Font.NORMAL, 11);

            Phrase nomeAnimal = new Phrase("NOME", fonteAnimal);
            var cellAnimal = new PdfPCell(nomeAnimal);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase rgdH1 = new Phrase("RGD", fonteAnimal);
            cellAnimal = new PdfPCell(rgdH1);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase dataNascimento = new Phrase("DATA NASC", fonteAnimal);
            cellAnimal = new PdfPCell(dataNascimento);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase pai = new Phrase("PAI", fonteAnimal);
            cellAnimal = new PdfPCell(pai);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase rgdPai = new Phrase("RGD", fonteAnimal);
            cellAnimal = new PdfPCell(rgdPai);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase mae = new Phrase("MÃE", fonteAnimal);
            cellAnimal = new PdfPCell(mae);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase rgdMae = new Phrase("RGD", fonteAnimal);
            cellAnimal = new PdfPCell(rgdMae);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);

            Phrase raca = new Phrase("RAÇA", fonteAnimal);
            cellAnimal = new PdfPCell(raca);
            cellAnimal.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tableAnimal.AddCell(cellAnimal);


            //row
            nomeAnimal = new Phrase(machoFemea.Nome, fonteAnimal);
            cellAnimal = new PdfPCell(nomeAnimal);
            tableAnimal.AddCell(cellAnimal);

            Phrase rgd = new Phrase(machoFemea.Rgd, fonteAnimal);
            cellAnimal = new PdfPCell(rgd);
            tableAnimal.AddCell(cellAnimal);

            dataNascimento = new Phrase((machoFemea.DataNascimento.ToString("dd/MM/yyyy")), fonteAnimal);
            cellAnimal = new PdfPCell(dataNascimento);
            tableAnimal.AddCell(cellAnimal);

            pai = new Phrase(machoFemea.NomePai, fonteAnimal);
            cellAnimal = new PdfPCell(pai);
            tableAnimal.AddCell(cellAnimal);

            rgdPai = new Phrase(machoFemea.RgdPai, fonteAnimal);
            cellAnimal = new PdfPCell(rgdPai);
            tableAnimal.AddCell(cellAnimal);

            mae = new Phrase(machoFemea.NomeMae, fonteAnimal);
            cellAnimal = new PdfPCell(mae);
            tableAnimal.AddCell(cellAnimal);

            rgdMae = new Phrase(machoFemea.RgdMae, fonteAnimal);
            cellAnimal = new PdfPCell(rgdMae);
            tableAnimal.AddCell(cellAnimal);

            raca = new Phrase(machoFemea.Raca, fonteAnimal);
            cellAnimal = new PdfPCell(raca);
            tableAnimal.AddCell(cellAnimal);

            //Phrase sexo = new Phrase(cria.Sexo, fonte);
            //cell = new PdfPCell(sexo);
            //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            //table.AddCell(cell);

            //Phrase datanascimento = new Phrase((cria.DataNascimento.ToString("dd/MM/yyyy")), fonte);
            //cell = new PdfPCell(datanascimento);
            //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            //table.AddCell(cell);

            doc.Add(tableAnimal);

            var crias = machoFemea.Crias;

            PdfPTable table = new PdfPTable(11) { HorizontalAlignment = Element.ALIGN_CENTER, WidthPercentage = 100 };
            float[] anchoDeColumnas = new float[] { 3f, 3f, 6f, 2f, 10f, 10f, 10f, 7f, 3f, 3f, 3f };
            table.SetWidths(anchoDeColumnas);
            Font fonte = new Font(Font.NORMAL, 10);

            Phrase ncriah = new Phrase("Cria", fonte);
            var cellh = new PdfPCell(ncriah);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase sexoh = new Phrase("Sexo", fonte);
            cellh = new PdfPCell(sexoh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase datanascimentoh = new Phrase("Data Nasc.", fonte);
            cellh = new PdfPCell(datanascimentoh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            cellh.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cellh);

            Phrase pnh = new Phrase("P.N.", fonte);
            cellh = new PdfPCell(pnh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase maeh = new Phrase("MÃE", fonte);
            cellh = new PdfPCell(maeh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase rgMaeh = new Phrase("RG", fonte);
            cellh = new PdfPCell(rgMaeh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);


            Phrase criah = new Phrase("NOME", fonte);
            cellh = new PdfPCell(criah);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            Phrase rgh = new Phrase("RG", fonte);
            cellh = new PdfPCell(rgh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            //Phrase ippieph = new Phrase("IPP/IEP", fonte);
            Phrase gmdh = new Phrase("GMD", fonte);
            cellh = new PdfPCell(gmdh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            //Phrase erh = new Phrase("E.R.", fonte);
            Phrase inicialh = new Phrase("Inic.", fonte);
            cellh = new PdfPCell(inicialh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            //Phrase kgieph = new Phrase("KG/IEP.", fonte);
            Phrase finalh = new Phrase("Final", fonte);
            cellh = new PdfPCell(finalh);
            cellh.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            table.AddCell(cellh);

            foreach (var cria in crias)
            {


                Phrase ncria = new Phrase(cria.NCria, fonte);
                var cell = new PdfPCell(ncria);
                table.AddCell(cell);

                string ssexo = "";

                if (cria.Sexo == "Macho")
                {
                    ssexo = "M";
                }
                else
                {
                    ssexo = "F";
                }

                Phrase sexo = new Phrase(ssexo, fonte);
                cell = new PdfPCell(sexo);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase datanascimento = new Phrase((cria.DataNascimento.ToString("dd/MM/yyyy")), fonte);
                cell = new PdfPCell(datanascimento);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase pn = new Phrase(cria.Pn.ToString(), fonte);
                cell = new PdfPCell(pn);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase nomeMae = new Phrase(cria.NomeMae, fonte);
                cell = new PdfPCell(nomeMae);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase rgdMae2 = new Phrase(cria.RgdMae, fonte);
                cell = new PdfPCell(rgdMae2);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);


                Phrase nomeCria = new Phrase(cria.Nome, fonte);
                cell = new PdfPCell(nomeCria);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                Phrase rgd2 = new Phrase(cria.Rgd, fonte);
                cell = new PdfPCell(rgd2);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                //var ippiepcorr = cria.IppIepMedio.ToString();
                //if (ippiepcorr == "0")
                //    ippiepcorr = "-";
                //Phrase ippiep = new Phrase(ippiepcorr, fonte);
                //cell = new PdfPCell(ippiep);
                //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                //table.AddCell(cell);

                //var ercorr = cria.ErMedio.ToString();
                //if (ercorr == "0")
                //    ercorr = "-";
                //Phrase er = new Phrase(ercorr, fonte);
                //cell = new PdfPCell(er);
                //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                //table.AddCell(cell);

                //var kgiepcorr = cria.KgIep.ToString();
                //if (kgiepcorr == "0")
                //    kgiepcorr = "-";
                //Phrase kgiep = new Phrase(kgiepcorr, fonte);
                //cell = new PdfPCell(kgiep);
                //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                //table.AddCell(cell);

                var gmdcorr = cria.Gmd.ToString();
                if (gmdcorr == "0")
                    gmdcorr = "-";
                Phrase gmd = new Phrase(gmdcorr, fonte);
                cell = new PdfPCell(gmd);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var pinicialcorr = cria.PInicial.ToString();
                if (pinicialcorr == "0")
                    pinicialcorr = "-";
                Phrase pinicial = new Phrase(pinicialcorr, fonte);
                cell = new PdfPCell(pinicial);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);

                var pfinalcorr = cria.PFinal.ToString();
                if (pfinalcorr == "0")
                    pfinalcorr = "-";
                Phrase pfinal = new Phrase(pfinalcorr, fonte);
                cell = new PdfPCell(pfinal);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                table.AddCell(cell);



                //Phrase valorAtual = new Phrase((multa.ValorAtual.ToString("C")), fonte);
                //cell = new PdfPCell(valorAtual);
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //table.AddCell(cell);


            }

            doc.Add(table);

            ////criando a variavel para paragrafo
            //Paragraph paragrafo2 = new Paragraph(dados,
            //new Font(Font.BOLD, 14));
            ////etipulando o alinhamneto
            //paragrafo2.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo2.Add("Valor da Infração: " + valor);
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo2);


            doc.Close();

            Response.Redirect("../DocumentosGerados/" + nome);


        }
    }
}