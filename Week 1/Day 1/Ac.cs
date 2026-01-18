using System;

class Ac {
    private int temperature;
    private bool isOn;
    string mode;
    
    public Ac(int temperature) {
        this.temperature = temperature;
        this.isOn = false;
        this.mode = "cooling";
    }
    
    public void TurnOn() {
        this.isOn = true;
        Console.WriteLine("AC is turned ON");
    }
    
    public void TurnOff() {
        this.isOn = false;
        Console.WriteLine("AC is turned OFF");
    }
    
    public void CheckStatus() {
        Console.WriteLine("AC Status: ");
        Console.WriteLine("Temperature: " + this.temperature);
        Console.WriteLine("Mode: " + this.mode);
        if(this.isOn) {
            Console.WriteLine("Power: ON");
        }
        else {
            Console.WriteLine("Power: OFF");
        }
    }
    
    public void ChangeTemperature(int temperature) {
        this.temperature = temperature;
        Console.WriteLine("temperature changed! current temperature: " + this.temperature);
    }
    
    public void ChangeMode(string mode) {
        this.mode = mode;
        Console.WriteLine("Mode changed to : " + this.mode);
    }
}