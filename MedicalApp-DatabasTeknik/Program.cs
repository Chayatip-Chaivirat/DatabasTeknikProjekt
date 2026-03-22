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

        public void PatientInformationHandler ()
        {
            PatientMainMenu();
            string patientChoice = Console.ReadLine();
            if (patientChoice == "1")
            {
                PatientPersonalInfoMenu();
                string personalInfoChoice = Console.ReadLine();
                if (personalInfoChoice == "1") 
                { UpdatePatientPersonalInfoMenu(); }
                else if (personalInfoChoice == "2") 
                { PatientMainMenu(); }
                else
                { Console.WriteLine("Invalid choice. Please try again."); }
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
                        program.PatientMainMenu();
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
    
