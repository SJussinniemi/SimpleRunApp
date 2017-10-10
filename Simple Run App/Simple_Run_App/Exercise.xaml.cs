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

            Device.BeginInvokeOnMainThread(() =>
            {
                exerciseMap.RouteCoordinates = new List<Position>
                {
                    // Hard coded triangle for test use atm
                    //new Position(60.977594, 24.476400),
                    //new Position(60.976042, 24.475240),
                    //new Position(60.976271, 24.477826),
                    //new Position(60.977594, 24.476400)
                };
            });

        }

        double HAMKLatitude = 60.9769334;
        double HAMKLongitude = 24.475909600000023;
        double lat; // Location latitude
        double lon; // Location longitude
        Boolean IsRunning; // For Device timer handling
        
        int i = 0; // For develop purposes. Tracks How Many times Device.StartTimer has looped.
        

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            // Get starting position WOIP perhaps Loading screen later.
            GetCurrentLocationAsync();

            PauseBtn.IsVisible = true;
            StartBtn.IsVisible = false;
            EndBtn.IsEnabled = true;
            IsRunning = true;
            StartRunning();


        }

        private void onPause_Clicked(object sender, EventArgs e)
        {
            PauseBtn.IsVisible = false;
            ResumeBtn.IsVisible = true;
            EndBtn.IsEnabled = true;

            IsRunning = false;
        }

        private void drawButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string path = DependencyService.Get<IFileHelper>().GetLocalFilePath("SimpleRunAppDB.db3");
                CurLocLatitude.Text = path;
                CreateDataBase();
                    
            }
            catch(Exception ex)
            {
                CurLocLongitude.Text = ex.ToString();
            }

        }

        private string CreateDataBase()
        {
            try
            {
                string path = DependencyService.Get<IFileHelper>().GetLocalFilePath("SimpleRunAppDB.db3");
                var db = new SQLiteConnection(path);

                db.CreateTable<ExerciseTable>();
                
                if(db.Table<ExerciseTable>().Count() == 0) 
                {
                    var poyta = new ExerciseTable();
                    poyta.ID = 1;
                    poyta.Test = "Heres some data";
                    db.Insert(poyta);

                }

                var dbreader = db.Get<ExerciseTable>(1);
                Ticktimes.Text = dbreader.Test;
                db.Close();
                return CurLocLongitude.Text = "Database Created";

            }catch(Exception ex)
            {
                return CurLocLongitude.Text = ex.ToString();
            }
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
            }
            catch(Exception ex)
            {
                CurLocLatitude.Text = ex.ToString();
            }
            
        }

        private async void EndBtn_ClickedAsync(object sender, EventArgs e)
        {
            StartBtn.IsVisible = true;
            PauseBtn.IsVisible = false;
            ResumeBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

            IsRunning = false;
            CurLocLatitude.Text = "End Button pressed";
            i = 0;
            var answer = await DisplayAlert("Great Run!", "Would you like to save it to history?", "Yes", "No");
            CurLocLongitude.Text = answer.ToString();

        }

        private void ResumeBtn_Clicked(object sender, EventArgs e)
        {
            PauseBtn.IsVisible = true;
            ResumeBtn.IsVisible = false;

            IsRunning = true;
            StartRunning();
        }

        public void StartRunning()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () => {

                GetCurrentLocationAsync();

                var list = new List<Position>(exerciseMap.RouteCoordinates);
                list.Add(new Position(lat, lon));
                exerciseMap.RouteCoordinates = list;

                Ticktimes.Text = "Device Timer ticks :" + i.ToString();
                i++;

                exerciseMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lon), Distance.FromKilometers(0.1)));
                return IsRunning;

            });
        }
    }
}
