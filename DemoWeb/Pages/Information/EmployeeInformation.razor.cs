using DemoShared.Constants;
using DemoShared.Models.Entities;
using DemoWeb.Services.API;
using Microsoft.AspNetCore.Components;
using BlazorInputFile;
using static System.Net.WebRequestMethods;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using System;
using Microsoft.AspNetCore.WebUtilities;
using static DemoShared.Constants.Navigation;
using DemoWeb.Services;
using DemoShared.Models.DTO;



namespace DemoWeb.Pages.Information
{
    public partial class EmployeeInformation : ComponentBase
    {
        #region Inject
        [Inject]
        IEmployeesConnection? iec { get; set; }
        [Inject]
        ICompanyConnection? icc { get; set; }
        [Inject]
        IFileUploadConnection? ifc { get; set; }
        [Inject]
        HttpClient? httpClient { get; set; }
        [Inject]
        IRedirectService? irs { get; set; }
        [Inject]
        IAccountConnection? iac { get; set; }
        #endregion

        #region Const
        private const string EMPLOYEE_INFORMATION = "Engineer Information";
        private const string TECH_ID = "Engineer ID";
        private const string COMPANY_NUMBER = "Company Number";
        private const string IDENTITY = "Full Name";
        private const string NAME_KATANA = "Full Name (Katakana)";
        private const string GENDER = "Gender";
        private const string REGIST_BY = "Created By";
        private const string UPDATE_BY = "Updated By";
        private const string DATE_BIRTH = "Date of Birth";
        private const string DATE_HIRE = "Hire Date";
        #endregion

        #region Variale
        /// <summary>
        /// 
        /// </summary>
        private List<EmployeesModel> employeeLst = new List<EmployeesModel>();
        /// <summary>
        /// 
        /// </summary>
        private List<EmployeesModel> employees = new List<EmployeesModel>();
        /// <summary>
        /// 
        /// </summary>
        private HashSet<EmployeesModel> selectedEmployees = new();
        /// <summary>
        /// 
        /// </summary>
        private EmployeesModel? employeePath;
        /// <summary>
        /// 
        /// </summary>
        private bool isShowEmpModal = false;
        /// <summary>
        /// 
        /// </summary>
        private List<CompanyModel> companyLst = new List<CompanyModel>();
        /// <summary>
        /// 
        /// </summary>
        private string? actionState;
        /// <summary>
        /// 
        /// </summary>
        private string? ImageUrl {  get; set; }
        /// <summary>
        /// 
        /// </summary>
        private bool hiddenImg = true;
        /// <summary>
        /// 
        /// </summary>
        private bool showImage;
        public byte[] ImgUploaded { get; set; }
        public string ImgUploadedName { get; set; }
        //Limit file size = 25MB
        private long maxFileSize = 25 * 1048576;
        //Limit file = 1
        private int maxAllowedFiles = 1;
        /// <summary>
        /// 
        /// </summary>
        private string userId = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string imageUrl;
        /// <summary>
        /// 
        /// </summary>
        private bool isDisableUpload = true;
        #endregion

        #region Function
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            System.Uri uri = irs!.GetURI();
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(QueryKey.USER_ID, out var param))
            {
                userId = param.First()!.Trim('"');
            }
            await getEmployee();
            companyLst = await icc!.getAllCompany();
            employeePath = await iec!.getInfor(userId);
            if (string.IsNullOrEmpty(employeePath.imageUrl))
            {
                showImage = false;
            }
            else
            {
                showImage = true;
            }
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// get all employee
        /// </summary>
        /// <returns></returns>
        private async Task getEmployee()
        {
            employeeLst = await iec!.getAllEmployee();
            employees = employeeLst.Where(col => col.emp_no.Equals(userId)).ToList();
            foreach (var employee in employees)
            {
                employee.sex = employee.sex!.Equals(Common.GENDER.MALE.value) ? Common.GENDER.MALE.name :
                               employee.sex!.Equals(Common.GENDER.FEMALE.value) ? Common.GENDER.FEMALE.name :
                               string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        private Task OnSelectedItemsChanged(HashSet<EmployeesModel> employees)
        {
            selectedEmployees = employees is not null && employees.Any() ? employees : new();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private async Task HandleFileSelected(InputFileChangeEventArgs files)
        {
            foreach (var file in files.GetMultipleFiles(maxAllowedFiles))
            {
                MemoryStream ms = new MemoryStream();
                await file.OpenReadStream(maxFileSize).CopyToAsync(ms);
                ImgUploaded = ms.ToArray();
                ImgUploadedName = file.Name;
                ImageUrl = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
            }
            imageUrl = await ifc!.FileUpload(ImgUploaded, ImgUploadedName);
            isDisableUpload = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task btnUploadOnClick()
        {
            hiddenImg = false;
            employeePath!.imageUrl = imageUrl;
            var res = await iec!.Update(employeePath);
            isDisableUpload = true;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objEmp"></param>
        /// <returns></returns>
        private async Task btnChangePaswordOnclick(EmployeesModel objEmp)
        {
            isShowEmpModal = true;
            employeePath = objEmp;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async void ConfirmEmpCallBack()
        {
            isShowEmpModal = false;
            employeeLst = await iec!.getAllEmployee();
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        private async void CancelEmpCallBack()
        {
            isShowEmpModal = false;
            employeeLst = await iec!.getAllEmployee();
            await InvokeAsync(StateHasChanged);
        }
        #endregion
    }
}
