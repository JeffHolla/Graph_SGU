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
        public Dictionary<GraphVertex, List<GraphEdge>> VertexEdges { get; }
        // Список классов граней в виде --  Вершина - Список граней  --  Класс граней хранит в себе доп инфу по сути.


        public int node;
        public int cost;
        public int[] other_nodes;
        public int city_left_to_expand;
        public Stack<int> inStack;
        public static int sizeOfAll;

        public int[,] matrix;

        public void ToMatrix()
        {
            matrix = new int[VertexEdges.Count, VertexEdges.Count];

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                //List<int> lst = VertexEdges[FindVertex("V" + i)].Select(x => x.EdgeWeight).ToList();

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
                    matrix[int.Parse(item.SecondVertex.Name.Remove(0, 1)), int.Parse(pair.Key.Name.Remove(0, 1))] = item.EdgeWeight;
                }
            }
        }

        public void PrintMatrix()
        {
            ToMatrix();

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    if (i != j)
                        Console.Write("{0, 3} ", matrix[i, j]);
                    else
                        Console.Write("inf ");
                }

                Console.WriteLine();
            }
        }

        public void Doklad()
        {
            ToMatrix();

            List<Tuple<int, int>> _arcs = new List<Tuple<int, int>>();
            List<int> _solution = new List<int>();

            _solution.Add(0);

            //while (_arcs.Count != 0)
            //{
            //    var iter = _arcs.First();
            //    while (iter != _arcs.Last())
            //    {
            //        // если есть ребро, исходящее из последней вершины
            //        // добавление в решение смежной вершины
            //        // и удаление этого ребра из списка
            //        if (iter->first == _solution.back())
            //        {
            //            _solution.push_back(iter->second);
            //            iter = _arcs.erase(iter);
            //        }
            //        else
            //            ++iter;
            //    }
            //}
        }

        public void Doklad_2()
        {
            ToMatrix();


            var start_time = DateTime.Now;

            int x = Reduce(matrix, 0, int.MaxValue, int.MaxValue);
            Graph user_obj = new Graph(VertexEdges.Count)
            {
                node = 0,
                cost = x,
                matrix = matrix,
                other_nodes = new int[VertexEdges.Count - 1],
                city_left_to_expand = VertexEdges.Count - 1
            };


            user_obj.inStack.Push(0);


            for (int i = 0; i < VertexEdges.Count - 1; i++)
            {
                user_obj.other_nodes[i] = i + 1;
            }

            // variable for keeping track of the number of expanded nodes
            // переменная для отслеживания количества расширенных узлов
            int count = 0;

            // Stack DS for maintaining the nodes in the tree
            // Стек DS для поддержания узлов в дереве
            Stack<Graph> stack_objs = new Stack<Graph>();

            // Pushing the root onto the stack
            // Помещаем корень в стек
            stack_objs.Push(user_obj);

            // Temporary variable for storing the best solution found so far
            // Временная переменная для хранения лучшего найденного решения
            Graph temp_best_solution = new Graph(VertexEdges.Count);
            int current_best_cost = 100000;

            // Output(user_obj);

            // Stack iterations including backtracking
            //      Итерации стека, включая возврат
            // Запускаем обхода дерева - проходим до тех пор, пока стек не станет пустым, т.е. все узлы не будут расширены(раскрыты).
            while (stack_objs.Count != 0)
            {
                // Variable Initialization
                List<Graph> mainList = new List<Graph>();
                Console.WriteLine();


                Graph last_node_from_stack = stack_objs.Pop();

                //Console.WriteLine("!!!!!!!!!!!!!!!--hell Var :--!!!!!!!!!!!!!!!");
                //Output(hell);
                //Console.WriteLine();

                // Expand Stack only if the node is not a leaf node and if its cost is better than the best so far
                // Разверните Stack, только если узел не является листовым и если его стоимость лучше, чем лучшая на данный момент
                if (last_node_from_stack.city_left_to_expand == 0)
                {
                    // Comparing the cost of this node to the best and updating if necessary
                    // Сравниваем стоимость этого узла с лучшими и обновляем при необходимости
                    if (last_node_from_stack.cost <= current_best_cost)
                    {
                        temp_best_solution = last_node_from_stack;
                        current_best_cost = temp_best_solution.cost;
                    }
                }
                else if (last_node_from_stack.city_left_to_expand != 0)
                {
                    if (last_node_from_stack.cost <= current_best_cost)
                    {
                        count++;
                        // Expanding the latest node popped from stack
                        // Разворачиваем последний узел, извлеченный из стека
                        mainList = Expand(last_node_from_stack);

                        // Determing the order in which the expanded nodes should be pushed onto the stack
                        // Определение порядка, в котором развернутые узлы должны быть помещены в стек
                        int[] arrow = new int[mainList.Count()];

                        for (int pi = 0; pi < mainList.Count(); pi++)
                        {
                            Graph help = mainList.ElementAt(pi);
                            arrow[pi] = help.cost;
                        }

                        // Sorting nodes in decreasing order based on their costs
                        // Сортировка узлов в порядке убывания в зависимости от их стоимости
                        int[] sorted_nodes = DecreasingSort(arrow);
                        for (int pi = 0; pi < sorted_nodes.Length; pi++)
                        {
                            // Pushing the node objects into stack in decreasing order
                            stack_objs.Push(mainList.ElementAt(sorted_nodes[pi]));

                        }
                    }
                }
            }

            // Calculating Stop time for Run Time
            var stop_time = DateTime.Now;
            var run_time = stop_time - start_time;

            // Checking if a solution is found
            // Проверяем найдено ли было решение
            if (temp_best_solution.cost < 9000)
            {
                // Printing Tour Cost
                // Печать цены маршрута
                Console.WriteLine();
                Console.Write("Current Best Cost = ");
                Console.WriteLine(current_best_cost);
                Console.WriteLine();

                // Printing Optimal Tour
                // Печать оптимального маршрута
                Console.Write("[ ");
                for (int st_i = 0; st_i < VertexEdges.Count; st_i++)
                {
                    Console.Write(temp_best_solution.inStack.ElementAt(st_i));

                    Console.Write(", ");
                }
                Console.Write("0 ");
                Console.Write("]");
                Console.WriteLine();
                Console.WriteLine();

                // Printing Running Time
                Console.WriteLine(run_time);
                Console.WriteLine();

                //The number of nodes expanded in the algorithm
                // Количество node развёрнутых в алгоритме
                Console.WriteLine(count);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nNo Solution.\n");
                // Printing Running Time
                Console.WriteLine(run_time);
                Console.WriteLine();
            }
        }


       

        



        // Уменьшает каждый элемент массива на значение min
        public int[] Min(int[] array, int min)
        {
            // Каждый элемент массива уменьшаем на значение min
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] - min;
            }

            // Возвращается уменьшенный массив
            return array;
        }

        /*
        Метод - Вычисляет минимальное значение, с помощью которого матрица может быть уменьшена
        Входные данные  - Массив, для которого необходимо вычислить минимальное значение (может быть строка или столбец из матрицы)
        Return - Минимальное значение
        */
        public int Minimum(int[] array)
        {
            // Объявляем по умолчанию значение, которое меньшее, чем бесконечность, 
            // но большее, чем допустимые значения
            int declaredMin = 9000;

            // Находим минимальное значение в массиве
            int min_in_array = array.Min();

            // Если минимальное значение больше, чем условное большее допустимого, т.е. min_in_array > 9000
            if (min_in_array > declaredMin)
            {
                // Возвращаем 0 в качестве сигнала, что уменьшать нечего
                return 0;
            }
            // Иначе возвращаем минимальный элемент
            else
            {
                return min_in_array;
            }
        }

        public void Output(Graph node)
        {
            Console.WriteLine("============================================");
            Console.WriteLine("This city is :" + node.node);
            Console.WriteLine("The node cost function:" + node.cost);
            // Printing remaining cities
            Console.WriteLine("The remaining cities to be expanded from this node");
            for (int i = 0; i < node.other_nodes.Length; i++)
            {
                Console.Write(node.other_nodes[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("The number of possible remaining expansions from this node are" + node.city_left_to_expand);
            Console.WriteLine();
            Console.WriteLine("============================================");
        }

        public List<Graph> Expand(Graph objsToTravel)
        {
            List<Graph> lst = new List<Graph>();

            // Точки, которые нужно посетить
            int length = objsToTravel.other_nodes.Length;
            for (int i = 0; i < length; i++)
            {
                // Захардкоженный костыль для node 0
                if (objsToTravel.other_nodes[i] == 0) continue;

                // Сохраняем значения в переменные
                int cost = objsToTravel.cost;
                int node = objsToTravel.node;
                Stack<int> inStack = new Stack<int>();

                Console.WriteLine($"Objects in Stack({objsToTravel.node})");

                for (int st_i = 0; st_i < objsToTravel.inStack.Count; st_i++)
                {

                    int k = objsToTravel.inStack.ElementAt(st_i);
                    Console.WriteLine(k);
                    inStack.Push(k);
                }

                inStack.Push(objsToTravel.other_nodes[i]);



                // Fetching matrix contents into a temporary matrix for reduction
                // Извлечение содержимого матрицы во временную матрицу для сокращения
                int[,] temp_arr_2d = new int[sizeOfAll, sizeOfAll];

                
                //Просто копирка
                for (int i_1 = 0; i_1 < sizeOfAll; i_1++)
                {
                    for (int i_2 = 0; i_2 < sizeOfAll; i_2++)
                    {
                        temp_arr_2d[i_1, i_2] = objsToTravel.matrix[i_1, i_2];
                    }
                }

                // Вывод tempaaray
                for (int i_1 = 0; i_1 < sizeOfAll; i_1++)
                {
                    for (int i_2 = 0; i_2 < sizeOfAll; i_2++)
                    {
                        if (temp_arr_2d[i_1, i_2] == int.MaxValue)
                            Console.Write("{0, 3} ", "inf");
                        else
                            Console.Write("{0, 3} ", temp_arr_2d[i_1, i_2]);
                    }
                    Console.WriteLine();
                }

                // Adding the value of edge (i,j) to the cost
                // Добавление значения edge (i, j) к стоимости
                cost = cost + temp_arr_2d[node, objsToTravel.other_nodes[i]];

                // Making the ith row and jth column to be infinity
                // Делаем i-ю строку и j-й столбец бесконечными
                for (int j = 0; j < sizeOfAll; j++)
                {
                    temp_arr_2d[node, j] = int.MaxValue;
                    temp_arr_2d[j, objsToTravel.other_nodes[i]] = int.MaxValue;
                }

                // Making (j,0) to be infinity
                // Превращение (j, 0) в бесконечность
                temp_arr_2d[objsToTravel.other_nodes[i], 0] = int.MaxValue;

                //Reducing this matrix according to the rules specified
                int cost1 = Reduce(temp_arr_2d, cost, node, objsToTravel.other_nodes[i]);

                // Updating object contents corresponding to current city tour
                Graph finall = new Graph(sizeOfAll)
                {
                    node = objsToTravel.other_nodes[i],
                    cost = cost1,
                    matrix = temp_arr_2d
                };

                int[] temp_arr_1d = new int[objsToTravel.other_nodes.Length];

                // Limiting the expansion in case of backtracking
                // Ограничение раскрытия при возврате
                for (int i_3 = 0; i_3 < temp_arr_1d.Length; i_3++)
                {
                    temp_arr_1d[i_3] = objsToTravel.other_nodes[i_3];
                }

                temp_arr_1d[i] = 0;

                finall.other_nodes = temp_arr_1d;
                finall.city_left_to_expand = objsToTravel.city_left_to_expand - 1;
                finall.inStack = inStack;

                lst.Add(finall);
            }

            return lst;
        }

        /*
        Reduce - Reduces the passed Matrix with minimum value possible
               - Уменьшает пройденную матрицу до минимально возможного значения
        Input - 2D Array to be reduced, Previous Step's Cost, Row to be processed, Column to be processed
              - 2D-массив, подлежащий уменьшению, стоимость предыдущего шага, обрабатываемая строка, обрабатываемый столбец
        Return - Cost of Reduction
               - Стоимость уменьшения
        */
        public int Reduce(int[,] array, int cost, int row, int column)
        {
            // Переменные, чтобы хранить строки и столбца для уменьшения
            int[] array_to_reduce = new int[sizeOfAll];
            int[] reduced_array = new int[sizeOfAll];

            // Переменная, чтобы хранить обновлённую цену
            int new_cost = cost;
            
            // Цикл для уменьшения строк
            for (int i = 0; i < sizeOfAll; i++)
            {
                // Если строка совпадает с текущим городом - не уменьшаем
                if (i == row) continue;

                // If the row is not corresponding current city, try to reduce
                // Если строка не текущий город, то пытаемся уменьшить
                for (int j = 0; j < sizeOfAll; j++)
                {
                    // Извлекаем строку, которую нужно уменьшить
                    array_to_reduce[j] = array[i, j];
                }

                // Проверяем может ли текущая строка быть уменьшена
                if (Minimum(array_to_reduce) != 0)
                {
                    // Обновляем новую стоимость
                    new_cost = Minimum(array_to_reduce) + new_cost;
                    
                    // Уменьшаем значения в строке (строку)
                    reduced_array = Min(array_to_reduce, Minimum(array_to_reduce));
                    
                    // Возвращаем уменьшенную строку в исходный массив
                    for (int k = 0; k < sizeOfAll; k++)
                    {
                        array[i, k] = reduced_array[k];
                    }
                }
            }
            
            // Цикл для уменьшения столбцов
            for (int i = 0; i < sizeOfAll; i++)
            {
                // Если столбец совпадает с текущим городом, то пропускаем одну итерацию
                if (i == column) continue;

                // Если столбец не совпадает с текущим городом, то уменьшаем
                for (int j = 0; j < sizeOfAll; j++)
                {
                    // Извлекаем столбец, который нужно уменьшить
                    array_to_reduce[j] = array[j, i];
                }
                
                // Проверяем может ли текущий столбец быть уменьшен
                if (Minimum(array_to_reduce) != 0)
                {
                    
                    // Обновляем текущую цену
                    new_cost = Minimum(array_to_reduce) + new_cost;

                    
                    // Уменьшаем колонку
                    reduced_array = Min(array_to_reduce, Minimum(array_to_reduce));
                    

                    // Возвращаем уменьшенный столбец в исходный массив
                    for (int k = 0; k < sizeOfAll; k++)
                    {
                        array[k, i] = reduced_array[k];
                    }
                }
            }

            // Уменьшение выполнено, возвращаем новую цену
            return new_cost;
        }

        public int[] DecreasingSort(int[] temp)
        {
            int[] y = new int[temp.Length];
            // Retreiving Array contents
            // Получение содержимого массива

            for (int j = 0; j < temp.Length; j++)
            {
                y[j] = temp[j];
            }


            // Sorting
            int tmp_value = 0;
            for (int i = 0; i < temp.Length - 1; i++)
            {
                if (temp[i] < temp[i + 1])
                {
                    tmp_value = temp[i];
                    temp[i] = temp[i + 1];
                    temp[i + 1] = tmp_value;
                }
            }

            int[] to_be_returned = new int[temp.Length];

            // Putting sorted contents into array to be returned
            for (int i = 0; i < temp.Length; i++)
            {
                for (int j = 0; j < temp.Length; j++)
                {
                    if (temp[i] == y[j])
                    {
                        to_be_returned[i] = j;
                    }
                }
            }

            return to_be_returned;
        }



        // Остальное
        // =======================================================================================
        // Граф под доклад
        public Graph(int number)
        {
            VertexEdges = new Dictionary<GraphVertex, List<GraphEdge>>();

            matrix = new int[number, number];
            inStack = new Stack<int>();

            sizeOfAll = matrix.GetLength(0);
        }

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

        public void RemoveVertex(string name)
        {
            if (FindVertex(name) != null)
            {

                // Проходим по каждому ребру из удаляемой вершины
                foreach (var edge in VertexEdges[FindVertex(name)])
                {
                    // Прыгаем по конечным точкам (в GraphEdge указываются только конечная точка ребра) ребра вершины и удаляем их.
                    // VertexEdges[edge.SecondVertex] - прыжок в конечную вершину ребра и просмотр из этой вершины остальных рёбер
                    // Remove(... .Find(v => v.SecondVertex == FindVertex(name)) - поиск и удаление ребра из списка рёбер VertexEdges
                    VertexEdges[edge.SecondVertex].Remove(VertexEdges[edge.SecondVertex].Find(v => v.SecondVertex == FindVertex(name)));
                }

                // Находим ребро в словаре и удаляем
                VertexEdges.Remove(FindVertex(name));


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
    }
}
