using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for AddVisitWindow2.xaml
    /// </summary>
    public partial class AddVisitWindow2 : Window
    {
        MainWindow mainWindow = null;
        static string connectionString = MainWindow.connectionString;
        static SqlConnection connection = new SqlConnection(connectionString);
        static SqlCommand command;
        static SqlDataAdapter dataAdapter;
        static DataTable table = new DataTable();

        static int cardID = 1;
        static string date = "01.01.2000";
        static string complaints = "Головний біль";
        static string diagnosis = "Мігрень";
        static int departmentID = 1;
        static string has_sickleave = "Ні";
        static int sickleaveDuration = -1;
        static int doctorID = -1;

        

        public AddVisitWindow2(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            GetPatientVisitsData();
            UpdateComboboxes();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }

        private void TB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }

        private void Button_SetDafaultValues_Click(object sender, RoutedEventArgs e)
        {
            // TextBoxes:
            TB_Date_1.Text = "01.01.2000";
            TB_Complaints_2.Text = "Головний біль";
            TB_Diagnosis_3.Text = "Мігрень";
            TB_HasSickleave_4.Text = "Ні";
            TB_SickleaveDuration_5.Text = "5";

            // ComboBoxes:
            CB_Patient_1.SelectedIndex = 0;
            CB_Direction_2.SelectedItem = 0;
        }

        private void Button_Insert_Click(object sender, RoutedEventArgs e)
        {
            string strCardID = CB_Patient_1.SelectedItem.ToString();
            string strDepID = CB_Direction_2.SelectedItem.ToString();
            string strDocID = CB_Doctor_3.SelectedItem.ToString();

            string[] arrCardID = strCardID.Split(' ');
            string[] arrDepID = strDepID.Split(' ');
            string[] arrDocID = strDocID.Split(' ');

            string query = "";
            if (TB_HasSickleave_4.Text == "Ні")
            {
                cardID = int.Parse(arrCardID[0]);
                date = SwapDate(TB_Date_1.Text);
                complaints = TB_Complaints_2.Text;
                diagnosis = TB_Diagnosis_3.Text;
                departmentID = int.Parse(arrDepID[0]);
                has_sickleave = TB_HasSickleave_4.Text;

                query = $"insert into dbo.Visits (id, card_id, date, complaints, diagnosis, department_id, has_sickleave) " +
                    $"values((select count(id) from dbo.Visits) + 1, " +
                    $"{cardID}, '{date}', '{complaints}', '{diagnosis}', {departmentID}, '{has_sickleave}')";
                try
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception err)
                {
                    connection.Close();
                    MessageBox.Show(err.Message);
                }
            }
            else if (has_sickleave == "Так")
            {
                cardID = int.Parse(arrCardID[0]);
                date = SwapDate(TB_Date_1.Text);
                complaints = TB_Complaints_2.Text;
                diagnosis = TB_Diagnosis_3.Text;
                departmentID = int.Parse(arrDepID[0]);
                has_sickleave = TB_HasSickleave_4.Text;
                sickleaveDuration = int.Parse(TB_SickleaveDuration_5.Text);
                doctorID = int.Parse(arrDocID[0]);

                query = $"insert into dbo.Visits values((select count(id) from dbo.Visits) + 1, '{cardID}', '{date}', '{complaints}', " +
                $"'{diagnosis}', '{departmentID}', '{has_sickleave}', '{sickleaveDuration}', '{doctorID}')";
                try
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception err)
                {
                    connection.Close();
                    MessageBox.Show(err.Message);
                }
            }
            GetPatientVisitsData();
        }

        /// <summary>
        /// Змінити місяць та день місцями.
        /// </summary>
        /// <param name="currentDate"> Початкова дата. </param>
        /// <returns> Дата, у якої місяць та день змінили місця </returns>
        public static string SwapDate(string currentDate)
        {
            // Swapping day and month
            string[] arrCurDate1 = currentDate.Split('.');
            string[] arrCurDate2 = new string[arrCurDate1.Length];

            Array.Copy(arrCurDate1, arrCurDate2, arrCurDate1.Length);
            string newCurrentDate = "";

            arrCurDate2[0] = arrCurDate1[1];
            arrCurDate2[1] = arrCurDate1[0];

            newCurrentDate += arrCurDate2[0] + ".";
            newCurrentDate += arrCurDate2[1] + ".";
            newCurrentDate += arrCurDate1[2];

            return newCurrentDate;
        }

        private void TB_Date_1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TB_Date_1.Text.Length > 8 && TB_Date_1.Text.Length < 12)
            {
                try
                {
                    if (DateTime.Parse(TB_Date_1.Text) > DateTime.Now)
                    {
                        TB_Date_1.Text = "";
                        MessageBox.Show("Invalid date. Try again!");
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid date. Try again!");
                }
            }
        }

        private void TB_Complaints_2_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Complaints_2.Text.Length < 200 && TB_Complaints_2.Text.Length > 0)
            {
                try
                {
                    complaints = TB_Complaints_2.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_Complaints_2.Text.Length > 200)
            {
                complaints = "Болить голова";
                TB_Complaints_2.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void TB_Diagnosis_3_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_Diagnosis_3.Text.Length < 50 && TB_Diagnosis_3.Text.Length > 0)
            {
                try
                {
                    diagnosis = TB_Diagnosis_3.Text;
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_Diagnosis_3.Text.Length > 50)
            {
                diagnosis = "Болить голова";
                TB_Diagnosis_3.Text = "";
                MessageBox.Show("Invalid value. Try again!");

            }
        }

        private void TB_HasSickleave_4_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_HasSickleave_4.Text.Length < 4 && TB_HasSickleave_4.Text.Length > 0)
            {
                try
                {
                    has_sickleave = TB_HasSickleave_4.Text;
                    if (TB_HasSickleave_4.Text == "Так")
                    {
                        TB_SickleaveDuration_5.IsEnabled = true;
                        CB_Doctor_3.IsEnabled = true;
                    }
                    else
                    {
                        TB_SickleaveDuration_5.IsEnabled = false;
                        CB_Doctor_3.IsEnabled = false;
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_HasSickleave_4.Text.Length > 4)
            {
                has_sickleave = "Ні";
                TB_HasSickleave_4.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void TB_HasSickleave_4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_HasSickleave_4.Text == "Так")
            {
                TB_SickleaveDuration_5.IsEnabled = true;
                CB_Doctor_3.IsEnabled = true;
                has_sickleave = TB_HasSickleave_4.Text;
            }
            else if (TB_SickleaveDuration_5 != null)
            {
                TB_SickleaveDuration_5.IsEnabled = false;
                CB_Doctor_3.IsEnabled = false;
                has_sickleave = TB_HasSickleave_4.Text;
            }
        }

        private void TB_SickleaveDuration_5_KeyUp(object sender, KeyEventArgs e)
        {
            if (TB_SickleaveDuration_5.Text.Length < 10 && TB_SickleaveDuration_5.Text.Length > 0)
            {
                try
                {
                    sickleaveDuration = int.Parse(TB_SickleaveDuration_5.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value. Try again!");
                }
            }
            else if (TB_SickleaveDuration_5.Text.Length > 10)
            {
                sickleaveDuration = 5;
                TB_SickleaveDuration_5.Text = "";
                MessageBox.Show("Invalid value. Try again!");
            }
        }

        private void CB_Patient_1_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string stringID = CB_Patient_1.SelectedItem.ToString();
                string[] arrID = stringID.Split(' ');
                cardID = int.Parse(arrID[0].ToString());
                GetPatientVisitsData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void CB_Direction_2_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string stringDirection = CB_Direction_2.SelectedItem.ToString();
                string[] arrDirection = stringDirection.Split(' ');
                departmentID = int.Parse(arrDirection[0].ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void CB_Doctor_3_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string stringDocID = CB_Direction_2.SelectedItem.ToString();
                string[] arrDocID = stringDocID.Split(' ');
                doctorID = int.Parse(stringDocID[0].ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        void GetAndShowData(string query)
        {
            table.Clear();
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(query, connection);
            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(table);
            DataGrid1.ItemsSource = table.DefaultView;
            connection.Close();
        }

        void GetPatientVisitsData()
        {
            string query = "";
            if (CB_Patient_1.SelectedItem != null)
            {
                string strCardID = CB_Patient_1.SelectedItem.ToString();
                string[] arrCardID = strCardID.Split(' ');

                query = "select dbo.Visits.id as [№], dbo.Cards.name as [Ім'я пацієнта]," +
                    " dbo.Cards.surname as [Прізвище пацієнта], dbo.Cards.secname as [По батькові пацієнта]," +
                    " dbo.Visits.date as [Дата відвідування], dbo.Visits.complaints as [Скарги]," +
                    " dbo.Visits.diagnosis as [Діагноз], dbo.Departments.department_name as [Направлення]," +
                    " dbo.Visits.has_sickleave as [Має лікарняний лист?]," +
                    " dbo.Visits.sickleave_term_days as [Тривалість лікарняняного (дні)]," +
                    " dbo.Doctors.Name as [Ім'я лікаря]," +
                    " dbo.Doctors.Surname as [Прізвище лікаря]" +
                    "from dbo.Visits inner join" +
                    " dbo.Cards on dbo.Cards.id = dbo.Visits.card_id inner join" +
                    " dbo.Departments on dbo.Departments.id = dbo.Visits.department_id left join" +
                    " dbo.Doctors on dbo.Doctors.id = dbo.Visits.doctor_id " +
                    $"where dbo.Cards.id = {arrCardID[0]}";
            }
            else
            {
                query = "select dbo.Visits.id as [№], dbo.Cards.name as [Ім'я пацієнта]," +
                    " dbo.Cards.surname as [Прізвище пацієнта], dbo.Cards.secname as [По батькові пацієнта]," +
                    " dbo.Visits.date as [Дата відвідування], dbo.Visits.complaints as [Скарги]," +
                    " dbo.Visits.diagnosis as [Діагноз], dbo.Departments.department_name as [Направлення]," +
                    " dbo.Visits.has_sickleave as [Має лікарняний лист?]," +
                    " dbo.Visits.sickleave_term_days as [Тривалість лікарняняного (дні)]," +
                    " dbo.Doctors.Name as [Ім'я лікаря]," +
                    " dbo.Doctors.Surname as [Прізвище лікаря]" +
                    "from dbo.Visits inner join" +
                    " dbo.Cards on dbo.Cards.id = dbo.Visits.card_id inner join" +
                    " dbo.Departments on dbo.Departments.id = dbo.Visits.department_id left join" +
                    " dbo.Doctors on dbo.Doctors.id = dbo.Visits.doctor_id " +
                    $"where dbo.Cards.id = 1";
            }
            GetAndShowData(query);
        }

        int GetNumOfNames()
        {
            int numOfNames = 0;
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
            connection = new SqlConnection(connectionString);
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

        int GetNumOfDirections()
        {
            int numOfNames = 0;
            connection.Open();
            SqlCommand cmd = new SqlCommand("(select count(id) from dbo.Departments)", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                numOfNames = int.Parse(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            return numOfNames;
        }

        string[] GetDirectionsData()
        {
            int numOfDirections = GetNumOfDirections() + 1;
            string[] line = new string[numOfDirections];
            connection = new SqlConnection(connectionString);
            for (int i = 0; i < numOfDirections; i++)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select department_name from dbo.Departments where id =@ID", connection);
                cmd.Parameters.AddWithValue("@ID", i);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    line[i] += i + " ";
                    line[i] += da.GetValue(0).ToString().Trim();
                }
                da.Close();
                connection.Close();
            }
            return line;
        }

        int GetNumOfDoctors()
        {
            int numOfDocs = 0;
            connection.Open();
            SqlCommand cmd = new SqlCommand("(select count(id) from dbo.Doctors)", connection);
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
                SqlCommand cmd = new SqlCommand("select name, surname from dbo.Doctors where id =@ID", connection);
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

        void UpdateComboboxes()
        {
            string[] dataNames = GetNamesData();
            for (int i = 0; i < dataNames.Length; i++)
            {
                if (dataNames[i] != null)
                {
                    CB_Patient_1.Items.Add(dataNames[i]);
                }
            }
            CB_Patient_1.SelectedIndex = 0;

            string[] dataDirections = GetDirectionsData();
            for (int i = 0; i < dataDirections.Length; i++)
            {
                if (dataDirections[i] != null)
                {
                    CB_Direction_2.Items.Add(dataDirections[i]);
                }
            }
            CB_Direction_2.SelectedIndex = 0;

            string[] dataDoctors = GetDoctorsData();
            for (int i = 0; i < dataDoctors.Length; i++)
            {
                if (dataDoctors[i] != null)
                {
                    CB_Doctor_3.Items.Add(dataDoctors[i]);
                }
            }
            CB_Doctor_3.SelectedIndex = 0;
        }
    }
}
