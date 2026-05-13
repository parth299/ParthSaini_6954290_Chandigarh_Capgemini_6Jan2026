public class UpiPayment: PaymentProcessor
{
    public UpiPayment(int paymentPrice): base(paymentPrice)
    {
        Console.WriteLine("UpiPayment Ctor!");
    }
    public override void ProcessPayment()
    {
        Console.WriteLine("Processing UPI payment : " + paymentPrice);
    }
}