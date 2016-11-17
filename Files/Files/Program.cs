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
            CsvStream stream = new CsvStream();
            //List<string[]> list=stream.ReadCsv1().ToList();
            stream.ReadCsv2<Measurementt>();            
        }
    }
}
