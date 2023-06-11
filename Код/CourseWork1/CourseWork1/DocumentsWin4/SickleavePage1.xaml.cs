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

namespace CourseWork1.DocumentsWin4
{
    /// <summary>
    /// Interaction logic for SickleavePage1.xaml
    /// </summary>
    public partial class SickleavePage1 : Page
    {
        static string connectionString = MainWindow.connectionString;
        static SqlConnection connection = new SqlConnection(connectionString);
        static DataTable table = new DataTable();

        static int patientID = -1;
        static int visitID = -1;

        public SickleavePage1()
        {
            InitializeComponent();
            GetPatientVisitsData();
            UpdateCombobox1();
        }

        void GetAndShowData(string query)
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

        void GetPatientVisitsData()
        {
            string query = "select Visits.id as [№], Cards.name as [Ім'я пацієнта], Cards.surname as [Прізвище пацієнта], " +
                "Cards.secname as [По батькові пацієнта], Doctors.name as [Ім'я лікаря], " +
                "Doctors.surname[Прізвище лікаря], sickleave_term_days as [Тривалість лікарняного(дні)], " +
                "date as [Дата виписки лікарняного] from Visits inner join " +
                "Cards on Cards.id = Visits.card_id inner join Doctors on Doctors.id = Visits.doctor_id";
            GetAndShowData(query);
        }

        private void CB_Patients_1_DropDownClosed(object sender, EventArgs e)
        {
            if (CB_Patients_1.SelectedItem != null)
            {
                CB_Visits_2.IsEnabled = true;
                string[] arrPatient = CB_Patients_1.SelectedItem.ToString().Split(' ');
                patientID = int.Parse(arrPatient[0]);
                string query = "select Visits.id as [№], Cards.name as [Ім'я пацієнта], Cards.surname as [Прізвище пацієнта], " +
                    "Cards.secname as [По батькові пацієнта], Doctors.name as [Ім'я лікаря], " +
                    "Doctors.surname[Прізвище лікаря], sickleave_term_days as [Тривалість лікарняного(дні)], " +
                    "date as [Дата виписки лікарняного] from Visits inner join " +
                    "Cards on Cards.id = Visits.card_id inner " +
                    $"join Doctors on Doctors.id = Visits.doctor_id where Visits.card_id = {patientID}";
                GetAndShowData(query);
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                table.Clear();
                adapter.Fill(table);
                connection.Close();
                UpdateComboBox2();
            }
        }

        

        private void CB_Visits_2_DropDownClosed(object sender, EventArgs e)
        {
            if (CB_Visits_2.SelectedItem != null)
            {
                visitID = int.Parse(CB_Visits_2.SelectedItem.ToString());
                string query = "select Visits.id as [№], Cards.name as [Ім'я пацієнта], Cards.surname as [Прізвище пацієнта], " +
                    "Cards.secname as [По батькові пацієнта], Doctors.name as [Ім'я лікаря], " +
                    "Doctors.surname[Прізвище лікаря], sickleave_term_days as [Тривалість лікарняного(дні)], " +
                    "date as [Дата виписки лікарняного] from Visits inner join " +
                    "Cards on Cards.id = Visits.card_id inner " +
                    $"join Doctors on Doctors.id = Visits.doctor_id where Visits.id = {visitID}";
                GetAndShowData(query);
            }
        }

        int GetNumOfPatients()
        {
            int numOfDocs = 0;
            connection.Open();
            SqlCommand cmd = new SqlCommand("select count(id) from dbo.Visits", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                numOfDocs = int.Parse(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            return numOfDocs;
        }

        string[] GetPatientsData()
        {
            int numOfDirections = GetNumOfPatients() + 1;
            string[] line = new string[numOfDirections];
            connection = new SqlConnection(connectionString);
            for (int i = 0; i < numOfDirections; i++)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select surname, name from dbo.Cards where id =@ID", connection);
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

        void UpdateCombobox1()
        {
            string[] dataDoctors = GetPatientsData();
            for (int i = 0; i < dataDoctors.Length; i++)
            {
                if (dataDoctors[i] != null)
                {
                    CB_Patients_1.Items.Add(dataDoctors[i]);
                }
            }
        }

        private void UpdateComboBox2()
        {
            CB_Visits_2.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                object[] row = table.Rows[i].ItemArray;
                CB_Visits_2.Items.Add(row[0].ToString().Trim());
            }
        }
    }
}
