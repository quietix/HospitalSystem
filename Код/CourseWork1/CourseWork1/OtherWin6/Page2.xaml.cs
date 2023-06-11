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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        static string connectionString = MainWindow.connectionString;
        static DataTable table;
        public static DataTable totalTable;

        public Page2()
        {
            InitializeComponent();
            table = new DataTable();
            totalTable = new DataTable();
            SetTableConfig(totalTable);
            UpdateComboBox();
            ShowDoctorPIB();
        }

        private void CB1_Patients_Closed(object sender, EventArgs e)
        {
            ShowDoctorPIB();
        }

        private void SetTableConfig(DataTable dataTable)
        {
            dataTable.Columns.Add("visit_id", typeof(int));
            dataTable.Columns.Add("card_id", typeof(int));
            dataTable.Columns.Add("surname", typeof(string));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("secname", typeof(string));
            dataTable.Columns.Add("address", typeof(string));
            dataTable.Columns.Add("date", typeof(string));
            dataTable.Columns.Add("diagnosis", typeof(string));
            dataTable.Columns.Add("doctor_id", typeof(int));
            dataTable.Columns.Add("doctorSurname", typeof(string));
            dataTable.Columns.Add("doctorname", typeof(string));
            dataTable.Columns.Add("doctorSecname", typeof(string));
        }

        private void FillTable(int patientID)
        {
            table.Clear();
            string query = $"select dbo.Visits.id as [№ відвідування], dbo.Cards.id, " +
                $"dbo.Cards.surname as [Прізвище],  " +
                "dbo.Cards.name as [Ім'я], dbo.Cards.secname as [По батькові], " +
                "dbo.Cards.address as [Адреса], dbo.Visits.date as [Дата відвідування], " +
                "dbo.Visits.diagnosis as [Діагноз], dbo.Doctors.id, " +
                "dbo.Doctors.surname, " +
                "dbo.Doctors.name, dbo.Doctors.secname " +
                "from dbo.Cards inner join " +
                "dbo.Visits on dbo.Cards.id = dbo.Visits.card_id inner join " +
                "dbo.Doctors on dbo.Doctors.id = dbo.Visits.doctor_id " +
                $"where dbo.Cards.id = {patientID} order by dbo.Visits.id asc";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);
            con.Close();
        }

        private void ShowDoctorPIB()
        {
            int counter = 0;
            for (int i = 0; i <= CB1_Patients.Items.Count; i++)
            {
                // Getting last visit info of selected patient
                FillTable(i);
                int rowsNum = table.Rows.Count;

                if (rowsNum > 0)
                {
                    object[] arrLine = table.Rows[rowsNum - 1].ItemArray;

                    for (int j = 0; j < arrLine.Length; j++)
                    {
                        arrLine[j] = arrLine[j].ToString().Trim();
                    }

                    // Filling <totalTable> with last visit info of selected patient
                    totalTable.Rows.Add();
                    for (int j = 0; j < arrLine.Length; j++)
                    {
                        totalTable.Rows[counter][j] = arrLine[j];
                    }
                    counter++;
                }
            }

            totalTable.DefaultView.Sort = "visit_id asc";
            // Got full table with PIBs but with odd info.

            // __Getting only doctor: name, surnme, secname (table contains more info)
            string patient_id = CB1_Patients.SelectedItem.ToString().Split(' ')[0];

            DataView dv = new DataView(totalTable);
            dv.RowFilter = $"card_id = {patient_id}";

            DataTable tableToShow = new DataTable();
            tableToShow.Columns.Add("№ лікаря");
            tableToShow.Columns.Add("Ім'я лікаря");
            tableToShow.Columns.Add("Прізвище лікаря");
            tableToShow.Columns.Add("По батькові лікаря");

            for (int i = 0; i < dv.ToTable().Rows.Count; i++)
            {
                int tmpCounter = 0;
                if (dv.ToTable().Rows[i].ItemArray.Length > 0)
                {
                    tableToShow.Rows.Add();
                    tableToShow.Rows[tmpCounter][0] = dv.ToTable().Rows[tmpCounter][8];
                    tableToShow.Rows[tmpCounter][1] = dv.ToTable().Rows[tmpCounter][9];
                    tableToShow.Rows[tmpCounter][2] = dv.ToTable().Rows[tmpCounter][10];
                    tableToShow.Rows[tmpCounter][3] = dv.ToTable().Rows[tmpCounter][11];
                    tmpCounter++;
                }
            }
            DataGrid1.ItemsSource = tableToShow.DefaultView;
        }

        private int GetNumOfNames()
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
                SqlCommand cmd = new SqlCommand("select surname, name, secname from dbo.Cards where id =@ID", connection);
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

        private void UpdateComboBox()
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
