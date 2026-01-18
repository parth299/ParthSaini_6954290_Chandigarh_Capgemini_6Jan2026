class SortAndInsert
{
    public static int[] Execute(int[] input1, int input3, int element)
    {
        if (input3 < 0) return new int[] { -2 };

        foreach (int x in input1)
            if (x < 0) return new int[] { -1 };

        Array.Sort(input1);

        int[] result = new int[input3 + 1];
        int i = 0, j = 0;

        while (i < input3 && input1[i] < element)
            result[j++] = input1[i++];

        result[j++] = element;

        while (i < input3)
            result[j++] = input1[i++];

        return result;
    }
}
