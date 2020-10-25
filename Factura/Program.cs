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
            double cuadrito = 14.173228346457;

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            string logoPath = @"C:\Users\gusvo\Desktop\logo.jpg";
            logoPath = @"C:\Users\Alejandro Sierra\Desktop\prueba_logook.png";


            XImage image = XImage.FromFile(logoPath);
            gfx.DrawImage(image, marginLeft, marginTop, 200, 100);

            XPen pen = new XPen(XColors.Plum, 4.7);


            string[] company = { "Supermercado el dorado", "Direccion: Cra 90 bis #76-51", "Telefono: 3212261759", "ID: 1016072267", "Correo: gusvo21@hotmail.com" };
            string[] head = { "FACTURA DE VENTA", DateTime.UtcNow.ToShortDateString(), "#fACT11111" };
            string[] client = { "Cliente: Gustavo Alejandro Sierra", "Correo: gusvo21@hotmail.com", "Telefono: 3212261759", "ID: 1016072267", "Direccion: Cra 90 bis #76-51" };



            // Create a font
            XFont fontCompany = new XFont("Arial", 8, XFontStyle.Regular);
            XFont fontClient = new XFont("Arial", 8, XFontStyle.Regular);
            XFont fontHead = new XFont("Arial", 11, XFontStyle.Regular);
            XFont fontFooter = new XFont("Arial", 11, XFontStyle.Regular);


            //fact head
            double headHeight = marginTop;
            // Draw the HEADtext
            for (int i = 0; i < head.Length; i++)
            {

                gfx.DrawString(head[i], fontHead, XBrushes.Black,
                            new XRect(page.Width - page.Width / 3, marginTop, page.Width / 3 - marginLeft, headHeight), XStringFormats.CenterRight);
                headHeight += 30;
            }


            int clientNameHeight = 110;
            // Draw the CLIENTtext
            for (int i = 0; i < client.Length; i++)
            {
                clientNameHeight += 8;
                gfx.DrawString(client[i], fontClient, XBrushes.Black,
                            new XRect(marginLeft, clientNameHeight, 306, clientNameHeight), XStringFormats.CenterLeft);
            }

            int companyMarginHeight = 110;
            // Draw the COMPANYtext
            for (int i = 0; i < company.Length; i++)
            {
                companyMarginHeight += 8;
                gfx.DrawString(company[i], fontCompany, XBrushes.Black,
                            new XRect(page.Width - page.Width / 3, companyMarginHeight, 306, companyMarginHeight), XStringFormats.CenterLeft);
            }

            XFont fontSubTitle = new XFont("Arial", 11, XFontStyle.Bold);
            //gfx.DrawString("Adquiriente", fontSubTitle, XBrushes.Black,
            //new XRect(page.Width/2, 80, 306, companyMarginHeight), XStringFormats.CenterLeft);


            //Draw the GridDetails
            string[] gridTitles = { "Referencia", "Producto", "Tasa de impuesto", "Precio unitario", "Cant.", "Total" };
            string[] gridMovementsTitles = { "Referencia", "Producto", "Tasa de impuesto", "Precio unitario", "Cant.", "Total" };

            XFont fontGrid = new XFont("Arial", 9, XFontStyle.Bold);

            XPen penRect = new XPen(XColors.DarkGray, 0.25);
            gfx.DrawRectangle(penRect, XBrushes.WhiteSmoke, marginLeft, page.Height / 3, page.Width - (marginLeft * 2), 2 * cuadrito);
            gfx.DrawLine(penRect, marginLeft, page.Height / 3, marginLeft, (page.Height / 3) + 7 * cuadrito);
            gfx.DrawLine(penRect, page.Width - (page.Width - (2 * marginLeft)) / 3, page.Height / 3, page.Width - (page.Width - (2 * marginLeft)) / 3, (page.Height / 3) + 7 * cuadrito);
            gfx.DrawLine(penRect, (page.Width - (2 * marginLeft)) / 3, page.Height / 3, (page.Width - (2 * marginLeft)) / 3, (page.Height / 3) + 7 * cuadrito);
            gfx.DrawLine(penRect, page.Width - marginRight, page.Height / 3, page.Width - marginRight, (page.Height / 3) + 7 * cuadrito);
            gfx.DrawLine(penRect, marginLeft, (page.Height / 3) + 7 * cuadrito, page.Width - marginRight, (page.Height / 3) + 7 * cuadrito);

            gfx.DrawString("Valor Movimiento", fontGrid, XBrushes.Black,
                            new XRect(marginLeft, page.Height / 3, 10 * cuadrito, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("Descripcion", fontGrid, XBrushes.Black,
                            new XRect((page.Width - (2 * marginLeft)) / 3, page.Height / 3, 20 * cuadrito, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("Fecha Movimiento", fontGrid, XBrushes.Black,
                            new XRect(page.Width - (page.Width - (2 * marginLeft)) / 3, page.Height / 3, 10 * cuadrito, 2 * cuadrito), XStringFormats.Center);


            gfx.DrawRectangle(penRect, XBrushes.WhiteSmoke, marginLeft, page.Height / 2, page.Width - (marginLeft * 2), 2 * cuadrito);
            gfx.DrawLine(penRect, marginLeft, page.Height / 2, marginLeft, page.Height / 2 + 17 * cuadrito);
            gfx.DrawLine(penRect, marginLeft + 50, page.Height / 2, marginLeft + 50, page.Height / 2 + 17 * cuadrito);
            gfx.DrawLine(penRect, marginLeft + 50 + 250, page.Height / 2, marginLeft + 50 + 250, page.Height / 2 + 17 * cuadrito);
            gfx.DrawLine(penRect, marginLeft + 50 + 250 + 50, page.Height / 2, marginLeft + 50 + 250 + 50, page.Height / 2 + 17 * cuadrito);
            gfx.DrawLine(penRect, marginLeft + 50 + 250 + 50 + 70, page.Height / 2, marginLeft + 50 + 250 + 50 + 70, page.Height / 2 + 17 * cuadrito);
            gfx.DrawLine(penRect, marginLeft + 50 + 250 + 50 + 70 + 50, page.Height / 2, marginLeft + 50 + 250 + 50 + 70 + 50, page.Height / 2 + 17 * cuadrito);
            gfx.DrawLine(penRect, page.Width - marginRight, page.Height / 2, page.Width - marginRight, page.Height / 2 + 17 * cuadrito);
            gfx.DrawLine(penRect, marginLeft, page.Height / 2 + 17 * cuadrito, page.Width - marginRight, page.Height / 2 + 17 * cuadrito);


            gfx.DrawString("Ref", fontGrid, XBrushes.Black,
                            new XRect(marginLeft, page.Height / 2, 50, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("Producto", fontGrid, XBrushes.Black,
                            new XRect(marginLeft + 50, page.Height / 2, 250, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("Precio", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250, page.Height / 2, 50, cuadrito), XStringFormats.BottomCenter);
            gfx.DrawString("Unitario", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250, (page.Height / 2) + cuadrito, 50, cuadrito), XStringFormats.BottomCenter);
            gfx.DrawString("Cant", fontGrid, XBrushes.Black,
                            new XRect(marginLeft + 50 + 250 + 50, page.Height / 2, 70, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, (page.Height / 2) + cuadrito, 70, cuadrito), XStringFormats.Center);
            gfx.DrawString("SubTotal.", fontGrid, XBrushes.Black,
                            new XRect(marginLeft + 50 + 250 + 50 + 70, page.Height / 2, 50, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50 + 70 + 50, page.Height / 2, 50, 2 * cuadrito), XStringFormats.Center);


            XFont fontDetails = new XFont("Arial", 9, XFontStyle.Regular);


            string[] productDetail = { "0001", "COCA COLA ZERO", "2680", "1", "2680" };
            double[] gridmarginLeft = { marginLeft, marginLeft + 50, marginLeft + 50 + 250, marginLeft + 50 + 250 + 50, marginLeft + 50 + 250 + 50 + 70, marginLeft + 50 + 250 + 50 + 70 + 50 };
            double[] gridSpacing = { 50, 250, 50, 70, 50, 50 };
            string[] totals = { "$580.000", "Envio gratis", "$0", "$0", "$680", "$1.200.000" };
            double spacingProducts = 2 * cuadrito;
            for (int i = 0; i < productDetail.Length; i++)
            {

                gfx.DrawString(productDetail[i], fontDetails, XBrushes.Black,
                new XRect(gridmarginLeft[i], (page.Height / 2) + spacingProducts, gridSpacing[i], cuadrito), XStringFormats.Center);
            }

            gfx.DrawRectangle(penRect, XBrushes.White, marginLeft , page.Height / 2 + 18 * cuadrito, 24 * cuadrito, 8 * cuadrito);
            gfx.DrawLine(penRect, marginLeft, page.Height / 2 + 19 * cuadrito, marginLeft+ 24 * cuadrito, page.Height / 2 + 19 * cuadrito);
            gfx.DrawLine(penRect, marginLeft, page.Height / 2 + 20 * cuadrito, marginLeft+ 24 * cuadrito, page.Height / 2 + 20 * cuadrito);

            gfx.DrawString("Pago transaccion:", fontGrid, XBrushes.Black,
                new XRect(marginLeft+cuadrito, page.Height / 2 + 18 * cuadrito, 24 * cuadrito, cuadrito), XStringFormats.CenterLeft);
            gfx.DrawString("Pago Efectivo:", fontGrid, XBrushes.Black,
                new XRect(marginLeft + cuadrito, page.Height / 2 + 19 * cuadrito, 24 * cuadrito, cuadrito), XStringFormats.CenterLeft);
            gfx.DrawString("Detalles:", fontGrid, XBrushes.Black,
                new XRect(marginLeft + cuadrito, page.Height / 2 + 20 * cuadrito, 24 * cuadrito, 2 * cuadrito), XStringFormats.CenterLeft);

            gfx.DrawString("$1000", fontDetails, XBrushes.Black,
                new XRect(marginLeft + 12*cuadrito, page.Height / 2 + 18 * cuadrito, 11 * cuadrito, cuadrito), XStringFormats.CenterRight);
            gfx.DrawString("$2000", fontDetails, XBrushes.Black,
                new XRect(marginLeft + 12*cuadrito, page.Height / 2 + 19 * cuadrito, 11 * cuadrito, cuadrito), XStringFormats.CenterRight);
            gfx.DrawString("Cliente nuevo", fontDetails, XBrushes.Black,
                new XRect(marginLeft +12*cuadrito, page.Height / 2 + 20 * cuadrito, 11 * cuadrito, 2 * cuadrito), XStringFormats.CenterRight);




            gfx.DrawRectangle(penRect, XBrushes.WhiteSmoke, marginLeft + 50 + 250 + 50, page.Height / 2 + 18 * cuadrito, 9 * cuadrito, 8 * cuadrito);
            gfx.DrawRectangle(penRect, XBrushes.White, marginLeft + 50 + 250 + 50 + 70 + 50, page.Height / 2 + 18 * cuadrito, 4 * cuadrito, 8 * cuadrito);

            //Subtotal
            gfx.DrawString("Subtotal", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, page.Height / 2 + 18 * cuadrito, 9*cuadrito, 2*cuadrito), XStringFormats.Center);
            gfx.DrawString("Gastos de envio", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, page.Height / 2 + 19 * cuadrito, 9 * cuadrito, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("Impuesto 1.", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, page.Height / 2 + 20 * cuadrito, 9 * cuadrito, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("Impuesto 2.", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, page.Height / 2 + 21 * cuadrito, 9 * cuadrito, 2 * cuadrito), XStringFormats.Center);
            gfx.DrawString("Descuento", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, page.Height / 2 + 22 * cuadrito, 9 * cuadrito, 2 * cuadrito), XStringFormats.Center);

            gfx.DrawString("TOTAL", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, page.Height / 2 + 24 * cuadrito, 9 * cuadrito, 2 * cuadrito), XStringFormats.Center);

            double totalSpacing = page.Height / 2 + 18 * cuadrito;
            for (int i = 0; i < totals.Length; i++)
            {
                if (totals.Length == i + 1)
                {

                    gfx.DrawString(totals[i], fontGrid, XBrushes.Black,
                    new XRect(marginLeft + 50 + 250 + 50 + 70 + 50, page.Height / 2 + 24 * cuadrito, 4 * cuadrito, 2 * cuadrito), XStringFormats.Center);
                }
                else
                {
                    gfx.DrawString(totals[i], fontDetails, XBrushes.Black,
                    new XRect(marginLeft + 50 + 250 + 50 + 70 + 50, totalSpacing, 4 * cuadrito, 2*cuadrito), XStringFormats.Center);

                }
                totalSpacing += cuadrito;
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
