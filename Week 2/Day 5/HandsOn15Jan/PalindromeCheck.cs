class PalindromeCheck
{
    public static void Execute(int input)
    {
        if (input < 0) {
            Console.WriteLine("please enter valid integer");
            return ;
        }

        int original = input, reverse = 0;
        while (input > 0)
        {
            reverse = reverse * 10 + input % 10;
            input /= 10;
        }

        int ans = (original == reverse) ? 1 : -2;
        if(ans == 1) {
            Console.WriteLine("It is a palindrome");
        }
        else {
            Console.WriteLine("Not a palindrome");
        }
    }
}
