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
using Android.Locations;
using Simple_Run_App;

[assembly: Xamarin.Forms.Dependency(typeof(Exercise))]
namespace Simple_Run_App.Droid
{
    [Activity(Label = "LocationHandler")]
    public class LocationHandler : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            Context contxt = Android.App.Application.Context;

            LocationManager locMan;
            locMan = GetSystemService(Context.LocationService) as LocationManager;
            MoveString();
        }

        String Testi = "Täällä aktiviteetti androidissa";
        public string MoveString()
        {
            return "Täällä aktiviteetti androidissa";
        }
        
    }
}