using System;
using System.Collections.Generic;
using System.Linq;

public class RealEstateListing
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string Location { get; set; }
}

public class RealEstateApp
{
    private List<RealEstateListing> listings = new List<RealEstateListing>();

    public void AddListing(RealEstateListing listing)
    {
        listings.Add(listing);
    }

    public void RemoveListing(int listingID)
    {
        var listing = listings.FirstOrDefault(l => l.ID == listingID);
        if (listing != null)
        {
            listings.Remove(listing);
        }
    }

    public void UpdateListing(RealEstateListing listing)
    {
        var existingListing = listings.FirstOrDefault(l => l.ID == listing.ID);

        if (existingListing != null)
        {
            existingListing.Title = listing.Title;
            existingListing.Description = listing.Description;
            existingListing.Price = listing.Price;
            existingListing.Location = listing.Location;
        }
    }

    public List<RealEstateListing> GetListings()
    {
        return listings;
    }

    public List<RealEstateListing> GetListingsByLocation(string location)
    {
        return listings
            .Where(l => l.Location.Equals(location, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<RealEstateListing> GetListingsByPriceRange(int minPrice, int maxPrice)
    {
        return listings
            .Where(l => l.Price >= minPrice && l.Price <= maxPrice)
            .ToList();
    }
}