using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormPDF
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);
            string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\pdf\" + "relatorio.pdf";


            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            Paragraph titulo = new Paragraph();
            titulo.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 40);
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.Add("teste\n\n");
            doc.Add(titulo);

            Paragraph paragrafo = new Paragraph("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 12));
            string conteudo = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.\n\n";
            paragrafo.Add(conteudo);
            doc.Add(paragrafo);

            PdfPTable table = new PdfPTable(3);
            for (int i = 1; i <= 10; i++)
            {
                table.AddCell("Linha " + i + ", Coluna 1");
                table.AddCell("Linha " + i + ", Coluna 2");
                table.AddCell("Linha " + i + ", Coluna 3");
            }

            doc.Add(table);

            string simg = "https://trello-attachments.s3.amazonaws.com/570b74611cd5fe43e9d239c0/5bf4a765bacf1f555a85955f/8618d6b1bfd8b5bd7df764187a758770/logo-novo.png";
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(simg);
            img.ScaleAbsolute(100, 130);
            //img.SetAbsolutePosition(10, 30);

            doc.Add(img);

            doc.Close();
            Response.Redirect("/pdf/relatorio.pdf");
        }
    }
}