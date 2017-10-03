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

            //exerciseMap.RouteCoordinates.Add(new Position(60.977594, 24.476400));
            //exerciseMap.RouteCoordinates.Add(new Position(60.976042, 24.475240));
            //exerciseMap.RouteCoordinates.Add(new Position(60.976271, 24.477826));
            //exerciseMap.RouteCoordinates.Add(new Position(60.977594, 24.476400));

        }

        double HAMKLatitude = 60.9769334;
        double HAMKLongitude = 24.475909600000023;
        double lat;
        double lon;
        int i = 0;


        private void StartBtn_Clicked(object sender, EventArgs e)
        {

            PauseBtn.IsVisible = true;
            StartBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

            var MyPosition = new Position(lat, lon);

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = MyPosition,
                Label = "test"
            };

            exerciseMap.Pins.Add(pin);

            Device.StartTimer(TimeSpan.FromSeconds(3), () => {

                GetCurrentLocationAsync();
                exerciseMap.RouteCoordinates.Add(new Position(lat, lon));
                Ticktimes.Text = "Device Timer ticks :" + i.ToString();
                i++;
                return true;

            });

            if(lat != 0 && lon != 0)
            {
                exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lon), Distance.FromKilometers(0.05)));
            }
            

        }

        private void CurLocBtn_Clicked(object sender, EventArgs e)
        {


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

        public async void GetCurrentLocationAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(10000);

            lat = position.Latitude;
            lon = position.Longitude;
            CurLocLongitude.Text = lon.ToString();
            CurLocLatitude.Text = lat.ToString();
        }
    }
}
