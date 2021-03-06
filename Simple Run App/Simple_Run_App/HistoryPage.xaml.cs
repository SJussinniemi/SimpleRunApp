﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple_Run_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {

        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var db = App.database;
            var show = new List<ExerciseTable>();
            show = await db.GetItemsAsync();

            try
            {
                for (int i = 0; i < show.Count(); i++)
                {
                    //We show the elements inside the database, we create a button to delete it when the user whats
                    int elem = i;
                    historyPage.Children.Add(new Label { FontSize = 16, Text = " Exercise number: " + show[i].ID + "\n Date: " + show[i].DATETIME.ToString() + "\n " + show[i].DURATION + "\n " + show[i].DISTANCE + "\n Average Speed: " + show[i].AVGSPEED + " Km/h" });
                    historyPage.Children.Add(new Button
                    {
                        Text = "Delete",
                        Command = new Command(async () => { //the function of our button

                            bool answer = await DisplayAlert("Notice!", "Are you sure you want to delete this exercise?", "Yes", "No");
                            if (answer)
                            {
                                await db.DeleteItemAsync(show[elem]);  //delate de Exercise                            
                            }

                        })
                    });
                }

                Content = new ScrollView { Content = historyPage };

            }
            catch (Exception ex)
            {
                debug.Text = ex.ToString();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            historyPage.Children.Clear();
        }

    }
}
