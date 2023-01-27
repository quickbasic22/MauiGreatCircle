using System;
using System.Collections.Generic;
using System.Text;

namespace MauiGreatCircle.Model
{
    public class LocationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public override string ToString()
        {
            return Name + " " + Latitude + " " + Longitude;
        }

    }
}
