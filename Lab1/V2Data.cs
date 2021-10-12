using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public abstract class V2Data
    {
        public string objId { get; }
        public DateTime timing { get; }
        public V2Data(string objId, DateTime timing)
        {
            this.objId = objId;
            this.timing = timing;
        }
        public abstract int Count { get; }
        public abstract float MinDistance { get; }
        public abstract string ToLongString(string format);
        public override string ToString()
        {
            return ($"object: {objId}, date: {timing}");
        }
    }
}
