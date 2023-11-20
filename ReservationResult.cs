namespace HotelReservationSystem
{
    public class ReservationResult
    {
        public List<RoomTypeEnum> RoomTypes { get; }
        public int TotalPrice { get; }

        public ReservationResult(List<RoomTypeEnum> roomTypes, int totalPrice)
        {
            RoomTypes = roomTypes;
            TotalPrice = totalPrice;
        }
    }
}
