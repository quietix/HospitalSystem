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
    /// Interaction logic for Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        static string connectionString = MainWindow.connectionString;

        public Page5()
        {
            InitializeComponent();
        }

        private string GetDepartmentName(string complaint)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "select dbo.Visits.complaints as [Скарги], " +
                "dbo.Departments.department_name as [Направлення] " +
                "from dbo.Doctors inner join " +
                "dbo.Visits on dbo.Visits.doctor_id = dbo.Doctors.id inner " +
                "join dbo.Departments on dbo.Departments.id = dbo.Visits.department_id " +
                $"where dbo.Visits.complaints = '{complaint}'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();

            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (!dict.ContainsKey(table.Rows[i][1].ToString()))
                {
                    dict.Add(table.Rows[i][1].ToString(), 1);
                }
                else
                {
                    dict[table.Rows[i][1].ToString()]++;
                }
            }

            List<string> oddKeys = new List<string>();

            if (dict.Count > 0)
            {
                int max = dict[table.Rows[0][1].ToString()];
                oddKeys.Add(table.Rows[0][1].ToString());

                for (int i = 1; i < table.Rows.Count; i++)
                {
                    string key = table.Rows[i][1].ToString();
                    if (!oddKeys.Contains(key))
                    {
                        max = Math.Max(max, dict[key]);
                        oddKeys.Add(key);
                    }
                }

                foreach (var item in dict)
                {
                    if (item.Value == max)
                    {
                        return item.Key;
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// Отримати id лікарів, які працюють в заданому відділенні.
        /// </summary>
        /// <returns> id типу int </returns>
        private List<int> GetDoctorsIds(string brench)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "select dbo.Doctors.id as [№ лікаря], " +
                "dbo.Doctors.name as [Ім'я лікаря], " +
                "dbo.Doctors.surname as [Прізвище лікаря], " +
                "dbo.Doctors.secname as [По батькові лікаря] " +
                "from dbo.Doctors inner join " +
                "dbo.DepatrmentToDoctor on dbo.DepatrmentToDoctor.doctor_id = dbo.Doctors.id inner " +
                "join " +
                "dbo.Departments on dbo.Departments.id = dbo.DepatrmentToDoctor.department_id " +
                $"where dbo.Departments.department_name = '{brench}'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();

            List<int> doctor_ids = new List<int>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int id = int.Parse(table.Rows[i][0].ToString());
                doctor_ids.Add(id);
            }

            return doctor_ids;
        }

        /// <summary>
        /// Отримати кільксть пацієнтів, що знаходяться на лікуванні у даного лікаря.
        /// </summary>
        /// <returns> Число пацієнтів. </returns>
        private int GetNumOfPatientsOfDoctor(int doctor_id)
        {
            DataTable patientInfoTable = Page4.GetPatientInfo(doctor_id);
            patientInfoTable = Page4.RemoveOldVisits(patientInfoTable);
            return patientInfoTable.Rows.Count;
        }

        /// <summary>
        /// Отримати id лікаря, що має найменше клієнтів.
        /// </summary>
        /// <param name="doctor_ids"> Список з усіма лікарями даного відділу. </param>
        /// <returns></returns>
        private int GetDoctorID(List<int> doctor_ids)
        {
            Dictionary<int, int> docId_NumOfPatients = new Dictionary<int, int>();

            for (int i = 0; i < doctor_ids.Count; i++)
            {
                if (!docId_NumOfPatients.Keys.Contains(doctor_ids[i]))
                {
                    int numOfPatients = GetNumOfPatientsOfDoctor(doctor_ids[i]);
                    docId_NumOfPatients.Add(doctor_ids[i], numOfPatients);
                }
            }

            int minNumOfPatients = GetNumOfPatientsOfDoctor(doctor_ids[0]);
            foreach (var item in docId_NumOfPatients)
            {
                minNumOfPatients = Math.Min(minNumOfPatients, item.Value);
            }

            foreach (var item in docId_NumOfPatients)
            {
                if (item.Value == minNumOfPatients)
                {
                    return item.Key;
                }
            }

            return doctor_ids[0];
        }

        /// <summary>
        /// Показати ПІБ лікаря, кабінет, час та дні роботи.
        /// </summary>
        private void ShowData(int doctor_id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "select dbo.Doctors.id as [№ лікаря], " +
                "dbo.Doctors.surname as [Прізвище лікаря], " +
                "dbo.Doctors.name as [Ім'я лікаря], " +
                "dbo.Doctors.secname as [По батькові лікаря], " +
                "dbo.Doctors.days_of_reception as [Дні прийому], " +
                "dbo.Doctors.time_start as [Час початку прийому], " +
                "dbo.Doctors.time_end as [Час закінчення прийому], " +
                "dbo.Doctors.cabinet as [Кабінет] " +
                $"from dbo.Doctors where dbo.Doctors.id = {doctor_id}";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            DataGrid1.ItemsSource = table.DefaultView;
        }

        private void TB1_KeyUP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string complaint = TB1_Complaint.Text;
                if (complaint.Length > 0)
                {
                    string department_name = GetDepartmentName(complaint);
                    List<int> doctors_ids = GetDoctorsIds(department_name);
                    if (doctors_ids.Count > 0)
                    {
                        int doctorIdToShow = GetDoctorID(doctors_ids);
                        ShowData(doctorIdToShow);
                    }
                    else
                    {
                        department_name = "Діагностичне відділення";
                        doctors_ids = GetDoctorsIds(department_name);
                        if (doctors_ids.Count > 0)
                        {
                            int doctorIdToShow = GetDoctorID(doctors_ids);
                            ShowData(doctorIdToShow);
                            MessageBox.Show("Такої скарги ще не було. Направляємо вас до Діагностичного відділення.");
                        }
                        else
                        {
                            MessageBox.Show("Наразі немає лікарів, які можуть прийняти пацієнта.");
                        }
                    }
                }
            }
        }
    }
}
