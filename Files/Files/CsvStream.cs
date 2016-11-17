using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        internal IEnumerable<T> ReadCsv2<T>() where T : IMeasurement, new()
        {
            List<string[]> data = ReadCsv1().ToList();
            Dictionary<string, int> head = new Dictionary<string, int>();
            int index = 0;
            data.First().ToList().ForEach(
                value => { head.Add(value, index); }
                );
            data.RemoveAt(0);
            foreach (var line in data)
            {
                T objectT = new T();
                objectT.SetAttribute(head, line);
                yield return objectT;
            }
        }
    }
}
