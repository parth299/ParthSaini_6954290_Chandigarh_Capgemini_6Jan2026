using System;

class Computer
{
    protected string brand;
    protected int ramSize;
    protected bool isSSD;

    public Computer(string brand, int ramSize)
    {
        this.brand = brand;
        this.ramSize = ramSize;
        this.isSSD = false;
    }

    public string GetBrand()
    {
        return brand;
    }

    public int GetRamSize()
    {
        return ramSize;
    }

    public bool GetIsSSD()
    {
        return isSSD;
    }

    public void SetIsSSD(bool value)
    {
        isSSD = value;
    }

    public virtual string GetComputerType()
    {
        return "Computer";
    }

    public virtual string GetComputerStatus()
    {
        return isSSD ? "SSD installed" : "SSD not installed";
    }
}

class PersonalComputer : Computer
{
    public PersonalComputer(string brand, int ramSize) : base(brand, ramSize)
    {
    }

    public override string GetComputerType()
    {
        return "Personal Computer";
    }

    public override string GetComputerStatus()
    {
        string ssdStatus = isSSD ? "with SSD" : "without SSD";
        return $"{brand} {ramSize}GB RAM {ssdStatus}";
    }
}

class Solution
{
    static void Main()
    {
        string brand = Console.ReadLine();
        int ram = Convert.ToInt32(Console.ReadLine());
        bool ssd = Convert.ToBoolean(Console.ReadLine());

        PersonalComputer pc = new PersonalComputer(brand, ram);
        pc.SetIsSSD(ssd);

        Console.WriteLine(pc.GetComputerType());
        Console.WriteLine(pc.GetComputerStatus());
    }
}