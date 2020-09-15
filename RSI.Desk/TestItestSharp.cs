using iTextSharp.text;
using iTextSharp.text.pdf;
using RSI.Negocio;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class TestItestSharp : Form
    {
        private object folderBrowserDialogGuardar;

        public TestItestSharp()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            #region Common Part
            PdfPTable pdfTableBlank = new PdfPTable(1);

            //Footer Section
            PdfPTable pdfTableFooter = new PdfPTable(1);
            pdfTableFooter.DefaultCell.BorderWidth = 0;
            pdfTableFooter.WidthPercentage = 100;
            pdfTableFooter.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            Chunk cnkFooter = new Chunk("Mi Tierra Eventos", FontFactory.GetFont("Times New Roman"));
            //cnkFooter.Font.SetStyle(1);
            cnkFooter.Font.Size = 10;
            pdfTableFooter.AddCell(new Phrase(cnkFooter));
            //End Of Footer Section

            pdfTableBlank.AddCell(new Phrase(" "));
            pdfTableBlank.DefaultCell.Border = 0;
            #endregion

            #region Page
            #region Section-1

            PdfPTable pdfTable1 = new PdfPTable(1);//Here 1 is Used For Count of Column
            PdfPTable pdfTable2 = new PdfPTable(1);
            PdfPTable pdfTable3 = new PdfPTable(2);

            //Font Style
            System.Drawing.Font fontH1 = new System.Drawing.Font("Currier", 16);

            //pdfTable1.DefaultCell.Padding = 5;
            pdfTable1.WidthPercentage = 80;
            pdfTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable1.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            //pdfTable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
            pdfTable1.DefaultCell.BorderWidth = 0;


            //pdfTable1.DefaultCell.Padding = 5;
            pdfTable2.WidthPercentage = 80;
            pdfTable2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable2.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            //pdfTab2e1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
            pdfTable2.DefaultCell.BorderWidth = 0;

            pdfTable3.DefaultCell.Padding = 5;
            pdfTable3.WidthPercentage = 80;
            pdfTable3.DefaultCell.BorderWidth = 0.5f;


            Chunk c1 = new Chunk("Reserva Turística", FontFactory.GetFont("Times New Roman"));
            c1.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c1.Font.SetStyle(0);
            c1.Font.Size = 14;
            Phrase p1 = new Phrase();
            p1.Add(c1);
            pdfTable1.AddCell(p1);
            Chunk c2 = new Chunk($"Reserva Numero: {"1235"}", FontFactory.GetFont("Times New Roman"));
            c2.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c2.Font.SetStyle(0);//0 For Normal Font
            c2.Font.Size = 11;
            Phrase p2 = new Phrase();
            p2.Add(c2);
            pdfTable2.AddCell(p2);
            Chunk c3 = new Chunk("Customer Care : 033-XXXX-XXXX, 98300 56947 / XXXXX XXXXX", FontFactory.GetFont("Times New Roman"));
            c3.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c3.Font.SetStyle(0);
            c3.Font.Size = 11;
            Phrase p3 = new Phrase();
            p3.Add(c3);
            pdfTable2.AddCell(p3);

            #endregion
            #region Section-1
            PdfPTable pdfTable4 = new PdfPTable(4);
            pdfTable4.DefaultCell.Padding = 5;
            pdfTable4.WidthPercentage = 80;
            pdfTable4.DefaultCell.BorderWidth = 0.0f;

            pdfTable4.AddCell(new Phrase("Bill No "));
            pdfTable4.AddCell(new Phrase("B001"));
            pdfTable4.AddCell(new Phrase("Date "));
            pdfTable4.AddCell(new Phrase("01-01-2017"));
            pdfTable4.AddCell(new Phrase("Vendor "));
            pdfTable4.AddCell(new Phrase("Demo Vendor"));
            pdfTable4.AddCell(new Phrase("Address "));
            pdfTable4.AddCell(new Phrase("Kolkata"));
            #endregion
            #region Section-Image

            string imageURL = @"D:\ReservasTuristicas\RSI.Mvc.Web\img\Logo.jpg";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);

            //Resize image depend upon your need
            jpg.ScaleToFit(140f, 120f);
            //Give space before image
            jpg.SpacingBefore = 10f;
            //Give some space after the image
            jpg.SpacingAfter = 1f;

            jpg.Alignment = Element.ALIGN_CENTER;
            #endregion

            #region section Table
            pdfTable3.AddCell(new Phrase("COMPANY NAME "));
            pdfTable3.AddCell(new Phrase(""));
            pdfTable3.AddCell(new Phrase("JOB TITLE "));
            pdfTable3.AddCell(new Phrase(""));

            pdfTable3.AddCell(new Phrase("ADDRESS"));
            pdfTable3.AddCell(new Phrase(""));
            pdfTable3.AddCell(new Phrase("CONTACT NO "));
            pdfTable3.AddCell(new Phrase(""));
            #endregion

            #endregion


            #region Pdf Generation
            string folderPath = "D:\\PDF\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //File Name
            int fileCount = Directory.GetFiles("D:\\PDF").Length;
            string strFileName = "DescriptionForm" + (fileCount + 1) + ".pdf";

            using (FileStream stream = new FileStream(folderPath + strFileName, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                #region PAGE-1
                pdfDoc.Add(pdfTable1);
                pdfDoc.Add(pdfTable2);
                pdfDoc.Add(pdfTableBlank);
                pdfDoc.Add(jpg);
                pdfDoc.Add(pdfTable3);
                pdfDoc.Add(pdfTableFooter);
                pdfDoc.NewPage();
                #endregion

                // pdfDoc.Add(jpg);

                //pdfDoc.Add(pdfTable2);
                pdfDoc.Close();
                stream.Close();
            }
            #endregion

            #region Display PDF
            System.Diagnostics.Process.Start(folderPath + "\\" + strFileName);
            #endregion
        }
    }
}
