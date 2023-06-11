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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork1.OtherWin6
{
    /// <summary>
    /// Interaction logic for Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        static string connectionString = MainWindow.connectionString;

        public Page6()
        {
            InitializeComponent();
            UpdateComboBox();
            ShowInfo();
        }

        private void CB1_Cabinets_DropDownClosed(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void UpdateComboBox()
        {
            string[] dataCabinets = GetCabinetsData();

            for (int i = 0; i < dataCabinets.Length; i++)
            {
                if (dataCabinets[i] != null)
                {
                    CB1_Cabinets.Items.Add(dataCabinets[i]);
                }
            }

            CB1_Cabinets.SelectedIndex = 0;
        }

        private void ShowInfo()
        {
            if (CB1_Cabinets.SelectedItem != null)
            {
                string doctor_id = CB1_Cabinets.SelectedItem.ToString().Split(' ')[0];
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string query = "select dbo.Doctors.id as [№ лікаря], " +
                    "dbo.Doctors.surname as [Прізвище лікаря], " +
                    "dbo.Doctors.name as [Ім'я лікаря], " +
                    "dbo.Doctors.secname as [По батькові лікаря], " +
                    "dbo.Doctors.cabinet as [Кабінет] " +
                    $"from dbo.Doctors where dbo.Doctors.cabinet = {doctor_id}";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                con.Close();
                DataGrid1.ItemsSource = table.DefaultView;
            }
        }

        private int GetNumOfCabinets()
        {
            int numOfNames = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("(select count(id) from dbo.Doctors)", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                numOfNames = int.Parse(dataReader.GetValue(0).ToString());
            }

            connection.Close();
            return numOfNames;
        }

        string[] GetCabinetsData()
        {
            int numOfNames = GetNumOfCabinets() + 1;
            string[] line = new string[numOfNames];
            SqlConnection connection = new SqlConnection(connectionString);

            for (int i = 0; i < numOfNames; i++)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"select cabinet from dbo.Doctors where id = {i}", connection);
                SqlDataReader da = cmd.ExecuteReader();

                while (da.Read())
                {
                    line[i] += da.GetValue(0).ToString().Trim();
                }

                da.Close();
                connection.Close();
            }

            return line;
        }
    }
}
