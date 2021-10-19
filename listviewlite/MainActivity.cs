using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using SQLite;
using System;
using System.Collections.Generic;
using AlertDialog = Android.App.AlertDialog;

namespace listviewlite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static List<Student> list;
        EditText name;
        EditText age;
        EditText halfone;
        EditText halftwo;
        Button save;
        Button display;
        Button dp;
        Button create;
        CheckBox male;
        CheckBox female;
        ListView lv;
        public static Bitmap malepic;
        public static Bitmap femalepic;
        StudentAdapter apr;
        int delete;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            malepic = BitmapFactory.DecodeResource(Resources, Resources.GetIdentifier("male", "drawable", PackageName));
            femalepic = BitmapFactory.DecodeResource(Resources, Resources.GetIdentifier("female", "drawable", PackageName));
            var db = new SQLiteConnection(Helper.Path());
            db.CreateTable<Student>();
            list = GetAllStudents();
            Toast.MakeText(this, "" + list.Count, ToastLength.Long).Show();
            SetContentView(Resource.Layout.activity_main);
            name = (EditText)FindViewById(Resource.Id.name);
            age = (EditText)FindViewById(Resource.Id.age);
            halfone = (EditText)FindViewById(Resource.Id.halfone);
            halftwo = (EditText)FindViewById(Resource.Id.halftwo);
            save = (Button)FindViewById(Resource.Id.add);
            display = (Button)FindViewById(Resource.Id.show);
            dp = (Button)FindViewById(Resource.Id.dp);
            create = FindViewById<Button>(Resource.Id.create);
            male = FindViewById<CheckBox>(Resource.Id.male);
            female = FindViewById<CheckBox>(Resource.Id.female);
            male.CheckedChange += Male_CheckedChange;
            female.CheckedChange += Female_CheckedChange;
            dp.Click += Dp_Click;
            save.Click += Save_Click;
            display.Click += Display_Click;
            create.Click += Create_Click;
            delete = -1;
        }

        private void Create_Click(object sender, EventArgs e)
        {
            int pos = -1;
            Intent i = new Intent(this, typeof(EditActivity));
            i.PutExtra("pos", pos);
            StartActivity(i);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (apr != null)
            {
                apr.NotifyDataSetChanged();
            }
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int pos = e.Position;
            Intent i = new Intent(this, typeof(EditActivity));
            i.PutExtra("pos", pos);
            StartActivity(i);
        }

        private void Female_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (female.Checked)
            {
                male.Checked = false;
            }
            else if (!male.Checked && !female.Checked)
            {
                female.Checked = true;
            }
        }

        private void Male_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (male.Checked)
            {
                female.Checked = false;
            }
            else if (!male.Checked && !female.Checked)
            {
                male.Checked = true;
            }
        }

        private void Dp_Click(object sender, System.EventArgs e)
        {
            lv = (Android.Widget.ListView)FindViewById(Resource.Id.listView1);
            apr = new StudentAdapter(this, list);
            lv.Adapter = apr;
            lv.ItemClick += Lv_ItemClick;
            lv.ItemLongClick += Lv_ItemLongClick;
        }

        private void Lv_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            AlertDialog.Builder build = new AlertDialog.Builder(this);
            build.SetMessage("are you sure you want to delete?");
            build.SetPositiveButton("yes", agree);
            build.SetNegativeButton("no", disagree);
            build.Show();
        }
        private void disagree(object sender, EventArgs e)
        {
            Console.WriteLine("nope");
        }
        private void agree(object sender, System.EventArgs e)
        {
            if (delete >= 0)
            {
                list.RemoveAt(delete);
                apr.NotifyDataSetChanged();
            }
        }

        private void Display_Click(object sender, System.EventArgs e)
        {
            string str = "";
            foreach (Student student in list)
            {
                str += student.Name + " ";
            }
            Toast.MakeText(this, str, ToastLength.Long).Show();
        }

        private void Save_Click(object sender, System.EventArgs e)
        {
            int age = 0;
            int halfone = 0;
            int halftwo = 0;
            int.TryParse(this.age.Text, out age);
            int.TryParse(this.halfone.Text, out halfone);
            int.TryParse(this.halftwo.Text, out halftwo);
            Bitmap pic;
            if (male.Checked)
            {
                pic = malepic;
            }
            else
            {
                pic = femalepic;
            }
            list.Add(new Student(name.Text, age, halfone, halftwo, Helper.BitmapToBase64(pic)));
            var db = new SQLiteConnection(Helper.Path());
            db.Insert(list[list.Count - 1]);
            this.name.Text = "";
            this.age.Text = "";
            this.halfone.Text = "";
            this.halftwo.Text = "";
            Toast.MakeText(this, "" + list.Count, ToastLength.Long).Show();
        }

        public List<Student> GetAllStudents()
        {
            List<Student> StudentList = new List<Student>();
            var db = new SQLiteConnection(Helper.Path());
            string strsql = string.Format("SELECT * FROM students");
            var persons = db.Query<Student>(strsql);
            if (persons.Count > 0)
            {
                foreach (var item in persons)
                {
                    StudentList.Add(item);
                }
            }
            return StudentList;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}