public class CreditCardPayment: PaymentProcessor
{
    public CreditCardPayment(int paymentPrice): base(paymentPrice)
    {
        Console.WriteLine("CreditCardPayment ctor!");
    }
    public override void ProcessPayment()
    {
        Console.WriteLine("Processing credit card payment : " + paymentPrice);
    }
}