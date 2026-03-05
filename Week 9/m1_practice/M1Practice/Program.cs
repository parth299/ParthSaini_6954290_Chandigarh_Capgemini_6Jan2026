public class Program {
    public static void Main(string[] args) {
        RealEstateApp rea = new RealEstateApp();
        rea.AddListing(new RealEstateListing{ID=1, Title="Bunker", Description="toughed bunkers", Price= 10000, Location="Goregoan"});
        var list = rea.GetListings();

        foreach(var i in list) {
            Console.WriteLine(i);
        }
    }
}