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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            num1.Focus();
        }

        public struct rolling_phase_division
        {
            public int j { get; set; }
            public int Qj { get; set; }
            public int Rj { get; set; }
            public int Rj1 { get; set; }
            public int Sj_1 { get; set; }
            public int Sj { get; set; }
            public int Tj_1 { get; set; }
            public int Tj { get; set; }

            public rolling_phase_division(int j, int qj, int rj, int rj1, int sj_1, int sj, int tj_1, int tj) : this()
            {
                this.j = j;
                Qj = qj;
                Rj = rj;
                Rj1 = rj1;
                Sj_1 = sj_1;
                Sj = sj;
                Tj_1 = tj_1;
                Tj = tj;
            }
        }
        
        private void calculate()
        {
            List<rolling_phase_division> l = new List<rolling_phase_division>();

            int a = int.Parse(num1.Text);
            int b = int.Parse(num2.Text);
            int j = 0, qj = 0, rj = a, rj1 = b, sj_1 = 0, sj = 1, tj_1 = 1, tj = 0;
            rolling_phase_division r1 = new rolling_phase_division(j, qj, rj, rj1, sj_1, sj, tj_1, tj);
            l.Add(r1);
         
            while (rj1 > 0) 
            {
                j++;
                int qm = qj;
                qj = rj / rj1;
                int rm = rj % rj1;
                rj = rj1;
                rj1 = rm;
                int sm = sj;
                sj = sj_1 + (-1) * qm * sj;
                sj_1 = sm;
                int tm = tj;
                tj = tj_1 + (-1) * qm * tj;
                tj_1 = tm;
                rolling_phase_division r = new rolling_phase_division(j, qj, rj, rj1, sj_1, sj, tj_1, tj);
                l.Add(r);
            }

            dataGrid.ItemsSource = l;
            result.Text = "("+a+"," + b+")="+Gcd(a,b)+"\n" + a+"*"+sj+"+"+b+"*"+tj+"="+rj;
        }

        public int Gcd(int a,int b)
        {
            if (a < b)
                swap(a, b);
            return b==0?a:Gcd(b,a%b);
        }

        public void swap(Object a,Object b)
        {
            Object c = a;
            a = b;
            b = c;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            calculate();
        }
        
    }

}
