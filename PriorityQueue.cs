using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoGrafos.DataStructure;

namespace EP
{
    public class PriorityQueue
    {
        List<Node> elements;

        public PriorityQueue()
        {
            elements = new List<Node>();
            
        }
        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < elements.Count;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < elements.Count;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private int GetLeftChild(int elementIndex) => (elements[GetLeftChildIndex(elementIndex)].grade + elements[GetLeftChildIndex(elementIndex)].Nivel);
        private int GetRightChild(int elementIndex) => (elements[GetRightChildIndex(elementIndex)].grade + elements[GetRightChildIndex(elementIndex)].Nivel);
        private int GetParent(int elementIndex) => (elements[GetParentIndex(elementIndex)].grade + elements[GetParentIndex(elementIndex)].Nivel);

        public bool IsEmpty()
        {
            return elements.Count == 0;
        }
        public Node Peek()
        {
            if (IsEmpty())
                throw new IndexOutOfRangeException();

            return elements[0];
        }

        public void Add(Node item)
        {
            elements.Add(item);
            SortUp();
        }

        public void Delete(Node item)
        {
            int i = elements.IndexOf(item);
            int last = elements.Count - 1;

            elements[i] = elements[last];
            elements.RemoveAt(last);
            SortDown();
        }

        public Node Pop()
        {
            if (IsEmpty())
                throw new IndexOutOfRangeException();
            Node item = elements[0];
            Delete(item);
            return item;

            //if (elements.Count > 0)
            //{
            //    T item = elements[0];
            //    Delete(item);
            //    return item;
            //}
            //relook at this - should we just throw exception?
            //return default(T);
        }

        private void SortDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index) < GetLeftChild(index))
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (SumGradeNivel(elements,smallerIndex) >= SumGradeNivel(elements,index))
                {
                    break;
                }

                SwapElements(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void SortUp()
        {
            var index = elements.Count - 1;
            while (!IsRoot(index) && SumGradeNivel(elements,index) < GetParent(index))
            {
                var parentIndex = GetParentIndex(index);
                SwapElements(parentIndex, index);
                index = parentIndex;
            }
        }

        private int SumGradeNivel(List<Node> i, int index)
        {
            return i[index].Nivel + i[index].grade;
        }

        //private void Sort()
        //{
        //    for (int i = elements.Count - 1; i > 0; i--)
        //    {
        //        int parentPosition = (i + 1) / 2 - 1;
        //        parentPosition = parentPosition >= 0 ? parentPosition : 0;

        //        if (elements[parentPosition].CompareTo(elements[i]) > 1)
        //        {
        //            SwapElements(parentPosition, i);
        //        }
        //    }
        //}

        private void SwapElements(int firstIndex, int secondIndex)
        {
            Node tmp = elements[firstIndex];
            elements[firstIndex] = elements[secondIndex];
            elements[secondIndex] = tmp;
        }
    }
}
