using AngleSharp.Html.Parser;
using Leaf.xNet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParserUI.Work
{
    /// <summary>
    /// класс для работы с 2Droid
    /// </summary>
    class TwoDroid
    {
        /// <summary>
        /// Получить исходный код страницы
        /// </summary>
        /// <param name="link">ссылка на страницу для парсинга</param>
        /// <returns></returns>
        public static string GetPage(string link)
        {
            HttpRequest request = new HttpRequest();
            string response = request.Get(link).ToString();
            return response;
        }
        /// <summary>
        /// парсер 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Product>> ParsProductAsync(string response)
        {
            HtmlParser htmlParser = new HtmlParser();
            var doc = await htmlParser.ParseDocumentAsync(response);
            List<Product> products = new List<Product>();
            string test = doc.QuerySelector("div.wrapper>div.main-content>div.main>div.row.no-margin>div.col-md-9>div.items.row.no-maring>div.col-md-4.col-sm-4>div.product>div.item-wrapper>div.item").InnerHtml;

            foreach (var item in doc.QuerySelectorAll("div.wrapper>div.main-content>div.main>div.row.no-margin>div.col-md-9>div.items.row.no-maring>div.col-md-4.col-sm-4>div.product>div.item-wrapper>div.item"))
            {
                products.Add(new Product
                {
                    Name = item.QuerySelector("div.white-block.description>div.info>span.type>a").TextContent,
                    Description = item.QuerySelector("div.white-block.description>div.info>div.title>a").TextContent,
                    Price = item.QuerySelector("div.white-block.description>span.price").TextContent,
                    ImageUrl =item.QuerySelector("div.image.full-gallery>a>img").GetAttribute("src")
                });
            }
            return products;
        }
    }
}
