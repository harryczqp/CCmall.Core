using System;
using System.Collections.Generic;
using ConsoleTest.Entity;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Linq;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var actual = 0;
            using (var data = new BlockingCollection<Slice>())
            {
                Parallel.For(0, 3600, item =>
            {
                var num = random.Next(1, 20);
                Interlocked.Add(ref actual, num);
                for (int i = 0; i < num; i++)
                {
                    data.Add(new Slice { Count = Interlocked.Increment(ref item) });
                }
            });
                data.CompleteAdding();
            }
        }
    }
}
