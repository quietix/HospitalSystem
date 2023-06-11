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
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        static string connectionString = MainWindow.connectionString;

        public Page4()
        {
            InitializeComponent();
            UpdateComboBox();
            ShowInfo();
        }

        private int GetNumOfNames()
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

        string[] GetNamesData()
        {
            int numOfNames = GetNumOfNames() + 1;
            string[] line = new string[numOfNames];
            SqlConnection connection = new SqlConnection(connectionString);

            for (int i = 0; i < numOfNames; i++)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select surname, name, secname from dbo.Doctors where id =@ID", connection);
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
                    CB1_Doctors.Items.Add(dataNames[i]);
                }
            }

            CB1_Doctors.SelectedIndex = 0;
        }

        /// <summary>
        /// Отримати усі відвідування певного пацієнта.
        /// </summary>
        public static DataTable GetPatientInfo(int doctor_id)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "select dbo.Cards.id as [patient_id], " +
                "dbo.Cards.name, dbo.Cards.surname, dbo.Cards.secname, " +
                "Visits.sickleave_term_days, dbo.Visits.date " +
                "from dbo.Visits inner join " +
                "dbo.Doctors on dbo.Doctors.id = dbo.Visits.doctor_id inner join " +
                "dbo.Cards on dbo.Cards.id = dbo.Visits.card_id " +
                $"where dbo.Doctors.id = {doctor_id}";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            con.Close();
            return dt;
        }

        /// <summary>
        /// Прибрати відвідування зі старим лікарняним.
        /// </summary>
        /// <returns> Відвідування з актуальними лікарняними. </returns>
        public static DataTable RemoveOldVisits(DataTable tableToRemove)
        {
            DataTable dt = tableToRemove.Clone();

            for (int i = 0; i < tableToRemove.Rows.Count; i++)
            {
                string dateOfVisit = tableToRemove.Rows[i][5].ToString();
                int sickleaveTerm = int.Parse(tableToRemove.Rows[i][4].ToString());

                DateTime dateOfSickleaveEnd = DateTime.Parse(dateOfVisit).AddDays(sickleaveTerm);

                if (dateOfSickleaveEnd > DateTime.Now)
                {
                    dt.Rows.Add(tableToRemove.Rows[i].ItemArray);
                }                
            }

            return dt;
        }

        private void ShowInfo()
        {
            if (CB1_Doctors.SelectedItem != null)
            {
                string doctor_id = CB1_Doctors.SelectedItem.ToString().Split(' ')[0];
                DataTable patientInfoTable = GetPatientInfo(int.Parse(doctor_id));
                patientInfoTable = RemoveOldVisits(patientInfoTable);
                DataGrid1.ItemsSource = patientInfoTable.DefaultView;
            }
        }

        private void CB1_Patients_Closed(object sender, EventArgs e)
        {
            ShowInfo();
        }
    }
}
