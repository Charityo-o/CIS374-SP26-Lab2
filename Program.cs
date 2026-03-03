using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lab1;

class Program
{
    static void Main(string[] args)
    {
        // var data = BenchmarkRunner.Run<StringBenchmark>();

        // var data = BenchmarkRunner.Run<InsertKeyValueMapBenchmarks>();
        // var data = BenchmarkRunner.Run<LookupKeyValueMapBenchmarks>();
        // var data = BenchmarkRunner.Run<RemoveKeyValueMapBenchmarks>();

        
        int n = 1000000;
        var heightRandom = new HeightKeyValueMapBenchmarks(n, false);
        double heightBST = heightRandom.HeightOfBST();
        double heightAVL = heightRandom.HeightOfAVLTree();
        double heightRB = heightRandom.HeightOfRedBlackTree();

        System.Console.WriteLine($"Height (n={n})");
        System.Console.WriteLine($"BST: {heightBST}");
        System.Console.WriteLine($"AVL: {heightAVL}");
        System.Console.WriteLine($"Red-Black: {heightRB}");


    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class StringBenchmark
{
    [Params(100, 500, 1000)]
    public int N ;

    [Benchmark(Baseline = true)]
    public string SimpleStringBuilder() {

        string result = "";

        for(int i=0; i< N; i++)
        {
            result += i;
        }

        return result;
    }

    [Benchmark]
    public string BetterStringBuilder() {
        StringBuilder result = new StringBuilder();

        for(int i=0; i< N; i++)
        {
            result.Append(i);
        }

        return result.ToString();
    }



}