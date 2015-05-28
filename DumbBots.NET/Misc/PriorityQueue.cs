using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Misc
{
    /// <summary>
    /// This class implements a priority queue (or heap).
    /// It is a "semi-sorted" list which has the smallest element at the front.
    /// </summary>
    /// <remarks>
    /// Based on BenDi's Priority Queue code at CodeProject.
    /// (http://www.codeproject.com/csharp/priorityqueue.asp)
    /// Also based on Vincente's Priority Queue code found in the Jad Engine
    /// (http://www.codeplex.com/JADENGINE)
    /// </remarks>
    internal class PriorityQueue<T> : IPriorityQueue<T>
    {
        protected List<T> _heapList;
        protected IComparer<T> _comparer;

        public PriorityQueue()
            : this(System.Collections.Generic.Comparer<T>.Default)
        { }

        public PriorityQueue(IComparer<T> comparer)
        {
            _heapList = new List<T>();
            _comparer = comparer;
        }

        public PriorityQueue(int Capacity)
            : this(System.Collections.Generic.Comparer<T>.Default, Capacity)
        { }

        public PriorityQueue(IComparer<T> comparer, int Capacity)
        {
            _heapList = new List<T>();
            _comparer = comparer;
            _heapList.Capacity = Capacity;
        }

        protected virtual void SwitchElements(int i, int j)
        {
            T h = _heapList[i];
            _heapList[i] = _heapList[j];
            _heapList[j] = h;
        }

        protected virtual int OnCompare(int i, int j)
        {
            return _comparer.Compare(_heapList[i], _heapList[j]);
        }

        #region IPriorityQueue<T> Members

        public virtual int Push(T element)
        {
            int p = _heapList.Count, p2;
            _heapList.Add(element); //E[p] = element
            do
            {
                if (p == 0) //Element then has to be only element in list
                {
                    break;
                }

                p2 = (p - 1) / 2;
                if (OnCompare(p, p2) < 0) //E[p] less than E[p2]
                {
                    SwitchElements(p, p2); //Order them
                    p = p2;
                }
                else
                {
                    break; //Compared elements in order
                }
            } while (true);
            return p;
        }

        public virtual T Pop()
        {
            T result = _heapList[0]; //First element
            int p = 0, p1, p2, pn;

            _heapList[0] = _heapList[_heapList.Count - 1]; //First element is now last element
            _heapList.RemoveAt(_heapList.Count - 1); //Remove last element
            do
            {
                pn = p;
                p1 = 2 * p + 1;
                p2 = 2 * p + 2;

                if (_heapList.Count > p1 && OnCompare(p, p1) > 0) //Valid element AND E[p] greater than E[2p+1]
                {
                    p = p1;
                }

                if (_heapList.Count > p2 && OnCompare(p, p2) > 0) ////Valid element AND E[p] greater than E[2p+2]
                {
                    p = p2;
                }

                if (p == pn)
                {
                    break;
                }
                SwitchElements(p, pn); //Swop E[p] and E[pn]
            } while (true);
            return result;
        }

        public virtual T Peek()
        {
            if (_heapList.Count > 0)
            {
                return _heapList[0];
            }
            return default(T);
        }

        public virtual void Update(int i)
        {
            int p = i, pn;
            int p1, p2;
            do
            {
                if (p == 0) //No elements "under" it to look at
                {
                    break;
                }

                p2 = (p - 1) / 2;
                if (OnCompare(p, p2) < 0) //E[p] less than E[(p-1)/2]
                {
                    SwitchElements(p, p2);
                    p = p2;
                }
                else
                {
                    break;
                }
            } while (true);
            if (p < i)
            {
                return;
            }
            do
            {
                pn = p;
                p1 = 2 * p + 1;
                p2 = 2 * p + 2;
                if (_heapList.Count > p1 && OnCompare(p, p1) > 0) //Valid element AND E[p] greater than E[2p+1]
                {
                    p = p1;
                }
                if (_heapList.Count > p2 && OnCompare(p, p2) > 0) //Valid element AND E[p] greater than E[2p+2]
                {
                    p = p2;
                }

                if (p == pn)
                {
                    break;
                }
                SwitchElements(p, pn);
            } while (true);
        }

        #endregion IPriorityQueue<T> Members

        #region ICollection<T> Members

        public void CopyTo(T[] array, int index)
        {
            _heapList.CopyTo(array, index);
        }

        public int Count
        {
            get { return _heapList.Count; }
        }

        #endregion ICollection<T> Members

        #region IEnumerable<T> Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _heapList.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _heapList.GetEnumerator();
        }

        #endregion IEnumerable<T> Members

        #region IList<T> Members

        public void Add(T value)
        {
            Push(value);
        }

        public void Clear()
        {
            _heapList.Clear();
        }

        public bool Contains(T value)
        {
            return _heapList.Contains(value);
        }

        public int IndexOf(T value)
        {
            return _heapList.IndexOf(value);
        }

        public void Insert(int index, T value)
        {
            throw new NotSupportedException();
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T value)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public T this[int index]
        {
            get { return _heapList[index]; }
            set
            {
                _heapList[index] = value;
                Update(index);
            }
        }

        #endregion IList<T> Members
    }
}