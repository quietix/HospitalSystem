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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using CourseWork1.OtherWin6;

namespace CourseWork1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RegistrationWindow1 registrationWindow1 = null;
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Registration_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            if (registrationWindow1 == null)
            {
                registrationWindow1 = new RegistrationWindow1(this);
                registrationWindow1.Show();
            }
            else
            {
                registrationWindow1.Show();
            }
        }

        private void Button_AddVisit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            AddVisitWindow2 addVisitWindow2 = new AddVisitWindow2(this);
            addVisitWindow2.Show();
        }

        private void Button_GoToAdministratorWin_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DeleteDoctorWindow3 deleteDoctorWindow3 = new DeleteDoctorWindow3(this);
            deleteDoctorWindow3.Show();
        }

        private void Button_DocumentsIssuence_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DocumentsWindow4xaml documentsWindow4xaml = new DocumentsWindow4xaml(this);
            documentsWindow4xaml.Show();
        }

        private void Button_WorkSchedule_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            WorkScheduleWindow5 workScheduleWindow5 = new WorkScheduleWindow5(this);
            workScheduleWindow5.Show();
        }

        private void Button_OtherWin_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            OtherWindow6 otherWindow6 = new OtherWindow6(this);
            otherWindow6.Show();
        }
    }
}
