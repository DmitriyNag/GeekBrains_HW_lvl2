using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для EmployeeChangeWindow.xaml
    /// </summary>
    public partial class EmployeeChangeWindow : Window
    {
        static private EmployeeChangeWindow employeeChangeWindow;
        Regex regex;
        ObservableCollection<Department>? departments;
        public enum ModeType
        {
            Add,
            Change
        }
        public static ModeType Mode { get; set; }
        static Employee? E;

        public EmployeeChangeWindow()
        {
            InitializeComponent();
            regex = new Regex("[^0-9]");
        }

        internal static void WorkWithEmployee(ModeType mode, Employee? e, MainWindow owner)
        {
            employeeChangeWindow = new EmployeeChangeWindow
            {
                Owner = owner
            };
            Mode = mode;
            E = e;
            employeeChangeWindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("вот так");
            departments = new((Owner as MainWindow)?.CurrentS.Departments);
            DepartmensList.ItemsSource = departments;
            if (Mode == ModeType.Change)
            {
                if (E == null) throw new Exception("didn't get Employee for changing data");
                Title = "Изменить сотрудника";
                TextBoxName.Text = E.FirstName;
                TextBoxSurName.Text = E.SecondName;
                TextBoxAge.Text = E.Age.ToString();
                TextBoxJobTitle.Text = E.JobTitle;
                CheckBoxIsHead.IsChecked = E.IsHead;
                DepartmensList.SelectedItem  = departments.Single(x => x.ID == E.ParentDepartmentId);

            }
            else
            {
                Title = "Добавить сотрудника";
                TextBoxName.Text = "Имя";
                TextBoxSurName.Text = "Фамилия";
                TextBoxAge.Clear();
                TextBoxJobTitle.Text = "Должность";
                CheckBoxIsHead.IsChecked = false;
            }
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmensList.SelectedItem != null)
            {
                DialogResult = true;
                this.Close();
                if (Mode == ModeType.Add) (Owner as MainWindow)?.CurrentS.AddEmployee(TextBoxName.Text, TextBoxSurName.Text, Convert.ToInt32(TextBoxAge.Text), TextBoxJobTitle.Text, (DepartmensList.SelectedItem as Department).ID, CheckBoxIsHead.IsChecked ?? false);
                else E.ChangeData(TextBoxName.Text, TextBoxSurName.Text, Convert.ToInt32(TextBoxAge.Text), TextBoxJobTitle.Text, (DepartmensList.SelectedItem as Department).ID, CheckBoxIsHead.IsChecked ?? false);
            }
            else MessageBox.Show("Не выбран департамент сотрудника");

        }
        private void NumberValidationTextBox (object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
