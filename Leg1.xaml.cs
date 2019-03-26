using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Timers;

namespace Kitness1
{
    /// <summary>
    /// leg1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Leg1 : Window
    {
        public Leg1()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, RoutedEventArgs e)
        {

            Timer t = new Timer(5000); //5초 쉬고
            Leg1_Run leg1_Run = new Leg1_Run(); //창 전환
            Close();
            leg1_Run.ShowDialog();t.Start();
        }

        private void btPrevious_Click(object sender, RoutedEventArgs e)
        {
            Leg_Selection leg_Selection = new Leg_Selection();
            Close();
            leg_Selection.ShowDialog();
        }
    }
}
