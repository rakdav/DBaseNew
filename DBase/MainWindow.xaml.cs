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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateUI();
            using (ModelDB db = new ModelDB())
            {
                List<Staff> list = db.Staff.ToList();
                foreach(Staff i in list)
                {
                    Person.Items.Add(i.Surname+" "+i.Name+" "+i.Lastname);
                }
            }
        }

        private void UpdateUI()
        {
            using (ModelDB db = new ModelDB())
            { 
                var res = from pay in db.Pay
                          group pay by new { pay.T_number, pay.Pay_day } into g
                          orderby g.Key.Pay_day descending
                          select new ClassPay
                          {
                              ID = g.Key.T_number,
                              DatePay = g.Key.Pay_day,
                              Summa = g.Sum(e => e.Sum_pay),
                              FIO = db.Staff.Where(p => p.T_number == g.Key.T_number).FirstOrDefault().Surname+" "+
                                    db.Staff.Where(p => p.T_number == g.Key.T_number).FirstOrDefault().Name+" "+
                                    db.Staff.Where(p => p.T_number == g.Key.T_number).FirstOrDefault().Lastname,
                              PostStaff=db.Staff.Where(p => p.T_number == g.Key.T_number).FirstOrDefault().Post
                          };
                ZP.ItemsSource = res.ToList();
                
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e) 
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            WindowStaff window = new WindowStaff();
            window.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            WindowItemPay window = new WindowItemPay();
            window.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            WindowPay window = new WindowPay();
            window.Show();
            UpdateUI();
        }

        private void ZP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassPay l = (ClassPay)ZP.SelectedItem;
            int id = l.ID;
            DateTime? d =l.DatePay;
            using (ModelDB db = new ModelDB())
            {
                var result = from pay in db.Pay
                             join staff in db.Staff on pay.T_number equals staff.T_number
                             join pay_item in db.Items_pay on pay.Code_items equals pay_item.Code_Items
                             where pay.T_number == id && pay.Pay_day == d
                             select new
                             {
                                 Code = pay.id_pay,
                                 FIO = staff.Surname + " " + staff.Name + " " + staff.Lastname,
                                 PayItem = pay_item.Item_pay,
                                 DatePay = pay.Pay_day,
                                 Summa = pay.Sum_pay
                             };
                PaysTable.ItemsSource = result.ToList();
            }

        }

        private void Person_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZP.ItemsSource = null;
            using (ModelDB db = new ModelDB())
            {
                string fio = Person.Items[Person.SelectedIndex].ToString();
                string[] par = fio.Split(' ');
                string sn = par[0];
                string name = par[1];
                string last = par[2];
                int id = db.Staff.Where(p => p.Name.Equals(name) && p.Surname.Equals(sn) && p.Lastname.Equals(last)).FirstOrDefault().T_number;
                var res = from pay in db.Pay
                          group pay by new { pay.T_number, pay.Pay_day } into g
                          where g.Key.T_number==id
                          orderby g.Key.Pay_day descending
                          select new ClassPay
                          {
                              ID = g.Key.T_number,
                              DatePay = g.Key.Pay_day,
                              Summa = g.Sum(l => l.Sum_pay),
                              FIO = db.Staff.Where(p => p.T_number == g.Key.T_number).FirstOrDefault().Surname + " " +
                                    db.Staff.Where(p => p.T_number == g.Key.T_number).FirstOrDefault().Name + " " +
                                    db.Staff.Where(p => p.T_number == g.Key.T_number).FirstOrDefault().Lastname,
                              PostStaff = db.Staff.Where(p => p.T_number == g.Key.T_number).FirstOrDefault().Post
                          };
                ZP.ItemsSource = res.ToList();

            }
        }
    }
}
