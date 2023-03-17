using System;
using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    internal class SubscriptionService : ISubscriptionService
    {
        readonly IList<Subscription> allSubcriptions = (IList<Subscription>)Store.Subscriptions.Data;
        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            IList<Subscription> subscriptions = new List<Subscription>();
            foreach (var subscription in allSubcriptions)
            {
                foreach(var id in ids)
                {
                    if (subscription.Id == id)
                    {
                        subscriptions.Add(subscription);
                    }
                }
                return subscriptions;
            }
            throw new NotImplementedException();
        }
    }
}
