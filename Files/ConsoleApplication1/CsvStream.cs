using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
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
                    yield return lineValues.Split(',').Select(x =>
                    {
                        return (x == "NA") ? null : x.Replace("\"", "");
                    }).ToArray();
                }
            }
        }
        internal IEnumerable<T> ReadCsv2<T>(string path) where T : new()
        {
            var data = ReadCsv1(path);
            Dictionary<string, int> header = GetHeader(data);
            bool isHeader = true;
            foreach (var line in data)
            {
                if (isHeader)
                    isHeader = false;
                else
                {
                    T objectT = new T();
                    foreach (var property in objectT.GetType().GetProperties())
                    {
                        var value = line[header[property.GetCustomAttribute<DisplayNameAttribute>().DisplayName]];
                        if (property.PropertyType == typeof(int?) || property.PropertyType == typeof(int))
                            property.SetValue(objectT, int.Parse(value ?? "0"));
                        else if (property.PropertyType == typeof(double?) || property.PropertyType == typeof(double))
                            property.SetValue(objectT, double.Parse(value ?? "0", CultureInfo.InvariantCulture));
                        else
                            property.SetValue(objectT, value);
                    }
                    yield return objectT;
                }
            }
        }
        internal IEnumerable<Dictionary<string, object>> ReadCsv3(string path)
        {
            var data = ReadCsv1(path);
            List<string> header = data.First().ToList();
            bool isHeader = true;
            foreach (var line in data)
            {
                if (isHeader)
                    isHeader = false;
                else
                {
                    int indexLine = 0;
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    foreach (var value in line)
                    {
                        dictionary.Add(header[indexLine], Parse(value));
                        indexLine++;
                    }
                    yield return dictionary;
                }
            }
        }
        internal IEnumerable<dynamic> ReadCsv4(string path)
        {
            var data = ReadCsv1(path);
            List<string> header = data.First().ToList();
            bool isHeader = true;
            foreach (var line in data)
            {
                if (isHeader)
                    isHeader = false;
                else
                {
                    dynamic dynamicObject = new ExpandoObject();
                    //если прописывать четко поля name,ozone и т.д., то это уже опять привязка к данным, поэтому Dictionary
                    //или просто dynamic dynamicObject = new Dictionary<dynamic, dynamic>();
                    //-------------------------------------------
                    dynamicObject.dictionary = new Dictionary<dynamic, dynamic>();
                    int indexLine = 0;
                    foreach (var value in line)
                    {
                        dynamicObject.dictionary.Add(header[indexLine], Parse(value));
                        indexLine++;
                    }
                    //-------------------------------------------
                    //но если всё таки нужно с четко прописанными полями, то
                    /*
                        int indexLine = 0;
                        dynamicObject.Name = Parse(line[indexLine++]);
                        dynamicObject.Ozone = Parse(line[indexLine++]);
                        dynamicObject.Solar = Parse(line[indexLine++]);
                        dynamicObject.Wind = Parse(line[indexLine++]);
                        dynamicObject.Temp = Parse(line[indexLine++]);
                        dynamicObject.Month = Parse(line[indexLine++]);
                        dynamicObject.Day = Parse(line[indexLine]);
                     */
                    yield return dynamicObject;
                }
            }
        }
        private Dictionary<string, int> GetHeader(IEnumerable<string[]> data)
        {
            Dictionary<string, int> head = new Dictionary<string, int>();
            int index = 0;
            data.First().ToList().ForEach(value => head.Add(value, index++));
            return head;
        }
        private object Parse(string value)
        {
            int intValue;
            double doubleValue;
            if (int.TryParse(value ?? "0", out intValue))
                return intValue;
            else if (double.TryParse(value ?? "0", NumberStyles.Number, CultureInfo.InvariantCulture, out doubleValue))
                return doubleValue;
            else
                return value;
        }
    }
}
