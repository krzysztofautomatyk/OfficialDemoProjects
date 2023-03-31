using System.Reflection;

namespace ConsoleApp_Basic_Reflection_Attributes
{
    class Program
    {
        static void Display(object obj)
        {
            Type objType =  obj.GetType();
            var properties = objType.GetProperties();

            foreach ( var property in properties )
            {
                var propValue = property.GetValue( obj);
                var propType = propValue.GetType();

                if ( propType.IsPrimitive || propType == typeof(string))
                {
                    var displayPropetyAttribute = property.GetCustomAttribute<DisplayPropertyAttribute>();

                    if(displayPropetyAttribute != null )
                    {
                        Console.WriteLine($"{displayPropetyAttribute.DisplayName}:{propValue}");
                    }
                    else
                    {
                        Console.WriteLine($"{property.Name}:{propValue}");
                    }                    
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Basic Demo of Reflection and Attributes!");

            Addresss addresss = new Addresss()
            {
                City = "Krakow",
                PostalCode = "31-556",
                Street = "Grodzka 65"
            };

            Person person = new Person()
            {
                FirstName = "John",
                LastName = "Smith",
                Address = addresss
            };

            Console.WriteLine("Person: ");
            Display(person);
            //Console.WriteLine("Address: ");
            //Display(addresss);

            Console.WriteLine("Insert person property to update:");
            var propertyToUpdate = Console.ReadLine();

            Console.WriteLine("Insert value:");
            var value = Console.ReadLine();

            SetValue(person,propertyToUpdate,value);

            Console.WriteLine("Person: ");
            Display(person);

        }

        static void SetValue<T>(T obj, string propName, string value)
        {
            Type objType = typeof(T);

            var propopertyToUpdate = objType.GetProperty(propName);

            if (propopertyToUpdate != null)
            {
                propopertyToUpdate.SetValue(obj, value);
            }
        }
    }
}