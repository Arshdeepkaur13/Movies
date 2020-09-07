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
    [Activity(Label = "New_Movie_Activity")]
    public class New_Movie_Activity : Activity
    {
        List<Movie_Tbl> List_All_Movies;
        List<Type_Tbl> List_All_Types;
        EditText txtmovie_name;
        EditText txt_movie_year;
        Button btn_save_movie;
        Button btndelete_movie;
        Button btnupdate_movie;
        Button btnnew_movie;

        Spinner spinnershowtype;
        Spinner spinnershowmovie;

        TextView txt_movie_id;
        TextView txt_type_id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.New_Movie_Layout);
            btnnew_movie = FindViewById<Button>(Resource.Id.btn_new_movie);

            btn_save_movie = FindViewById<Button>(Resource.Id.btn_save_movie);

            btndelete_movie = FindViewById<Button>(Resource.Id.btn_delete_movie);
            btnupdate_movie = FindViewById<Button>(Resource.Id.btn_update_movie);
            txtmovie_name = FindViewById<EditText>(Resource.Id.txt_movie_name);
            txt_movie_year = FindViewById<EditText>(Resource.Id.txt_year);
            spinnershowtype = FindViewById<Spinner>(Resource.Id.spinner_show_type);
            spinnershowmovie = FindViewById<Spinner>(Resource.Id.spinner_show_movie);

            txt_movie_id = FindViewById<TextView>(Resource.Id.txt_movie_id);
            txt_type_id = FindViewById<TextView>(Resource.Id.txt_type_id);

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);

            db.CreateTable<Movie_Tbl>();






            load_spiner_type();
            load_spiner_movies();


            spinnershowtype.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_show_type_ItemSelected);
            spinnershowmovie.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_show_movies_ItemSelected);
            btn_save_movie.Click += btn_save_movie_Click;
            btndelete_movie.Click += btndelete_movie_Click;
            btnupdate_movie.Click += btnupdate_movie_Click;

        }

        private void btnupdate_movie_Click(object sender, EventArgs e)
        {
            var item_movie = new Movie_Tbl();
            item_movie.Id = Convert.ToInt32(txt_movie_id.Text);
            item_movie.Movie_name  = txtmovie_name.Text;
            item_movie.Movie_year = Convert.ToInt32(txt_movie_year.Text);
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);

            var data = db.Update(item_movie);

            Toast.MakeText(this, "Record Updated Successfully...,", ToastLength.Short).Show();

            load_spiner_type();
            load_spiner_movies();
        }

        private void btndelete_movie_Click(object sender, EventArgs e)
        {


            var item = new Movie_Tbl();
            item.Id = Convert.ToInt32(txt_movie_id.Text);

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data = db.Delete(item);
            Toast.MakeText(this, "Record Deleted Successfully...,", ToastLength.Short).Show();
            txtmovie_name.Text = "";
            load_spiner_type();
            load_spiner_movies();
        }

        private void btn_save_movie_Click(object sender, EventArgs e)
        {
            if (txtmovie_name.Text.Length>0 && txt_movie_year.Text.Length>0)
            {

           
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<Movie_Tbl>();
            Movie_Tbl tbl = new Movie_Tbl();
            tbl.Movie_name  = Convert.ToString(txtmovie_name.Text);
            tbl.Tid = Convert.ToInt32(txt_type_id.Text);
            tbl.Movie_year = Convert.ToInt32(txt_movie_year.Text);
            db.Insert(tbl);
            Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            txtmovie_name.Text = "";
            load_spiner_type();
            load_spiner_movies();
            }
            else
            {
                Toast.MakeText(this, "Movie Name/Year Required...,", ToastLength.Short).Show();
            }

        }

        private void spinner_show_movies_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var id = this.List_All_Movies.ElementAt(e.Position).Id;
            var moviename = this.List_All_Movies.ElementAt(e.Position).Movie_name ;

            txt_movie_id.Text = Convert.ToString(id);
          

        }

        private void spinner_show_type_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner)sender;
            var id = this.List_All_Types.ElementAt(e.Position).Id;

            txt_type_id.Text = Convert.ToString(id);

            load_spiner_movies();
        }

        private void load_spiner_type()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Type_Tbl>("select *  from  type_tbl");
            List_All_Types = data_s;
            Movies.Resources.Adpter_Type da = new Resources.Adpter_Type(this, List_All_Types);
            spinnershowtype.Adapter = da;
            txt_type_id.Text = Convert.ToString(0);
        }

        private void load_spiner_movies()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Movie_Tbl>("select *  from Movie_Tbl where tid=" + Convert.ToInt32 (txt_type_id.Text));
            //var data_s = db.Query<Movie_Tbl>("select *  from Movie_Tbl where tid=2");
            List_All_Movies = data_s;
            Movies.Resources.Adpter_Movie da = new Resources.Adpter_Movie(this, List_All_Movies);
            spinnershowmovie.Adapter = da;
        }
    }
}