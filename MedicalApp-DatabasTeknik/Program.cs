using System;
using Npgsql;
namespace MedicalApp_DatabasTeknik
{
    internal class Program
    {
        public void MainMenu()
        {
            Console.WriteLine("Welcome to the Medical App");
            Console.WriteLine("Login as:");
            Console.WriteLine("1. Patient");
            Console.WriteLine("2. Doctor");
            Console.WriteLine("3. Administrator");
            Console.WriteLine("4. Exit");
        }

        public void FillInID()
        {
            string id = Console.ReadLine();
            Console.WriteLine("Fill in your ID: ");
            if (id == "")
            {
                Console.WriteLine("ID cannot be empty. Please try again.");
                FillInID(); // Recursively call the method until a valid ID is entered
            }
            // Code to handle ID input and validation
        }

        public void FillInPassword()
        {
            string password = Console.ReadLine();
            Console.WriteLine("Fill in your Password: ");
            if (password == "")
            {
                Console.WriteLine("Password cannot be empty. Please try again.");
                FillInPassword(); // Recursively call the method until a valid password is entered
            }
            // Code to handle password input and validation
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

        public void PatientPersonalInfoMenu()
        {
            Console.WriteLine("Viewing Personal Information...");
            // Code to retrieve and display personal information from the database
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

        public void FillInPatientFirstName()
        {
            while (true)
            {
                Console.WriteLine("Fill in first name: ");
                string firstName = Console.ReadLine();

                if (string.IsNullOrEmpty(firstName))
                {
                    Console.WriteLine("Name cannot be empty.");
                }
                else if (firstName.Any(char.IsDigit))
                {
                    Console.WriteLine("No numbers allowed.");
                }
                else
                {
                    Console.WriteLine("Name updated successfully.");
                    return;
                }
            }
        }

        public void FillInPatientLastName()
        {
            Console.WriteLine("Fill in your Last Name: ");
            string lastName = Console.ReadLine();
            while (true)
            {
                if (lastName == "")
                {
                    Console.WriteLine("Last Name cannot be empty. Please try again.");
                    FillInPatientLastName(); // Recursively call the method until a valid last name is entered
                }
                else if (lastName.Any(char.IsDigit))
                {
                    Console.WriteLine("Last Name cannot contain numbers. Please try again.");
                    FillInPatientLastName(); // Recursively call the method until a valid last name is entered
                }
                else
                {
                    Console.WriteLine("Last Name updated successfully.");
                    return ;
                }
            }

            // Code to handle last name input and validation
        }

        public void FillInPatientGender()
        {
            Console.WriteLine("Fill in your gender: ");
            string gender = Console.ReadLine();
            while (true)
            {
                if (gender == "")
                {
                    Console.WriteLine("Gender cannot be empty. Please try again.");
                    FillInPatientGender();
                }
                else if (gender.Any(char.IsDigit))
                {
                    Console.WriteLine("Last Name cannot contain numbers. Please try again.");
                    FillInPatientGender();
                }
                else
                {
                    Console.WriteLine("Gender updated successfully.");
                    return;
                }
                // Code to handle gender input and validation
            } 
        }

        public void FillInPatientAdress()
        {
            Console.WriteLine("Fill in your Adress: ");
            string adress = Console.ReadLine();
            while (true)
            {
                if (adress == "")
                {
                    Console.WriteLine("Adress cannot be empty. Please try again.");
                    FillInPatientAdress();
                }
                else
                {
                    Console.WriteLine("Adress updated successfully.");
                    return;
                }
                // Code to handle adress input and validation
            } 
        }

        public void FillInPatientPhoneNumber()
        {
            Console.WriteLine("Fill in your Phone Number: ");
            string phoneNumber = Console.ReadLine();
            while (true)
            {
                if (phoneNumber == "")
                {
                    Console.WriteLine("Phone Number cannot be empty. Please try again.");
                    FillInPatientPhoneNumber();
                }
                else if (!phoneNumber.All(char.IsDigit))
                {
                    Console.WriteLine("Phone Number can only contain numbers. Please try again.");
                    FillInPatientPhoneNumber();
                }
                else
                {
                    Console.WriteLine("Phone Number updated successfully.");
                    return;
                }
                // Code to handle phone number input and validation
            } 
        }

        public void FillInPatientDateOfBirth()
        {
            Console.WriteLine("Fill in your Date of Birth (YYYY-MM-DD): ");
            string dateOfBirth = Console.ReadLine();
            while (true)
            {
                if (dateOfBirth == "")
                {
                    Console.WriteLine("Date of Birth cannot be empty. Please try again.");
                    FillInPatientDateOfBirth();
                }
                else if (!DateTime.TryParse(dateOfBirth, out _))
                {
                    Console.WriteLine("Invalid Date of Birth format. Please use YYYY-MM-DD. Try again.");
                    FillInPatientDateOfBirth();
                }
                else
                {
                    Console.WriteLine("Date of Birth updated successfully.");
                    UpdatePatientPersonalInfoMenu(); // Return to the update menu after successful update
                }
                // Code to handle date of birth input and validation
            } 
        }

        public void FillInPatientPersonalInfoHandler(string infoType)
        {
            if (infoType == "1") { FillInPatientFirstName(); }
            else if (infoType == "2") { FillInPatientLastName(); }
            else if (infoType == "3") { FillInPatientGender(); }
            else if (infoType == "4") { FillInPatientAdress(); }
            else if (infoType == "5") { FillInPatientPhoneNumber(); }
            else if (infoType == "6") { FillInPatientDateOfBirth(); }
            else if (infoType == "7") { PatientPersonalInfoMenu(); }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                PatientInformationHandler();
            }
        }

        public void BookAppointment()
        {
            Console.WriteLine("Booking an appointment");
            Console.WriteLine("Choose a day: ");
            string day = Console.ReadLine();
            if (day == "")
            {
                Console.WriteLine("Day cannot be empty. Please try again.");
                BookAppointment();
            }
            else
            {
                Console.WriteLine("Day selected: " + day);
                // Code to handle appointment booking
            }

            Console.WriteLine("Choose a time: ");
            string time = Console.ReadLine();
            if (time == "")
            {
                Console.WriteLine("Time cannot be empty. Please try again.");
                BookAppointment();
            }
            else
            {
                Console.WriteLine("Time selected: " + time);
                Console.WriteLine("Appointment id: " + new Random().Next(1000, 9999)); // Generate a random appointment ID for demonstration
                // Code to handle appointment booking
            }
            Console.WriteLine("Appointment booked successfully for " + day + " at " + time);
        }

        public void ViewMedicalRecord()
        {
            Console.WriteLine("Viewing Medical Record...");
            // Code to retrieve and display medical record from the database

            Console.WriteLine("Record id: ");
            Console.WriteLine("Patient id: ");
            Console.WriteLine("Appointment id: ");
            Console.WriteLine("You have an appointment at: ");
            Console.WriteLine("Diagnosis: ");
            Console.WriteLine("Description: ");
        }

        public void PatientInformationHandler()
        {
            while (true)
            {
                Console.WriteLine("\n");
                PatientMainMenu();
                string patientChoice = Console.ReadLine();

                if (patientChoice == "1")
                {
                    Console.WriteLine("\n");
                    PatientPersonalInfoMenu();
                    string personalInfoChoice = Console.ReadLine();

                    if (personalInfoChoice == "1")
                    {
                        Console.WriteLine("\n");
                        UpdatePatientPersonalInfoMenu();
                        string updateChoice = Console.ReadLine();
                        FillInPatientPersonalInfoHandler(updateChoice);
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
                    BookAppointment();
                }
                else if (patientChoice == "3")
                {
                    Console.WriteLine("\n");
                    ViewMedicalRecord();
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

        public void ViewAppointments()
        {
            Console.WriteLine("Appointments: ");
            Console.WriteLine("Monday: " + new Random().Next(1,10) + "appointments.");
            Console.WriteLine("Timeslots: ");
            Console.WriteLine("\nTuesday: " + new Random().Next(1,10) + "appointments.");
            Console.WriteLine("Timeslots: ");
            Console.WriteLine("\nWednesday: " + new Random().Next(1,10) + "appointments.");
            Console.WriteLine("Timeslots: ");
            Console.WriteLine("\nThursday: " + new Random().Next(1,10) + "appointments.");
            Console.WriteLine("Timeslots: ");
            Console.WriteLine("\nFriday: " + new Random().Next(1,10) + "appointments.");
            Console.WriteLine("Timeslots: ");
            Console.WriteLine("\nSaturday: " + new Random().Next(1,10) + "appointments.");
            Console.WriteLine("Timeslots: ");
            Console.WriteLine("\nSunday: " + new Random().Next(1,10) + "appointments.");
            Console.WriteLine("Timeslots: ");
        }

        public void UpdateTimeTable()
        {
            Console.WriteLine("Update Time Table:");
            Console.WriteLine("Choose a day to update: ");
            string day = Console.ReadLine();
            int availableTimeslots = new Random().Next(1, 5);  
            Console.WriteLine("Available timeslots: " + availableTimeslots);
            Console.WriteLine("Choose a timeslot to update: ");
            string timeslot = Console.ReadLine();
            if (timeslot == "" || !int.TryParse(timeslot, out int timeslotNumber) || timeslotNumber < 1 || timeslotNumber > availableTimeslots)
            {
                Console.WriteLine("Invalid timeslot, try again.");
                UpdateTimeTable();
            }
            else
            {
                Console.WriteLine("Timeslot " + timeslot + " updated successfully for " + day);
                // Code to update the time table in the database
            }
        }

        public void UpdateMedicalRecord()
        {
            Console.WriteLine("Updating Medical Record");
            Console.WriteLine("Enter Patient ID: ");
            string patientId = Console.ReadLine();
            ViewMedicalRecord();
            UpdatePatientPersonalInfoMenu();
            string updateChoice = Console.ReadLine();   
            FillInPatientPersonalInfoHandler(updateChoice);
        }

        public void DoctorInformationHandler()
        {
            while (true)
            {
                Console.WriteLine("\n");
                DoctorMainMenu();
                string doctorChoice = Console.ReadLine();
                if (doctorChoice == "1")
                {
                    Console.WriteLine("\n");
                    ViewAppointments();
                }
                else if (doctorChoice == "2")
                {
                    Console.WriteLine("\n");
                    UpdateTimeTable();
                }
                else if (doctorChoice == "3")
                {
                    Console.WriteLine("\n");
                    UpdateMedicalRecord();
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
            Console.WriteLine("4. Go Back");
        }

        public void AddSpecialization()
        {
            Console.WriteLine("Adding Specialization:");
            Console.WriteLine("Enter Specialization: ");
            string specialization = Console.ReadLine();
            if (specialization == "")
            {
                Console.WriteLine("Doctor ID and Specialization cannot be empty. Please try again.");
                AddSpecialization();
            }
            else
            {
                Console.WriteLine("Specialization " + specialization + " added successfully. Its ID is: ");
                // Code to add specialization to the doctor in the database
            }
        }

        public void UpdateDoctorInformationMenu()
        {
            Console.WriteLine("Updating Doctor Information:");
            Console.WriteLine("Choose the following to Update: ");
            Console.WriteLine("1. Full Name");
            Console.WriteLine("2. Phone Number");
            Console.WriteLine("3. Specialization");
        }

        public void UpdateDoctorFullName()
        {
            Console.WriteLine("Fill in Doctor's Full Name: ");
            string fullName = Console.ReadLine();
            if (fullName == "")
            {
                Console.WriteLine("Full Name cannot be empty. Please try again.");
                UpdateDoctorFullName();
            }
            else
            {
                Console.WriteLine("Doctor's Full Name updated successfully to: " + fullName);
                // Code to update doctor's full name in the database
            }
        }

        public void UpdateDoctorPhoneNumber()
        {
            Console.WriteLine("Fill in Doctor's Phone Number: ");
            string phoneNumber = Console.ReadLine();
            if (phoneNumber == "")
            {
                Console.WriteLine("Phone Number cannot be empty. Please try again.");
                UpdateDoctorPhoneNumber();
            }
            else if (!phoneNumber.All(char.IsDigit))
            {
                Console.WriteLine("Phone Number can only contain numbers. Please try again.");
                UpdateDoctorPhoneNumber();
            }
            else
            {
                Console.WriteLine("Doctor's Phone Number updated successfully to: " + phoneNumber);
                // Code to update doctor's phone number in the database
            }
        }

        public void UpdateDoctorSpecialization()
        {
            Console.WriteLine("Fill in Doctor's Specialization: ");
            string specialization = Console.ReadLine();
            if (specialization == "")
            {
                Console.WriteLine("Specialization cannot be empty. Please try again.");
                UpdateDoctorSpecialization();
            }
            else
            {
                Console.WriteLine("Doctor's Specialization updated successfully to: " + specialization);
                // Code to update doctor's specialization in the database
            }
        }

        public void FillInDoctorInformationHandler(string infoType)
        {
            if (infoType == "1") { UpdateDoctorFullName(); }
            else if (infoType == "2") { UpdateDoctorPhoneNumber(); }
            else if (infoType == "3") { UpdateDoctorSpecialization(); }
            else if (infoType == "4") { AdminManageDoctorsMenu(); }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                UpdateDoctorInformationMenu();
            }
        }

        public void RemoveDoctor()
        {
            Console.WriteLine("Removing Doctor:");
            Console.WriteLine("Enter Doctor ID: ");
            string doctorId = Console.ReadLine();
            if (doctorId == "")
            {
                Console.WriteLine("Doctor ID cannot be empty. Please try again.");
                RemoveDoctor();
            }
            else
            {
                Console.WriteLine("Doctor with ID " + doctorId + " removed successfully.");
                // Code to remove doctor from the database
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
            Console.WriteLine("Patient List:");
            Random patientInList = new Random();
            int numberOfPatients = patientInList.Next(1, 10);
            Console.WriteLine("There are " + numberOfPatients + "patients");
            for (int i = 1; i <= numberOfPatients; i++)
            {
                Console.WriteLine("Patient ID: " + new Random().Next(1000, 9999) + ", Name: Patient " + i);
            }
        }

        public void ViewUpcomingAppointments()
        {
            Console.WriteLine("Upcoming Appointments:");
            Random appointmentInList = new Random();
            int numberOfAppointments = appointmentInList.Next(1, 10);
            Console.WriteLine("There are " + numberOfAppointments + " upcoming appointments.");
            for (int i = 1; i <= numberOfAppointments; i++)
            {
                Console.WriteLine("Appointment ID: " + new Random().Next(1000, 9999) + ", Patient ID: " + new Random().Next(1000, 9999) + ", Date: " + DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
            }
        }

        public void ViewMedicalRecords()
        {
            Console.WriteLine("Viewing Medical Records:");
            Console.WriteLine("Enter Patient ID: ");
            string patientId = Console.ReadLine();
            Console.WriteLine("Medical Records for Patient ID: " + patientId);
        }

        public void AdminManageDoctorsHandler(string doctorChoice)
        {
            if (doctorChoice == "1") { AddSpecialization(); }
            else if (doctorChoice == "2") 
            { 
                UpdateDoctorInformationMenu(); 
                string updateChoice = Console.ReadLine();
                FillInDoctorInformationHandler(updateChoice);
            }
            else if (doctorChoice == "3") { RemoveDoctor(); }
            else if (doctorChoice == "4") { AdminMainMenu(); }
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
            else if (patientChoice == "3") { ViewMedicalRecords(); }
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
            //string connString = "Host=postgres.mau.se;Username=an5964;Password=vzsjll4k;Database=an5964;Port=55432";
            //NpgsqlConnection conn = new NpgsqlConnection(connString);
            //conn.Open();

            //Console.WriteLine("Connected to database!");

            //conn.Close();

            while (true)
            {
                Console.WriteLine("\n");
                MainMenu();
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\n");
                    PatientInformationHandler();
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n");
                    DoctorInformationHandler();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("\n");
                    AdminInformationHandler();
                }
                else if (choice == "4")
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
