using System.Diagnostics;
using System.Threading.Channels;

namespace ConsoleApp_Basic_DateTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Basic Demo of DateTime!");

            //DateTimeModification();
            //DateTimeFormatting();
            //TimeMeasurement();
            //DateTimeHelpers();
            //Reservation();
            Reservation();
        }

        static void DateTimeModification()
        {
            DateTime now = DateTime.Now;
            DateTime openDate = new DateTime(1992, 6, 18);

            TimeSpan result = now - openDate;

            DateTime expiresAt = now.AddDays(7);
            DateTime expiresAt2 = now.Add(new TimeSpan(days: 7, hours: 0, minutes:0, seconds:0));  

            bool expiresAtTheSameDay = expiresAt.Date == expiresAt2.Date;

            Console.WriteLine(result.Days);
            Console.WriteLine(result.TotalDays);
            Console.WriteLine($"DateTime AddDays : {expiresAt}");
            Console.WriteLine($"DateTime Add TimeSpan : {expiresAt2}");
            Console.WriteLine($"DateTime Equal : {expiresAtTheSameDay}");
        }

        static void DateTimeFormatting()
        {
            DateTime now = DateTime.Now;

            Console.WriteLine($"DateTime ToShortDateString : {now.ToShortDateString()}");
            Console.WriteLine($"DateTime ToLongDateString : {now.ToLongDateString()}");
            Console.WriteLine($"DateTime ToString : {now.ToString("g")}");
            Console.WriteLine($"DateTime ToString : {now.ToString("G")}");
            Console.WriteLine($"DateTime ToString : {now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        static void TimeMeasurement()
        {
            Console.WriteLine("What is 2+5?");
            Console.WriteLine("A) 4");
            Console.WriteLine("B) 6");
            Console.WriteLine("C) 7");

            

            DateTime start = DateTime.Now;
            Stopwatch sw = Stopwatch.StartNew();

            string answer = Console.ReadLine();

            sw.Stop();
            DateTime stop = DateTime.Now;

            TimeSpan responseTime = stop - start;

            Console.WriteLine($"Response TimeSpan : {responseTime.TotalSeconds}");
            Console.WriteLine($"Response Stopwatch : {sw.Elapsed.TotalSeconds}");
        }

        static void DateTimeHelpers()
        {
            int daysInMay2023 = DateTime.DaysInMonth( 2023, 5 );
            bool is2023LeapYear = DateTime.IsLeapYear( 2023 );

            Console.WriteLine($"DateTime DaysInMonth : {daysInMay2023}");
            Console.WriteLine($"DateTime IsLeapYear : {is2023LeapYear}");
        }


        static void Reservation()
        {
            var bookedReservations = GetBookedReservation();
            DisplayReservations( bookedReservations );

            Console.WriteLine("Insert new booking start date: (dd.MM.yyyy)");            
            string startDateString = Console.ReadLine();
            DateTime startDate = DateTime.ParseExact(startDateString, "dd.MM.yyyy", null);

            Console.WriteLine("Insert new booking end date: (dd.MM.yyyy)");
            string endDateString = Console.ReadLine();
            DateTime endDate = DateTime.ParseExact(endDateString, "dd.MM.yyyy", null);

            bool isNewReservationPossible = IsNewReservationPossible(startDate, endDate, bookedReservations);

            

            if (isNewReservationPossible)
            {
                Console.WriteLine("Reservation is done");
            }
            else { 
                Console.WriteLine("Reservation is not possible!"); 
            }

            static List<Reservation> GetBookedReservation()
            {
                var reservations = new List<Reservation>() {
                    new Reservation(new DateTime(2023,05,4),new DateTime(2023,05,06)),
                    new Reservation(new DateTime(2023,05,8),new DateTime(2023,05,12)),
                    new Reservation(new DateTime(2023,05,18),new DateTime(2023,05,20)),
                    new Reservation(new DateTime(2023,05,24),new DateTime(2023,05,25)),
                };

                return reservations;
            }

            static void DisplayReservations(List<Reservation> reservations)
            {
                Console.WriteLine("Booked reservations:");

                foreach (Reservation reservation in reservations)
                {
                    Console.WriteLine($"From: {reservation.From} to {reservation.To} is booked");
                }
            }

            static bool IsNewReservationPossible(DateTime startDate, DateTime endDate,List<Reservation> bookedreservations)
            {
                foreach (var bookReservation in bookedreservations)
                {
                    if(startDate.Date >= bookReservation.From.Date && startDate.Date <= bookReservation.To.Date
                        || endDate.Date >= bookReservation.From.Date && endDate.Date <= bookReservation.To.Date)
                    {
                         return false;
                    }

                    if(startDate.Date <= bookReservation.From.Date && endDate.Date >= bookReservation.To.Date)
                    {
                        return false;
                    }

                }

                return true;
            }
        }


    }
}