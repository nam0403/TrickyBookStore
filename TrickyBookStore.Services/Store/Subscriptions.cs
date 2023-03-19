using System.Collections.Generic;
using TrickyBookStore.Models;
using System;
namespace TrickyBookStore.Services.Store
{
    public static class Subscriptions
    {
        public static readonly IEnumerable<Subscription> Data = new List<Subscription>
        {
            new Subscription { Id = 1, SubscriptionType = SubscriptionTypes.Paid, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    {"FixPrice", 50 },
                    {"LimitBuyNewBook", 3 },
                    {"DiscountBuyNewBook", 0.05},
                    {"FeeCharge", 0.05 }
                }
            },
            new Subscription { Id = 2, SubscriptionType = SubscriptionTypes.Free, Priority = 4,
                PriceDetails = new Dictionary<string, double>
                {
                    {"FixPrice", 0 },
                    {"LimitBuyNewBook", 0 },
                    {"DiscountBuyNewBook", 0 },
                    {"DiscountBuyOldBook", 0.1 }
                }
            },
            new Subscription { Id = 3, SubscriptionType = SubscriptionTypes.Premium, Priority = 1,
                PriceDetails = new Dictionary<string, double>
                {
                    {"FixPrice", 200 },
                    {"LimitBuyNewBook", 3 },
                    {"DiscountBuyNewBook", 0.15}
                }
            },
            new Subscription { Id = 4, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 2,
                PriceDetails = new Dictionary<string, double>
                {
                  { "FixPrice", 75 },
                  {"LimitBuyNewBook", 3 },
                  {"DiscountBuyNewBook", 0.15}

                }
            },
            new Subscription { Id = 5, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 2,
                PriceDetails = new Dictionary<string, double>
                {
                    { "FixPrice", 75 },
                    {"LimitBuyNewBook", 3 },
                    {"DiscountBuyNewBook", 0.15}

                },
                BookCategoryId = 1
            },
            new Subscription { Id = 6, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 2,
                PriceDetails = new Dictionary<string, double>
                { 
                    { "FixPrice", 75 }, 
                    { "LimitBuyNewBook", 3 }, 
                    { "DiscountBuyNewBook", 0.15 } 
                },
                BookCategoryId = 3
            }
        };
    }
}
