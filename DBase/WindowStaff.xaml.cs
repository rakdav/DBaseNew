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

        private void Staffs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Staff st= Staffs.SelectedItems[0] as Staff;
            string name = st.Name;
            string surname = st.Surname;
            string lastname = st.Lastname;
            using (ModelDB db=new ModelDB())
            {
                Staff staff = db.Staff.Where(p=>p.Name.Equals(name)&&p.Lastname.Equals(lastname)&&p.Surname.Equals(surname)).FirstOrDefault();
                AddStaff edit = new AddStaff(staff);
                if(edit.ShowDialog()==true)
                {
                    staff.Surname = edit.SurName;
                    staff.Name = edit.NameStaff;
                    staff.Lastname = edit.LastNameStaff;
                    staff.Birthday = edit.BirthdayStaff;
                    staff.Phone = edit.PhoneStaff;
                    staff.Post = edit.PostStaff;
                    staff.Type_post = edit.TypePostStaff;
                    staff.Date_input = edit.DateInputStaff;
                    db.Entry(staff).State = EntityState.Modified;
                    db.SaveChanges();
                    UpdateDB();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Staff st = Staffs.SelectedItems[0] as Staff;
            using (ModelDB db=new ModelDB()) {
                db.Entry(st).State = EntityState.Deleted;
                db.SaveChanges();
                UpdateDB();
            }
        }
    }
}
