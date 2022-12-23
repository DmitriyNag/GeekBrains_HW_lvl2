using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApp2
{
    public class Department
    {
        //private static int IdCounter;
        public string Name { get; private set; }
        public int ID { get; }
        public int ParentDepartmentId { get; private set; }
        //private Structure ParentStructure { get; }
        //public List<Department>? Departments { get; set; }

        //public List<Employee> Employees { get; set; }

        //public Employee? Head
        //{
        //    get
        //    {
        //        return Employees.Where(x => x.IsHead && x.).FirstOrDefault();
        //    }
        //}

        public Department(string Name, int parentDepartmentId, int id)
        {
            ID = id;
            this.Name = (Name != String.Empty) ? Name : $"DefaultName_{ID}";
            ParentDepartmentId = parentDepartmentId;
        }

        public void SetDepartment (string name, int parentDepartmentId =-1)
        {
            ParentDepartmentId = parentDepartmentId;
            Name = name;
        }
        //public void SetParentD(Department d) 
        //{
        //    var ChildsD = ParentStructure.Departments.Where(x => x.ParentD == this).ToList();
        //    if (d!=null)
        //    {
                
        //        if(ChildsD.Contains(d)) throw new Exception($"Can't set parent department {d.Name}, it is alredy child department");
        //        if(!d.ChildsD.Contains(this)) d.ChildsD.Add(this);
        //        ParentD = d;
        //    }
        //}
        //public void ClearParentD()
        //{
        //    if(ParentD != null)
        //    {
        //        ParentD.ChildsD.Remove(this);
        //        ParentD = null;
        //    }
        //}
        //public void AddDepartment(Department d) 
        //{
        //    if(d!=null)
        //    {
        //        //if (d == ParentD) throw new Exception("can't add child department, it's already set as a parent"); 
        //        if(!Departments.Contains(d)) Departments.Add(d);
        //        //d.ParentD = this;
        //    }
        //}
        //public void RemoveChildD(Department d)
        //{
        //    if (d != null)
        //    {
        //        if (Departments.Contains(d))
        //        {
        //            //d.ParentD = null;
        //            Departments.Remove(d);
        //        }
        //    }
        //}
        //public void AddEmployee(string firstName, string secondName, int age, string jobTitle, bool isHead)
        //{
        //    Employees.Add(new Employee(firstName, secondName, age, jobTitle, this, isHead));
        //}
        public override string ToString() => Name;

    }
}
