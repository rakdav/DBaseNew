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
    /// Логика взаимодействия для WindowStaff.xaml
    /// </summary>
    public partial class WindowStaff : Window
    {
        public WindowStaff()
        {
            InitializeComponent();
            UpdateDB();
        }

        private void UpdateDB()
        {
            Staffs.ItemsSource = null;
            using (ModelDB db = new ModelDB())
            {
                db.Staff.Load();
                Staffs.ItemsSource = db.Staff.Local.ToBindingList();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStaff addStaff = new AddStaff();
            if(addStaff.ShowDialog()==true)
            {
                using (ModelDB db=new ModelDB())
                {
                    Staff staff = new Staff();
                    staff.Surname = addStaff.SurName;
                    staff.Name = addStaff.NameStaff;
                    staff.Lastname = addStaff.LastNameStaff;
                    staff.Birthday = addStaff.BirthdayStaff;
                    staff.Phone = addStaff.PhoneStaff;
                    staff.Post = addStaff.PostStaff;
                    staff.Type_post = addStaff.TypePostStaff;
                    staff.Date_input = addStaff.DateInputStaff;
                    db.Staff.Add(staff);
                    db.SaveChanges();
                    UpdateDB();
                }
            }
        }
    }
}
