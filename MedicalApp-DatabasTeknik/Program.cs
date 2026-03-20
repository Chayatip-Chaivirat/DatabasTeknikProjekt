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
        public void PatientMainMenu()
        {
            Console.WriteLine("Patient Main Menu");
            Console.WriteLine("1. View Personal Information");
            Console.WriteLine("2. Update Personal Information");
            Console.WriteLine("3. Book Appointment");
            Console.WriteLine("4. View Medical Record");
            Console.WriteLine("5. Logout");
        }

        public void DoctorMainMenu()
        {
            Console.WriteLine("Doctor Main Menu");
            Console.WriteLine("1. View Appointments");
            Console.WriteLine("2. Update Time Table");
            Console.WriteLine("3. Update Medical Record");
            Console.WriteLine("4. Logout");
        }

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

        }
    }
}
