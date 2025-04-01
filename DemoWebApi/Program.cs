using DemoWeb.Services;
using DemoWebApi.Data;
using DemoWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using DemoShared.Constants;
using Microsoft.Extensions.FileProviders;

namespace DemoWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

                builder.Services.AddSingleton(p => p.GetRequiredService<IConfiguration>().Get<ConfiguresApi>());

                // Add services to the container.

                builder.Services.AddControllers();

                builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddDbContextFactory<DataContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

                //ConfigureDbContexts(builder);

                builder.Services.AddTransient<IEmployeesService, EmployeesService>();
                builder.Services.AddTransient<ICompanyService, CompanyService>();
                builder.Services.AddTransient<IAccountService, AccountService>();
                builder.Services.AddTransient<IEmpRoleMappingsService, EmpRoleMappingsService>();
                builder.Services.AddCors();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseCors(policy => policy
                    .AllowAnyMethod()
                    //.AllowCredentials()
                    .AllowAnyHeader()
                    //.WithHeaders(headers)
                    .AllowAnyOrigin()
                    //.WithOrigins(origins)
                    );

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception exception)
            {
                
            }
        }

        private static void ConfigureDbContexts(WebApplicationBuilder builder)
        {
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            /* VehicleMonitoringSystem */
            builder.Services.AddDbContextFactory<DataContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(AppSettings.ConnectionString.DB_CONNECTION),
                    serverOptions =>
                    {
                        serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        serverOptions.CommandTimeout(300);    // Command Timeout
                        serverOptions.EnableRetryOnFailure(  // データベース接続の再試行
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    }
                );
                options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettings.ConnectionString.DB_CONNECTION), b => b.MigrationsAssembly("DemoWebApi"));

#if DEBUG
                options.EnableSensitiveDataLogging();
#endif
            });
        }
    }
}


