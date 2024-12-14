using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UserEntities
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime EndDate { get; set; }

        public Subscription()
        {
            Title = string.Empty;
            Description = string.Empty;
            EndDate = DateTime.Now.AddDays(1);
        }
        public Subscription(Guid subscriptionId, string title, string? description, DateTime endDate)
        {
            SubscriptionId = subscriptionId;
            Title = title;
            Description = description;
            EndDate = endDate;
        }
    }
}
