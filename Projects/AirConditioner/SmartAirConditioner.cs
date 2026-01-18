class SmartAirConditioner : AirConditioner
{
    private bool wifiEnabled;

    public SmartAirConditioner(string brand_name)
        : base(brand_name)
    {
        wifiEnabled = true;
    }

    public void ControlUsingMobileApp()
    {
        if (wifiEnabled)
            Console.WriteLine("AC is being controlled using mobile app.");
        else
            Console.WriteLine("WiFi not enabled.");

        Console.WriteLine("\n");
    }
}
