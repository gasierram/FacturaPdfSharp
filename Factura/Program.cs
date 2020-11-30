using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
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

            double marginLeft = 42.52;
            double marginRight = 42.52;
            double marginTop = 42.52;
            double cuadrito = 14.173228346457;

            LayoutHelper helper = new LayoutHelper(document, marginTop, XUnit.FromCentimeter(29.7 - 2.5));
            string logoPath = @"C:\Users\gusvo\Desktop\logo.jpg";
            logoPath = @"C:\Users\Alejandro Sierra\Desktop\logo.png";

            XImage image = XImage.FromFile(logoPath);
            marginTop = helper.GetLinePosition(XUnit.FromCentimeter(29.7 - 2.5));
            //Debug control
            Debug.WriteLine("marginTop= " + marginTop.ToString());

            helper.Gfx.DrawImage(image, marginLeft, marginTop, 200, 100);
            XPen pen = new XPen(XColors.Plum, 4.7);
            
            string[] company = { "Supermercado el dorado", "Direccion: Cra 90 bis #76-51", "Telefono: 3212261759", "ID: 1016072267", "Correo: gusvo21@hotmail.com" };
            string[] head = { "FACTURA DE VENTA", DateTime.UtcNow.ToShortDateString(), "#FACT-11111" };
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
                helper.Gfx.DrawString(head[i], fontHead, head.Length == i +1 ? XBrushes.Gray : XBrushes.Black,
                            new XRect(helper.Page.Width - helper.Page.Width / 3, marginTop, helper.Page.Width / 3 - marginLeft, headHeight), XStringFormats.CenterRight);
                headHeight += 30;
            }

            int clientNameHeight = 110;
            // Draw the CLIENTtext
            for (int i = 0; i < client.Length; i++)
            {
                clientNameHeight += 8;
                helper.Gfx.DrawString(client[i], fontClient, XBrushes.Black,
                            new XRect(marginLeft, clientNameHeight, 306, clientNameHeight), XStringFormats.CenterLeft);
            }

            int companyMarginHeight = 110;
            // Draw the COMPANYtext
            for (int i = 0; i < company.Length; i++)
            {
                companyMarginHeight += 8;
                helper.Gfx.DrawString(company[i], fontCompany, XBrushes.Black,
                            new XRect(helper.Page.Width - helper.Page.Width / 3, companyMarginHeight, 306, companyMarginHeight), XStringFormats.CenterLeft);
            }

            XFont fontSubTitle = new XFont("Arial", 11, XFontStyle.Bold);
            //Draw the GridDetails
            string[] gridTitles = { "Referencia", "Producto", "Tasa de impuesto", "Precio unitario", "Cant.", "Total" };
            string[] gridMovementsTitles = { "Referencia", "Producto", "Tasa de impuesto", "Precio unitario", "Cant.", "Total" };
            string[] list1 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list2 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list3 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list4 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list5 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list6 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list7 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list8 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list9 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            string[] list10 = new string[3] { "$20.000", "Pago parcial", "26/10/2020" };
            
            List<string[]> movementDetails = new List<string[]>() { list1, list2, list3, list4, list5, list6, list7, list8
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3
            , list9, list10, list1, list10, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3, list3};

            XFont fontGrid = new XFont("Arial", 9, XFontStyle.Bold);
            XPen penRect = new XPen(XColors.DarkGray, 0.25);
            helper.Gfx.DrawRectangle(penRect, XBrushes.WhiteSmoke, marginLeft, helper.Page.Height / 3, helper.Page.Width - (marginLeft * 2), 2 * cuadrito);
            helper.Gfx.DrawString("Valor Movimiento", fontGrid, XBrushes.Black,
                            new XRect(marginLeft, helper.Page.Height / 3, 10 * cuadrito, 2 * cuadrito), XStringFormats.Center);
            helper.Gfx.DrawString("Descripcion", fontGrid, XBrushes.Black,
                            new XRect((helper.Page.Width - (2 * marginLeft)) / 3, helper.Page.Height / 3, 20 * cuadrito, 2 * cuadrito), XStringFormats.Center);
            helper.Gfx.DrawString("Fecha Movimiento", fontGrid, XBrushes.Black,
                            new XRect(helper.Page.Width - (helper.Page.Width - (2 * marginLeft)) / 3, helper.Page.Height / 3, 10 * cuadrito, 2 * cuadrito), XStringFormats.Center);

            XFont fontMovement = new XFont("Arial", 9, XFontStyle.Regular);

            double[] movementMarginLeft = { marginLeft, (helper.Page.Width - (2 * marginLeft)) / 3, helper.Page.Width - (helper.Page.Width - (2 * marginLeft)) / 3};
            double[] movementSpacing = { 10 * cuadrito, 20 * cuadrito, 10 * cuadrito };
            double spacingMovement = 2 * cuadrito + (helper.Page.Height / 3) ;
            foreach(var item in movementDetails)
            {
                for (int j = 0; j < item.Length; j++)
                {
                    if (spacingMovement + cuadrito > helper.Page.Height - marginTop)
                    {
                        XUnit top = helper.GetLinePosition(XUnit.FromCentimeter(29.7 - 2.5));
                        Debug.WriteLine("TOP " + j + " " + top.ToString() + ", spacingMovement: " + spacingMovement);

                        spacingMovement = top;

                    }
                    helper.Gfx.DrawString(item[j], fontMovement, XBrushes.Black,
                        new XRect(movementMarginLeft[j], spacingMovement, movementSpacing[j], cuadrito), XStringFormats.Center);
                    helper.Gfx.DrawLine(penRect, movementMarginLeft[j], spacingMovement, movementMarginLeft[j], spacingMovement + cuadrito);
                    helper.Gfx.DrawLine(penRect, helper.Page.Width - marginRight, spacingMovement, helper.Page.Width - marginRight, spacingMovement+cuadrito);
                    helper.Gfx.DrawLine(penRect, marginLeft, spacingMovement, helper.Page.Width - marginRight, spacingMovement);
                    
                }
                spacingMovement += cuadrito;
                helper.Gfx.DrawLine(penRect, marginLeft, spacingMovement, helper.Page.Width - marginRight, spacingMovement);
            }

            spacingMovement += cuadrito;


            helper.Gfx.DrawRectangle(penRect, XBrushes.WhiteSmoke, marginLeft, spacingMovement, helper.Page.Width - (marginLeft * 2), 2 * cuadrito);
            helper.Gfx.DrawString("Ref", fontGrid, XBrushes.Black,
                            new XRect(marginLeft, spacingMovement, 50, 2 * cuadrito), XStringFormats.Center);
            helper.Gfx.DrawString("Producto", fontGrid, XBrushes.Black,
                            new XRect(marginLeft + 50, spacingMovement, 300, 2 * cuadrito), XStringFormats.Center);
            helper.Gfx.DrawString("Precio", fontGrid, XBrushes.Black,
                            new XRect(marginLeft + 50 + 250 + 50, spacingMovement, 70, cuadrito), XStringFormats.Center);
            helper.Gfx.DrawString("Unitario", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50, spacingMovement + cuadrito, 70, cuadrito), XStringFormats.Center);
            helper.Gfx.DrawString("Cant.", fontGrid, XBrushes.Black,
                            new XRect(marginLeft + 50 + 250 + 50 + 70, spacingMovement, 50, 2 * cuadrito), XStringFormats.Center);
            helper.Gfx.DrawString("SubTotal", fontGrid, XBrushes.Black,
                new XRect(marginLeft + 50 + 250 + 50 + 70 + 50, spacingMovement, 55, 2 * cuadrito), XStringFormats.Center);
            
            XFont fontDetails = new XFont("Arial", 9, XFontStyle.Regular);

            string[] product1 = { "0001", "COCA COLA ZERO", "2680", "1", "50.000" };
            string[] product2 = { "0001", "COCA COLA ZERO", "2680", "1", "2680" };
            string[] product3 = { "0001", "COCA COLA ZERO", "2680", "1", "1.000.000" };
            string[] product4 = { "0001", "COCA COLA ZERO", "2680", "1", "2680" };
            string[] product5 = { "0001", "COCA COLA ZERO", "2680", "1", "26.850" };
            string[] product6 = { "0001", "COCA COLA ZERO", "2680", "1", "2680" };
            string[] product7 = { "0001", "COCA COLA ZERO", "2680", "1", "2680" };
            string[] product8 = { "0001", "COCA COLA ZERO", "2680", "1", "122.680" };
            string[] product9 = { "0001", "COCA COLA ZERO", "2680", "1", "2680" };
            string[] product10 = { "0001", "COCA COLA ZERO", "2680", "1", "2680" };
            string[] product11 = { "0001", "COCA COLA ZERO", "2680", "1", "2680" };
            string[] product12 = { "0001", "COCA COLA ZERO", "2680", "1", "2.680.899" };
            double[] gridmarginLeft = { marginLeft, marginLeft + 50, marginLeft + 50 + 250 + 50, marginLeft + 50 + 250 + 50+70, marginLeft + 50 + 250 + 50 + 70 + 50 };
            double[] gridSpacing = { 50, 300, 70, 50, 55 };
            string[] totals = { "$580.000", "Envio gratis", "$0", "$0", "$680", "$1.200.000" };
            List<string[]>  products = new List<string[]>() { product1, product2, product3, product3, product3, product3, product3, product3, product3, product4, product5,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12,
                product6, product7, product8, product9, product10, product11, product12 };

            double spacingProducts = 2 * cuadrito + spacingMovement;
            foreach (var item in products)
            {
                for (int i =0; i < item.Length; i++)
                {
                    if (spacingProducts + cuadrito > helper.Page.Height - marginTop)
                    {
                        XUnit top = helper.GetLinePosition(XUnit.FromCentimeter(29.7 - 2.5));
                        spacingProducts = top;
                    }
                    else
                    {
                        helper.Gfx.DrawLine(penRect, marginLeft, spacingProducts, helper.Page.Width - marginRight, spacingProducts);
                    }
                    helper.Gfx.DrawString(item[i], fontDetails, XBrushes.Black,
                           new XRect(gridmarginLeft[i], spacingProducts, gridSpacing[i], cuadrito), XStringFormats.Center);
                    helper.Gfx.DrawLine(penRect, gridmarginLeft[i], spacingProducts, gridmarginLeft[i], spacingProducts + cuadrito);
                    helper.Gfx.DrawLine(penRect, helper.Page.Width - marginRight, spacingProducts, helper.Page.Width - marginRight, spacingProducts + cuadrito);
                }
                spacingProducts += cuadrito;
                helper.Gfx.DrawLine(penRect, marginRight, spacingProducts, helper.Page.Width - marginRight, spacingProducts);
            }

            XTextFormatter tf = new XTextFormatter(helper.Gfx);

            string[] footerFields = { "Pago Transacción", "Pago Efectivo", "Detalles" };
            string[] footerDetails= { "$1.000", "$1.000", "Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo Cliente Nuevo " };
            double[] footerFieldsSpacing = { 8 * cuadrito, marginLeft + 12 * cuadrito };
            double totalSpacing = spacingProducts;
            string[] totalFields = { "Subtotal", "Gastos de envío", "Impuesto1", "Impuesto2", "Descuento", "TOTAL" };
            double[] totalFieldsSpacing = { marginLeft + 50 + 250 + 50 };

            if (spacingProducts + 7 * cuadrito > helper.Page.Height - marginTop)
            {
                XUnit top = helper.GetLinePosition(XUnit.FromCentimeter(29.7 - 2.5));
                spacingProducts = totalSpacing = top;
            }
            else
            {
                totalSpacing = spacingProducts += cuadrito;
            }
            helper.Gfx.DrawRectangle(penRect, XBrushes.White, marginLeft, totalSpacing, 24 * cuadrito, 8 * cuadrito);
            helper.Gfx.DrawRectangle(penRect, XBrushes.WhiteSmoke, marginLeft + 50 + 250 + 50, totalSpacing, 9 * cuadrito, 8 * cuadrito);
            helper.Gfx.DrawRectangle(penRect, XBrushes.White, marginLeft + 50 + 250 + 50 + 70 + 50, totalSpacing, 4 * cuadrito, 8 * cuadrito);
            for (int i = 0; i < footerFields.Length; i++)
            {
                helper.Gfx.DrawString(footerFields[i], fontGrid, XBrushes.Black,
                    new XRect(marginLeft + cuadrito, spacingProducts, marginLeft + footerFieldsSpacing[0], cuadrito), XStringFormats.CenterLeft);

                helper.Gfx.DrawLine(penRect, marginLeft, spacingProducts, marginLeft + 24 * cuadrito, spacingProducts);
                
                if(footerDetails.Length - 1 == i)
                {
                    tf.DrawString(footerDetails[i], fontDetails, XBrushes.Black,
                    new XRect(marginLeft+cuadrito, spacingProducts + cuadrito, 22 * cuadrito, 5 * cuadrito), XStringFormats.TopLeft);
                }
                else
                {
                    helper.Gfx.DrawString(footerDetails[i], fontDetails, XBrushes.Black,
                    new XRect(footerFieldsSpacing[1], spacingProducts, marginLeft + footerFieldsSpacing[0], cuadrito), XStringFormats.CenterRight);
                }

                helper.Gfx.DrawLine(penRect, marginLeft, spacingProducts, marginLeft + 24 * cuadrito, spacingProducts);

                spacingProducts += cuadrito; 
            }
            for (int i = 0; i < totalFields.Length; i++)
            {
                helper.Gfx.DrawString(totalFields[i], fontGrid, XBrushes.Black,
                    new XRect(totalFieldsSpacing[0], totals.Length == i + 1 ? totalSpacing + cuadrito : totalSpacing, 9 * cuadrito, cuadrito), XStringFormats.Center);
                helper.Gfx.DrawString(totals[i], totals.Length == i + 1 ? fontGrid : fontDetails, XBrushes.Black,
                    new XRect(marginLeft + 50 + 250 + 50 + 70 + 50, totals.Length == i + 1 ? totalSpacing + cuadrito : totalSpacing, 4 * cuadrito, cuadrito), XStringFormats.Center);
                totalSpacing += cuadrito;
            }
            

            //612pt*792pt letter paper is the same to A4
            Debug.WriteLine("seconds= " + (DateTime.Now - now).TotalSeconds.ToString());
            //Saving
            document.Save(fileName);
            //start view
            Process.Start(fileName);
        }
    }
}
