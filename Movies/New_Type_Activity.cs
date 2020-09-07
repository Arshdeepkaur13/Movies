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
    [Activity(Label = "New_Type_Activity")]
    public class New_Type_Activity : Activity
    {
        List<Type_Tbl> List_All;
        EditText txt_name;
        Spinner spinner_type;
        TextView txt_type_id;
        Button btn_save;
            Button btn_delete;
            Button btn_new;
            Button btn_update;
        Button btn_new_movie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Movies_Type );

            btn_new_movie = FindViewById<Button>(Resource.Id.btn_new_movie);
            btn_save = FindViewById<Button>(Resource.Id.btn_submit);
            btn_delete = FindViewById<Button>(Resource.Id.btn_delete);

            btn_new = FindViewById<Button>(Resource.Id.btn_new);
            btn_update = FindViewById<Button>(Resource.Id.btn_update);
            txt_name = FindViewById<EditText>(Resource.Id.txt_name);
            spinner_type = FindViewById<Spinner>(Resource.Id.spinner_type);
            txt_type_id = FindViewById<TextView>(Resource.Id.txt_type_id);


            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);

            db.CreateTable<Type_Tbl>();






            load_spiner_type();

            spinner_type.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_type_ItemSelected);


            btn_save.Click += Btn_save_Click; ;
            btn_new.Click += Btn_new_Click;
            btn_delete.Click += Btn_delete_Click;
            btn_update.Click += Btn_update_Click;
            btn_new_movie.Click += Btn_new_movie_Click;
        }

        private void Btn_new_movie_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(New_Movie_Activity));
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);





            var item = new Type_Tbl();

            item.Id = Convert.ToInt32(txt_type_id.Text);




            item.Movie_Type  = txt_name.Text;


            db.Update(item);

            Toast.MakeText(this, "Record Updated Successfully...,", ToastLength.Short).Show();

            load_spiner_type();
        }

        private void Btn_new_Click(object sender, EventArgs e)
        {
            txt_name.Text = "";

        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {




            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);


            var subitem = new Movie_Tbl();
            subitem.Tid = Convert.ToInt32(txt_type_id.Text);

            var data_s = db.Query<Movie_Tbl>("select *  from Movie_Tbl where Tid=" + Convert.ToInt32(txt_type_id.Text));
            if (data_s.Count > 0)
            {
                Toast.MakeText(this, "Record Will not deleted as Movie Exists...,", ToastLength.Short).Show();

            }
            else
            {
                var item = new Type_Tbl();
                item.Id = Convert.ToInt32(txt_type_id.Text);
                var data = db.Delete(item);
                Toast.MakeText(this, "Record Deleted Successfully...,", ToastLength.Short).Show();
                txt_name.Text = "";
                load_spiner_type();

            }







        }

        private void spinner_type_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner)sender;
            var id = this.List_All.ElementAt(e.Position).Id;
            var masteraccountname = this.List_All.ElementAt(e.Position).Movie_Type;

            txt_type_id.Text = Convert.ToString(id);
            // txtmastergp.Text = masteraccountname;
            btn_delete.Enabled = true;

        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<Type_Tbl>();
            Type_Tbl tbl = new Type_Tbl();
            tbl.Movie_Type  = txt_name.Text;
            db.Insert(tbl);
            Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            txt_name.Text = "";
            load_spiner_type();

        }

        private void load_spiner_type()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Type_Tbl>("select *  from Type_Tbl");
            List_All = data_s;
            Movies.Resources.Adpter_Type da = new Resources.Adpter_Type(this,List_All);
            spinner_type.Adapter = da;

        }
    }
}