using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace prak3_oop_winform
{
    public partial class Form1 : Form
    {
        int N = 49;
        int k = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double x1, x2;
            double y = 0, y1, y3, y4, y5;
            float t1 = float.Parse(textBox1.Text);
            float t2 = float.Parse(textBox1.Text);
            double result = 0;

            x1 = 2 * t1;
            x2 = 5 * t2;
            y1 = 1 / (4 + x2);
            y3 = Math.Exp(x2 / x1);
            y4 = Math.Pow(Math.Cos(y3), 2);
            y5 = Math.Sqrt(y4);
            y += y1 * y5;
            result = y;

            textBox2.Text = result.ToString();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            //Маштаб по осі х
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 50);
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            //Маштаб по осі у
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-1, 1);
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;

            

            for (int i = 0; i < 50; i++)
            {
                double x1, x2;
                double y = 0, y1, y3, y4, y5;
                
                double t1=0, t2=0;

                x1 = 2 * (t1 + i);
                x2 = 5 * (t2 + i);
                y1 = 1 / (4 + x2);
                y3 = Math.Exp(x2 / x1);
                y4 = Math.Pow(Math.Cos(y3), 2);
                y5 = Math.Sqrt(y4);
                y += y1 * y5;

                chart1.Series[0].Points.AddXY(i, y);
            }
        }
        
            public void timer1_Tick(object sender, EventArgs e)
        {
            double x1, x2;
            double y = 0, y1, y3, y4, y5;

            double t1 = 0, t2 = 0;

            x1 = 2 * (t1 + N);
            x2 = 5 * (t2 + N);
            y1 = 1 / (4 + x2);
            y3 = Math.Exp(x2 / x1);
            y4 = Math.Pow(Math.Cos(y3), 2);
            y5 = Math.Sqrt(y4);
            y += y1 * y5;
            N++;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, N);
            chart1.Series[0].Points.RemoveAt(0);
            chart1.Series[0].Points.AddXY(N, y);
            chart1.ChartAreas[0].AxisX.Minimum = N - 50;
            chart1.ChartAreas[0].AxisX.Maximum = N;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                button1.Text = "Старт";
            } 
            else
            {
                timer1.Enabled = true;
                button1.Text = "Зупинити";
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            timer1.Interval = hScrollBar1.Value;
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            if (k == 0)
            {
                chart2.Series[0].Points.AddXY(k, 3);
                chart2.Series[0].Points[k].LegendText = "Відмінно";
            } if (k == 1)
            {
                chart2.Series[0].Points.AddXY(k, 10);
                chart2.Series[0].Points[k].LegendText = "Добре";
            } if (k == 2)
            {
                chart2.Series[0].Points.AddXY(k, 5);
                chart2.Series[0].Points[k].LegendText = "Задовільно";
            } if (k == 3)
            {
                chart2.Series[0].Points.AddXY(k, 2);
                chart2.Series[0].Points[k].LegendText = "Погано";
            }
            k++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                int X = 0;
                while (!streamReader.EndOfStream) 
                {
                    int y = Convert.ToInt16(streamReader.ReadLine());
                    chart3.Series[0].Points.AddXY(X, y);
                    X++;
                }
                streamReader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog1.FileName);
                for (int i = 0; i < chart3.Series[0].Points.Count; i++)
                {
                    streamWriter.WriteLine(chart3.Series[0].Points[i].YValues[0]);
                }
                streamWriter.Close();
            }
        }
    }
}
