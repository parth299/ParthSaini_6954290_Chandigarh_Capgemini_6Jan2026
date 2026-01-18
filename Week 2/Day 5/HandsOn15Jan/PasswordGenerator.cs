class PasswordGenerator
{
    public static int Execute(int[] input1, int input3)
    {
        if (input3 < 0) return -2;

        int oddSum = 0, evenSum = 0;
        foreach (int x in input1)
        {
            if (x < 0) return -1;
            if (x % 2 == 0) evenSum += x;
            else oddSum += x;
        }

        return (oddSum + evenSum) / 2;
    }
}
