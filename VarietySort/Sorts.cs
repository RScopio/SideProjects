using System;
using System.Collections;
using System.Collections.Generic;
public static class Sorts
{
    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static void Quick<T>(T[] arr, Comparison<T> c)
    {
        if (arr.Length <= 1)
        {
            return;
        }

        Sort(0, arr.Length - 1);

        void Sort(int lo, int hi)
        {
            if (lo < hi)
            {
                int p = Partition(lo, hi);
                Sort(lo, p - 1);
                Sort(p + 1, hi);
            }
        }

        int Partition(int lo, int hi)
        {
            var pivot = arr[hi];
            int i = lo - 1;
            for (int j = lo; j < hi; j++)
            {
                if (c(arr[j], pivot) < 0)
                {
                    i++;
                    Swap(ref arr[i], ref arr[j]);
                }
            }
            Swap(ref arr[i + 1], ref arr[hi]);
            return i;
        }
    }

    public static void Bogo<T>(T[] arr, Comparison<T> c)
    {
        if (arr.Length <= 1)
        {
            return;
        }
        Random rand = new Random(Guid.NewGuid().GetHashCode());

        while (!Sorted())
        {
            Shuffle();
        }

        bool Sorted()
        {
            bool sorted = true;
            for (int i = 1; i < arr.Length; i++)
            {
                if (c(arr[i], arr[i - 1]) < 0)
                {
                    sorted = false;
                }
            }
            return sorted;
        }

        void Shuffle()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int swapIndex = rand.Next(arr.Length);
                Swap(ref arr[swapIndex], ref arr[i]);
            }
        }
    }
    public static void Insertion<T>(T[] arr, Comparison<T> c)
    {
        if (arr.Length <= 1)
        {
            return;
        }
        for (int i = 1; i < arr.Length; i++)
        {
            for (int j = i; j > 0 && c(arr[j], arr[j - 1]) < 0; j--)
            {
                Swap(ref arr[j], ref arr[j - 1]);
            }
        }
    }

    public static void Bubble<T>(T[] arr, Comparison<T> c)
    {
        if (arr.Length <= 1)
        {
            return;
        }
        bool swap = true;
        while (swap)
        {
            swap = false;
            for (int j = 1; j < arr.Length; j++)
            {
                if (c(arr[j - 1], arr[j]) > 0)
                {
                    swap = true;
                    Swap(ref arr[j], ref arr[j - 1]);
                }
            }
        }
    }
}