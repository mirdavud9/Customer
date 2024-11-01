using mentortime31._10;
using Newtonsoft.Json;

namespace mentor
{
    internal class Program
    {
        private static string path = @"C:\Users\Mirdavud\source\repos\mentortime31.10\mentortime31.10\Data\customers.json";

        static void Main(string[] args)
        {
            Directory.CreateDirectory(@"C:\Users\Mirdavud\source\repos\mentortime31.10\mentortime31.10\Data");


            List<Customer> customers = new List<Customer>
            {
                new Customer(213443, "Kazimov", "Qasim", "051-574-44-38"),
                new Customer(542513, "Xaliqov", "Kitabulla", "051-123-32-67"),
                new Customer(234234, "Stekanci", "Abdulkerim", "055-978-67-42"),
                new Customer(746464, "Eliyev", "Putin", "077-345-67-19")
            };

            string jsonString = JsonConvert.SerializeObject(customers);
            File.WriteAllText(path, jsonString);

            GetAll();
        }

        static void Add(Customer customer)
        {
            var customers = GetCustomersFromFile();
            customers.Add(customer);
            SaveCustomersToFile(customers);
        }

        static void CustomerSearch(int id)
        {
            var customers = GetCustomersFromFile();
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.FirstName} {customer.LastName}, Phone: {customer.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        static void Update(int id, string newFirstName, string newLastName, string newPhoneNumber)
        {
            var customers = GetCustomersFromFile();
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customer.FirstName = newFirstName;
                customer.LastName = newLastName;
                customer.PhoneNumber = newPhoneNumber;
                SaveCustomersToFile(customers);
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        static void DeleteCustomer(int id)
        {
            var customers = GetCustomersFromFile();
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
                SaveCustomersToFile(customers);
                Console.WriteLine("Customer deleted.");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        static void GetAll()
        {
            var customers = GetCustomersFromFile();
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.FirstName} {customer.LastName}, Phone: {customer.PhoneNumber}");
            }
        }

        static List<Customer> GetCustomersFromFile()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<Customer>>(json) ?? new List<Customer>();
            }
            return new List<Customer>();
        }

        static void SaveCustomersToFile(List<Customer> customers)
        {
            string jsonString = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText(path, jsonString);
        }
    }
}
