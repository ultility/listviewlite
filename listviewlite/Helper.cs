using Android.Graphics;
using Android.Util;
using System;
using System.IO;

class Helper
{
    public static string dbname = "dbTest6";
    public Helper()
    {
    }
    public static string Path()
    {
        string path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Helper.dbname);
        return path;
    }
    public static string BitmapToBase64(Bitmap bitmap)
    {
        string str = "";
        using (var stream = new MemoryStream())
        {
            bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
            var bytes = stream.ToArray();
            str = Convert.ToBase64String(bytes);
        }
        return str;
    }

    public static Bitmap Base64ToBitmap(String base64String)
    {
        byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
        return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
    }
}
