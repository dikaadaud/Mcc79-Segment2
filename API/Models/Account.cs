using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password", TypeName = "nvarchar(max)")]
        public string Password { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("otp")]
        public int? Otp { get; set; }

        [Column("is_used")]
        public bool? IsUsed { get; set; }

        [Column("exprired_time")]
        public DateTime? ExpriedTime { get; set; }

        //Cardinality
        public ICollection<AccountRole> AccountRoles { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
