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
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Partselection : Window
    {
        Start start;
        public string ID
        {
            get; set;
        }

        public Partselection()
        {
            
            InitializeComponent();
        }

        public Partselection(Start st) //오버라이딩
        {
            this.start = st;
            InitializeComponent();
        }

        private void btPrevious_Click(object sender, RoutedEventArgs e)
        {
            ID = this.start.ID;
            Start start = new Start(this); //창 전환
            Close();
            start.ShowDialog();
        }

        private void btnNeck_Click(object sender, RoutedEventArgs e)
        {
            Neck_Selection neck_Selection = new Neck_Selection(); //창 전환
            Close();
            neck_Selection.ShowDialog();
        }

        private void btnWaist_Click(object sender, RoutedEventArgs e)
        {
            Spine_Selection spine_Selection = new Spine_Selection(); //창 전환
            Close();
            spine_Selection.ShowDialog();
        }

        private void btnLeg_Click(object sender, RoutedEventArgs e)
        {
            Leg_Selection leg_Selection = new Leg_Selection(); //창 전환
            Close();
            leg_Selection.ShowDialog();
        }
    }
}
