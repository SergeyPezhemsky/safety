using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PezhemksyKDZ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Add("p");
            chart1.Series.ElementAt(0).ChartType = SeriesChartType.Spline;
        }

        private void resultButton_Click(object sender, EventArgs e)
        {
            float MTBF = float.Parse(MTBFTexBox.Text);
            float MTTR = float.Parse(MTTRTexBox.Text);
            kTextBox.Text = (MTBF / (MTBF + MTTR)).ToString();
            float N = float.Parse(NTextBox.Text);
            pListBox.Items.Clear();
            aListBox.Items.Clear();
            lyamdaListBox.Items.Clear();
            for (int i = 0; i < tListBox.Items.Count; i++)
            {
                float n = float.Parse(nListBox.Items[i].ToString());
                pListBox.Items.Add(getP(N, n));

                float dt, ndt, Nsr;
                if (i != tListBox.Items.Count - 1)
                {
                    dt = float.Parse(tListBox.Items[i + 1].ToString()) - float.Parse(tListBox.Items[i].ToString());
                    ndt = float.Parse(nListBox.Items[i].ToString()) - float.Parse(nListBox.Items[i + 1].ToString());
                    Nsr = (2 * N - 2* float.Parse(nListBox.Items[i].ToString()) - float.Parse(nListBox.Items[i + 1].ToString())) / 2;
                } else
                {
                    dt = float.Parse(tListBox.Items[i].ToString()) - float.Parse(tListBox.Items[i - 1].ToString());
                    ndt = float.Parse(nListBox.Items[i - 1].ToString()) - float.Parse(nListBox.Items[i].ToString());
                    Nsr = (2 * N - 2 * float.Parse(nListBox.Items[i - 1].ToString()) - float.Parse(nListBox.Items[i].ToString())) / 2;
                }

                aListBox.Items.Add(getA(N, dt, ndt));
                lyamdaListBox.Items.Add(getY(Nsr, dt, ndt));
            }
            float c1 = float.Parse(c1textBox.Text);
            float c2 = float.Parse(c2textBox.Text);
            float y1 = float.Parse(y1textBox.Text);
            float y2 = float.Parse(y2textBox.Text);

            p2listBox.Items.Clear();
            t3listBox.Items.Clear();
            yListBox.Items.Clear();
            for (int i = 0; i < t2listBox.Items.Count; i++)
            {
                float t = float.Parse(t2listBox.Items[i].ToString());

                double f(double x) => c1 * y1 * Math.Exp(-y1 * t) + c2 * y2 * Math.Exp(-y2 * t);
                double p = LeftTriangle(f, t, 100000, 200);

                p2listBox.Items.Add(p);

                t3listBox.Items.Add(LeftTriangle(f, 0, t, 200));
                yListBox.Items.Add(f(t) / LeftTriangle(f, t, 100000, 200));
                chart1.Series.ElementAt(0).Points.AddXY(p, t);
            }
        }

        private float getP(float N, float n)
        {
            return (N - n) / N;
        }

        private float getA(float N, float t, float n)
        {
            return n / (N * t);
        }

        private float getY(float N, float dt, float n)
        {
            return n / (dt * N);
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            tListBox.Items.Add(tTextBox.Text);
            nListBox.Items.Add(nTTextbox.Text);
        }

        static double LeftTriangle(Func<double, double> f, double a, double b, int n)
        {
            var h = (b - a) / n;
            var sum = 0d;
            for (var i = 0; i <= n - 1; i++)
            {
                var x = a + i * h;
                sum += f(x);
            }

            var result = h * sum;
            return result;
        }

        private void add3Button_Click(object sender, EventArgs e)
        {
            t2listBox.Items.Add(t2textBox.Text);
        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
