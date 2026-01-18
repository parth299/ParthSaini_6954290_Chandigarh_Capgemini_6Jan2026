using System;

class Digits
{
    public static void CountDigits()
    {
        int number = 345;
        int output1 = 0;

        if (number < 0)
        {
            output1 = -1;
        }
        else
        {
            int count = 0;
            do
            {
                count++;
                number = number / 10;
            } while (number > 0);

            output1 = count;
        }

        Console.WriteLine(output1);
    }
}
