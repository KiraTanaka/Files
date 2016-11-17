using System;
using System.Collections.Generic;
using System.Globalization;
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
        internal IEnumerable<string[]> ReadCsv1(string path)
        {
            using (var stream = new StreamReader(path))
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
                        return (x == "NA") ? null : x.Replace("\"", "");
                    }).ToArray();
                }
            }
        }
        internal IEnumerable<T> ReadCsv2<T>(string path) where T : IMeasurement, new()
        {
            List<string[]> data = ReadCsv1(path).ToList();
            Dictionary<string, int> head = GetHeader(data);
            data.RemoveAt(0);
            foreach (var line in data)
            {
                T objectT = new T();
                objectT.SetAttribute(head, line);
                yield return objectT;
            }
        }
        internal IEnumerable<Dictionary<string, object>> ReadCsv3(string path)
        {           
            List<string[]> data = ReadCsv1(path).ToList();
            List<string> head = new List<string>();
            head = data.First().ToList();
            data.RemoveAt(0);
            foreach (var line in data)
            {
                int index = 0;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add(head[index], line[index++]);
                dictionary.Add(head[index], int.Parse(line[index++] ?? "0"));
                dictionary.Add(head[index], int.Parse(line[index++] ?? "0"));
                dictionary.Add(head[index], double.Parse(line[index++] ?? "0", CultureInfo.InvariantCulture));
                dictionary.Add(head[index], int.Parse(line[index++] ?? "0"));
                dictionary.Add(head[index], int.Parse(line[index++]));
                dictionary.Add(head[index], int.Parse(line[index++]));
                yield return dictionary;
            }
        }
        internal IEnumerable<dynamic> ReadCsv4(string path)
        {
            List<string[]> data = ReadCsv1(path).ToList();
            Dictionary<string, int> head = GetHeader(data);
            data.RemoveAt(0);
            foreach (var line in data)
            {
                dynamic dynamicObject = new DynamicMeasurement();
                dynamicObject.SetAttribute(head, line);
                yield return dynamicObject;
            }
        }
        private Dictionary<string, int> GetHeader(List<string[]> data)
        {
            Dictionary<string, int> head = new Dictionary<string, int>();
            int index = 0;
            data.First().ToList().ForEach(value => head.Add(value, index++));
            return head;
        }
    }
}
