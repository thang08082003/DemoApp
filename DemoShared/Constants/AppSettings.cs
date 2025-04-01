using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Constants
{
    public class AppSettings
    {
        public class ConnectionString
        {
            // Use this for SQL DB
            public static readonly string DB_CONNECTION = "DbConnection";
            // Use this for Blob Storage
            public static readonly string STORAGE_CONNECTION_BLOB = "StorageConnection";
            public static readonly string STORAGE_CONNECTION_QUEUE = "StorageConnection";
            // Use this for AppInsight (Instrumentation Key)
            public static readonly string APPINSIGHT_CONNECTION = "AppInsightConnection";
        }

        public class AzureAD
        {
            public static readonly string ADMIN_GROUP_NAME = "AzureAD:AdminGroupName";
            public static readonly string MANAGER_GROUP_NAME = "AzureAD:ManagerGroupName";
            public static readonly string USER_GROUP_NAME = "AzureAD:UserGroupName";
            public static readonly string INSTANCE = "AzureAD:Instance";
            public static readonly string TENANT_ID = "AzureAD:TenantId";
            public static readonly string CLIENT_ID = "AzureAD:ClientId";
            public static readonly string CLIENT_SECRET = "AzureAD:ClientSecret";
            public static readonly string API_KEY = "AzureAD:ApiKey";
            public static readonly string API_SCOPE = "AzureAD:ApiScope";
        }
    }
}
