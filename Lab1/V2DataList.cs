using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class V2DataList : V2Data
    {
        List<DataItem> points { get; }
        public V2DataList(string objId, DateTime timing): base(objId, timing)
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
                return min;
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
    }
}
