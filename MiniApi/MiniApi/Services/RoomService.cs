using Project.DTO;
using Project.Models;
using Project.Repositories;

namespace Project.Services
{
    public class RoomService : IRoomServ
    {
        private readonly IRoom _room_context;
        private readonly IHotel _hotel_context;

        public RoomService(IRoom roomRepository, IHotel hotelRepository)
        {
            _room_context = roomRepository;
            _hotel_context = hotelRepository;
        }

        public async Task<Room> CreateAsync(RoomCreateDto dto)
        {
            var hotel = await _hotel_context.GetByIdAsync(dto.HotelId);
            if (hotel == null)
            {
                throw new Exception("Hotel not found");
            }

            var room = new Room
            {
                HotelId = dto.HotelId,
                RoomNumber = dto.RoomNumber,
                Price = dto.Price,
                Type = dto.Type,
                IsAvailable = true
            };

            await _room_context.AddAsync(room);
            await _room_context.SaveChangesAsync();

            return room;
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _room_context.GetByIdAsync(id);
            if (room == null)
            {
                throw new Exception("Room not found");
            }

            _room_context.Remove(room);
            await _room_context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _room_context.GetAllAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _room_context.GetRoomWithHotelAsync(id);
        }

        public async Task<IEnumerable<Room>> GetByHotelIdAsync(int hotelId)
        {
            return await _room_context.GetRoomsByHotelIdAsync(hotelId);
        }

        public async Task UpdateAsync(int id, RoomUpdateDto dto)
        {
            var room = await _room_context.GetByIdAsync(id);
            if (room == null)
            {
                throw new Exception("Room not found");
            }

            var hotel = await _hotel_context.GetByIdAsync(dto.HotelId);
            if (hotel == null) {
                throw new Exception("Hotel not found");
            }

            room.HotelId = dto.HotelId;
            room.RoomNumber = dto.RoomNumber;
            room.Type = dto.Type;
            room.Price = dto.Price;
            room.IsAvailable = dto.IsAvailable;

            _room_context.Update(room);
            await _room_context.SaveChangesAsync();
        }
    }
}
