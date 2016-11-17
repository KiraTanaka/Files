using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    internal class DynamicMeasurement:IMeasurement
    {
        public dynamic Name { get; set; }
        public dynamic Ozone { get; set; }
        public dynamic Solar { get; set; }
        public dynamic Wind { get; set; }
        public dynamic Temp { get; set; }
        public dynamic Month { get; set; }
        public dynamic Day { get; set; }
        public void SetAttribute(Dictionary<string, int> header, string[] line)
        {
            Name = line[header["Name"]];
            Ozone = line[header["Ozone"]];
            Solar = line[header["Solar.R"]];
            Wind = line[header["Wind"]];
            Temp = line[header["Temp"]];
            Month = line[header["Month"]];
            Day = line[header["Day"]];
        }
    }
}
