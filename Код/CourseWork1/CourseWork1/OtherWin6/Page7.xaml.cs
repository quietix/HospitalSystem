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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork1.OtherWin6
{
    /// <summary>
    /// Interaction logic for Page7.xaml
    /// </summary>
    public partial class Page7 : Page
    {
        static string connectionString = MainWindow.connectionString;
        static SqlConnection connection = new SqlConnection(connectionString);
        static DataTable table = new DataTable();

        public Page7()
        {
            InitializeComponent();
            GetVisitsTableData();
            UpdateCB1();
        }

        private void CB1_Patients_DropDownClosed(object sender, EventArgs e)
        {
            if (CB1_Patients.SelectedItem != null)
            {
                string query = "select Cards.id as [№ пацієнта], " +
                "Cards.surname as [Прізвище пацієнта], " +
                "Cards.name as [Ім'я пацієнта], " +
                "Cards.secname as [По батькові пацієнта], " +
                "Visits.id as [№ відвідування], " +
                "Visits.date as [Дата відвідування], " +
                "Visits.complaints as [Скарги], " +
                "Visits.diagnosis as [Діагноз], " +
                "department_id as [№ направлення], " +
                "department_name as [Направлення], " +
                "Visits.has_sickleave as [Має лікарняний?], " +
                "Visits.sickleave_term_days as [Тривалість лікарняного(дні)], " +
                "Doctors.name as [Ім'я лікаря], Doctors.surname[Прізвище лікаря] " +
                "from Visits inner join " +
                "Cards on Cards.id = Visits.card_id left join " +
                "Doctors on Doctors.id = Visits.doctor_id inner " +
                "join Departments on Departments.id = Visits.department_id " +
                $"where Visits.date >= '{GetPrevMonthData()}' and dbo.Cards.id = {CB1_Patients.SelectedItem.ToString().Split(' ')[0]} " +
                $"order by Cards.id, dbo.Visits.id asc";
                GetAndShowData(query);
            }
        }

        void UpdateCB1()
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string item = table.Rows[i][0].ToString().Trim() + " ";
                item += table.Rows[i][1].ToString().Trim() + " ";
                item += table.Rows[i][2].ToString().Trim() + " ";
                item += table.Rows[i][3].ToString().Trim();
                if (!CB1_Patients.Items.Contains(item))
                {
                    CB1_Patients.Items.Add(item);
                }
            }
        }

        void GetVisitsTableData()
        {
            string query = "select Cards.id as [№ пацієнта], " +
                "Cards.surname as [Прізвище пацієнта], " +
                "Cards.name as [Ім'я пацієнта], " +
                "Cards.secname as [По батькові пацієнта], " +
                "Visits.id as [№ відвідування], " +
                "Visits.date as [Дата відвідування], " +
                "Visits.complaints as [Скарги], " +
                "Visits.diagnosis as [Діагноз], " +
                "department_id as [№ направлення], " +
                "department_name as [Направлення], " +
                "Visits.has_sickleave as [Має лікарняний?], " +
                "Visits.sickleave_term_days as [Тривалість лікарняного(дні)], " +
                "Doctors.name as [Ім'я лікаря], Doctors.surname[Прізвище лікаря] " +
                "from Visits inner join " +
                "Cards on Cards.id = Visits.card_id left join " +
                "Doctors on Doctors.id = Visits.doctor_id inner " +
                "join Departments on Departments.id = Visits.department_id " +
                $"where Visits.date >= '{GetPrevMonthData()}' order by Cards.id, dbo.Visits.id asc";
            GetAndShowData(query);
            FillTable(query);
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

        string GetPrevMonthData()
        {
            DateTime now = DateTime.Now;
            DateTime lastMonth = now.AddMonths(-1);

            string currentDate = lastMonth.ToShortDateString();
            string[] arrCurDate1 = currentDate.Split('.');
            string[] arrCurDate2 = new string[arrCurDate1.Length];

            Array.Copy(arrCurDate1, arrCurDate2, arrCurDate1.Length);

            arrCurDate2[0] = arrCurDate1[1];
            arrCurDate2[1] = arrCurDate1[0];

            string newCurrentDate = "";

            newCurrentDate += arrCurDate2[0] + ".";
            newCurrentDate += arrCurDate2[1] + ".";
            newCurrentDate += arrCurDate2[2];

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int numOfRows = DataGrid1.Items.Count - 1;
            TB1_ShowNumOfVisits.Text = numOfRows.ToString();
        }
    }
}
