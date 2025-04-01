using DemoShared.Models.Entities;
using DemoWebApi.Data;
using DemoWebApi.Services.Repository;
using Microsoft.EntityFrameworkCore;
using DemoShared.Models.Parameter;
using DemoShared.Constants;
using DemoShared.Models.DTO;

namespace DemoWebApi.Services
{
    public interface IAccountService : IRepository<AccountModel>
    {
        Task<EmployeeDTO> getUserLogin(string empId);
        Task<bool> isExistAccount(AccountParam param);
        Task<AccountModel> getIdByUserName(AccountParam param);
        Task<AccountModel> getPattById(string empId);
    }

    public class AccountService : Repository<AccountModel>, IAccountService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IServiceScopeFactory _scopeFactory;

        public AccountService
                        (IDbContextFactory<DataContext> contextFactory,
                        IServiceScopeFactory scopeFactory) : base(contextFactory, scopeFactory)
        {
            _contextFactory = contextFactory;
            _scopeFactory = scopeFactory;
        }

        public async Task<EmployeeDTO> getUserLogin(string empId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var ret = await _context.Accounts
                    .Where(ele => ele.emp_no.Equals(empId))
                    .GroupJoin(
                        _context.Employees,
                        acm => acm.emp_no,
                        em => em.emp_no,
                        (acm, em) => new
                        {
                            A = acm,
                            B = em }
                        )
                    .SelectMany(EmpGroup => EmpGroup.B.DefaultIfEmpty(), (Account, Employee) =>
                    new
                    {
                        A = Account.A,
                        B = Employee
                    })
                    .GroupJoin(
                        _context.EmpRoles,
                        AccountGroup => AccountGroup.A.emp_no,
                        erm => erm.emp_no,
                        (AccountGroup, erm) => new
                        {
                            A = AccountGroup.A,
                            B = AccountGroup.B,
                            C = erm
                        }
                        )
                    .SelectMany(Role => Role.C.DefaultIfEmpty(), (AccountGroup, Role) =>
                    new
                    {
                        A = AccountGroup.A,
                        B = AccountGroup.B,
                        C = Role
                    })
                    .Select(res => new EmployeeDTO
                    {
                        empId = res.A.emp_no,
                        userName = res.A.user_id,
                        passWord = res.A.password,
                        empName = res.B.emp_name,
                        roleId = res.C.app_role_id,
                        imageUrl = res.B.imageUrl
                    })
                    .FirstOrDefaultAsync();
                    return ret;
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> isExistAccount(AccountParam param)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var ret = await _context.Accounts
                    .AnyAsync(
                        ele => ele.user_id.Equals(param.UserName) &&
                        ele.password.Equals(param.PassWord));
                    return ret;
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AccountModel> getIdByUserName(AccountParam param)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var ret = await _context.Accounts
                        .Where(res => res.user_id.Equals(param.UserName) && res.password.Equals(param.PassWord))
                        .FirstAsync();
                    return ret;
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }
        }

        public async Task<AccountModel> getPattById(string empId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var ret = await _context.Accounts
                        .Where(col => col.emp_no.Equals(empId))
                    .FirstOrDefaultAsync();
                    return ret;
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }
        }
    }
}
