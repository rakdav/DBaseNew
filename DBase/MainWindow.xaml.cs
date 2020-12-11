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
        }

        private void UpdateUI()
        {
            using (ModelDB db = new ModelDB())
            {

                var result = from pay in db.Pay orderby pay.Pay_day descending
                             join staff in db.Staff on pay.T_number equals staff.T_number
                             join pay_item in db.Items_pay on pay.Code_items equals pay_item.Code_Items
                             select new ClassPay
                             {
                                 ID = pay.T_number,
                                 Code = pay.id_pay,
                                 FIO = staff.Surname + " " + staff.Name + " " + staff.Lastname,
                                 PostStaff = staff.Post,
                                 DatePay = pay.Pay_day,
                                 Summa=pay.Sum_pay
                             };

                ZP.ItemsSource = result.GroupBy(p=>new {p.ID,p.Summa}).Select(g=>new {ID=g.Key,Summa=g.Sum(e=>e.Summa)});

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
            var l = (IEnumerable<IGrouping<int, ClassPay>>)ZP.ItemsSource;
            List<ClassPay> list = l.Where(p => ((IGrouping<int,ClassPay>)ZP.SelectedItem).Any(x=>p.Key==x.ID)).SelectMany(x=>x).ToList();
            int id = list[0].ID;
            DateTime? d = list[0].DatePay;
            using (ModelDB db = new ModelDB())
            {
                var result = from pay in db.Pay
                             join staff in db.Staff on pay.T_number equals staff.T_number
                             join pay_item in db.Items_pay on pay.Code_items equals pay_item.Code_Items where pay.T_number==id&&pay.Pay_day==d
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
    }
}
