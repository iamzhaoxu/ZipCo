namespace ZipCo.Users.Domain.Entities.Members
{
    public class Frequency: BaseEntity
    {
        public string Name { get; set; }
    }

    public enum FrequencyIds
    {
        Month = 1,
        Annual = 2,
    }
}
