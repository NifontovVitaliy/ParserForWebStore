using ParserUI.Work;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ParserUI
{
    public partial class ParserMainForm : Form
    {
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
        private async void StartButton_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 5; i++)//45 страниц
            {
                string response = TwoDroid.GetPage($"https://2droida.ru/catalog/Smartfony-i-telefony/?page= {i}");
                var parsing = await TwoDroid.ParsProductAsync(response);
                foreach (var item in parsing)
                {
                    richTextBox1.AppendText(item.ToString() + "\n\n");
                }
            }
            
        }
        /// <summary>
        /// Кнопка остановки парсинга
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {

        }
    }
}
