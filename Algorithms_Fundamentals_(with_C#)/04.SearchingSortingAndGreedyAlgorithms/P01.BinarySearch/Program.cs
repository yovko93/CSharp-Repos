﻿using System;
using System.Linq;

namespace P01.BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int key = int.Parse(Console.ReadLine());

            int result = BinarySearch(arr, key);

            Console.WriteLine($"{result}");
        }

        public static int BinarySearch(int[] arr, int key)
        {
            var left = 0;
            var right = arr.Length - 1;

            while (left <= right)
            {
                var mid = (left + right) / 2;

                var element = arr[mid];

                if (element == key)
                {
                    return mid;
                }

                if (key > element)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }
    }
}
