namespace ConsoleApp1
{
    public class WorkerByTimeSalary : Worker
    {
        protected double HourRate { get; set; }
        /// <summary>
        /// Конструктор, создаем сотрудника с почасовой оплатой
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Отдел</param>
        /// <param name="hourRate">Оплата в час</param>
        public WorkerByTimeSalary(string name, string surname, int age, string department, double hourRate) : base(name, surname, age,
            department)
        {
            HourRate = hourRate;
        }
        /// <summary>
        /// Расчет помесячной зарплаты для сотрудника с почасовой оплатой
        /// </summary>
        public override void CountSalary()
        {
            salary = 20.8 * 8 * HourRate;
        }
        public override string ToString()
        {
            return $"Number - {number}, name -{Name}, surname - {Surname}, age - {Age}, department - {Department}, hourRate - {HourRate}, Salary - {salary}";
        }
    }
}
