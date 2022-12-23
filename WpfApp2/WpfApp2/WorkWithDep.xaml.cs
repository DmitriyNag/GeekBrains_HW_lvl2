using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.Interop;
using System.Text;
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
    /// Логика взаимодействия для WorkWithDep.xaml
    /// </summary>
    public partial class WorkWithDep : Window
    {
        public enum ModeType
        {
            Add,
            Change
        }
        public ModeType Mode { get;set; }

        //public static void ConfirmDepChange(Department department = null)
        //{
            
        //    var workWithDep = new WorkWithDep();
        //    workWithDep.DepName.Text = department.Name ?? "Новый департамент";
        //    if (workWithDep.ShowDialog() == true) return workWithDep.DepName.Text;
        //    else return String.Empty;
        //}
        public WorkWithDep()
        {
            InitializeComponent();
            Mode = ModeType.Add;
        }

        private void DepCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void DepSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            try
            {
                //TODO: Сделать указание вышестоящего департамента
                if (Mode == ModeType.Add)
                    (Owner as MainWindow).CurrentS.AddDepartment(DepName.Text, -1);
                else
                {
                    (Owner as MainWindow).CurrentS.Departments.Single(x => x.ID == Convert.ToInt32(DepIDLabel.Content)).SetDepartment(DepName.Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
    }
}
