using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddPay.xaml
    /// </summary>
    public partial class AddPay : Window
    {
        public AddPay()
        {
            InitializeComponent();
            using (ModelDB db=new ModelDB())
            {
                List<Staff> staffs = db.Staff.ToList();
                foreach(Staff i in staffs)
                {
                    Staffs.Items.Add(i.Surname + " " + i.Name + " " + i.Lastname);
                }
                List<Items_pay> pays = db.Items_pay.ToList();
                foreach(Items_pay i in pays)
                {
                    Item_Pays.Items.Add(i.Item_pay);
                }
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public Staff GetStaff()
        {
            using(ModelDB db=new ModelDB())
            {
                string value = Staffs.Text.Trim();
                string[] vs = value.Split(' ');
                string firstname = vs[0];
                string name = vs[1];
                string lastname= vs[2];
                Staff st = db.Staff.Where(p => p.Surname.Equals(firstname) &&
                                          p.Name.Equals(name) &&
                                          p.Lastname.Equals(lastname)).FirstOrDefault();
                return st;
            }
        }

        public Items_pay getPay()
        {
            using (ModelDB db = new ModelDB())
            {
                string value = Item_Pays.Text;
                Items_pay st = db.Items_pay.Where(p => p.Item_pay.Equals(value)).FirstOrDefault();
                return st;
            }
        }

        public DateTime? DatePay
        {
            get { return Birthday.SelectedDate; }
        }

        public decimal Summa
        {
            get { return decimal.Parse(Sum.Text); }
        }
    }
}
