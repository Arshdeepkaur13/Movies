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
    public class Connection_cls
    {
        private string dbPath { get; set; }

        private SQLiteConnection db { get; set; }
        public Connection_cls()
        {

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Movies.sqlite");

            db = new SQLiteConnection(dbPath);


        }
    }
}