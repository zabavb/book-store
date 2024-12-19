namespace UserAPI.Models
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Guid>? UserIds { get; set; }
        public ICollection<User>? Users { get; set; }

        public Subscription()
        {
            Title = string.Empty;
            Description = string.Empty;
            EndDate = DateTime.Now.AddDays(1);
        }
    }
}
