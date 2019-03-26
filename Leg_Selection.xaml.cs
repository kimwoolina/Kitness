using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// leg_selection.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Leg_Selection : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
       // System.Windows.Threading.DispatcherTimer dispatcherTimer3 = new System.Windows.Threading.DispatcherTimer();

        public ManualResetEvent areStep1 = new ManualResetEvent(false);
        public ManualResetEvent areStep2 = new ManualResetEvent(false);
        //public ManualResetEvent areStep3 = new ManualResetEvent(false);

        public Leg_Selection()
        {
            InitializeComponent();
            dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer1.Tick += new EventHandler(dispatcherTimer_Tick1);
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 5);  // 5초 타이머

            dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer2.Tick += new EventHandler(dispatcherTimer_Tick2);
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 5);  // 5초 타이머
            /*
            dispatcherTimer3 = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer3.Tick += new EventHandler(dispatcherTimer_Tick3);
            dispatcherTimer3.Interval = new TimeSpan(0, 0, 5);  // 5초 타이머
            */
        }
        /*
        private void dispatcherTimer_Tick3(object sender, EventArgs e)
        {
            areStep3.Set();
            notice2.Visibility = Visibility.Hidden;
        }
        */
        private void dispatcherTimer_Tick2(object sender, EventArgs e)
        {
            areStep2.Set();
            notice2.Visibility = Visibility.Hidden;
        }

        private void dispatcherTimer_Tick1(object sender, EventArgs e)
        {
            areStep1.Set();
            notice2.Visibility = Visibility.Hidden;
            

        }

        private void leg1_Click(object sender, RoutedEventArgs e)
        {
            //Leg1 leg1 = new Leg1(); //창 전환
            //Close();
            //leg1.ShowDialog();

            Thread thread1 = new Thread(new ThreadStart(step1));
            Thread thread2 = new Thread(new ThreadStart(step2));
            //Thread thread3 = new Thread(new ThreadStart(step3));
            Console.WriteLine("Thread 1, 2, 3 are started");
            thread1.Start();
            thread2.Start();
            //thread3.Start(); 
            areStep1.Set();
        }
        
        public void step1()
        {
            areStep1.WaitOne();
            areStep1.Reset();
            notice2.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                 new Action( delegate ()
                 {
                     notice2.Content = "된다";
                     notice2.Visibility = Visibility.Visible;
                     dispatcherTimer1.Start();
                 }));
            //areStep2.Set();
        }

        public void step2()
        {
            areStep1.WaitOne();
            areStep1.Reset();
            notice2.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                new Action(delegate ()
                {
                    notice2.Content = "된다2";
                    notice2.Visibility = Visibility.Visible;
                    dispatcherTimer2.Start();
                }));
            //areStep2.Set();
        }
        /*
        public void step3()
        {
            areStep1.WaitOne();
            areStep1.Reset();
            notice2.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                 new Action(delegate ()
                 {
                     notice2.Content = "된다3";
                     notice2.Visibility = Visibility.Visible;
                     dispatcherTimer3.Start();
                 }));
            //areStep2.Set();
        }
        */
        private void leg2_Click(object sender, RoutedEventArgs e)
        {
            Leg2 leg2 = new Leg2(); //창 전환
            Close();
            leg2.ShowDialog();
        }

        private void leg3_Click(object sender, RoutedEventArgs e)
        {
            Leg3 leg3 = new Leg3(); //창 전환
            Close();
            leg3.ShowDialog();
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            Partselection partselection = new Partselection(); //창전환
            Close();
            partselection.ShowDialog();
        }
    }
}
