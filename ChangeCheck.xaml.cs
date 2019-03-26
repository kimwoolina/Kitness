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
    public partial class ChangeCheck : Window
    {
        Useredit useredit;
        public ChangeCheck(Useredit edit)
        {
            this.useredit = edit;
            InitializeComponent();
            Set();
        }
        DBM dbm = new DBM();

        public void Set()
        {
            if (useredit.Kind)
            {
                lblAsk.Content = "정말 회원탈퇴를 하시겠습니까?";
            }
            else
            {
                lblAsk.Content = "정말 암호를 바꾸시겠습니까?";
            }
            
        }

        private void btYes_Click(object sender, RoutedEventArgs e)
        {
            if (useredit.Kind)
            {
                String strQuery = "DELETE FROM Member naturaljoin 테이블명 Where id = '";
                strQuery += useredit.ID;
                strQuery += "'";
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
                MessageBox.Show("회원탈퇴 완료! \n 프로그램이 종료됩니다.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0);
            }
            else
            {
                String strQuery = "Update Member Set pw ='";
                strQuery += useredit.tbCPW +"' Where id = '";
                strQuery += useredit.ID;
                strQuery += "'";
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
                MessageBox.Show("암호변경 완료!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
