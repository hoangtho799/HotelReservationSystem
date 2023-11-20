namespace HotelReservationSystem
{
    public class Hotel
    {
        private readonly HashSet<RoomType> roomTypes = new HashSet<RoomType>();

        public void AddRoomType(RoomType roomType)
        {
            roomTypes.Add(roomType);
        }

        public async Task<ReservationResult> FindCheapestReservation(int numberOfGuests)
        {
            // Initialize dynamic programming table
            Dictionary<int, List<List<RoomTypeEnum>>> dpTable = new Dictionary<int, List<List<RoomTypeEnum>>>();

            // Generate all possible combinations of room types
            List<List<RoomTypeEnum>> allCombinations = await GenerateRoomCombinations(numberOfGuests, dpTable);

            // Filter combinations that are valid and calculate their total prices
            List<ReservationResult> validReservations = allCombinations
                .Where(combination => combination.Sum(room => roomTypes.First(rt => rt.Type == room).Sleeps) == numberOfGuests)
                .Select(combination => new ReservationResult(combination, combination.Sum(room => roomTypes.First(rt => rt.Type == room).Price)))
                .ToList();

            // Return the cheapest valid reservation
            return validReservations.OrderBy(r => r.TotalPrice).FirstOrDefault();
        }

        private async Task<List<List<RoomTypeEnum>>> GenerateRoomCombinations(int numberOfGuests, Dictionary<int, List<List<RoomTypeEnum>>> dpTable)
        {
            if (dpTable.TryGetValue(numberOfGuests, out var result))
            {
                return result;
            }

            result = new List<List<RoomTypeEnum>>();
            int[] dpArray = new int[numberOfGuests + 1];
            dpArray[0] = 1;

            foreach (var roomType in roomTypes)
            {
                for (int i = roomType.Sleeps; i <= numberOfGuests; i++)
                {
                    dpArray[i] += dpArray[i - roomType.Sleeps];
                }
            }

            await GenerateCombinationsHelper(numberOfGuests, roomTypes.Select(rt => rt.Type).ToList(), new List<RoomTypeEnum>(), result);

            dpTable[numberOfGuests] = result;
            return result;
        }

        private async Task GenerateCombinationsHelper(int remainingGuests, List<RoomTypeEnum> roomTypes, List<RoomTypeEnum> currentCombination, List<List<RoomTypeEnum>> result)
        {
            if (remainingGuests == 0)
            {
                result.Add(new List<RoomTypeEnum>(currentCombination));
                return;
            }

            foreach (var roomType in roomTypes)
            {
                if (remainingGuests - this.roomTypes.First(rt => rt.Type == roomType).Sleeps >= 0)
                {
                    currentCombination.Add(roomType);
                    await GenerateCombinationsHelper(remainingGuests - this.roomTypes.First(rt => rt.Type == roomType).Sleeps, roomTypes, currentCombination, result);
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                }
            }
        }
    }
}
