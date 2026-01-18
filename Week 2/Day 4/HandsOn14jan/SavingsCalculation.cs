class SavingsCalculation
{
    public static int Execute(int input1, int input2)
    {
        if (input1 > 9000) return -1;
        if (input1 < 0) return -2;
        if (input2 < 0) return -4;

        int extra = (input2 == 31) ? 500 : 0;
        double expenses = input1 * 0.7;

        return (int)(input1 - expenses + extra);
    }
}
