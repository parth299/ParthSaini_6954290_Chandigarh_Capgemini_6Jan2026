class ShipStory
{
    public static int[] Execute(int[] input1, int[] input2, int input3)
    {
        if (input3 < 0) return new int[] { -2 };

        int[] output = new int[input3];

        for (int i = 0; i < input3; i++)
        {
            if (input1[i] < 0 || input2[i] < 0)
                return new int[] { -1 };

            output[i] = input1[i] + input2[input3 - 1 - i];
        }
        return output;
    }
}
