using Graphs.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Graphs
{
    class Program
    {
        static void Main()
        {
            //FileGraph();

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

            //ConsoleGraph();

            //Task_IA();

            //Task_IA_2();

            //Task_IB();

            //Task_II_9();

            //Task_II_27();

            //Task_II_Victoria();

            Task_III_Prim();

            Console.WriteLine();
        }

        static void Task_III_Prim()
        {
            Graph graph = new Graph();

            graph.AddVertices("V1", "V2", "V3", "V4", "V5");
            
            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V1", "V2", weight: 15);
            graph.AddEdgeDict("V3", "V2", weight: 75);
            graph.AddEdgeDict("V4", "V1", weight: 85);

            graph.AddEdgeDict("V5", "V3", weight: 25);


            graph.AddVertices("V6", "V7", "V8");


            graph.AddEdgeDict("V4", "V6", weight: 35);
            graph.AddEdgeDict("V6", "V1", weight: 65);

            graph.AddEdgeDict("V4", "V7", weight: 45);
            graph.AddEdgeDict("V7", "V3", weight: 20);

            graph.AddEdgeDict("V5", "V8", weight: 33);
            graph.AddEdgeDict("V8", "V1", weight: 44);


            graph.PrintVertices();
            graph.PrintListEdges();

            // Работает только при последовательном ходе вершин, т.е. при V0, V1, ..., Vn к примеру.
            // В вызове указывается сдвиг вершин, т.е. кол-во вершин до нулевой(V0)
            graph.PrintMatrix(1);

            Console.WriteLine("=======================================");
            Console.WriteLine("=======================================");
            
            graph.PrimStart("V1");
            //graph.PrimStartRandom();
        }

        static void Task_II_27()
        {
            Graph graph = new Graph();

            graph.AddVertex("V1");
            graph.AddVertex("V2");
            graph.AddVertex("V3");
            graph.AddVertex("V4");

            graph.AddVertex("V5");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V1", "V2");
            graph.AddEdgeDict("V3", "V2");
            graph.AddEdgeDict("V4", "V1");

            graph.AddEdgeDict("V5", "V3");


            graph.AddVertex("V6");
            graph.AddVertex("V7");
            graph.AddVertex("V8");

            graph.AddEdgeDict("V4", "V6");
            graph.AddEdgeDict("V6", "V1");

            graph.AddEdgeDict("V4", "V7");
            graph.AddEdgeDict("V7", "V3");

            graph.AddEdgeDict("V5", "V8");
            graph.AddEdgeDict("V8", "V1");


            graph.PrintVertices();
            graph.PrintListEdges();

            //graph.StartDFS("V4", "V5");

            graph.StartBFSForTask_II_27();
        }

        static void Task_II_9()
        {
            Graph graph = new Graph();

            graph.AddVertex("V1");
            graph.AddVertex("V2");
            graph.AddVertex("V3");
            graph.AddVertex("V4");

            graph.AddVertex("V5");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V1", "V2");
            graph.AddEdgeDict("V3", "V2");
            graph.AddEdgeDict("V4", "V1");

            graph.AddEdgeDict("V5", "V3");

            graph.AddVertex("V6");
            graph.AddVertex("V7");
            graph.AddVertex("V8");

            graph.AddEdgeDict("V4", "V6");
            graph.AddEdgeDict("V6", "V1");

            graph.AddEdgeDict("V4", "V7");
            graph.AddEdgeDict("V7", "V3");

            graph.AddEdgeDict("V5", "V8");
            graph.AddEdgeDict("V8", "V1");

            graph.PrintVertices();
            graph.PrintListEdges();

            //graph.DFS_Task_Solve();

            //graph.StartDFS("V4", "V5");

            List<string> solvedPath = graph.TaskSolve_II_1("V4", "V5", new[] { "V6", "V7", "V8"});

            Console.Write("Path is : ");
            foreach(var item in solvedPath)
            {
                Console.Write(item + " ");
            }
        }

        static void Task_II_Victoria()
        {
            Graph graph = new Graph();

            //graph.AddVertex("V0");

            graph.AddVertex("V1");
            graph.AddVertex("V2");
            graph.AddVertex("V3");
            graph.AddVertex("V4");

            graph.AddVertex("V5");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V1", "V2");
            graph.AddEdgeDict("V3", "V2");
            graph.AddEdgeDict("V4", "V1");

            graph.AddEdgeDict("V5", "V3");


            graph.AddVertex("V6");
            graph.AddVertex("V7");
            graph.AddVertex("V8");

            graph.AddVertex("V9");

            graph.AddEdgeDict("V6", "V7");
            graph.AddEdgeDict("V7", "V8");
            graph.AddEdgeDict("V6", "V8");


            graph.PrintVertices();
            graph.PrintListEdges();
            graph.PrintVertices();
            graph.PrintListEdges();

            //graph.DFS_Task_Solve();

            //graph.StartDFS("V4", "V5");
            //Console.WriteLine("Существует ли ориентированная дуга из вершины {0} в {1} : {2}", graph.Task_IA());
            //Console.WriteLine("Существует ли ориентированная дуга из вершины V2 в V3 : {0}", graph.Task_IA("V2", "V3"));

        }

        // Определить, существует ли вершина, в которую есть дуга из вершины u, но нет из v. Вывести такую вершину.
        static void Task_IA()
        {
            Graph graph = new Graph(@"..\..\Graph_la.txt");
            graph.AddEdgeDict("V3", "V1", true);
            graph.PrintVertices();
            graph.PrintListEdges();

            graph.Task_IA();

            //Console.WriteLine("Существует ли ориентированная дуга из вершины {0} в {1} : {2}", graph.Task_IA());
            //Console.WriteLine("Существует ли ориентированная дуга из вершины V2 в V3 : {0}", graph.Task_IA("V2", "V3"));

        }

        // Для каждой вершины графа вывести её степень.
        static void Task_IA_2()
        {
            Graph graph = new Graph();

            graph.AddVertex("V0");
            graph.AddVertex("V1");
            graph.AddVertex("V2");
            graph.AddVertex("V3");
            graph.AddVertex("V4");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V1", "V2");
            graph.AddEdgeDict("V3", "V2");
            graph.AddEdgeDict("V4", "V1", true);

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.Task_IA_2();
            Console.WriteLine();
            graph.Task_IA_2(true);
        }

        static void Task_IB()
        {
            Graph graph = new Graph();

            graph.AddVertex("V1");
            graph.AddVertex("V2");
            graph.AddVertex("V3");
            graph.AddVertex("V4");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V1", "V2");
            graph.AddEdgeDict("V3", "V2");
            graph.AddEdgeDict("V4", "V1");

            graph.PrintVertices();
            graph.PrintListEdges();

            //Task
            Console.WriteLine("Added Graph ->");
            Graph addedGraph = graph.AddedGraph();
            addedGraph.PrintVertices();
            addedGraph.PrintListEdges();
        }

        static void FileGraph()
        {
            Graph graph = new Graph(@"..\..\Graph.txt");
            graph.PrintVertices();
            graph.PrintListEdges();

            graph.WriteInFile(@"..\..\GraphOut.txt");

            graph.RemoveEdgeDict("V1", "V2");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.RemoveEdgeDict("V0", "V2");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.RemoveVertex("V2");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddVertex("V5");

            //graph.WriteInFile(@"..\..\GraphOut.txt");
        }

        static void ConsoleGraph()
        {
            Graph graph = new Graph();

            graph.AddVertex("V0");
            graph.AddVertex("V1");
            graph.AddVertex("V2");
            graph.AddVertex("V3");
            graph.AddVertex("V4");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V1", "V2");
            graph.AddEdgeDict("V3", "V2");
            graph.AddEdgeDict("V4", "V1");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.RemoveEdgeDict("V1", "V2");
            graph.RemoveEdgeDict("V0", "V2");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.RemoveVertex("V2");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.WriteInFile(@"..\..\GraphOut.txt");
        }

    }
}
