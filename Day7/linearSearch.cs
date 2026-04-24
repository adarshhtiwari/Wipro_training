using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Searching_N_Sorting_in_C_
{
    internal class linearSearch
    {
        public static int LinearSea(int[] arr, int element)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == element)
                {
                    return i; // Return the index of the found element
                }
            }
            return -1; // Return -1 if the element is not found
        }
    }
}

