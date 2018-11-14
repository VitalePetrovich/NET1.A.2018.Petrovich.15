using System;
using System.Collections;
using System.Collections.Generic;

namespace NET1.A._2018.Petrovich._15
{
    public class CustomQueue<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
    {
        private int defaultCapasity = 4;

        private T[] arr;

        private int head;

        private int tail;

        internal int Version { get; private set; }

        public int Count { get; private set; }

        public bool IsReadOnly { get; set; }

        public CustomQueue()
        {
            this.arr = new T[this.defaultCapasity];
            this.Count = 0;
            this.head = 0;
            this.tail = 0;
            this.Version = 0;
            this.IsReadOnly = false;
        }

        public CustomQueue(int capasity)
        {
            this.arr = new T[capasity];
            this.Count = 0;
            this.head = 0;
            this.tail = 0;
            this.Version = 0;
            this.IsReadOnly = false;
        }

        public CustomQueue(IEnumerable<T> collection)
            : this()
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public void Add(T item)
        {
            if (this.Count == this.arr.Length)
            {
                Array.Resize(ref this.arr, this.arr.Length * 2);
                this.tail = this.Count;
            }

            this.arr[this.tail] = item;
            this.tail = (this.tail + 1) % this.arr.Length;
            this.Count++;
            this.Version++;
        }

        public T Get()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();

            T temp = this.arr[this.head];
            this.arr[this.head] = default(T);
            this.head = (this.head + 1) % this.arr.Length;
            this.Count--;
            this.Version++;

            return temp;
        }

        public T Peek()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();

            return this.arr[this.head];
        }

        public void Clear()
        {
            this.arr = new T[defaultCapasity];

            this.Version++;
            this.Count = 0;
            this.head = 0;
            this.tail = 0;
        }

        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            int numberOfElements = this.Count;
            int currentElement = this.head;

            while (numberOfElements-- > 0)
            {
                if (comparer.Equals(this.arr[currentElement], item))
                    return true;

                currentElement = (currentElement + 1) % this.arr.Length;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length < this.Count)
                throw new InvalidOperationException();

            if (arrayIndex >= array.Length || arrayIndex < 0)
                throw new ArgumentException(nameof(arrayIndex));

            int numberOfElements = this.Count;
            int currentElement = this.head;

            while (numberOfElements-- > 0)
            {
                array[arrayIndex++] = this.arr[currentElement];

                currentElement = (currentElement + 1) % this.arr.Length;
            }
        }

        public CustomQueueEnumerator GetEnumerator()
        {
            return new CustomQueueEnumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public T this[int index] => this.arr[index];

        public struct CustomQueueEnumerator : IEnumerator<T>, IEnumerator
        {
            private CustomQueue<T> queue;

            private int index;

            private int numberOfElements;

            private int initialVersion;

            public CustomQueueEnumerator(CustomQueue<T> queue)
            {
                this.queue = queue;
                this.initialVersion = queue.Version;
                this.index = queue.head - 1;
                this.numberOfElements = queue.Count;
            }

            public T Current => this.queue[index];

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (this.initialVersion != this.queue.Version)
                    throw new InvalidOperationException($"Queue has been modifyed!");

                this.index = (this.index + 1) % this.queue.arr.Length;

                return this.numberOfElements-- > 0;
            }

            public void Reset()
            {
                this.index = -1;
            }
        }
    }

    
}
