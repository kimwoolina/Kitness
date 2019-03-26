using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Kitness1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DBM dbm = new DBM();

        public string ID
        {
            get; set;
        }

        public MainWindow()
        {
            InitializeComponent();
        }     

        private void btHelp(object sender, RoutedEventArgs e) //도움말 버튼
        {
            help start = new help(); //창 전환
            start.ShowDialog();
        }

        private void tbID_KeyUp(object sender, KeyEventArgs e)
        {
            //한글 입력은 디자인 쪽에서 InputMethod.IsInputMethodEnabled="False" 이것으로 막음
            if (tbID.Text.Length > 15) tbID.Text = tbID.Text.Remove(15, 1);
            //아이디는 최대 15글자라서 15자 이상오면 자동으로 삭제시킴
            tbID.Text = Regex.Replace(tbID.Text, @"[^a-zA-Z0-9가-힣_]", "", RegexOptions.Singleline);
            //특수문자가 오면 빈공간으로 변환시켜준다
        }

        private void tbPW_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbID.Text.Length > 15) tbID.Text = tbID.Text.Remove(15, 1);
            //비밀번호 역시 최대 15글자라서 15자 이상오면 자동 삭제
        }

        private void btStart(object sender, RoutedEventArgs e)// 시작버튼
        {/*
            if (tbID.Text == "")
            {
                MessageBox.Show("아이디를 입력하세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (tbPW.Password == "")
            {
                MessageBox.Show("암호를 입력하세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            String pw = "", strQuery = "SELECT pw FROM Member Where id = '";
            strQuery += tbID.Text;
            strQuery += "'";
            //id입력창에 입력한 id을 가져와서 pw값만 가져올 것이므로 sql문을 이렇게 작성
            try
            {
                dbm.dbOpen(); //DB open
                dbm.DB_stmt.CommandText = strQuery;
                dbm.DB_rs = dbm.DB_stmt.ExecuteReader();
                while (dbm.DB_rs.Read())
                {
                    pw = dbm.DB_rs["pw"].ToString();
                } //sql실행 결과 값을 돌려 pw에 해당하는 값만 가져와서 저장

                dbm.dbClose();
            } catch (Exception ex)
            {
                Console.WriteLine("SQLException : " + ex.Message);
            }

            if (pw.Equals(tbPW.Password))
            {
                //sql실행 결과로 가져온 pw값의 길이가 0이라면 null이므로 없는 아이디를 입력한 것이고
                //pw가 존재한다해도 해당 아이디의 비번 값과 일치하지 않는다면 로그인을 할 수 없다.
                //따라서 이 조건문에서는 위의 조건을 배제한 경우기때문에 로그인을 성공한 것이다
                Console.WriteLine("ID : " + tbID.Text + "PW : " + pw + "로그인 성공\n");
                ID = tbID.Text;
                Start start = new Start(this); //창 전환
                Close();
                start.ShowDialog();
                //값넘기기 참고 : http://www.wolfpack.pe.kr/290
            }
            else
            { //이 경우는 조건에 걸리기 때문에 로그인에 실패한 것이다
                Console.WriteLine("ID : " + tbID.Text + "PW : " + pw + "로그인 실패\n");
                MessageBox.Show("로그인 실패\n아이디 혹은 암호를 확인해주세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }*/
            Start start = new Start(this); //창 전환
            Close();
            start.ShowDialog();
        }

        private void btFind_Click(object sender, RoutedEventArgs e)
        {
            Find find = new Find(); //창 전환
            find.ShowDialog();
        }

        private void btSign_Cilck(object sender, RoutedEventArgs e)
        {
            Signup signup = new Signup(); //창 전환
            signup.ShowDialog();
        }
    }
}
