using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Models.Entities
{
    [Table("m_company")]
    public class CompanyModel
    {
        [Key]
        [Column("CPN_ID")]
        [MaxLength(8)]
        public string company_id { get; set; }

        [Column("CPN_NAME")]
        [MaxLength(100)]
        public string? company_name { get; set; }

        [Column("CRE_DT")]
        public DateTime? created_at { get; set; }

        [Column("UPD_DT")]
        public DateTime? updated_at { get; set; }
    }
}
