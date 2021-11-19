using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab1
{
    public class V2DataArray : V2Data, IEnumerable<DataItem>, ISerializable
    {
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }
        public Vector2 step { get; private set; }
        public Complex[,] field { get; private set; }
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

        public override IEnumerator<DataItem> GetEnumerator()
        {
            for (int i = 0; i < sizeX; ++i)
                for(int j = 0; j < sizeY; ++j)
                {
                    float x = step.X * i;
                    float y = step.Y * j;
                    yield return new DataItem (new Vector2(x, y), field[i, j]);
                }
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

        public bool SaveBinary(string filename)
        {
            FileStream stream = null;
            try
            {
                stream = File.Create(filename);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(objId);
                writer.Write(timing.ToString());
                writer.Write(sizeX);
                writer.Write(sizeY);
                writer.Write(step.X);
                writer.Write(step.Y);
                for (int i = 0; i < sizeX; ++i)
                    for (int j = 0; j < sizeY; ++j)
                    {
                        writer.Write(field[i, j].Real);
                        writer.Write(field[i, j].Imaginary);
                    }
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to save Binary.\nError message:{ex.Message}");
                return false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
        public static bool LoadBinary(string filename, ref V2DataArray array)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(filename);
                BinaryReader reader = new BinaryReader(stream);
                array.objId = reader.ReadString();
                array.timing = DateTime.Parse(reader.ReadString());
                array.sizeX = reader.ReadInt32();
                array.sizeY = reader.ReadInt32();
                array.step = new Vector2(reader.ReadSingle(), reader.ReadSingle()); 
                array.field = new Complex[array.sizeX, array.sizeY];
                for (int i = 0; i < array.sizeX; ++i)
                {
                    for (int j = 0; j < array.sizeY; ++j)
                    {
                        array.field[i, j] = new Complex(reader.ReadDouble(), reader.ReadDouble());
                    }
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to load Binary.\nError message:{ex.Message}");
                return false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
