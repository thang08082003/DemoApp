using DemoShared.Constants;
using DemoShared.Models.CallBackModels;
using DemoShared.Models.Entities;
using DemoWeb.Services.API.Repository;
using System.Net.Http.Json;
using DemoShared.Models.Parameter;
using DemoShared.Models.DTO;

namespace DemoWeb.Services.API
{
    public interface IEmpRoleMappingsConnection : IRepositoryAPI<EmpRoleMappingsModel>
    {
        
    }

    public class EmpRoleMappingsConnection : RepositoryAPI<EmpRoleMappingsModel>, IEmpRoleMappingsConnection
    {
        private static readonly string controlURI = $"api/EmpRoleMappings";
        

        private readonly IHttpClientFactory _http;

        public EmpRoleMappingsConnection(IHttpClientFactory http) : base(http, controlURI)
        {
            _http = http;
        }

    }
}
