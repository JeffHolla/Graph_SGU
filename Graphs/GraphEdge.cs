using System;

namespace Graphs.Graphs
{
    /// <summary>
    /// Ребро графа
    /// </summary>
    public class GraphEdge:ICloneable
    {
        // Связанная вершины
        public GraphVertex SecondVertex { get; }

        // Вес ребра
        public int EdgeWeight { get; }

        // Ориентированное ли ребро
        // Помогает при записи в файл
        public bool Is_oriented { get; }

        /// <param name="second_vertex">Связанная вершина</param>
        /// <param name="weight">Вес ребра</param>
        /// <param name="is_oriented">Ориентированное ли ребро</param>
        public GraphEdge(GraphVertex second_vertex, int weight = 0, bool is_oriented = false)
        {
            SecondVertex = second_vertex;
            EdgeWeight = weight;
            Is_oriented = is_oriented;
        }

        public object Clone()
        {
            return new GraphEdge(SecondVertex.Clone() as GraphVertex, EdgeWeight, Is_oriented);
        }

        public override string ToString()
        {
            return SecondVertex.Name;
        }
    }
}