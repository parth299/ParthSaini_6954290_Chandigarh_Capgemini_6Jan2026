class FinanceEmployee : Employee
{
    public FinanceEmployee(string name, string location)
        : base("Finance", name, location)
    {
    }

    public override string GetDepartment()
    {
        return department;
    }

    public override string GetName()
    {
        return name;
    }

    public override string GetLocation()
    {
        return location;
    }

    public override bool GetStatus()
    {
        return isOnVacation;
    }

    public override void SwitchStatus()
    {
        isOnVacation = !isOnVacation;
    }
}

class MarketingEmployee : Employee
{
    public MarketingEmployee(string name, string location)
        : base("Marketing", name, location)
    {
    }

    public override string GetDepartment()
    {
        return department;
    }

    public override string GetName()
    {
        return name;
    }

    public override string GetLocation()
    {
        return location;
    }

    public override bool GetStatus()
    {
        return isOnVacation;
    }

    public override void SwitchStatus()
    {
        isOnVacation = !isOnVacation;
    }
}