using System.ComponentModel.DataAnnotations.Schema;

namespace Client2.Models
{
    [Table("tb_m_characteristics")]
    public class Characteristic : BaseEntity
    {
        [Column("business_field")]
        public string BusinessField { get; set; }

        [Column("company_type")]
        public string CompanyType { get; set; }

        public Account? Account { get; set; }
    }
}
