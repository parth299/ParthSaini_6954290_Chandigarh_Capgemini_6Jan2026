public abstract class PaymentProcessor
{
    protected int paymentPrice;
    public abstract void ProcessPayment();

    public PaymentProcessor(int paymentPrice)
    {
        this.paymentPrice = paymentPrice;
    }

    public void LogTransaction()
    {
        Console.WriteLine("Logging transaction");
    }
}