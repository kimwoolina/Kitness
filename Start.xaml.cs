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
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Start : Window
    {
        MainWindow main;
        Partselection part;
        public string ID
        {
            get; set;
        }
        public Start(MainWindow mainfrm)
        {
            this.main = mainfrm;
            InitializeComponent();
            calluser();
        }
        public Start(Partselection partfrm)
        {
            this.part = partfrm;
            InitializeComponent();
            calluser();
        }

        public void calluser()
        {
            if (main != null)
            {
                lblID.Content = main.ID + "님";
                ID = main.ID;
            }
            else
            {
                lblID.Content = part.ID + "님";
                ID = part.ID;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//시작하기 버튼
            ID = main.ID;
            Partselection partselection = new Partselection(this); //창 전환
            Close();
            partselection.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { //정보 수정 버튼 Useredit
            ID = main.ID;
            Useredit edit = new Useredit(this); //창 전환
            edit.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        { //정확도 버튼 Useraccuracy
            ID = main.ID;
            Useraccuracy Useraccuracy = new Useraccuracy(this); //창 전환
            Useraccuracy.ShowDialog();
        }
    }
}
