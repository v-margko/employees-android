namespace EmployeesLibrary
{
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Database
    {
        readonly private string databasePath;

        private static Database instance;

        private Database()
        {
            databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Employees.db");
            using (var db = new SQLiteConnection(databasePath))
            {
                if (!TableExists("Employees"))
                {
                    db.CreateTable<EmployeeModel>();
                    FillInTestData();
                }
            }
        }

        public bool TableExists(string tableName)
        {
            using (var db = new SQLiteConnection(databasePath))
            {
                SQLiteCommand cmd = db.CreateCommand("SELECT * FROM sqlite_master WHERE type = 'table' AND name = @name", tableName);
                return (cmd.ExecuteScalar<string>() != null);
            }
        }

        public static Database Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }

                return instance;
            }
        }

        public void FillInTestData()
        {
            using (var db = new SQLiteConnection(databasePath))
            {
                db.Insert(new EmployeeModel()
                {
                    FirstName = "Ivan",
                    LastName = "Dorn",
                    Mail = "ivdorn@mail.com"
                });
                db.Insert(new EmployeeModel()
                {
                    FirstName = "Anna",
                    LastName = "Petrova",
                    Mail = "apetrova@mail.com"
                });
                db.Insert(new EmployeeModel()
                {
                    FirstName = "User",
                    LastName = "Name",
                    Mail = "username@mail.com"
                });
                db.Insert(new EmployeeModel()
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Mail = "Test@mail.com"
                });
            }
        }

        internal List<T> GetAllEmployees<T>(string tableName)
        {
            using (var db = new SQLiteConnection(databasePath))
            {
                return db.CreateCommand("SELECT * FROM " + tableName).ExecuteQuery<T>();
            }
        }

        internal void AddEmployee(EmployeeModel employeeModel)
        {
            using (var db = new SQLiteConnection(databasePath))
            {
                db.Insert(employeeModel);
            }
        }
    }
}