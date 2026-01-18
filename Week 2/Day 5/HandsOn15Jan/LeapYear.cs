class LeapYear
{
    public static void Execute(int year)
    {
        if (year < 0) {
            Console.WriteLine("Please eneter a valid year");
            return ;
        } 

        int isLeapYear = (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0)) ? 1 : 0;
        if(isLeapYear == 1) {
            Console.WriteLine("Year: " + year + " is a leap year");
        }
        else {
            Console.WriteLine("Year is not a leap year");
        }
    }
}
