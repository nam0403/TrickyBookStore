using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    internal class SubscriptionService : ISubscriptionService
    {
        readonly IList<Subscription> allSubscriptions = (IList<Subscription>)Store.Subscriptions.Data;
        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            IList<Subscription> subscriptions = new List<Subscription>();
            subscriptions = allSubscriptions.Where(sub =>
               (ids.Length == 0 && sub.SubscriptionType.ToString() == "Free") || ids.Contains(sub.Id)).ToList();
            return subscriptions;
            throw new NotImplementedException();
        }
    }
}
