using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    [Table("tb_m_projects")]
    public class Project : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("user_guid")]
        public Guid UserGuid { get; set; }

        public User? User { get; set; }

        public ICollection <UserProject>? UserProjects { get; set; }
    }
}
