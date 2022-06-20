using FinanceManager.Functional.GlobalPatterns.Observable;
using FinanceManager.Functional.GlobalPatterns.Observe;
using Lexipun.DotNetFramework.DataProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.functional.Localization
{
    public class Translate : INotifyPropertyChanged
    {
        #region Fields
        private static readonly string pathToSave = "./Resources/SavedData/";
        private static readonly string filetoSave = "Language.settings";
        private static PropertyInfo[] properties;
        private static Language language = new Language();

        #endregion Fields

        #region Properties
        protected static Type Type { get; } = typeof(Translate);
        protected static string CurrentCulture { get; set; }
        public static string AddCause { get; set; }
        public static string Save { get; set; }
        public static string Cancel { get; set; }
        public static string Income { get; set; }
        public static string Expense { get; set; }
        public static string ExistCause { get; set; }
        public static string IncomeTooSmall { get; set; }
        public static string ExpenseTooBig { get; set; }
        public static string Summary { get; set; }


        #endregion

        static Translate()
        {
            properties = Type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            LoadCurrentLanguage();
        }


        private static void SetLanguage(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                Language.Culture = CultureInfo.GetCultureInfo(culture);
                CurrentCulture = culture;
            }
            else
            {
                Language.Culture = CultureInfo.InstalledUICulture;
                CurrentCulture = CultureInfo.InstalledUICulture.TwoLetterISOLanguageName;
            }

            foreach (PropertyInfo property in properties)
            {
                property.SetValue(language, Language.ResourceManager.GetString(property.Name, Language.Culture));
                OnStaticPropertyChanged(property.Name);
            }

        }

        public static async void SetLanguageAsync(string culture)
        {
            await Task.Run(() => SetLanguage(culture));
            SaveCurrentLanguage();
            UpdateDataObservable.Observe.PushUpdateDependenciedData(Type);
        }


        private static void LoadCurrentLanguage()
        {
            string loadedData = string.Empty;

            if (Directory.Exists(pathToSave) && File.Exists(string.Concat(pathToSave, filetoSave)))
            {
                using (StreamReader reader = new StreamReader(string.Concat(pathToSave, filetoSave)))
                {
                    loadedData = reader.ReadToEnd();
                }
            }

            SetLanguageAsync(loadedData);
        }


        private static void SaveCurrentLanguage()
        {
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            using (StreamWriter writer = new StreamWriter(string.Concat(pathToSave, filetoSave)))
            {
                writer.Write(CurrentCulture);
                writer.Close();
            }
        }

        #region INotifyPropertyChanged member



        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public static void OnStaticPropertyChanged(string PropertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
        }


        #endregion INotifyPropertyChanged member
    }
}
