using System;

interface AC
{
    void PowerOn();
    void PowerOff();
    void CheckStatus();
    void ChangeMode(string mode);
    void ChangeTemperature(int temperature);
}

public enum ACmode
{
    Cooling,
    Heating,
    Fan,
    Dry
}

class AirConditioner : AC
{
    static int totalAirConditioners;

    private string brand_name;
    private int temperature;
    private ACmode mode;
    private bool isOn;

    static AirConditioner()
    {
        totalAirConditioners = 0;
    }

    public AirConditioner(string brand_name)
    {
        this.brand_name = brand_name;
        this.temperature = 25;
        this.mode = ACmode.Cooling;
        this.isOn = false;
        totalAirConditioners++;
    }

    public AirConditioner(AirConditioner newAc)
    {
        this.brand_name = newAc.brand_name;
        this.temperature = newAc.temperature;
        this.mode = newAc.mode;
        this.isOn = newAc.isOn;
        totalAirConditioners++;
    }

    public void PowerOn()
    {
        isOn = true;
        Console.WriteLine("AC " + brand_name + " powered ON\n");
    }

    public void PowerOff()
    {
        isOn = false;
        Console.WriteLine("AC " + brand_name + " powered OFF\n");
    }

    public void CheckStatus()
    {
        Console.WriteLine("AC Details:");
        Console.WriteLine("Brand: " + brand_name);
        Console.WriteLine("Power Status: " + isOn);
        Console.WriteLine("Temperature: " + temperature);
        Console.WriteLine("Mode: " + mode);
        Console.WriteLine();
    }

    public static void GetTotalAcInstalled()
    {
        Console.WriteLine("Total AirConditioners: " + totalAirConditioners + "\n");
    }

    public void ChangeMode(string mode)
    {
        if (Enum.TryParse(mode, true, out ACmode newMode))
        {
            this.mode = newMode;
            Console.WriteLine("Updated mode: " + this.mode);
        }
        else
        {
            Console.WriteLine("Mode could not be changed! Try again");
        }
        Console.WriteLine();
    }

    public void ChangeMode(ACmode mode)
    {
        this.mode = mode;
        Console.WriteLine("Updated mode: " + this.mode);
        Console.WriteLine();
    }

    public void ChangeTemperature(int temperature)
    {
        if (temperature < 15 || temperature > 30)
        {
            Console.WriteLine("Please enter a valid temperature!\n");
        }
        else
        {
            this.temperature = temperature;
            Console.WriteLine("Updated temperature: " + this.temperature + "\n");
        }
    }
}

partial class AirConditioner
{
    public string BrandName
    {
        get { return brand_name; }
        set { brand_name = value; }
    }

    public int Temperature
    {
        get { return temperature; }
        set
        {
            if (value >= 15 && value <= 30)
                temperature = value;
        }
    }

    public ACmode Mode
    {
        get { return mode; }
        set { mode = value; }
    }

    public bool IsOn
    {
        get { return isOn; }
        set { isOn = value; }
    }
}

sealed class ACServiceCenter
{
    public void ServiceAC(AirConditioner ac)
    {
        Console.WriteLine("Servicing AC of brand: " + ac.BrandName);
        ac.PowerOff();
        Console.WriteLine("Service completed.\n");
    }
}
