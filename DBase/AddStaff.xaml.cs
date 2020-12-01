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
    /// Логика взаимодействия для AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Window
    {
        public AddStaff()
        {
            InitializeComponent();
        }

        public AddStaff(Staff staff)
        {
            InitializeComponent();
            Name.Text = staff.Name;
            LastName.Text = staff.Lastname;
            Surname.Text = staff.Surname;
            Birthday.SelectedDate = staff.Birthday;
            Phone.Text = staff.Phone;
            Post.Text = staff.Post;
            Type_Post.Text = staff.Type_post;
            DateInput.SelectedDate = staff.Date_input;
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        public string SurName
        {
            get { return Surname.Text; }
        }
        public string NameStaff
        {
            get { return Name.Text; }
        }

        public string LastNameStaff
        {
            get { return LastName.Text; }
        }

        public DateTime BirthdayStaff
        {
            get { return Birthday.DisplayDate; }
        }

        public string PhoneStaff
        {
            get { return Phone.Text; }
        }

        public string PostStaff
        {
            get { return Post.Text; }
        }

        public string TypePostStaff
        {
            get { return Type_Post.Text; }
        }
        public DateTime DateInputStaff
        {
            get { return DateInput.DisplayDate; }
        }
    }
}
