using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

//2. Конструкторы (не менее 3-х):
// конструктор по умолчанию, создающий пустой граф  -- done

// конструктор, заполняющий данные графа из файла

// конструктор-копию(аккуратно, не все сразу делают именно копию) -- done
// специфические конструкторы для удобства тестирования -- нужен ли?


//3. Методы:
// добавляющие вершину, -- done
// добавляющие ребро(дугу), -- done(x2)
// удаляющие вершину, -- done
// удаляющие ребро(дугу), -- done(x2)

// выводящие список смежности в файл(в том числе в пригодном для чтения конструктором формате).


//4. Должны поддерживаться как ориентированные, так и неориентированные графы. -- done
// Возможность добавления меток и\или весов для дуг. -- done

namespace Graphs.Graphs
{
    /// Граф
    public class Graph // Граф имеет возможность быть смешанным.
    {
        // Список классов граней в виде --  Вершина - Список граней  --  Класс граней хранит в себе доп инфу по сути.
        public Dictionary<GraphVertex, List<GraphEdge>> VertexEdges { get; }

        // Пустой граф
        public Graph()
        {
            VertexEdges = new Dictionary<GraphVertex, List<GraphEdge>>();
        }

        // Заполнение из файла
        public Graph(string path)
        {
            VertexEdges = new Dictionary<GraphVertex, List<GraphEdge>>();

            using (StreamReader reader = new StreamReader(path))
            {
                string[] vertexes = reader.ReadLine().Trim().Split(' ');

                foreach (var vert in vertexes) // Заполнение списка вершин
                {
                    // Vertices.Add(new GraphVertex(int.Parse(vert)));
                    VertexEdges.Add(new GraphVertex(vert), new List<GraphEdge>());
                }

                while (reader.EndOfStream == false)
                {
                    string[] values = reader.ReadLine().Trim().Split(new char[] { ':', ' ' }); // Первое значение - вершина, второе - кол-во связей(граней)
                    for (int i = 0; i < int.Parse(values[1]); ++i)// Добавление граней и их свойств
                    {
                        string[] edge_values = reader.ReadLine().Trim().Split(':'); // Первое значение - вершина к которой идёт ребро, второе значение весы, третье - ориентированность ребра


                        if (bool.Parse(edge_values[2]) == true) // Если ребро ориентированное, то проводим только с одной стороны
                        {
                            if (VertexEdges[FindVertex(values[0])].Find(vert => vert.SecondVertex == FindVertex(edge_values[0])) == null)
                            { // Если такого ребра не найдено
                                VertexEdges[FindVertex(values[0])].Add(new GraphEdge(FindVertex(edge_values[0]), weight: int.Parse(edge_values[1]), is_oriented: bool.Parse(edge_values[2])));
                            }
                            else
                            {
                                Console.WriteLine("Такое ребро уже проведена!");
                            }
                        }
                        else if (bool.Parse(edge_values[2]) == false) // Иначе с двух сторон
                        {
                            if (VertexEdges[FindVertex(values[0])].Find(vert => vert.SecondVertex == FindVertex(edge_values[0])) == null)
                            {
                                VertexEdges[FindVertex(values[0])].Add(new GraphEdge(FindVertex(edge_values[0]), int.Parse(edge_values[1])));
                                //VertexEdges[FindVertex(edge_values[0])].Add(new GraphEdge(FindVertex(values[0]), int.Parse(edge_values[1])));
                            }

                            // Вариант при котором ребро неориентированно, но в файле ошибка. Т.е. восстановление неориентированности ребра:
                            //if (VertexEdges[FindVertex(values[0])].Find(vert => vert.SecondVertex == FindVertex(edge_values[0])) == null)
                            //    VertexEdges[FindVertex(values[0])].Add(new GraphEdge(FindVertex(edge_values[0]), int.Parse(edge_values[1])));

                            //if (VertexEdges[FindVertex(edge_values[0])].Find(vert => vert.SecondVertex == FindVertex(values[0])) == null)
                            //    VertexEdges[FindVertex(edge_values[0])].Add(new GraphEdge(FindVertex(values[0]), int.Parse(edge_values[1])));
                        }

                    }
                }
            }
        }

        // Копия графа -- Fixed
        public Graph(Graph graph)
        {
            VertexEdges = new Dictionary<GraphVertex, List<GraphEdge>>();

            foreach (var pair in graph.VertexEdges)
            {
                VertexEdges.Add(pair.Key, new List<GraphEdge>(pair.Value));
            }
        }

        public void AddVertex(string name)
        {
            if (FindVertex(name) == null)
                VertexEdges.Add(new GraphVertex(name), new List<GraphEdge>());
            else
                Console.WriteLine($"Вершина с именем |{name}| уже существует! Новая вершина не была создана.");
        }

        public void AddVertices(params string[] vertices)
        {
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        // Deprecated RemoveVertex
        //public void RemoveVertex(string name)
        //{
        //    if (FindVertex(name) != null)
        //    {

        //        // Проходим по каждому ребру из удаляемой вершины
        //        foreach (var edge in VertexEdges[FindVertex(name)])
        //        {
        //            // Прыгаем по конечным точкам (в GraphEdge указываются только конечная точка ребра) ребра вершины и удаляем их.
        //            // VertexEdges[edge.SecondVertex] - прыжок в конечную вершину ребра и просмотр из этой вершины остальных рёбер
        //            // Remove(... .Find(v => v.SecondVertex == FindVertex(name)) - поиск и удаление ребра из списка рёбер VertexEdges
        //            VertexEdges[edge.SecondVertex].Remove(VertexEdges[edge.SecondVertex].Find(v => v.SecondVertex == FindVertex(name)));
        //        }

        //        // Находим ребро в словаре и удаляем
        //        VertexEdges.Remove(FindVertex(name));


        //    }
        //    else
        //    {
        //        Console.WriteLine("Вершина не найдена!");
        //    }
        //}

        public void RemoveVertex(string name)
        {
            GraphVertex vertexToDel = FindVertex(name);

            // Если найдена такая вершина
            if (vertexToDel != null)
            {
                // Проходим по каждой вершине из списка
                // ToList сделан для того, чтобы передать копию, потому что мы изменяем коллекцию в цикле
                foreach (var vertex in VertexEdges.Keys.ToList())
                {
                    // Если имя текущей вершины не совпадает с именем вершины
                    if (vertex.Name != vertexToDel.Name)
                    {
                        // Создаём копию рёбер, потому что в цикле будем менять коллекцию
                        List<GraphEdge> copyOfEdges = new List<GraphEdge>(VertexEdges[vertex]);
                        // Просматриваем каждое ребро исходящее из текущего vertex
                        foreach (var edge in VertexEdges[vertex])
                        {
                            // Если конечная вершина ребра равна той вершине, которую нам нужно удалить, то
                            // удаляем это ребро из копии списка рёбер
                            if (edge.SecondVertex.Name == vertexToDel.Name)
                                copyOfEdges.Remove(edge);
                        }
                        // присваеваем этой вершине новый список рёбер.
                        // По сути из этого списка удалены рёбра, которые идут В удаляемую вершину.
                        VertexEdges[vertex] = copyOfEdges;
                    }
                }

                // Удаляем рёбра, которые идут ИЗ удаляемой вершины.
                VertexEdges[vertexToDel].Clear();

                // Находим Вершину в словаре и удаляем
                VertexEdges.Remove(vertexToDel);
            }
            else
            {
                Console.WriteLine("Вершина не найдена!");
            }
        }


        public void AddEdgeDict(string name_of_vertex_1, string name_of_vertex_2, bool is_oriented = false, int weight = 0)
        {
            if (FindVertex(name_of_vertex_1) != null && FindVertex(name_of_vertex_2) != null)
            {
                if (is_oriented == false)
                {
                    // Если нет ребра не из одной из вершин
                    if (VertexEdges[FindVertex(name_of_vertex_1)].Find(v => v.SecondVertex == FindVertex(name_of_vertex_2)) == null &&
                            VertexEdges[FindVertex(name_of_vertex_2)].Find(v => v.SecondVertex == FindVertex(name_of_vertex_1)) == null)
                    {
                        // Проводим ребро в две вершины, т.к. неориентированное ребро
                        VertexEdges[FindVertex(name_of_vertex_1)].Add(new GraphEdge(FindVertex(name_of_vertex_2), weight: weight));
                        VertexEdges[FindVertex(name_of_vertex_2)].Add(new GraphEdge(FindVertex(name_of_vertex_1), weight: weight));
                    }
                    else
                    {
                        Console.WriteLine("Неориентированное ребро уже существует!");
                    }
                }
                else
                {
                    VertexEdges[FindVertex(name_of_vertex_1)].Add(new GraphEdge(FindVertex(name_of_vertex_2), is_oriented: true, weight: weight));
                }
            }
            else
            {
                Console.WriteLine("Вершина не найдена!");
            }
        }

        //Fixed
        public void RemoveEdgeDict(string name_of_vertex_1, string name_of_vertex_2, bool is_oriented = false)
        {
            if (FindVertex(name_of_vertex_1) != null && FindVertex(name_of_vertex_2) != null)
            {
                if (is_oriented == false)
                {
                    if (VertexEdges[FindVertex(name_of_vertex_1)].Find(edge => edge.SecondVertex == FindVertex(name_of_vertex_2)) != null &&
                            VertexEdges[FindVertex(name_of_vertex_2)].Find(edge => edge.SecondVertex == FindVertex(name_of_vertex_1)) != null)
                    {
                        VertexEdges[FindVertex(name_of_vertex_1)].Remove(VertexEdges[FindVertex(name_of_vertex_1)].Find(v => v.SecondVertex == FindVertex(name_of_vertex_2)));
                        VertexEdges[FindVertex(name_of_vertex_2)].Remove(VertexEdges[FindVertex(name_of_vertex_2)].Find(v => v.SecondVertex == FindVertex(name_of_vertex_1)));
                    }
                    else
                    {
                        Console.WriteLine("Такого ребра не существует!");
                    }
                }
                else if (is_oriented == true)
                {
                    if (VertexEdges[FindVertex(name_of_vertex_1)].Find(edge => edge.SecondVertex == FindVertex(name_of_vertex_2)) != null)
                    {
                        VertexEdges[FindVertex(name_of_vertex_1)].Remove(VertexEdges[FindVertex(name_of_vertex_1)].Find(v => v.SecondVertex == FindVertex(name_of_vertex_2)));
                    }
                    else
                    {
                        Console.WriteLine("Такого ребра не существует!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Вершина не найдена!");
            }
        }

        public void WriteInFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var vertex in VertexEdges.Keys)
                {
                    writer.Write(vertex.Name + " ");
                }

                writer.WriteLine();

                foreach (var item in VertexEdges)
                {
                    writer.WriteLine("{0}:{1}", item.Key.Name, item.Value.Count);// Стоит поменять привязку к id на Name везде
                    foreach (var vertexEdge in item.Value)
                    {
                        writer.WriteLine("{0}:{1}:{2}", vertexEdge.SecondVertex.Name, vertexEdge.EdgeWeight, vertexEdge.Is_oriented);
                    }
                }
            }
        }



        /// <summary>
        /// Работает только при последовательном ходе вершин, т.е. при V0, V1, ..., Vn к примеру.
        /// В вызове указывается сдвиг вершин, т.е. кол-во вершин до нулевой(V0)
        /// </summary>
        /// <param name="shiftOfVertices">Сдвиг до V0, т.е. количество вершин до V0</param>
        /// <param name="reversed">
        /// При False столбецы матрицы - исходящие вершины, строка - конечная вершина.
        /// При True строка матрицы - исходящая вершина, столбцы - конечные вершины.
        /// </param>
        /// <param name="verticalLines">Рисуются ли вертикальные линии между столбцами в матрице</param>
        public void PrintMatrix(int shiftOfVertices, bool reversed = false, bool verticalLines = false, bool horizontalLines = false)
        {
            int[,] matrix = ToMatrix(shiftOfVertices, reversed);

            Console.Write("  |");
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                if (verticalLines == false)
                    Console.Write($"{i,3} |");
                else
                    Console.Write($"{i,3} |");
            }

            // Горизонтальная линия после верхних индексов
            Console.WriteLine("");
            Console.Write("---");
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                Console.Write("{0,3}", "-----");
            }

            Console.WriteLine("");

            if (verticalLines == false)
                for (int i = 0; i < matrix.GetLength(0); ++i)
                {
                    Console.Write("{0,2}|", i);
                    for (int j = 0; j < matrix.GetLength(1); ++j)
                    {
                        if (i != j)
                        {
                            Console.Write("{0, 4} ", matrix[i, j]);
                        }
                        else
                            Console.Write("{0, 4} ", "inf");
                    }

                    if (horizontalLines == true)
                    {
                        Console.WriteLine("");
                        Console.Write("---");
                        for (int j = 0; j < matrix.GetLength(0); ++j)
                        {
                            Console.Write("{0,3}", "-----");
                        }
                    }

                    Console.WriteLine();
                }
            else
                for (int i = 0; i < matrix.GetLength(0); ++i)
                {
                    Console.Write("{0,2}|", i);
                    for (int j = 0; j < matrix.GetLength(1); ++j)
                    {
                        if (i != j)
                        {
                            Console.Write("{0, 3} |", matrix[i, j]);
                        }
                        else
                            Console.Write("{0, 3} |", "inf");
                    }

                    if (horizontalLines == true)
                    {
                        Console.WriteLine("");
                        Console.Write("---");
                        for (int j = 0; j < matrix.GetLength(0); ++j)
                        {
                            Console.Write("{0,3}", "-----");
                        }
                    }

                    Console.WriteLine();
                }
        }

        public void PrintListEdges()
        {
            foreach (var vertexAndEdges in VertexEdges)
            {
                Console.Write(vertexAndEdges.Key.Name + " : ");
                foreach (var edge in vertexAndEdges.Value)
                {
                    Console.Write(edge.SecondVertex.Name + "{" + edge.EdgeWeight + "}" + ", ");
                }
                Console.WriteLine();
            }
        }

        public void PrintVertices()
        {
            Console.Write("Vertices : ");
            foreach (var vert in VertexEdges.Keys)
            {
                Console.Write(vert.Name + ", ");
            }
            Console.WriteLine();
        }
        // Поиск вершины
        private GraphVertex FindVertex(string name)
        {
            foreach (var vert in VertexEdges.Keys)
            {
                if (vert.Name == name)
                {
                    return vert;
                }
            }

            return null;
        }
        // Возвращает матрицу текущего графа
        // Указывается сдвиг до V0
        private int[,] ToMatrix(int shiftOfVertices, bool reversed = false)
        {
            int[,] matrix = new int[VertexEdges.Count, VertexEdges.Count];

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    if (i != j)
                        matrix[i, j] = 0;
                    else
                        matrix[i, j] = int.MaxValue;
                }
            }

            foreach (var pair in VertexEdges)
            {
                foreach (var item in pair.Value)
                {
                    if (reversed == false)
                        matrix[int.Parse(item.SecondVertex.Name.Remove(0, 1)) - shiftOfVertices,
                            int.Parse(pair.Key.Name.Remove(0, 1)) - shiftOfVertices] = item.EdgeWeight;
                    else
                        matrix[int.Parse(pair.Key.Name.Remove(0, 1)) - shiftOfVertices,
                        int.Parse(item.SecondVertex.Name.Remove(0, 1)) - shiftOfVertices] = item.EdgeWeight;
                }
            }

            return matrix;
        }


        #region Tasks

        #region Task_IA()
        // Task Ia - 1 -- Определить, существует ли вершина, в которую есть дуга из вершины u, но нет из v. Вывести такую вершину.
        public void Task_IA()
        {
            // Проходим по всем вершинам 
            foreach (var pair in VertexEdges)
            {
                // Проходим по каждой смежной вершине от текущей
                foreach (var value in pair.Value)
                {
                    // Вывод всех вершин
                    //Console.Write(IsExistEdgeFromVertexUInVertexV(pair.Key.Name, value.SecondVertex.Name));
                    if (IsExistEdgeFromVertexUInVertexV(pair.Key.Name, value.SecondVertex.Name) == true)
                    {
                        Console.WriteLine("Вершина, в которую есть дуга из {0}, но нет из {1} -- {0}", pair.Key.Name, value.SecondVertex.Name);
                    }
                }
            }
        }

        private bool IsExistEdgeFromVertexUInVertexV(string vertex_u, string vertex_v)
        {
            if (FindVertex(vertex_u) != null && FindVertex(vertex_v) != null)
            {
                //Если есть такое ребро из vertex_u в vertex_v
                //т.е. такое ребро было найдено
                if (VertexEdges[FindVertex(vertex_u)].Find(vert => vert.SecondVertex == FindVertex(vertex_v)) != null &&
                    // но такого ребра нет из vertex_v в vertex_v
                    VertexEdges[FindVertex(vertex_v)].Find(vert => vert.SecondVertex == FindVertex(vertex_u)) == null)
                    return true;
                else
                    return false;
            }
            else
            {
                Console.WriteLine("Вершина не найдена!");
                return false;
            }
        }
        #endregion

        #region Task_IA_2
        // Task Ia - 2 -- Для каждой вершины графа вывести её степень.
        public void Task_IA_2(bool is_oriented = false)// Не поддерживает смешанный граф!
        {
            if (is_oriented == false)
                foreach (var pair in VertexEdges)
                {
                    // pair.Value.Count*2, т.к. в обе стороны исходят рёбра
                    Console.WriteLine("Вершина = {0}, степень вершины = {1}", pair.Key, pair.Value.Count * 2);
                }
            else
                foreach (var pair in VertexEdges)
                {
                    // d(v) = d(v)+   +   d(v)-
                    Console.WriteLine("Вершина = {0}, степень вершины = {1}", pair.Key, pair.Value.Count + HelperTask_IA_2_ForeachEdition(pair));
                    Console.WriteLine("Oriented. Count of D+ = {0}, Count of D- = {1}", pair.Value.Count, HelperTask_IA_2_ForeachEdition(pair));
                    Console.WriteLine();
                }
        }

        // Буквально помогает считать степени при ориентированности ребра
        // d(v)- = кол-во входящих дуг
        // d(v)+ = кол-во исходящих дуг
        private int HelperTask_IA_2_ForeachEdition(KeyValuePair<GraphVertex, List<GraphEdge>> keyValuePair)
        {
            // Счётчик для подсчёта входящих рёбер
            // По сути - d(v)-
            int counter = 0;

            // Проходимся по всем вершинам с рёбрами
            foreach (var vertex in VertexEdges.Keys)
            {
                // Осуществляем проверку, чтобы не посчитать ребро и которого вышли
                if (vertex != keyValuePair.Key)
                {
                    // Если нашли ребро в нужную вершину, то прибавляем счётчик
                    if (VertexEdges[vertex].Find(x => x.SecondVertex == keyValuePair.Key) != null)
                        counter++;
                }
            }
            return counter;
        }
        #endregion

        #region Task_IB
        public Graph AddedGraph()
        {
            //Делаем копию графа
            Graph addedGraph = new Graph(this);

            // Очищаем список рёбер
            foreach (var vertex in addedGraph.VertexEdges.Keys)
            {
                addedGraph.VertexEdges[vertex].Clear();
            }

            // Для каждой вершины строим дополненный рёбра
            foreach (var current_vertex in addedGraph.VertexEdges.Keys)
            {
                // Для этого создадим список вершин, которые нужно исключить
                List<GraphVertex> VertexesFromCur = new List<GraphVertex>();
                foreach (var EdgeFromCur in VertexEdges[current_vertex])
                {
                    VertexesFromCur.Add(EdgeFromCur.SecondVertex);
                }

                foreach (var vertex_from_edges in addedGraph.VertexEdges.Keys)
                {
                    if (VertexesFromCur.Contains(vertex_from_edges) == false && vertex_from_edges != current_vertex)
                    {
                        addedGraph.AddEdgeDict(current_vertex.Name, vertex_from_edges.Name);
                    }
                }
            }

            return addedGraph;
        }
        #endregion

        #region Tasks_II_9_&_27
        // 9.Найти путь, соединяющий вершины u и v и не проходящий через заданное подмножество вершин V.

        // Словарь marks для запоминания мест, где мы были
        // GraphVertex - вершина, bool - были ли мы здесь или нет
        private Dictionary<GraphVertex, bool> marks = new Dictionary<GraphVertex, bool>();

        // Словарь toGetPath для восстановления пути
        // Первый GraphVertex - вершина куда пришли
        // Второй GraphVertex - вершина откуда пришли
        private Dictionary<GraphVertex, GraphVertex> toGetPath = new Dictionary<GraphVertex, GraphVertex>();

        private GraphVertex FinishVertex;
        private GraphVertex StartVertex;

        public string StartDFS(string startVertex, string endVertex)
        {
            marks.Clear();
            foreach (var vertex in VertexEdges.Keys)
            {
                marks.Add(vertex, false);
            }

            string status = "Fail";

            FinishVertex = FindVertex(endVertex);
            StartVertex = FindVertex(startVertex);

            DFS(StartVertex, FinishVertex, ref status);
            //Console.WriteLine(status);


            //foreach(var item in get_path())
            //{
            //    Console.WriteLine(item.Name);
            //}


            return status;
        }

        private void DFS(GraphVertex vertex, GraphVertex from, ref string result)
        {
            if (marks[vertex] == true)
            {
                return;
            }
            marks[vertex] = true;
            toGetPath[vertex] = from;

            if (vertex == FinishVertex)
            {
                result = "Path was found!";
                return;
            }

            foreach (var vertex_out in VertexEdges[vertex])
            {
                DFS(vertex_out.SecondVertex, vertex, ref result);
            }
        }

        private List<GraphVertex> get_path()
        {
            List<GraphVertex> path = new List<GraphVertex>();

            path.Add(StartVertex);

            foreach (var key in toGetPath.Keys)
                if (key != StartVertex)
                    path.Add(key);
                else
                    continue;

            return path;
        }

        public string GetPath()
        {
            // Да, лучше использовать StringBuilder, но это фактически разовая операция
            string path = "";
            foreach (var vertex in get_path())
            {
                path += vertex.Name + " ";
            }
            return path;
        }

        // 9.Найти путь, соединяющий вершины u и v и не проходящий через заданное подмножество вершин V.
        // SubSet - подмножество, которое мы не трогаем
        public List<string> TaskSolve_II_1(string startVertex, string endVertex, IEnumerable subSet)
        {
            //List<GraphVertex> subSetOfVertices = new List<GraphVertex>();
            //foreach (var item in subSet)
            //{
            //    Console.WriteLine(item);
            //    subSetOfVertices.Add(FindVertex((string)item));
            //}

            Graph graphWithoutSubSet = new Graph(this);

            foreach (var item in subSet)
            {
                graphWithoutSubSet.RemoveVertex((string)item);
            }

            graphWithoutSubSet.StartDFS("V4", "V5");

            return graphWithoutSubSet.get_path().Select(x => x.Name).ToList();
        }

        public void StartBFSForTask_II_27()
        {
            foreach (var startVertex in VertexEdges.Keys)
            {
                Console.WriteLine("--------------");
                Console.WriteLine("--------------");
                Console.WriteLine($"-Start Vertex is {startVertex.Name}");
                Console.WriteLine("--------------");

                BFS_To_Other(startVertex.Name);

            }
        }

        // Вывести кратчайшие (по числу рёбер) пути из вершины u во все остальные.
        public void BFS_To_Other(string startVertex_str)
        {
            // длина любого кратчайшего пути не превосходит n - 1,
            // поэтому n - достаточное значение для "бесконечности";
            // после работы алгоритма dist[v] = n, если v недостижима из s

            GraphVertex startVertex = FindVertex(startVertex_str);

            Dictionary<GraphVertex, int> dist = new Dictionary<GraphVertex, int>();
            Dictionary<GraphVertex, GraphVertex> path = new Dictionary<GraphVertex, GraphVertex>();

            marks.Clear();
            foreach (var vertex in VertexEdges.Keys)
            {
                marks.Add(vertex, false);
            }

            foreach (var vertex in VertexEdges.Keys)
            {
                dist.Add(vertex, VertexEdges.Keys.Count);
                path.Add(vertex, vertex);
            }
            dist[startVertex] = 0;


            Queue<GraphVertex> q = new Queue<GraphVertex>();
            q.Enqueue(startVertex);

            marks[startVertex] = true;


            while (q.Count != 0)
            {
                GraphVertex v = q.Peek();
                q.Dequeue();

                foreach (var edge in VertexEdges[v])
                {
                    if (marks[edge.SecondVertex] == false)
                    {
                        path[edge.SecondVertex] = v;

                        // Вычисляем расстояние
                        dist[edge.SecondVertex] = dist[v] + 1;

                        marks[edge.SecondVertex] = true;

                        q.Enqueue(edge.SecondVertex);
                    }
                }
            }

            //if(dist[FindVertex(endVertex)] == VertexEdges.Keys.Count)
            //{
            //    Console.WriteLine("Error! Path was not found!");
            //}

            foreach (var vertexEnd in VertexEdges.Keys)
            {
                List<GraphVertex> path_current = new List<GraphVertex>();

                var vertex_tmp = vertexEnd;

                if (vertex_tmp != startVertex)
                {
                    while (vertex_tmp != startVertex)
                    {
                        path_current.Add(vertex_tmp);
                        vertex_tmp = path[vertex_tmp];
                    }
                }

                // Переворачиваем путь, т.к. шли с конца
                path_current.Reverse();

                Console.WriteLine(startVertex.Name + "->" + vertexEnd.Name + ":");
                foreach (var item in path_current)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("---------------");
            }



            //
            foreach (var item in dist)
            {
                Console.Write(item.Key + " : " + item.Value + "| ");
            }

            Console.WriteLine();
        }


        #region Задача Виктории
        // Находит компоненты связности через нахождение одних и выпиливания их из общего списка
        // public
        private void DFS_Task_Solve()
        {
            // Лист вершин, которые нужно пройти
            List<GraphVertex> needToGoThrough = new List<GraphVertex>(VertexEdges.Keys);

            // Количество компонент(списков) связностей
            int counts_of_linkers = 0;

            // Пока есть вершины, которые нужной пройти
            while (needToGoThrough.Count > 0)
            {
                // Текущий список компоненты связности
                List<GraphVertex> current_linkers = new List<GraphVertex>();

                // Проходим по каждой вершине из вершин, которые нужной пройти
                foreach (var vert in needToGoThrough)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Current Start V = {needToGoThrough[needToGoThrough.Count - 1].Name}");
                    Console.WriteLine($"Current End V = {vert.Name}");

                    // Если текущая вершина не равна последней
                    // (Берём последнюю вершины потому что почему бы и нет, можно брать и первую всегда)
                    if (needToGoThrough[needToGoThrough.Count - 1] != vert)
                    {
                        // Получаем статус DFS от последней вершины к вершине, которую нужно пройти
                        string status = StartDFS(needToGoThrough[needToGoThrough.Count - 1].Name, vert.Name);
                        // Если путь найден (статус равен "Path was found!), то добавляем эту вершину в список компоненты связности
                        // он же линкер лист в этой программе.
                        if (status == "Path was found!")
                        {
                            current_linkers.Add(vert);
                        }
                    }
                }

                // Добавляем вершину от которой ходили к остальным в список линкеров(список текущей компоненты связности)
                current_linkers.Add(needToGoThrough[needToGoThrough.Count - 1]);
                // Увеличиваем количество компонент связности
                ++counts_of_linkers;

                // Выводим текущий список компонент связности
                foreach (var linker in current_linkers)
                {
                    Console.Write(linker.Name + " ");
                }

                // Удаляем из списка вершин, которые нужно пройти список вершин, которые входят в текущую компоненту связности
                // Остаются вершины, которые находятся в других графах и так пока есть вершины, которые нужно пройти
                needToGoThrough = needToGoThrough.Except(current_linkers).ToList();
            }

            Console.WriteLine();
            Console.WriteLine(counts_of_linkers);
        }
        #endregion

        #endregion

        #region Task_III_Прим
        // Дан взвешенный неориентированный граф из N вершин и M ребер. 
        // Требуется найти в нем каркас минимального веса.
        // Алгоритм Прима

        // Остов - граф из тех же вершин, но не без зацикливания
        public void PrimStartRandom()
        {
            Random random = new Random();

            // Выбираем случайную вершину
            GraphVertex randomStartVertex = VertexEdges.Keys.ToList()[random.Next(VertexEdges.Keys.Count)];
        }

        public void PrimStart(string vertex)
        {
            Task_III_Prim(FindVertex(vertex));
        }

        private void Task_III_Prim(GraphVertex choosenVertex)
        {
            Graph ostovGraph = new Graph();

            // Добавляем определённую вершину
            ostovGraph.AddVertex(choosenVertex.Name);

            // Список для хранения пройденных вершин
            List<GraphVertex> ostovGraphVertices = new List<GraphVertex>();
            // Сразу добавляем пройденную, т.е. стартовую вершину в список
            ostovGraphVertices.Add(choosenVertex);

            // Создаём копию оригинального графа.
            // Из него мы будем удалять пути, которые появляются в оставном графе
            // Ориентируемся также по нему(второй цикл)
            Graph originalGraphCopy = new Graph(this);

            // Пока все вершины из оригинального графа не будут записаны в остовной
            while (ostovGraphVertices.Count != VertexEdges.Keys.Count)
            {
                // Ищем минимальный вес от тех вершин, до которых добрались, т.е. те, что записаны в ostovGraphVertices
                int min_weight = int.MaxValue;
                // Для каждой вершины из списка
                foreach (var vertex in ostovGraphVertices)
                {
                    // Ищем минимальный вес ребра, которые исходят из доступных вершин
                    foreach (var edge in originalGraphCopy.VertexEdges[vertex])
                    {
                        if (edge.EdgeWeight < min_weight)
                        {
                            min_weight = edge.EdgeWeight;
                        }
                    }
                }

                // По мере прохода по остовному графу мы будем удалять из вспомогательного графа рёбра, но т.к. это делается в массиве, то 
                // перед проходом создадим копию вспомогательного графа и будем проходить по ней, удаляя рёбра из вспомогательного
                // (можно вместо копии сделать список рёбер, которые нужно удалить, и удалить их после прохода по циклу, а проходить
                // по оригинальному вспомогательному графу)
                Graph copyOfCopy = new Graph(originalGraphCopy);

                // Т.к. мы будем проводить рёбра только от тех вершин, которые есть в остовном графе, то мы
                // Т.к. мы будем добавлять вершины в список вершин нашего оставного графа одновременно проходя по нему, а
                // лист во время прохода мы изменять не можем, то создаём копию, в которую и будем добавлять
                List<GraphVertex> copyOfGraphVertices = new List<GraphVertex>(ostovGraphVertices);
                // Для каждой вершины из списка
                foreach (var vertex in ostovGraphVertices)
                {
                    // Для каждого ребра
                    foreach (var edge in copyOfCopy.VertexEdges[vertex])
                    {
                        // смотрим минимальный вес, и, если он совпадает с найденным минимальным весом, и
                        if (edge.EdgeWeight == min_weight)
                        {
                            // такой вершины ещё нет в списке вершин, то
                            if (ostovGraphVertices.Contains(edge.SecondVertex) == false)
                            {
                                // копию списка вершин мы добавляем вершину
                                copyOfGraphVertices.Add(edge.SecondVertex);
                                // добавляем вершину в остовной граф
                                ostovGraph.AddVertex(edge.SecondVertex.Name);
                                // и проводим ребро между вершинами 
                                ostovGraph.AddEdgeDict(vertex.Name, edge.SecondVertex.Name, weight: edge.EdgeWeight);

                                // в копии вспомогательного графа убираем такое ребро, чтобы не создать зацикленность по рёбрам
                                originalGraphCopy.RemoveEdgeDict(vertex.Name, edge.SecondVertex.Name);
                            }
                        }
                    }
                }
                // Выводим список смежности оставного графа
                ostovGraph.PrintListEdges();
                Console.WriteLine();

                // копируем список вершин остовного графа
                ostovGraphVertices = copyOfGraphVertices;
            }

            // выводим матрицу
            ostovGraph.PrintMatrix(1);
        }

        #endregion

        #region Task_IV_A_Dijkstra
        // В графе нет рёбер отрицательного веса.
        // 6.Найти вершину, сумма длин кратчайших путей от которой до остальных вершин минимальна.

        /* Stable Deprecated Version of Dijkstra
        //public Dictionary<GraphVertex, int> Dijkstra(string startVert)
        //{
        //    // Стартовая вершина
        //    GraphVertex startVertex = FindVertex(startVert);

        //    // Создаём копию графа.
        //    // В дальнейшем будем работать с копией списка смежности из копии графа потому что там удобнее удаление вершин :)
        //    Graph graphCopy = new Graph(this);

        //    // Словарь для меток. Первый параметр - вершина, второй - её метка.
        //    Dictionary<GraphVertex, int> listMarksOfVertices = new Dictionary<GraphVertex, int>();
        //    // Заполняем весь словарь. Метки изначально ставим максимальные
        //    foreach (var vertex in graphCopy.VertexEdges.Keys)
        //    {
        //        listMarksOfVertices.Add(vertex, int.MaxValue);
        //    }

        //    // Стартовую метку помечаем как 0
        //    listMarksOfVertices[startVertex] = 0;

        //    // Очередь не подходит т.к. нам нужно выбирать вершину для перехода по наименьшему пути
        //    // Словарь для вершин, которые нам нужно пройти
        //    // Фактически этот словарь нужен для сохранения других вершин, если в текущих вершинах тупик.
        //    // Обновляется в случае, если есть путь дальше
        //    Dictionary<GraphVertex, int> vertexWay = new Dictionary<GraphVertex, int>();
        //    // Заполняем возможные для посещения вершины от стартовой
        //    foreach (var edge in graphCopy.VertexEdges[startVertex])
        //    {
        //        vertexWay.Add(edge.SecondVertex, edge.EdgeWeight);
        //    }

        //    // Текущая вершина для обозначения вершины, с которой работаем на данный момент
        //    GraphVertex currentVertex = startVertex;

        //    // Пока есть пути, в которые мы можем пойти или пока есть вершины, которые мы не посетили
        //    while (vertexWay.Count != 0 && graphCopy.VertexEdges.Count != 0)
        //    {
        //        // Высчитываем по алгоритму минимальный вес до вершины
        //        foreach (var edge in graphCopy.VertexEdges[currentVertex])
        //        {
        //            // Если сумма ребра и текущей метки меньше, чем метка конечной вершины, то заменяем метку конечной вершины этой суммой
        //            if (edge.EdgeWeight + listMarksOfVertices[currentVertex] < listMarksOfVertices[edge.SecondVertex])
        //            {
        //                listMarksOfVertices[edge.SecondVertex] = edge.EdgeWeight + listMarksOfVertices[currentVertex];
        //            }
        //        }

        //        // Если от текущей вершины есть другие пути
        //        if (graphCopy.VertexEdges[currentVertex].Count != 0)
        //        {
        //            // Очищаем список возможных путей
        //            vertexWay.Clear();
        //            // Создаём новый список возможных путей
        //            foreach (var edge in graphCopy.VertexEdges[currentVertex])
        //            {
        //                vertexWay.Add(edge.SecondVertex, edge.EdgeWeight);
        //            }

        //            // Сортируем по значению(по int) и записываем обратно в словарь
        //            vertexWay = vertexWay.OrderBy(vertex => vertex.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

        //            // Удаляем из списка смежности копии графа(для сохранности данных и удобного удаления) текущую вершину и считаем, что посетили её.
        //            graphCopy.RemoveVertex(currentVertex.Name);
        //            // Если больше не осталось вершин в списке смежности, то выходим из цикла.
        //            if (graphCopy.VertexEdges.Count == 0)
        //                break;

        //            // Т.к. мы отсортировали возможные пути, то берём первый, т.к. он будет самым минимальным
        //            currentVertex = vertexWay.First().Key;
        //            // Убираем из возможных путей текущий путь, чтобы не уйти в рекурсию
        //            vertexWay.Remove(currentVertex);
        //        }
        //        else // Если из текущей вершиные нет путей, то
        //        {
        //            // Удаляем из списка смежности копии графа(для сохранности данных и удобного удаления) текущую вершину и считаем, что посетили её.
        //            graphCopy.RemoveVertex(currentVertex.Name);
        //            // Если больше не осталось вершин в списке смежности, то выходим из цикла.
        //            if (graphCopy.VertexEdges.Count == 0)
        //                break;

        //            // Т.к. мы всегда удаляем из списка текущих путей взятую вершину, то следующая "первая" вершина окажется другой.
        //            // Берём её.
        //            currentVertex = vertexWay.First().Key;
        //            // Убираем из возможных путей текущий путь, чтобы не уйти в рекурсию
        //            vertexWay.Remove(currentVertex);
        //        }
        //    }

        //    //Console.WriteLine("Min ways : ");
        //    //foreach (var item in listMarksOfVertices)
        //    //{
        //    //    Console.Write(item + " ");
        //    //}

        //    return listMarksOfVertices;
        //}
        */

        public Dictionary<GraphVertex, int> Dijkstra(string startVert)
        {
            // Стартовая вершина
            GraphVertex startVertex = FindVertex(startVert);

            // Создаём копию графа.
            // В дальнейшем будем работать с копией списка смежности из копии графа потому что там удобнее удаление вершин :)
            Graph graphCopy = new Graph(this);

            // Словарь для меток. Первый параметр - вершина, второй - её метка.
            Dictionary<GraphVertex, int> listMarksOfVertices = new Dictionary<GraphVertex, int>();
            // Заполняем весь словарь. Метки изначально ставим максимальные
            foreach (var vertex in graphCopy.VertexEdges.Keys)
            {
                listMarksOfVertices.Add(vertex, int.MaxValue);
            }

            // Стартовую метку помечаем как 0
            listMarksOfVertices[startVertex] = 0;

            // Очередь не подходит т.к. нам нужно выбирать вершину для перехода по наименьшему пути
            // Словарь для вершин, которые нам нужно пройти
            // Фактически этот словарь нужен для сохранения других вершин, если в текущих вершинах тупик.
            // Обновляется в случае, если есть путь дальше

            // Создаём список из словарей, которые нам нужно пройти.
            // Словарь нужен для сохранения вершин и путей к ним от текущей.
            // Лист со словарём реализуют "уровневость" запоминания прохода по графу.
            // С помощью него мы можем возвращаться практически к основанию графа, в случае, если пути дальше не будет.
            List<Dictionary<GraphVertex, int>> vertexWay = new List<Dictionary<GraphVertex, int>>();
            // Инициализируем нулевой уровень - стартовая вершина
            vertexWay.Add(new Dictionary<GraphVertex, int>());
            // Заполняем возможные для посещения вершины от стартовой
            foreach (var edge in graphCopy.VertexEdges[startVertex])
            {
                vertexWay[0].Add(edge.SecondVertex, edge.EdgeWeight);
            }

            // Текущая вершина для обозначения вершины, с которой работаем на данный момент
            GraphVertex currentVertex = startVertex;

            // Инициализируем глубину уровней
            int dephtOfVertices = 0;

            // Пока есть пути, в которые мы можем пойти или пока есть вершины, которые мы не посетили
            while (vertexWay.Count != 0 && graphCopy.VertexEdges.Count != 0)
            {
                // Высчитываем по алгоритму минимальный вес до вершины
                foreach (var edge in graphCopy.VertexEdges[currentVertex])
                {
                    // Если сумма ребра и текущей метки меньше, чем метка конечной вершины, то заменяем метку конечной вершины этой суммой
                    if (edge.EdgeWeight + listMarksOfVertices[currentVertex] < listMarksOfVertices[edge.SecondVertex])
                    {
                        listMarksOfVertices[edge.SecondVertex] = edge.EdgeWeight + listMarksOfVertices[currentVertex];
                    }
                }

                // Если от текущей вершины есть другие пути
                if (graphCopy.VertexEdges[currentVertex].Count != 0)
                {
                    // Если есть другие пути, то поднимаемся на новый уровень
                    dephtOfVertices += 1;
                    // Инициализируем новый уровень
                    vertexWay.Add(new Dictionary<GraphVertex, int>());
                    // Создаём новый список возможных путей
                    foreach (var edge in graphCopy.VertexEdges[currentVertex])
                    {
                        vertexWay[dephtOfVertices].Add(edge.SecondVertex, edge.EdgeWeight);
                    }

                    // Сортируем по значению(по int) и записываем обратно в словарь
                    vertexWay[dephtOfVertices] = vertexWay[dephtOfVertices].
                                                OrderBy(vertex => vertex.Value).
                                                ToDictionary(pair => pair.Key, pair => pair.Value);

                    // Удаляем из списка смежности копии графа(для сохранности данных и удобного удаления) текущую вершину и считаем, что посетили её.
                    graphCopy.RemoveVertex(currentVertex.Name);
                    // Если больше не осталось вершин в списке смежности, то выходим из цикла.
                    if (graphCopy.VertexEdges.Count == 0)
                        break;

                    // Т.к. мы отсортировали возможные пути, то берём первый, т.к. он будет самым минимальным
                    currentVertex = vertexWay[dephtOfVertices].First().Key;
                    // Убираем из возможных путей текущий путь, чтобы не уйти в рекурсию
                    vertexWay[dephtOfVertices].Remove(currentVertex);
                }
                else // Если из текущей вершиные нет путей, то
                {
                    // Удаляем из списка смежности копии графа(для сохранности данных и удобного удаления) текущую вершину и считаем, что посетили её.
                    graphCopy.RemoveVertex(currentVertex.Name);
                    // Если больше не осталось вершин в списке смежности, то выходим из цикла.
                    if (graphCopy.VertexEdges.Count == 0)
                        break;

                    // Если же на текущем уровне, т.е. от текущей вершины некуда идти, то спускаемся на уровень ниже,
                    // пока не найдём уровень, на котором есть доступные пути.
                    while (dephtOfVertices >= 0 && vertexWay[dephtOfVertices].Keys.Count == 0)
                    {
                        vertexWay[dephtOfVertices].Clear();
                        dephtOfVertices -= 1;
                    }

                    // На случай, если есть недостижимые вершины, то глубина уйдёт в минус => прерываем цикл
                    if (dephtOfVertices < 0)
                        break;

                    currentVertex = vertexWay[dephtOfVertices].First().Key;
                    vertexWay[dephtOfVertices].Remove(currentVertex);
                }
            }

            return listMarksOfVertices;
        }

        public void Print_Dijkstra(string startVertex)
        {
            Console.WriteLine($"Min ways from {startVertex}: ");
            foreach (var item in Dijkstra(startVertex))
            {
                Console.Write(item + " ");
            }
        }

        // 6.Найти вершину, сумма длин кратчайших путей от которой до остальных вершин минимальна.
        public void Task_Dijkstra_IV_A_6()
        {
            List<Tuple<GraphVertex, int>> graphVerticesAndSums = new List<Tuple<GraphVertex, int>>();
            foreach (var vertex in VertexEdges.Keys)
            {
                // Нужно ли проверять вершины, которые не имеют пути только в одну другую вершину или нет?
                graphVerticesAndSums.Add(new Tuple<GraphVertex, int>(vertex, Dijkstra(vertex.Name).
                                                                              Where(pair => pair.Value != int.MaxValue).
                                                                              Sum(v => v.Value)));
                Print_Dijkstra(vertex.Name);
                Console.WriteLine();
            }

            foreach (var item in graphVerticesAndSums)
            {
                Console.WriteLine(item + " ");
            }

            Console.WriteLine($"Минимальная сумма длин кратчайших путей от которой " +
                $"до остальных вершин минимальна это : {graphVerticesAndSums.Min(pair => pair.Item2)}");
        }


        // Dnt wrk
        public void Task_IV_A_Dijkstra_6__MATRIX(string startVert)
        {
            GraphVertex startVertex = FindVertex(startVert);

            int int_startVertex = int.Parse(startVert.Remove(0, 1));

            bool[] marksInt = new bool[VertexEdges.Keys.Count];

            marksInt[int_startVertex] = true;

            //создаем матрицу с
            int[,] c_matrix = ToMatrix(0, true);
            for (int i = 0; i < VertexEdges.Keys.Count; ++i)
            {
                for (int j = 0; j < VertexEdges.Keys.Count; ++j)
                {
                    if (c_matrix[i, j] == 0)
                    {
                        c_matrix[i, j] = int.MaxValue;
                    }
                }
            }

            //создаем матрицы d и p
            long[] d = new long[VertexEdges.Keys.Count]; //все возможные пути из v
            int[] p = new int[VertexEdges.Keys.Count];  //кратчайшие маршруты
            for (int i = 0; i < VertexEdges.Keys.Count; i++) //цикл записывает в массив d все возможные пути из вершины v
                                                             // и кратчайшие маршруты в доступные вершины в массив p из вершины v
            {
                if (i != int_startVertex)
                {
                    d[i] = c_matrix[int_startVertex, i];
                    p[i] = int_startVertex;
                }
            }

            // Проходим по каждой вершине
            for (int i = 0; i < VertexEdges.Keys.Count - 1; i++)
            {
                // В первом цикле ищем вершину с самым маленьким путём и переходим на неё
                long min = int.MaxValue;
                int w = 0; // новая вершина с наименьшим значением веса(длина дороги и т.д.), на которую мы в последствии перейдём
                for (int u = 0; u < VertexEdges.Keys.Count; u++)
                {
                    if (marksInt[u] && min > d[u])
                    {
                        min = d[u];
                        w = u;
                    }
                }
                marksInt[w] = false; //помечаем вершину w как помеченную

                // Для каждой вершины, которую ещё не посетили(nov[u]), определяем кратчайший путь от
                // источника(w) до этой вершины
                for (int u = 0; u < VertexEdges.Keys.Count; u++)
                {
                    long distance = d[w] + c_matrix[w, u];// long - чтобы не переполнить int, если начнёт пребавляться int.MaxValue из c[w,u]
                    if (marksInt[u] && d[u] > distance)
                    {
                        d[u] = distance;
                        p[u] = w;
                    }
                }
            }

            Console.WriteLine("Длина кратчайшие пути от вершины {0} до вершины", int_startVertex);
            for (int i = 0; i < VertexEdges.Keys.Count; i++)
            {
                if (i != int_startVertex)
                {
                    Console.Write("{0} равна {1}, ", i, d[i]);
                    Console.Write("путь ");
                    if (d[i] != int.MaxValue)
                    {
                        Stack items = new Stack();
                        WayDijkstr(int_startVertex, i, p, ref items);
                        while (items.Count != 0)
                        {
                            Console.Write("{0} ", items.Pop());
                        }
                    }
                }
                Console.WriteLine();
            }
        }


        //public void Dijkstr(int v)
        //{
        //    graph.NovSet();//помечаем все вершины графа как непросмотренные
        //    int[] p;
        //    long[] d = graph.Dijkstr(v, out p); //запускаем алгоритм Дейкстры
        //                                        //анализируем полученные данные и выводим их на экран
        //    Console.WriteLine("Длина кратчайшие пути от вершины {0} до вершины", v);
        //    for (int i = 0; i < graph.Size; i++)
        //    {
        //        if (i != v)
        //        {
        //            Console.Write("{0} равна {1}, ", i, d[i]);
        //            Console.Write("путь ");
        //            if (d[i] != int.MaxValue)
        //            {
        //                Stack items = new Stack();
        //                graph.WayDijkstr(v, i, p, ref items);
        //                while (items.Count != 0)
        //                {
        //                    Console.Write("{0} ", items.Pop());
        //                }
        //            }
        //        }
        //        Console.WriteLine();
        //    }
        //}

        //восстановление пути от вершины a до вершины b для алгоритма Дейкстры
        public void WayDijkstr(int a, int b, int[] p, ref Stack items)
        {
            items.Push(b); //помещаем вершину b в стек
            if (a == p[b]) //если предыдущей для вершины b является вершина а, то
            {
                items.Push(a); //помещаем а в стек и завершаем восстановление пути
            }
            else //иначе метод рекурсивно вызывает сам себя для поиска пути
            { //от вершины а до вершины, предшествующей вершине b
                WayDijkstr(a, p[b], p, ref items);
            }
        }

        //реализация алгоритма Дейкстры
        //public long[] Dijkstr(int start_vertex, out int[] p)
        //{
        //    nov[start_vertex] = false; // помечаем вершину v как просмотренную

        //    //создаем матрицу с
        //    int[,] c = new int[Size, Size];
        //    for (int i = 0; i < Size; i++)
        //    {
        //        for (int u = 0; u < Size; u++)
        //        {
        //            if (array[i, u] == 0 || i == u)
        //            {
        //                c[i, u] = int.MaxValue;
        //            }
        //            else
        //            {
        //                c[i, u] = array[i, u];
        //            }
        //        }
        //    }

        //    //создаем матрицы d и p
        //    long[] d = new long[Size];//все возможные пути из v
        //    p = new int[Size];//кратчайшие маршруты
        //    for (int u = 0; u < Size; u++)//цикл записывает в массив d все возможные пути из вершины v
        //                                  // и кратчайшие маршруты в доступные вершины в массив p из вершины v
        //    {
        //        if (u != start_vertex)
        //        {
        //            d[u] = c[start_vertex, u];
        //            p[u] = start_vertex;
        //        }
        //    }

        //    // Проходим по каждой вершине
        //    for (int i = 0; i < Size - 1; i++)
        //    {

        //        // В первом цикле ищем вершину с самым маленьким путём и переходим на неё
        //        long min = int.MaxValue;
        //        int w = 0; // новая вершина с наименьшим значением веса(длина дороги и т.д.), на которую мы в последствии перейдём
        //        for (int u = 0; u < Size; u++)
        //        {
        //            if (nov[u] && min > d[u])
        //            {
        //                min = d[u];
        //                w = u;
        //            }
        //        }
        //        nov[w] = false; //помечаем вершину w как помеченную

        //        // Для каждой вершины, которую ещё не посетили(nov[u]), определяем кратчайший путь от
        //        // источника(w) до этой вершины
        //        for (int u = 0; u < Size; u++)
        //        {
        //            long distance = d[w] + c[w, u];// long - чтобы не переполнить int, если начнёт пребавляться int.MaxValue из c[w,u]
        //            if (nov[u] && d[u] > distance)
        //            {
        //                d[u] = distance;
        //                p[u] = w;
        //            }
        //        }
        //    }
        //    return d; //в качестве результата возвращаем массив кратчайших путей для
        //} //заданного источника

        #endregion

        #endregion
    }
}
