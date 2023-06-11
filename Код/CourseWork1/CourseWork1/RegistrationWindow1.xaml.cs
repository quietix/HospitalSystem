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
using System.Data.SqlClient;
using System.Data;

namespace CourseWork1
{
    /// <summary>
    /// Interaction logic for RegestrationWindow1.xaml
    /// </summary>
    public partial class RegistrationWindow1 : Window
    {
        static string connectionString = MainWindow.connectionString;
        static SqlConnection connection = new SqlConnection(connectionString);
        static SqlCommand command = new SqlCommand();

        MainWindow mainWindow = null;
        static string surname = "defaultSurname";
        static string name = "defaultName";
        static string secname = "";
        static string address = "defaultAddress";
        static string sex = "Чоловік";
        static int age = 0;
        static string insuranceNum = "XXXXXXXX";
        static string dateOfCreation = "01.01.2000";

        public RegistrationWindow1(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            ShowCardsData();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }
        public void MakeQuery(string query)
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void Button_Insert_Click(object sender, RoutedEventArgs e)
        {
            string query = $"insert into dbo.Cards values((select count(id) from dbo.Cards) + 1, '{surname}', '{name}', '{secname}', " +
                $"'{address}', '{sex}', {age}, '{insuranceNum}', '{dateOfCreation}')";
            bool succeed = false;
            try
            {
                MakeQuery(query);
                succeed = true;
            }
            catch (Exception err)
            {
                succeed = false;
                connection.Close();
                MessageBox.Show(err.Message);
            }
            if (succeed)
            {
                TB_Name_1.Text = "";
                TB_Surname_2.Text = "";
                TB_Secname_3.Text = "";
                TB_Address_4.Text = "";
                TB_Sex_5.Text = "";
                TB_Age_6.Text = "";
                TB_Police_7.Text = "";
                TB_Date_8.Text = "";
            }
            ShowCardsData();
        }

        private void TB_Name_1_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Name_1.Text.Length < 20 && TB_Name_1.Text.Length > 0)
            {
                try
                {
                    name = TB_Name_1.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_Name_1.Text.Length > 20)
            {
                surname = "defaultName";
                TB_Name_1.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void TB_Surname_2_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Surname_2.Text.Length < 20 && TB_Surname_2.Text.Length > 0)
            {
                try
                {
                    surname = TB_Surname_2.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_Surname_2.Text.Length > 20)
            {
                surname = "defaultSurname";
                TB_Surname_2.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void TB_Secname_3_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Secname_3.Text.Length < 30 && TB_Secname_3.Text.Length > 0)
            {
                try
                {
                    secname = TB_Secname_3.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_Secname_3.Text.Length > 30)
            {
                secname = "defaultSecname";
                TB_Secname_3.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void TB_Address_4_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Address_4.Text.Length < 100 && TB_Address_4.Text.Length > 0)
            {
                try
                {
                    address = TB_Address_4.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_Address_4.Text.Length > 100)
            {
                address = "defaultAddress";
                TB_Address_4.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void TB_Sex_5_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Sex_5.Text.Length < 10 && TB_Sex_5.Text.Length > 0)
            {
                try
                {
                    sex = TB_Sex_5.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_Sex_5.Text.Length > 10)
            {
                sex = "defaultSex";
                TB_Sex_5.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void TB_Age_6_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Age_6.Text.Length > 0)
            {
                try
                {
                    age = int.Parse(TB_Age_6.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
        }

        private void TB_Police_7_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Police_7.Text.Length < 10 && TB_Police_7.Text.Length > 0)
            {
                try
                {
                    insuranceNum = TB_Police_7.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_Police_7.Text.Length > 10)
            {
                insuranceNum = "defaultInsurance";
                TB_Police_7.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void TB_Date_8_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TB_Date_8.Text.Length > 8 && TB_Date_8.Text.Length < 12)
            {
                try
                {
                    if (DateTime.Parse(TB_Date_8.Text) > DateTime.Now)
                    {
                        TB_Date_8.Text = "";
                        MessageBox.Show("Invalid date. Try again!");
                    }
                    else
                    {
                        dateOfCreation = AddVisitWindow2.SwapDate(TB_Date_8.Text);
                    }
                }
                catch
                {
                    TB_Date_8.Text = "";
                    MessageBox.Show("Invalid date. Try again!");
                }
            }
        }

        private void ShowCardsData()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "select id as [№], surname as [Прізвище], name as [Ім'я], " +
                "secname as [По батькові], address as [Адреса], sex as [Стать], " +
                "age as [Вік], insurance_policy_number as [Страховий поліс], " +
                "date_of_card_creation as [Дата створення картки] from dbo.Cards";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);
            DataGrid1.ItemsSource = table.DefaultView;
            con.Close();
        }
    }
}
