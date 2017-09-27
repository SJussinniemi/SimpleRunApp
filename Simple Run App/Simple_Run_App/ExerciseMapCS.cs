using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Simple_Run_App
{
    public class ExerciseMapCS : ContentPage
    {
        public MapPageCS()
        {
            var customMap = new ExerciseMap
            {
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight
            };
        Content = customMap;
        }
    }
}
