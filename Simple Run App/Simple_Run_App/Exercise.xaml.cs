using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using SQLite;

namespace Simple_Run_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercise : ContentPage
    {
        public Exercise()
        {
            InitializeComponent();
            TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";

            Device.BeginInvokeOnMainThread(() =>
            {
                exerciseMap.RouteCoordinates = new List<Position>
                {
                };
            });
        }

        // ****** TODO REMOVE USELESS VARIABLES ****** 

        double lat; // Location latitude
        double lon; // Location longitude
        Boolean IsRunning; // For Device timer handling
        bool loc = false;
        int i = 0; // For develop purposes. Tracks How Many times Device.StartTimer has looped.
        ///////////
        public String DataSend;
        Boolean pause = false;
        int hr = 0;
        int min = 0;
        int sec = 0;
        int cont = 0;//Only For testting 
        // ---------- Vars Total Distance -------------
        double lat1 = 0.0, long1 = 0.0;//At the beginning initialize with the actual position
        double lat2 = 0.0, long2 = 0.0;
        double Distance2 = 0.0, FinalDistance = 0.0;//Distance =>Distance between two points; FinalDistance => Total Distance in the exercise.
        //--------------------------------------------
        // --------Vars Final Speed ------------------
        double FinalSpeed = 0.0;
        //--------------------------------------------

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            pause = true;
            // Get starting position WOIP perhaps Loading screen later.
            //GetCurrentLocationAsync();

            PauseBtn.IsVisible = true;
            StartBtn.IsVisible = false;
            EndBtn.IsEnabled = true;

            IsRunning = true;
            StartingPoint();
            StartTimer();
            StartRunning();

        }

        private void onPause_Clicked(object sender, EventArgs e)
        {
            pause = false;

            PauseBtn.IsVisible = false;
            ResumeBtn.IsVisible = true;
            EndBtn.IsEnabled = true;

            IsRunning = false;
        }

        private async void EndBtn_ClickedAsync(object sender, EventArgs e)
        {

            StartBtn.IsVisible = true;
            PauseBtn.IsVisible = false;
            ResumeBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

            pause = false;
            IsRunning = false;
            CurLocLatitude.Text = "End Button pressed";
            i = 0;

            bool answer = await DisplayAlert("Great Run!", "Would you like to save it to history?", "Yes", "No");

            if (answer)
            {
                try
                {
                    CurLocLongitude.Text = "juostu";
                    var db = App.database;

                    var tableItems = new ExerciseTable
                    {
                        DURATION = TimerLabel.Text,
                        DISTANCE = TimerDistance.Text,
                        AVGSPEED = TimerDistance.Text,
                        DATETIME = DateTime.Now
                    };

                    await db.SaveItemAsync(tableItems);

                }
                catch (SQLiteException ex)
                {
                    CurLocLongitude.Text = ex.ToString();
                }
            }
        }

        private void ResumeBtn_Clicked(object sender, EventArgs e)
        {
            PauseBtn.IsVisible = true;
            ResumeBtn.IsVisible = false;

            pause = true;
            StartTimer();

            IsRunning = true;
            StartRunning();
        }

        //For test usage, do not remove yet.
        private void drawButton_Clicked(object sender, EventArgs e)
        {

        }

        public async void GetCurrentLocationAsync()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(10000);

                lat = position.Latitude;
                lon = position.Longitude;

                CurLocLongitude.Text = lon.ToString();
                CurLocLatitude.Text = lat.ToString();
                loc = true;
            }
            catch (Exception ex)
            {
                CurLocLatitude.Text = ex.ToString();
            }

        }

        public void StartRunning()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () => {

                GetCurrentLocationAsync();

                if (loc == true)
                {
                    var list = new List<Position>(exerciseMap.RouteCoordinates);
                    list.Add(new Position(lat, lon));
                    exerciseMap.RouteCoordinates = list;

                    Ticktimes.Text = "Device Timer ticks :" + i.ToString();
                    i++;

                    exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lon), Distance.FromKilometers(0.1)));
                    TotalDistance(lat, lon);

                }
                return IsRunning;

            });
        }

        //TODO MERGE STARTRUNNING & STARTIMER

        public void StartTimer()
        {

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                sec++;
                if (sec == 60)
                {
                    min++;
                    sec = 0;
                }
                if (min == 60)
                {
                    hr++;
                    min = 0;
                }
                if (hr == 24) hr = 0;

                TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";
                Calculations();
                return pause; // True = Repeat again, False = Stop the timer

            });
        }

        public void Calculations()
        {
            CalculationClass calculationClass = new CalculationClass();

            FinalSpeed = calculationClass.finalSpeed(FinalDistance, hr, min, sec);

            TimerDistance.Text = "Distance: " + FinalDistance.ToString();
            TimerCurSpeed.Text = "Speed " + FinalSpeed.ToString();
            TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";

        }

        // TODO MERGE CALCULATIONS & TOTALDISTANCE METHODS

        public void TotalDistance(double nextLat, double nextLong)
        {
            lat2 = nextLat;
            long2 = nextLong;
            CalculationClass calculationClass = new CalculationClass();
            Distance2 = calculationClass.DistanceBtwnTwoPnts(lat1, long1, lat2, long2);
            FinalDistance = FinalDistance + Distance2;
            Distance2 = 0;
            //Now the second point is the first one and we wait for the coordinates of the next point
            lat1 = lat2;
            long1 = long2;
        }

        public async void StartingPoint()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(10000);

                lat = position.Latitude;
                lon = position.Longitude;

                lat1 = lat;
                long1 = lon;

                lat2 = lat;
                long2 = lon;
            }
            catch (Exception ex)
            {
                CurLocLatitude.Text = ex.ToString();
            }

        }
    }
}