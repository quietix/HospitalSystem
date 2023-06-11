using CourseWork1.DocumentsWin4;
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

namespace CourseWork1
{
    /// <summary>
    /// Interaction logic for DocumentsWindow4xaml.xaml
    /// </summary>
    public partial class DocumentsWindow4xaml : Window
    {
        MainWindow mainWindow = null;

        public DocumentsWindow4xaml(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            MainFrame.Content = new SickleavePage1();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }

        private void Button_SickLeave_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SickleavePage1();
        }

        private void Button_Certificate_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CertificatePage2();
        }
    }
}
