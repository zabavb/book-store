namespace UserAPI.Models.DTOs
{
    public class SubscriptionDTO
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }

        public SubscriptionDTO()
        {
            Title = string.Empty;
        }
        public SubscriptionDTO(Guid? id, string title, string? description, DateTime endDate)
        {
            Id = id;
            Title = title;
        }
    }
}
