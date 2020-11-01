using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZipCo.Users.Domain.Entities
{
    public class AudibleBaseEntity : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedOn { get; set; }

    }
}
