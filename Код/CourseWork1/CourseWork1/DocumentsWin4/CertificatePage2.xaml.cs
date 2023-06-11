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
    /// Interaction logic for CertificatePage2.xaml
    /// </summary>
    public partial class CertificatePage2 : Page
    {
        static string connectionString = MainWindow.connectionString;
        static SqlConnection connection = new SqlConnection(connectionString);
        static DataTable table = new DataTable();

        public CertificatePage2()
        {
            InitializeComponent();
            GetVisitsTableData();
            UpdateCB1();
        }

        private void CB1_Patients_DropDownClosed(object sender, EventArgs e)
        {
            if (CB1_Patients.SelectedItem != null)
            {
                CB2_Complaints.IsEnabled = true;
                CB3_Date.IsEnabled = true;
                string[] tmpArr = CB1_Patients.SelectedItem.ToString().Split(' ');
                int patientID = int.Parse(tmpArr[0]);
                string query = "select Visits.id as [№ відвідування], Cards.id as [№ пацієнта], " +
                "Cards.surname as [Прізвище пацієнта], Cards.name as [Ім'я пацієнта], Cards.secname as [По батькові пацієнта], " +
                "Visits.date as [Дата відвідування], Visits.complaints as [Скарги], " +
                "Visits.diagnosis as [Діагноз], department_id as [№ направлення], " +
                "department_name as [Направлення], Visits.has_sickleave as [Має лікарняний ?], " +
                "Visits.sickleave_term_days as [Тривалість лікарняного(дні)], " +
                "Doctors.name as [Ім'я лікаря], Doctors.surname[Прізвище лікаря] " +
                "from Visits inner join " +
                "Cards on Cards.id = Visits.card_id left join " +
                "Doctors on Doctors.id = Visits.doctor_id inner join " +
                "Departments on Departments.id = Visits.department_id " +
                $"where Visits.date >= '{GetPrevYearData()}' and Cards.id = {patientID} " +
                "order by Cards.id asc";
                GetAndShowData(query);
                FillTable(query);
                UpdateCB2();
                UpdateCB3();
            }
        }

        private void CB2_Complaints_DropDownClosed(object sender, EventArgs e)
        {
            if (CB2_Complaints.SelectedItem != null)
            {
                string[] tmpArr = CB1_Patients.SelectedItem.ToString().Split(' ');
                int patientID = int.Parse(tmpArr[0]);
                string furtherWay = (CB3_Date.SelectedItem != null) ?
                    ($"where Visits.date = '{CB3_Date.SelectedItem.ToString().Trim()}' and Cards.id = {patientID} and " +
                    $"Visits.complaints = '{CB2_Complaints.SelectedItem.ToString().Trim()}' " +
                    "order by Cards.id asc") :
                    ($"where Visits.date >= '{GetPrevYearData()}' and Cards.id = {patientID} and " +
                    $"Visits.complaints = '{CB2_Complaints.SelectedItem.ToString().Trim()}' " +
                    "order by Cards.id asc");

                string query = "select Visits.id as [№ відвідування], Cards.id as [№ пацієнта], Cards.surname as [Прізвище пацієнта], " +
                    "Cards.name as [Ім'я пацієнта], Cards.secname as [По батькові пацієнта], " +
                    "Visits.date as [Дата відвідування], Visits.complaints as [Скарги], " +
                    "Visits.diagnosis as [Діагноз], department_id as [№ направлення], " +
                    "department_name as [Направлення], Visits.has_sickleave as [Має лікарняний ?], " +
                    "Visits.sickleave_term_days as [Тривалість лікарняного(дні)], " +
                    "Doctors.name as [Ім'я лікаря], Doctors.surname[Прізвище лікаря] " +
                    "from Visits inner join " +
                    "Cards on Cards.id = Visits.card_id left join " +
                    "Doctors on Doctors.id = Visits.doctor_id inner join " +
                    "Departments on Departments.id = Visits.department_id " +
                    $"{furtherWay}";
                GetAndShowData(query);
                FillTable(query);
                UpdateCB3();
            }
        }

        private void CB3_Date_DropDownClosed(object sender, EventArgs e)
        {
            if (CB3_Date.SelectedItem != null)
            {
                string[] tmpArr = CB1_Patients.SelectedItem.ToString().Split(' ');
                int patientID = int.Parse(tmpArr[0]);
                string furtherWay = (CB2_Complaints.SelectedItem != null) ? 
                    ($" and Visits.complaints = '{CB2_Complaints.SelectedItem.ToString().Trim()}' ") : 
                    (" ");

                string query = "select Visits.id as [№ відвідування], Cards.id as [№ пацієнта], Cards.surname as [Прізвище пацієнта], " +
                    "Cards.name as [Ім'я пацієнта], Cards.secname as [По батькові пацієнта], " +
                    "Visits.date as [Дата відвідування], Visits.complaints as [Скарги], " +
                    "Visits.diagnosis as [Діагноз], department_id as [№ направлення], " +
                    "department_name as [Направлення], Visits.has_sickleave as [Має лікарняний ?], " +
                    "Visits.sickleave_term_days as [Тривалість лікарняного(дні)], " +
                    "Doctors.name as [Ім'я лікаря], Doctors.surname[Прізвище лікаря] " +
                    "from Visits inner join " +
                    "Cards on Cards.id = Visits.card_id left join " +
                    "Doctors on Doctors.id = Visits.doctor_id inner join " +
                    "Departments on Departments.id = Visits.department_id " +
                    $"where Visits.date = '{CB3_Date.SelectedItem.ToString().Trim()}' " +
                    $"and Cards.id = {patientID}{furtherWay}" +
                    "order by Cards.id asc";
                GetAndShowData(query);
                FillTable(query);
                UpdateCB2();
            }
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

        string GetPrevYearData()
        {
            string currentDate = DateTime.Now.ToShortDateString();
            string[] arrCurDate1 = currentDate.Split('.');
            string[] arrCurDate2 = new string[arrCurDate1.Length];
            Array.Copy(arrCurDate1, arrCurDate2, arrCurDate1.Length);
            arrCurDate2[0] = arrCurDate1[1];
            arrCurDate2[1] = arrCurDate1[0];
            string newCurrentDate = "";
            newCurrentDate += arrCurDate2[0] + ".";
            newCurrentDate += arrCurDate2[1] + ".";
            int prevYear = int.Parse(arrCurDate2[2]) - 1;
            newCurrentDate +=  prevYear.ToString();
            return newCurrentDate;
        }

        void FillTable(string query)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            table.Clear();
            adapter.Fill(table);
            connection.Close();
        }

        void GetVisitsTableData()
        {
            string query = "select Visits.id as [№ відвідування], Cards.id as [№ пацієнта], " +
                "Cards.surname as [Прізвище пацієнта],  Cards.name as [Ім'я пацієнта], Cards.secname as [По батькові пацієнта], " +
                "Visits.date as [Дата відвідування], Visits.complaints as [Скарги], " +
                "Visits.diagnosis as [Діагноз], department_id as [№ направлення], " +
                "department_name as [Направлення], Visits.has_sickleave as [Має лікарняний ?], " +
                "Visits.sickleave_term_days as [Тривалість лікарняного(дні)], " +
                "Doctors.name as [Ім'я лікаря], Doctors.surname[Прізвище лікаря] " +
                "from Visits inner join " +
                "Cards on Cards.id = Visits.card_id left join " +
                "Doctors on Doctors.id = Visits.doctor_id inner join " +
                "Departments on Departments.id = Visits.department_id " +
                $"where Visits.date >= '{GetPrevYearData()}' " +
                "order by Cards.id asc";
            GetAndShowData(query);
            FillTable(query);
        }

        string GetRewritenDate(string date1)
        {
            string date2 = "";
            string[] arrDate1 = date1.Split('.');
            string[] arrDate2 = new string[date1.Length];
            Array.Copy(arrDate1, arrDate2, arrDate1.Length);
            arrDate2[0] = arrDate1[1];
            arrDate2[1] = arrDate1[0];
            date2 += arrDate2[0] + ".";
            date2 += arrDate2[1] + ".";
            date2 += arrDate2[2];
            return date2;
        }

        void UpdateCB1()
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string item = table.Rows[i][1].ToString().Trim() + " ";
                item += table.Rows[i][2].ToString().Trim() + " ";
                item += table.Rows[i][3].ToString().Trim() + " ";
                item += table.Rows[i][4].ToString().Trim();
                if (!CB1_Patients.Items.Contains(item))
                {
                    CB1_Patients.Items.Add(item);
                }
            }
        }

        void UpdateCB2()
        {
            CB2_Complaints.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string complaint = table.Rows[i][6].ToString().Trim();
                if (!CB2_Complaints.Items.Contains(complaint))
                {
                    CB2_Complaints.Items.Add(complaint);
                }
            }
        }

        void UpdateCB3()
        {
            CB3_Date.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string date = table.Rows[i][5].ToString().Trim();
                if (!CB3_Date.Items.Contains(date))
                {
                    CB3_Date.Items.Add(GetRewritenDate(date));
                }
            }
        }
    }
}
