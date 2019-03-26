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
    /// Spine.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Spine_Selection : Window
    {
        public Spine_Selection()
        {
            InitializeComponent();
        }

        private void spine1_Click(object sender, RoutedEventArgs e)
        {
            Spine1 spine1 = new Spine1(); //창 전환
            Close();
            spine1.ShowDialog();
        }

        private void spine2_Click(object sender, RoutedEventArgs e)
        {
            Spine2 spine2 = new Spine2(); //창 전환
            Close();
            spine2.ShowDialog();
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            Partselection partselection = new Partselection(); //창전환
            Close();
            partselection.ShowDialog();
        }
    }
}
