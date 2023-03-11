using System.Threading.Channels;

namespace ConsoleApp_PhoneBook {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, from the PhoneBook App!");

            Console.WriteLine("1. Add contact");
            Console.WriteLine("2. Display contact by number");
            Console.WriteLine("3. Display all contats");
            Console.WriteLine("4. Search contacts");
            Console.WriteLine("");
            Console.WriteLine(" To exit press key \"x\" ");

            var userInput = Console.ReadLine();

            var phoneBook = new PhoneBook();

            while (true) {

                switch (userInput) {
                    case "1":
                        Console.WriteLine("Insert number:");
                        var number = Console.ReadLine();
                        Console.WriteLine("Insert nname:");
                        var name = Console.ReadLine();

                        var newContact = new Contact(name, number);

                        phoneBook.AddContact(newContact);

                        break;
                    case "2":
                        Console.WriteLine("Insert number:");

                        var numberToSearch = Console.ReadLine();

                        phoneBook.DisplayContact(numberToSearch);

                        break;
                    case "3":

                        phoneBook.DisplayAllContacts();

                        break;
                    case "4":

                        Console.WriteLine("Insert search phrase:");

                        var searchPhrase = Console.ReadLine();

                        phoneBook.DisplayMatchingContacts(searchPhrase);

                        break;

                    case "x":
                        return;   // Exit from main program and exit program
                    default:
                        Console.WriteLine("Invalid operation");
                        break;
                }
                Console.WriteLine("Select operation:");
                userInput = Console.ReadLine();

            }

     
        }


    }
}