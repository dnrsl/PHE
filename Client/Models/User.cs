using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    [Table("tb_m_users")]
    public class User : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("telephone")]
        public string Telephone { get; set; }

        [Column("photo")]
        public string Photo { get; set;}

        public Account? Account { get; set; }

        public ICollection<Project>? Projects { get; set; }

        public ICollection<UserProject>? UserProjects { get; set; }
    }
}
