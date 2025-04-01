using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Models.Entities
{
    [Table("m_account")]
    public class AccountModel
    {
        [Key]
        [Column("EMP_NO")]
        [MaxLength(8)]
        public string emp_no { get; set; }

        [Column("USR_ID")]
        [MaxLength(50)]
        public string? user_id { get; set; }

        [Column("PASSWORD")]
        [MaxLength(20)]
        public string? password { get; set; }

        [Column("EMAIL")]
        [MaxLength(50)]
        public string? email { get; set; }

        [Column("CRE_DT")]
        public DateTime? created_at { get; set; }

        [Column("UPD_DT")]
        public DateTime? updated_at { get; set; }
    }
}
