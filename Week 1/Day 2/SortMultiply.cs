using System;

class SortMultiply
{
    public static void FindSortAndMultiply()
    {
        int[] input1 = { 1, 2, 3, 4, 5 };
        int[] input2 = { 9, 8, 7, 6, 5 };
        int size = input1.Length;
        int[] output = new int[size];

        if (size < 0)
        {
            output[0] = -2;
        }
        else
        {
            foreach (int n in input1)
                if (n < 0) { output[0] = -1; Console.WriteLine(output[0]); return; }

            foreach (int n in input2)
                if (n < 0) { output[0] = -1; Console.WriteLine(output[0]); return; }

            Array.Sort(input1);
            Array.Sort(input2);
            Array.Reverse(input2);

            for (int i = 0; i < size; i++)
            {
                output[i] = input1[i] * input2[size - i - 1];
            }
        }

        foreach (int n in output)
            Console.Write(n + " ");
    }
}
