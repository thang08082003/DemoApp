using CsvHelper;
using DemoShared.Constants;
using System.Globalization;

namespace DemoWeb.Services.API
{
    public interface ICSVFileConnection
    {
        Task<IEnumerable<T>> ReadCSV<T>(Stream file);

        public class CSVFileConnection : ICSVFileConnection
        {
            private static readonly string controlURI = $"api/FileUpload";
            private static readonly string ENDPOINT_GET_FILE = "getFile";

            private readonly IHttpClientFactory _http;

            public CSVFileConnection(IHttpClientFactory http)
            {
                _http = http;
            }
            public Task<IEnumerable<T>> ReadCSV<T>(Stream file)
            {
                using (var _http = this._http.CreateClient(Common.PUBLIC))
                {
                    var reader = new StreamReader(file);
                    var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                    var records = csv.GetRecords<T>();
                    return (Task<IEnumerable<T>>)records;
                }
                    
            }
        }
    }
}
