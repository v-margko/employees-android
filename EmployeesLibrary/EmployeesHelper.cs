namespace EmployeesLibrary
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeesHelper
    {
        private Database database;
        private List<EmployeeModel> employees;

        public EmployeesHelper()
        {
            database = Database.Current;
        }

        public List<EmployeeModel> GetEmployees()
        {
            employees = database.GetAllEmployees<EmployeeModel>("Employees");
            employees.Sort((emp1, emp2) => emp1.Id.CompareTo(emp2.Id));
            return employees;
        }

        public List<string> GetEmployeesAsStrings()
        {
            employees = database.GetAllEmployees<EmployeeModel>("Employees");
            employees.Sort((emp1, emp2) => emp1.Id.CompareTo(emp2.Id));
            return employees.Select((e) => e.ToString()).ToList();
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            employees = database.GetAllEmployees<EmployeeModel>("Employees");
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public EmployeeModel GetEmployeeByMail(string mail)
        {
            employees = database.GetAllEmployees<EmployeeModel>("Employees");
            return employees.FirstOrDefault(e => e.Mail == mail);
        }

        public EmployeeModel GetEmployeeByName(string name)
        {
            employees = database.GetAllEmployees<EmployeeModel>("Employees");
            var names = name.Split(' ');
            return employees.FirstOrDefault(e => e.FirstName == names[0] && e.LastName == names[1]);
        }

        public void AddEmployee(EmployeeModel employeeModel)
        {
            database.AddEmployee(employeeModel);
        }

        public void AddEmployee(string empl)
        {
            if (empl == null)
            {
                return;
            }
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(empl);
            this.AddEmployee(new EmployeeModel()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Mail = employee.Mail,
            });
        }

        public string GetEmployee(string by)
        {
            var parsed = int.TryParse(by, out int id);
            EmployeeModel result = null;
            if (parsed)
            {
                result = GetEmployeeById(id);
            }
            else if (by.Contains("@"))
            {
                result = GetEmployeeByMail(by);
            }
            else
            {
                result = GetEmployeeByName(by);
            }

            return result != null ? result.ToString() : "Not found!";
        }
    }
}
