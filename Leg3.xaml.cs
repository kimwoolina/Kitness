using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kitness1
{
    /// <summary>
    /// leg3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Leg3 : Window
    {
        public Leg3()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, RoutedEventArgs e)
        {
            Timer t = new Timer(5000); //5초 쉬고
            Leg3_Run leg3_Run = new Leg3_Run(); //창 전환
            Close();
            leg3_Run.ShowDialog();t.Start();
        }

        private void btPrevious_Click(object sender, RoutedEventArgs e)
        {
            Leg_Selection leg_Selection = new Leg_Selection();
            Close();
            leg_Selection.ShowDialog();
        }
    }
}
