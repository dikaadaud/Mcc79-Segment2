﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key]
        [Column("guid")]
        public Guid Guid { get; set; }

        [Column("password", TypeName = "nvarchar(max)")]
        public string Password { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("otp")]
        public int Otp { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("exprired_time")]
        public DateTime ExpriedTime { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
