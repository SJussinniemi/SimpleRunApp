using System;
using System.Collections.Generic;
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
        public Exercise()
        {
            InitializeComponent();
        }

        private void onStart(object sender, EventArgs e)
        {
            PauseBtn.IsVisible = true;
            StartBtn.IsVisible = false;
            EndBtn.IsEnabled = false;
        }

        private void onPause(object sender, EventArgs e)
        {
            PauseBtn.IsVisible = false;
            StartBtn.IsVisible = true;
            EndBtn.IsEnabled = true;
        }
    }
}
