using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace Homework_16
{
    public static class InOut
    {
        /// <summary>
        /// Save json data to file
        /// </summary>
        /// <param name="bank"></param>
        public static async void SaveDataAsync(ObservableCollection<BankDep> bank)
        {
            await Task.Run(() => SaveData(bank));
        }

        /// <summary>
        /// Serialization method, save data to file
        /// </summary>
        static void SaveData(ObservableCollection<BankDep> bank)
        {
            string json = JsonConvert.SerializeObject(bank);
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "data";
            save.DefaultExt = ".json";
            save.Filter = "JSON file (.json)|*.json";

            if (save.ShowDialog() == true)
            {
                string filename = save.FileName;

                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine(json);
                }
            }
            MessageBox.Show("Data successfully saved", "Save data", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
