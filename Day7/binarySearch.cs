using System;
using System.Collections.Generic;
using System.Text;

namespace Searching_N_Sorting_in_C_
{
    internal class binarySearch
    {
        //Step 1 : Create a method for binary search that takes a sorted array and an element to be searched as parameters
        //Step 2 : Initialize two pointers, one at the beginning of the array and another at the end of the array
        //Step 3 : While the left pointer is less than or equal to the right pointer, do the following:
        //Step 4 : Calculate the mid index of the array
        //Step 5 : If the element at the mid index is equal to the element to be searched, return the mid index
        //Step 6 : If the element at the mid index is less than the element to be searched, move the left pointer to mid + 1
        //Step 7 : If the element at the mid index is greater than the element to be searched, move the right pointer to mid - 1
        //Note: The array must be sorted in ascending order for binary search to work correctly.
        //If the element is not found in the array, return -1.
        public static int Search(int[] arr, int target)
        {
            int left = 0;
            int right = arr.Length - 1;//pointing to the last index of the array
            while (left <= right)
            {
                int mid = left + (right - left) / 2;// Calculate the mid index to avoid overflow
                if (arr[mid] == target)
                {
                    return mid; // Element found at index mid
                }
                else if (arr[mid] < target)
                {
                    left = mid + 1; // Move the left pointer to mid + 1
                }
                else
                {
                    right = mid - 1; // Move the right pointer to mid - 1
                }
            }
            return -1; // Element not found in the array
        }
    }
}
