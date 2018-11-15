using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET1.A._2018.Petrovich._15;

namespace CustomQueue.Test
{
    using System.Collections;
    using System.Diagnostics;

    using NUnit.Framework;

    class Point : IEquatable<Point>
    {
        private int x, y;

        public Point(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Point other)
        {
            return (this.x == other.x && this.y == other.y);
        }
    }

    [TestFixture]
    public class CustomQueueTest
    {
        [Test]
        public void GeneralFunctionalityBCLTypesInt()
        {
            CustomQueue<int> customQueue = new CustomQueue<int>();
            
            customQueue.Add(5);
            customQueue.Add(15);
            Assert.AreEqual(customQueue, new[] {5, 15});

            customQueue.Get();
            Assert.AreEqual(customQueue, new[] {15});

            Assert.AreEqual(customQueue.Peek(), 15);
            Assert.AreEqual(customQueue.Count, 1);
            Assert.IsTrue(customQueue.Contains(15));
            Assert.IsFalse(customQueue.Contains(5));
        }

        [Test]
        public void GeneralFunctionalityWithArrayList()
        {
            CustomQueue<ArrayList> customQueue = new CustomQueue<ArrayList>();

            customQueue.Add(new ArrayList() {2});
            customQueue.Add(new ArrayList() {"Hello"});
            Assert.AreEqual(customQueue, new[] {new ArrayList() {2}, new ArrayList() {"Hello"}});

            customQueue.Get();
            Assert.AreEqual(customQueue, new[] {new ArrayList() { "Hello" }});

            Assert.AreEqual(customQueue.Peek(), new ArrayList() { "Hello" } );
            Assert.AreEqual(customQueue.Count, 1);
            Assert.IsFalse(customQueue.Contains(new ArrayList() { "Hello" }));
            Assert.IsFalse(customQueue.Contains(new ArrayList() { 2 }));
        }

        [Test]
        public void GeneralFunctionalityWithPointEquatable()
        {
            CustomQueue<Point> customQueue = new CustomQueue<Point>();

            customQueue.Add(new Point(2, 4));
            customQueue.Add(new Point(1, 8));
            Assert.AreEqual(customQueue, new[] { new Point(2, 4), new Point(1, 8) });

            customQueue.Get();
            Assert.AreEqual(customQueue, new[] { new Point(1, 8) });

            Assert.AreEqual(customQueue.Peek(), new Point(1, 8));
            Assert.AreEqual(customQueue.Count, 1);
            Assert.IsTrue(customQueue.Contains(new Point(1, 8)));
            Assert.IsFalse(customQueue.Contains(new Point(2, 4)));
        }
    }
}
