using System;
using System.Numerics;

namespace Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" [ 1 ]\n");
            Fv2Complex F = StaticClass.noize1;
            V2DataArray fieldArr = new V2DataArray("\"test 1 data 1\"", DateTime.Now, 3, 4, new System.Numerics.Vector2(3, 7), F);
            Console.WriteLine(fieldArr.ToLongString("F2"));
            V2DataList fieldList = (V2DataList)fieldArr;
            Console.WriteLine(fieldList.ToLongString("F2"));
            Console.WriteLine($"Array:\n\tCount: {fieldArr.Count}, Minimal distance: {fieldArr.MinDistance}" +
                $"\nList:\n\tCount: {fieldList.Count}, Minimal distance: {fieldList.MinDistance}");

            Console.WriteLine("\n\n\n [ 3 ]\n");
            V2MainCollection data = new V2MainCollection();
            data.Add(new V2DataArray("\"test 2 data 1\"", DateTime.UtcNow, 0, 1, new Vector2 (0, (float)0.2), F));
            data.Add(new V2DataArray("\"test 2 data 2\"", DateTime.UtcNow, 2, 2, new Vector2(3, 3), F));
            V2DataList data3 = new V2DataList("\"test 2 data 3\"", DateTime.Today);
            data3.AddDefaults(7, F);
            data.Add(data3);
            V2DataList data4 = new V2DataList("\"test 2 data 4\"", DateTime.Today);
            data4.Add(new DataItem(new Vector2(2, 1), 0));
            data4.Add(new DataItem(new Vector2(0, 1), 3));
            data4.Add(new DataItem(new Vector2(1, 7), 5));
            data4.Add(new DataItem(new Vector2(-3, 4), 10));
            data4.Add(new DataItem(new Vector2(-1, 0), 1));
            data.Add(data4);
            for (int i = 0; i < 4; ++i)
            {
                Console.WriteLine($"Dataset {i + 1}: Count = {data[i].Count}, MinDistance = {data[i].MinDistance}\n");
            }

        }
    }
}
