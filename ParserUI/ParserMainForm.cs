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
            string response = TwoDroid.GetPage(@"https://2droida.ru/catalog/Smartfony-i-telefony/");
            var parsing = await TwoDroid.ParsProductAsync(response);
            string test = String.Empty;
            foreach (var item in parsing)
            {
                test += item.ToString() + "\r\n";
            }

            richTextBox1.Text = test;
        }
        /// <summary>
        /// Кнопка остановки парсинга
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {

        }
    }
}
