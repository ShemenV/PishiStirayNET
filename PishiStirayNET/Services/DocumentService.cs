using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using PishiStirayNET.Data;
using PishiStirayNET.Models;
using System;

namespace PishiStirayNET.Services
{
    public class DocumentService
    {
        private readonly TradeContext _tradeContext;

        public DocumentService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }

        public async void CreateDocument(Order order, string path)
        {
            PdfWriter writer = new($"{path}//Товарный чек от {DateOnly.FromDateTime(DateTime.Now).ToString("D")}.pdf");
            PdfDocument pdf = new(writer);
            Document document = new(pdf);

            PdfFont comic = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\Arial.ttf", PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_NOT_EMBEDDED);

            var content = new Paragraph($"ООО «Пиши-Стирай»")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(16);

            document.Add(content);

            content = new Paragraph($"Талон на получение заказа № {string.Format("{0}", order.OrderId)} от {DateOnly.FromDateTime(DateTime.Now).ToString("D")}")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(16);

            document.Add(content);

            content = new Paragraph($"Дата оформления заказа - {order.OrderDeliveryDate.ToString("D")}")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(16);
            document.Add(content);

            content = new Paragraph($"Получатель - {order.Fio}")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(14);
            document.Add(content);

            content = new Paragraph($"Kод получения - {order.CodePoluch}")
             .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
             .SetFont(comic)
             .SetFontSize(22)
             .SetBold();
            document.Add(content);


            content = new Paragraph($"Состав заказа")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(14)
                .SetBold();
            document.Add(content);



            Table table = new(6);
            table.SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.BOTTOM);
            table.SetFont(comic);
            table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);

            table.AddCell("Артикул");
            table.AddCell("Название");
            table.AddCell("Описание");
            table.AddCell("Производитель");
            table.AddCell("Цена");
            table.AddCell("Скидка");




            foreach (var product in order.Products)
            {
                table.AddCell(product.ProductArticleNumber);
                table.AddCell(product.ProductName);
                table.AddCell(product.ProductDescription);
                table.AddCell(product.ProductManufacturerNavigation.Name);
                table.AddCell(product.ProductCost.ToString("F2"));
                table.AddCell(product.ProductDiscountAmount.ToString());
            }

            document.Add(table);


            content = new Paragraph($"Полная стоимость заказа - {order.FullPrice} ₽")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFont(comic)
                .SetFontSize(14);
            document.Add(content);
            content = new Paragraph($"Скидка на заказ - {order.Discount} ₽")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFont(comic)
                .SetFontSize(14);
            document.Add(content);
            content = new Paragraph($"Итоговая стоимость - {order.FullPrice - order.Discount} ₽")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFont(comic)
                .SetFontSize(14);
            document.Add(content);








            document.Close();
        }
    }
}
