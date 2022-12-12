
internal class Program
{
    private static void Main(string[] args)
    {
        string s = String.Empty;
        /*Дана коллекция List<T>. Требуется подсчитать, сколько раз каждый элемент встречается в
            данной коллекции:
                a. для целых чисел;
                b. * для обобщенной коллекции;
                c. ** используя Linq. */
        Console.WriteLine("Дана коллекция List<T>. Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции");
        List<char> chars = new List<char>() { 'd', 'f', 'g', 'd', 'd', 'h', 'u', 'u', 'u', 'g', 'h', 'g', '1', 'a', 'a', 'a', 'a', '1', 'd'};
        foreach (var item in chars) Console.Write(item + " ");
        Console.Write("\n");
        Console.WriteLine(CountElemsLinq<char>(chars));

        List<int> ints = new List<int>();
        Random r = new();
        for (int i = 0; i < 50; i++)
        {
            ints.Add(r.Next(0, 9));
            Console.Write(ints[i] + " ");
        }
        Console.WriteLine("\n");
        Console.WriteLine(CountElemsLinq<int>(ints));
        
        /*Свернуть обращение к OrderBy с использованием лямбда-выражения =>.*/
        Console.WriteLine("Свернуть обращение к OrderBy с использованием лямбда-выражения");
        Dictionary<string, int> dict = new Dictionary<string, int>() { { "four", 4 }, { "two", 2 }, { "one", 1 }, { "three", 3 } };
        var d = dict.OrderBy(e => e.Value);

        foreach (var pair in d)
        {
            Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
        }
        Console.ReadKey();

    }

    internal static string CountElemsLinq<T>(List<T> list)
    where T : notnull
    {
        string s = String.Empty;
        var query = list.GroupBy(e => e).OrderBy(e=>e.Key);
        //var query = from elem in list
        //            group elem by elem into elemGroup
        //            orderby elemGroup.Key ascending
        //            select new { Key = elemGroup.Key, Count = elemGroup.Count() };
        foreach (var q in query)
        {
            s += $"{q.Key,2} | {q.Count(),2}\n";
        }
        return s;
    }

    internal static string CountElems<T>(List<T> list)
        where T : notnull
    {
        string s = String.Empty;
        if (list != null)
        {
            Dictionary<T, int> dict = new Dictionary<T, int>();
            foreach (var item in list)
            {
                if (!dict.ContainsKey(item)) dict.Add(item, 1);
                else dict[item] = ++dict[item];
            }
            foreach (var item in dict)
            {
                s += $"{item.Key,2} | {item.Value,2}\n";
            }
        }
        return s;
    }
}