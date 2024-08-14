using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp_Basic_Multilanguage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Dictionary<string, Dictionary<string, string>> _languages;
        private string _currentLanguage;

        public string CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    OnPropertyChanged(nameof(CurrentLanguage));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string PolishButtonName => GetButtonName("pl");
        public string EnglishButtonName => GetButtonName("en");

        public string FormTitle { get; set; }
        public string Greeting { get; set; }
        public ObservableCollection<string> AvailableLanguages { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            AvailableLanguages = new ObservableCollection<string>();
            LoadLanguages();

            // Wczytaj język z pliku konfiguracyjnego
            string defaultLanguage = LoadDefaultLanguageFromConfig();
            Debug.WriteLine(defaultLanguage);

            if (AvailableLanguages.Contains(defaultLanguage))
            {
                SetLanguage(defaultLanguage);
            }
            else
            {
                SetLanguage("pl"); // Domyślnie ustaw język polski, jeśli język systemowy nie jest dostępny
            }
        }

        private string LoadDefaultLanguageFromConfig()
        {
            try
            {
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Languages\config.json")
))
                {
                    string json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Languages\config.json"), Encoding.UTF8);
                    var configData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (configData != null && configData.ContainsKey("DefaultLanguage"))
                    {
                        Debug.WriteLine("Odczyt:" + configData["DefaultLanguage"]);
                        return configData["DefaultLanguage"];
                    }
                }
                Debug.WriteLine("File not exist:");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Odczyt:" + ex.ToString());
                MessageBox.Show($"Błąd wczytywania pliku konfiguracyjnego: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return "en"; // Domyślny język, jeśli coś pójdzie nie tak
        }

        private void SetLanguage(string languageCode)
        {
            if (_languages.ContainsKey(languageCode))
            {
                CurrentLanguage = languageCode;
                UpdateUI();
                SaveDefaultLanguageToConfig(languageCode); // Zapisz wybrany język do pliku config.json
            }
            else
            {
                MessageBox.Show($"Nie znaleziono języka: {languageCode}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveDefaultLanguageToConfig(string languageCode)
        {
            try
            {
                var configData = new Dictionary<string, string>
        {
            { "DefaultLanguage", languageCode }
        };

                string json = JsonConvert.SerializeObject(configData, Formatting.Indented);
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Languages\config.json"), json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd zapisu pliku konfiguracyjnego: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadLanguages()
        {
            _languages = new Dictionary<string, Dictionary<string, string>>();
            string[] languageFiles = Directory.GetFiles("Languages", "*.json");

            foreach (string file in languageFiles)
            {
                try
                {
                    string fileName = Path.GetFileName(file);

                    // Pomijamy plik config.json
                    if (fileName.Equals("config.json", StringComparison.OrdinalIgnoreCase))
                        continue;

                    string languageCode = Path.GetFileNameWithoutExtension(file);
                    string json = File.ReadAllText(file);
                    var languageData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (languageData == null || !languageData.Any())
                    {
                        throw new InvalidDataException($"Plik {file} jest nieprawidłowy lub pusty.");
                    }

                    _languages[languageCode] = languageData;
                    AvailableLanguages.Add(languageCode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd ładowania pliku językowego {file}: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            OnPropertyChanged(nameof(AvailableLanguages));
        }

        private void UpdateUI()
        {
            if (_languages.TryGetValue(_currentLanguage, out var currentStrings))
            {
                FormTitle = currentStrings.GetValueOrDefault("FormTitle", "Domyślny tytuł");
                Greeting = currentStrings.GetValueOrDefault("Greeting", "Witaj!");

                OnPropertyChanged(nameof(FormTitle));
                OnPropertyChanged(nameof(Greeting));
                OnPropertyChanged(nameof(PolishButtonName));  // Dodaj to
                OnPropertyChanged(nameof(EnglishButtonName)); // Dodaj to
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is string selectedLanguage)
            {
                SetLanguage(selectedLanguage);
            }
        }

        public string GetButtonName(string languageCode)
        {
            if (_languages.TryGetValue(_currentLanguage, out var currentStrings) &&
                currentStrings.ContainsKey($"{languageCode}Button"))
            {
                return currentStrings[$"{languageCode}Button"];
            }

            return languageCode; // Fallback do kodu języka, jeśli nie znaleziono tłumaczenia
        }
    }
}