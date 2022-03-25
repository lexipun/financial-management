using Lexipun.DotNetFramework.DataProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.functional.Localization
{
    public class Translate : INotifyPropertyChanged
    {
        #region Fields
        private static readonly string pathToSave = "./resources/SavedData/";
        private static readonly string filetoSave = "Language.settings";
        private static string addCause;

        #endregion Fields

        #region Properties
        public static string AddCause { get => addCause; set{ addCause = value; OnStaticPropertyChanged("AddCause"); } }
        protected static string CurrentCulture { get; set; }


        #endregion

        public void SetLanguage(string culture)
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

            AddCause = Language.AddCause;
        }

        public async void SetLanguageAsync(string culture)
        {
            await Task.Run(() => SetLanguage(culture));
            SaveCurrentLanguage();
            UpdateObservable.Update();
        }


        public void LoadCurrentLanguage()
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


        public void SaveCurrentLanguage()
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

        public static void OnStaticPropertyChanged(string PropertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion INotifyPropertyChanged member
    }
}
