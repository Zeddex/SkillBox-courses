using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Homework_12
{
    class InOut : IIO
    {
        string json;

        /// <summary>
        /// Serialization method, save data to file
        /// </summary>
        public void SaveData()
        {
            json = JsonConvert.SerializeObject(Core.org);
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

        /// <summary>
        /// Load Employees from file
        /// </summary>
        public ObservableCollection<Organisation> LoadData()
        {
            OpenFileDialog load = new OpenFileDialog();
            load.FileName = "data";
            load.DefaultExt = ".json";
            load.Filter = "JSON file (.json)|*.json";

            if (load.ShowDialog() == true)
            {
                string filename = load.FileName;

                using (StreamReader sr = new StreamReader(filename))
                {
                    json = sr.ReadToEnd();
                }
                JsonConverter[] converters = { new EmployeeConverter() };
                Core.org = JsonConvert.DeserializeObject<ObservableCollection<Organisation>>(json, new JsonSerializerSettings() { Converters = converters });
            }

            MessageBox.Show("Data successsfully loaded", "Load data", MessageBoxButton.OK, MessageBoxImage.Information);
            return Core.org;
        }

        public class EmployeeConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(Employee));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject jo = JObject.Load(reader);
                if (jo["Position"].Value<string>() == "CEO")
                    return jo.ToObject<CEO>(serializer);

                if (jo["Position"].Value<string>() == "Administrator")
                    return jo.ToObject<Administrator>(serializer);

                if (jo["Position"].Value<string>() == "Manager")
                    return jo.ToObject<Manager>(serializer);

                if (jo["Position"].Value<string>() == "Staff")
                    return jo.ToObject<Staff>(serializer);

                if (jo["Position"].Value<string>() == "Intern")
                    return jo.ToObject<Intern>(serializer);

                return null;
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
