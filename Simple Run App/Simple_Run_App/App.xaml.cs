﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Simple_Run_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Simple_Run_App.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Exercise exercise = new Exercise();
            exercise.GetCurrentLocationAsync();
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
