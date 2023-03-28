using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace ConsoleApp_Basic_GenericType
{
    public class Program
    {
        public delegate void Display(string value);
        public delegate bool GenericPredicate<T>(T value);
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Basic Demo of Generic Type!");

            //var stringRepository = new Repository<string>();
            //stringRepository.Add("Jan");

            var userRepository = new Repository<string,User>();
            userRepository.Add("Jan", new User()
            {
                Name = "Jan"
            });

            var getUser = userRepository.GetElement("Jan"); 
            
            int[] intArray = new int[] { 1, 2, 5 };

           // Utils.Swap<int>(ref intArray[0],ref intArray[2]);
            Utils.Swap(ref intArray[0], ref intArray[2]);

            Console.WriteLine(string.Join(" , ", intArray));

            Display display = (string value) => Console.Write(value + ", ");
            display("Demo");

            var number = new int[] { 10, 20, 30 };
            DisplayNumbers(number, display);

            Console.WriteLine(); Console.WriteLine();

            var count = Count(number,(int value) => value > 25);
            Console.WriteLine(count);

            Console.WriteLine(); Console.WriteLine();

            var strings = new string[] { "Jan", "Kowalski", "Piotr" };
            var countString = Count(strings, (string value) => value.Length > 3);

            Console.WriteLine($"Conut strings: {countString}");
        }

        static int Count<T>(IEnumerable<T> elements, GenericPredicate<T> predicate)
        {
            int count = 0;
            foreach (T element in elements)
            {
                if(predicate(element))
                {
                    count++;
                }
            }
            return count;
        }

        static void DisplayNumbers(IEnumerable<int> number, Display display)
        {
            foreach(int numberItem in number)
            {
                display(numberItem.ToString());
            }
        }
    }
}