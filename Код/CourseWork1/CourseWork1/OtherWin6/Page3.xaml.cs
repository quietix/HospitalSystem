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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        static string connectionString = MainWindow.connectionString;

        public Page3()
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
                    CB1_Patients.Items.Add(dataNames[i]);
                }
            }
            CB1_Patients.SelectedIndex = 0;
        }

        private void CB1_Patients_Closed(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void ShowInfo()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            if (CB1_Patients.SelectedItem != null)
            {
                string doctor_id = CB1_Patients.SelectedItem.ToString().Split(' ')[0];
                string query = "select dbo.Doctors.cabinet as [Кабінет], " +
                "dbo.Doctors.days_of_reception as [Дні прийому], " +
                "dbo.Doctors.time_start as [Час початку прийому], " +
                "dbo.Doctors.time_end as [Час закінчення прийому] from dbo.Doctors " +
                $"where dbo.Doctors.id = {doctor_id}";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                DataGrid1.ItemsSource = table.DefaultView;
                con.Close();
            }
        }
    }
}
