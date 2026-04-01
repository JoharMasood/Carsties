namespace AuctionService.DTOs;

public class AuctionDto
{
     public Guid Id { get; set; }

    public int ReservePrice { get; set; } = 0;

    public string Seller { get; set; }

    public string Winner { get; set; }

    public int SoldAmount { get; set; }

    public DateTime CreatedAT { get; set ;}

    public DateTime UpdatedAT { get; set ;} = DateTime.UtcNow;
    public DateTime AuctionEnd { get; set ;}

    public String Status { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public int Year { get; set; }

    public string Color { get; set; }

    public int Mileage { get; set; }

    public string ImageUrl { get; set; }

    
}