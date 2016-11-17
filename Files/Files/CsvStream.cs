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
            using (var stream = new StreamReader(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "airquality.csv")))
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
            data.First().ToList().ForEach(value => head.Add(value, index++));
            data.RemoveAt(0);
            foreach (var line in data)
            {
                T objectT = new T();
                objectT.SetAttribute(head, line);
                yield return objectT;
            }
        }
        internal IEnumerable<Dictionary<string, object>> ReadCsv3()
        {
            /*Сделайте метод ReadCsv3 (без generic-параметра), 
             * который бы возвращал ленивое перечисление Dictionary<string,object>. 
             * Каждый такой Dictionary будет состоять из пар (name,value), где name - имя 
             * соответствующего поля, а value - его значение после конвертации в int, double или string.
             *  NA следует конвертировать в null.*/
            int index = 0;
            List<string[]> data = ReadCsv1().ToList();
            List<string> head = new List<string>();
            head = data.First().ToList();
            data.RemoveAt(0);
            foreach (var line in data)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add(head[index], line[index++]);
                dictionary.Add(head[index], int.Parse(line[index++]));
                dictionary.Add(head[index], int.Parse(line[index++]));
                dictionary.Add(head[index], double.Parse(line[index++]));
                dictionary.Add(head[index], int.Parse(line[index++]));
                dictionary.Add(head[index], int.Parse(line[index++]));
                dictionary.Add(head[index], int.Parse(line[index++]));
                yield return dictionary;
            }
        }
    }
}
