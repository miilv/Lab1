using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1
{
    public class V2DataList : V2Data, IEnumerable<DataItem>
    {
        List<DataItem> points { get; }
        public V2DataList(string objId, DateTime timing) : base(objId, timing)
        {
            points = new List<DataItem>();
        }
        public bool Add(DataItem newItem)
        {
            if (points.Exists(x => x.coords == newItem.coords))
            {
                return false;
            }
            else
            {
                points.Add(newItem);
                return true;
            }
        }
        public int AddDefaults(int nItems, Fv2Complex F)
        {
            int count = 0;
            Random rnd = new Random();
            for (int i = 0; i < nItems; ++i)
            {
                Vector2 rndCoords = new Vector2((float)rnd.NextDouble() * 100, (float)rnd.NextDouble() * 100);
                Complex rndField = F(rndCoords);
                DataItem rndItem = new DataItem(rndCoords, rndField);
                if (Add(rndItem))
                {
                    ++count;
                }
            }
            return count;
        }
        public override int Count
        {
            get
            {
                return points.Count;
            }
        }
        public override float MinDistance
        {
            get
            {
                float min = float.MaxValue;
                foreach (DataItem p1 in points)
                {
                    foreach (DataItem p2 in points.Where(x => x.coords != p1.coords))
                    {
                        float dist = Vector2.Distance(p1.coords, p2.coords);
                        if (dist < min) min = dist;
                    }
                }
                return min == float.MaxValue ? -1 : min;
            }
        }
        public override string ToString()
        {
            return ($"Type: List; {base.ToString()}; Number of elements: {Count}.");
        }
        public override string ToLongString(string format)
        {
            string outString = $"{ToString()}\n";
            foreach (DataItem point in points)
            {
                outString += point.ToLongString(format) + '\n';
            }
            return outString;
        }

        public override IEnumerator<DataItem> GetEnumerator()
        {
            foreach (DataItem point in points)
            {
                yield return point;
            }
        }
        public bool SaveAsText(string filename)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(filename, FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(objId);
                writer.WriteLine(timing.ToString());
                writer.WriteLine(points.Count);
                foreach (DataItem el in points)
                {
                    writer.WriteLine(el.coords.X.ToString());
                    writer.WriteLine(el.coords.Y.ToString());
                    writer.WriteLine(el.fieldStrength.Real.ToString());
                    writer.WriteLine(el.fieldStrength.Imaginary.ToString());
                }
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to save data list as a text file.\nError message:{ex.Message}");
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
        public static bool LoadAsText(string filename, ref V2DataList list)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(filename);
                StreamReader reader = new StreamReader(stream);
                list.objId = reader.ReadLine();
                list.timing = DateTime.Parse(reader.ReadLine());
                int size = Convert.ToInt32(reader.ReadLine());
                for (int i = 0; i < size; ++i)
                {
                    Vector2 coords = new Vector2(Convert.ToSingle(reader.ReadLine()), Convert.ToSingle(reader.ReadLine()));
                    Complex field = new Complex(Convert.ToDouble(reader.ReadLine()), Convert.ToDouble(reader.ReadLine()));
                    list.Add(new DataItem(coords, field));
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to open data list as a text file.\nError message:{ex.Message}");
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
    }
}
