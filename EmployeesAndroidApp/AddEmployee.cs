namespace EmployeesAndroidApp
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using EmployeesLibrary;
    using System;

    [Activity(Label = "AddEmployee")]
    public class AddEmployee : Activity
    {
        EmployeesHelper eh;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_employee);

            eh = new EmployeesHelper();

            FindViewById<Button>(Resource.Id.add_employee_button).Click += AddEmployeeAction;
        }

        private void AddEmployeeAction(object sender, EventArgs eventArgs)
        {
            var employee = new EmployeeModel()
            {
                FirstName = FindViewById<TextView>(Resource.Id.firstNameInput).Text,
                LastName = FindViewById<TextView>(Resource.Id.lastNameInput).Text,
                Mail = FindViewById<TextView>(Resource.Id.mailInput).Text,
            };
            eh.AddEmployee(employee);
            StartActivity(typeof(MainActivity));
        }
    }
}