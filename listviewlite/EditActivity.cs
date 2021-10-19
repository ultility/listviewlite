using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace listviewlite
{
    [Activity(Label = "EditActivity")]
    public class EditActivity : Activity
    {
        EditText name;
        EditText age;
        EditText grade1;
        EditText grade2;
        Button save;
        Student edited;
        CheckBox male;
        CheckBox female;
        Button photo;
        ImageView image;
        int pos;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.edit);
            name = FindViewById<EditText>(Resource.Id.name);
            age = FindViewById<EditText>(Resource.Id.age);
            grade1 = FindViewById<EditText>(Resource.Id.grade1);
            grade2= FindViewById<EditText>(Resource.Id.grade2);
            save = FindViewById<Button>(Resource.Id.save);
            save.Click += Save_Click;
            photo = FindViewById<Button>(Resource.Id.button1);
            image = FindViewById<ImageView>(Resource.Id.imageView1);
            photo.Click += Photo_Click;
            male = FindViewById<CheckBox>(Resource.Id.male);
            female = FindViewById<CheckBox>(Resource.Id.female);
            male.CheckedChange += Male_CheckedChange;
            female.CheckedChange += Female_CheckedChange;
            pos = Intent.GetIntExtra("pos", -1);
            if (pos >= 0 && pos < MainActivity.list.Count)
            {
                edited = MainActivity.list[pos];
                name.Text = edited.Name;
                age.Text = "" + edited.grade1;
                grade1.Text = "" + edited.grade2;
                grade2.Text = "" + edited.grade3;
                image.SetImageBitmap(Helper.Base64ToBitmap(edited.Bitmap));
            }
            else
            {
                edited = new Student();
            }


        }
        private void Photo_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Android.Provider.MediaStore.ActionImageCapture);
            StartActivityForResult(i, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0)
            {
                if (resultCode == Result.Ok)
                {
                    Bitmap bm = (Bitmap)data.Extras.Get("data");
                    edited.Bitmap = Helper.BitmapToBase64(bm);
                    image.SetImageBitmap(bm);
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            edited.Name = name.Text;
            edited.grade1 = int.Parse(age.Text);
            edited.grade2 = int.Parse(grade1.Text);
            edited.grade3 = int.Parse(grade2.Text);
            if (edited.Bitmap == null) {
                if (male.Checked)
                {
                    edited.Bitmap = Helper.BitmapToBase64(MainActivity.malepic);
                }
                else
                {
                    edited.Bitmap = Helper.BitmapToBase64(MainActivity.femalepic);
                }
            }
            var db = new SQLiteConnection(Helper.Path());
            if (pos == -1)
            {
                db.Insert(edited);
                MainActivity.list.Add(edited);
            }
            else
            {
                db.Update(edited);
                MainActivity.list[pos] = edited;
            }
            Finish();
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
    }
}