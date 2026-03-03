using BenchmarkDotNet.Attributes;
using DSA.DataStructures.Trees;

namespace Lab1
{
	[MemoryDiagnoser]
	[ShortRunJob]
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter]
    public class InsertKeyValueMapBenchmarks
	{
		public List<KeyValuePair<int, int>>? keyValuePairs;

		[Params(100, 1_000, 10_000, 100_000, 1_000_000)]
		public int N;

        [Params(true, false)]
        public bool isInOrder;

        [GlobalSetup]
        public void Setup()
        {
			keyValuePairs = new List<KeyValuePair<int, int>>();

			for(int i=0; i < N; i++)
			{
				keyValuePairs.Add(new KeyValuePair<int, int>(i, i*10)); 
			}

            if(!isInOrder)
            {
                keyValuePairs.Shuffle();
            }
        }

        // [Benchmark]
		public void InsertIntoDictionary( )
		{
			var dictionary = new Dictionary<int, int>();

			foreach(var kvp in keyValuePairs)
			{
				dictionary.Add(kvp.Key, kvp.Value);
			}
		}

        [Benchmark]
        public void InsertIntoBST()
        {
            var bst = new BinarySearchTreeMap<int, int>();

            foreach (var kvp in keyValuePairs)
            {
                bst.Add(kvp.Key, kvp.Value);
            }
        }

        // [Benchmark]
        public void InsertIntoAVLTree()
        {
            var avltree = new AVLTreeMap<int, int>();

            foreach (var kvp in keyValuePairs)
            {
                avltree.Add(kvp.Key, kvp.Value);
            }
        }

        // [Benchmark]
        public void InsertIntoRedBlackTree()
        {
            var rbtree = new RedBlackTreeMap<int, int>();

            foreach (var kvp in keyValuePairs)
            {
                rbtree.Add(kvp.Key, kvp.Value);
            }
        }

    }
}

