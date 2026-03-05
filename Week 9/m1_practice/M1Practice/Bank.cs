class BankOperations : IBankAccountOperation
{
    private decimal balance = 0;

    public void Deposit(decimal d)
    {
        balance += d;
    }

    public void Withdraw(decimal d)
    {
        balance -= d;
    }

    public decimal ProcessOperation(string message)
    {
        string msg = message.ToLower();

        decimal value = 0;
        var match = System.Text.RegularExpressions.Regex.Match(msg, @"\d+");
        if (match.Success)
        {
            value = Convert.ToDecimal(match.Value);
        }

        if (msg.Contains("withdraw") || msg.Contains("pull"))
        {
            Withdraw(value);
        }
        else if (msg.Contains("deposit") || msg.Contains("invest") || msg.Contains("transfer") || msg.Contains("see money"))
        {
            Deposit(value);
        }
        else if (msg.Contains("balance"))
        {
            return balance;
        }

        return balance;
    }
}