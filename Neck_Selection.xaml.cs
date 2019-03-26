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

namespace Kitness1
{
    /// <summary>
    /// Neck_Selection.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Neck_Selection : Window
    {
        public Neck_Selection()
        {
            InitializeComponent();
        }

        private void neck1_Click(object sender, RoutedEventArgs e)
        {
            Neck1 neck1 = new Neck1(); //창 전환
            Close();
            neck1.ShowDialog();
        }

        private void neck2_Click(object sender, RoutedEventArgs e)
        {
            Neck2 neck2 = new Neck2(); //창 전환
            Close();
            neck2.ShowDialog();
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            Partselection partselection = new Partselection(); //창전환
            Close();
            partselection.ShowDialog();
        }
    }
}
