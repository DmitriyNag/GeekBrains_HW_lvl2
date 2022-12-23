using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Employee
    {
        //private static int IdCounter;
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public int Age { get; private set; }
        public string JobTitle { get; private set; }
        public string FullName { get { return FirstName + " " + SecondName; } }
        public int ParentDepartmentId { get; set; }
        //TODO: отображать наименование департамента в главном списке
        //private Structure ref ParentStructure { get;}
        //public string ParentDepartmentName { get
        //    {

        //    } 
        //}
        public bool IsHead { get; set; }
        public int ID { get; }

        #pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public Employee (string firstName, string secondName, int age, string jobTitle, int parentDepartmentId, bool isHead, int Id)
        #pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
            SetData(firstName, secondName, age, jobTitle, parentDepartmentId, isHead);
            ID = Id;
        }
        public void ChangeData(string firstName, string secondName, int age, string jobTitle, int parentDepartmentId, bool isHead)
        {
            SetData(firstName, secondName, age, jobTitle, parentDepartmentId, isHead);
        }
        internal void SetData(string firstName, string secondName, int age, string jobTitle, int parentDepartmentId, bool isHead)
        {
            FirstName = (firstName != null && firstName != String.Empty) ? firstName : throw new Exception("can't create new employee, Firstname is empty");
            SecondName = (secondName != null && secondName != String.Empty) ? secondName : throw new Exception("can't create new employee, Secondname is empty");
            Age = (age > 0) ? age : 0;
            JobTitle = jobTitle ?? String.Empty;
            ParentDepartmentId = parentDepartmentId;
            IsHead = isHead;
        }
        public void ChangeDepartment(int d)
        {
            ParentDepartmentId = d;
        }
        public override string ToString() => this.FullName;

    }
}
