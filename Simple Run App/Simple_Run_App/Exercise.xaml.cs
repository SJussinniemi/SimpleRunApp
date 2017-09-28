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

            exerciseMap.RouteCoordinates.Add(new Position(60.977594, 24.476400));
            exerciseMap.RouteCoordinates.Add(new Position(60.976042, 24.475240));
            exerciseMap.RouteCoordinates.Add(new Position(60.976271, 24.477826));
            exerciseMap.RouteCoordinates.Add(new Position(60.977594, 24.476400));


        }

        double HAMKLatitude = 60.9769334;
        double HAMKLongitude = 24.475909600000023;

        private void StartBtn_Clicked(object sender, EventArgs e)
        {

            PauseBtn.IsVisible = true;
            StartBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

            
            
            var position = new Position(HAMKLatitude, HAMKLongitude);

            exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(HAMKLatitude, HAMKLongitude), Distance.FromKilometers(0.1)));
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

            double lat = position.Latitude;
            double lon = position.Longitude;
            var MyPosition = new Position(lat, lon);

            CurLocLongitude.Text = lon.ToString();
            CurLocLatitude.Text = lat.ToString();

            exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat,lon), Distance.FromKilometers(0.1)));

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = MyPosition,
                Label = "test"
            };

            exerciseMap.Pins.Add(pin);
        }

        private void onPause(object sender, EventArgs e)
        {
            PauseBtn.IsVisible = false;
            StartBtn.IsVisible = true;
            EndBtn.IsEnabled = true;
        }

        private void drawButton_Clicked(object sender, EventArgs e)
        {
            exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(HAMKLatitude, HAMKLongitude), Distance.FromKilometers(0.1)));
        }
    }
}
