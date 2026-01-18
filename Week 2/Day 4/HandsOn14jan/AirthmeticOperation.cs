class ArithmeticOperation
{
    public static int Execute(int input1, int input2, int input3)
    {
        if (input1 < 0 && input2 < 0) return -1;

        return input3 switch
        {
            1 => input1 + input2,
            2 => input1 - input2,
            3 => input1 * input2,
            4 => input1 / input2,
            _ => 0
        };
    }
}
