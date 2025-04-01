using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DemoShared.Constants;
using BlazorBootstrap;
using DemoWeb.Services.API;
using DemoShared.Models.Entities;
using DemoWeb.Services;
using Microsoft.AspNetCore.WebUtilities;
using static DemoShared.Constants.Navigation;
using DemoShared.Models.DTO;

namespace DemoWeb.Layout
{
    public partial class NavMenu : ComponentBase
    {
        [Inject]
        IAccountConnection? iac { get; set; }
        [Inject]
        IRedirectService? irs { get; set; }
        Sidebar? sidebar;
        IEnumerable<NavItem>? navItems;
        public EmployeeDTO? account { get; set; }
        string userId = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            System.Uri uri = irs!.GetURI();
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(QueryKey.USER_ID, out var param))
            {
                userId = param.First()!.Trim('"');
            }
            await InvokeAsync(StateHasChanged);
        }

        private async Task<SidebarDataProviderResult> SidebarDataProvider(SidebarDataProviderRequest request)
        {
            account = await iac!.getUserLogin(userId);

            if (account!.roleId.Equals(Common.Role.ADMIN))
            {
                if (navItems is null)
                    navItems = GetNavItemsAdmin();
            }
            else
            {
                if (navItems is null)
                    navItems = GetNavItemsUser();
            }
            
            return await Task.FromResult(request.ApplyTo(navItems));
        }

        private IEnumerable<NavItem> GetNavItemsAdmin()
        {
            navItems = new List<NavItem>
        {
            new NavItem { Id = "1", Href = "/Home", IconName = IconName.LayoutSidebarInset, Text = Common.Header.EMPLOYEE_MASTER },
            new NavItem { Id = "2", Href = "/EmployeeMaster", IconName = IconName.PersonSquare, Text = Common.Header.EMPLOYEE_MASTER_MANAGEMENT, ParentId="1" },
        };

            return navItems;
        }

        private IEnumerable<NavItem> GetNavItemsUser()
        {
            navItems = new List<NavItem>
        {
            new NavItem { Id = "1", IconName = IconName.LayoutSidebarInset, Text = Common.Header.INFORMATION },
            new NavItem { Id = "2", Href = "/EmployeeInformation", IconName = IconName.PersonSquare, Text = Common.Header.EMPLOYEE_INFORMATION, ParentId="1" },
        };

            return navItems;
        }

        private void ToggleSidebar() => sidebar!.ToggleSidebar();
    }
}
