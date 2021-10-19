using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace listviewlite
{
    public class StudentAdapter : BaseAdapter<Student>
    {
        public List<Student> objects;
        private Context context;
        public StudentAdapter(Context context, List<Student> list)
        {
            objects = list;
            this.context = context;
        }
        public override Student this[int position]
        {
            get
            {
                return objects[position];
            }
        }
        public override int Count
        {
            get
            {
                return objects.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (convertView == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.cell_layout, null, false);
            }
            TextView tv1 = (TextView)view.FindViewById(Resource.Id.name);
            TextView tv2 = (TextView)view.FindViewById(Resource.Id.grade1);
            TextView tv3 = (TextView)view.FindViewById(Resource.Id.grade2);
            TextView tv4 = (TextView)view.FindViewById(Resource.Id.grade3);
            ImageView iv = (ImageView)view.FindViewById(Resource.Id.pic);
            Student std = objects[position];
            if (std != null)
            {
                tv1.Text = std.Name;
                tv2.Text = "" + std.grade1;
                tv3.Text = "" + std.grade2;
                tv4.Text = "" + std.grade3;
                iv.SetImageBitmap(Helper.Base64ToBitmap(std.Bitmap));
            }
            return view;
        }
    }
}