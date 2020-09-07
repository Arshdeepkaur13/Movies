using System;
using System.Collections.Generic;
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
   public class Movie_Tbl   
    {
        [PrimaryKey, AutoIncrement] //Column("Id")]
        public int Id { get; set; }
        public int Tid { get; set; }
        public string Movie_name { get; set; }

        public int Movie_year { get; set; }
    }
}