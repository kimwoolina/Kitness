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

//워터마크 출처 : http://egnore.egloos.com/6133692

namespace Kitness1
{
    /// <summary>
    /// Find.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Find : Window
    {
        DBM dbm = new DBM();

        public Find()
        {
            InitializeComponent();
        }

        private void tbidEmail_keyup(object sender, KeyEventArgs e)
        {
            tbidEmail.Text = Regex.Replace(tbidEmail.Text, @"[^a-zA-Z0-9_]", "", RegexOptions.Singleline);
            //특수문자가 오면 빈공간으로 변환시켜준다
        }

        private void btidFind_Click(object sender, RoutedEventArgs e)
        {
            if(tbidEmail.Text.Length == 0)
            {
                MessageBox.Show("이메일을 입력하세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            String email = tbidEmail.Text + "@";
            switch (cbidEmail.SelectedIndex)
            {
                case 0:
                    email += lblidEmail0.Content;
                    break;
                case 1:
                    email += lblidEmail1.Content;
                    break;
                case 2:
                    email += lblidEmail2.Content;
                    break;
                case 3:
                    email += lblidEmail3.Content;
                    break;
                case 4:
                    email += tbidEmailKind.Text;
                    if (!Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                    {//이메일이 글자.글자 형식이 맞는가를 검사한 후 true/false를 반환 >> false면 아래 코드 실행하고 종료
                        MessageBox.Show("이메일이 유효하지 않습니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    } //이메일이 유효하니 계속 진행
                    break;
                default:
                    MessageBox.Show("Error", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
            }
            String id = "", strQuery = "SELECT id FROM Member Where email = '";
            strQuery += email;
            strQuery += "'";
            //Email입력창에 입력한 email을 가져와서 id값만 가져올 것이므로 sql문을 이렇게 작성
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

            if(id.Length == 0)
            {
                MessageBox.Show("해당 회원이 없습니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("해당 회원의 아이디는 '"+id+"'입니다.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void tbpwID_Keyup(object sender, KeyEventArgs e)
        {
            tbpwID.Text = Regex.Replace(tbpwID.Text, @"[^a-zA-Z0-9_]", "", RegexOptions.Singleline);
            //특수문자가 오면 빈공간으로 변환시켜준다
        }

        private void tbpwEmail_Keyup(object sender, KeyEventArgs e)
        {
            tbpwEmail.Text = Regex.Replace(tbpwEmail.Text, @"[^a-zA-Z0-9_]", "", RegexOptions.Singleline);
            //특수문자가 오면 빈공간으로 변환시켜준다
        }

        private void btpwFind_Click(object sender, RoutedEventArgs e)
        {
            if (tbpwEmail.Text.Length == 0 || tbpwID.Text.Length == 0)
            {
                MessageBox.Show("모든 입력창에 입력하세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            String email = tbpwEmail.Text + "@";
            switch (cbidEmail.SelectedIndex)
            {
                case 0:
                    email += lblpwEmail0.Content;
                    break;
                case 1:
                    email += lblpwEmail1.Content;
                    break;
                case 2:
                    email += lblpwEmail2.Content;
                    break;
                case 3:
                    email += lblpwEmail3.Content;
                    break;
                case 4:
                    email += tbpwEmailKind.Text;
                    if(!Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                    {//이메일이 글자.글자 형식이 맞는가를 검사한 후 true/false를 반환 >> false면 아래 코드 실행하고 종료
                        MessageBox.Show("이메일이 유효하지 않습니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    } //이메이링 유효하니 계속 진행
                    break;
                default:
                    MessageBox.Show("Error", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
            }
            String pw = "", strQuery = "SELECT pw FROM Member Where email = '";
            strQuery += email;
            strQuery += "' And id = '"+tbpwID.Text+"'";
            //Email, ID입력창에 입력한 email, id을 가져와서 pw값만 가져올 것이므로 sql문을 이렇게 작성
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQLException : " + ex.Message);
            }

            if (pw.Length == 0)
            {
                MessageBox.Show("해당 회원이 없습니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show(tbpwID.Text + " 해당 회원의 암호는 '" + pw + "'입니다.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void tbidEmailKind_Keyup(object sender, KeyEventArgs e)
        {
            int cnt = 0, here = 0;//cnt :.이 나오는 수 here : 2번 이상 나오면 해당 위치
            char[] check = tbidEmailKind.Text.ToCharArray();
            foreach (char a in check)
            {
                here++;
                if (a.Equals('.')) cnt++;
                if (cnt > 1) break;
            }if(cnt > 1){
                tbidEmailKind.Text = "";
                for(int a = 0; a < here-1; a++)
                { //here .이 나온 위치이니 그 전까지 반복해 텍스트박스에 넣어준다
                    tbidEmailKind.Text += check[a].ToString();
                }
            }
        }

        private void tbpwEmailKind_Keyup(object sender, KeyEventArgs e)
        {
            int cnt = 0, here = 0;//cnt :.이 나오는 수 here : 2번 이상 나오면 해당 위치
            char[] check = tbpwEmailKind.Text.ToCharArray();
            foreach (char a in check)
            {
                here++;
                if (a.Equals('.')) cnt++;
                if (cnt > 1) break;
            }
            if (cnt > 1)
            {
                tbpwEmailKind.Text = "";
                for (int a = 0; a < here - 1; a++)
                { //here .이 나온 위치이니 그 전까지 반복해 텍스트박스에 넣어준다
                    tbpwEmailKind.Text += check[a].ToString();
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
