using Newtonsoft.Json;
using ParserUI.Work;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ParserUI
{
    public partial class ParserMainForm : Form
    {
        Thread[] thread = new Thread[1];
        private List<Product> Products = new List<Product>();
        /// <summary>
        /// Главная форма
        /// </summary>
        public ParserMainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Конпка запуска парсинга
        /// </summary>
        private void StartButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            thread[0] = new Thread(new ThreadStart(delegate { Work(); }));
            thread[0].Start();
        }
        private void Send(string text) 
        {
            this.Invoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText(text);
            });            
        }

        /// <summary>
        /// Парсинг страниц
        /// </summary>
        private void Work()
        {
            Check();
            int min = Page.FirstPage;
            int max = Page.LastPage;
            for (int i = min; i < max+1; i++)
            {
                string response = TwoDroid.GetPage($"https://2droida.ru/catalog/Smartfony-i-telefony/?page= {i}");
                Products.AddRange(TwoDroid.ParsProduct(response));
                foreach (var item in Products)
                {
                    Send(item.ToString() + "\n\n");
                }
            }
        }
        /// <summary>
        /// Кнопка остановки парсинга
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            ImageDownload.GetImage(@"/files/import_files/82/82bba02659f011e98dbc7085c22cb04d_ac5c4dd159f011e98dbc7085c22cb04d.jpg");
            thread[0].Abort();
        }
        /// <summary>
        /// Выбор страниц для парсинга
        /// </summary>
        private void Check()
        {
            if (SelectAllCheckBox.Checked)
            {
                Page.FirstPage = 1;
                Page.LastPage = 45;
            }
            else
            {
                Page.FirstPage = (int)FromPageNumeric.Value;
                Page.LastPage = (int)ToPageNumeric.Value;
            }
        }
        /// <summary>
        /// Сериализация в Json
        /// </summary>
        private void SaveJson()
        {
            string json = JsonConvert.SerializeObject(Products, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            File.WriteAllText("Product.json", json);
        }
        /// <summary>
        /// Десериализация из Json
        /// </summary>
        private void OpenJson()
        {
            if (File.Exists("Product.json"))
            {
                string json = File.ReadAllText("Product.json");
                Products = JsonConvert.DeserializeObject<List<Product>>(json,
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }
        }

        private void ParserMainForm_Load(object sender, EventArgs e)
        {
            OpenJson();
        }

        private void ParserMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveJson();
        }
    }
}
