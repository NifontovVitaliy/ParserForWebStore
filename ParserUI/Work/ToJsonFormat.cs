using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserUI.Work
{
    public class ToJsonFormat
    {
        public static async Task FromatDataAsync(Product product)
        {
            using(FileStream stream = new FileStream($"F:\\Work\\product.json", FileMode.Append))
            {
                await JsonSerializer.SerializeAsync<Product>(stream, product);
            }
        }
    }
}
