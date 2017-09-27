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
        public Exercise()
        {
            InitializeComponent();
            TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";
        }

        private void End_Button(object sender, EventArgs e)
        {
            //DataSend = TimerLabel.Text+"\n"+TimerDistance.Text+"\n"+TimerCurSpeed.Text;
            //HistoryPage.Values(DataSend);
            if (!pause)
            {
                min = 0;
                sec = 0;
                TimerLabel.Text = hr.ToString() + " h: " + min.ToString() + " m: " + sec.ToString() + " s";
            }
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            if (pause)
                pause = false;
            else
                pause = true;
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
    }
}