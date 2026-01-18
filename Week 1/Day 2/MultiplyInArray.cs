using System;

class MultiplyInArray
{
    public static void MulArrayElements()
    {
        int[] arr = { 1, 2, 3, 4 };
        int output = 1;

        if (arr.Length < 0)
        {
            output = -2;
        }
        else
        {
            foreach (int n in arr)
            {
                if (n > 0)
                    output *= n;
            }
        }

        Console.WriteLine("Product of all is: " + output);
    }
}
