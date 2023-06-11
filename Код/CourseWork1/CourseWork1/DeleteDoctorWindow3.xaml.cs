using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CourseWork1
{
    /// <summary>
    /// Interaction logic for DeleteDoctorWindow3.xaml
    /// </summary>
    public partial class DeleteDoctorWindow3 : Window
    {
        MainWindow mainWindow = null;
        static string connectionString = MainWindow.connectionString;
        static SqlConnection connection = new SqlConnection(connectionString);

        static int idDocToDelete = -1;
        static int idPatientToTransfer = -1;
        static int idGettingPatientDoctor = -1;

        public DeleteDoctorWindow3(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            UpdateComboboxes();
            GetPatientVisitsData();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }

        private void Button_ShowAllPatients_Click(object sender, RoutedEventArgs e)
        {
            string query = "select dbo.Cards.id as [№], dbo.Cards.surname as [Прізвище]," +
                "dbo.Cards.name as [Ім'я], dbo.Cards.secname as [Ім'я по батькові]," +
                "dbo.Cards.address as [Адреса], dbo.Cards.sex as [Стать]," +
                "dbo.Cards.age as [Вік], dbo.Cards.insurance_policy_number as [Номер страхового полісу]," +
                "dbo.Cards.date_of_card_creation as [Дата створення карти]" +
                "from dbo.Cards";
            try
            {
                GetAndShowData(query);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Button_ShowAllDoctors_Click(object sender, RoutedEventArgs e)
        {
            GetPatientVisitsData();
        }

        private void Button_TransferPatient_Click(object sender, RoutedEventArgs e)
        {
            if (CB_TakingDoctor_3.SelectedItem != null)
            {
                string query = $"update dbo.Visits set doctor_id = {idGettingPatientDoctor} where doctor_id = {idDocToDelete} and card_id = {idPatientToTransfer}";
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                ShowPatientsData(idDocToDelete);
                UpdateComboboxes();
            }
        }

        private void Button_DeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            UpdateComboboxes();
            string[] dataPatients = null;
            if (CB_DoctorToDelete_1.SelectedItem != null)
            {
                dataPatients = GetPatientsData(int.Parse(CB_DoctorToDelete_1.SelectedItem.ToString().Split(' ')[0]));
            }
            if (dataPatients != null)
            {
                if (dataPatients[0] != null)
                {
                    MessageBox.Show("Список з пацієнтами не порожній. Видалення неможливе");
                }
                else
                {
                    MessageBox.Show("Видалення лікаря!");
                    string query = $"delete from dbo.Doctors where dbo.Doctors.id = {idDocToDelete}";
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    GetPatientVisitsData();
                    CB_DoctorToDelete_1.Items.Clear();
                    UpdateComboboxes();
                    CB_DoctorToDelete_1.SelectedIndex = -1;
                    CB_PatientToTransfer_2.SelectedIndex = -1;
                    CB_TakingDoctor_3.SelectedIndex = -1;
                }
            }
        }

        private void CB_Patient_1_DropDownClosed(object sender, EventArgs e)
        {
            if (CB_DoctorToDelete_1.SelectedItem != null)
            {
                string stringDocID = CB_DoctorToDelete_1.SelectedItem.ToString();
                string[] arrDocID = stringDocID.Split(' ');
                idDocToDelete = int.Parse(arrDocID[0].ToString());
                ShowPatientsData(idDocToDelete);
                UpdateComboboxes();
            }
        }

        private void CB_Direction_2_DropDownClosed(object sender, EventArgs e)
        {
            if (CB_PatientToTransfer_2.SelectedItem != null)
            {
                string stringPatID = CB_PatientToTransfer_2.SelectedItem.ToString();
                string[] arrPatID = stringPatID.Split(' ');
                idPatientToTransfer = int.Parse(arrPatID[0].ToString());
            }
        }

        private void CB_TakingDoctor_3_DropDownClosed(object sender, EventArgs e)
        {
            if (CB_TakingDoctor_3.SelectedItem != null)
            {
                string stringDocTakingID = CB_TakingDoctor_3.SelectedItem.ToString();
                string[] arrDocTakingID = stringDocTakingID.Split(' ');
                idGettingPatientDoctor = int.Parse(arrDocTakingID[0].ToString());
            }
        }

        int GetNumOfDoctors()
        {
            int numOfDocs = 0;
            connection.Open();
            SqlCommand cmd = new SqlCommand("select count(id) from dbo.Doctors", connection);
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
                SqlCommand cmd = new SqlCommand("select surname, name from dbo.Doctors where id =@ID", connection);
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

        int GetNumOfPatients()
        {
            int numOfDocs = 0;
            connection.Open();
            SqlCommand cmd = new SqlCommand("(select count(id) from dbo.Cards)", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                numOfDocs = int.Parse(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            return numOfDocs;
        }

        void ShowPatientsData(int docID)
        {
            string query = "select dbo.Cards.id as [ID пацієнта], dbo.Cards.surname as [Прізвище], " +
                "dbo.Cards.name as [Ім'я], dbo.Cards.secname as [По батькові], " +
                "dbo.Cards.address as [Адреса], sex as [Стать], age as [Вік], " +
                "insurance_policy_number as [Номер полісу], date_of_card_creation as [Дата створення картки] " +
                "from dbo.Cards inner join " +
                "dbo.Visits on dbo.Visits.card_id = dbo.Cards.id inner " +
                "join dbo.Doctors on dbo.Visits.doctor_id = dbo.Doctors.id " +
                $"where dbo.Doctors.id = {docID} order by dbo.Cards.id asc";
            GetAndShowData(query);
        }

        string[] GetPatientsData(int docID)
        {
            int numOfDirections = GetNumOfPatients() + 1;
            string[] line = new string[numOfDirections];
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select dbo.Cards.id, dbo.Cards.name, dbo.Cards.surname, dbo.Cards.secname " +
                "from dbo.Cards inner join dbo.Visits on dbo.Visits.card_id = dbo.Cards.id inner " +
                "join dbo.Doctors on dbo.Visits.doctor_id = dbo.Doctors.id where dbo.Doctors.id = @ID order by dbo.Cards.id asc", connection);
            cmd.Parameters.AddWithValue("@ID", docID);
            SqlDataReader da = cmd.ExecuteReader();
            int i = 0;
            while (da.Read())
            {
                string tmpLine = da.GetValue(0).ToString().Trim() + " " + da.GetValue(1).ToString().Trim() + " " +
                                 da.GetValue(2).ToString().Trim() + " " + da.GetValue(3).ToString().Trim();
                if (Array.IndexOf(line, tmpLine) == -1)
                {
                    line[i] += da.GetValue(0).ToString().Trim() + " ";
                    line[i] += da.GetValue(1).ToString().Trim() + " ";
                    line[i] += da.GetValue(2).ToString().Trim() + " ";
                    line[i] += da.GetValue(3).ToString().Trim();
                }
                i++;
            }
            da.Close();
            connection.Close();
            return line;
        }

        void UpdateComboboxes()
        {
            string[] dataDoctors = GetDoctorsData();
            for (int i = 0; i < dataDoctors.Length; i++)
            {
                if (dataDoctors[i] != null)
                {
                    CB_DoctorToDelete_1.Items.Add(dataDoctors[i]);
                }
            }

            if (CB_DoctorToDelete_1.SelectedItem != null)
            {
                string[] dataPatients = GetPatientsData(int.Parse(CB_DoctorToDelete_1.SelectedItem.ToString().Split(' ')[0]));
                CB_PatientToTransfer_2.Items.Clear(); ;
                for (int i = 0; i < dataPatients.Length; i++)
                {
                    if (dataPatients[i] != null)
                    {
                        CB_PatientToTransfer_2.Items.Add(dataPatients[i]);
                    }
                }
            }

            if (CB_DoctorToDelete_1.SelectedItem != null)
            {
                CB_TakingDoctor_3.Items.Clear();
                for (int i = 0; i < dataDoctors.Length; i++)
                {
                    if (dataDoctors[i] != null)
                    {
                        string[] arrLine = dataDoctors[i].Split(' ');
                        if (int.Parse(arrLine[0]) != idDocToDelete)
                        {
                            CB_TakingDoctor_3.Items.Add(dataDoctors[i]);
                        }
                    }
                }
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

        void GetPatientVisitsData(int index = -1)
        {
            if (index == -1)
            {
                string query = "select id as [№], surname as [Прізвище], name as [Ім'я], secname as [По батькові], " +
                "days_of_reception as [Дні прийому], time_start as [Час початку прийому], " +
                "time_end as [Час закінчення прийому], cabinet as [Кабінет] from dbo.Doctors";
                GetAndShowData(query);
            }
            else
            {
                string query = "select id as [№], surname as [Прізвище], name as [Ім'я], secname as [По батькові], " +
                "days_of_reception as [Дні прийому], time_start as [Час початку прийому], " +
               $"time_end as [Час закінчення прийому], cabinet as [Кабінет] from dbo.Doctors where dbo.Doctors.id = {index}";
                GetAndShowData(query);  
            }
        }
    }
}
