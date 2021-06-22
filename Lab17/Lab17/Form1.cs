using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int n = 10000;
        private void btStart_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 6; i++)
            {
                chart1.Series[i].Points.Clear();
            }
            Model P1 = new Model((double)lam1.Value, n);
            Model P2 = new Model((double)lam2.Value, n);
            while (P1.t > P1.Time)
            {
                P1.ExpRV();
                chart1.Series[0].Points.AddXY(P1.Time, P1.lambda);
                chart1.Series[3].Points.AddXY(P1.Time, P1.lambda);
                chart1.Series[2].Points.AddXY(P1.Time, P1.lambda + P2.lambda);
                chart1.Series[5].Points.AddXY(P1.Time, P1.lambda + P2.lambda);
            }
            while (P2.t > P2.Time)
            {
                P2.ExpRV();
                chart1.Series[1].Points.AddXY(P2.Time, P2.lambda);
                chart1.Series[4].Points.AddXY(P2.Time, P2.lambda);
                chart1.Series[2].Points.AddXY(P2.Time, P1.lambda + P2.lambda);
                chart1.Series[5].Points.AddXY(P2.Time, P1.lambda + P2.lambda);
            }

            int max1 = P1.SC();
            int max2 = P2.SC();
            int size;
            if (max1 < max2)
            {
                size = max2 + 1;
            }
            else
            {
                size = max1 + 1;
            }
            double[] Freq = new double[size];
            for (int i = 0; i < P1.N; i++)
            {
                Freq[P1.ArrayCountPoints[i]]++;
            }
            for (int i = 0; i < P2.N; i++)
            {
                Freq[P2.ArrayCountPoints[i]]++;
            }
            for (int i = 0; i < size; i++)
            {
                Freq[i] = Freq[i] / P1.N * 2;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Model P1 = new Model((double)lam1.Value, n);
            Model P2 = new Model((double)lam2.Value, n);
            while (P1.t > P1.Time)
            {
                P1.ExpRV();
                chart1.Series[0].Points.AddXY(P1.Time, P2.lambda);
                chart1.Series[3].Points.AddXY(P1.Time, P2.lambda);
            }
        }
    }
}
