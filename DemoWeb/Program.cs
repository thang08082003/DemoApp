using DemoShared.Models.Entities;
using DemoWeb;
using DemoWeb.Services;
using DemoWeb.Services.API;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static DemoWeb.Services.API.IFileUploadConnection;

namespace DemoWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Basic
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var baseAddress = builder.Configuration.GetValue<string>("Url:BaseUrl");
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            });

            builder.Services.AddSingleton(p => p.GetRequiredService<IConfiguration>().Get<Configures>());

            // Support URL
            var webapibaseurl = builder.Configuration["WebApiBaseUrl"];

            builder.Services.AddHttpClient(DemoShared.Constants.Common.PUBLIC, client =>
                client.BaseAddress = new Uri(webapibaseurl));
            // Add bootstrap blazor
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddTransient<IEmployeesConnection, EmployeesConnection>();
            builder.Services.AddTransient<ICompanyConnection, CompanyConnection>();
            builder.Services.AddTransient<IAccountConnection, AccountConnection>();
            builder.Services.AddTransient<IFileUploadConnection, FileUploadConnection>();
            builder.Services.AddTransient<IEmpRoleMappingsConnection, EmpRoleMappingsConnection>();
            builder.Services.AddScoped<IRedirectService, RedirectService>();
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            await builder.Build().RunAsync();
        }
    }
}


