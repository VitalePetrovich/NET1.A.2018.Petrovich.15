using System;
using System.Collections;
using System.Collections.Generic;

namespace NET1.A._2018.Petrovich._15
{
    /// <summary>
    /// Custom Queue class.
    /// </summary>
    /// <typeparam name="T">Type of elements in Queue.</typeparam>
    public class CustomQueue<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
    {
        private int defaultCapasity = 4;

        private T[] arr;

        private int head;

        private int tail;

        private int version;

        public int Count { get; private set; }

        public bool IsReadOnly { get; set; }

        #region ctors
        

        public CustomQueue()
        {
            this.arr = new T[this.defaultCapasity];
            this.Count = 0;
            this.head = 0;
            this.tail = 0;
            this.version = 0;
            this.IsReadOnly = false;
        }

        public CustomQueue(int capasity)
        {
            this.arr = new T[capasity];
            this.Count = 0;
            this.head = 0;
            this.tail = 0;
            this.version = 0;
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

        #endregion

        /// <summary>
        /// Add element to end of queue.
        /// </summary>
        /// <param name="item">Element.</param>
        public void Add(T item)
        {
            if (this.Count == this.arr.Length)
            {
                T[] newArray = new T[this.arr.Length * 2];
                if (this.head == this.tail)
                {
                    Array.Copy(this.arr, this.head, newArray, 0, Count - this.head);
                    Array.Copy(this.arr, 0, newArray, Count - this.head, this.tail);
                }
                else
                {
                    Array.Copy(this.arr, newArray, Count);
                }

                this.arr = newArray;
                this.head = 0;
                this.tail = this.Count;
            }

            this.arr[this.tail] = item;
            this.tail = (this.tail + 1) % this.arr.Length;
            this.Count++;
            this.version++;
        }

        /// <summary>
        /// Dequeue elements from queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws if queue is empty.</exception>
        /// <returns>Upper element of queue.</returns>
        public T Get()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();

            T temp = this.arr[this.head];
            this.arr[this.head] = default(T);
            this.head = (this.head + 1) % this.arr.Length;
            this.Count--;
            this.version++;

            return temp;
        }

        /// <summary>
        /// Look upon upper element.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws if queue is empty.</exception>
        /// <returns>Upper element of queue.</returns>
        public T Peek()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();

            return this.arr[this.head];
        }

        /// <summary>
        /// Clear queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws if queue is empty.</exception>
        public void Clear()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();

            this.arr = new T[defaultCapasity];

            this.version++;
            this.Count = 0;
            this.head = 0;
            this.tail = 0;
        }

        /// <summary>
        /// Look for element in queue.
        /// </summary>
        /// <param name="item">Element.</param>
        /// <returns>TRUE, if queue containt item.</returns>
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

        /// <summary>
        /// Copy elements of queue to array.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="arrayIndex">Insertin index in destination array.</param>
        /// <exception cref="InvalidOperationException">Throws if array length is less than queue lenght.</exception>
        /// <exception cref="ArgumentException">arrayIndex is invalid.</exception>
        /// <exception cref="ArgumentNullException">Ref to destination array is null.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

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

        /// <summary>
        /// Return enumerator of queue.
        /// </summary>
        /// <returns>Enumerator.</returns>
        public CustomQueueEnumerator GetEnumerator()
        {
            return new CustomQueueEnumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private T this[int index] => this.arr[index];

        /// <summary>
        /// Struct of emunerator.
        /// </summary>
        public struct CustomQueueEnumerator : IEnumerator<T>, IEnumerator
        {
            private CustomQueue<T> queue;

            private int index;

            private int numberOfElements;

            private int initialVersion;

            public CustomQueueEnumerator(CustomQueue<T> queue)
            {
                this.queue = queue;
                this.initialVersion = queue.version;
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
                if (this.initialVersion != this.queue.version)
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
