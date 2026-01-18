class Salary
{
    public static int Execute(int salary, int days)
    {
        if (salary > 9000) return -1;
        if (salary < 0) return -3;
        if (days < 0) return -4;

        int extra = (days == 31) ? 500 : 0;
        double expenses = salary * 0.7;

        return (int)(salary - expenses + extra);
    }
}
