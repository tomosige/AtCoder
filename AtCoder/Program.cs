using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using static System.Console;
using static System.Math;
using static AtCoder.Program;

namespace AtCoder
{
    class Program
    {
        static void Main(string[] args)
        {
            new ABC135().SolveA(new ConsoleInput(Console.In, ' '));
        }
    }

    public class ConsoleInput
    {
        private readonly System.IO.TextReader _stream;
        private char _separator = ' ';
        private Queue<string> inputStream;
        public ConsoleInput(System.IO.TextReader stream, char separator = ' ')
        {
            this._separator = separator;
            this._stream = stream;
            inputStream = new Queue<string>();
        }
        public string Read
        {
            get
            {
                if (inputStream.Count != 0) return inputStream.Dequeue();
                string[] tmp = _stream.ReadLine().Split(_separator);
                for (int i = 0; i < tmp.Length; ++i)
                    inputStream.Enqueue(tmp[i]);
                return inputStream.Dequeue();
            }
        }
        public string ReadLine { get { return _stream.ReadLine(); } }
        public int ReadInt { get { return int.Parse(Read); } }
        public long ReadLong { get { return long.Parse(Read); } }
        public double ReadDouble { get { return double.Parse(Read); } }
        public string[] ReadStrArray(long N) { var ret = new string[N]; for (long i = 0; i < N; ++i) ret[i] = Read; return ret; }
        public int[] ReadIntArray(long N) { var ret = new int[N]; for (long i = 0; i < N; ++i) ret[i] = ReadInt; return ret; }
        public long[] ReadLongArray(long N) { var ret = new long[N]; for (long i = 0; i < N; ++i) ret[i] = ReadLong; return ret; }

    }

    public static class AtCoderLib
    {
        // ユークリッドの互除法 
        public static long Gcd(long a, long b)
        {
            if (a < b)
                // 引数を入替えて自分を呼び出す
                return Gcd(b, a);
            while (b != 0)
            {
                var remainder = a % b;
                a = b;
                b = remainder;
            }
            return a;
        }

        // 最小公倍数
        public static long Lcm(long a, long b)
        {
            return a * b / Gcd(a, b);
        }

        //10^9- 7 の剰余
        public static int Add(int a, int b)
        {
            var MOD = Pow(10, 9) - 7;
            return (int)((a + b) % MOD);
        }
    }

    public class PriorityQueue<T> where T : IComparable
    {
        private IComparer<T> _comparer = null;
        private int _type = 0;

        private T[] _heap;
        private int _sz = 0;

        private int _count = 0;

        /// <summary>
        /// Priority Queue with custom comparer
        /// </summary>
        public PriorityQueue(int maxSize, IComparer<T> comparer)
        {
            _heap = new T[maxSize];
            _comparer = comparer;
        }

        /// <summary>
        /// Priority queue
        /// </summary>
        /// <param name="maxSize">max size</param>
        /// <param name="type">0: asc, 1:desc</param>
        public PriorityQueue(int maxSize, int type = 0)
        {
            _heap = new T[maxSize];
            _type = type;
        }

        private int Compare(T x, T y)
        {
            if (_comparer != null) return _comparer.Compare(x, y);
            return _type == 0 ? x.CompareTo(y) : y.CompareTo(x);
        }

        public void Push(T x)
        {
            _count++;

            //node number
            var i = _sz++;

            while (i > 0)
            {
                //parent node number
                var p = (i - 1) / 2;

                if (Compare(_heap[p], x) <= 0) break;

                _heap[i] = _heap[p];
                i = p;
            }

            _heap[i] = x;
        }

        public T Pop()
        {
            _count--;

            T ret = _heap[0];
            T x = _heap[--_sz];

            int i = 0;
            while (i * 2 + 1 < _sz)
            {
                //children
                int a = i * 2 + 1;
                int b = i * 2 + 2;

                if (b < _sz && Compare(_heap[b], _heap[a]) < 0) a = b;

                if (Compare(_heap[a], x) >= 0) break;

                _heap[i] = _heap[a];
                i = a;
            }

            _heap[i] = x;

            return ret;
        }

        public int Count()
        {
            return _count;
        }

        public T Peek()
        {
            return _heap[0];
        }

        public bool Contains(T x)
        {
            for (int i = 0; i < _sz; i++) if (x.Equals(_heap[i])) return true;
            return false;
        }

        public void Clear()
        {
            while (this.Count() > 0) this.Pop();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var ret = new List<T>();

            while (this.Count() > 0)
            {
                ret.Add(this.Pop());
            }

            foreach (var r in ret)
            {
                this.Push(r);
                yield return r;
            }
        }

        public T[] ToArray()
        {
            T[] array = new T[_sz];
            int i = 0;

            foreach (var r in this)
            {
                array[i++] = r;
            }

            return array;
        }
    }

    public class UnionFind
    {
        List<int> rank { get; set; }
        List<int> size { get; set; }
        List<int> parentId { get; set; }

        public UnionFind(int size)
        {
            this.rank = Enumerable.Repeat(0, size).ToList();
            this.size = Enumerable.Repeat(1, size).ToList();
            this.parentId = Enumerable.Range(0, size).ToList();
        }

        public bool Same(int x, int y)
        {
            return Root(x) == Root(y);
        }

        public int Root(int x)
        {
            if (x != this.parentId[x])
            {
                // 経路中のノードのparentを更新しておく
                this.parentId[x] = Root(this.parentId[x]);
            }

            return this.parentId[x];
        }

        public void Merge(int x, int y)
        {
            x = Root(x);
            y = Root(y);

            if (x == y) return;

            if (this.rank[y] < this.rank[x])
            {
                this.parentId[y] = x;
                this.size[x] += this.size[y];
            }
            else
            {
                this.parentId[x] = y;
                this.size[y] += this.size[x];

                if (this.rank[x] == this.rank[y])
                {
                    this.rank[y]++;
                }
            }
        }

        // xと同じ親を持つノードの数
        public int Size(int x)
        {
            x = Root(x);

            return this.size[x];
        }
    }
}