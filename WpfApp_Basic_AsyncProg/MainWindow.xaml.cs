using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_Basic_AsyncProg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Read_Files_Handler(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "Reading started...";
            var stopWatch = Stopwatch.StartNew();
            ProcessFiles();
            stopWatch.Stop();
            ResultLabel.Content = $"Finisher in: {stopWatch.Elapsed.TotalMilliseconds}";

        }

        

        private async void Read_Files_Handler_Async(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "Reading started...";
            var stopWatch = Stopwatch.StartNew();
            var result = await ProcessFilesAsync();
            stopWatch.Stop();
            ResultLabel.Content = $"Finisher in: {stopWatch.Elapsed.TotalMilliseconds} and result {result}";

        }

        private void ProcessFiles()
        {

            var path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\";

            for (int i = 1; i <= 5; i++)
            {
                var filePath = path + $"{i}.txt";

                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    ResultLabel.Content = $"Reading {filePath}";

                    Thread.Sleep(1000);

                    var fileContent = reader.ReadToEnd();
                }
            }
        }

        private async Task<int> ProcessFilesAsync()
        {

            var path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\";
            var result = 0;

            List<Task> tasks = new List<Task>();

            for (int i = 1; i <= 5; i++)
            {
                var filePath = path + $"{i}.txt";

                var task = Task.Run(() =>
                {
                     using (var reader = new StreamReader(filePath, Encoding.UTF8))
                     {
                        //ResultLabel.Content = $"Reading {filePath}";
                          Task.Delay(1000);
                        // await Task.Delay(1000);
                        //var fileContent = await Task.Run(() => reader.ReadToEnd());

                        var fileContent = reader.ReadToEnd();
                         result = result + fileContent.Length;
                     }
                 });
                tasks.Add(task);    
            }

            await Task.WhenAll(tasks);

            return result;
        }

        private async void Read_Files_Handler_Async_Delay(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = $"START";

            await Task.Delay(5000);

            ResultLabel.Content = $"STOP";
        }
    }
}