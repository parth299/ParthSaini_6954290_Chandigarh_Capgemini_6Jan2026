using System;

class DetectKeyPress
{
    public static void DetectKey()
    {
        Console.WriteLine("Press a key between 0 and 9:");

        int num = int.Parse(Console.ReadLine());

        switch (num)
        {
            case 0:
                Console.WriteLine("pressed 0");
                break;
            case 1:
                Console.WriteLine("pressed 1");
                break;
            case 2:
                Console.WriteLine("pressed 2");
                break;
            case 3:
                Console.WriteLine("pressed 3");
                break;
            case 4:
                Console.WriteLine("pressed 4");
                break;
            case 5:
                Console.WriteLine("pressed 5");
                break;
            case 6:
                Console.WriteLine("pressed 6");
                break;
            case 7:
                Console.WriteLine("pressed 7");
                break;
            case 8:
                Console.WriteLine("pressed 8");
                break;
            case 9:
                Console.WriteLine("pressed 9");
                break;
            default:
                Console.WriteLine("Invalid input. Please enter a number between 0 and 9.");
                break;
        }
    }
}
