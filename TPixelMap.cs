using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Fractals
{
    public class TPixelMap
    {
        public int[,] Pixels;
        public Color[] Palette;
        public Bitmap Image {
            get{
                var pf = PixelFormat.Format8bppIndexed;
                var bmp = new Bitmap(Pixels.GetLength(1), Pixels.GetLength(0), pf);
                var rc = new Rectangle(0, 0, bmp.Width, bmp.Height);
                var data = bmp.LockBits(rc, ImageLockMode.WriteOnly, bmp.PixelFormat);
                var buffer = new byte[Pixels.Length];
                //Array.Copy(Pixels, buffer, buffer.Length);
                for (int y=0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        buffer[y * bmp.Width + x] = (byte)Pixels[y,x];
                    }
                }
                Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
                var pal = bmp.Palette;
                Array.Copy(Palette, pal.Entries, Palette.Length);
                bmp.Palette = pal;
                bmp.UnlockBits(data);
                
                return bmp;
            }
        }
        public TPixelMap(int Height, int Width)
        {
            Pixels = new int[Height, Width];
        }
        public static Color[] CreatePalette(int startHue, int stopHue, int offset)
        {
            var pal = new Color[256];

            for (int i = 0; i < pal.Length; i++)
            {
                double hue = startHue + (stopHue - startHue) * i /(double) (pal.Length - 1);
                var r = getComponent(hue + offset, offset);
                var g = getComponent(hue, offset);
                var b = getComponent(hue - offset, offset);
                pal[i] = Color.FromArgb((int)r,(int) g,(int) b);
             }
            return pal;
        }

        public static double getComponent(double hue, int offset)
        {
            double c = 0;
            if (hue < 0)
                hue += 360;
            if (hue > 360)
                hue -= 360;
            //hue = hue % 360;
            if (hue < 60)
            {
                c =  255*hue/60;
            }
            else if (hue<60+offset)
             {
                c = 255;
            }
            else if (hue<120+offset)
            {
                c = 255 * (120 + offset - hue) / 60;
            }

            return c;
        }
    }
}
