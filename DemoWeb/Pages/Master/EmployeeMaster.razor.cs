using DemoShared.Models.Entities;
using Microsoft.AspNetCore.Components;
using DemoShared.Constants;
using DemoWeb.Services.API;
using Microsoft.AspNetCore.WebUtilities;
using static DemoShared.Constants.Navigation;
using DemoShared.Models.DTO;
using DemoWeb.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using DemoShared.Models;

namespace DemoWeb.Pages.Master
{
    public partial class EmployeeMaster : ComponentBase
    {
        #region Inject
        [Inject]
        IEmployeesConnection? iec { get; set; }
        [Inject]
        ICompanyConnection? icc { get; set; }
        [Inject]
        IAccountConnection? iac { get; set; }
        [Inject]
        IRedirectService? irs { get; set; }
        [Inject]
        IJSRuntime? JS { get; set; }
        #endregion

        #region Const
        private const string COMPANY_MASTER_MANAGEMENT = "◆ Company Master Management Input";
        private const string SEARCH = "◆ Search";
        private const string NAME_KATA = "Name or Name";
        private const string TECH_ID = "Engineer ID";
        private const string COMPANY_NUMBER = "Company Number";
        private const string IDENTITY = "Full Name";
        private const string NAME_KATANA = "Name";
        private const string GENDER = "Gender";
        private const string REGIST_BY = "Registered By";
        private const string UPDATE_BY = "Updated By";
        private const string DATE_BIRTH = "Birth Date";
        private const string DATE_HIRE = "Hire Date";
        private const int NOT_SELECT = 0;
        private const int SELECT_ONLY = 1;
        private const int SELECT_MANY = 2;
        private const string ROLE_UPLOAD_INFO = "role_upload_information";
        #endregion

        #region Variale
        /// <summary>
        /// 
        /// </summary>
        private List<EmployeesModel> employeeLst = new List<EmployeesModel>();
        /// <summary>
        /// 
        /// </summary>
        private List<EmployeesModel> empImportLst = new List<EmployeesModel>();
        /// <summary>
        /// 
        /// </summary>
        private HashSet<EmployeesModel> selectedEmployees = new();
        /// <summary>
        /// 
        /// </summary>
        private List<CompanyModel> companyLst = new List<CompanyModel>();
        /// <summary>
        /// 
        /// </summary>
        private EmployeesModel? modalTrans;
        /// <summary>
        /// 
        /// </summary>
        private bool isDisableDelButton = true;
        /// <summary>
        /// 
        /// </summary>
        private bool isShowModal = false;
        /// <summary>
        /// 
        /// </summary>
        private bool isShowEmpModal = false;
        /// <summary>
        /// 
        /// </summary>
        private string? actionState;
        /// <summary>
        /// 
        /// </summary>
        private string? SearchNameValue;
        /// <summary>
        /// 
        /// </summary>
        string userId = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private EmployeeDTO? account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private bool isImport = false;
        /// <summary>
        /// 
        /// </summary>
        private bool isDisableImport = false;
        /// <summary>
        /// 
        /// </summary>
        private bool invalidValueImport = false;
        /// <summary>
        /// 
        /// </summary>
        private string err_invalidValueImport => invalidValueImport ? Messages.Error.MSGE0002 : string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string FileName = string.Empty;
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
            account = await iac!.getUserLogin(userId);
            if (!isImport)
            {
                await GetAllEmployee();
            }
            else
            {

            }
            companyLst = await icc!.getAllCompany();
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// get all employee
        /// </summary>
        /// <returns></returns>
        private async Task GetAllEmployee()
        {
            employeeLst = await iec!.getAllEmployee();
            foreach (var employee in employeeLst)
            {
                employee.sex = employee.sex!.Equals(Common.GENDER.MALE.value) ? Common.GENDER.MALE.name :
                               employee.sex!.Equals(Common.GENDER.FEMALE.value) ? Common.GENDER.FEMALE.name :
                               string.Empty;
            }
        }

        /// <summary>
        /// search employee follow name or full name
        /// </summary>
        /// <returns></returns>
        private async Task SearchEmployee()
        {
            employeeLst = await iec!.SearchEmployee(SearchNameValue!);
            foreach (var employee in employeeLst)
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
            if (selectedEmployees.Count > NOT_SELECT)
            {
                isDisableDelButton = false;
            }
            else
            {
                isDisableDelButton = true;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        private async Task FullNameOnChanged(ChangeEventArgs e)
        {
            SearchNameValue = e.Value!.ToString();
            if (!string.IsNullOrEmpty(SearchNameValue))
            {
                await SearchEmployee();
            }
            else
            {
                await GetAllEmployee();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async void ConfirmCallBack()
        {
            isShowModal = false;
            if (selectedEmployees.Count == SELECT_ONLY)
            {
                var employee = selectedEmployees.First();
                var res = await iec!.Delete(employee.emp_no);
            }
            else if (selectedEmployees.Count >= SELECT_MANY)
            {
                var employeeLst = selectedEmployees.ToList();
                foreach (var employee in employeeLst)
                {
                    employee.sex = employee.sex!.Equals(Common.GENDER.MALE.name) ? Common.GENDER.MALE.value :
                                   employee.sex!.Equals(Common.GENDER.FEMALE.name) ? Common.GENDER.FEMALE.value :
                                   string.Empty;
                }
                var res = await iec!.DeleteItems(employeeLst);
            }

            // lấy lại dữ liệu sau khi thực hiện xóa
            if (!string.IsNullOrEmpty(SearchNameValue))
            {
                await SearchEmployee();
            }
            else
            {
                await GetAllEmployee();
            }
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        private async void CancelCallBack()
        {
            isShowModal = false;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task BtnOpenModalOnClick()
        {
            isShowModal = true;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private async Task BtnDetailOnclick(EmployeesModel employee)
        {
            isShowEmpModal = true;
            actionState = Common.Action.UPDATE;
            modalTrans = employee;
            modalTrans.sex = employee.sex!.Equals(Common.GENDER.MALE.name) ? Common.GENDER.MALE.value :
                             employee.sex!.Equals(Common.GENDER.FEMALE.name) ? Common.GENDER.FEMALE.value :
                             string.Empty;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task BtnCreatOnClick()
        {
            actionState = Common.Action.INSERT;
            modalTrans = new EmployeesModel();
            isShowEmpModal = true;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async void ConfirmEmpCallBack()
        {
            isShowEmpModal = false;
            if (!string.IsNullOrEmpty(SearchNameValue))
            {
                await SearchEmployee();
            }
            else
            {
                await GetAllEmployee();
            }
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        private async void CancelEmpCallBack()
        {
            isShowEmpModal = false;
            if (!string.IsNullOrEmpty(SearchNameValue))
            {
                await SearchEmployee();
            }
            else
            {
                await GetAllEmployee();
            }
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ImportFileOnClick()
        {
            bool res = await JS!.InvokeAsync<bool>(Common.JSFunc.UPLOAD_TRIGGER, ROLE_UPLOAD_INFO);
        }

        /// <summary>
        /// import record in CSV file
        /// </summary>
        /// <param name="e"></param>
        private async void HandleFileSelected(InputFileChangeEventArgs e)
        {
            isImport = true;
            isDisableImport = false;
            invalidValueImport = false;
            empImportLst = new List<EmployeesModel>();
            var file = e.File;
            FileName = file.Name;
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using (var reader = new StreamReader(memoryStream))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<EmployeesModel>().ToList();
                    foreach (var record in records)
                    {
                        record.sex = record.sex!.Equals(Common.GENDER.MALE.value) ? Common.GENDER.MALE.name :
                                     record.sex!.Equals(Common.GENDER.FEMALE.value) ? Common.GENDER.FEMALE.name :
                                     string.Empty;
                    }
                    empImportLst.AddRange(records);
                }
                await Task.Delay(3000);
            }
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task BtnResetOnClick()
        {
            isImport = false;
            isDisableImport = true;
            invalidValueImport = false;
            FileName = string.Empty;
            await GetAllEmployee();
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task BtnConfirmDataOnclick()
        {
            invalidValueImport = false;
            List<EmployeesModel> employeeLstInsert = empImportLst;
            var employeesId = empImportLst.Select(col => col.emp_no).ToList();
            if (employeeLstInsert.Count > Common.LIST_EMPTY)
            {
                foreach (var employee in employeeLstInsert)
                {
                    if (string.IsNullOrEmpty(employee.emp_no) || string.IsNullOrEmpty(employee.company_id))
                    {
                        invalidValueImport = true;
                        return;
                    }
                    else
                    {
                        employee.sex = employee.sex!.Equals(Common.GENDER.MALE.name) ? Common.GENDER.MALE.value :
                                       employee.sex!.Equals(Common.GENDER.FEMALE.name) ? Common.GENDER.FEMALE.value :
                                       string.Empty;
                    }
                }
                bool isExist = await iec!.isExistEmployee(employeeLstInsert);
                if (!isExist)
                {
                    var res = await iec!.CreateItems(employeeLstInsert);
                }
                else
                {
                    var res = await iec!.UpdateItems(employeeLstInsert);
                }
            }
            isImport = false;
            isDisableImport = true;
            await GetAllEmployee();
            await InvokeAsync(StateHasChanged);
        }
        #endregion
    }
}
