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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            //ExerciseView.IsEnabled = false;
        }
        private async void ShowHistory(object sender, EventArgs e)
        {
            var db = App.database;
            //ExerciseView.IsEnabled = true;
            try
            {
                var show = new List<ExerciseTable>();
                show = await db.GetItemsAsync();
                NumElements.Text = "Count of rows in list: " + show.Count().ToString();
                //StackLayout s1 = new StackLayout();
                for (int i=0; i<show.Count(); i++)
                {
                    //Values.Text = show[i].ID + ": " + show[i].DATETIME.ToString() + " " + show[i].DURATION + " " + show[i].DISTANCE + " " + show[i].AVGSPEED;
                 
                    //label.Text = show[i].ID + ": " + show[i].DATETIME.ToString() + " " + show[i].DURATION + " " + show[i].DISTANCE + " " + show[i].AVGSPEED;
                    historyPage.Children.Add(new Label {Text= show[i].ID + ": " + show[i].DATETIME.ToString() + " " + show[i].DURATION + " " + show[i].DISTANCE + " " + show[i].AVGSPEED});
                }
                Content = historyPage;
            }
            catch (Exception ex)
            {
                NumElements.Text = ex.ToString();
            }
        }
    }
}
