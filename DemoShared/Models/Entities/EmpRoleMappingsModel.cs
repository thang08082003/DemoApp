using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Models.Entities
{
    [Table("m_emp_role_mappings")]
    public class EmpRoleMappingsModel
    {
        [Key]
        [Column("EMP_NO")]
        [MaxLength(8)]
        public string emp_no { get; set; }

        [Key]
        [Column("ROLE_ID")]
        [MaxLength(30)]
        public string app_role_id { get; set; }

        [Column("CRE_DT")]
        public DateTime? created_at { get; set; }

        [Column("UPD_DT")]
        public DateTime? updated_at { get; set; }
    }
}
