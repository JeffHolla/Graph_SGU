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

            Task_For_Doklad();

            Console.WriteLine();
        }

        static void Task_For_Doklad()
        {
            //Алгоритм Литтла
            Graph graph = new Graph();

            graph.AddVertices("V0", "V1", "V2", "V3", "V4");

            //graph.AddEdgeDict("V0", "V1", weight: 5);
            //graph.AddEdgeDict("V0", "V4", weight: 7);
            //graph.AddEdgeDict("V0", "V5", weight: 10);

            //graph.AddEdgeDict("V1", "V2", weight: 4);
            //graph.AddEdgeDict("V1", "V3", weight: 15);
            //graph.AddEdgeDict("V1", "V4", weight: 3);

            //graph.AddEdgeDict("V2", "V3", weight: 8);

            //graph.AddEdgeDict("V5", "V4", weight: 6);
            //graph.AddEdgeDict("V5", "V2", weight: 9);

            //graph.AddEdgeDict("V6", "V2", weight: 9);
            //graph.AddEdgeDict("V6", "V3", weight: 10);
            //graph.AddEdgeDict("V6", "V5", weight: 8);


            graph.AddEdgeDict("V0", "V1", true, weight: 5);
            graph.AddEdgeDict("V0", "V2", true, weight: 12);
            graph.AddEdgeDict("V0", "V3", true, weight: 11);
            graph.AddEdgeDict("V0", "V4", true, weight: 5);


            graph.AddEdgeDict("V1", "V0", true, weight: 20);
            graph.AddEdgeDict("V1", "V2", true, weight: 18);
            graph.AddEdgeDict("V1", "V3", true, weight: 17);
            graph.AddEdgeDict("V1", "V4", true, weight: 5);


            graph.AddEdgeDict("V2", "V0", true, weight: 18);
            graph.AddEdgeDict("V2", "V1", true, weight: 14);
            graph.AddEdgeDict("V2", "V3", true, weight: 11);
            graph.AddEdgeDict("V2", "V4", true, weight: 5);


            graph.AddEdgeDict("V3", "V0", true, weight: 12);
            graph.AddEdgeDict("V3", "V1", true, weight: 7);
            graph.AddEdgeDict("V3", "V2", true, weight: 6);
            graph.AddEdgeDict("V3", "V4", true, weight: 5);


            graph.AddEdgeDict("V4", "V0", true, weight: 8);
            graph.AddEdgeDict("V4", "V1", true, weight: 11);
            graph.AddEdgeDict("V4", "V2", true, weight: 11);
            graph.AddEdgeDict("V4", "V3", true, weight: 12);


            graph.PrintListEdges();

            graph.PrintMatrix();
            graph.Doklad_2();
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
