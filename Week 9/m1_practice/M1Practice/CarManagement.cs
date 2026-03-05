using System;
using System.Collections.Generic;
using System.Linq;

class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }

    public Car(string brand, string model, int year, double price)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
    }

    public void Display()
    {
        Console.WriteLine($"{Brand} {Model} {Year} {Price}");
    }
}

class Solution
{
    static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        List<Car> cars = new List<Car>();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split(' ');
            string brand = input[0];
            string model = input[1];
            int year = Convert.ToInt32(input[2]);
            double price = Convert.ToDouble(input[3]);

            cars.Add(new Car(brand, model, year, price));
        }

        foreach (var car in cars)
        {
            car.Display();
        }

        var mostExpensive = cars.OrderByDescending(c => c.Price).First();

        Console.WriteLine("Most Expensive Car:");
        mostExpensive.Display();
    }
}