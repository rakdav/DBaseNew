using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DBase
{
    /// <summary>
    /// Логика взаимодействия для WindowItemPay.xaml
    /// </summary>
    public partial class WindowItemPay : Window
    {
        private bool val = false;
        string global_Pay;
        public WindowItemPay()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            Pays.Items.Clear();
            using (ModelDB db = new ModelDB())
            {
                List<Items_pay> list = db.Items_pay.ToList();
                foreach(Items_pay i in list)
                {
                    Pays.Items.Add(i.Item_pay);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ModelDB db = new ModelDB())
            {
                if (val == false)
                {
                    string pay = Pay.Text;
                    Items_pay items = new Items_pay();
                    items.Item_pay = pay;
                    db.Items_pay.Add(items);
                    db.SaveChanges();           
                }
                else
                {
                    string pay = Pay.Text;
                    Items_pay items = db.Items_pay.Where(p => p.Item_pay.Equals(global_Pay)).FirstOrDefault();
                    items.Item_pay = pay;
                    db.Entry(items).State = EntityState.Modified;
                    db.SaveChanges();
                }
                UpdateUI();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Pays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Pays.SelectedIndex != -1)
            {
                string items_Pay = Pays.SelectedItems[0].ToString();
                global_Pay = items_Pay;            
                Pay.Text = items_Pay;
            }
        }

        private void Pay_SelectionChanged(object sender, RoutedEventArgs e)
        {
            val = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            using (ModelDB db = new ModelDB())
            {
                string pay = Pay.Text;
                Items_pay items = db.Items_pay.Where(p => p.Item_pay.Equals(pay)).FirstOrDefault();
                db.Entry(items).State = EntityState.Deleted;
                db.SaveChanges();
                UpdateUI();
            }
        }
    }
}
