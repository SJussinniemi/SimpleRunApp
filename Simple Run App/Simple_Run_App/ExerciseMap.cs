using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Simple_Run_App
{
    public class ExerciseMap : Map
    {
        public List<Position> RouteCoordinates { get; set; }

        public ExerciseMap()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}