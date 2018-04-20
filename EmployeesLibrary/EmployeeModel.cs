namespace EmployeesLibrary
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as EmployeeModel;
            return model != null &&
                   this.Id == model.Id &&
                   this.FirstName == model.FirstName &&
                   LastName == model.LastName &&
                   Mail == model.Mail;
        }

        public override string ToString()
        {
            return $"ID: {Id}\n First Name: {this.FirstName}\n Last Name: {LastName}\n E-mail: {Mail}";
        }
    }
}