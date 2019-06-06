using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using ProjetoGrafos.DataStructure;

namespace EP
{
    /// <summary>
    /// EPAgent - searchs solution for the eight puzzle problem
    /// </summary>
    public class EightPuzzle : Graph
    {
        private int[] initState;
        private int[] target;
        private HashSet<string> estados;

        /// <summary>
        /// Creating the agent and setting the initialstate plus target
        /// </summary>
        /// <param name="InitialState"></param>
        public EightPuzzle(int[] InitialState, int[] Target)
        {
            initState = InitialState;
            target = Target;

        }

        /// <summary>
        /// Accessor for the solution
        /// </summary>
        public int[] GetSolution()
        {
            return FindSolution();
        }

        /// <summary>
        /// Função principal de busca
        /// </summary>
        /// <returns></returns>
        private int[] FindSolution()
        {
            estados = new HashSet<string>();
            PriorityQueue pqueue = new PriorityQueue();
            List<Node> temp = new List<Node>();
            Node no = new Node("@", initState, 0);
            pqueue.Add(no);
            while (!pqueue.IsEmpty())
            {
                no = pqueue.Pop();
                if (TargetFound(no))
                    return BuildAnswer(no);
                temp = GetSucessors(no);
                foreach (Node n in temp)
                {
                    pqueue.Add(n);
                }
            }
            return null;
        }

        private List<Node> GetSucessors(Node n)
        {
            int[] estado = (int[])n.Info;
            List<int> lista = new List<int>();
            foreach (int i in estado)
                lista.Add(i);
            List<Node> nos = new List<Node>();
            double tam = Math.Sqrt(lista.Count);
            int index = lista.IndexOf(0);//,num = 0;

            if ((index + 1) > tam)
                createState(nos, n, index, (index - Convert.ToInt32(tam)));
            if ((index + 1) % tam != 0)
                createState(nos, n, index, index + 1);
            if ((index + 1) <= lista.Count - tam)
                createState(nos, n, index, (index + Convert.ToInt32(tam)));
            if ((index + 1) % tam != 1)
                createState(nos, n, index, index - 1);

            //switch (num)
            //{
            //    case 0:
            //        if(checkState(n,index + 1))
            //            nos.Add(createState(n,index,index+1));
            //        if (checkState(n, index - 1))
            //            nos.Add(createState(n, index, index - 1));
            //        if (checkState(n, index - Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, (index - Convert.ToInt32(tam))));
            //        if (checkState(n, index + Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, (index + Convert.ToInt32(tam))));
            //        break;
            //    case 1:
            //        if (checkState(n, index + 1))
            //            nos.Add(createState(n,index,index+1));
            //        if (checkState(n, index - 1))
            //            nos.Add(createState(n, index, index - 1));
            //        if (checkState(n, index + Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index + Convert.ToInt32(tam)));
            //        break;
            //    case 2:
            //        if (checkState(n, index - 1))
            //            nos.Add(createState(n, index, index - 1));
            //        if (checkState(n, index - Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index - Convert.ToInt32(tam)));
            //        if (checkState(n, index + Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index + Convert.ToInt32(tam)));
            //        break;
            //    case 3:
            //        if (checkState(n, index - 1))
            //            nos.Add(createState(n, index, index - 1));
            //        if (checkState(n, index + Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index + Convert.ToInt32(tam)));
            //        break;
            //    case 4:
            //        if (checkState(n, index + 1))
            //            nos.Add(createState(n, index, index + 1));
            //        if (checkState(n, index - 1))
            //            nos.Add(createState(n, index, index - 1));
            //        if (checkState(n, index - Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index - Convert.ToInt32(tam)));
            //        break;
            //    case 6:
            //        if (checkState(n, index - 1))
            //            nos.Add(createState(n, index, index - 1));
            //        if (checkState(n, index - Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index - Convert.ToInt32(tam)));
            //        break;
            //    case 8:
            //        if (checkState(n, index + 1))
            //            nos.Add(createState(n, index, index + 1));
            //        if (checkState(n, index - Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index - Convert.ToInt32(tam)));
            //        if (checkState(n, index + Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index + Convert.ToInt32(tam)));
            //        break;
            //    case 9:
            //        if (checkState(n, index + 1))
            //            nos.Add(createState(n, index, index + 1));
            //        if (checkState(n, index + Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index + Convert.ToInt32(tam)));
            //        break;
            //    case 12:
            //        if (checkState(n, index + 1))
            //            nos.Add(createState(n, index, index + 1));
            //        if (checkState(n, index - Convert.ToInt32(tam)))
            //            nos.Add(createState(n, index, index - Convert.ToInt32(tam)));
            //        break;
            //}
            return nos;

        }

        private bool checkState(Node n, int index)
        {

            if (n.parent == null)
                return true;
            else if (n.parent.Name == "@")
            {
                return true;
            }
            if (int.Parse(n.parent.Name) != ((int[])n.Info)[index])
                return true;
            return false;
        }

        private void createState(List<Node> nos, Node n, int indexEmpty, int indexChange)
        {
            int[] newState = (int[])((int[])n.Info).Clone();
            newState[indexEmpty] = newState[indexChange];
            newState[indexChange] = 0;
            /*for (int i = 0; i < ((int[])n.Info).Length; i++)
            {
                if (i == indexEmpty)
                {
                    newState[indexEmpty] = ((int[])n.Info)[indexChange];
                    newState[indexChange] = ((int[])n.Info)[indexEmpty];
                }
                else if (i != indexChange)
                {
                    newState[i] = ((int[])n.Info)[i];
                }
            }*/
            string id = String.Join(",", newState);
            if (!estados.Contains(id))
            {
                Node resp = new Node(((int[])n.Info)[indexChange].ToString(), newState, 0);
                //n.Edges.Add(new Edge(n, resp));
                resp.parent = n;
                resp.grade = ManhattanDistance((int[])n.Info);
                estados.Add(id);
                nos.Add(resp);
            }
        }

        private int[] BuildAnswer(Node n)
        {
            List<int> resp = new List<int>();
            while (n.Name != "@")
            {
                resp.Add(int.Parse(n.Name));
                n = n.parent;
            }
            resp.Reverse();
            return resp.ToArray();
        }

        private bool TargetFound(Node n)
        {
            int count = 0;
            foreach (int i in (int[])n.Info)
            {
                if (i != target[count])
                    return false;
                count++;
            }
            return true;
        }

        private int ManhattanDistance(int[] estado)
        {
            int tam = Convert.ToInt32(Math.Sqrt(estado.Length)), count = 0, distance = 0, x1, x2, y1, y2;
            foreach (int a in estado)
            {
                if (a != 0)
                {
                    x1 = a / tam;
                    x2 = count / tam;
                    y1 = a % tam;
                    y2 = count % tam;
                    distance += Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
                }
                count++;
            }
            return distance;
        }
    }
}

