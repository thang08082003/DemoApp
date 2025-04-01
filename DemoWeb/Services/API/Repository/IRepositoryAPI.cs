using DemoShared.Constants;
using System.Net.Http.Json;
using DemoShared.Models.CallBackModels;
using System.Reflection.Emit;
using DemoShared.Models.Entities;

namespace DemoWeb.Services.API.Repository
{
    public interface IRepositoryAPI<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<ErrorResBoolModel> Add(T entity);
        Task<ErrorResBoolModel> Update(T entity);
        Task<ErrorResBoolModel> Delete(string id);
    }

    public class RepositoryAPI<T> : IRepositoryAPI<T> where T : class
    {
        private readonly string _controller;
        private static readonly string ENDPOINT_ADD = "insert";
        private static readonly string ENDPOINT_UPDATE = "update";
        private readonly IHttpClientFactory _http;
        public RepositoryAPI(IHttpClientFactory http, string controller)
        {
            _http = http;
            _controller = controller;
        }

        /// <summary>
        /// get All employees
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                var res = await _http.GetAsync($"{_controller}/GetAll");
                if (!res.IsSuccessStatusCode)
                {
                    return null;
                }
                var ret = await res.Content.ReadFromJsonAsync<IEnumerable<T>>();
                return ret;
            }
        }

        /// <summary>
        /// Insert record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ErrorResBoolModel> Add(T entity)
        {
            ErrorResBoolModel ret = new ErrorResBoolModel();
            bool isResNull = true;
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.PostAsJsonAsync($"{_controller}/{ENDPOINT_ADD}", entity);

                    isResNull = false;
                    if (!ret.messKind(res, Common.Action.INSERT_TITLE)) return ret;

                    if (res.IsSuccessStatusCode)
                    {
                        ret.resIsSuccess = true;
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    if (isResNull)
                    {
                        if (!ret.messKind(null, Common.Action.INSERT_TITLE))
                        {
                            return ret;
                        }
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ErrorResBoolModel> Update(T entity)
        {
            ErrorResBoolModel ret = new ErrorResBoolModel();
            bool isResNull = true;
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.PostAsJsonAsync($"{_controller}/{ENDPOINT_UPDATE}", entity);

                    isResNull = false;
                    if (!ret.messKind(res, Common.Action.UPDATE_TITLE)) return ret;

                    if (res.IsSuccessStatusCode)
                    {
                        ret.resIsSuccess = true;
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    if (isResNull)
                    {
                        if (!ret.messKind(null, Common.Action.UPDATE_TITLE))
                        {
                            return ret;
                        }
                    }
                    return null;
                }

            }
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ErrorResBoolModel> Delete(string id)
        {
            ErrorResBoolModel ret = new ErrorResBoolModel();
            bool isResNull = true;
            using (var _http = this._http.CreateClient(Common.PUBLIC))
            {
                try
                {
                    var res = await _http.DeleteAsync($"{_controller}/{id}");

                    isResNull = false;
                    if (!ret.messKind(res, Common.Action.DELETE_TITLE)) return ret;

                    if (res.IsSuccessStatusCode)
                    {
                        ret.resIsSuccess = true;
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    if (isResNull)
                    {
                        if (!ret.messKind(null, Common.Action.DELETE_TITLE))
                        {
                            return ret;
                        }
                    }
                    return null;
                }
            }
        }

    }
}
