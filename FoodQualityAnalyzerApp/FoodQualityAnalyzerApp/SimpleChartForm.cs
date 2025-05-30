
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Model;

namespace FoodQualityAnalyzerApp
{
    public class SimpleChartForm : Form
    {
        private List<FoodProduct> products;
        private Panel chartPanel;

        public SimpleChartForm(List<FoodProduct> selectedProducts)
        {
            products = selectedProducts;

            this.Text = "Диаграмма качества продуктов";
            this.Width = 600;
            this.Height = 400;

            chartPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            chartPanel.Paint += ChartPanel_Paint;

            this.Controls.Add(chartPanel);

            SaveReport();
        }

        private void ChartPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            if (products == null || products.Count == 0)
                return;

            int margin = 40;
            int chartWidth = chartPanel.Width - 2 * margin;
            int chartHeight = chartPanel.Height - 2 * margin;

            int barWidth = chartWidth / products.Count - 10;
            int maxQuality = 100;

            Pen axisPen = new Pen(Color.Black, 2);
            g.DrawLine(axisPen, margin, margin, margin, margin + chartHeight);
            g.DrawLine(axisPen, margin, margin + chartHeight, margin + chartWidth, margin + chartHeight);

            for (int i = 0; i < products.Count; i++)
            {
                double quality = products[i].GetQuality();
                int barHeight = (int)(chartHeight * (quality / maxQuality));

                int x = margin + i * (barWidth + 10) + 5;
                int y = margin + chartHeight - barHeight;

                Rectangle barRect = new Rectangle(x, y, barWidth, barHeight);
                g.FillRectangle(Brushes.SteelBlue, barRect);
                g.DrawRectangle(Pens.Black, barRect);

                string name = products[i].Name;
                SizeF nameSize = g.MeasureString(name, this.Font);
                float nameX = x + (barWidth - nameSize.Width) / 2;
                float nameY = margin + chartHeight + 5;
                g.DrawString(name, this.Font, Brushes.Black, nameX, nameY);

                string qualityStr = quality.ToString("F1");
                SizeF qSize = g.MeasureString(qualityStr, this.Font);
                float qX = x + (barWidth - qSize.Width) / 2;
                float qY = y - qSize.Height - 2;
                g.DrawString(qualityStr, this.Font, Brushes.Black, qX, qY);
            }
        }

        private void SaveReport()
        {
            string reportsFolder = "Reports";
            Directory.CreateDirectory(reportsFolder);

            string datePart = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"Отчет_№1_от_{datePart}.json";
            string fullPath = Path.Combine(reportsFolder, fileName);

            var reportData = new List<object>();
            foreach (var p in products)
            {
                reportData.Add(new
                {
                    p.Name,
                    Quality = p.GetQuality()
                });
            }

            string json = JsonConvert.SerializeObject(reportData, Formatting.Indented);
            File.WriteAllText(fullPath, json);
        }
    }
}
