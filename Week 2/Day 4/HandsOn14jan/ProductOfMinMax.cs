class ProductOfMaxMin
{
    public static int Execute(int[] input1, int input3)
    {
        if (input3 < 0) return -2;

        foreach (int x in input1)
            if (x < 0) return -1;

        int max = input1[0], min = input1[0];
        foreach (int x in input1)
        {
            if (x > max) max = x;
            if (x < min) min = x;
        }
        return max * min;
    }
}
