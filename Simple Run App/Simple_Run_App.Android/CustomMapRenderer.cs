using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using MapOverlay.Droid;
using Simple_Run_App;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(ExerciseMap), typeof(CustomMapRenderer))]
namespace MapOverlay.Droid
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        List<Position> routeCoordinates;
        ExerciseMap map;
        bool isDrawn;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (ExerciseMap)e.NewElement;
                routeCoordinates = formsMap.RouteCoordinates;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            base.OnElementPropertyChanged(sender, e);

            if ((e.PropertyName == "RouteCoordinates" || e.PropertyName == "VisibleRegion") && !isDrawn)
            {
                var polylineOptions = new PolylineOptions();
                polylineOptions.InvokeColor(0x66FF0000);

                var coordinates = ((ExerciseMap)Element).RouteCoordinates;

                foreach (var position in coordinates)
                    polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));

                NativeMap.AddPolyline(polylineOptions);
                isDrawn = coordinates.Count > 0;
            }
        }
    }
}