namespace travel_agency_system.DTO;

public class RoomDto
{
    public int RoomId { get; set; }
    public string RoomType { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string HotelName { get; set; }
    public string HotelLocation { get; set; }
}