using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab1
{
    public struct DataItem
    {
        public Vector2 coords { get; set; }
        public Complex fieldStrength { get; set; }
        public DataItem(Vector2 coords, Complex fieldStrength)
        {
            this.coords = coords;
            this.fieldStrength = fieldStrength;
        }
        public string ToLongString(string format)
        {
            return ($"X = {coords.X.ToString(format)}, Y = {coords.Y.ToString(format)}," +
                $" Field: {fieldStrength.Real.ToString(format)} + {fieldStrength.Imaginary.ToString(format)}i" +
                $" of magnitude {fieldStrength.Magnitude.ToString(format)}");
        }
        public override string ToString()
        {
            return ($"X={coords.X}, Y={coords.Y}, Field={fieldStrength.Real}+{fieldStrength.Imaginary}i");
        }
    }

    
}
