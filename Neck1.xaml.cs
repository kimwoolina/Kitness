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
    /// Neck1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Neck1 : Window
    {
        public Neck1()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, RoutedEventArgs e)
        {
            Timer t = new Timer(5000); //5초 쉬고
            Neck1_Run neck1_Run = new Neck1_Run(); //창 전환
            Close();
            neck1_Run.ShowDialog();t.Start();
        }

        private void btPrevious_Click(object sender, RoutedEventArgs e)
        {
            Neck_Selection neck_Selection = new Neck_Selection();
            Close();
            neck_Selection.ShowDialog();
        }
    }
}
