namespace travel_agency_system.DTO;

public class AddRoomDto
{
    public int HotelId { get; set; }

    public string RoomType { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }
}