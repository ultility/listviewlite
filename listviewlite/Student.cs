using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace listviewlite
{
    [Table("Students")]
    public class Student
    {   
        public Student(string name, int Grade1, int Grade2, int Grade3, string bitmap)
        {
            Name = name;
            grade1 = Grade1;
            grade2 = Grade2;
            grade3 = Grade3;
            Bitmap = bitmap;
        }

        public Student()
        {

        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }
        public string Name { get; set; }
        public int grade1 { get; set; }
        public int grade2 { get; set; }
        public int grade3 { get; set; }
        public string Bitmap { get; set; }

    }
}