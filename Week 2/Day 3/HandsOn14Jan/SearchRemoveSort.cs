using System;

class RemoveAndSortArray
{
    public static int[] ProcessArray(int[] input1, int input2, int input3)
    {
        if (input2 < 0)
            return new int[] { -2 };

        foreach (int val in input1)
        {
            if (val < 0)
                return new int[] { -1 };
        }

        int index = Array.IndexOf(input1, input3);
        if (index == -1)
            return new int[] { -3 };

        int[] result = new int[input2 - 1];
        int j = 0;

        for (int i = 0; i < input2; i++)
        {
            if (i != index)
                result[j++] = input1[i];
        }

        Array.Sort(result);
        return result;
    }

    public static void Main()
    {
        int[] arr = { 54, 26, 78, 32, 55 };
        int[] output = ProcessArray(arr, 5, 78);

        foreach (int val in output)
            Console.Write(val + " ");
    }
}
