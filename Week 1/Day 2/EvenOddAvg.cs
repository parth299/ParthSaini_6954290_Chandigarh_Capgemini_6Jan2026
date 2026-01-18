using System;

class EvenOddAvg
{
    public static void FindEvenOddAvg()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6 };
        int output1 = 0;

        if (arr.Length < 0)
        {
            output1 = -2;
        }
        else
        {
            int evenSum = 0;
            int oddSum = 0;

            foreach (int num in arr)
            {
                if (num < 0)
                {
                    output1 = -1;
                    Console.WriteLine(output1);
                    return;
                }

                if (num % 2 == 0)
                    evenSum += num;
                else
                    oddSum += num;
            }

            output1 = (evenSum + oddSum) / 2;
            Console.WriteLine("Even sum: " + evenSum);
            Console.WriteLine("Odd sum: " + oddSum);
        }

        Console.WriteLine(output1);
    }
}
