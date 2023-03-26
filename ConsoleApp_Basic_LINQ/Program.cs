using CsvHelper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;

namespace ConsoleApp_Basic_LINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Basic Demo of LINQ!");

            string csvPath = @"\googleplaystore1.csv";
            string sciezka = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var googleApps = LoadGoogleAps(sciezka + csvPath);

            var sd = new GoogleApp();

            //Display(googleApps);
            //GetData(googleApps);
            //ProjectData(googleApps);
            //DivideData(googleApps);
            //OrderData(googleApps);
            //DataSerOperation(googleApps);
            //DataVerification(googleApps);
            //GroupingData(googleApps);
            //GroupDataOperation(googleApps);
            DataIntegration();


        }
        static void DataIntegration()
        {
            var people = LoadePeople();
            var addresses = LoadAddresses();

            //var joinedData = people.Join(addresses,
            //    p => p.ID,
            //    a => a.PersonID,
            //    (person, address) => new { person.Name, address.Street, address.City });


            var joinedData = people.GroupJoin(addresses,
                person => person.ID,
                address => address.PersonID,
                (person, addressList) => new { person.Name, Addresses = addressList });

            foreach (var element in joinedData)
            {
                Console.WriteLine($"Name: {element.Name}");
                foreach (var address in element.Addresses)
                {
                    Console.WriteLine($"\t City: {address.City}, Street: {address.Street}");
                }
            }
            Console.WriteLine();
        }

        static List<Address> LoadAddresses()
        {
            string jsonPath = @"\Addresses.json";
            string sciezka = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var json = File.ReadAllText(sciezka + jsonPath);

            return JsonConvert.DeserializeObject<List<Address>>(json);
        }

        static List<Person> LoadePeople()
        {
            string jsonPath = @"\People.json";
            string sciezka = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var json = File.ReadAllText(sciezka + jsonPath);

            return JsonConvert.DeserializeObject<List<Person>>(json);
        }

        static void GroupDataOperation(IEnumerable<GoogleApp> googleApps)
        {
            var categoryGroup = googleApps
                .GroupBy(g => g.Category);

            var categoryGroup2 = googleApps
                .GroupBy(g => g.Category)
                .Where(g => g.Min(a=>a.Reviews) >=10);

            foreach (var group in categoryGroup2)
            {
                var avarageReviews = group.Average(g => g.Reviews);
                var minReviews = group.Min(g => g.Reviews);
                var maxReviews = group.Max(g => g.Reviews);
                var sumReviews = group.Sum(g => g.Reviews);
                var count = group.Count();
                var key = group.Key;

                var allAppsFromGroupHaveRatingOfThree = group.All(g => g.Rating > 3.0);

                Console.WriteLine();
                Console.WriteLine($"Displaing elements for group {group.Key} ");
                Console.WriteLine($"Display min Reviews: {minReviews}");
                Console.WriteLine($"Display max Reviews: {maxReviews}");
                Console.WriteLine($"Display avg Reviews: {avarageReviews}");
                Console.WriteLine($"Display sum Reviews: {sumReviews}");
                Console.WriteLine($"Display count Reviews: {count}");
                Console.WriteLine($"Display all Rating > 3.0 : {allAppsFromGroupHaveRatingOfThree}");
            }
        }
        static void GroupingData(IEnumerable<GoogleApp> googleApps)
        {
            var categoryGroup = googleApps.GroupBy(g => g.Category);
            var categoryGroup2 = googleApps.GroupBy(g => new {g.Category,g.Type});
            var artAndDesigns = categoryGroup.FirstOrDefault(g => g.Key == Category.ART_AND_DESIGN);
            var app = artAndDesigns.Select(g => g);
            var appList = artAndDesigns.ToList();

            Console.WriteLine($"Displaing elements for group {artAndDesigns.Key}");
            

            foreach(var  group in categoryGroup2)
            {
                var key = group.Key;
                Console.WriteLine($"Displaing elements for group {group.Key.Category} and {group.Key.Type}");
                var apps = group.ToList();
                Display(apps);
            }

            Display(app);
            Console.WriteLine();
            Display(appList);
        }

        static void DataVerification(IEnumerable<GoogleApp> googleApps)
        {
            var allOperatorResult = googleApps.Where(g => g.Category == Category.WEATHER)
                .All(g => g.Reviews > 10);
            var anyOperatorResult = googleApps.Where(g => g.Category == Category.WEATHER)
                .Any(g => g.Reviews > 2_000_000);
            

            Console.WriteLine($"All operator Result {allOperatorResult}");

            Console.WriteLine($"Any operator Result {anyOperatorResult}");
        }

        static void DataSerOperation(IEnumerable<GoogleApp> googleApps)
        {
            var paidAppsCategories = googleApps.Where(g => g.Type == Type.Paid)
                 .Select(g => g.Category).Distinct();   // Unikalne wartości

            Console.WriteLine($"Paid apps categories: {string.Join(", ", paidAppsCategories)}");

            var setA = googleApps.Where(g => g.Rating > 4.7 && g.Type == Type.Paid && g.Reviews > 1000);
            var setB = googleApps.Where(g => g.Name.Contains("Pro") && g.Rating > 4.6 && g.Reviews > 10000);

            var appsUnion = setA.Union(setB); // it creates a set that combines the elements of setA and setB, without including any duplicates. 
            var appsIntersect = setA.Intersect(setB); // it creates a set that contains only the elements that are in both setA and setB.
            var appExcept = setA.Except(setB); //it creates a set that contains all the elements of setA, except for those that are also in setB

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Display(setA);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Display(setB);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.WriteLine("Union ");
            //Display(appsUnion);

            //Console.WriteLine("Intersect ");
            //Display(appsIntersect);

            Console.WriteLine("Except ");
            Display(appExcept);

            Console.ResetColor();

        }

        static void OrderData(IEnumerable<GoogleApp> googleApps)
        {
            var highRatedBeautyApps = googleApps.Where(g => g.Rating > 4.4 && g.Category == Category.BEAUTY);
            var sortedResults = highRatedBeautyApps
                .OrderByDescending(g => g.Rating)
                .ThenBy(g => g.Name)
                .Take(6);


            Console.WriteLine();
            Display(highRatedBeautyApps);

            Console.WriteLine();
            Display(sortedResults);
        }

        static void GetData(IEnumerable<GoogleApp> googleApps)
        {

            ////predyktata
            //bool IsHighRatedApp(GoogleApp app)
            //{
            //    return app.Rating > 4.6;
            //}

            //var query = googleApps.Where(IsHighRatedApp);

            //var highRatedApp = googleApps.Where((GoogleApp g) => g.Rating > 4.6).ToList();
            var highRatedApp = googleApps.Where(g => g.Rating > 4.6);
            var highRatedBeautyApps = googleApps.Where(g => g.Rating > 4.6 && g.Category == Category.BEAUTY);
            var firstHighRatedBeautyApps = highRatedBeautyApps.FirstOrDefault(g => g.Reviews <300);
            var lastHighRatedBeautyApps = highRatedBeautyApps.LastOrDefault(g => g.Reviews < 300);



            Console.WriteLine();
            Display(highRatedApp);

            Console.WriteLine();
            Display(highRatedBeautyApps);

            Console.WriteLine();
            Display(firstHighRatedBeautyApps);

            Console.WriteLine();
            Display(lastHighRatedBeautyApps);

        }   

        static void DivideData(IEnumerable<GoogleApp> googleApps)
        {
            var highRatedBeautyApps = googleApps.Where(g => g.Rating > 4.6 && g.Category == Category.BEAUTY);
            var high5RatedBeautyAppsNames = highRatedBeautyApps.Take(5);
            var high5RatedBeautyAppsNamesTakeWhile = highRatedBeautyApps.TakeWhile(g => g.Reviews > 1500);
            var skippedResults = highRatedBeautyApps.SkipWhile(g => g.Reviews > 1500);

            Console.WriteLine();
            Display(highRatedBeautyApps);

            Console.WriteLine();
            Display(high5RatedBeautyAppsNames);

            Console.WriteLine();
            Display(high5RatedBeautyAppsNamesTakeWhile);

            Console.WriteLine();
            Display(high5RatedBeautyAppsNamesTakeWhile);

            Console.WriteLine();
            Display(skippedResults);

        }

        static void ProjectData(IEnumerable<GoogleApp> googleApps)
        {
            var highRatedBeautyApps = googleApps.Where(g => g.Rating > 4.6 && g.Category == Category.BEAUTY);
            var highRatedBeautyAppsNames = highRatedBeautyApps.Select(g => g.Name);            

            var dtos = highRatedBeautyApps.Select(g => new GoogleAppDto()
            {
                Reviews = g.Reviews,
                Name= g.Name
            });

            foreach(var dto in dtos)
            {
                Console.WriteLine($"{dto.Name}: {dto.Reviews}");
            }

            // Typ anonymous
            var anonymousDtos = highRatedBeautyApps.Select(g => new 
            {
                Reviews = g.Reviews,
                Name = g.Name,
                Category = g.Category
            });

            foreach (var dto in anonymousDtos)
            {
                Console.WriteLine($"{dto.Name}: {dto.Reviews} -> {dto.Category}");
            }

            var genres = highRatedBeautyApps.Select(g => g.Genres);
            var genres2 = highRatedBeautyApps.SelectMany(g => g.Genres);
            Console.WriteLine();
            Console.WriteLine(string.Join(", ", genres2));

            Console.WriteLine();
            Display(highRatedBeautyApps);


            Console.WriteLine();
            Console.WriteLine(string.Join(", ", highRatedBeautyAppsNames));
        }

        static void Display(IEnumerable<GoogleApp> googleApps)
        {
            foreach (var googleApp in googleApps)
            {
                Console.WriteLine(googleApp);
            }

        }
        static void Display(GoogleApp googleApp)
        {
            Console.WriteLine(googleApp);
        }

        static List<GoogleApp> LoadGoogleAps(string csvPath)
        {
            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<GoogleAppMap>();
                var records = csv.GetRecords<GoogleApp>().ToList();
                return records;
            }

        }
    }
}