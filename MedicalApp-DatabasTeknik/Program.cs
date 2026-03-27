using System;
using Npgsql;
namespace MedicalApp_DatabasTeknik
{
    internal class Program
    {
        public NpgsqlConnection GetUserConnection() 
        {
            string connString = $"Host=postgres.mau.se;Username=an5964;Password=vzsjll4k;Database=an5964;Port=55432";

            return new NpgsqlConnection(connString);
        }

        public string PatientLogin()
        {
            Console.Write("Patient ID: ");
            string id = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "SELECT patient_id FROM patient WHERE patient_id = @id AND patient_password = @password";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("Login successful!");
                            return reader["patient_id"].ToString();
                        }
                        else
                        {
                            Console.WriteLine("Invalid login.");
                            return null;
                        }
                    }
                }
            }
        }

        public string RetrivePatientIdFromDatabase(string id)
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "SELECT patient_id FROM patient WHERE patient_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["patient_id"].ToString();
                        }
                        else
                        {
                            Console.WriteLine("Patient ID not found.");
                            return null;
                        }
                    }
                }
            }
        }

        public string DoctorLogin()
        {
            Console.Write("Doctor ID: ");
            string id = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "SELECT * FROM doctor WHERE doctor_id = @id AND doctor_password = @password";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("Login successful!");
                            return reader["doctor_id"].ToString(); ;
                        }
                        else
                        {
                            Console.WriteLine("Invalid login.");
                            return null;
                        }
                    }
                }
            }
        }

        public bool AdminLogin()
        {
            Console.Write("Admin ID: ");
            string id = Console.ReadLine();

            Console.Write("Admin Name: ");
            string name = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = @"SELECT * 
                         FROM administrator 
                         WHERE administrator_id = @id 
                         AND administrator_name = @name 
                         AND administrator_password = @password";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    if (!int.TryParse(id, out int adminId))
                    {
                        Console.WriteLine("Invalid ID format.");
                        return false;
                    }

                    cmd.Parameters.AddWithValue("id", adminId);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("Login successful!");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid login.");
                            return false;
                        }
                    }
                }
            }
        }
        public void MainMenu()
        {
            Console.WriteLine("Welcome to the Medical App");
            Console.WriteLine("Login as:");
            Console.WriteLine("1. Patient");
            Console.WriteLine("2. Doctor");
            Console.WriteLine("3. Administrator");
            Console.WriteLine("4. Register a patient");
            Console.WriteLine("5. Exit");
        }

        // Patient 
        public void PatientMainMenu()
        {
            Console.WriteLine("Patient Main Menu");
            Console.WriteLine("1. View Personal Information");
            Console.WriteLine("2. Book Appointment");
            Console.WriteLine("3. View Medical Record");
            Console.WriteLine("4. Logout");
        }

        public void ViewPatientPersonalInfo(string patientID)
        {
            Console.WriteLine("Personal Information:");

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = @"SELECT patient_id, first_name, last_name, gender, address, phone, birthdate 
                         FROM patient 
                         WHERE patient_id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", patientID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["patient_id"]}");
                            Console.WriteLine($"Name: {reader["first_name"]} {reader["last_name"]}");
                            Console.WriteLine($"Gender: {reader["gender"]}");
                            Console.WriteLine($"Address: {reader["address"]}");
                            Console.WriteLine($"Phone: {reader["phone"]}");
                            Console.WriteLine($"Date of Birth: {reader["birthdate"]}");
                        }
                        else
                        {
                            Console.WriteLine("Patient not found.");
                        }
                    }
                }
            }
        }

        public void ViewPatientAppointments(string patientID)
        {
            Console.WriteLine("Your Appointments:");
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = @"SELECT a.appointment_id, a.appointment_date, a.appointment_time, d.full_name AS doctor_name
                         FROM appointment a
                         JOIN doctor d ON a.doctor_id = d.doctor_id
                         WHERE a.patient_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", patientID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Appointment ID: {reader["appointment_id"]}, Date: {reader["appointment_date"]}, Time: {reader["appointment_time"]}, Doctor: {reader["doctor_name"]}");
                        }
                    }
                }
            }
        }

        public void ViewPatientMedicalRecord(string patientID)
        {
            Console.WriteLine($"Medical Record for patient: {patientID}");
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = @"SELECT record_id, appointment_id, diagnosis, prescription, description 
                         FROM medical_record 
                         WHERE patient_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", patientID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Record ID: {reader["record_id"]}");
                            Console.WriteLine($"Appointment ID: {reader["appointment_id"]}");
                            Console.WriteLine($"Diagnosis: {reader["diagnosis"]}");
                            Console.WriteLine($"Prescription: {reader["prescription"]}");
                            Console.WriteLine($"Description: {reader["description"]}");
                        }
                    }
                }
            }
        }
        public void PatientPersonalInfoMenu(string patientID)
        {
            ViewPatientPersonalInfo(patientID);

            Console.WriteLine("\n");
            Console.WriteLine("Choose the following to: ");
            Console.WriteLine("1. Update Personal Information");
            Console.WriteLine("2. Back to Main Menu"); // To return to the patient main menu
        }

        public void UpdatePatientPersonalInfoMenu()
        {
            Console.WriteLine("Updating Personal Information...");
            // Code to update personal information in the database
            Console.WriteLine("Choose the following to Update: ");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.WriteLine("3. Gender");
            Console.WriteLine("4. Adress");
            Console.WriteLine("5. Phone Number");
            Console.WriteLine("6. Date of Birth");
            Console.WriteLine("7. Back to Personal Information Menu");
        }

        public void FillInPatientFirstName(string patientID)
        {
            Console.WriteLine("Fill in first name: ");
            string firstName = Console.ReadLine();

            if (string.IsNullOrEmpty(firstName) || firstName.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "UPDATE patient SET first_name = @first WHERE patient_id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("first", firstName);
                    cmd.Parameters.AddWithValue("id", patientID);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("First name updated");
        }

        public void FillInPatientLastName(string patientID)
        {
           Console.WriteLine("Fill in last name: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrEmpty(lastName)|| lastName.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "UPDATE patient SET last_name = @last WHERE patient_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("last", lastName);
                    cmd.Parameters.AddWithValue("id", patientID);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Last name updated");
        }

        public void FillInPatientGender(string patientID)
        {
            Console.WriteLine("Fill in your gender: ");
            string gender = Console.ReadLine();
            if (string.IsNullOrEmpty(gender) || gender.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid gender.");
                return;
            }

            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "UPDATE patient SET gender = @gen WHERE patient_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("gen", gender);
                    cmd.Parameters.AddWithValue("id", patientID);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Gender updated");
        }

        public void FillInPatientAdress(string patientID)
        {
            Console.WriteLine("Fill in your Adress: ");
            string adress = Console.ReadLine();

            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "UPDATE patient SET address = @adr WHERE patient_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("adr", adress);
                    cmd.Parameters.AddWithValue("id", patientID);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Adress updated");
        }

        public void FillInPatientPhoneNumber(string patientID)
        {
            Console.WriteLine("Fill in your Phone Number: ");
            string phoneNumber = Console.ReadLine();
            
            if (string.IsNullOrEmpty(phoneNumber))
            {
                Console.WriteLine("Phone number cannot be empty. Please try again.");
                return;
            }

            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "UPDATE patient SET phone = @pho WHERE patient_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("pho", phoneNumber);
                    cmd.Parameters.AddWithValue("id", patientID);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Phone number updated");
        }

        public void FillInPatientDateOfBirth(string patientID)
        {
            Console.WriteLine("Fill in your Date of Birth (YYYY-MM-DD): ");
            string input = Console.ReadLine();

            if (!DateTime.TryParse(input, out DateTime birthDate))
            {
                Console.WriteLine("Invalid date format. Use YYYY-MM-DD.");
                return;
            }

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "UPDATE patient SET birthdate = @birth WHERE patient_id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("birth", birthDate); 
                    cmd.Parameters.AddWithValue("id", patientID);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Date of birth updated");
        }

        public void FillInPatientPersonalInfoHandler(string infoType, string patientID)
        {
            if (infoType == "1") { FillInPatientFirstName(patientID); }
            else if (infoType == "2") { FillInPatientLastName(patientID); }
            else if (infoType == "3") { FillInPatientGender(patientID); }
            else if (infoType == "4") { FillInPatientAdress(patientID); }
            else if (infoType == "5") { FillInPatientPhoneNumber(patientID); }
            else if (infoType == "6") { FillInPatientDateOfBirth(patientID); }
            else if (infoType == "7") { PatientPersonalInfoMenu(patientID); }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        public void ShowDoctors()
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "SELECT doctor_id, full_name, specialization_id FROM doctor";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Available Doctors:");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["doctor_id"]}: {reader["full_name"]} - {reader["specialization_id"]}");
                    }
                }
            }

            ShowSpecializations();
        }

        public void DeleteDoctor(string doctorID)
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "DELETE FROM doctor WHERE doctor_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", doctorID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Doctor removed successfully.");
                    else
                        Console.WriteLine("Doctor not found.");
                }
            }
        }

        public void ShowSpecializations()
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "SELECT specialization_id, spec_name, visit_cost FROM specialization";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Available Specializations:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["specialization_id"]}: {reader["spec_name"]}");
                        Console.WriteLine($"Visit Cost: {reader["visit_cost"]}");
                        Console.WriteLine("\n");
                    }
                }
            }
        }

        public void BookAppointment(string patientID)
        {
            ViewPatientAppointments(patientID);

            string validPatient = RetrivePatientIdFromDatabase(patientID);
            if (validPatient == null)
            {
                Console.WriteLine("Invalid patient ID.");
                return;
            }

            ShowDoctors();
            Console.Write("Doctor ID: ");
            string doctorId = Console.ReadLine();

            if (!DoctorExists(doctorId))
            {
                Console.WriteLine("Doctor does not exist!");
                return;
            }

            ShowAvailableTimeSlots(doctorId);

            Console.Write("Choose Day: ");
            string day = Console.ReadLine();

            Console.Write("Choose Time (HH:MM): ");
            TimeSpan time = TimeSpan.Parse(Console.ReadLine());

            using (var conn = GetUserConnection())
            {
                conn.Open();

                // Check if slot exists and is available
                string checkQuery = @"SELECT is_available 
                             FROM doctor_schedule
                             WHERE doctor_id = @doc 
                             AND day_of_week = @day 
                             AND time_slot = @time";

                using (var checkCmd = new NpgsqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("doc", doctorId);
                    checkCmd.Parameters.AddWithValue("day", day);
                    checkCmd.Parameters.AddWithValue("time", time);

                    var result = checkCmd.ExecuteScalar();

                    if (result == null || !(bool)result)
                    {
                        Console.WriteLine("Slot not available!");
                        return;
                    }
                }

                // Insert appointment
                string insertQuery = @"INSERT INTO appointment 
            (patient_id, doctor_id, appointment_date, appointment_time)
            VALUES (@pid, @did, @day, @time)"; // @day is not correct

                using (var cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("pid", patientID);
                    cmd.Parameters.AddWithValue("did", doctorId);
                    cmd.Parameters.AddWithValue("time", time);
                    cmd.Parameters.AddWithValue("day", day);

                    cmd.ExecuteNonQuery();
                }

                // Mark slot as unavailable
                string updateQuery = @"UPDATE doctor_schedule
                               SET is_available = FALSE
                               WHERE doctor_id = @doc
                               AND day_of_week = @day
                               AND time_slot = @time";

                using (var updateCmd = new NpgsqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("doc", doctorId);
                    updateCmd.Parameters.AddWithValue("day", day);
                    updateCmd.Parameters.AddWithValue("time", time);

                    updateCmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Appointment is booked");
        }

        public void CreateMedicalRecord(string doctorId)
        {
            Console.WriteLine("Create Medical Record");

            Console.Write("Enter Patient ID: ");
            string patientId = Console.ReadLine();

            if (RetrivePatientIdFromDatabase(patientId) == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            ViewSpecificPatientAppointments(patientId);

            Console.Write("Enter Appointment ID: ");
            string appointmentId = Console.ReadLine();

            Console.Write("Diagnosis: ");
            string diagnosis = Console.ReadLine();

            Console.Write("Prescription: ");
            string prescription = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = @"INSERT INTO medical_record
                (patient_id, appointment_id, diagnosis, prescription, description)
                VALUES (@pid, @aid, @diag, @pres, @desc)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("pid", patientId);
                    cmd.Parameters.AddWithValue("aid", int.Parse(appointmentId));
                    cmd.Parameters.AddWithValue("diag", diagnosis);
                    cmd.Parameters.AddWithValue("pres", prescription);
                    cmd.Parameters.AddWithValue("desc", description);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Medical record created successfully!");
        }

        public void ViewMedicalRecord(string patientID)
        {
            Console.WriteLine($"Medical Record for patient: {patientID}");
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = @"SELECT record_id, appointment_id, diagnosis, prescription, description 
                         FROM medical_record 
                         WHERE patient_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", patientID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Record ID: {reader["record_id"]}");
                            Console.WriteLine($"Appointment ID: {reader["appointment_id"]}");
                            Console.WriteLine($"Diagnosis: {reader["diagnosis"]}");
                            Console.WriteLine($"Prescription: {reader["prescription"]}");
                            Console.WriteLine($"Description: {reader["description"]}");
                        }
                    }
                }
            }
        }

        public void RegisterAPatient()
        {
            Console.WriteLine("Register as a patient:");
            Console.WriteLine("---------------------");

            Console.Write("ID number: ");
            string idNumber = Console.ReadLine();

            Console.Write("First name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Gender: ");
            string gender = Console.ReadLine();

            Console.Write("Phone number: ");
            string phone = Console.ReadLine();

            Console.Write("Date of birth (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            DateTime registrationDate = DateTime.Now;

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = @"INSERT INTO patient 
                (patient_id, first_name, last_name, address, gender, phone, birthdate, registration_date, patient_password)
                VALUES 
                (@id, @first, @last, @address, @gender, @phone, @birth, @regDate, @pass)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", idNumber);
                    cmd.Parameters.AddWithValue("first", firstName);
                    cmd.Parameters.AddWithValue("last", lastName);
                    cmd.Parameters.AddWithValue("address", address);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.Parameters.AddWithValue("phone", phone);
                    cmd.Parameters.AddWithValue("birth", birthDate);
                    cmd.Parameters.AddWithValue("regDate", registrationDate);
                    cmd.Parameters.AddWithValue("pass", pass);

                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("Patient registered successfully!");
            }
        }

        public void PatientInformationHandler(string patientID)
        {
            while (true)
            {
                Console.WriteLine("\n");
                PatientMainMenu();
                string patientChoice = Console.ReadLine();

                if (patientChoice == "1")
                {
                    Console.WriteLine("\n");
                    PatientPersonalInfoMenu(patientID);
                    string personalInfoChoice = Console.ReadLine();

                    if (personalInfoChoice == "1")
                    {
                        Console.WriteLine("\n");
                        UpdatePatientPersonalInfoMenu();
                        string updateChoice = Console.ReadLine();
                        FillInPatientPersonalInfoHandler(updateChoice, patientID);
                    }
                    else if (personalInfoChoice == "2")
                    {
                        Console.WriteLine("\n");
                        continue; // go back to patient menu
                    }
                }
                else if (patientChoice == "2")
                {
                    Console.WriteLine("\n");
                    BookAppointment(patientID);
                }
                else if (patientChoice == "3")
                {
                    Console.WriteLine("\n");
                    ViewMedicalRecord(patientID);
                    continue;
                }

                else if (patientChoice == "4")
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Logging out...");
                    return; 
                }
                else
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        // Doctor
        public void DoctorMainMenu()
        {
            Console.WriteLine("Doctor Main Menu");
            Console.WriteLine("1. View Appointments");
            Console.WriteLine("2. Update Time Table");
            Console.WriteLine("3. Update Medical Record");
            Console.WriteLine("4. Logout");
        }

        public void GenerateScheduleForDoctor(string doctorId)
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            List<TimeSpan> times = new List<TimeSpan>
            {
                new TimeSpan(9, 0, 0),
                new TimeSpan(9, 30, 0),
                new TimeSpan(10, 0, 0),
                new TimeSpan(10, 30, 0)
            };

            using (var conn = GetUserConnection())
            {
                conn.Open();

                foreach (var day in days)
                {
                    foreach (var time in times)
                    {
                        string query = @"INSERT INTO doctor_schedule 
                        (doctor_id, day_of_week, time_slot)
                        VALUES (@doc, @day, @time)
                        ON CONFLICT DO NOTHING"; // prevents duplicates

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("doc", doctorId);
                            cmd.Parameters.AddWithValue("day", day);
                            cmd.Parameters.AddWithValue("time", time);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            Console.WriteLine("Schedule generated for doctor: " + doctorId);
        }

        public void GenerateScheduleForAllDoctors()
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "SELECT doctor_id FROM doctor";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    List<string> doctorIds = new List<string>();

                    while (reader.Read())
                    {
                        doctorIds.Add(reader["doctor_id"].ToString());
                    }

                    reader.Close();

                    foreach (var doctorId in doctorIds)
                    {
                        GenerateScheduleForDoctor(doctorId);
                    }
                }
            }
        }
        public void ShowAvailableTimeSlots(string doctorId)
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = @"SELECT day_of_week, time_slot
                         FROM doctor_schedule
                         WHERE doctor_id = @doc
                         AND is_available = TRUE
                         ORDER BY day_of_week, time_slot";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("doc", doctorId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Available Time Slots:");

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["day_of_week"]} - {reader["time_slot"]}");
                        }
                    }
                }
            }
        }

        public void UpdateTimeSlot(string doctorId)
        {
            Console.WriteLine("Enter day (e.g., Monday): ");
            string day = Console.ReadLine();

            Console.WriteLine("Enter time (HH:MM): ");
            TimeSpan time = TimeSpan.Parse(Console.ReadLine());

            Console.WriteLine("1. Set AVAILABLE");
            Console.WriteLine("2. Set UNAVAILABLE");
            string choice = Console.ReadLine();

            bool newStatus;

            if (choice == "1")
                newStatus = true;
            else if (choice == "2")
                newStatus = false;
            else
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = @"UPDATE doctor_schedule
                         SET is_available = @status
                         WHERE doctor_id = @doc
                         AND day_of_week = @day
                         AND time_slot = @time";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("status", newStatus);
                    cmd.Parameters.AddWithValue("doc", doctorId);
                    cmd.Parameters.AddWithValue("day", day);
                    cmd.Parameters.AddWithValue("time", time);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        if (newStatus)
                            Console.WriteLine("The timeslot is now AVAILABLE");
                        else
                            Console.WriteLine("The timeslot is now UNAVAILABLE");
                    }
                    else
                    {
                        Console.WriteLine("Timeslot not found.");
                    }
                }
            }
        }

        public void UpdateMedicalRecord()
        {
            Console.WriteLine("Updating Medical Record");
            Console.WriteLine("Enter Patient ID: ");
            string patientId = Console.ReadLine();
            ViewMedicalRecord(patientId);
            UpdatePatientPersonalInfoMenu();
            string updateChoice = Console.ReadLine();   
            FillInPatientPersonalInfoHandler(updateChoice, patientId);
        }

        public void DoctorInformationHandler(string doctorID)
        {
            while (true)
            {
                Console.WriteLine("\n");
                DoctorMainMenu();
                string doctorChoice = Console.ReadLine();
                if (doctorChoice == "1")
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter patient ID: ");
                    string patientID = Console.ReadLine();
                    ViewPatientAppointments(patientID);
                }
                else if (doctorChoice == "2")
                {
                    Console.WriteLine("\n");
                    ShowAvailableTimeSlots(doctorID);
                    UpdateTimeSlot(doctorID);
                }
                else if (doctorChoice == "3")
                {
                    Console.WriteLine("\n");
                    CreateMedicalRecord(doctorID);
                }
                else if (doctorChoice == "4")
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Logging out...");
                    return; 
                }
                else
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        public bool DoctorExists(string doctorId)
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "SELECT 1 FROM doctor WHERE doctor_id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", doctorId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.Read(); // true if found
                    }
                }
            }
        }

        // Admin
        public void AdminMainMenu()
        {
            Console.WriteLine("Admin Main Menu");
            Console.WriteLine("1. Manage Doctors");
            Console.WriteLine("2. Manage Patients");
            Console.WriteLine("3. Logout");
        }

        public void AdminManageDoctorsMenu()
        {
            Console.WriteLine("Managing Doctors:");
            Console.WriteLine("1. Add Specialization");
            Console.WriteLine("2. Update Doctor Information");
            Console.WriteLine("3. Remove Doctor");
            Console.WriteLine("4. Register a Doctor");
            Console.WriteLine("5. Go Back");
        }

        public void AddSpecialization()
        {
            ShowSpecializations();
            using (var conn = GetUserConnection())
            {
                conn.Open();
                Console.WriteLine("Enter new specialization name: ");
                string specName = Console.ReadLine();

                Console.WriteLine("Add visit cost: ");
                string visitCostInput = Console.ReadLine();

                if (string.IsNullOrEmpty(specName) || string.IsNullOrEmpty(visitCostInput))
                {
                    Console.WriteLine("Specialization name or visit cost cannot be empty. Please try again.");
                    AddSpecialization();
                    return;
                }
                string query = "INSERT INTO specialization (spec_name, visit_cost) VALUES (@name, @cost)";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", specName);
                    cmd.Parameters.AddWithValue("cost", decimal.Parse(visitCostInput));
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Specialization added successfully: " + specName);
                Console.WriteLine("Visit cost: " + visitCostInput);
            }
        }

        public void ShowDoctorInfoForAdmin()
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "SELECT doctor_id, full_name, specialization_id, phone, doctor_password FROM doctor";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Doctors List:");
                    while (reader.Read())
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine($"{reader["doctor_id"]}: {reader["full_name"]}");
                        Console.WriteLine($"Specialization ID: {reader["specialization_id"]}");
                        Console.WriteLine($"Phone: {reader["phone"]}");
                        Console.WriteLine($"Password: {reader["doctor_password"]}");
                    }
                }
            }
        }

        public void AdminUpdateDoctorFullName(string doctorID)
        {
            Console.WriteLine("Fill in Doctor's Full Name: ");
            string fullName = Console.ReadLine();
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "UPDATE doctor SET full_name = @fullName WHERE doctor_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("fullName", fullName);
                    cmd.Parameters.AddWithValue("id", doctorID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateDoctorInformationMenu()
        {
            ShowDoctorInfoForAdmin();
            Console.WriteLine("\n");
            ShowSpecializations();
            Console.WriteLine("\n");
            Console.WriteLine("Updating Doctor Information:");
            Console.WriteLine("Choose the following to Update: ");
            Console.WriteLine("1. Full Name");
            Console.WriteLine("2. Phone Number");
            Console.WriteLine("3. Specialization");
        }

        public void UpdateDoctorPhoneNumber(string doctorID)
        {
            Console.WriteLine("Fill in Doctor's Phone Number: ");
            string phoneNumber = Console.ReadLine();
           using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "UPDATE doctor SET phone = @phone WHERE doctor_id = @id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("phone", phoneNumber);
                    cmd.Parameters.AddWithValue("id", doctorID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool SpecializationExists(int specId)
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "SELECT 1 FROM specialization WHERE specialization_id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", specId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        public void UpdateDoctorSpecialization(string doctorID)
        {
            Console.WriteLine("Enter Doctor's Specialization ID: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int specializationId))
            {
                Console.WriteLine("Invalid specialization ID. Must be a number.");
                return;
            }

            if (!SpecializationExists(specializationId))
            {
                Console.WriteLine("Specialization ID does not exist.");
                return;
            }

            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "UPDATE doctor SET specialization_id = @specId WHERE doctor_id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("specId", specializationId);
                    cmd.Parameters.AddWithValue("id", doctorID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Doctor's specialization updated successfully.");
                    else
                        Console.WriteLine("Doctor not found.");
                }
            }
        }

        public void RegisterADoctor()
        {
            Console.WriteLine("Register a new doctor:");
            Console.WriteLine("---------------------");
            Console.Write("Doctor ID in this format: AAAxxx where x is number: ");
            string doctorId = Console.ReadLine();
            Console.Write("Full Name: ");
            string fullName = Console.ReadLine();
            Console.Write("Specialization ID: ");
            string specializationId = Console.ReadLine();
            if (!int.TryParse(specializationId, out int specId))
            {
                Console.WriteLine("Invalid specialization ID. Must be a number.");
                return;
            }
             if (!SpecializationExists(specId))
            {
                Console.WriteLine("Specialization ID does not exist.");
                return;
            }

            Console.Write("Phone Number: ");
            string phone = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = @"INSERT INTO doctor 
                (doctor_id, full_name, specialization_id, phone, doctor_password)
                VALUES 
                (@id, @fullName, @specId, @phone, @pass)";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", doctorId);
                    cmd.Parameters.AddWithValue("fullName", fullName);
                    cmd.Parameters.AddWithValue("specId", specId); 
                    cmd.Parameters.AddWithValue("phone", phone);
                    cmd.Parameters.AddWithValue("pass", password);
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Doctor registered successfully!");

                GenerateScheduleForDoctor(doctorId);
            }
        }

        public void FillInDoctorInformationHandler(string infoType, string doctorID)
        {
            if (infoType == "1") { AdminUpdateDoctorFullName(doctorID); }
            else if (infoType == "2") { UpdateDoctorPhoneNumber(doctorID); }
            else if (infoType == "3") { UpdateDoctorSpecialization(doctorID); }
            else if (infoType == "4") { AdminManageDoctorsMenu(); }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                UpdateDoctorInformationMenu();
            }
        }

        public void AdminManagePatientsMenu()
        {
            Console.WriteLine("Managing Patients:");
            Console.WriteLine("1. View Patient List"); // Can also view patient details from this menu
            Console.WriteLine("2. View Upcoming Appointments");
            Console.WriteLine("3. View Medical Records");
            Console.WriteLine("4. Go Back");
        }

        public void ViewPatientList()
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();

                string query = "SELECT patient_id, first_name, last_name, gender, address, phone, birthdate, registration_date, patient_password FROM patient";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Patient List:");

                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["patient_id"]}, Name: {reader["first_name"]} {reader["last_name"]}");
                        Console.WriteLine($"Gender: {reader["gender"]}, Address: {reader["address"]}, Phone: {reader["phone"]}");
                        Console.WriteLine($"Birthdate: {reader["birthdate"]}, Registration Date: {reader["registration_date"]}, Password: {reader["patient_password"]}");
                        Console.WriteLine("\n");
                    }
                }
            }
        }

        public void ViewUpcomingAppointments()
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "SELECT appointment_id, patient_id, doctor_id, appointment_date, appointment_time, booking_date FROM appointment WHERE appointment_date >= CURRENT_DATE ORDER BY appointment_date";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Upcoming Appointments:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Appointment ID: {reader["appointment_id"]}, Patient ID: {reader["patient_id"]}, Doctor ID: {reader["doctor_id"]}, Date: {reader["appointment_date"]}");
                        Console.WriteLine($"Time: {reader["appointment_time"]}, Booking Date: {reader["booking_date"]}");
                        Console.WriteLine("\n");
                    }
                }
            }
        }

        public void ViewSpecificPatientAppointments(string patientID)
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "SELECT appointment_id, doctor_id, appointment_date FROM appointment WHERE patient_id = @pid ORDER BY appointment_date";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("pid", patientID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine($"Appointments for Patient ID: {patientID}");
                        while (reader.Read())
                        {
                            Console.WriteLine(
                                $"Appointment ID: {reader["appointment_id"]}, Doctor ID: {reader["doctor_id"]}, Date: {reader["appointment_date"]}"
                            );
                        }
                    }
                }
            }
        }

        public void ViewMedicalRecordsForAdmin()
        {
            using (var conn = GetUserConnection())
            {
                conn.Open();
                string query = "SELECT record_id, patient_id, diagnosis, description, prescription FROM medical_record";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Medical Records:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Record ID: {reader["record_id"]}, Patient ID: {reader["patient_id"]}, Diagnosis: {reader["diagnosis"]}");
                        Console.WriteLine($"Description: {reader["description"]}");
                        Console.WriteLine($"Prescription: {reader["prescription"]}");
                    }
                }
            }
        }

        public void AdminManageDoctorsHandler(string doctorChoice)
        {
            if (doctorChoice == "1") { AddSpecialization(); }
            else if (doctorChoice == "2") 
            { 
                UpdateDoctorInformationMenu(); 
                string updateChoice = Console.ReadLine();
                Console.WriteLine("Enter Doctor ID to update: ");
                string doctorID = Console.ReadLine();
                FillInDoctorInformationHandler(updateChoice, doctorID);
            }
            else if (doctorChoice == "3") 
            {
                Console.WriteLine("Enter Doctor ID to update: ");
                string doctorID = Console.ReadLine();
                DeleteDoctor(doctorID); 
            }
            else if (doctorChoice == "4") 
            { 
                RegisterADoctor();
            }
            else if (doctorChoice == "5") { return; }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                AdminManageDoctorsMenu();
            }
        }

        public void AdminManagePatientsHandler(string patientChoice)
        {
            if (patientChoice == "1") { ViewPatientList(); }
            else if (patientChoice == "2") { ViewUpcomingAppointments(); }
            else if (patientChoice == "3") { ViewMedicalRecordsForAdmin(); }
            else if (patientChoice == "4") { AdminMainMenu(); }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                AdminManagePatientsMenu();
            }
        }

        public void AdminInformationHandler()
        {
            while (true)
            {
                Console.WriteLine("\n");
                AdminMainMenu();
                string adminChoice = Console.ReadLine();
                if (adminChoice == "1")
                {
                    Console.WriteLine("\n");
                    AdminManageDoctorsMenu();
                    string doctorInfoChoice = Console.ReadLine();
                    AdminManageDoctorsHandler(doctorInfoChoice);
                }

                else if (adminChoice == "2")
                {
                    Console.WriteLine("\n");
                    AdminManagePatientsMenu();
                    string patientInfoChoice = Console.ReadLine();
                    AdminManagePatientsHandler(patientInfoChoice);
                    // Code to handle patient management
                }
                else if (adminChoice == "3")
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Logging out...");
                    return;
                }
                else
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }
        public void AllMenues()
        {
            while (true)
            {
                Console.WriteLine("\n");
                MainMenu();
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    string patientId = PatientLogin();
                    if (patientId != null)
                    {
                        PatientInformationHandler(patientId);
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n");
                    string doctorId = DoctorLogin();
                    if (doctorId != null)
                        DoctorInformationHandler(doctorId);
                }
                else if (choice == "3")
                {
                    Console.WriteLine("\n");
                    if (!AdminLogin())
                    {
                        Console.WriteLine("Invalid admin credentials.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\n");
                        AdminInformationHandler();
                    }
                }
                else if (choice == "4")
                {
                    RegisterAPatient();
                }
                else if (choice == "5")
                {
                    Console.WriteLine("Exiting...");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.AllMenues();
        }
    }
}