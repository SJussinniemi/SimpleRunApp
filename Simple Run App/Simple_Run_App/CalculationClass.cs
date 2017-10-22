using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Run_App
{
    public class CalculationClass
    {
        double R = 6371.0 * 1000;//Earth's radius in metres
        double FirstFormula = 0.0;
        double SecondFormula = 0.0;
        //----- For calculate the speed --------------
        double Fspd = 0;
        double Cspd = 0;
        //--------------------------------------------
        public double DistanceBtwnTwoPnts(double lati1, double longi1, double lati2, double longi2)
        {
            double SupLat = ((lati2 - lati1) * Math.PI) / 180.0;//Transformation from grade to radians
            double SupLong = ((longi2 - longi1) * Math.PI) / 180.0;
            FirstFormula = (Math.Sin(SupLat / 2) * Math.Sin(SupLat / 2)) + Math.Cos(SupLat) * Math.Cos(SupLong) * (Math.Sin(SupLong / 2) * Math.Sin(SupLong / 2));
            SecondFormula = 2 * Math.Atan2(Math.Sqrt(FirstFormula), Math.Sqrt(1 - FirstFormula));
            double Distance = Math.Round(R * SecondFormula, 2);
            return Distance;
        }

        public double finalSpeed(double distnc, int h, int m, int s)
        {
            double T = (3600.0 * h) + (60.0 * m) + s;
            T = (T / 3600.0);// Becouse T is in sec and we need hours
            double D = (distnc / 1000);// Because distnc is in metres and we need Km
            Fspd = Math.Round(D / T , 2);//Speed in km/h
            return Fspd;
        }

        public double currentSpeed(double distance)
        {
            double T = 1;// Period in the Device.StartTimer(...) TODO CHANGE THIS
            T = (T / 3600.0);
            double D = (distance / 1000);
            Cspd = Math.Round(D / T , 2);
            return Cspd;
        }
    }
}