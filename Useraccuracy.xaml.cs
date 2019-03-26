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
    /// Useraccuracy.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Useraccuracy : Window
    {
        DBM dbm = new DBM();
        List<string> day = new List<string>();
        List<string> spine =new List<string>();
        List<string> neck = new List<string>();
        List<string> leg =  new List<string>();
        Start start;
        Polyline l_poly = new Polyline();
        Polyline n_poly = new Polyline();
        Polyline s_poly = new Polyline();

        public Useraccuracy(Start st)
        {
            this.start = st;
            InitializeComponent();
            
            l_poly.Stroke = new SolidColorBrush(Colors.Green);
            l_poly.StrokeThickness = 3;
            l_poly.Visibility = Visibility.Collapsed;
            Canvas.SetTop(l_poly, 0);
            Canvas.SetLeft(l_poly, 0);
            lcanvas.Children.Add(l_poly);

            n_poly.Stroke = new SolidColorBrush(Colors.Green);
            n_poly.StrokeThickness = 3;
            n_poly.Visibility = Visibility.Collapsed;
            Canvas.SetTop(n_poly, 0);
            Canvas.SetLeft(n_poly, 0);
            ncanvas.Children.Add(n_poly);

            s_poly.Stroke = new SolidColorBrush(Colors.Green);
            s_poly.StrokeThickness = 3;
            s_poly.Visibility = Visibility.Collapsed;
            Canvas.SetTop(s_poly, 0);
            Canvas.SetLeft(s_poly, 0);
            scanvas.Children.Add(s_poly);

            set();
        }

        public void set()
        {
            username.Content = start.ID;
            int cnt = 0, accuracy = 0;
            PointCollection sp = new PointCollection(7);
            PointCollection np = new PointCollection(7);
            PointCollection lp = new PointCollection(7);
            String strQuery = "SELECT * FROM Record Where id = '";
            strQuery += start.ID + "'";
            //Email입력창에 입력한 email을 가져와서 id값만 가져올 것이므로 sql문을 이렇게 작성
            try
            {
                dbm.dbOpen(); //DB open
                dbm.DB_stmt.CommandText = strQuery;
                dbm.DB_rs = dbm.DB_stmt.ExecuteReader();
                while (dbm.DB_rs.Read())
                {
                    day.Add(dbm.DB_rs["date"].ToString());
                    spine.Add(dbm.DB_rs["spine"].ToString());
                    neck.Add(dbm.DB_rs["neck"].ToString());
                    leg.Add(dbm.DB_rs["leg"].ToString());
                }
                dbm.dbClose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQLException : " + ex.Message);
                return;
            }

            Label[] sdate = { sday1, sday2, sday3, sday4, sday5, sday6, sday7 };
            Label[] ldate = { lday1, lday2, lday3, lday4, lday5, lday6, lday7 };
            Label[] ndate = { nday1, nday2, nday3, nday4, nday5, nday6, nday7 };
            for (int c = 1; c < day.Count; c++)
            {
                sdate[c].Content = day.ElementAt(c);
                ldate[c].Content = day.ElementAt(c);
                ndate[c].Content = day.ElementAt(c);
            }

            for (int c = 0; c < spine.Count; c++)
            {
                cnt = 0;
                if (c == 0)
                {
                    accuracy = int.Parse(spine.ElementAt(c))*2;
                    sp.Add(new Point((int)(100 * c - 5), (int)(accuracy)));
                    continue;
                }
                Char[] acc;
                try
                {
                    acc = spine.ElementAt(c).ToCharArray();
                    for (int a = 0; a < acc.Length; a++)
                    {
                        if (acc[a].Equals('1')) ++cnt;
                    }
                    if (acc.Length != 0)
                    {
                        accuracy = (int)((cnt / acc.Length) * 100)*2;
                    }
                    sp.Add(new Point((int)(100*c-5), (int)(accuracy)));
                }
                catch(Exception e)
                {
                    Console.WriteLine("SQLException : " + e.Message);
                }
            }

            for (int c = 0; c < leg.Count; c++)
            {
                cnt = 0;
                if (c == 0)
                {
                    accuracy = int.Parse(leg.ElementAt(c)) ;
                    lp.Add(new Point(-50,50));
                    continue;
                }
                Char[] acc;
                try
                {
                    acc = leg.ElementAt(c).ToCharArray();
                    for (int a = 0; a < acc.Length; a++)
                    {
                        if (acc[a].Equals('1')) ++cnt;
                    }
                    if (acc.Length != 0)
                    {
                        accuracy = (int)(((double)cnt / (double)acc.Length) * 100) ;
                    }

                    lp.Add(new Point((int)(100 * c), (int)(accuracy)));
                }
                catch (Exception e)
                {
                    Console.WriteLine("SQLException : " + e.Message);
                }
            }

            for (int c = 0; c < neck.Count; c++)
            {
                cnt = 0;
                if (c == 0)
                {
                    accuracy = int.Parse(neck.ElementAt(c)) * 2;
                    np.Add(new Point((int)(100 * c - 5), (int)(accuracy)));
                    continue;
                }
                Char[] acc;
                try
                {
                    acc = neck.ElementAt(c).ToCharArray();
                    for (int a = 0; a < acc.Length; a++)
                    {
                        if (acc[a].Equals('1')) ++cnt;
                    }
                    if (acc.Length != 0)
                    {
                        accuracy = (int)((cnt / acc.Length) * 100) * 2;
                    }

                    np.Add(new Point((int)(100 * c), (int)(accuracy)));
                }
                catch (Exception e)
                {
                    Console.WriteLine("SQLException : " + e.Message);
                }
            }

            l_poly.Points = lp;
            l_poly.Visibility = Visibility.Visible;

            n_poly.Points = np;
            n_poly.Visibility = Visibility.Visible;

            s_poly.Points = sp;
            s_poly.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
