using System;

class CompareArray
{
    public static void CompareArrays()
    {
        int[] arr1 = { 4, 6, 8 };
        int[] arr2 = { 5, 3, 9 };
        int[] output = new int[arr1.Length];

        if (arr1.Length < 0 || arr2.Length < 0)
        {
            output[0] = -2;
        }
        else
        {
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] < 0 || arr2[i] < 0)
                {
                    output[0] = -1;
                    break;
                }
                output[i] = arr1[i] > arr2[i] ? arr1[i] : arr2[i];
            }
        }

        foreach (int n in output)
            Console.Write(n + " ");
    }
}
