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
using Xamarin.Forms;
using Simple_Run_App.Droid;
using System.IO;

[assembly: Dependency(typeof(FileHelper))]
namespace Simple_Run_App.Droid
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            // /data/user/0/simplerunapp.simplerunapp/files/
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(path, filename);
            // /data/user/0/simplerunapp.simplerunapp/files/SimpleRunAppDB.db3
            return dbPath;
        }
    }
}