using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class V2MainCollection
    {
        List<V2Data> broadList = new List<V2Data>();
        public int Count
        {
            get => broadList.Count;
        }
        public V2Data this[int i]
        {
            get => broadList[i];
        }
        public bool Contains (string ID)
        {
            return broadList.Exists(x => x.objId == ID);
        }
        public bool Add (V2Data v2Data)
        {
            if (!broadList.Exists(x => x.objId == v2Data.objId))
            {
                broadList.Add(v2Data);
                return true;
            }
            else return false;
        }
        public string ToLongString(string format)
        {
            string outString = "";
            foreach (V2Data el in broadList)
            {
                outString += el.ToLongString(format) + '\n';
            }
            return outString;
        }
        public override string ToString()
        {
            string outString = "";
            foreach (V2Data el in broadList)
            {
                outString += el.ToString() + '\n';
            }
            return outString;
        }
    }
}
