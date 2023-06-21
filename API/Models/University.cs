using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_universities")]
    public class University : BaseEntity
    {

        [Column("code", TypeName = "varchar(50)")]
        public string Code { get; set; }

        [Column("name", TypeName = "varchar(100")]
        public string Name { get; set; }

        // Cardinality
        public ICollection<Education> Educations { get; set; }


    }
}
