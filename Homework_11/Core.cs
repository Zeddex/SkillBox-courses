using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace Homework_11
{
    class Core
    {
        string json;
        uint parId;      // parent id for sub organisation
        private Random rnd = new Random();
        private ObservableCollection<Organisation> org;
        private ObservableCollection<Organisation> subDepartment;

        /// <summary>
        /// Create main organisation
        /// </summary>
        /// <param name="amount">number of root departments</param>
        public ObservableCollection<Organisation> CreateOrg(int amount)
        {
            org = new ObservableCollection<Organisation>();
            org.Add(new Organisation("MainOrg"));
            CreateSubOrgs(amount);
            AddEmpToOrg(500);
            List<int> deptsWithoutChilds = GetSubDeptsWODescendants();
            List<int> deptsWithChilds = GetDeptsWithDescendants(deptsWithoutChilds);
            GetAdminSalary(deptsWithoutChilds);
            GetAllSalary(deptsWithChilds);
            return org;
        }

        /// <summary>
        /// Create root departaments
        /// </summary>
        /// <param name="amount">Amount of sub departaments</param>
        private void CreateSubOrgs(int amount)
        {
            ++parId;
            for (int i = 0; i < amount; i++)
            {
                org.Add(new Organisation() {ParId = parId});
            }
            CreateSubDepts(amount);
            parId = 0;
        }

        /// <summary>
        /// Create sub departaments
        /// </summary>
        /// <param name="amount"></param>
        private void CreateSubDepts(int amount)
        {
            for (int i = 0; i < amount*4; i++)
            {
                org.Add(new Organisation(){ParId = (uint)rnd.Next(2, amount+2)});
            }
        }

        /// <summary>
        /// Add employees to department and calculate their salary (without administrator's salary)
        /// </summary>
        /// <param name="empAmount">Amount of employees</param>
        private void AddEmpToOrg(int empAmount)
        {
            org[0].Employees.Add(new CEO());
            int orgNumb;

            for (int i = 0; i < empAmount; i++)
            {
                switch (rnd.Next(4))
                {
                    case 0:     // There is only 1 administrator could be in one sub department
                        orgNumb = rnd.Next(1, org.Count);
                        if (!CheckAdminInOrg(org[orgNumb].Employees))
                        {
                            org[orgNumb].Employees.Add(new Administrator());
                            org[orgNumb].AdministratorId = (uint)org[orgNumb].Employees.Count - 1;      // get Administrator ID

                        }
                        break;
                    case 1:
                        orgNumb = rnd.Next(1, org.Count);
                        org[orgNumb].Employees.Add(new Manager());

                        foreach (var emp in org[orgNumb].Employees)     // add Manager's salary to sub department salary list
                        {
                            if (emp is Manager)
                            {
                                org[orgNumb].SalaryAmount += emp.Salary;
                                break;
                            }
                            
                        }
                        break;
                    case 2:
                        orgNumb = rnd.Next(1, org.Count);
                        org[orgNumb].Employees.Add(new Staff());

                        foreach (var emp in org[orgNumb].Employees)     // add Staff's salary to sub department salary list
                        {
                            if (emp is Staff)
                            {
                                org[orgNumb].SalaryAmount += emp.Salary;
                                break;
                            }
                        }
                        break;
                    case 3:
                        orgNumb = rnd.Next(1, org.Count);
                        org[orgNumb].Employees.Add(new Intern());

                        foreach (var emp in org[orgNumb].Employees)     // add Intern's salary to sub department salary list
                        {
                            if (emp is Intern)
                            {
                                org[orgNumb].SalaryAmount += emp.Salary;
                                break;
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Calculate administrator's and the whole department's salary in the list of the departments without childs
        /// </summary>
        private void GetAdminSalary(List<int> depNumbs)
        {
            int AdminID;
            int estimatedSalary;

            for (int i = 0; i < depNumbs.Count; i++)             // Get and set Administrator's salary
            {
                AdminID = (int)org[depNumbs[i]].AdministratorId;
                estimatedSalary = (int)org[depNumbs[i]].SalaryAmount / 100 * 15;
                if (estimatedSalary > org[depNumbs[i]].Employees[AdminID].Salary)
                {
                    org[depNumbs[i]].Employees[AdminID].Salary = (uint)estimatedSalary;
                }
                org[depNumbs[i]].SalaryAmount = org[depNumbs[i]].Employees[AdminID].Salary + org[depNumbs[i]].SalaryAmount;      // Salary of the whole department
                org[depNumbs[i]].SalaryFlag = true;              // Salary of current department is calculated
            }
        }

        /// <summary>
        /// Calculate salary of the administrator and the whole department
        /// </summary>
        /// <param name="currDept">Number of current department</param>
        /// <param name="subDepts">Sub departments list of current account</param>
        private void GetAdminSalary(int currDept, List<int> subDepts)
        {
            int AdminID;
            int estimatedSalary;
            int estimatedSalaryAllSubDepts = 0;

            AdminID = (int)org[currDept].AdministratorId;

            for (int i = 0; i < subDepts.Count; i++)
            {
                estimatedSalaryAllSubDepts += (int)org[subDepts[i]].SalaryAmount;
            }

            estimatedSalaryAllSubDepts += (int) org[currDept].SalaryAmount;         // Add current department salary
            estimatedSalary = estimatedSalaryAllSubDepts / 100 * 15;


            if (estimatedSalary > org[currDept].Employees[AdminID].Salary)
            {
                org[currDept].Employees[AdminID].Salary = (uint)estimatedSalary;
            }
            org[currDept].SalaryAmount = org[currDept].Employees[AdminID].Salary + org[currDept].SalaryAmount;      // Salary of the whole department
            org[currDept].SalaryFlag = true;              // Salary of department is calculated
        }

        /// <summary>
        /// Calculate salary of all sub departments and departments
        /// </summary>
        private void GetAllSalary(List<int> depNumbs)       // Depts with child sub departments
        {
            List<int> subDepts;

            do
            {
                for (int i = 0; i < depNumbs.Count; i++)
                {
                    subDepts = GetSubDepts(depNumbs[i]);            // Get sub departments list of selected department

                    for (int j = 0; j < subDepts.Count; j++)        
                    {
                        if (org[subDepts[j]].SalaryFlag)
                        {
                            GetAdminSalary(depNumbs[i], subDepts);
                        }
                    }
                }
            } while (!CheckSalaryFlag(depNumbs));
        }

        /// <summary>
        /// Is the salary in all departments calculated?
        /// </summary>
        /// <returns></returns>
        private bool CheckSalaryFlag(List<int> deptsId)
        {
            foreach (var depts in deptsId)
            {
                if (!org[depts].SalaryFlag)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get numbers of sub departments without descendants
        /// </summary>
        /// <returns></returns>
        private List<int> GetSubDeptsWODescendants()
        {
            bool inDep = false;
            List<int> DeptsWOChilds = new List<int>();

            for (int i = 0; i <= org.Count; i++)        // Department number
            {
                for (int j = 0; j < org.Count; j++)     // Parent ID number
                {
                    if (org[j].ParId == i)
                    {
                        inDep = true;
                        j = org.Count;                  // Exit condition
                    }
                }

                if (!inDep)
                {
                    DeptsWOChilds.Add(i-1);        // Add sub department without descendants to list
                }
                inDep = false;
            }
            return DeptsWOChilds;
        }

        /// <summary>
        /// Get numbers of departments with descendants (except ID0)
        /// </summary>
        /// <returns></returns>
        private List<int> GetDeptsWithDescendants(List<int> deptsWODescendants)
        {
            List<int> deptsWithChilds = new List<int>();

            for (int i = 1; i < org.Count; i++)         
            {
                deptsWithChilds.Add(i);
            }

            foreach (int dep in deptsWODescendants)
            {
                deptsWithChilds.Remove(dep);
            }
            return deptsWithChilds;
        }

        /// <summary>
        /// Get list of sub departments of selected department
        /// </summary>
        /// <param name="id">Department ID</param>
        /// <returns></returns>
        private List<int> GetSubDepts(int dep)
        {
            List<int> subDepts = new List<int>();

            for (int i = 0; i < org.Count; i++)
            {
                if (org[i].ParId-1 == dep)
                {
                    subDepts.Add((int)org[i].Id-1);
                }
            }
            return subDepts;
        }

        /// <summary>
        /// Get list of departments sorted by parent ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObservableCollection<Organisation> GetSubDepts(uint id)
        {
            subDepartment = new ObservableCollection<Organisation>();

            for (int i = 0; i < org.Count; i++)
            {
                if (org[i].ParId == id)
                {
                    subDepartment.Add(org[i]);
                }
            }
            return subDepartment;
        }

        /// <summary>
        /// Check administrator in subdepartment
        /// </summary>
        /// <param name="emp">Employees list</param>
        /// <returns></returns>
        private bool CheckAdminInOrg(ObservableCollection<Employee> emp)
        {
            for (int i = 0; i < emp.Count; i++)
            {
                if (emp[i] is Administrator)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Clear all data
        /// </summary>
        public void ClearData()
        {
            Employee.count = 0;
            Organisation.count = 0;
            org.Clear();
            subDepartment.Clear();
        }

        /// <summary>
        /// Serialization method, save data to file
        /// </summary>
        public void SaveData()
        {
            json = JsonConvert.SerializeObject(org);
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
                org = JsonConvert.DeserializeObject<ObservableCollection<Organisation>>(json, new JsonSerializerSettings() { Converters = converters });
            }

            MessageBox.Show("Data successsfully loaded", "Load data", MessageBoxButton.OK, MessageBoxImage.Information);
            return org;
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
