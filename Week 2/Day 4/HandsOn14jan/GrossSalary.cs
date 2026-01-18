class GrossSalaryCalculation
{
    public static int Execute(int basicPay, int workingDays)
    {
        if (basicPay < 0) return -1;
        if (basicPay > 10000) return -2;
        if (workingDays > 31) return -3;

        double da = basicPay * 0.75;
        double hra = basicPay * 0.50;

        return (int)(basicPay + da + hra);
    }
}
