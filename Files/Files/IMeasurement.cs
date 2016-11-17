using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    public interface IMeasurement
    {
        void SetAttribute(Dictionary<string, int> header, string[] line);
    }
}
