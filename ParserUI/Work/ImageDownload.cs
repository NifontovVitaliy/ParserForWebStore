using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParserUI.Work
{
    public class ImageDownload
    {
        public static string GetImage(string url)
        {
            string pathToSaveImage = @"F:\Work\WebStoreApplication\WebStoreApplication\wwwroot\images\";
            var imgName = url.Substring('/');
            string path = pathToSaveImage + imgName;
            string imageUrl = @"https://2droida.ru" + url;
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Referer", imageUrl);
            webClient.DownloadFile(imageUrl, path);
            path = @"images\" + imgName;
            return path;
        }
    }
}
