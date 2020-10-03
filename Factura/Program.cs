﻿using System;
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

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);


            BeginBox(gfx, 1, "DrawLine");
            gfx.DrawLine(XPens.DarkGreen, 0, 0, 250, 0);
            gfx.DrawLine(XPens.Gold, 15, 7, 230, 15);


            // Create a font
            XFont font = new XFont("Arial", 14, XFontStyle.Regular);

            // Draw the text
            //gfx.DrawString("Hello, World!", font, XBrushes.Black,
            //new XRect(0, 0, page.Width, page.Height),                XStringFormats.Center);

            //612pt*792pt letter paper
            gfx.DrawString(page.Width.ToString() + page.Height.ToString() + " (landscape)", font,
                XBrushes.DarkRed, new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);

            //SamplePage1(document);


            Debug.WriteLine("seconds= " + (DateTime.Now - now).TotalSeconds.ToString());


            //Saving
            document.Save(fileName);

            //start view
            Process.Start(fileName);
        }
    }
}
