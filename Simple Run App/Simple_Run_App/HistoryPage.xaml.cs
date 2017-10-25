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
        int aux;
        int i;
        public HistoryPage()
        {
            
            InitializeComponent();
            //ExerciseView.IsEnabled = false;
            aux = 0;
        }
        private async void ShowHistory(object sender, EventArgs e)
        {
            var db = App.database;
            BtnHistory.Text = "Refresh/close History";
            DeleteBtn.IsEnabled = true;
            entryID.IsEnabled = true;
            try
            {
                var show = new List<ExerciseTable>();
                show = await db.GetItemsAsync();
                NumElements.Text = "Count of rows in list: " + show.Count().ToString();
                //StackLayout s1 = new StackLayout();
                if (aux == show.Count())
                {
                    InitializeComponent();
                    aux = 0;
                }
                else
                {
                    for (i = aux; i < show.Count(); i++)// Creatting tje Labels
                    {
                        historyPage.Children.Add(new Label { Text = "Exercise number: " + show[i].ID + " - Delete Code: " + i + "\n Date: " + show[i].DATETIME.ToString() + "\n " + show[i].DURATION + "\n " + show[i].DISTANCE + "\n " + show[i].AVGSPEED });
                        
                    }
                    Content = new ScrollView { Content = historyPage };
                    //Content = historyPage;
                    aux = i;// If the user add a new Exercise history, the next time when i press this btn the for starts in the las position and prints only the last additions
                }
            }
            catch (Exception ex)
            {
                NumElements.Text = ex.ToString();
            }
        }
        private async void DelateExer(object sender, EventArgs e)
        {

            int IDExer = int.Parse(entryID.Text); ; //The Exercise ID that the user wants to delate

            var db = App.database;
            var list = new List<ExerciseTable>();
            list = await db.GetItemsAsync();
            if (IDExer < 0 || IDExer >= list.Count())
            {
                await DisplayAlert("Sorry!!", "The Delete Code doesn't Exist, please put another one", "ok");
            }
            else
            {
                aux = 0;
                await db.DeleteItemAsync(list[IDExer]); //Delate this list frome the dataBase. 
                InitializeComponent();
            }
        }
    }
}
