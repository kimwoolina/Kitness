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
    /// Useredit.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Useredit : Window
    {
        public String ID
        {
            get; set;
        } 
        public Boolean Kind
        {
            get; set;
        } //해당 회원이 선택한 종료 (회원탈퇴 - true/암호변경 - false)
        Start start;
        public Useredit(Start st)
        {
            this.start = st;
            InitializeComponent();
            Set();
        }

        DBM dbm = new DBM();

        public void Set()
        {
            String email = "", strQuery = "SELECT email FROM Member Where id = '";
            strQuery += start.ID;
            strQuery += "'";
            //id입력창에 입력한 id을 가져와서 pw값만 가져올 것이므로 sql문을 이렇게 작성
            try
            {
                dbm.dbOpen(); //DB open
                dbm.DB_stmt.CommandText = strQuery;
                dbm.DB_rs = dbm.DB_stmt.ExecuteReader();
                while (dbm.DB_rs.Read())
                {
                    email = dbm.DB_rs["email"].ToString();
                } //sql실행 결과 값을 돌려 pw에 해당하는 값만 가져와서 저장

                dbm.dbClose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQLException : " + ex.Message);
            }
            lblUserID.Content = start.ID + "님의 정보";
            lblUserEmail.Content = email;
        }

        private void btGoodbye_Click(object sender, RoutedEventArgs e)
        { //회원탈퇴 버튼
            if(tbPW.Password.Length == 0)
            {
                MessageBox.Show("PW를 채우세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Kind = true; ID = start.ID;
            ChangeCheck check = new ChangeCheck(this);
            check.ShowDialog();
        }

        private void btCPW_Click(object sender, RoutedEventArgs e)
        { //암호변경 버튼
            if (tbCPW.Password.Length == 0 || tbCPWC.Password.Length == 0)
            {
                MessageBox.Show("CPW, CPWC를 채우세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!tbCPW.Password.Equals(tbCPWC.Password))
            {
                MessageBox.Show("CPW, CPWC가 서로 암호가 다릅니다. \n 같게 입력해주세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Kind = false; ID = start.ID;
            ChangeCheck check = new ChangeCheck(this);
            check.ShowDialog();
        }
    }
}
