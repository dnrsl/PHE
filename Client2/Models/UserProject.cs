using System.ComponentModel.DataAnnotations.Schema;

namespace Client2.Models
{
    [Table("tb_tr_user_projects")]
    public class UserProject : BaseEntity
    {
        [Column("user_guid")]
        public Guid UserGuid { get; set; }

        [Column("project_guid")]
        public Guid ProjectGuid { get; set; }

        public User? User { get; set; }
        public Project? Project { get; set; }   
    }
}
