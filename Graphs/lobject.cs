using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Graphs.Graphs
{
    public class UserClass
    {
        public int node;
        public int cost;
        public int[,] matrix;
        public int[] other_nodes;
        public int city_left_to_expand;
        public Stack<int> inStack;
        public static int sizeOfAll;
        public UserClass(int number)
        {
            matrix = new int[number, number];
            inStack = new Stack<int>();

            sizeOfAll = matrix.GetLength(0);
        }
    }
    class Program
    {
        //static int tmp_value = 6;
        /* 
         * Recurse through array and reduce with the passed "min" value
         * Return reduced array
        */
        public static int[] Min(int[] array, int min)
        {
            // Recurse through array and reduce with the passed "min" value
            for (int j = 0; j < array.Length; j++)
            {
                array[j] = array[j] - min;
            }
            // Return reduced array
            return array;
        }

        /*
        Minimum Function - Calculates the minimum value with which a matrix can be reduced
        Input  - Array for which minimum value is to be calculated
        Return - Minimum value
        */
        public static int Minimum(int[] array)
        {
            // Declaring default as something lesser than infinity but higher than valid values
            int min = 9000;
            // Recursing through array to find minimum value
            for (int i = 0; i < array.Length; i++)
            {
                // If value is valid i.e. less than infinity, reset min with that value
                if (array[i] < min)
                {
                    min = array[i];
                }
            }
            // Check if min value is unchanged i.e. we met an infinity array
            if (min == 9000)
            {
                // Return 0 as nothing to reduce
                return 0;
            }
            // Else return the min value
            else
            {
                return min;
            }
        }

        public static void Output(UserClass l1)
        {
            Console.WriteLine("============================================");
            Console.WriteLine("============================================");
            Console.WriteLine("This city is :" + l1.node);
            Console.WriteLine("The node cost function:" + l1.cost);
            // Printing remaining cities
            Console.WriteLine("The remaining cities to be expanded from this node");
            for (int h = 0; h < l1.other_nodes.Length; h++)
            {
                Console.Write(l1.other_nodes[h]);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("The number of possible remaining expansions from this node are" + l1.city_left_to_expand);
            Console.WriteLine();
            Console.WriteLine("============================================");
            Console.WriteLine("============================================");
        }

        public static void Expand(List<UserClass> lst, UserClass iterObj)
        {
            // Number of cities to be traversed
            int length = iterObj.other_nodes.Length;
            for (int i = 0; i < length; i++)
            {
                // Variable Initialization
                if (iterObj.other_nodes[i] == 0) continue;

                int cost = 0;
                cost = iterObj.cost;
                int node = iterObj.node;
                Stack<int> inStack = new Stack<int>();

                for (int st_i = 0; st_i < iterObj.inStack.Count; st_i++)
                {

                    int k = iterObj.inStack.ElementAt(st_i);
                    Console.WriteLine(k);
                    inStack.Push(k);
                }

                inStack.Push(iterObj.other_nodes[i]);



                // Fetching matrix contents into a temporary matrix for reduction
                int[,] temparray = new int[UserClass.sizeOfAll, UserClass.sizeOfAll];

                for (int i_1 = 0; i_1 < UserClass.sizeOfAll; i_1++)
                {
                    for (int i_2 = 0; i_2 < UserClass.sizeOfAll; i_2++)
                    {
                        temparray[i_1, i_2] = iterObj.matrix[i_1, i_2];
                    }
                }
                //Adding the value of edge (i,j) to the cost
                cost = cost + temparray[node, iterObj.other_nodes[i]];

                //Making the ith row and jth column to be infinity
                for (int j = 0; j < UserClass.sizeOfAll; j++)
                {
                    temparray[node, j] = 9999;
                    temparray[j, iterObj.other_nodes[i]] = 9999;
                }

                //Making (j,0) to be infinity
                temparray[iterObj.other_nodes[i], 0] = 9999;

                //Reducing this matrix according to the rules specified
                int cost1 = Reduce(temparray, cost, node, iterObj.other_nodes[i]);

                // Updating object contents corresponding to current city tour
                UserClass finall = new UserClass(UserClass.sizeOfAll)
                {
                    node = iterObj.other_nodes[i],
                    cost = cost1,
                    matrix = temparray
                };

                int[] temp_array = new int[iterObj.other_nodes.Length];

                // Limiting the expansion in case of backtracking
                for (int i_3 = 0; i_3 < temp_array.Length; i_3++)
                {
                    temp_array[i_3] = iterObj.other_nodes[i_3];
                }

                temp_array[i] = 0;

                finall.other_nodes = temp_array;
                finall.city_left_to_expand = iterObj.city_left_to_expand - 1;
                finall.inStack = inStack;

                lst.Add(finall);
            }
        }

        /*
        Reduce - Reduces the passed Matrix with minimum value possible
        Input - 2D Array to be reduced, Previous Step's Cost, Row to be processed, Column to be processed
        Return - Cost of Reduction
        */
        public static int Reduce(int[,] array, int cost, int row, int column)
        {
            //int tmp_value = 5;

            // Variables
            // Arrays to store rows and columns to be reduced
            int[] array_to_reduce = new int[UserClass.sizeOfAll];
            int[] reduced_array = new int[UserClass.sizeOfAll];

            // Variable to store updated cost
            int new_cost = cost;

            // Loop for reducing rows
            for (int i = 0; i < UserClass.sizeOfAll; i++)
            {
                // If the row matches current city, do not reduce
                if (i == row) continue;

                // If the row is not corresponding current city, try to reduce
                for (int j = 0; j < UserClass.sizeOfAll; j++)
                {
                    // Fetch the row to be reduced
                    array_to_reduce[j] = array[i, j];
                }

                // Check if current row can be reduced
                if (Minimum(array_to_reduce) != 0)
                {
                    // Updating new cost
                    new_cost = Minimum(array_to_reduce) + new_cost;

                    // Reducing the row
                    reduced_array = Min(array_to_reduce, Minimum(array_to_reduce));

                    // Pushing the reduced row back into original array
                    for (int k = 0; k < UserClass.sizeOfAll; k++)
                    {
                        array[i, k] = reduced_array[k];
                    }
                }
            }
            // Loop for reducing columns
            for (int i = 0; i < UserClass.sizeOfAll; i++)
            {
                // If column matches current city, do not reduce
                if (i == column) continue;

                // If column does not match current city, try to reduce
                for (int j = 0; j < UserClass.sizeOfAll; j++)
                {
                    // Fetching column to be reduced
                    array_to_reduce[j] = array[j, i];
                }

                // Check if current column can be reduced
                if (Minimum(array_to_reduce) != 0)
                {
                    // Updating current cost
                    new_cost = Minimum(array_to_reduce) + new_cost;

                    // Reducing the column
                    reduced_array = Min(array_to_reduce, Minimum(array_to_reduce));

                    // Pushing the reduced column back into original array
                    for (int k = 0; k < UserClass.sizeOfAll; k++)
                    {
                        array[k, i] = reduced_array[k];
                    }
                }
            }
            // Reduction done, return the new cost
            return new_cost;
        }

        public static int[] DecreasingSort(int[] temp)
        {
            int[] y = new int[temp.Length];
            // Retreiving Array contents

            for (int j = 0; j < temp.Length; j++)
            {
                y[j] = temp[j];
            }
            int x = 0;
            // Sorting

            for (int i = 0; i < temp.Length - 1; i++)
            {
                if (temp[i] < temp[i + 1])
                {
                    x = temp[i];
                    temp[i] = temp[i + 1];
                    temp[i + 1] = x;
                }
            }
            int[] to_be_returned = new int[temp.Length];
            
            // Putting sorted contents into array to be returned
            for (int j = 0; j < temp.Length; j++)
            {
                for (int j1 = 0; j1 < temp.Length; j1++)
                {
                    if (temp[j] == y[j1])
                    {
                        to_be_returned[j] = j1;
                    }
                }
            }

            return to_be_returned;
        }

    }
}