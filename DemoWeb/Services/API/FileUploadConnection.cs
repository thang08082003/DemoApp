using BlazorInputFile;
using DemoShared.Constants;
using DemoShared.Models.Parameter;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DemoWeb.Services.API
{
    public interface IFileUploadConnection
    {
        Task<string> FileUpload(byte[] UserImages, string UrlImag);

        public class FileUploadConnection : IFileUploadConnection
        {

            private static readonly string controlURI = $"api/FileUpload";
            private static readonly string ENDPOINT_GET_FILE = "getFile";

            private readonly IHttpClientFactory _http;

            public FileUploadConnection(IHttpClientFactory http)
            {
                _http = http;
            }

            public async Task<string> FileUpload(byte[] UserImages, string UrlImage)
            {
                FileUpload imgParam = new FileUpload();
                imgParam.UserImage = UserImages;
                imgParam.ImageName = UrlImage;
                using (var _http = this._http.CreateClient(Common.PUBLIC))
                {
                    try
                    {
                        var res = await _http
                        .PostAsJsonAsync<FileUpload>($"{controlURI}/{ENDPOINT_GET_FILE}", imgParam);
                        if (!res.IsSuccessStatusCode)
                        {
                            return null;
                        }
                        return $@"\images\{UrlImage}";
                    }
                    catch (Exception ex)
                    {
                        return null;

                    }
                }
            }
        }


    }
}
