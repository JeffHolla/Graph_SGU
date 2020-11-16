using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Graphs
{
    /// Вершина графа
    [Serializable]
    public class GraphVertex:ICloneable
    {
        // Название вершины
        public string Name { get; } 

        /// Конструктор
        /// <param name="vertexName">Название вершины</param>
        public GraphVertex(string name)
        {
            Name = name;
        }

        /// Преобразование в строку
        /// <returns>Имя вершины</returns>
        public override string ToString() => Name;

        public object Clone()
        {
            return new GraphVertex(string.Copy(Name));
        }
    }
}
