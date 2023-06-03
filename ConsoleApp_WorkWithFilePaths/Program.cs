// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, Work with files and paths!");
Console.WriteLine();
Console.WriteLine();

Console.WriteLine(Directory.GetCurrentDirectory());
Console.WriteLine();
// outputs: C:\Users\Krzysztof\Source\Repos\krzysztofautomatyk\OffcialDemoProjects\ConsoleApp_WorkWithFilePath\bin\Debug\net6.0

Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
Console.WriteLine();
// outputs: C:\User\Krzysztof\Documents

Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");
Console.WriteLine();
// returns:
// stores\201 on Windows
//
// stores/201 on macOS

Console.WriteLine(Path.Combine("stores", "201"));
Console.WriteLine();
// outputs: stores/201

Console.WriteLine(Path.GetExtension("sales.json"));
Console.WriteLine();
// outputs: .json

string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";
FileInfo info = new FileInfo(fileName);
Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}");
Console.WriteLine();
// And many more

IEnumerable<string> listOfDirectories = Directory.EnumerateDirectories("stores");

foreach (var dir in listOfDirectories)
{
    Console.WriteLine(dir);
}
Console.WriteLine();
// Outputs:
// stores/201
// stores/202
// stores/203
// stores/204

IEnumerable<string> files = Directory.EnumerateFiles("stores");

foreach (var file in files)
{
    Console.WriteLine(file);
}
Console.WriteLine();
// Outputs:
// stores/totals.txt
// stores/sales.json

// Find all *.txt files in the stores folder and its subfolders
IEnumerable<string> allFilesInAllFolders = Directory.EnumerateFiles("stores", "*.txt", SearchOption.AllDirectories);

foreach (var file in allFilesInAllFolders)
{
    Console.WriteLine(file);
}
Console.WriteLine();
// Outputs:
// stores/totals.txt
// stores/201/inventory.txt
// stores/202/inventory.txt
// stores/203/inventory.txt
// stores/204/inventory.txt

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path, so only check the end of it
        if (file.EndsWith("sales.json"))
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

var salesFiles = FindFiles("stores");

foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}
Console.WriteLine();
//stores\sales.json
//stores\201\sales.json
//stores\202\sales.json
//stores\203\sales.json
//stores\204\sales.json

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesFiles1 = FindFiles(storesDirectory);

foreach (var file in salesFiles1)
{
    Console.WriteLine(file);
}
Console.WriteLine();
//C:\Users\Krzysztof\Source\Repos\krzysztofautomatyk\OfficialDemoProjects\ConsoleApp_WorkWithFilePaths\bin\Debug\net6.0\stores\sales.json
//C:\Users\Krzysztof\Source\Repos\krzysztofautomatyk\OfficialDemoProjects\ConsoleApp_WorkWithFilePaths\bin\Debug\net6.0\stores\201\sales.json
//C:\Users\Krzysztof\Source\Repos\krzysztofautomatyk\OfficialDemoProjects\ConsoleApp_WorkWithFilePaths\bin\Debug\net6.0\stores\202\sales.json
//C:\Users\Krzysztof\Source\Repos\krzysztofautomatyk\OfficialDemoProjects\ConsoleApp_WorkWithFilePaths\bin\Debug\net6.0\stores\203\sales.json
//C:\Users\Krzysztof\Source\Repos\krzysztofautomatyk\OfficialDemoProjects\ConsoleApp_WorkWithFilePaths\bin\Debug\net6.0\stores\204\sales.json


IEnumerable<string> FindFiles2(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}
var currentDirectory2 = Directory.GetCurrentDirectory();
var storesDirectory2 = Path.Combine(currentDirectory2, "stores");

var salesFiles2 = FindFiles2(storesDirectory2);

foreach (var file in salesFiles2)
{
    Console.WriteLine(file);
}
Console.WriteLine();


// Create Directory
Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "stores", "201", "newDir"));
bool doesDirectoryExist = Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "stores", "201", "newDir"));

File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "greeting.txt"), "Hello World!");

var currentDirectory3 = Directory.GetCurrentDirectory();
var storesDirectory3 = Path.Combine(currentDirectory3, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);
var salesFiles3 = FindFiles3(storesDirectory3);

File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), String.Empty);

IEnumerable<string> FindFiles3(string folderName)
{
    List<string> salesFiles3 = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles3.Add(file);
        }
    }

    return salesFiles3;
}

// Read data from file
File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");

var salesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

Console.WriteLine(salesData.Total);

// Save data to file
var data2 = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

File.WriteAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", data2.Total.ToString());

// totals.txt
// 22385.32

// Add data to exists file
var data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

File.AppendAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", $"{data.Total}{Environment.NewLine}");

// totals.txt
// 22385.32
// 22385.32