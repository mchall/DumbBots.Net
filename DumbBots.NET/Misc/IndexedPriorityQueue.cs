using System;
using System.Collections.Generic;

namespace Misc
{
    /// <summary>
    /// This class implements a indexed priority queue (or heap). 
    /// It is a "semi-sorted" list which has the smallest element at the front.
    /// Instead of ordering itself, it orders an index list
    /// </summary>
    /// <remarks>
    /// Based on Vincente's Indexed Priority Queue code found in the Jad Engine
    /// (http://www.codeplex.com/JADENGINE) 
    /// </remarks>
    class IndexedPriorityQueue<T> : PriorityQueue<int>
    {
        protected List<T> _indexedList;
        protected IComparer<T> _indexComparer;
        protected List<int> _reverseList;

        public IndexedPriorityQueue()
            : this(System.Collections.Generic.Comparer<T>.Default)
        { }

        public IndexedPriorityQueue(IComparer<T> comparer)
        {
            _indexedList = new List<T>();
            _indexComparer = comparer;
        }

        public IndexedPriorityQueue(List<T> indexedElements)
            : this(System.Collections.Generic.Comparer<T>.Default)
        {
            _indexedList = indexedElements;

            //Create and initialize the reversed indexes list
            _reverseList = new List<int>(indexedElements.Count);
            for (int i = 0; i < indexedElements.Count; i++)
            {
                _reverseList.Add(-1);
            }

            this._heapList.Capacity = indexedElements.Count;
        }

        public IndexedPriorityQueue(IComparer<T> comparer, List<T> indexedElements)
            : this(indexedElements)
        {
            _indexComparer = comparer;
        }

        public List<T> IndexedList
        {
            get { return _indexedList; }
            set
            {
                _indexedList = value;
                _reverseList = new List<int>(_indexedList.Count);
                for (int i = 0; i < _indexedList.Count; i++)
                {
                    _reverseList.Add(-1);
                }
            }
        }

        public override int Push(int element)
        {
            int p = _heapList.Count, p2;
            _heapList.Add(element);
            _reverseList[element] = p;

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

        public override int Pop()
        {
            int result = _heapList[0];
            int p = 0, p1, p2, pn;
            _reverseList[result] = -1;

            _heapList[0] = _heapList[_heapList.Count - 1]; //First element is now last element
            _heapList.RemoveAt(_heapList.Count - 1); //Remove last element

            if (_heapList.Count != 0)
            {
                _reverseList[_heapList[0]] = 0;
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

        protected override void SwitchElements(int i, int j)
        {
            int h;

            h = _reverseList[_heapList[i]];
            _reverseList[_heapList[i]] = _reverseList[_heapList[j]];
            _reverseList[_heapList[j]] = h;

            base.SwitchElements(i, j);
        }

        protected override int OnCompare(int i, int j)
        {
            return _indexComparer.Compare(_indexedList[(Int32)_heapList[i]], _indexedList[(Int32)_heapList[j]]);
        }

        public void ChangePriority(int i)
        {
            this.Update(_reverseList[i]);
        }
    }
}