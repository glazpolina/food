using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace FoodQualityAnalyzerApp
{
    public partial class Main : Form
    {
        /*public Main()
        {
            InitializeComponent();
        }*/

        /*private void Main_Load(object sender, EventArgs e)
        {

        }*/
        private FoodQualityAnalyzer analyzer = new FoodQualityAnalyzer();

        private CheckedListBox checkedListBox;
        private Button btnShowQuality;

        public Main()
        {
            InitializeComponent();
            InitializeUI();
            LoadOrCreateProducts();
        }

        private void InitializeUI()
        {
            this.Text = "Анализ качества продуктов";
            this.Width = 400;
            this.Height = 500;

            checkedListBox = new CheckedListBox
            {
                Dock = DockStyle.Top,
                Height = 350
            };
            checkedListBox.ItemCheck += CheckedListBox_ItemCheck;

            btnShowQuality = new Button
            {
                Text = "Показать качество",
                Dock = DockStyle.Bottom,
                Enabled = false,
                Height = 40
            };
            btnShowQuality.Click += BtnShowQuality_Click;

            this.Controls.Add(checkedListBox);
            this.Controls.Add(btnShowQuality);
        }

        private void LoadOrCreateProducts()
        {
            var products = JsonHelper.LoadProducts();
            if (products == null)
            {
                products = ProductFactory.CreateSampleProducts();
                JsonHelper.SaveProducts(products);
            }

            analyzer.Add(products);

            foreach (var product in analyzer.Products)
            {
                checkedListBox.Items.Add(product.Name);
            }
        }

        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                btnShowQuality.Enabled = checkedListBox.CheckedItems.Count > 0;
            }));
        }

        private void BtnShowQuality_Click(object sender, EventArgs e)
        {
            var selectedProducts = new List<FoodProduct>();
            foreach (int index in checkedListBox.CheckedIndices)
            {
                selectedProducts.Add(analyzer.Products[index]);
            }

            var chartForm = new SimpleChartForm(selectedProducts);
            chartForm.ShowDialog();
        }
    }
}
