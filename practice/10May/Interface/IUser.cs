public interface IUser
{
    int Age{get;set;}

    static string AppName = "UserApp";

    public void Log(string message)
    {
        Console.WriteLine("Hello");
    }

    private void Validate()
    {
        Console.WriteLine("Working on validation");
    }
}