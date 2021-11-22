using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        //Cвойство типа float, возвращающее минимальное расстояние между точками, в
        //которых измерено поле, среди всех результатов измерений в List<V2Data>.Если в
        //коллекции нет элементов, свойство возвращает значение float.NaN.
        public float MinDistanceCollection
        {
            get
            {
                if (broadList.Count() == 0)
                {
                    return float.NaN;
                }
                var res = from data in broadList
                          where data.Count() != 0
                          select data.MinDistance;
                if (res.Count() == 0)
                {
                    return float.NaN;
                }
                return res.ToList().Min();
            }
        }


        public float MaxDistanceCollection
        {
            get
            {
                if (broadList.Count() == 0)
                {
                    return float.NaN;
                }
                var xy = from data in broadList
                          where data.Count() != 0
                          from el in data
                          select el.coords;

                if (xy.Count() == 0)
                {
                    return float.NaN;
                }

                var dist = from el1 in xy
                           from el2 in xy
                           select Vector2.Distance(el1, el2);

                return dist.ToList().Max();
            }
        }

        //Cвойство типа IEnumerable<Vector2>, которое перечисляет все точки, где измерено
        //поле, и которые встречаются среди всех результатов измерений в коллекции
        //V2MainCollection только один раз.Если в коллекции нет элементов или в коллекции
        //нет таких точек, свойство возвращает значение null.
        public IEnumerable<Vector2> UniqPoints
        {
            get
            {
                var points = (from v2Data in broadList
                           from item in v2Data
                           select item.coords).ToList();
                if (points.Count() == 0)
                {
                    return null;
                }
                var res = from point in points
                          group point by point into titleGroup
                          where titleGroup.Count() == 1
                          select titleGroup.Key;
                if (res.Count() == 0)
                {
                    return null;
                }
                return res;
            }
        }



        //Свойство типа IEnumerable<V2DataList>, которое перечисляет все элементы
        //коллекции V2MainCollection, которые имеют тип V2DataList и такие, что все
        //значения поля имеют ненулевую мнимую часть. Если в коллекции нет
        //элементов, свойство возвращает значение null.
        public IEnumerable<V2DataList> ComplexList
        {
            get
            {
                var lists = (
                    from collection in broadList
                    let list = collection as V2DataList
                    where list != null && (from el in list where el.fieldStrength.Imaginary == 0 select el.fieldStrength).Count() == 0
                    select list
                    );
                if (lists.Count() == 0)
                {
                    return null;
                }
                return lists;
            }
        }
    }
}
