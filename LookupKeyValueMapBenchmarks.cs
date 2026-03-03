using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using DSA.DataStructures.Trees;

namespace Lab1
{
    [MemoryDiagnoser]
    [ShortRunJob]
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter]
    public class LookupKeyValueMapBenchmarks
    {
        public enum KeyValueMapType { Dictionary, BST, AVL, RedBlack }

        public List<KeyValuePair<int, int>>? keyValuePairs;
        public List<KeyValuePair<int, int>>? keyValuePairsShuffled;

        public Dictionary<int,int> dictionary = new Dictionary<int, int>();
        public BinarySearchTreeMap<int, int> bst = new BinarySearchTreeMap<int, int>();
        public AVLTreeMap<int, int> avlTree = new AVLTreeMap<int, int>();
        public RedBlackTreeMap<int, int> redBlackTree = new RedBlackTreeMap<int, int>();

        [Params(100, 1_000, 10_000, 100_000, 1_000_000)]
        public int N;

        [Params(true, false)]
        public bool isInOrder;

        [Params(KeyValueMapType.Dictionary, KeyValueMapType.BST, KeyValueMapType.AVL, KeyValueMapType.RedBlack)]
        public KeyValueMapType keyValueMapType;

        [GlobalSetup]
        public void Setup()
        {
            keyValuePairs = new List<KeyValuePair<int, int>>();
            keyValuePairsShuffled = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < N; i++)
            {
                keyValuePairs.Add(new KeyValuePair<int, int>(i, i * 10));
            }

            if (!isInOrder)
            {
                keyValuePairs.Shuffle();
            }

            foreach(var kvp in keyValuePairs)
            {
                keyValuePairsShuffled.Add(kvp);
            }
            keyValuePairsShuffled.Shuffle();

            // Create the data structure before doing lookup
            switch (keyValueMapType)
            {
                case KeyValueMapType.Dictionary:
                    InsertIntoDictionary();
                    break;
                case KeyValueMapType.BST:
                    InsertIntoBST();
                    break;
                case KeyValueMapType.AVL:
                    InsertIntoAVLTree();
                    break;
                case KeyValueMapType.RedBlack:
                    InsertIntoRedBlackTree();
                    break;
            }
        }


        [Benchmark]
        public int Lookup()
        {
            int value=0; 

            foreach(var kvp in keyValuePairsShuffled)
            {
                switch (keyValueMapType)
                {
                    case KeyValueMapType.Dictionary:
                        // accumulate XOR operation across 
                        // all iterations so every iteration must execute
                        // and ensure the JIT compiler doesn't skip any iterations
                        value ^= dictionary[kvp.Key];  
                        break;
                    case KeyValueMapType.BST:
                        value ^= bst[kvp.Key];
                        break;
                    case KeyValueMapType.AVL:
                        value ^= avlTree[kvp.Key];
                        break;
                    case KeyValueMapType.RedBlack:
                        value ^= redBlackTree[kvp.Key];
                        break;
                }
            }

            return value;
        }


        public void InsertIntoDictionary()
        {
            foreach (var kvp in keyValuePairs)
            {
                dictionary.Add(kvp.Key, kvp.Value);
            }
        }

        public void InsertIntoBST()
        {
            foreach (var kvp in keyValuePairs)
            {
                bst.Add(kvp.Key, kvp.Value);
            }
        }

        public void InsertIntoAVLTree()
        {
            foreach (var kvp in keyValuePairs)
            {
                avlTree.Add(kvp.Key, kvp.Value);
            }
        }

        public void InsertIntoRedBlackTree()
        {
            foreach (var kvp in keyValuePairs)
            {
                redBlackTree.Add(kvp.Key, kvp.Value);
            }
        }

    }
}

