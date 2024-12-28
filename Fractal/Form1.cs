using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal
{
    public partial class Form1 : Form
    {
        public int iterator = 0;
        public double zoom = 0.005f;
        public double shiftX = 500d;
        public double shiftY = 210d;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Fractal();
        }

        public void Fractal()
        { 
            var bmp = new Bitmap(Width, Height);
            for (int i = 0; i < bmp.Width; i++)
            {

                for (int j = 0; j < bmp.Height; j++)
                {
                    var z = new Complex(0, 0);

                    double x = (double)(i - shiftX) * zoom;
                    double y = (double)(j - shiftY) * zoom;

                    var c = new Complex(x, y);

                    int iterator = 0;

                    while ((z.x * z.x + z.y * z.y) <= 4 && iterator < 255)
                    {
                        z = z * z + c;
                        iterator++;
                    }

                    if (iterator == 255)
                        bmp.SetPixel(i, j, Color.FromArgb(0, 0, 0));

                    bmp.SetPixel(i, j, Color.FromArgb((byte)(iterator), (byte)(iterator), (byte)(iterator)));
                }
                pictureBox1.Image = bmp;
            }
        }

        int SHIFT = 90;


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if (e.KeyCode == Keys.A)
            {
                shiftX += SHIFT;
                Fractal();
            }
            if (e.KeyCode == Keys.D)
            {
                shiftX -= SHIFT;
                Fractal();
            }

            if (e.KeyCode == Keys.W)
            {
                shiftY += SHIFT;
                Fractal();
            }
            if (e.KeyCode == Keys.S)
            {
                shiftY -= SHIFT;
                Fractal();
            }
            if (e.KeyCode == Keys.X) 
            {
                zoom += 0.003f;
                Fractal();
            }
            if (e.KeyCode == Keys.C)
            {
                zoom -= 0.003f;
                Fractal();
            }
        }
        public class Complex
        {
            public double x;
            public double y;
            public Complex(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            public static Complex operator +(Complex a, Complex b)
            {
                var tmp = new Complex(0, 0);
                tmp.x = a.x + b.x;
                tmp.y = a.y + b.y;
                return tmp;
            }
            public static Complex operator *(Complex a, Complex b)
            {
                var tmp = new Complex(0, 0);
                tmp.x = a.x * b.x - b.y * a.y;
                tmp.y = a.x * b.y + b.x * a.y;
                return tmp;
            }
        }
    }
}

