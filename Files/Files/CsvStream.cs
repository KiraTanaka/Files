using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    internal class CsvStream
    {
        internal IEnumerable<string[]> ReadCsv1()
        {
            using (var stream = new StreamReader(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName,"airquality.csv")))
            {
                while (true)
                {
                    var lineValues = stream.ReadLine();
                    if (lineValues == null)
                    {
                        stream.Close();
                        yield break;
                    }
                    yield return lineValues.Split(',').ToList().Select(x =>
                    {
                        x = (x == "NA") ? null : x;
                        return x;
                    }).ToArray();
                }
            }      
        }

    }
}
