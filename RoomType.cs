namespace HotelReservationSystem
{
    public class RoomType : IEquatable<RoomType>
    {
        public RoomTypeEnum Type { get; }
        public int Sleeps { get; }
        public int NumberOfRooms { get; }
        public int Price { get; }

        public RoomType(RoomTypeEnum type, int sleeps, int numberOfRooms, int price)
        {
            Type = type;
            Sleeps = sleeps;
            NumberOfRooms = numberOfRooms;
            Price = price;
        }

        public bool Equals(RoomType? other)
        {
            if (other == null) return false;
            return this.Type.Equals(other.Type);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}
