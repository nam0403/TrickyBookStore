using Microsoft.Extensions.DependencyInjection;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.DI
{
    public class DI
    {
        
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IPurchaseTransactionService>(provider =>
                new PurchaseTransactionService(provider.GetService<IBookService>()));

            services.AddScoped<IPaymentService>(provider =>
                new PaymentService(
                    provider.GetService<ICustomerService>(),
                    provider.GetService<IPurchaseTransactionService>(),
                    provider.GetService<ISubscriptionService>(),
                    provider.GetService<IBookService>()
                ));
        }

    }
}
