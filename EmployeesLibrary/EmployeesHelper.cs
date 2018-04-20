using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EmployeesLibrary
{
    public class EmployeesHelper
    {
        private List<EmployeeModel> employees;
        private string employeesString;
        string pathToJson;

        public int MyProperty { get; set; }

        public EmployeesHelper(string pathToJson)
        {
            this.pathToJson = pathToJson;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(EmployeesHelper)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("EmployeesLibrary.employees.json");
            using (var reader = new StreamReader(stream))
            {
                employeesString = reader.ReadToEnd();
            }

            //employeesString = File.ReadAllText(pathToJson);
            employees = new List<EmployeeModel>(JsonConvert.DeserializeObject<EmployeeModel[]>(employeesString));
        }

        public List<EmployeeModel> GetEmployees()
        {
            employees.Sort((emp1, emp2) => emp1.Id.CompareTo(emp2.Id));
            return employees;
        }

        public List<string> GetEmployeesAsStrings()
        {
            employees.Sort((emp1, emp2) => emp1.Id.CompareTo(emp2.Id));
            return employees.Select((e) => e.ToString()).ToList();
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            this.MyProperty = 8;
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public EmployeeModel GetEmployeeByMail(string mail)
        {
            return employees.FirstOrDefault(e => e.Mail == mail);
        }

        public EmployeeModel GetEmployeeByName(string name)
        {
            var names = name.Split(' ');
            return employees.FirstOrDefault(e => e.FirstName == names[0] && e.LastName == names[1]);
        }

        public void AddEmployee(EmployeeModel employeeModel)
        {
            employees.Add(employeeModel);
            /*File.Create("employees.json");
            File.WriteAllText("employees.json", JsonConvert.SerializeObject(employees));*/
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
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Mail = employee.Mail,
            });
        }
    }
}
