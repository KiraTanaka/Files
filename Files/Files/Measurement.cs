using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    class HelpAttribute : Attribute
    {
        public string HelpText { get; set; }

    }
    internal class Measurement : IMeasurement
    {
        public string Name { get; set; }
        public int? Ozone { get; set; }
        public int? Solar { get; set; }
        public double? Wind { get; set; }
        public int? Temp { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public void SetAttribute(Dictionary<string, int> header, string[] line)
        {
            Name = line[header["Name"]];
            Ozone = int.Parse(line[header["Ozone"]]);
            Solar = int.Parse(line[header["Solar.R"]]);
            Wind = double.Parse(line[header["Wind"]]);
            Temp = int.Parse(line[header["Temp"]]);
            Month = int.Parse(line[header["Month"]]);
            Day = int.Parse(line[header["Day"]]);
        }
    }
}
