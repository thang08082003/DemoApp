using DemoShared.Models.CallBackModels;
using DemoShared.Models.Entities;
using DemoWeb.Services.API.Repository;
using DemoWebApi.Data;
using DemoWebApi.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi.Services
{
    public interface ICompanyService : IRepositoryAPI<CompanyModel>
    {
        Task<IEnumerable<CompanyModel>> getAllCompany();

    }

    public class CompanyService : Repository<CompanyModel>, ICompanyService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IServiceScopeFactory _scopeFactory;

        public CompanyService
                        (IDbContextFactory<DataContext> contextFactory,
                        IServiceScopeFactory scopeFactory) : base(contextFactory, scopeFactory)
        {
            _contextFactory = contextFactory;
            _scopeFactory = scopeFactory;
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorResBoolModel> DeleteItems(List<EmployeesModel> employs)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompanyModel>> getAllCompany()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var ret = await _context.Companies.ToListAsync();
                    return ret;
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }
        }

        Task<ErrorResBoolModel> IRepositoryAPI<CompanyModel>.Add(CompanyModel entity)
        {
            throw new NotImplementedException();
        }

        Task<ErrorResBoolModel> IRepositoryAPI<CompanyModel>.Delete(string id)
        {
            throw new NotImplementedException();
        }

        Task<ErrorResBoolModel> IRepositoryAPI<CompanyModel>.Update(CompanyModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
