using System;

class Search
{
    public static void SearchElement()
    {
        int[] arr = { 3, 6, 9, 12 };
        int search = 9;
        int output = 1;

        if (arr.Length < 0)
        {
            output = -2;
        }
        else
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 0)
                {
                    output = -1;
                    break;
                }
                if (arr[i] == search)
                {
                    output = i;
                    break;
                }
            }
        }

        Console.WriteLine(output);
    }
}
