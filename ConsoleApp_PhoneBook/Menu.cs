using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_PhoneBook {
    public static class Menu {
        public static void MainMenu() {
            Console.Clear();

            string text = "Hello, from the PhoneBook App!";
            string[] words = text.Split(' ');

            ConsoleColor[] colors = new ConsoleColor[] {
                ConsoleColor.Magenta, ConsoleColor.Gray,ConsoleColor.Cyan,ConsoleColor.Yellow, ConsoleColor.Magenta
            };

            for (int i = 0; i < words.Length; i++) {
                Console.ForegroundColor = colors[i];
                Console.Write(words[i] + " ");
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("1");
            Console.ResetColor();
            Console.WriteLine(". Add contact");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("2");
            Console.ResetColor();
            Console.WriteLine(". Display contact by number");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("3");
            Console.ResetColor();
            Console.WriteLine(". Display all contats");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("4");
            Console.ResetColor();
            Console.WriteLine(". Search contacts");

            Console.WriteLine("");


            Console.Write("To exit press key");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" \"x\"");
            Console.ResetColor();

            Console.WriteLine("");
            Console.WriteLine("");

            Console.Write("Please press key \"");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("1-4");
            Console.ResetColor();
            Console.Write("\" or \"");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("x");
            Console.ResetColor();
            Console.Write("\" : ");

            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public static void InsertMenu() {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("Insert number:");
            var number = Console.ReadLine();
            Console.WriteLine("Insert nname:");
            var name = Console.ReadLine();

            var newContact = new Contact(name, number);
            
            PhoneBook.Contacts.Add(newContact);
            
        }

        public static void DisplayContact() {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("Insert number:");

            var numberToSearch = Console.ReadLine();
            var phoneBook = new PhoneBook();
            phoneBook.DisplayContact(numberToSearch);

            Console.WriteLine("Press any key to back to menu...");
            Console.ReadKey();

        }

        public static void DisplayAllContacts() {
            Console.Clear();
            Console.ResetColor();
            var phoneBook = new PhoneBook();
            phoneBook.DisplayAllContacts();

            Console.WriteLine("Press any key to back to menu...");
            Console.ReadKey();

        }

        public static void SearchContact() {
            Console.Clear();
            Console.ResetColor();
            var phoneBook = new PhoneBook();
            Console.WriteLine("Insert search phrase:");

            var searchPhrase = Console.ReadLine();

            phoneBook.DisplayMatchingContacts(searchPhrase);

            Console.WriteLine("Press any key to back to menu...");
            Console.ReadKey();

        }
    }
}
