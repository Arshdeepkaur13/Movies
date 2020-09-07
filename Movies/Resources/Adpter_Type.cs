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
  public class Adpter_Type : BaseAdapter<Type_Tbl>
    {
        private readonly Activity context;
        private readonly List<Type_Tbl> mItems;

        public Adpter_Type(Activity context, List<Type_Tbl> items)
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

        public override Type_Tbl this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var row = convertView;


            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.List_Movie_Types, null, false);
            }

            // Set the txtRowName.Text which is in the listview_row layout to the Players Name
            TextView txtRowName = row.FindViewById<TextView>(Resource.Id.txtRow_type_Name);
            txtRowName.Text = mItems[position].Movie_Type;



            return row;


        }
    }
}