using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Structure CurrentS;
        private WorkWithDep workWithDep;
        //private EmployeeChangeWindow employeeChangeWindow;

        //TODO: создать свои исключения
        //TODO: реализовать изменние руководителя департамента
        //TODO: отображение карты департаментов и зависимостей

        //TODO: (mid) сериализация при любом изменении
        //TODO: меню выбора и загрузки орг структуры
        //TODO: (mid) выбор вышестоящего департамента в окне создания\изменения
        //TODO: (mid) выбор нижестоящих департаментов в окне создания\изменения

        //TODO: (mid) десериализация в файл
        //TODO: (mid) для хранения сотрудников без департамента создать департамент "без департамента", который нельзя удалить.
        public MainWindow()
        {
            InitializeComponent();
            
                        
            CurrentS = new("Моя компания");
            CurrentS.AddDepartment("Главный самый");
            CurrentS.AddDepartment("Менеджеры", 0);
            CurrentS.AddDepartment("Программисты", 0);
            CurrentS.AddDepartment("деп.4", 1);
            CurrentS.AddEmployee("Иван", "Пучков", 30, "Менеджер", 1, false);
            CurrentS.AddEmployee("Степан", "Мокров", 28, "Начаник менеджеров", 1, true);
            CurrentS.AddEmployee("Руслан", "Прохоров", 35, "Руководитель", 0, true);
            CurrentS.AddEmployee("Борислав", "Иванов", 24, "Программист", 2, false);
            CurrentS.AddEmployee("Николай", "Суворов", 26, "Программист", 2, false);
            CurrentS.AddEmployee("Прохор", "Шаляпин", 31, "Старший программист", 2, true);
            CurrentS.AddEmployee("Виталий", "Шаляпин", 34, "Дизайнер", 3, true);

            LabelHeader.Content = CurrentS.Name;
            ListViewChildDeps.ItemsSource = CurrentS.Departments;
            RenewEmployeeListByDepartment();

            //try
            //{
            //    CurrentS.SerializeStructure("db.json");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //Structure? CurrentS2 = Structure.DeserializeStructure("db.json");
            //if(CurrentS2 != null) { CurrentS2.SerializeStructure("db2.json"); }

        }

        
        private void ListViewChildDeps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RenewEmployeeListByDepartment();
        }

        private void DepAdd_Click(object sender, RoutedEventArgs e)
        {
            workWithDep = new WorkWithDep
            {
                Owner = this,
                Title = "Добавить департамент"
            };
            workWithDep.ShowDialog();
        }

        private void DepDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewChildDeps.SelectedItem == null) MessageBox.Show("Выберите Департамент");
            else 
            {
                var t = MessageBox.Show("Вы уверены что хотите удалить департамент?", "Удаление департамента", MessageBoxButton.OKCancel);
                if (t == MessageBoxResult.OK)
                {
                    var query = CurrentS.Employees?.Where(x => x.ParentDepartmentId == (ListViewChildDeps.SelectedItem as Department)?.ID).ToList();
                    for (int i = 0; i < query.Count; i++)
                    {
                        query[i].ChangeDepartment(-1);
                    }
                    CurrentS.Departments?.Remove(ListViewChildDeps.SelectedItem as Department);
                }
            }
        }

        private void DepChange_Click(object sender, RoutedEventArgs e)
        {
            var item = ListViewChildDeps.SelectedItem;
            if (item == null) MessageBox.Show("Выберите Департамент");
            else
            {
                workWithDep = new WorkWithDep
                {
                    Owner = this,
                    Title = "Изменить департамент"
                };
                workWithDep.DepName.Text = (item as Department)?.Name;
                workWithDep.DepIDLabel.Content = (item as Department)?.ID;
                workWithDep.Mode = WorkWithDep.ModeType.Change;
                workWithDep.ShowDialog();
                //TODO:костыль, так как при изменении свойства не приходит уведомление. Создать свой ObserverCollection с поправленным уведомлением.
                ListViewChildDeps.ItemsSource = null;
                ListViewChildDeps.ItemsSource = CurrentS.Departments;
            }
        }

        private void EmplDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewEmployees.SelectedItem == null) MessageBox.Show("Выберите сотрудника");
            else
            {
                var t = MessageBox.Show("Вы уверены что хотите удалить сотрудника?", "Удаление сотрудника", MessageBoxButton.OKCancel);
                if (t == MessageBoxResult.OK)
                {
                    CurrentS.Employees?.Remove(ListViewEmployees.SelectedItem as Employee);
                    RenewEmployeeListByDepartment();
                }
            }
        }

        private void EmplChange_Click(object sender, RoutedEventArgs e)
        {
            var item = ListViewEmployees.SelectedItem as Employee;
            if (item == null) MessageBox.Show("Выберите сотрудника");
            else
            {
                EmployeeChangeWindow.WorkWithEmployee(EmployeeChangeWindow.ModeType.Change, item, this);
                //employeeChangeWindow = new EmployeeChangeWindow
                //{
                //    Owner = this,
                //    Title = "Изменить сотрудника"
                //};
                //employeeChangeWindow.DepName.Text = (item as Department)?.Name;
                //workWithDep.DepIDLabel.Content = (item as Department)?.ID;
                //workWithDep.Mode = WorkWithDep.ModeType.Change;
                //workWithDep.ShowDialog();

                //TODO:костыль, так как при изменении свойства не приходит уведомление. Создать свой ObserverCollection с поправленным уведомлением.
                RenewEmployeeListByDepartment();
            }
        }

        private void EmplAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeChangeWindow.WorkWithEmployee(EmployeeChangeWindow.ModeType.Add, null, this);
            //TODO не могу вызывать EmployeeChangeWindow.ShowDialog() - нужен рефактор, видимо какие то переменнные не определены.
        }
        private void RenewEmployeeListByDepartment() => ListViewEmployees.ItemsSource = CurrentS.EmployeeListInDep((ListViewChildDeps.SelectedItem as Department));

    }
}
