using System;
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
using System.Windows.Shapes;

namespace Kitness1
{
    /// <summary>
    /// Signup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Signup : Window
    {
        DBM dbm = new DBM();
        Boolean idcheck = false, emailcheck = false;

        public Signup()
        {
            InitializeComponent();
        }

        public String email_return()
        {
            String email = tbEmail.Text + "@";
            switch (cbEmail.SelectedIndex)
            {
                case 0:
                    email += lblEmail0.Content;
                    break;
                case 1:
                    email += lblEmail1.Content;
                    break;
                case 2:
                    email += lblEmail2.Content;
                    break;
                case 3:
                    email += lblEmail3.Content;
                    break;
                case 4:
                    email += tbEmailKind.Text;
                    break;
                default:
                    MessageBox.Show("Error", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return "";
            }
            return email;
        }

        private void btSignup_Click(object sender, RoutedEventArgs e)
        { //회원가입 버튼 클릭
            if(tbID.Text.Length == 0 || tbPW.Password.Length == 0 ||
                tbPWC.Password.Length == 0 || tbEmail.Text.Length == 0)
            {
                MessageBox.Show("모든 입력창에 입력하세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(tbPW.Password.Length < 8)
            {
                MessageBox.Show("암호는 최소 8글자입니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!idcheck)
            {
                MessageBox.Show("아이디 중복확인을 해주세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!emailcheck)
            {
                MessageBox.Show("이메일 중복확인을 해주세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!tbPW.Password.Equals(tbPWC.Password))
            {
                MessageBox.Show("입력하신 암호와 암호 확인이 서로 다릅니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            String email = email_return();
            if (email.Length == 0) return;

            String strQuery = "Insert into Member Values('";
            strQuery += tbID.Text + "', '";
            strQuery += tbPW.Password + "', '";
            strQuery += email + "')";
            //Email입력창에 입력한 email을 가져와서 id값만 가져올 것이므로 sql문을 이렇게 작성
            try
            {
                dbm.dbOpen(); //DB open
                dbm.DB_stmt.CommandText = strQuery;
                dbm.DB_stmt.ExecuteReader();
                dbm.dbClose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQLException : " + ex.Message);
            }
            MessageBox.Show("회원가입 완료\n id : "+tbID.Text+"\n pw : "+tbPW.Password+"\n email : "+email, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void tbEmailKind_Keyup(object sender, KeyEventArgs e)
        {
            int cnt = 0, here = 0;//cnt :.이 나오는 수 here : 2번 이상 나오면 해당 위치
            char[] check = tbEmailKind.Text.ToCharArray();
            foreach (char a in check)
            {
                here++;
                if (a.Equals('.')) cnt++;
                if (cnt > 1) break;
            }
            if (cnt > 1)
            {
                tbEmailKind.Text = "";
                for (int a = 0; a < here - 1; a++)
                { //here .이 나온 위치이니 그 전까지 반복해 텍스트박스에 넣어준다
                    tbEmailKind.Text += check[a].ToString();
                }
            }
            emailcheck = false;
        }

        private void tbEmail_Keyup(object sender, KeyEventArgs e)
        {
            tbEmail.Text = Regex.Replace(tbEmail.Text, @"[^a-zA-Z0-9_]", "", RegexOptions.Singleline);
            //특수문자가 오면 빈공간으로 변환시켜준다
            emailcheck = false;
        }

        private void cbEmail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            emailcheck = false;
        }

        private void tbID_Keyup(object sender, KeyEventArgs e)
        {
            tbID.Text = Regex.Replace(tbID.Text, @"[^a-zA-Z0-9_]", "", RegexOptions.Singleline);
            //특수문자가 오면 빈공간으로 변환시켜준다
            idcheck = false;
        }

        private void btEmailCheck_Click(object sender, RoutedEventArgs e)
        {// 이메일 중복 체크
            String email = email_return();
            if (email.Length == 0) return;

            if (!Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {//이메일이 글자.글자 형식이 맞는가를 검사한 후 true/false를 반환 >> false면 아래 코드 실행하고 종료
                MessageBox.Show("이메일이 유효하지 않습니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } //이메일이 유효하니 계속 진행
            String result="", strQuery = "SELECT * FROM Member Where email = '";
            strQuery += email;
            strQuery += "'";
            //ID입력창에 입력한 id을 가져와서 모든 값을 가져올 것이므로 sql문을 이렇게 작성
            try
            {
                dbm.dbOpen(); //DB open
                dbm.DB_stmt.CommandText = strQuery;
                dbm.DB_rs = dbm.DB_stmt.ExecuteReader();
                while (dbm.DB_rs.Read())
                {
                    result = dbm.DB_rs["email"].ToString();
                } //sql실행 결과 값을 돌려 pw에 해당하는 값만 가져와서 저장
                dbm.dbClose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQLException : " + ex.Message);
            }
            if (result.Length != 0)
            {
                MessageBox.Show("이메일이 존재합니다", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show(email + "는 사용가능합니다.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            emailcheck = true;
        }

        private void btIDCheck_Click(object sender, RoutedEventArgs e)
        {// 아이디 중복 체크
            String id = "", strQuery = "SELECT * FROM Member Where id = '";
            strQuery += tbID.Text;
            strQuery += "'";
            //ID입력창에 입력한 id을 가져와서 모든 값을 가져올 것이므로 sql문을 이렇게 작성
            try
            {
                dbm.dbOpen(); //DB open
                dbm.DB_stmt.CommandText = strQuery;
                dbm.DB_rs = dbm.DB_stmt.ExecuteReader();
                while (dbm.DB_rs.Read())
                {
                    id = dbm.DB_rs["id"].ToString();
                } //sql실행 결과 값을 돌려 pw에 해당하는 값만 가져와서 저장
                dbm.dbClose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQLException : " + ex.Message);
            }
            if(id.Length != 0)
            {
                MessageBox.Show("아이디가 존재합니다", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show(tbID.Text + "는 사용가능합니다.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            idcheck = true;
        }
    }
}
