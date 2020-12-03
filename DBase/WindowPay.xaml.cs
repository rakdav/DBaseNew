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
    /// Логика взаимодействия для WindowPay.xaml
    /// </summary>
    public partial class WindowPay : Window
    {
        public WindowPay()
        {
            InitializeComponent();
            UpdateUI();
        }
        private void UpdateUI()
        {
            PaysTable.ItemsSource = null;
            using (ModelDB db=new ModelDB())
            {
                var result = from pay in db.Pay
                             join staff in db.Staff on pay.T_number equals staff.T_number
                             join pay_item in db.Items_pay on pay.Code_items equals pay_item.Code_Items
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPay addPay = new AddPay();
            if(addPay.ShowDialog()==true)
            {
                Pay pay = new Pay();
                pay.T_number = addPay.GetStaff().T_number;
                pay.Code_items = addPay.getPay().Code_Items;
                pay.Pay_day = addPay.DatePay;
                pay.Sum_pay = addPay.Summa;
                using (ModelDB db=new ModelDB())
                {
                    db.Pay.Add(pay);
                    db.SaveChanges();
                }
                UpdateUI();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
