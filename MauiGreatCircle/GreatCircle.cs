using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiGreatCircle
{
    public class GreatCircle
    {
        public double DMS_Degrees(double deg, double min, double sec)
        {
            double answer = deg + (min / 60) + (sec / 3600);
            if (deg < 0)
                answer -= 1;
            return answer;
        }
        
        public string Degrees_DMS(double deg)
        {
            double Negation = Math.Sign(deg);
            
            double DecimalPart = Math.Truncate(deg);
            double IntegerPart = deg % 1;
            double Min = IntegerPart * 60;
            double MinDecimalPart = Math.Truncate(Min);
            double MinIntegerPart = Min % 1.0;
            double Sec = Math.Round(MinIntegerPart * 60.0, 1);
            double IntegerWithSign = Negation * IntegerPart;
            string result = $"{Negation * DecimalPart} {MinDecimalPart} {Sec}";
            return result;
        }
        public double GreatCircle_Calculation(double LatDeg1, double LongDeg1, double LatDeg2, double LongDeg2)
        {
            double lat1 = DegreesToRadians(LatDeg1);
            double long1 = DegreesToRadians(LongDeg1);
            double lat2 = DegreesToRadians(LatDeg2);
            double long2 = DegreesToRadians(LongDeg2);

            // d=acos(sin(lat1)*sin(lat2)+cos(lat1)*cos(lat2)*cos(lon1-lon2))

            double result = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(long1 - long2));
            double result2 = result * 3959;

            // A mathematically equivalent formula, which is less subject to rounding error for short distances is:

            //d = 2 * asin(sqrt((sin((lat1 - lat2) / 2)) ^ 2 +
            //                 cos(lat1) * cos(lat2) * (sin((lon1 - lon2) / 2)) ^ 2))



            return result2;
        }
        public string ViewableMileage_AtHeight(double heightFeet)
        {
            Console.WriteLine("Height above earth in feet");

            //double height = double.Parse(Console.ReadLine());
            double heightMiles = 0;
            heightMiles = (heightFeet / 5280) + 3959;
            double angleDegrees = (Math.Asin((3959 / heightMiles)) * (180 / Math.PI));
            angleDegrees = Math.Round(angleDegrees * 2, 1);
            double answer = Math.Round(180 - angleDegrees, 1);
            double visible = Math.PI * 2 * answer;
            //Console.WriteLine($"Viewing {answer} degrees of 360 in two directions");
            double EarthCircumference = Math.PI * 3959 * 2;
            double arch = answer / 360;
            answer = arch * EarthCircumference;
            answer = Math.Round(answer / 2, 0);
            // Console.WriteLine($"visible part {answer} miles in one direction.");
            //Console.ReadKey();
            return answer.ToString();

        }
        public double[] Get_AntiPodal(double latdeg, double latmin, double latsec, double longdeg, double longmin, double longsec)
        {
            double Lat = DMS_Degrees(latdeg, latmin, latsec);
            double Long = DMS_Degrees(longdeg, longmin, longsec);
            Lat = -Lat;
            if (Long > 0 && Long < 360)
                Long = Long - 180;
            else
                Long = Long + 180;
            return new double[] { Lat, Long };
        }

        public double GetDistantThroughEarth(double latDeg, double lngDeg, double latDeg2, double lngDeg2)
        {
            double lat1Radians = DegreesToRadians(latDeg);
            double lng1Radians = DegreesToRadians(lngDeg);
            double lat2Radians = DegreesToRadians(latDeg2);
            double lng2Radians = DegreesToRadians(lngDeg2);

            double X = Math.Cos(lat1Radians) * Math.Cos(lng1Radians);
            double Y = Math.Cos(lat1Radians) * Math.Sin(lng1Radians);
            double Z = Math.Sin(lat1Radians);

            double X2 = Math.Cos(lat2Radians) * Math.Cos(lng2Radians);
            double Y2 = Math.Cos(lat2Radians) * Math.Sin(lng2Radians);
            double Z2 = Math.Sin(lat2Radians);

            double result = Math.Sqrt(Math.Pow((X2 - X), 2) + Math.Pow((Y2 - Y), 2) + Math.Pow((Z2 - Z), 2));
            result = result * 3959;
            return result;
        }

        public double RadiansToDegrees(double radians)
        {
            return (180 / Math.PI) * radians;
        }


        public double DegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        public double CourseBetweenPoints(double distance, double latDeg, double lngDeg, double lat2Deg, double lng2Deg)
        {
            double lat1Radians = DegreesToRadians(latDeg);
            double lng1Radians = DegreesToRadians(lngDeg);
            double lat2Radians = DegreesToRadians(lat2Deg);
            double lng2Radians = DegreesToRadians(lng2Deg);

            double tc1 = 0;


            if (Math.Cos(lat1Radians) < 0.00010)
                if (lat1Radians > 0)
                    tc1 = Math.PI;
                else
                    tc1 = 2 * Math.PI;
            else if (Math.Sin(lng2Radians - lng1Radians) < 0)
            {
                tc1 = Math.Acos((Math.Sin(lat2Radians) - Math.Sin(lat1Radians) * Math.Cos(distance)) / (Math.Sin(distance) * Math.Cos(lat1Radians)));
            }
            else
                tc1 = 2 * Math.PI - Math.Acos((Math.Sin(lat2Radians) - Math.Sin(lat1Radians) * Math.Cos(distance)) / (Math.Sin(distance) * Math.Cos(lat1Radians)));
            double CourseDegrees = Math.Round(RadiansToDegrees(tc1), 0);

            return CourseDegrees;
        }

        public async System.Threading.Tasks.Task<string[]> Get_Cooridates(object sender, EventArgs e)
        {
            var coor = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(30)
            });
            var cooridates = new string[] { coor.Latitude.ToString(), coor.Longitude.ToString() };
            return cooridates;
        }

       

    }
}
