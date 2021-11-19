using System;
using System.Collections.Generic;
using System.Numerics;

namespace Lab1
{
    public class Program
    {

        static void Main(string[] args)
        {
            /*
             Lab 1
              
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

            Lab 2
            */
            TestFilesavingFeature();
            TestLINQrequests();

 

        }
        static void TestFilesavingFeature()
        {
            Fv2Complex F = StaticClass.noize1;
            V2DataArray beforeArr = new V2DataArray("\"test 1 data 1\"", DateTime.Now, 3, 4, new System.Numerics.Vector2(3, 7), F);
            Console.WriteLine("Random V2DataArray:\n");
            Console.WriteLine(beforeArr.ToLongString("F2"));
            V2DataArray afterArr = new V2DataArray("loader", DateTime.UtcNow);
            beforeArr.SaveBinary("v2DataArray.bin");
            V2DataArray.LoadBinary("v2DataArray.bin", ref afterArr);
            Console.WriteLine("Array after loading from binary:\n");
            Console.WriteLine(afterArr.ToLongString("F2"));

            V2DataList beforeList = new V2DataList("\"test 2 data 4\"", DateTime.Today);
            beforeList.Add(new DataItem(new Vector2(2, 1), 0));
            beforeList.Add(new DataItem(new Vector2(0, 1), 3));
            beforeList.Add(new DataItem(new Vector2(1, 7), 5));
            beforeList.Add(new DataItem(new Vector2(-3, 4), 10));
            beforeList.Add(new DataItem(new Vector2(-1, 0), 1));
            Console.WriteLine("Random V2DataList:\n");
            Console.WriteLine(beforeList.ToLongString("F2"));
            beforeList.SaveAsText("V2DataList.txt");
            V2DataList afterList = new V2DataList("\"listing\"", DateTime.Today);
            V2DataList.LoadAsText("V2DataList.txt", ref afterList);
            Console.WriteLine("List after loading from text:\n");
            Console.WriteLine(afterList.ToLongString("F2"));
        }
        static void TestLINQrequests()
        {
            Console.WriteLine("\nLINQ REQUESTS\n");
            V2DataArray emptyArray = new V2DataArray("nothing in here", DateTime.UtcNow);
            V2DataList emptyList = new V2DataList("nothing in here x2", DateTime.UtcNow);
            Fv2Complex F = StaticClass.noize1;
            V2DataArray randomArray = new V2DataArray("random Array", DateTime.Now, 2, 3, new System.Numerics.Vector2(3, 7), F);
            V2DataList randomList = new V2DataList("random List", DateTime.UtcNow);
            randomList.Add(new DataItem(new Vector2(2, 1), 0));
            randomList.Add(new DataItem(new Vector2(0, 1), 3));
            randomList.Add(new DataItem(new Vector2(1, 7), 5));
            randomList.Add(new DataItem(new Vector2(-3, 4), 10));
            randomList.Add(new DataItem(new Vector2(-1, 0), 1));

            V2MainCollection CollectionLINQ_1 = new V2MainCollection();
            Console.WriteLine("1. MinDistanceCollection");
            Console.WriteLine($"\tEmpty collection: {CollectionLINQ_1.MinDistanceCollection}");
            CollectionLINQ_1.Add(emptyArray);
            Console.WriteLine($"\tEmpty array in the collection: {CollectionLINQ_1.MinDistanceCollection}");
            CollectionLINQ_1.Add(emptyList);
            Console.WriteLine($"\tAdding an empty List to the collection: {CollectionLINQ_1.MinDistanceCollection}");
            CollectionLINQ_1.Add(randomArray);
            Console.WriteLine($"\tAdding an array with {randomArray.MinDistance} minimal distance to the collection: {CollectionLINQ_1.MinDistanceCollection}");
            CollectionLINQ_1.Add(randomList);
            Console.WriteLine($"\tAdding a list with {randomList.MinDistance} minimal distance to the collection: {CollectionLINQ_1.MinDistanceCollection}");

            V2MainCollection CollectionLINQ_2 = new V2MainCollection();
            Console.WriteLine("\n3. UniqPoints");
            Console.WriteLine("\tEmpty collection:");
            WriteSeq(CollectionLINQ_2.UniqPoints);
            CollectionLINQ_2.Add(emptyArray);
            Console.WriteLine("\tAddding an empty array to the collection:");
            WriteSeq(CollectionLINQ_2.UniqPoints);
            CollectionLINQ_2.Add(emptyList);
            Console.WriteLine("\tAddding an empty List to the collection:");
            WriteSeq(CollectionLINQ_2.UniqPoints);
            CollectionLINQ_2.Add(randomArray);
            Console.WriteLine("\tAdding an array to the collection:");
            WriteSeq(CollectionLINQ_2.UniqPoints);
            V2DataArray randomArrayCopy = new V2DataArray("random Array copy", DateTime.Now, 2, 3, new System.Numerics.Vector2(3, 7), F);
            CollectionLINQ_2.Add(randomArrayCopy);
            Console.WriteLine("\tAdding the same array to the collection:");
            WriteSeq(CollectionLINQ_2.UniqPoints);
            CollectionLINQ_2.Add(randomList);
            Console.WriteLine("\tAdding a list to the collection:");
            WriteSeq(CollectionLINQ_2.UniqPoints);
            V2DataList intersectList = new V2DataList("intersecting List", DateTime.UtcNow);
            randomList.Add(new DataItem(new Vector2(3, 4), 1));
            randomList.Add(new DataItem(new Vector2(0, 1), 3));
            randomList.Add(new DataItem(new Vector2(1, 7), 5));
            randomList.Add(new DataItem(new Vector2(-3, 0), 3));
            CollectionLINQ_2.Add(randomList);
            Console.WriteLine("\tAdding a list of size 4 with intersecting points to the collection:");
            WriteSeq(CollectionLINQ_2.UniqPoints);

            V2MainCollection CollectionLINQ_3 = new V2MainCollection();
            Console.WriteLine("\n3. ComplexList");
            Console.WriteLine("\tEmpty collection:");
            WriteSeq(CollectionLINQ_3.ComplexList);
            CollectionLINQ_3.Add(randomArray);
            Console.WriteLine("\tArray in the collection:");
            WriteSeq(CollectionLINQ_3.ComplexList);
            CollectionLINQ_3.Add(emptyList);
            Console.WriteLine("\tAdding an empty List to the collection:");
            WriteSeq(CollectionLINQ_3.ComplexList);
            CollectionLINQ_3.Add(randomList);
            Console.WriteLine("\tAdding a list with non-complex elements to the collection:");
            WriteSeq(CollectionLINQ_3.ComplexList);
            V2DataList targetList = new V2DataList("complex List", DateTime.UtcNow);
            targetList.Add(new DataItem(new Vector2(0, 1), 0));
            targetList.Add(new DataItem(new Vector2(1, 2), 3));
            targetList.Add(new DataItem(new Vector2(2, 3), 5));
            targetList.Add(new DataItem(new Vector2(3, 4), 10));
            CollectionLINQ_3.Add(targetList);
            Console.WriteLine("\tAdding a list with only complex elements to the collection:");
            WriteSeq(CollectionLINQ_3.ComplexList);
        }   

        static void WriteSeq<T>(IEnumerable<T> seq)
        {
            if (seq == null)
            {
                Console.WriteLine("\t\tnull");
            }
            else
            {
                foreach(T el in seq)
                {
                    Console.WriteLine($"\t\t{el}");
                }
            }
            
        }
    }
}
