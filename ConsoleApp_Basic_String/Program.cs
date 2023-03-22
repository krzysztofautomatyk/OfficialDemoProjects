using System.Data.SqlTypes;
using System.Text;

namespace ConsoleApp_Basic_String
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Basic Demo of String!");
            Console.WriteLine("Insert input string:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            string input = Console.ReadLine();

            Console.ResetColor();




            //SubString(input);
            //Replace(input);
            //Modify(input);
            //AlterTextCase(input);
            //Split(input);                 // "Jan Andrzej Kowalski"
            //CheckString(input);           // "doc-sadsa.txt
            CamelToKebabCase(input);      // someVariableName
            //KebabToCamelCase(input);      // some-variable-name
        }

        static void SubString(string input)
        {
            if (input.Length > 10)
            {
                string startSubstring = input.Substring(0, 10);
                string endSubstring = input.Substring(input.Length - 10);

                Console.WriteLine($"{startSubstring} ... , ... {endSubstring}");
                Console.WriteLine(input);
            }
        }

        static void Replace(string input)
        {
            string template = "Hello {name}, how are you?";
            string output = template.Replace("{name}", input);
            Console.WriteLine(output);
        }

        static void Modify(string input)
        {
            string removedString = input.Remove(5);  // First 5 char is stay
            string substring = input.Substring(5);   // First 5 char is cutted
            string insert = input.Insert(5, "DEMO");  // At 5 position in string add text
            string trim = input.Trim(); // remove wihite space from begin and end string

            Console.WriteLine($"String Remove : {removedString}");
            Console.WriteLine($"String Substring : {substring}");
            Console.WriteLine($"String Insert : {insert}");
            Console.WriteLine($"String Trim : {trim}");
        }

        static void AlterTextCase(string input)
        {
            string upperCase = input.ToUpper();
            string lowerCase = input.ToLower();

            Console.WriteLine($"String ToUpper : {upperCase}");
            Console.WriteLine($"String ToLower : {lowerCase}");
        }

        static void Split(string input)
        {
            string[] inputParts = input.Split(" ");
            string firstName = inputParts[0];
            string lastName = inputParts[inputParts.Length - 1];

            Console.WriteLine($"Hello {firstName} {lastName}");
        }

        static void CheckString(string input)
        {
            bool isTextFile = input.EndsWith(".txt");
            bool isDocFile = input.StartsWith("doc-");
            bool isContance = input.Contains("test");

            Console.WriteLine($"String EndsWith : {isTextFile}");
            Console.WriteLine($"String StartsWith : {isDocFile}");
            Console.WriteLine($"String Contains : {isContance}");
        }

        static void CamelToKebabCase(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsUpper(c) == true)
                {
                    sb.Append('-');
                    sb.Append(char.ToLower(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            Console.WriteLine(sb.ToString());
        }

        static void KebabToCamelCase(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                if (currentChar != '-')
                {
                    sb.Append(currentChar);
                }
                else
                {
                    char nextChar = input[i + 1];
                    sb.Append(char.ToUpper(nextChar));
                    i++;
                }
            }
            Console.WriteLine(sb.ToString());

        }
    }
}