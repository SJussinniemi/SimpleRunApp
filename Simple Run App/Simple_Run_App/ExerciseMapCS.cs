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
        public ExerciseMapCS()
        {
            var customMap = new ExerciseMap
            {
                MapType = MapType.Street,
                WidthRequest = 320,
                HeightRequest = 200
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(customMap);
            Content = customMap;
        }
    }
}
