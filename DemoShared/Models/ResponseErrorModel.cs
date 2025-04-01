using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Models
{
    public class ResponseErrorModel
    {
        public int status { get; set; }
        public string error { get; set; }
        public string? id { get; set; }
    }
}
