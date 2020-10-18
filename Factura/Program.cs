using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Content.Objects;
using PdfSharp.Pdf.IO;
namespace Factura
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            string fileName = "ticket.pdf";
            fileName = Guid.NewGuid().ToString("D").ToUpper() + ".pdf";
            PdfDocument document = new PdfDocument();
            document.Info.Title = "FACTURA DE VENTA";
            document.Info.Author = "";
            document.Info.Subject = "";
            document.Info.Keywords = "Other Words";

            // Create an empty page
            PdfPage page = document.AddPage();
            page.Width = 612;
            page.Height = 792;
            double marginLeft = 42.52;
            double marginRight = 42.52;
            double marginTop = 42.52;

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            string logoPath = @"C:\Users\gusvo\Desktop\logo.jpg";
            XImage image = XImage.FromFile(logoPath);
            gfx.DrawImage(image, 42.52, 42.52, 150, 60);

            XPen pen = new XPen(XColors.Plum, 4.7);


            string[] company = { "Supermercado el dorado", "1016072267", "Cra 90 bis #76-51", "3212261759", "gusvo21@hotmail.com" };
            string[] head = { "FACTURA DE VENTA", DateTime.UtcNow.ToShortDateString(), "#fACT11111"};
            string[] client = { "Gustavo Alejandro Sierra", "1016072267", "Cra 90 bis #76-51", "3212261759", "gusvo21@hotmail.com" };



            // Create a font
            XFont fontCompany = new XFont("Arial", 8, XFontStyle.Regular);
            XFont fontClient= new XFont("Arial", 9, XFontStyle.Regular);
            XFont fontHead = new XFont("Arial", 11, XFontStyle.Regular);
            XFont fontFooter = new XFont("Arial", 11, XFontStyle.Regular);


            //fact head
            double headHeight = marginTop;
            // Draw the HEADtext
            for (int i = 0; i < head.Length; i++)
            {
                
                gfx.DrawString(head[i], fontHead, XBrushes.Black,
                            new XRect(page.Width-page.Width/3, marginTop, page.Width / 3 - marginLeft, headHeight), XStringFormats.CenterRight);
                headHeight += 30;
            }



            int companyMarginHeight = 80;
            // Draw the COMPANYtext
            for (int i = 0; i < company.Length; i++)
            {
                companyMarginHeight += 8;
                gfx.DrawString(company[i], fontCompany, XBrushes.Black,
                            new XRect(marginLeft, companyMarginHeight, 306, companyMarginHeight), XStringFormats.CenterLeft);
            }

            XFont fontSubTitle = new XFont("Arial", 11, XFontStyle.Bold);
            gfx.DrawString("Datos del cliente", fontSubTitle, XBrushes.Black,
            new XRect(marginLeft, companyMarginHeight+20, 306, companyMarginHeight), XStringFormats.CenterLeft);

            int clientNameHeight = 135;
            // Draw the CLIENTtext
            for (int i = 0; i < client.Length; i++)
            {
                clientNameHeight += 10;
                gfx.DrawString(client[i], fontClient, XBrushes.Black,
                            new XRect(marginLeft, clientNameHeight, 306, clientNameHeight), XStringFormats.CenterLeft);
            }

            //Draw the GridDetails
            string[] gridTitles = { "Referencia", "Producto", "Tasa de impuesto", "Precio unitario", "Cant.", "Total"};
            XFont fontGrid = new XFont("Arial", 10, XFontStyle.Bold);

            XPen penRect = new XPen(XColors.Black, 0.25);
            gfx.DrawRectangle(penRect, marginLeft, page.Height/2 , page.Width-(marginLeft*2), 50);
            gfx.DrawLine(penRect, marginLeft, page.Height / 2, marginLeft, page.Height - page.Height / 6);
            gfx.DrawLine(penRect, marginLeft+50, page.Height / 2, marginLeft + 50, page.Height - page.Height / 6);
            gfx.DrawLine(penRect, marginLeft+50+250, page.Height / 2, marginLeft + 50 + 250, page.Height - page.Height / 6);
            gfx.DrawLine(penRect, marginLeft+50+250+50, page.Height / 2, marginLeft + 50 + 250 + 50, page.Height - page.Height / 6);
            gfx.DrawLine(penRect, marginLeft+50+250+50+70, page.Height / 2, marginLeft + 50 + 250 + 50 + 70, page.Height - page.Height / 6);
            gfx.DrawLine(penRect, marginLeft+50+250+50+70+50, page.Height / 2, marginLeft + 50 + 250 + 50 + 70 + 50, page.Height - page.Height / 6);
            gfx.DrawLine(penRect, page.Width-marginRight, page.Height / 2, page.Width - marginRight, page.Height - page.Height / 6);


            gfx.DrawString("Referencia", fontGrid, XBrushes.Black,
                            new XRect(marginLeft, page.Height / 2, 50, 50), XStringFormats.Center);
            gfx.DrawString("Producto", fontGrid, XBrushes.Black,
                            new XRect(marginLeft+50, page.Height / 2, 250, 50), XStringFormats.Center);
            gfx.DrawString("% TASA", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250, page.Height / 2, 50, 50), XStringFormats.Center);
            gfx.DrawString("IMP.", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250, (page.Height / 2)+25, 50, 25), XStringFormats.Center);
            gfx.DrawString("Precio", fontGrid, XBrushes.Black,
                            new XRect(marginLeft+ 50+250+50, page.Height / 2, 70, 50), XStringFormats.Center);
            gfx.DrawString("unitario", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, (page.Height / 2)+25, 70, 25), XStringFormats.Center);
            gfx.DrawString("Cant.", fontGrid, XBrushes.Black,
                            new XRect(marginLeft + 50 + 250 + 50 + 70, page.Height / 2, 50, 50), XStringFormats.Center);
            gfx.DrawString("Total.", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50 + 70 + 50, page.Height / 2, 50, 50), XStringFormats.Center);


            XFont fontDetails = new XFont("Arial", 9, XFontStyle.Regular);


            string[] productDetail = { "0001","COCA COLA ZERO", "19" , "2680","1","2680"};
            double[] gridmarginLeft = { marginLeft, marginLeft + 50, marginLeft + 50 + 250, marginLeft + 50 + 250 + 50, marginLeft + 50 + 250 + 50 + 70, marginLeft + 50 + 250 + 50 + 70 +50};
            double[] gridSpacing = { 50, 250, 50, 70, 50 ,50};
            string[] totals = { "$2680","Envio gratis", "$2680","3000"};
            int spacingProducts = 50;
            for (int i = 0; i < productDetail.Length; i++)
            {

                gfx.DrawString(productDetail[i], fontDetails, XBrushes.Black,
                new XRect(gridmarginLeft[i], (page.Height / 2)+spacingProducts, gridSpacing[i], 50), XStringFormats.Center);
            }


            //Subtotal
            gfx.DrawString("Total Productos", fontGrid, XBrushes.Black,
                new XRect((page.Width-page.Width/3), page.Height - page.Height / 6, 50, 15), XStringFormats.Center);
            gfx.DrawString("Gastos de envio", fontGrid, XBrushes.Black,
                new XRect((page.Width - page.Width / 3), (page.Height - page.Height / 6) + 15, 50, 15), XStringFormats.Center);
            gfx.DrawString("Total sin imp.", fontGrid, XBrushes.Black,
                new XRect((page.Width - page.Width / 3), (page.Height - page.Height / 6) + 15 + 15, 50, 15), XStringFormats.Center);
            gfx.DrawString("Total", fontGrid, XBrushes.Black,
                new XRect((page.Width - page.Width / 3), (page.Height - page.Height / 6) + 15 + 15 + 15, 50, 15), XStringFormats.Center);

            double totalSpacing = (page.Height - page.Height / 6);
            for (int i = 0; i < totals.Length; i++)
            {
                gfx.DrawString(totals[i], fontDetails, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50 + 70 + 50, totalSpacing, 50, 15), XStringFormats.Center);
                totalSpacing += 15;
            }


            //612pt*792pt letter paper
            //gfx.DrawString(page.Width.ToString() + page.Height.ToString() + " (landscape)", fontHead,
            //    XBrushes.DarkRed, new XRect(0, 0, page.Width, page.Height),
            //    XStringFormats.Center);
            Debug.WriteLine("seconds= " + (DateTime.Now - now).TotalSeconds.ToString());
            //Saving
            document.Save(fileName);
            //start view
            Process.Start(fileName);
        }
    }
}
