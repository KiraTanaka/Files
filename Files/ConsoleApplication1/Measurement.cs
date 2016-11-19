using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class Measurement
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Ozone")]
        public int? Ozone { get; set; }
        [DisplayName("Solar.R")]
        public int? Solar { get; set; }
        [DisplayName("Wind")]
        public double? Wind { get; set; }
        [DisplayName("Temp")]
        public int? Temp { get; set; }
        [DisplayName("Month")]
        public int Month { get; set; }
        [DisplayName("Day")]
        public int Day { get; set; }
    }
}
