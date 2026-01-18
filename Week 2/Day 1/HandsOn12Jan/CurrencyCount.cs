using System;

class CurrencyCount
{
    public static void Main()
    {
        int amount = 689;

        if (amount < 0)
        {
            Console.WriteLine("-1");
            return;
        }

        int totalNotes = 0;

        totalNotes += amount / 500;
        amount %= 500;

        totalNotes += amount / 100;
        amount %= 100;

        totalNotes += amount / 50;
        amount %= 50;

        totalNotes += amount / 10;
        amount %= 10;

        totalNotes += amount;

        Console.WriteLine("Output1 = " + totalNotes);
    }
}
