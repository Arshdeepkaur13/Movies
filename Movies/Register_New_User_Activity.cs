using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Movies
{
    [Activity(Label = "Register_New_User_Activity")]
    public class Register_New_User_Activity : Activity
    {
        EditText txt_user;
        EditText txt_Password;
        Button btnsubmit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register_New_User_Layout);
            // Create your application here

            btnsubmit = FindViewById<Button>(Resource.Id.btn_Submit);
            txt_user = FindViewById<EditText>(Resource.Id.txt_user);
            txt_Password = FindViewById<EditText>(Resource.Id.txt_pass);

            btnsubmit.Click += btnsubmit_Click;
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {

            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<Login_Tbl>();
                Login_Tbl tbl = new Login_Tbl();
                tbl.username = txt_user.Text;
                tbl.password = txt_Password.Text;
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                SetContentView(Resource.Layout.activity_main);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}
