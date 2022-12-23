using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Windows;
using System.Collections;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.ObjectModel;

namespace WpfApp2
{
    public class Structure
    {
        public string Name { get; set; }   
        public ObservableCollection<Department>? Departments { get; internal set; }
        public ObservableCollection<Employee>? Employees { get; internal set; }
        public static int DepartmentCount { get; private set; }
        public static int EmployeeCount { get; private set; }

        private readonly JsonSerializerOptions options;
        public Structure(string name) 
        {
           Name = name;
           Departments = new ObservableCollection<Department>();
           Employees = new ObservableCollection<Employee>();
           EmployeeCount = DepartmentCount = 0;
           options = new JsonSerializerOptions
           {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
           };
           

        }

        public void AddDepartment(string name, int parentDepartmentId = -1)
        {
            if((parentDepartmentId >= -1) && (name != String.Empty)) 
            {
                //TODO: Упростить
                if (!this.IfDepExists(name))
                {
                    Department d = new (name, parentDepartmentId, DepartmentCount++);
                    Departments?.Add(d);
                }

                else throw new Exception($"Department with name = {name} already exists");
            } 
        }
        public bool DeleteDepartment (int id) => Departments?.Remove(Departments.Single(x => x.ID == id)) ?? false;

        public void AddEmployee(string firstName, string secondName, int age, string jobTitle, int parentDepartmentId, bool isHead) 
        {
            if (parentDepartmentId >= -1)
                Employees?.Add(new Employee(firstName, secondName, age, jobTitle, parentDepartmentId, isHead, EmployeeCount++));
            else throw new Exception($"Unable to create employee in department id = {parentDepartmentId}");
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                return Employees?.Remove(Employees.Single(x => x.ID == id)) ?? false;
            }
            catch (InvalidOperationException)
            {

                throw new Exception("Employee wasn't find or it's more than 1 employee with this ID");
            }
        }

        public void SerializeStructure(string filename)
        {
            if (filename != null)
            {
                try
                {
                    MessageBox.Show(JsonSerializer.Serialize(this, options));
                    //File.WriteAllText(filename, JsonSerializer.Serialize(this, options));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }                
            }
        }
        public static Structure? DeserializeStructure (string filename)
        {
            if(File.Exists(filename))
            {
                try
                {
                    string s = File.ReadAllText(filename);
                    return JsonSerializer.Deserialize<Structure>(s, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                catch (Exception)
                {
                    throw new Exception($"Can't read json file as a Structure from {filename}");
                }
            }
            throw new Exception($"Can't find file at: {filename}");
        }
        public bool IfDepExists(string name) => (name != string.Empty) && (Departments?.Where(x => x.Name == name).ToList().Count > 0);

        public ObservableCollection<Employee> EmployeeListInDep(Department? department) => (department != null) ? new(Employees?.Where(x => x.ParentDepartmentId == department.ID).ToList()) : new(Employees?.ToList());
    }
}
