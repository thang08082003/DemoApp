using System.ComponentModel.DataAnnotations.Schema;

namespace DemoShared.Models.DTO
{
    public class EmployeeDTO
    {
        public string empId { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string roleId { get; set; }
        public string empName { get; set; }
        public string imageUrl { get; set; }
    }
}
