using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab1
{
    public class V2DataArray : V2Data
    {
        public int sizeX { get; }
        public int sizeY { get; }
        public Vector2 step { get; }
        public Complex[,] field { get; }
        public V2DataArray(string objId, DateTime timing) : base(objId, timing)
        {
            field = new Complex[0, 0];
        }
        public V2DataArray(string objId, DateTime timing, int sizeX, int sizeY, Vector2 step, Fv2Complex F) : base(objId, timing)
        {
            this.sizeX = sizeX > 0 ? sizeX : 0;
            this.sizeY = sizeY > 0 ? sizeY : 0;
            this.step = step;
            field = new Complex[sizeX, sizeY];
            for (int i = 0; i < sizeX; ++i)
            {
                for (int j = 0; j < sizeY; ++j)
                {
                    field[i, j] = F(new Vector2(step.X * i, step.Y * j));
                }
            }

        }
        public override int Count
        {
            get
            {
                return sizeY * sizeX;
            }
        }
        public override float MinDistance
        {
            get
            {
                if (sizeX < 1 || sizeY < 1) return -1;
                else if (sizeY == 1)
                {
                    return step.X;
                }
                else if (sizeX == 1)
                {
                    return step.Y;
                }
                else return Math.Min(step.X, step.Y);
            }
        }
        public override string ToString()
        {
            return ($"Type: Array; {base.ToString()}; Dimensions: x = {sizeX}, y = {sizeY}; Steps: x = {step.X}, y = {step.Y}.");
        }
        public override string ToLongString(string format)
        {
            string outString = $"{ToString()}\n";
            for (int i = 0; i < sizeX; ++i)
            {
                for (int j = 0; j < sizeY; ++j)
                {
                    outString += $"X = {(i * step.X).ToString(format)}, Y = {(j * step.Y).ToString(format)}," +
                        $" Field: {field[i, j].Real.ToString(format)} + {field[i, j].Imaginary.ToString(format)}i" +
                        $" of magnitude {field[i, j].Magnitude.ToString(format)}" + '\n';
                }
            }
            return outString;
        }
        public static explicit operator V2DataList(V2DataArray data)
        {
            V2DataList outList = new V2DataList(data.objId, data.timing);
            for (int i = 0; i < data.sizeX; ++i)
            {
                for (int j = 0; j < data.sizeY; ++j)
                {
                    DataItem item = new DataItem(new Vector2(data.step.X * i, data.step.Y * j), data.field[i, j]);
                    outList.Add(item);
                }
            }
            return outList;
        }
    }
}
