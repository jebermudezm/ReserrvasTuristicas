using System;
using iTextSharp.text;
using iTextSharp;
using iTextSharp.text.pdf;
using System.Drawing;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello World");
    }
}

public class PdfHelper
{
    public static iTextSharp.text.Image GetImgFrmUrl(string url)
    {
        return iTextSharp.text.Image.GetInstance(new Uri(url));
    }

    public static iTextSharp.text.Font TopHeaderFont()
    {
        return new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

    }
    public static iTextSharp.text.Font HeaderFont()
    {
        return new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

    }
    public static iTextSharp.text.Font NoramlFont()
    {
        return new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
    }
    public static PdfPCell CreateCell(Phrase text)
    {
        PdfPCell cell = new PdfPCell(text) { BorderWidth = 0, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 0 };

        return cell;
    }


    public static Phrase GetTopHeaderPhrase(string text)
    {
        Phrase phrs = new Phrase(text, TopHeaderFont());
        return phrs;
    }
    public static Phrase GetSubHeaderPhrase(string text)
    {
        Phrase phrs = new Phrase(text, HeaderFont());
        return phrs;
    }
    public static Phrase GetSubNormalPhrase(string text)
    {
        Phrase phrs = new Phrase(text, NoramlFont());
        return phrs;
    }
    public static PdfPCell MergeCells(int mergeCellCnt, PdfPCell cell)
    {
        cell.Colspan = mergeCellCnt;
        return cell;
    }
    public static iTextSharp.text.Image SetAlignment(iTextSharp.text.Image img)
    {
        img.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_LEFT;
        img.SpacingAfter = 9f;
        img.SpacingBefore = 9f;
        img.BorderWidthTop = 36f;
        img.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
        return img;
    }
    public static iTextSharp.text.pdf.PdfPTable CreateMainTable()
    {
        float[] columnWidths = { 400, 175 };
        iTextSharp.text.pdf.PdfPTable tbl = new iTextSharp.text.pdf.PdfPTable(2);
        tbl.WidthPercentage = 100;
        //tbl.TotalWidth=PageSize.A4.Width;
        tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tbl.AddCell(GetTitle());
        tbl.AddCell(CreateSubTable());
        return tbl;
    }

    public static iTextSharp.text.pdf.PdfPTable CreateSubTable()
    {
        var p = new Paragraph();
        PdfPTable tbl = new PdfPTable(2);
        tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tbl.AddCell((new Phrase("Insured: ", NoramlFont())));
        tbl.AddCell((new Phrase("TOMMY R & ELENA EST RUTLEDGE", HeaderFont())));
        tbl.AddCell((new Phrase("Claim #: ", NoramlFont())));
        tbl.AddCell((new Phrase("01781970", HeaderFont())));
        tbl.AddCell((new Phrase("Policy #: ", NoramlFont())));
        tbl.AddCell((new Phrase("DFS1242010", HeaderFont())));

        return tbl;
    }
    public static PdfPTable CreateSubTable2()
    {
        var p = new Paragraph();
        PdfPTable tbl = new PdfPTable(2);
        tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tbl.AddCell(new Phrase("Date Taken:", HeaderFont()));
        tbl.AddCell("8/28/2017");
        tbl.AddCell("Taken By:");
        tbl.AddCell("Karl Campbell");
        return tbl;
    }


    public static Paragraph GetTitle()
    {
        string line1 = "Photo Sheet" + "\n";
        string line2 = " Insurance" + "\n";
        Paragraph p1 = new Paragraph();
        Phrase ph1 = new Phrase(line1, TopHeaderFont());
        Phrase ph2 = new Phrase(line2, HeaderFont());
        p1.Add(ph1);
        p1.Add(ph2);
        return p1;
    }
}

public class ITextEvents : PdfPageEventHelper
{
    // This is the contentbyte object of the writer
    PdfContentByte cb;

    // we will put the final number of pages in a template
    PdfTemplate headerTemplate, footerTemplate;

    // this is the BaseFont we are going to use for the header / footer
    BaseFont bf = null;

    // This keeps track of the creation time
    DateTime PrintTime = DateTime.Now;

    #region Fields
    private string _header;
    #endregion

    #region Properties
    public string Header
    {
        get { return _header; }
        set { _header = value; }
    }
    #endregion    

    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
        try
        {
            PrintTime = DateTime.Now;
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            headerTemplate = cb.CreateTemplate(100, 100);
            footerTemplate = cb.CreateTemplate(50, 50);
        }
        catch (DocumentException de)
        {
        }
        catch (System.IO.IOException ioe)
        {
        }
    }

    public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
    {
        base.OnEndPage(writer, document);
        String text = "Page " + writer.PageNumber;

        //Create PdfTable object
        PdfPTable pdfTab = new PdfPTable(3);


        //Add paging to header
        {
            cb.BeginText();
            cb.SetFontAndSize(bf, 12);
            cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
            cb.ShowText(text);
            cb.EndText();
            float len = bf.GetWidthPoint(text, 12);
            //Adds "12" in Page 1 of 12
            cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));
        }
        //Add paging to footer
        {
            cb.BeginText();
            cb.SetFontAndSize(bf, 12);
            cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(30));
            cb.ShowText(text);
            cb.EndText();
            float len = bf.GetWidthPoint(text, 12);
            cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));
        }

        //Row 1 
        PdfPCell pdfCell5 = new PdfPCell(new Phrase("Date:" + PrintTime.ToShortDateString()));
        PdfPCell pdfCell6 = new PdfPCell();
        PdfPCell pdfCell7 = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now)));

        //set the alignment of all three cells and set border to 0
        pdfTab.AddCell(pdfCell5);
        pdfTab.AddCell(pdfCell6);
        pdfTab.AddCell(pdfCell7);

        pdfTab.TotalWidth = document.PageSize.Width - 80f;
        pdfTab.WidthPercentage = 70;
        //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;    

        //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
        //first param is start row. -1 indicates there is no end row and all the rows to be included to write
        //Third and fourth param is x and y position to start writing
        pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
        //set pdfContent value

        //Move the pointer and draw line to separate header section from rest of page
        cb.MoveTo(40, document.PageSize.Height - 100);
        cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
        cb.Stroke();

        //Move the pointer and draw line to separate footer section from rest of page
        cb.MoveTo(40, document.PageSize.GetBottom(50));
        pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
        cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
        cb.Stroke();
    }

    public override void OnCloseDocument(PdfWriter writer, Document document)
    {
        base.OnCloseDocument(writer, document);

        headerTemplate.BeginText();
        headerTemplate.SetFontAndSize(bf, 12);
        headerTemplate.SetTextMatrix(0, 0);
        headerTemplate.ShowText((writer.PageNumber - 1).ToString());
        headerTemplate.EndText();

        footerTemplate.BeginText();
        footerTemplate.SetFontAndSize(bf, 12);
        footerTemplate.SetTextMatrix(0, 0);
        footerTemplate.ShowText((writer.PageNumber - 1).ToString());
        footerTemplate.EndText();
    }
}