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
        int hr = 0;
        int min = 0;
        int sec = 0;

        // ---------- Vars Total Distance -------------
        double lat1 = 0.0, long1 = 0.0;//At the beginning initialize with the actual position
        double lat2 = 0.0, long2 = 0.0;
        double Distance2 = 0.0, FinalDistance = 0.0;//Distance =>Distance between two points; FinalDistance => Total Distance in the exercise.
        //--------------------------------------------
        // --------Vars Final Speed ------------------
        double FinalSpeed = 0.0;
        double CurrentSpeed = 0.0;
        //--------------------------------------------

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            ResetValues();
            IsRunning = true;

            PauseBtn.IsVisible = true;
            StartBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

            StartingPoint();
            StartRunning();
        }

        private void onPause_Clicked(object sender, EventArgs e)
        {

            IsRunning = false;

            PauseBtn.IsVisible = false;
            ResumeBtn.IsVisible = true;
            EndBtn.IsEnabled = true;
        }

        private async void EndBtn_ClickedAsync(object sender, EventArgs e)
        {

            IsRunning = false;

            StartBtn.IsVisible = true;
            PauseBtn.IsVisible = false;
            ResumeBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

            bool answer = await DisplayAlert("Great Run!", "Would you like to save it to history?", "Yes", "No");

            if (answer)
            {
                CalculationClass calculationsClass = new CalculationClass();
                FinalSpeed = calculationsClass.finalSpeed(FinalSpeed, hr, min, sec);
                try
                {
                    var db = App.database;

                    var tableItems = new ExerciseTable
                    {
                        DURATION = TimerLabel.Text,
                        DISTANCE = TimerDistance.Text,
                        AVGSPEED = FinalSpeed.ToString(),
                        DATETIME = DateTime.Now
                    };

                    await db.SaveItemAsync(tableItems);

                }
                catch (SQLiteException ex)
                {
                    TimerLabel.Text = ex.ToString();
                }
            }
        }

        private void ResumeBtn_Clicked(object sender, EventArgs e)
        {

            IsRunning = true;

            PauseBtn.IsVisible = true;
            ResumeBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

            StartRunning();
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

                loc = true;
            }
            catch (Exception ex)
            {
                TimerLabel.Text = ex.ToString();
            }

        }

        public void StartRunning()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {

                GetCurrentLocationAsync();

                if (loc == true)
                {
             
                    var list = new List<Position>(exerciseMap.RouteCoordinates);
                    list.Add(new Position(lat, lon));
                    exerciseMap.RouteCoordinates = list;
                    Calculations(lat, lon);

                    sec++;

                    if (sec % 30 == 0)
                    {
                        exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lon), Distance.FromKilometers(0.1)));
                    }

                    if (sec == 60)
                    {
                        min++;
                        sec = 0;
                    }
                    else if (min == 60)
                    {
                        hr++;
                        min = 0;
                    }
                    else if (hr == 24)
                    {
                        hr = 0;
                    }

                    TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";
                    return IsRunning; // True = Repeat again, False = Stop the timer
                }
                return IsRunning;
            });
        }

        public void Calculations(double nextLat, double nextLong)
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

            CurrentSpeed = calculationClass.currentSpeed(Distance2);
   
            TimerDistance.Text = "Distance: " + FinalDistance.ToString() + " Meters";
            TimerCurSpeed.Text = "Speed: " + CurrentSpeed.ToString() + " Km/h";
            TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";
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

                exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lon), Distance.FromKilometers(0.1)));
            }
            catch (Exception ex)
            {
                TimerLabel.Text = ex.ToString();
            }

        }

        public void ResetValues()
        {
            // Reset all values and labels for next run.
            hr = 0;
            min = 0;
            sec = 0;

            Distance2 = 0;
            FinalDistance = 0;
            FinalSpeed = 0;
            CurrentSpeed = 0;

            // ** TODO ***
            //DependencyService.Get<IMaphelper>().RemoveLines();


            exerciseMap.RouteCoordinates.Clear();

            TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";
            TimerDistance.Text = "Distance: " + FinalDistance.ToString() + " Meters";
            TimerCurSpeed.Text = "Speed: " + CurrentSpeed.ToString() + " Km/h";
        }
    }
}