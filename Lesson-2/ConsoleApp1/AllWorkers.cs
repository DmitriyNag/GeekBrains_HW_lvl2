using System;

using System.Collections;

//1. Построить три класса(базовый и 2 потомка), описывающих некоторых работников с почасовой оплатой(один из потомков) и фиксированной оплатой(второй потомок).
//а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной платы.Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка», для работников с фиксированной оплатой «среднемесячная заработная плата = фиксированная месячная оплата».
//б) Создать на базе абстрактного класса массив сотрудников и заполнить его.
//в) * Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort().
//г) * Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
namespace ConsoleApp1
{
    class AllWorkers : IEnumerable
    {

        public Worker[] workers;
        static Random rnd = new Random();

        public IEnumerator GetEnumerator()
        {
            foreach (Worker worker in workers)
                yield return worker;
        }

        /// <summary>
        /// Конструктор, создаем массив работниов
        /// </summary>
        /// <param name="numOfWorkers">Количество работников в компании</param>
        public AllWorkers(int numOfWorkers)
        {
            if (numOfWorkers > 0)
                workers = new Worker[numOfWorkers];
            else throw new WorkersException($"Передано не верное количество работников: {numOfWorkers}");
        }
        /// <summary>
        /// Наполняем массив произвольными данными о сотрудниках с помесячной и почасовой оплатой
        /// </summary>
        public void GenerateCustomWorkers()
        {
            for (int i = 0; i < workers.Length; i++)
            {
                if (rnd.Next(1, 3) == 1)
                {
                    workers[i] = new WorkerFixedSalary($"name_{i}", $"surname_{i}", i * 2 + 20, "ИТ отдел", 20000 + 1000 * i);
                }
                else
                {
                    workers[i] = new WorkerByTimeSalary($"name_{i}", $"surname_{i}", i * 2 + 20, "Отдел продаж", 300 + 20 * i);
                }

            }
        }

        /// <summary>
        /// Сортировка массива работников по среднеесячной зарлпате
        /// </summary>
        public void Sort()
        {
            Array.Sort(workers);
        }

    }
}
