namespace ConsoleApp1
{
    public class WorkerFixedSalary : Worker
    {
        protected double MounthlyRate { get; set; }
        /// <summary>
        /// Конструктор, создаем сотдруника с помесячной оплатой
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Отдел</param>
        /// <param name="mounthlyRate">Оплата в месяц</param>
        public WorkerFixedSalary(string name, string surname, int age, string department, double mounthlyRate) : base(name, surname, age,
            department)
        {
            MounthlyRate = mounthlyRate;
        }
        /// <summary>
        /// Расчет среднемесячной зарплаты для сотрудника с помесячной оплатой
        /// </summary>
        public override void CountSalary()
        {
            salary = MounthlyRate;
        }

        public override string ToString()
        {
            return $"Number - {number}, name -{Name}, surname - {Surname}, age - {Age}, department - {Department}, mounthlyRate - {MounthlyRate}, Salary - {salary}";
        }
    }
}
