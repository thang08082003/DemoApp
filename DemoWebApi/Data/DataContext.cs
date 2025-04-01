using DemoShared.Models.Entities;
using Microsoft.EntityFrameworkCore;

using System.Data.Common;

namespace DemoWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<AppRolesModel> AppRoles { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<EmployeesModel> Employees { get; set; }
        public DbSet<EmpRoleMappingsModel> EmpRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().HasKey(table => new
            {
                table.emp_no
            });
            modelBuilder.Entity<AppRolesModel>().HasKey(table => new
            {
                table.app_role_id
            });
            modelBuilder.Entity<CompanyModel>().HasKey(table => new
            {
                table.company_id
            });
            modelBuilder.Entity<EmployeesModel>().HasKey(table => new
            {
                table.emp_no
            });
            modelBuilder.Entity<EmpRoleMappingsModel>().HasKey(table => new
            {
                table.emp_no,
                table.app_role_id
            });
        }

    }
}
