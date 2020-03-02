using AngleSharp.Html.Parser;
using Leaf.xNet;
using System;
using System.Collections.Generic;

namespace ParserUI.Work
{
    /// <summary>
    /// класс для работы с 2Droid
    /// </summary>
    class TwoDroid
    {
        static Random random = new Random();
        /// <summary>
        /// Получить исходный код страницы
        /// </summary>
        /// <param name="link">ссылка на страницу для парсинга</param>
        /// <returns></returns>
        public static string GetPage(string link)
        {
            HttpRequest request = new HttpRequest();
            try
            {
                string response = request.Get(link).ToString();
                return response;
            }
            catch{}
            return string.Empty;
        }
        /// <summary>
        /// парсер 
        /// </summary>
        /// <returns></returns>
        public static List<Product> ParsProduct(string response)
        {
            HtmlParser htmlParser = new HtmlParser();
            var doc = htmlParser.ParseDocument(response);
            List<Product> products = new List<Product>();

            foreach (var item in doc.QuerySelectorAll("div.wrapper>div.main-content>div.main>div.row.no-margin>div.col-md-9>div.items.row.no-maring>div.col-md-4.col-sm-4"))
            {
                string name = item.QuerySelector("div.white-block.description>>div.info>span.type>a").TextContent;
                string description = item.QuerySelector("div.white-block.description>div.info>div.title>a").TextContent;
                string price = string.Empty;
                    //item.QuerySelector("div.white-block.description>span.price").TextContent;
                try
                {
                    price = item.QuerySelector("div.white-block.description>span.price>span").GetAttribute("content");
                }
                catch
                {
                    price = (random.Next(10000,30000)).ToString();
                }
                string imgUrl = item.QuerySelector("div.image.full-gallery>a>img").GetAttribute("src");
                string backImageUrl = ImageDownload.GetImage(imgUrl);                
                string company = name.Split(' ').GetValue(0).ToString();
                Product product = new Product
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    Company = company,
                    ImageUrl = backImageUrl
                };
                products.Add(product);

            }
            using(var db = new ParserDbContext())
            {
                db.Products.AddRange(products);
                db.SaveChanges();
            }
            return products;
        }
    }
}
