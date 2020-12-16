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

            //Task_III_Prim();

            //Task_IV_A_Dijkstra_6(); // Вопрос про достижимость

            //Task_IV_A_Dijkstra_6_Alt_Graph();

            //Task_IV_B_Ford_Bellman__Floyd();

            //Taks_IV_C_Ford_Bellman();

            //Task_Victoria_IV_15();

            //Task_Victoria_IV_2();

            //CombinedGraph_Victoria();

            //test();

            Stream_Algorithm_Ford_Falkerson();

            Console.WriteLine();
        }

        static void Stream_Algorithm_Ford_Falkerson()
        {
            Graph graph = new Graph();

            graph.AddVertices("S", "1", "2", "3", "4", "T");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("S", "1", weight: 16, is_oriented: true);
            graph.AddEdgeDict("S", "2", weight: 13, is_oriented: true);

            graph.AddEdgeDict("1", "3", weight: 12, is_oriented: true);
            graph.AddEdgeDict("1", "2", weight: 10, is_oriented: true);

            graph.AddEdgeDict("2", "1", weight: 4, is_oriented: true);
            graph.AddEdgeDict("2", "4", weight: 14, is_oriented: true);

            graph.AddEdgeDict("3", "T", weight: 20, is_oriented: true);
            graph.AddEdgeDict("3", "2", weight: 9, is_oriented: true);

            graph.AddEdgeDict("4", "3", weight: 7, is_oriented: true);
            graph.AddEdgeDict("4", "T", weight: 4, is_oriented: true);

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.Ford_Falkerson("S", "T");

            // ==============================================
            //graph.AddVertices("S", "A", "B", "C", "D", "T");

            //graph.PrintVertices();
            //graph.PrintListEdges();

            //graph.AddEdgeDict("S", "A", weight: 10, is_oriented: true);
            //graph.AddEdgeDict("S", "C", weight: 10, is_oriented: true);

            //graph.AddEdgeDict("A", "B", weight: 4, is_oriented: true);
            //graph.AddEdgeDict("A", "D", weight: 8, is_oriented: true);
            //graph.AddEdgeDict("A", "C", weight: 2, is_oriented: true);

            //graph.AddEdgeDict("B", "T", weight: 10, is_oriented: true);

            //graph.AddEdgeDict("C", "D", weight: 9, is_oriented: true);

            //graph.AddEdgeDict("D", "T", weight: 10, is_oriented: true);
            //graph.AddEdgeDict("D", "B", weight: 6, is_oriented: true);

            //graph.PrintVertices();
            //graph.PrintListEdges();

            //graph.Ford_Falkerson("S", "T");

            // =================================================================
            //graph.AddVertices("V1", "V2", "V3", "V4", "V5", "V6", "V7", "V8");

            //graph.PrintVertices();
            //graph.PrintListEdges();

            //graph.AddEdgeDict("V1", "V3", weight: 95, is_oriented: true);
            //graph.AddEdgeDict("V1", "V4", weight: 75, is_oriented: true);
            //graph.AddEdgeDict("V1", "V2", weight: 32, is_oriented: true);

            //graph.AddEdgeDict("V2", "V3", weight: 5, is_oriented: true);
            //graph.AddEdgeDict("V2", "V5", weight: 23, is_oriented: true);
            //graph.AddEdgeDict("V2", "V8", weight: 16, is_oriented: true);

            //graph.AddEdgeDict("V3", "V6", weight: 6, is_oriented: true);
            //graph.AddEdgeDict("V3", "V4", weight: 18, is_oriented: true);


            //graph.AddEdgeDict("V4", "V6", weight: 9, is_oriented: true);
            //graph.AddEdgeDict("V4", "V5", weight: 24, is_oriented: true);

            //graph.AddEdgeDict("V5", "V7", weight: 20, is_oriented: true);
            //graph.AddEdgeDict("V5", "V8", weight: 94, is_oriented: true);

            //graph.AddEdgeDict("V6", "V7", weight: 7, is_oriented: true);
            //graph.AddEdgeDict("V6", "V5", weight: 11, is_oriented: true);

            //graph.AddEdgeDict("V7", "V8", weight: 81, is_oriented: true);


            //graph.PrintVertices();
            //graph.PrintListEdges();

            //graph.Ford_Falkerson("V1", "V8");
        }

        static void test()
        {
            Graph graph = new Graph();

            graph.AddVertices("V0", "V1", "V2", "V3", "V4");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V0", "V1", weight: -1, is_oriented: true);
            graph.AddEdgeDict("V0", "V4", weight: 4, is_oriented: true);

            graph.AddEdgeDict("V1", "V2", weight: 2, is_oriented: true);
            graph.AddEdgeDict("V1", "V3", weight: 2, is_oriented: true);
            //graph.AddEdgeDict("V1", "V3", weight: -2, is_oriented: true); // Отрицательный цикл
            graph.AddEdgeDict("V1", "V4", weight: 3, is_oriented: true);

            graph.AddEdgeDict("V2", "V3", weight: -3, is_oriented: true);

            graph.AddEdgeDict("V3", "V1", weight: 1, is_oriented: true);
            graph.AddEdgeDict("V3", "V4", weight: 5, is_oriented: true);

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.StartFord_Bellman("V3", negateCycleVersion: true);
            //graph.Task_IV_C_Ford_Bellman();
        }

        static void CombinedGraph_Victoria()
        {
            Graph graph_1 = new Graph();
            Graph graph_2 = new Graph();

            graph_1.AddVertices("V1", "V2", "V3");
            graph_2.AddVertices("V1", "V3", "V4");

            graph_1.PrintVertices();
            graph_2.PrintVertices();

            graph_1.AddEdgeDict("V1", "V3", weight: 7, is_oriented: true);
            graph_1.AddEdgeDict("V1", "V2", weight: 5, is_oriented: true);

            graph_2.AddEdgeDict("V3", "V3", weight: 9, is_oriented: true);
            graph_2.AddEdgeDict("V1", "V4", weight: 4, is_oriented: true);

            graph_1.PrintListEdges();
            graph_2.PrintListEdges();

            Graph combinedGraph = Graph.CombineTwoOrientedGraphs(graph_1, graph_2);

            combinedGraph.PrintListEdges();
            combinedGraph.PrintVertices();
        }

        static void Task_Victoria_IV_2()
        {
            Graph graph = new Graph();

            graph.AddVertices("V0", "V1", "V2", "V3", "V4");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V0", "V1", weight: -1, is_oriented: true);
            graph.AddEdgeDict("V0", "V4", weight: 4, is_oriented: true);

            graph.AddEdgeDict("V1", "V2", weight: 2, is_oriented: true);
            graph.AddEdgeDict("V1", "V3", weight: 2, is_oriented: true);
            graph.AddEdgeDict("V1", "V4", weight: 3, is_oriented: true);

            graph.AddEdgeDict("V2", "V3", weight: -3, is_oriented: true);

            graph.AddEdgeDict("V3", "V1", weight: 1, is_oriented: true);
            graph.AddEdgeDict("V3", "V4", weight: 5, is_oriented: true);

            graph.PrintVertices();
            graph.PrintListEdges();

            Console.WriteLine();

            graph.Victoria_IV_2_Bellman(3);
        }

        static void Task_Victoria_IV_15()
        {
            Graph graph = new Graph();

            graph.AddVertices("V0", "V1", "V2", "V3", "V4");

            graph.AddEdgeDict("V0", "V1", true, weight: 10);
            graph.AddEdgeDict("V0", "V2", true, weight: 30);
            graph.AddEdgeDict("V0", "V3", true, weight: 50);
            graph.AddEdgeDict("V0", "V4", true, weight: 10);

            graph.AddEdgeDict("V2", "V4", true, weight: 10);

            graph.AddEdgeDict("V3", "V1", true, weight: 40);
            graph.AddEdgeDict("V3", "V2", true, weight: 20);

            graph.AddEdgeDict("V4", "V0", true, weight: 10);
            graph.AddEdgeDict("V4", "V2", true, weight: 10);
            graph.AddEdgeDict("V4", "V3", true, weight: 30);

            graph.PrintVertices();
            graph.PrintListEdges();
            graph.PrintMatrix(0, true, false);
            Console.WriteLine();
            graph.PrintMatrix(0, true, true);

           //graph.Print_Dijkstra("V0");
            Console.WriteLine();

            graph.Victoria_IV_15_Dijkstra("V0", "V3", "V1");
            //graph.Task_Dijkstra_IV_A_6();
        }

        static void Taks_IV_C_Ford_Bellman()
        {
            Graph graph = new Graph();

            graph.AddVertices("V0", "V1", "V2", "V3", "V4");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V0", "V1", weight: -1, is_oriented: true);
            graph.AddEdgeDict("V0", "V4", weight: 4, is_oriented: true);

            graph.AddEdgeDict("V1", "V2", weight: 2, is_oriented: true);
            graph.AddEdgeDict("V1", "V3", weight: 2, is_oriented: true);
            //graph.AddEdgeDict("V1", "V3", weight: -2, is_oriented: true); // Отрицательный цикл
            graph.AddEdgeDict("V1", "V4", weight: 3, is_oriented: true);

            graph.AddEdgeDict("V2", "V3", weight: -3, is_oriented: true);

            graph.AddEdgeDict("V3", "V1", weight: 1, is_oriented: true);
            graph.AddEdgeDict("V3", "V4", weight: 5, is_oriented: true);

            graph.PrintVertices();
            graph.PrintListEdges();

            //graph.StartFord_Bellman("V0", negateCycleVersion: true);
            graph.Task_IV_C_Ford_Bellman();
        }

        static void Task_IV_B_Ford_Bellman()
        {
            Graph graph = new Graph();

            graph.AddVertices("V0", "V1", "V2", "V3", "V4");

            graph.PrintVertices();
            graph.PrintListEdges();

            graph.AddEdgeDict("V0", "V1", weight: -1, is_oriented: true);
            graph.AddEdgeDict("V0", "V4", weight: 4, is_oriented: true);

            graph.AddEdgeDict("V1", "V2", weight: 2, is_oriented: true);
            graph.AddEdgeDict("V1", "V3", weight: 2, is_oriented: true);
            graph.AddEdgeDict("V1", "V4", weight: 3, is_oriented: true);

            graph.AddEdgeDict("V2", "V3", weight: -3, is_oriented: true);

            graph.AddEdgeDict("V3", "V1", weight: 1, is_oriented: true);
            graph.AddEdgeDict("V3", "V4", weight: 5, is_oriented: true);

            graph.PrintVertices();
            graph.PrintListEdges();

            //graph.Ford_Bellman_PrintWay("V0", "V3");
            //Console.WriteLine();
            //graph.Ford_Bellman_PrintWay("V0", "V2");
            graph.Task_IV_B_Ford_Bellman("V0", "V2", "V3");

            // ==============================================
            //graph.AddVertices("V1", "V2", "V3", "V4", "V5");

            //graph.PrintVertices();
            //graph.PrintListEdges();

            //graph.AddEdgeDict("V1", "V2", weight: 15);
            //graph.AddEdgeDict("V3", "V2", weight: 75);
            //graph.AddEdgeDict("V4", "V1", weight: 85);

            //graph.AddEdgeDict("V5", "V3", weight: 25);


            //graph.AddVertices("V6", "V7", "V8");


            //graph.AddEdgeDict("V4", "V6", weight: 35);
            //graph.AddEdgeDict("V6", "V1", weight: 65);

            //graph.AddEdgeDict("V4", "V7", weight: 45);
            //graph.AddEdgeDict("V7", "V3", weight: 20);

            //graph.AddEdgeDict("V5", "V8", weight: 33);
            //graph.AddEdgeDict("V8", "V1", weight: 44);

            //graph.PrintVertices();
            //graph.PrintListEdges();

            //graph.Task_IV_B_Ford_Bellman__Floyd("V1");

            // ==============================================
            //graph.AddVertices("S", "A", "B", "C", "D", "E");

            //graph.AddEdgeDict("S", "E", weight: 8, is_oriented: true);
            //graph.AddEdgeDict("S", "A", weight: 10, is_oriented: true);

            //graph.AddEdgeDict("E", "D", weight: 1, is_oriented: true);

            //graph.AddEdgeDict("D", "A", weight: -4, is_oriented: true);
            //graph.AddEdgeDict("D", "C", weight: -1, is_oriented: true);

            //graph.AddEdgeDict("C", "B", weight: -2, is_oriented: true);

            //graph.AddEdgeDict("B", "A", weight: 1, is_oriented: true);

            //graph.AddEdgeDict("A", "C", weight: 2, is_oriented: true);

            //graph.PrintVertices();
            //graph.PrintListEdges();

            //graph.Task_IV_B_Ford_Bellman__Floyd("S");
        }

        static void Task_IV_A_Dijkstra_6_Alt_Graph()
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
            graph.PrintMatrix(1, true, false);
            Console.WriteLine();
            graph.PrintMatrix(1, true, true);

            //graph.Print_Dijkstra("V0");
            Console.WriteLine();
            graph.Task_Dijkstra_IV_A_6();
        }

        // Вопрос с вершинами
        static void Task_IV_A_Dijkstra_6()
        {
            Graph graph = new Graph();

            graph.AddVertices("V0", "V1", "V2", "V3", "V4");

            graph.AddEdgeDict("V0", "V1", true, weight: 10);
            graph.AddEdgeDict("V0", "V2", true, weight: 30);
            graph.AddEdgeDict("V0", "V3", true, weight: 50);
            graph.AddEdgeDict("V0", "V4", true, weight: 10);

            graph.AddEdgeDict("V2", "V4", true, weight: 10);

            graph.AddEdgeDict("V3", "V1", true, weight: 40);
            graph.AddEdgeDict("V3", "V2", true, weight: 20);

            graph.AddEdgeDict("V4", "V0", true, weight: 10);
            graph.AddEdgeDict("V4", "V2", true, weight: 10);
            graph.AddEdgeDict("V4", "V3", true, weight: 30);

            graph.PrintVertices();
            graph.PrintListEdges();
            graph.PrintMatrix(0, true, false);
            Console.WriteLine();
            graph.PrintMatrix(0, true, true);

            //graph.Print_Dijkstra("V0");
            Console.WriteLine();
            graph.Task_Dijkstra_IV_A_6();
            // graph.Task_IV_A_Dijkstra_6__MATRIX("V0");
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

            List<string> solvedPath = graph.TaskSolve_II_1("V4", "V5", new[] { "V6", "V7", "V8" });

            Console.Write("Path is : ");
            foreach (var item in solvedPath)
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
