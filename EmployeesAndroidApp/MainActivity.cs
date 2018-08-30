namespace EmployeesAndroidApp
{
    using System;
    using Android.App;
    using Android.Widget;
    using Android.OS;
    using Android.Support.Design.Widget;
    using Android.Support.V7.App;
    using Android.Views;
    using EmployeesLibrary;
    using System.Collections.Generic;
    using Android.Content;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EmployeesHelper eh;

        public List<EmployeeModel> EmployeesList { get; set; }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            if (hasFocus)
            {
                SetContentView(Resource.Layout.activity_main);

                FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
                fab.Click += FabOnClick;

                eh = new EmployeesHelper();
                EmployeesList = eh.GetEmployees();
                var list = (ListView)FindViewById(Resource.Id.employeesListView);

                var ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, eh.GetEmployeesAsStrings());
                list.Adapter = ListAdapter;
                list.ItemClick += EmployeesListView_ItemClick;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(AddEmployee));
        }

        private void EmployeesListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            StartActivity(typeof(AddEmployee));
        }
    }
}

