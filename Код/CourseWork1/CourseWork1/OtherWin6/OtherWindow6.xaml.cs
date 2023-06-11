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

namespace CourseWork1.OtherWin6
{
    /// <summary>
    /// Interaction logic for OtherWindow6.xaml
    /// </summary>
    public partial class OtherWindow6 : Window
    {
        MainWindow mainWindow = null;

        public OtherWindow6(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            FillComboBox();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }

        private void FillComboBox()
        {
            CB1_MainCB.Items.Add("1. Дані про певного хворого: адреса, дата останнього відвідування, діагноз");
            CB1_MainCB.Items.Add("2. ПІБ лікаря даного хворого");
            CB1_MainCB.Items.Add("3. Розклад певного лікаря");
            CB1_MainCB.Items.Add("4. Хворі, що на даний момент знаходиться на лікуванні у даного лікаря");
            CB1_MainCB.Items.Add("5. Аналіз скарг, отримання первинного направлення");
            CB1_MainCB.Items.Add("6. Лікар у даному кабінеті");
            CB1_MainCB.Items.Add("7. Кількість відвідувань пацієнта за останній місяць");
            CB1_MainCB.Items.Add("8. Кількість пацієнтів, яку обслужив кожен з лікарів за минулий місяць");
            CB1_MainCB.SelectedIndex = 0;
            MainFrame.Content = new Page1();
        }

        private void CB1_MainCB_Closed(object sender, EventArgs e)
        {
            if (CB1_MainCB.SelectedItem != null)
            {
                if (CB1_MainCB.SelectedItem.ToString().Split(' ')[0] == "1.")
                {
                    MainFrame.Content = new Page1();
                }
                else if (CB1_MainCB.SelectedItem.ToString().Split(' ')[0] == "2.")
                {
                    MainFrame.Content = new Page2();
                }
                else if (CB1_MainCB.SelectedItem.ToString().Split(' ')[0] == "3.")
                {
                    MainFrame.Content = new Page3();
                }
                else if (CB1_MainCB.SelectedItem.ToString().Split(' ')[0] == "4.")
                {
                    MainFrame.Content = new Page4();
                }
                else if (CB1_MainCB.SelectedItem.ToString().Split(' ')[0] == "5.")
                {
                    MainFrame.Content = new Page5();
                }
                else if (CB1_MainCB.SelectedItem.ToString().Split(' ')[0] == "6.")
                {
                    MainFrame.Content = new Page6();
                }
                else if (CB1_MainCB.SelectedItem.ToString().Split(' ')[0] == "7.")
                {
                    MainFrame.Content = new Page7();
                }
                else if (CB1_MainCB.SelectedItem.ToString().Split(' ')[0] == "8.")
                {
                    MainFrame.Content = new Page8();
                }
            }
        }
    }
}
