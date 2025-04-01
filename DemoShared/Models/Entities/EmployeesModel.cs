using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace DemoShared.Models.Entities
{
    [Table("m_employees")]
    public class EmployeesModel
    {
        [Key]
        [Column("EMP_NO")]
        [Required(ErrorMessage = "Please enter Engineer ID")]
        [Name("Engineer ID")]
        [MaxLength(8)]
        public string emp_no { get; set; }

        [NotMapped]
        [Name("Company Number")]
        public string? company_name { get; set; }

        [Column("USR_NAME")]
        [MaxLength(20)]
        [Name("Full Name")]
        public string? emp_name { get; set; }

        [Column("USR_NAME_KATA")]
        [MaxLength(40)]
        [Name("Full Name (Katakana)")]
        public string? emp_name_kana { get; set; }

        // 1 : Female, 0 : Male
        [Column("SEX")]
        [MaxLength(1)]
        [Name("Gender")]
        public string? sex { get; set; }

        [Column("DATE_OF_BIRTH")]
        [Name("Date of Birth")]
        public DateTime? birthday { get; set; }

        [Column("HIRE_DATE")]
        [Name("Hire Date")]
        public DateTime? hire_date { get; set; }

        [Column("CPN_ID")]
        [Required(ErrorMessage = "Please enter Company ID")]
        [Name("Company ID")]
        [MaxLength(8)]
        public string? company_id { get; set; }

        [Column("POST_CD")]
        [Name("Post Code")]
        [MaxLength(5)]
        public string? post_cd { get; set; }

        // 1 : Yes, 0 : No
        [Column("RETIED")]
        [Name("Retired")]
        [MaxLength(1)]
        public string? is_retired { get; set; }

        [Column("CRE_BY")]
        [Name("Created By")]
        public string? created_by { get; set; }

        [Column("UPD_BY")]
        [Name("Updated By")]
        public string? updated_by { get; set; }

        [Column("CRE_DT")]
        [Name("Created At")]
        public DateTime? created_at { get; set; }

        [Column("UPD_DT")]
        [Name("Updated At")]
        public DateTime? updated_at { get; set; }

        [Column("IMAGE")]
        [Name("Image")]
        public string? imageUrl { get; set; }
    }
}