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
                    Console.WriteLine("Enter year:");
                    if (int.TryParse(Console.ReadLine(), out int year))
                    {
                        Console.WriteLine("Enter month: ");
                        if (int.TryParse(Console.ReadLine(), out int month))
                        {
                            DateTimeOffset fromDate = new DateTimeOffset(new DateTime(year, month, 1));
                            DateTimeOffset toDate = new DateTimeOffset(new DateTime(year, month, DateTime.DaysInMonth(year,month)));
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
