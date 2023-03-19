using TrickyBookStore.TrickyBookServiceProvider;
using TrickyBookStore.DI;
using Microsoft.Extensions.DependencyInjection;

 
var serviceCollection = new ServiceCollection();
DI.ConfigureService(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();
var trickyBookService = new TrickyBookServiceProvider(serviceProvider);
trickyBookService.CalculateCustomerPaymentAmount();
