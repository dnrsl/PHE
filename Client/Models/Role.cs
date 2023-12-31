﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    [Table("tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
