using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Models.Entities
{
    [Table("m_app_roles")]
    public class AppRolesModel
    {
        [Key]
        [Column("ROLE_ID")]
        [MaxLength(30)]
        public string app_role_id { get; set; }

        [Column("DESCRIPTION")]
        [MaxLength(100)]
        public string? description { get; set; }

        [Column("CRE_DT")]
        public DateTime? created_at { get; set; }

        [Column("UPD_DT")]
        public DateTime? updated_at { get; set; }
    }
}
