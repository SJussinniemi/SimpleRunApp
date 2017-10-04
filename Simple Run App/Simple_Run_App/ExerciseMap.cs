using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Simple_Run_App
{
    public class ExerciseMap : Map
    {
        public static readonly BindableProperty RouteCoordinatesProperty =
        BindableProperty.Create(nameof(RouteCoordinates), typeof(List<Position>), typeof(ExerciseMap), new List<Position>(), BindingMode.TwoWay);



        public List<Position> RouteCoordinates
        {
            get { return (List<Position>)GetValue(RouteCoordinatesProperty); }
            set { SetValue(RouteCoordinatesProperty, value); }
        }

        public ExerciseMap()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}