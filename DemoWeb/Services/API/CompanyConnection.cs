using DemoShared.Constants;
using DemoShared.Models.Entities;
using DemoWeb.Services.API.Repository;
using System.Net.Http.Json;

namespace DemoWeb.Services.API
{
    public interface ICompanyConnection : IRepositoryAPI<CompanyModel>
    {
        Task<List<CompanyModel>> getAllCompany();

    }

    public class CompanyConnection : RepositoryAPI<CompanyModel>, ICompanyConnection
    {
        private static readonly string controlURI = $"api/Company";
        private static readonly string ENDPOINT_GET_ALL = "getAll";
        private static readonly string ENDPOINT_SEARCH_BY_NAME = "searchByName";

        private readonly IHttpClientFactory _http;

        public CompanyConnection(IHttpClientFactory http) : base(http, controlURI)
        {
            _http = http;
        }

        /// <summary>
        /// get All employee
        /// </summary>
        /// <returns></returns>
        public async Task<List<CompanyModel>> getAllCompany()
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.GetAsync($"{controlURI}/{ENDPOINT_GET_ALL}");
                    List<CompanyModel> ret = new List<CompanyModel>();
                    if (res.IsSuccessStatusCode)
                    {
                        ret = await res.Content.ReadFromJsonAsync<List<CompanyModel>>();
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    return new List<CompanyModel>();
                }
            }
        }
    }
}
