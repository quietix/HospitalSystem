using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for WorkScheduleWindow5.xaml
    /// </summary>
    public partial class WorkScheduleWindow5 : Window
    {
        MainWindow mainWindow = null;
        static string connectionString = MainWindow.connectionString;
        static SqlConnection connection = new SqlConnection(connectionString);

        public WorkScheduleWindow5(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            ShowDoctorsTable(true);
            UpdateCombobox();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }

        void ShowData(string query)
        {
            DataTable table = new DataTable();
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(table);
            DataGrid1.ItemsSource = table.DefaultView;
            connection.Close();
        }

        void ShowDoctorsTable(bool needFullInfo)
        {
            if (needFullInfo)
            {
                string query = "select * from dbo.Doctors";
                ShowData(query);
            }
            else
            {
                if (CB1_Doctors.SelectedItem != null)
                {
                    string fullDocData = CB1_Doctors.SelectedItem.ToString();
                    if (fullDocData == "Показати всіх лікарів")
                    {
                        ShowDoctorsTable(true);
                    }
                    else
                    {
                        string[] arrfullDocData = fullDocData.Split(' ');
                        string query = $"select * from dbo.Doctors where id = {arrfullDocData[0]}";
                        ShowData(query);
                    }
                }
            }
        }

        private void CB1_Doctor_DropDownClosed(object sender, EventArgs e)
        {
            ShowDoctorsTable(false);
        }

        int GetNumOfDoctors()
        {
            int numOfDocs = 0;
            connection.Open();
            SqlCommand cmd = new SqlCommand("select count(id) from dbo.Doctors", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                numOfDocs = int.Parse(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            return numOfDocs;
        }

        string[] GetDoctorsData()
        {
            int numOfDirections = GetNumOfDoctors() + 1;
            string[] line = new string[numOfDirections];
            connection = new SqlConnection(connectionString);
            for (int i = 0; i < numOfDirections; i++)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select surname, name from dbo.Doctors where id =@ID", connection);
                cmd.Parameters.AddWithValue("@ID", i);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    line[i] += i + " ";
                    line[i] += da.GetValue(0).ToString().Trim() + " ";
                    line[i] += da.GetValue(1).ToString().Trim();
                }
                da.Close();
                connection.Close();
            }
            return line;
        }

        void UpdateCombobox()
        {
            string[] dataDoctors = GetDoctorsData();
            CB1_Doctors.Items.Add("Показати всіх лікарів");
            for (int i = 0; i < dataDoctors.Length; i++)
            {
                if (dataDoctors[i] != null)
                {
                    CB1_Doctors.Items.Add(dataDoctors[i]);
                }
            }
        }
    }
}
