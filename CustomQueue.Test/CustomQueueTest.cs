using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET1.A._2018.Petrovich._15;

namespace CustomQueue.Test
{
    using System.Diagnostics;

    using NUnit.Framework;

    [TestFixture]
    public class CustomQueueTest
    {
        [Test]
        public void GeneralFunctionality()
        {
            CustomQueue<int> customQueue = new CustomQueue<int>() {4, 5, 0};
            Debug.WriteLine($"Number of elements before adding elements: {customQueue.Count}");
            customQueue.Add(2);
            customQueue.Get();
            customQueue.Add(3);
            Debug.WriteLine($"Get element from head of queue: {customQueue.Get()}");
            Debug.WriteLine($"Peek header element: {customQueue.Peek()}");
            Debug.WriteLine($"Peek header element again: {customQueue.Peek()}");
            Debug.WriteLine($"Is queue contains '5': {customQueue.Contains(5)}");
            Debug.WriteLine($"Is queue contains '4': {customQueue.Contains(4)}");
            Debug.WriteLine("====================================");
            foreach (var item in customQueue)
            {
                Debug.Write(item + " ");
            }
            Debug.WriteLine("\n====================================");
            customQueue.Clear();
        }
    }
}
