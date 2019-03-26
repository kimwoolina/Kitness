﻿using System;
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
    /// Spine1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Spine1 : Window
    {
        public Spine1()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, RoutedEventArgs e)
        {
            Timer t = new Timer(5000); //5초 쉬고
            Spine1_Run spine1_Run = new Spine1_Run(); //창 전환
            Close();
            spine1_Run.ShowDialog();t.Start();
        }

        private void btPrevious_Click(object sender, RoutedEventArgs e)
        {
            Spine_Selection spine_Selection = new Spine_Selection();
            Close();
            spine_Selection.ShowDialog();
        }
    }
}
