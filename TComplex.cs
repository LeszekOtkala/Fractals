using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    public struct TComplex
    {
        public double Re;
        public double Im;
        public double Magnitude {
            get
            {
                return Math.Sqrt(Re * Re + Im * Im);
            }
            set
            {
                var phase = Phase;
                Re = value * Math.Cos(phase);
                Im = value * Math.Sin(phase);
            }
        }

        public double Phase
        {
            get
            {
                return Math.Atan2(Im, Re);
            }

            set
            {
                var mag = Magnitude;
                Re = mag * Math.Cos(value);
                Im = mag * Math.Sin(value);
            }
        }

        public TComplex(double re, double im)
        {
            Re = re;
            Im = im;
        }

        public static TComplex operator *(TComplex left, TComplex right)
        {
            var result = new TComplex(0, 0);
            result.Magnitude = left.Magnitude * right.Magnitude;
            result.Phase = left.Phase + right.Phase;
            return result;
        }

        public static TComplex operator /(TComplex left, TComplex right)
        {
            var result = new TComplex(0, 0);
            result.Magnitude = left.Magnitude / right.Magnitude;
            result.Phase = left.Phase - right.Phase;
            return result;
        }

        public static TComplex operator +(TComplex left, TComplex right)
        {
            return new TComplex(left.Re+right.Re, left.Im+right.Im);
            
        }
        public static TComplex operator -(TComplex left, TComplex right)
        {
            return new TComplex(left.Re - right.Re, left.Im - right.Im);

        }

    }
}
