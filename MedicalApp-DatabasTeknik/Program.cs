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
            Console.WriteLine("Fill in first name: ");
            string firstName = Console.ReadLine();
            if (firstName == "")
            {
                Console.WriteLine("Name cannot be empty. Please try again.");
                FillInPatientFirstName(); // Recursively call the method until a valid name is entered
            }
            else if (firstName.Any(char.IsDigit))
            {
                Console.WriteLine("Name cannot contain numbers. Please try again.");
                FillInPatientFirstName(); // Recursively call the method until a valid name is entered
            }
            else
            {
                Console.WriteLine("Name updated successfully.");
                UpdatePatientPersonalInfoMenu(); // Return to the update menu after successful update
            }
            // Code to handle name input and validation
        }

        public void FillInPatientLastName()
        {
            Console.WriteLine("Fill in your Last Name: ");
            string lastName = Console.ReadLine();
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
                UpdatePatientPersonalInfoMenu(); // Return to the update menu after successful update
            }

            // Code to handle last name input and validation
        }

        public void FillInPatientGender()
        {
            Console.WriteLine("Fill in your gender: ");
            string gender = Console.ReadLine();
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
                UpdatePatientPersonalInfoMenu(); // Return to the update menu after successful update
            }
            // Code to handle gender input and validation
        }

        public void FillInPatientAdress()
        {
            Console.WriteLine("Fill in your Adress: ");
            string adress = Console.ReadLine();
            if (adress == "")
            {
                Console.WriteLine("Adress cannot be empty. Please try again.");
                FillInPatientAdress();
            }
            else
            {
                Console.WriteLine("Adress updated successfully.");
                UpdatePatientPersonalInfoMenu(); // Return to the update menu after successful update
            }
            // Code to handle adress input and validation
        }

        public void FillInPatientPhoneNumber()
        {
            Console.WriteLine("Fill in your Phone Number: ");
            string phoneNumber = Console.ReadLine();
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
                UpdatePatientPersonalInfoMenu(); // Return to the update menu after successful update
            }
            // Code to handle phone number input and validation
        }

        public void FillInPatientDateOfBirth()
        {
            Console.WriteLine("Fill in your Date of Birth (YYYY-MM-DD): ");
            string dateOfBirth = Console.ReadLine();
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

        public void FillInPatientPersonalInfoHandler(string infoType)
        {
            if (infoType == "1") { FillInPatientFirstName(); }
            else if (infoType == "2") { FillInPatientLastName(); }
            else if (infoType == "3") { FillInPatientGender(); }
            else if (infoType == "4") { FillInPatientPhoneNumber(); }
            else if (infoType == "5") { FillInPatientAdress(); }
            else if (infoType == "6") { FillInPatientDateOfBirth(); }
            else if (infoType == "7") { PatientPersonalInfoMenu(); }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                UpdatePatientPersonalInfoMenu();
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
                // Code to handle appointment booking
            }
            Console.WriteLine("Appointment booked successfully for " + day + " at " + time);
        }

        public void ViewMedicalRecord()
        {
            Console.WriteLine("Viewing Medical Record...");
            // Code to retrieve and display medical record from the database
        }

        public void PatientInformationHandler ()
        {
            PatientMainMenu();
            string patientChoice = Console.ReadLine();
            if (patientChoice == "1")
            {
                PatientPersonalInfoMenu();
                string personalInfoChoice = Console.ReadLine();
                if (personalInfoChoice == "1") 
                { 
                    UpdatePatientPersonalInfoMenu();
                    string updateChoice = Console.ReadLine();
                    FillInPatientPersonalInfoHandler(updateChoice);
                }
                else if (personalInfoChoice == "2") 
                { PatientMainMenu(); }
                else
                { Console.WriteLine("Invalid choice. Please try again."); }
            }
            if (patientChoice == "2")
            {
                BookAppointment();
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

        // Admin
        public void AdminMainMenu()
        {
            Console.WriteLine("Admin Main Menu");
            Console.WriteLine("1. Manage Doctors");
            Console.WriteLine("2. Manage Patients");
            Console.WriteLine("3. Manage Appointments");
            Console.WriteLine("4. Manage Specializations");
            Console.WriteLine("5. View Medical Records");
            Console.WriteLine("6. Logout");
        }
        static void Main(string[] args)
        {
            //string connString = "Host=postgres.mau.se;Username=an5964;Password=vzsjll4k;Database=an5964;Port=55432";
            //NpgsqlConnection conn = new NpgsqlConnection(connString);
            //conn.Open();

            //Console.WriteLine("Connected to database!");

            //conn.Close();

            while (true)
            {
                Program program = new Program();
                program.MainMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        program.PatientInformationHandler();
                        break;
                    case "2":
                        program.DoctorMainMenu();
                        break;
                    case "3":
                        program.AdminMainMenu();
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
    
