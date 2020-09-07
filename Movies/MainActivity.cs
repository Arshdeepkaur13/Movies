using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Movies
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txt_user_name;
        EditText txt_Pass_word;
        Button btn_new_user;
        Button btn_login;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            btn_login = FindViewById<Button>(Resource.Id.btn_login);
            btn_new_user = FindViewById<Button>(Resource.Id.btn_new_user);
            txt_user_name = FindViewById<EditText>(Resource.Id.txt_user_name);
            txt_Pass_word = FindViewById<EditText>(Resource.Id.txt_pass_word);

            btn_login.Click += btn_login_Click;
            btn_new_user.Click += btn_new_user_Click;


        }

        private void btn_new_user_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Register_New_User_Activity));
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<Login_Tbl>();

                var data1 = data.Where(x => x.username == txt_user_name.Text && x.password == txt_Pass_word.Text).FirstOrDefault();

                if (data1 != null)
                {
                    Toast.MakeText(this, "Login Success", ToastLength.Short).Show();

                    StartActivity(typeof(New_Type_Activity ));

                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();

            }

        }






    }
}