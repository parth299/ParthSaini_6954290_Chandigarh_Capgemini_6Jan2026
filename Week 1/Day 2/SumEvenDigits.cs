using System;

class EvenDigitsSum
{
    public static void SumEven()
    {
        int number = 2468;
        int output = 0;

        if (number < 0)
        {
            output = -1;
        }
        else if (number > 32767)
        {
            output = -2;
        }
        else
        {
            while (number > 0)
            {
                int digit = number % 10;
                if (digit % 2 == 0)
                    output += digit;
                number /= 10;
            }
        }

        Console.WriteLine(output);
    }
}
