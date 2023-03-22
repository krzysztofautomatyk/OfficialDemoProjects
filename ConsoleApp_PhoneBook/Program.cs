using System.Threading.Channels;

namespace ConsoleApp_PhoneBook {
    internal class Program {
        static void Main(string[] args) {

            Menu.MainMenu();            
            var userInput = Console.ReadLine();

            
            while (true) {

                switch (userInput) {
                    case "1":
                        
                        Menu.InsertMenu();                      

                        break;
                    case "2":

                        Menu.DisplayContact();
                        break;
                    case "3":

                        Menu.DisplayAllContacts();

                        break;
                    case "4":

                        Menu.SearchContact();

                        break;

                    case "x":
                        return;   // Exit from main program and exit program
                    default:
                        Console.WriteLine("Invalid operation");
                        break;
                }
                Menu.MainMenu();
                userInput = Console.ReadLine();

            }

     
        }


    }
}