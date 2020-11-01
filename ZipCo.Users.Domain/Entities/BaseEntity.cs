
namespace ZipCo.Users.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public bool IsNew => Id == default;
    }
}
