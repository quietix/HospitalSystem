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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        static string connectionString = MainWindow.connectionString;
        static DataTable table = new DataTable();

        public Page1()
        {
            InitializeComponent();
            UpdateComboBox();
            ShowSelectedPatient();
        }

        private void ShowSelectedPatient()
        {
            if (CB1_Patients.SelectedItem != null)
            {
                table.Clear();
                string fullPatientData = CB1_Patients.SelectedItem.ToString();
                string[] arrfullPatData = fullPatientData.Split(' ');
                string query = $"select dbo.Visits.id as [№ відвідування], dbo.Cards.name as [Ім'я], " +
                    "dbo.Cards.surname as [Прізвище], dbo.Cards.secname as [По батькові], " +
                    "dbo.Cards.address as [Адреса], dbo.Visits.date as [Дата відвідування], " +
                    "dbo.Visits.diagnosis as [Діагноз] from dbo.Cards inner join " +
                    "dbo.Visits on dbo.Cards.id = dbo.Visits.card_id " +
                    $"where dbo.Cards.id = {arrfullPatData[0]} order by dbo.Visits.id asc";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                DataGrid1.ItemsSource = table.DefaultView;
                con.Close();
            }
        }

        private void CB1_Patients_Closed(object sender, EventArgs e)
        {
            ShowSelectedPatient();
        }

        private void Button1_ShowLastVisit_Click(object sender, RoutedEventArgs e)
        {
            int rowsNum = table.Rows.Count;
            object[] arrLine = table.Rows[rowsNum - 1].ItemArray;
            for (int i = 0; i < arrLine.Length; i++)
            {
                arrLine[i] = arrLine[i].ToString().Trim();
            }
            table.Rows.Clear();
            table.Rows.Add();
            for (int i = 0; i < arrLine.Length; i++)
            {
                table.Rows[0][i] = arrLine[i];
            }
            DataGrid1.ItemsSource = table.DefaultView;
        }

        int GetNumOfNames()
        {
            int numOfNames = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("(select count(id) from dbo.Cards)", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                numOfNames = int.Parse(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            return numOfNames;
        }

        string[] GetNamesData()
        {
            int numOfNames = GetNumOfNames() + 1;
            string[] line = new string[numOfNames];
            SqlConnection connection = new SqlConnection(connectionString);
            for (int i = 0; i < numOfNames; i++)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select name, surname, secname from dbo.Cards where id =@ID", connection);
                cmd.Parameters.AddWithValue("@ID", i);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    line[i] += i + " ";
                    line[i] += da.GetValue(0).ToString().Trim() + " ";
                    line[i] += da.GetValue(1).ToString().Trim() + " ";
                    line[i] += da.GetValue(2).ToString().Trim();
                }
                da.Close();
                connection.Close();
            }
            return line;
        }

        void UpdateComboBox()
        {
            string[] dataNames = GetNamesData();
            for (int i = 0; i < dataNames.Length; i++)
            {
                if (dataNames[i] != null)
                {
                    CB1_Patients.Items.Add(dataNames[i]);
                }
            }
            CB1_Patients.SelectedIndex = 0;
        }
    }
}
