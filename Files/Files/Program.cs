using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "airquality.csv");
            CsvStream stream = new CsvStream();
            var windValues = stream.ReadCsv4(path).Where(z=>z.Ozone>10).Select(z=>z.Wind);
            Console.ReadLine();
        }
    }
}
