namespace HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            // Initialize the hotel with room types and prices
            Hotel hotel = new Hotel();
            hotel.AddRoomType(new RoomType(RoomTypeEnum.Single, 1, 2, 30));
            hotel.AddRoomType(new RoomType(RoomTypeEnum.Double, 2, 3, 50));
            hotel.AddRoomType(new RoomType(RoomTypeEnum.Family, 4, 1, 85));

            // Get reservation request from the user
            Console.Write("Enter the number of guests: ");
            bool inputValid = int.TryParse(Console.ReadLine(), out int numberOfGuests);
            Console.WriteLine("Processing...");

            while (!inputValid)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write("Enter the number of guests: ");
                inputValid = int.TryParse(Console.ReadLine(), out numberOfGuests);
            }

            // Find the cheapest combination of rooms to fulfill the reservation
            ReservationResult reservationResult = hotel.FindCheapestReservation(numberOfGuests).GetAwaiter().GetResult();

            // Display the result
            if (reservationResult != null)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"Number of guest(s): {numberOfGuests}");
                Console.WriteLine($"Reservation: {string.Join(" ", reservationResult.RoomTypes)} - Total Price: ${reservationResult.TotalPrice}");
            }
            else
            {
                Console.WriteLine("No option");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
