using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple_Run_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercise : ContentPage
    {
        public String DataSend;
        Boolean pause = false;
        int hr = 0;
        int min = 0;
        int sec = 0;
        int cont = 0;//Only For testting 

        // ---------- Vars Total Distance -------------
        double lat1 = 0.0, long1 = 0.0;//At the beginning initialize with the actual position
        double lat2 = 0.0, long2 = 0.0;
        double Distance = 0.0, FinalDistance = 0.0;//Distance =>Distance between two points; FinalDistance => Total Distance in the exercise.
        //--------------------------------------------
        // --------Vars Final Speed ------------------
        double FinalSpeed = 0.0;
        //--------------------------------------------

        public List<History> RunHistory = new List<History>();//At the beginning the list is empty
        public Exercise()
        {
            InitializeComponent();

            TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";
      
        }

        private void End_Button(object sender, EventArgs e)
        {
            CalculationClass calculationClass = new CalculationClass();
            FinalSpeed = calculationClass.finalSpeed(FinalDistance,hr, min, sec);

            TimerDistance.Text = "Distance: " + FinalDistance.ToString();
            TimerCurSpeed.Text = "Speed " + FinalSpeed.ToString();

            string infoTime = TimerLabel.Text;
            string infoDistance = TimerDistance.Text;
            string infoSpeed = TimerCurSpeed.Text;

            History run = new History(infoTime, infoDistance, infoSpeed);
            RunHistory.Add(run);
            if (!pause)
            {
                hr = 0;
                min = 0;
                sec = 0;
                TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";
            }
        }
        private void onPause(object sender, EventArgs e)
        {
            pause = false;

            PauseBtn.IsVisible = false;
            StartBtn.IsVisible = true;
            EndBtn.IsEnabled = true;
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            pause = true;

            PauseBtn.IsVisible = true;
            StartBtn.IsVisible = false;
            EndBtn.IsEnabled = false;

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
                return pause; // True = Repeat again, False = Stop the timer
            });
        }

        public void TotalDistance(double nextLat, double nextLong)
        {
            lat2 = nextLat;
            long2 = nextLong;
            CalculationClass calculationClass = new CalculationClass();
            Distance = calculationClass.DistanceBtwnTwoPnts(lat1, long1, lat2, long2);
            FinalDistance = FinalDistance + Distance;
            Distance = 0;
            //Now the second point is the first one and we wait for the coordinates of the next point
            lat1 = lat2;
            long1 = long2;
        }
       
    }
}