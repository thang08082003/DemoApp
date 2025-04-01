using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Constants
{
    public class Common
    {
        public static readonly string PUBLIC = "public";
        public static readonly string SYSTEM_COPYRIGHT = "©2025 copyright";
        public static readonly string IS_RETIRED = "1";

        public class Label
        {
            public const string NAME_MAIN = "Technology DB System";
            public const string LOGIN = "Login";
            public const string EDIT = "Edit";
            public const string DELETE = "Delete";
            public const string ADD = "Add";
            public const string LOAD_IMP = "Load Imp.";
            public const string CANCEL = "Cancel";
            public const string LOG_OUT = "Sign Out";
            public const string DELETE_TITLE = "Delete";
            public const string INSERT = "Insert";
            public const string UPDATE = "Update";
            public const string UPLOAD_IMG = "Upload Icon";
            public const string REFERRENCE = "Reference...";
            public const string CHANGE = "Change";
        }
        public class Header
        {
            public const string EMPLOYEE_MASTER = "Master Management";
            public const string EMPLOYEE_MASTER_MANAGEMENT = "Engineer Master Management";
            public const string INFORMATION = "Information Management";
            public const string EMPLOYEE_INFORMATION = "Engineer Information";
            public const string NEW_ADDITION = "New Addition";
            public const string CHANGE_PASSWORD = "Change Password";
        }
        public class Action
        {
            public const string UPDATE = "UPDATE";
            public const string INSERT = "INSERT";
            public const string DELETE = "DELETE";
            public const string UPDATE_TITLE = "Update";
            public const string INSERT_TITLE = "Insert";
            public const string DELETE_TITLE = "Delete";
            public const string EDIT_TITLE = "Edit";
            public const string ADD_TITLE = "Add";
            public const string ADD_NEW_TITLE = "New Addition";
            public const string GET = "Get";
        }

        public class GENDER
        {
            public static readonly (string name, string value) SELECT = new("Select", "");
            public static readonly (string name, string value) MALE = new("Male", "0");
            public static readonly (string name, string value) FEMALE = new("Female", "1");
        }
        public class Character
        {
            public const int ONE_CHARACTER = 0;
            public const string DOUBLE_DOT = ":";
            public const string SPACE_HALF_WIDTH = " ";
        }
        public const int LIST_EMPTY = 0;

        public class TimeZone
        {
            public const string JAPAN_TIME_ZONE = "Tokyo Standard Time";
        }

        public class Role
        {
            public const string ADMIN = "admin";
            public const string USER = "user";
        }

        public class JSFunc
        {
            public const string UPLOAD_TRIGGER = "uploadTrigger";
        }

        public class DateFormat
        {
            public const string YYYY_MM_DD_FORMAT = "yyyy/MM/dd";
        }
    }
}