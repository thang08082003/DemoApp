using DemoShared.Constants;
using DemoShared.Models.DTO;
using DemoShared.Models.Entities;
using DemoWebApi.Data;
using DemoWebApi.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DemoWebApi.Services
{
    public interface IEmployeesService : IRepository<EmployeesModel>
    {
        Task<bool> DeleteById(string Id);
        Task<IEnumerable<EmployeesModel>> getAllEmployee();
        Task<IEnumerable<EmployeesModel>> SearchEmployee(string name);
        Task<EmployeesModel> getInfor(string empId);
        Task<bool> isExistEmployee(List<EmployeesModel> employeeLst);
    }

    public class EmployeesService : Repository<EmployeesModel>, IEmployeesService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IServiceScopeFactory _scopeFactory;

        public EmployeesService
                        (IDbContextFactory<DataContext> contextFactory,
                        IServiceScopeFactory scopeFactory) : base(contextFactory, scopeFactory)
        {
            _contextFactory = contextFactory;
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<EmployeesModel>> getAllEmployee()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var ret = await _context.Employees
                    .GroupJoin
                    (
                        _context.Companies,
                        em => em.company_id,
                        cm => cm.company_id,
                        (em, cm) => new { A = em, B = cm }
                    )
                    .SelectMany(Company => Company.B.DefaultIfEmpty(), (Employees, Company) =>
                    new
                    {
                        A = Employees.A,
                        B = Company
                    })
                    .Select(res => new EmployeesModel
                    {
                        emp_no = res.A.emp_no,
                        company_id = res.A.company_id,
                        company_name = res.B.company_name,
                        emp_name = res.A.emp_name,
                        emp_name_kana = res.A.emp_name_kana,
                        sex = res.A.sex,
                        created_by = res.A.created_by,
                        updated_by = res.A.updated_by,
                        birthday = res.A.birthday,
                        hire_date = res.A.hire_date
                    })
                    .OrderByDescending(col => col.emp_no)
                    .ToListAsync();

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
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<EmployeesModel>> SearchEmployee(string name)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var ret = await _context.Employees
                    .Where(ele => ele.emp_name.Contains(name) || ele.emp_name_kana.Contains(name))
                    .GroupJoin
                    (
                        _context.Companies,
                        em => em.company_id,
                        cm => cm.company_id,
                        (em, cm) => new { A = em, B = cm }
                    )
                    .SelectMany(Company => Company.B.DefaultIfEmpty(), (Employees, Company) =>
                    new
                    {
                        A = Employees.A,
                        B = Company
                    })
                    .Select(res => new EmployeesModel
                    {
                        emp_no = res.A.emp_no,
                        company_id = res.A.company_id,
                        company_name = res.B.company_name,
                        emp_name = res.A.emp_name,
                        emp_name_kana = res.A.emp_name_kana,
                        sex = res.A.sex,
                        created_by = res.A.created_by,
                        updated_by = res.A.updated_by,
                        birthday = res.A.birthday,
                        hire_date = res.A.hire_date
                    })
                    .OrderByDescending(col => col.emp_no)
                    .ToListAsync();
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
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteById(string Id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var executionStrategy = context.Database.CreateExecutionStrategy();
                bool ret = false;
                await executionStrategy.Execute(async () =>
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            EmployeesModel entDelete = context.Employees.Where(itm => itm.emp_no.Equals(Id)).FirstOrDefault();
                            context.Employees.Remove(entDelete);
                            await context.SaveChangesAsync();
                            trans.Commit();

                            ret = true;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            ret = false;
                        }
                    }
                });
                if (ret)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<EmployeesModel> getInfor(string empId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var ret = await _context.Employees
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

        public async Task<bool> isExistEmployee(List<EmployeesModel> employeeLst)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var empolyeeId = employeeLst.Select(col => col.emp_no).ToList();
                try
                {
                    var ret = await _context.Employees
                        .Where(emp => empolyeeId.Contains(emp.emp_no))
                        .AnyAsync();
                    return ret;
                }
                catch(Exception ex)
                {
                    throw new Exception();
                }
            }
        }
    }
}
