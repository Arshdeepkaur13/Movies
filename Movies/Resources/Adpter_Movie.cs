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

namespace Movies.Resources
{
   public class Adpter_Movie : BaseAdapter<Movie_Tbl>
    {
        private readonly Activity context;
        private readonly List<Movie_Tbl> mItems;

        public Adpter_Movie(Activity context, List<Movie_Tbl> items)
        {
            this.mItems = items;
            this.context = context;
        }



        public override int Count
        {
            get { return mItems.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Movie_Tbl this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var row = convertView;


            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.List_All_Movies , null, false);
            }

            // Set the txtRowName.Text which is in the listview_row layout to the movies Name
            TextView txtRowName_movie_name = row.FindViewById<TextView>(Resource.Id.txtRowName_movie);
            txtRowName_movie_name.Text = mItems[position].Movie_name ;
           
            TextView txt_year = row.FindViewById<TextView>(Resource.Id.txt_movie_year);

            txt_year.Text =  Convert.ToString(mItems[position].Movie_year);


            return row;


        }
    }
}