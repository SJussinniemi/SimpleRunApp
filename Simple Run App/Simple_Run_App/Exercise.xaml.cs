using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;

namespace Simple_Run_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercise : ContentPage
    {
        public Exercise()
        {
            InitializeComponent();
            
            
        }
       
        private void StartBtn_Clicked(object sender, EventArgs e)
        {

            PauseBtn.IsVisible = true;
            StartBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

            double HAMKLatitude = 60.9769334;
            double HAMKLongitude = 24.475909600000023;
            var position = new Position(HAMKLatitude, HAMKLongitude);

            exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(HAMKLatitude, HAMKLongitude), Distance.FromKilometers(0.5)));

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "test"
            };

            exerciseMap.Pins.Add(pin);
        }

        private async void CurLocBtn_Clicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(10000);

            CurLocLongitude.Text = position.Longitude.ToString();
            CurLocLatitude.Text = position.Latitude.ToString(); 
        

        }

        private void onPause(object sender, EventArgs e)
        {
            PauseBtn.IsVisible = false;
            StartBtn.IsVisible = true;
            EndBtn.IsEnabled = true;
        }
    }
}
