using System;

class MostRepeated
{
    public static void FindMostRepeated()
    {
        int[] input = { 2, 2, 2, 2, 3, 3, 3, 3, 4 };
        int n = input.Length;

        int maxCount = 0;

        for (int i = 0; i < n; i++)
        {
            int count = 1;

            bool alreadyCounted = false;
            for (int k = 0; k < i; k++)
            {
                if (input[i] == input[k])
                {
                    alreadyCounted = true;
                    break;
                }
            }

            if (alreadyCounted)
                continue;

            for (int j = i + 1; j < n; j++)
            {
                if (input[i] == input[j])
                    count++;
            }

            if (count > maxCount)
                maxCount = count;
        }

        for (int i = 0; i < n; i++)
        {
            int count = 1;
            bool alreadyPrinted = false;

            for (int k = 0; k < i; k++)
            {
                if (input[i] == input[k])
                {
                    alreadyPrinted = true;
                    break;
                }
            }

            if (alreadyPrinted)
                continue;

            for (int j = i + 1; j < n; j++)
            {
                if (input[i] == input[j])
                    count++;
            }

            if (count == maxCount)
                Console.Write(input[i] + " ");
        }
    }
}
