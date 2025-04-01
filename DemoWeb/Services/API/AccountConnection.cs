using DemoShared.Constants;
using DemoShared.Models.Entities;
using DemoWeb.Services.API.Repository;
using System.Net.Http.Json;
using DemoShared.Models.Parameter;
using DemoShared.Models.DTO;

namespace DemoWeb.Services.API
{
    public interface IAccountConnection : IRepositoryAPI<AccountModel>
    {
        Task<EmployeeDTO> getUserLogin(string empId);
        Task<bool> isExistAccount(string userName, string passWord);
        Task<AccountModel> getIdByUserName(string userName, string passWord);
        Task<AccountModel> getPattById(string empId);
    }

    public class AccountConnection : RepositoryAPI<AccountModel>, IAccountConnection
    {
        private static readonly string controlURI = $"api/Account";
        private static readonly string ENDPOINT_GET_USER = "getUser";
        private static readonly string ENDPOINT_CHK_EXIST = "isExist";
        private static readonly string ENDPOINT_GET_ID = "getIdByUserName";
        private static readonly string ENDPOINT_GET_PATT = "getPatt";

        private readonly IHttpClientFactory _http;

        public AccountConnection(IHttpClientFactory http) : base(http, controlURI)
        {
            _http = http;
        }

        public async Task<EmployeeDTO> getUserLogin(string empId)
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.GetAsync($"{controlURI}/{ENDPOINT_GET_USER}/{empId}");
                    EmployeeDTO ret = new EmployeeDTO();
                    if (res.IsSuccessStatusCode)
                    {
                        ret = await res.Content.ReadFromJsonAsync<EmployeeDTO>();
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    return new EmployeeDTO();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public async Task<bool> isExistAccount(string userName, string passWord)
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    AccountParam param = new AccountParam();
                    param.UserName = userName;
                    param.PassWord = passWord;
                    var res = await _http.PostAsJsonAsync($"{controlURI}/{ENDPOINT_CHK_EXIST}", param);
                    bool ret = false;
                    if (res.IsSuccessStatusCode)
                    {
                        ret = await res.Content.ReadFromJsonAsync<bool>();
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public async Task<AccountModel> getIdByUserName(string userName, string passWord)
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    AccountParam param = new AccountParam();
                    param.UserName = userName;
                    param.PassWord = passWord;
                    var res = await _http.PostAsJsonAsync($"{controlURI}/{ENDPOINT_GET_ID}", param);
                    AccountModel ret = new AccountModel();
                    if (res.IsSuccessStatusCode)
                    {
                        ret = await res.Content.ReadFromJsonAsync<AccountModel>();
                    }
                    return ret;

                }
                catch (Exception ex)
                {
                    return new AccountModel();
                }
            }
        }

        public async Task<AccountModel> getPattById(string empId)
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.GetAsync($"{controlURI}/{ENDPOINT_GET_PATT}/{empId}");
                    AccountModel ret = new AccountModel();
                    if (res.IsSuccessStatusCode)
                    {
                        ret = await res.Content.ReadFromJsonAsync<AccountModel>();
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    return new AccountModel();
                }
            }
        }
    }
}
