using System;
using System.Collections;
using System.Collections.Generic;

namespace VarietySort
{
    class Program
    {
        static (T[], T[]) Split<T>(T[] arr, int cut)
        {
            (T[] left, T[] right) split;
            split.left = new T[cut];
            split.right = new T[arr.Length - cut];
            for (int i = 0; i < cut; i++)
            {
                split.left[i] = arr[i];
            }
            for (int i = 0; i < arr.Length - cut; i++)
            {
                split.right[i] = arr[i + cut];
            }
            return split;
        }

        static T[] Merge<T>(T[] a, T[] b, Comparison<T> c)
        {
            T[] output = new T[a.Length + b.Length];

            int i = 0, j = 0, k = 0;

            while (i < a.Length && j < b.Length)
            {
                int temp = c(a[i], b[j]);
                if (temp < 0)
                {
                    output[k] = a[i];
                    i++;
                }
                else
                {
                    output[k] = b[j];
                    j++;
                }
                k++;
            }

            while (i < a.Length)
            {
                output[k] = a[i];
                i++;
                k++;
            }

            while (j < b.Length)
            {
                output[k] = b[j];
                j++;
                k++;
            }

            return output;
        }

        static T[] VarietySort<T>(T[] arr, Comparison<T> c, params Action<T[], Comparison<T>>[] sorts)
        {
            if (sorts.Length > arr.Length) throw new ArgumentException("no");

            T[][] subArrays = new T[sorts.Length][];
            T[] leftover = arr;
            for (int i = 0; i < sorts.Length; i++)
            {
                (T[] left, T[] right) split = Split(leftover, arr.Length / sorts.Length);
                subArrays[i] = split.left;
                leftover = split.right;
                sorts[i](subArrays[i], c);
            }

            T[] output = subArrays[0];
            for (int i = 1; i < subArrays.GetLength(0); i++)
            {
                output = Merge(output, subArrays[i], c);
            }
            return output;
        }

        static void Main(string[] args)
        {
            int[] array = { 1, 10, 12, 3, 8, 11, 4, 9, 13, 16, 6, 2, 7, 15, 14, 5 };

            array = VarietySort(
                array,
                (int a, int b) => a.CompareTo(b),
                Sorts.Insertion,
                Sorts.Bubble,
                Sorts.Bogo,
                Sorts.Quick);

            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
        }
    }
}
