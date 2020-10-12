using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractals
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            var amin = -1.5;
            var amax = 1.5;
            var bmin = -2.0;
            var bmax = 2.0;
            var W = 800;
            var aspect = (bmax - bmin) / (amax - amin);
            var H = (int)Math.Floor(W * aspect);
            var MaxIters = 30;
            var pixMap = new TPixelMap(H , W);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    var a = amin + (amax - amin) * x / (W - 1);
                    var b = bmin + (bmax - bmin) * y / (H - 1);
                    var z = new TComplex(a, b);
                    var c = new TComplex(0.065, 0.122);
                    for (int i = 0; i < MaxIters; i++)
                    {
                        /*a = z.Re;
                        b = z.Im;
                        z.Magnitude = Math.Exp(a);
                        z.Phase = b;
                        z = z + c;*/
                        z = z * z;

                        a = z.Re;
                        b = z.Im;

                        var ez = new TComplex(0, 0);
                        ez.Magnitude = Math.Exp(a);
                        ez.Phase = b;

                        var e_z = new TComplex(0, 0);
                        e_z.Magnitude = Math.Exp(-a);
                        e_z.Phase = -b;

                        var sinh = (ez - e_z) / new TComplex(2, 0);
                        z.Magnitude = Math.Sqrt(sinh.Magnitude);
                        z.Phase = sinh.Phase / 2;
                        z = z + c;
                        if (z.Magnitude > 1E6)
                        {
                            pixMap.Pixels[y, x] = 255 - 255 * i / (MaxIters-1);
                            break;
                        }
                    }
                }

            }
            pixMap.Palette = TPixelMap.CreatePalette(300, 0, 60);

            pictureBox1.Image = pixMap.Image;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
