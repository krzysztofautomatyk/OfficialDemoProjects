namespace ConsoleApp_Basic_Records
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var currency1 = new CurrencyRecord()
            //{
            //    Name = "PLN",
            //    Value = 4.5m
            //};

            //var currency2 = new CurrencyRecord()
            //{
            //    Name = "PLN",
            //    Value = 4.5m
            //};


            var currency1 = new CurrencyRecord("USD", 12.5m,"demoValue");
            var currency2 = currency1 with { Value = 2.5m };

            var name = currency1.Name;
            var value = currency1.Value;

            var (name2, value2,somevalue2) = currency1;


            Console.WriteLine("Hello, Basic Demo of Records!");

            Console.WriteLine(currency1);
            Console.WriteLine(currency2);

            Console.WriteLine($"currency1 == currency2 ? {currency1 == currency2}");
            Console.WriteLine($"object.ReferenceEquals(currency1, currency2) ? {object.ReferenceEquals(currency1, currency2)}");


        }
    }
    //public record CurrencyRecord
    //{
    //    public string Name { get; set; }
    //    public decimal Value { get; set; }
    //}


    // sharplab.io => decompilation records

    public abstract record SomeRecord(string SomeValue);

    public record CurrencyRecord(string Name, decimal Value, string SomeValue) : SomeRecord(SomeValue), IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CurrencyRecord()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class CurrencyClass
    {
        public CurrencyClass(string name, decimal value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public decimal Value { get; set; }
    

        public override string ToString()
        {
            return $"Name: {Name}, Value: {Value}";
        }

        public static bool operator == (CurrencyClass left, CurrencyClass right)
        {
            return EqualityComparer<CurrencyClass>.Default.Equals(left, right);
        }

        public static bool operator != (CurrencyClass left, CurrencyClass right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is CurrencyClass @class &&
                   Name == @class.Name &&
                   Value == @class.Value;
        }

        public virtual bool Equals(CurrencyClass other)
        {
            return Equals(other as CurrencyClass);
        }
    }


}