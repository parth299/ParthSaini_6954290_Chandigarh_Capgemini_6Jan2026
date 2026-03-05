using System;

class Car
{
    public int Mileage { get; set; }
    public int SeatingCapacity { get; set; }

    public Car(int mileage, int seatingCapacity)
    {
        Mileage = mileage;
        SeatingCapacity = seatingCapacity;
    }

    public virtual string Describe()
    {
        return "";
    }
}

class WagonR : Car
{
    public WagonR() : base(21, 4) { }

    public override string Describe()
    {
        return $"A WagonR is not scarce, it is a {SeatingCapacity} seater, and has a mileage of around {Mileage} kmpl.";
    }
}

class HondaCity : Car
{
    public HondaCity() : base(17, 5) { }

    public override string Describe()
    {
        return $"A HondaCity is not scarce, it is a {SeatingCapacity} seater, and has a mileage of around {Mileage} kmpl.";
    }
}

class InnovaCrysta : Car
{
    public InnovaCrysta() : base(15, 6) { }

    public override string Describe()
    {
        return $"An InnovaCrysta is not scarce, it is a {SeatingCapacity} seater, and has a mileage of around {Mileage} kmpl.";
    }
}

class Solution
{
    static void Main()
    {
        string carType = Console.ReadLine();

        Car car = null;

        if (carType == "WagonR")
            car = new WagonR();
        else if (carType == "HondaCity")
            car = new HondaCity();
        else if (carType == "InnovaCrysta")
            car = new InnovaCrysta();

        Console.WriteLine(car.Describe());
    }
}