using System;
using Microsoft.Extensions.DependencyInjection;
using TrickyBookStore.Services.Payment;


namespace TrickyBookStore.TrickyBookServiceProvider
{
    public class TrickyBookServiceProvider
    {
        private readonly IPaymentService _paymentService;

        public TrickyBookServiceProvider(IServiceProvider serviceProvider)
        {
            _paymentService = serviceProvider.GetService<IPaymentService>();
        }

        public void CalculateCustomerPaymentAmount()
        {
            while (true)
            {
                Console.WriteLine("Enter customer ID (or type 'exit' to quit):");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    break;
                }

                if (long.TryParse(input, out long customerId))
                {
                    Console.WriteLine("Enter 'from' date (MM/dd/yyyy):");
                    if (DateTimeOffset.TryParse(Console.ReadLine(), out DateTimeOffset fromDate))
                    {
                        Console.WriteLine("Enter 'to' date (MM/dd/yyyy):");
                        if (DateTimeOffset.TryParse(Console.ReadLine(), out DateTimeOffset toDate))
                        {
                            double customerPaymentAmount = _paymentService.GetPaymentAmount(customerId, fromDate, toDate);
                            Console.WriteLine($"The total customer payment amount is {customerPaymentAmount}.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid 'to' date input.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid 'from' date input.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid customer ID input.");
                }
            }
        }
    }
}
