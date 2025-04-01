using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Constants
{
    public class Messages
    {
        public class Question
        {
            public static readonly string MSGQ0001 = "Can I delete it?";
        }
        public class Error
        {
            public static string MSGE9002(string item) => $"A system error has occurred. Please contact your system administrator. {item}";
            public static readonly string MSGE0001 = "Login failed.";
            public static readonly string MSGE0002 = "The data in the file is invalid";
            public static string MSGE0007(string item) => $"The {item} you entered does not exist";
        }
        public class Information
        {
            public static string MSGI0003(string item) => $"Please enter {item}.";
        }
    }
}
