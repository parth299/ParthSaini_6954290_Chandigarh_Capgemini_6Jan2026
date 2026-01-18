class SecondLargestElement
{
    public static int Execute(int[] input1, int input3)
    {
        if (input3 < 0) return -2;

        foreach (int x in input1)
            if (x < 0) return -1;

        Array.Sort(input1);
        return input1[input3 - 2];
    }
}
