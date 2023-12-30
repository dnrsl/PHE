using System.ComponentModel.DataAnnotations.Schema;

namespace Client2.Models
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password")]
        public string Password { get; set; }

        [Column("otp")]
        public int OTP { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("expired_date")]
        public DateTime ExpiredDate { get; set; }

        [Column("approved_by_admin")]
        public bool ApprovedByAdmin { get; set; }

        [Column("approved_by_manager")]
        public bool ApprovedByManager { get; set; }

        //Cardinality
        public ICollection<AccountRole>? AccountRoles { get; set; }

        public Characteristic? Characteristic { get; set; }
        public User? User { get; set; }
    }
}
