using System;

namespace ConsoleApp1
{
    public abstract class Worker: IComparable
    {
        protected static int Count;
        protected int number;
        protected string Name { get; set; }
        protected string Surname { get; set; }
        protected int Age { get; set; }
        protected string Department { get; set; }
        protected double salary;
        /// <summary>
        /// Конструктор, создаем запись о сотруднике
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Отдел</param>
        public Worker(string name, string surname, int age, string department)
        {
            number = Count++;
            Name = name;
            Surname = surname;
            Age = age;
            Department = department;
 
        }
        /// <summary>
        /// Функция сравнения сотрудников по зарплате, для интерфейса IComparable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Worker w = obj as Worker;
            if (this.salary > w.salary) return 1;
            else if (this.salary < w.salary) return -1;
            else return 0;
        }

        /// <summary>
        /// Абстрактный метод расчета месячной зарплаты
        /// </summary>
        public abstract void CountSalary();

    }
}
