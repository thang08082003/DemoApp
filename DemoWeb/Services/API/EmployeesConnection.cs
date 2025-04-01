using DemoShared.Constants;
using DemoShared.Models.CallBackModels;
using DemoShared.Models.DTO;
using DemoShared.Models.Entities;
using DemoWeb.Services.API.Repository;
using System.Net.Http.Json;

namespace DemoWeb.Services.API
{
    public interface IEmployeesConnection : IRepositoryAPI<EmployeesModel>
    {
        Task<List<EmployeesModel>> getAllEmployee();
        Task<List<EmployeesModel>> SearchEmployee(string name);
        Task<bool> isExistEmployee(List<EmployeesModel> employeeLst);
        Task<ErrorResBoolModel> DeleteItems(List<EmployeesModel> employeeLst);
        Task<ErrorResBoolModel> CreateItems(List<EmployeesModel> employeeLst);
        Task<ErrorResBoolModel> UpdateItems(List<EmployeesModel> employeeLst);
        Task<EmployeesModel> getInfor(string empId);
    }

    public class EmployeesConnection : RepositoryAPI<EmployeesModel>, IEmployeesConnection
    {
        private static readonly string controlURI = $"api/Employee";
        private static readonly string ENDPOINT_GET_ALL = "getAll";
        private static readonly string ENDPOINT_SEARCH_BY_NAME = "searchByName";
        private static readonly string ENDPOINT_ISEXITS = "isExist";
        private static readonly string ENDPOINT_DELETE = "delete";
        private static readonly string ENDPOINT_INSERT = "insertLst";
        private static readonly string ENDPOINT_UPDATE = "updateLst";
        private static readonly string ENDPOINT_GET_INFOR = "getInfor";

        private readonly IHttpClientFactory _http;

        public EmployeesConnection(IHttpClientFactory http) : base(http, controlURI)
        {
            _http = http;
        }

        /// <summary>
        /// get All employee
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeesModel>> getAllEmployee()
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.GetAsync($"{controlURI}/{ENDPOINT_GET_ALL}");
                    List<EmployeesModel> ret = new List<EmployeesModel>();
                    if (res.IsSuccessStatusCode)
                    {
                        ret = await res.Content.ReadFromJsonAsync<List<EmployeesModel>>();
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    return new List<EmployeesModel>();
                }
            }
        }

        /// <summary>
        /// get employee follow search conditions
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<EmployeesModel>> SearchEmployee(string name)
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.GetAsync($"{controlURI}/{ENDPOINT_SEARCH_BY_NAME}/{name}");
                    List<EmployeesModel> ret = new List<EmployeesModel>();
                    if (res.IsSuccessStatusCode)
                    {
                        ret = await res.Content.ReadFromJsonAsync<List<EmployeesModel>>();
                    }
                    return ret;
                }
                catch
                {
                    return new List<EmployeesModel>();
                }
            }
        }

        public async Task<bool> isExistEmployee(List<EmployeesModel> employeeLst)
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.PostAsJsonAsync<List<EmployeesModel>>($"{controlURI}/{ENDPOINT_ISEXITS}", employeeLst);

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

        public async Task<ErrorResBoolModel> DeleteItems(List<EmployeesModel> employeeLst)
        {
            ErrorResBoolModel ret = new ErrorResBoolModel();

            if (employeeLst == null || employeeLst.Count == 0)
            {
                return ret;
            }

            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.PostAsJsonAsync<List<EmployeesModel>>($"{controlURI}/{ENDPOINT_DELETE}", employeeLst);

                    if (!ret.messKind(res, Common.Action.DELETE_TITLE))
                    {
                        return ret;
                    }

                    if (!res.IsSuccessStatusCode)
                    {
                        return ret;
                    }

                    ret.resIsSuccess = true;
                    return ret;
                }
                catch (Exception ex)
                {
                    return ret;
                }
            }
        }

        public async Task<ErrorResBoolModel> CreateItems(List<EmployeesModel> employeeLst)
        {
            ErrorResBoolModel ret = new ErrorResBoolModel();

            if (employeeLst == null || employeeLst.Count == 0)
            {
                return ret;
            }

            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.PostAsJsonAsync<List<EmployeesModel>>($"{controlURI}/{ENDPOINT_INSERT}", employeeLst);

                    if (!ret.messKind(res, Common.Action.INSERT_TITLE))
                    {
                        return ret;
                    }

                    if (!res.IsSuccessStatusCode)
                    {
                        return ret;
                    }

                    ret.resIsSuccess = true;
                    return ret;
                }
                catch (Exception ex)
                {
                    return ret;
                }
            }
        }

        public async Task<ErrorResBoolModel> UpdateItems(List<EmployeesModel> employeeLst)
        {
            ErrorResBoolModel ret = new ErrorResBoolModel();

            if (employeeLst == null || employeeLst.Count == 0)
            {
                return ret;
            }

            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.PostAsJsonAsync<List<EmployeesModel>>($"{controlURI}/{ENDPOINT_UPDATE}", employeeLst);

                    if (!ret.messKind(res, Common.Action.INSERT_TITLE))
                    {
                        return ret;
                    }

                    if (!res.IsSuccessStatusCode)
                    {
                        return ret;
                    }

                    ret.resIsSuccess = true;
                    return ret;
                }
                catch (Exception ex)
                {
                    return ret;
                }
            }
        }

        public async Task<EmployeesModel> getInfor(string empId)
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.GetAsync($"{controlURI}/{ENDPOINT_GET_INFOR}/{empId}");
                    EmployeesModel ret = new EmployeesModel();
                    if (res.IsSuccessStatusCode)
                    {
                        ret = await res.Content.ReadFromJsonAsync<EmployeesModel>();
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    return new EmployeesModel();
                }
            }
        }
    }

}
