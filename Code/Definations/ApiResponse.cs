using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovenseControl
{
    public class ApiResponse
    {
        public string code { get; set; }
        public Dictionary<string, Toy> data { get; set; }
        public string type { get; set; }
    }
}
