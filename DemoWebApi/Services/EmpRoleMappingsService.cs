using DemoShared.Models.Entities;
using DemoWebApi.Data;
using DemoWebApi.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi.Services
{
    public interface IEmpRoleMappingsService : IRepository<EmpRoleMappingsModel>
    {
        
    }

    public class EmpRoleMappingsService : Repository<EmpRoleMappingsModel>, IEmpRoleMappingsService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IServiceScopeFactory _scopeFactory;

        public EmpRoleMappingsService
                        (IDbContextFactory<DataContext> contextFactory,
                        IServiceScopeFactory scopeFactory) : base(contextFactory, scopeFactory)
        {
            _contextFactory = contextFactory;
            _scopeFactory = scopeFactory;
        }

    }
}
