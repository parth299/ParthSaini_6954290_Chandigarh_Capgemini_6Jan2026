public class Book : IBook
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
}

public class LibrarySystem : ILibrarySystem
{
    private Dictionary<int, (IBook book, int quantity)> books = new Dictionary<int, (IBook, int)>();

    public void AddBook(IBook book, int quantity)
    {
        if (books.ContainsKey(book.Id))
        {
            var existing = books[book.Id];
            books[book.Id] = (existing.book, existing.quantity + quantity);
        }
        else
        {
            books[book.Id] = (book, quantity);
        }
    }

    public void RemoveBook(IBook book, int quantity)
    {
        if (books.ContainsKey(book.Id))
        {
            var existing = books[book.Id];

            int newQty = existing.quantity - quantity;

            if (newQty <= 0)
                books.Remove(book.Id);
            else
                books[book.Id] = (existing.book, newQty);
        }
    }

    public int CalculateTotal()
    {
        int total = 0;

        foreach (var item in books.Values)
        {
            total += item.book.Price * item.quantity;
        }

        return total;
    }

    public List<(string, int)> CategoryTotalPrice()
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        foreach (var item in books.Values)
        {
            string category = item.book.Category;
            int price = item.book.Price * item.quantity;

            if (!result.ContainsKey(category))
                result[category] = 0;

            result[category] += price;
        }

        return result.Select(x => (x.Key, x.Value)).ToList();
    }

    public List<(string, int, int)> BooksInfo()
    {
        return books.Values
            .Select(x => (x.book.Title, x.quantity, x.book.Price))
            .ToList();
    }

    public List<(string, string, int)> CategoryAndAuthorWithCount()
    {
        Dictionary<(string, string), int> result = new Dictionary<(string, string), int>();

        foreach (var item in books.Values)
        {
            var key = (item.book.Category, item.book.Author);

            if (!result.ContainsKey(key))
                result[key] = 0;

            result[key] += item.quantity;
        }

        return result.Select(x => (x.Key.Item1, x.Key.Item2, x.Value)).ToList();
    }
}