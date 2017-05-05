using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatMgmt
{
    public class PerfCounter
    {
        SortedDictionary<DateTime, long> queue;
        private static readonly int MAX_SIZE = 10000;

        public PerfCounter()
        {
            queue = new SortedDictionary<DateTime, long>();
        }

        public void Add(long value)
        {
            var time = DateTime.Now;
            if (!queue.ContainsKey(time))
            {
                queue.Add(DateTime.Now, value);
                if (queue.Count > MAX_SIZE)
                {
                    queue.Remove(queue.First().Key);
                }
            }
        }

        public long LastValue()
        {
            return (queue.Count > 0) ? queue.Last().Value : 0;
        }
    }
}
