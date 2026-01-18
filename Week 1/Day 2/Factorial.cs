using System;

class Factorial
{
    public static void FindFactorial()
    {
        int number = 5;
        int output1 = 1;

        if (number < 0)
        {
            output1 = -1;
        }
        else if (number > 7)
        {
            output1 = -2;
        }
        else
        {
            int fact = 1;
            for (int i = 1; i <= number; i++)
            {
                fact = fact * i;
            }
            output1 = fact;
        }

        Console.WriteLine(output1);
    }
}
