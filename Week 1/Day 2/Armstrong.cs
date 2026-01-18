using System;

class Armstrong
{
    public static void FindArmstrong()
    {
        int number = 153;
        int output1 = 0;

        if (number < 0)
        {
            output1 = -1;
        }
        else if (number > 999)
        {
            output1 = -2;
        }
        else
        {
            int temp = number;
            int sum = 0;

            while (temp > 0)
            {
                int digit = temp % 10;
                sum = sum + (digit * digit * digit);
                temp = temp / 10;
            }

            if (sum == number)
                output1 = 1;
            else
                output1 = 0;
        }

        if(output1 == 1) {
            Console.WriteLine("Number is armstrong number");
        }
        else {
            Console.WriteLine("Number is not armstrong number");
        }
    }
}
