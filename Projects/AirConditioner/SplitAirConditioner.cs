class SplitAirConditioner : AirConditioner
{
    private int energyRating;

    public SplitAirConditioner(string brand_name, int energyRating)
        : base(brand_name)
    {
        this.energyRating = energyRating;
    }

    public void ShowEnergyRating()
    {
        Console.WriteLine("Energy Rating: " + energyRating + " Star");
        Console.WriteLine("\n");
    }
}
