using TrickyBookStore.ServiceCollections;

namespace TrickeyBookStore.View
{
    public class Program
    {
        static void Main(string[] args)
        {
            DateTimeOffset fromDate = new DateTimeOffset(new DateTime(2022, 01, 01));
            DateTimeOffset toDate = new DateTimeOffset(new DateTime(2022, 12, 31));
            ServiceCollections collection = new ServiceCollections();
            double s = collection.GetCustomerPayment(1, fromDate, toDate);
            Console.WriteLine(s);
        }
    }
}

