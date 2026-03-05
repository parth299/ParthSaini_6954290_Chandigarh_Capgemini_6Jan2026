using System;

public enum Gender
{
    Male,
    Female,
    Other
}

public abstract class UserBase
{
    protected string type;
    protected string name;
    protected Gender gender;
    protected int age;

    public UserBase(string type, string name, Gender gender, int age)
    {
        this.type = type;
        this.name = name;
        this.gender = gender;
        this.age = age;
    }

    public string GetUserType()
    {
        return type;
    }

    public string GetUserName()
    {
        return name;
    }

    public Gender GetUserGender()
    {
        return gender;
    }

    public int GetUserAge()
    {
        return age;
    }
}

public class Admin : UserBase
{
    public Admin(string name, Gender gender, int age)
        : base("Admin", name, gender, age)
    {
    }
}

public class User : UserBase
{
    public User(string name, Gender gender, int age)
        : base("User", name, gender, age)
    {
    }
}

public class Moderator : UserBase
{
    public Moderator(string name, Gender gender, int age)
        : base("Moderator", name, gender, age)
    {
    }
}

class Solution
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split(' ');
        string type = input[0];
        string name = input[1];
        Gender gender = (Gender)Enum.Parse(typeof(Gender), input[2]);
        int age = Convert.ToInt32(input[3]);

        UserBase user = null;

        if (type == "Admin")
            user = new Admin(name, gender, age);
        else if (type == "User")
            user = new User(name, gender, age);
        else
            user = new Moderator(name, gender, age);

        Console.WriteLine($"Type of user: {user.GetUserType()}");
        Console.WriteLine($"Name of user: {user.GetUserName()}");
        Console.WriteLine($"Gender of user: {user.GetUserGender()}");
        Console.WriteLine($"Age of user: {user.GetUserAge()}");
    }
}