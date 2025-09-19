using Project.DTO;
using Project.Models;
using Project.Repositories;

namespace Project.Services
{
    public class HotelServ : IHotelServ
    {
        private readonly IHotel _hotel_context;

        public HotelServ(IHotel hotelRep)
        {
            _hotel_context = hotelRep;
        }

        public async Task<HotelReadDto> CreateAsync(HotelCreateDto dto)
        {
            var hotel = new Hotel
            {
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country,
                Rating = dto.Rating
            };

            await _hotel_context.AddAsync(hotel);
            await _hotel_context.SaveChangesAsync();

            return new HotelReadDto(
                hotel.Id,
                hotel.Name,
                hotel.Address,
                hotel.City,
                hotel.Country,
                hotel.Rating, 
                Enumerable.Empty<RoomReadDto>()
            );
        }

        public async Task<IEnumerable<HotelReadDto>> GetAllAsync()
        {
            var hotels = await _hotel_context.GetAllAsync();

            return hotels.Select(h => new HotelReadDto(
                h.Id,
                h.Name,
                h.Address,
                h.City,
                h.Country,
                h.Rating,
                h.Rooms.Select(r => new RoomReadDto(
                    r.Id,
                    r.HotelId,
                    r.RoomNumber,
                    r.Type,
                    r.Price,
                    r.IsAvailable
                ))
            ));
        }

        public async Task<HotelReadDto?> GetByIdAsync(int id)
        {
            var hotel = await _hotel_context.GetHotelWithRoomsAsync(id);
            if (hotel == null)
            {
                return null;
            }

            return new HotelReadDto(
                hotel.Id,
                hotel.Name,
                hotel.Address,
                hotel.City,
                hotel.Country,
                hotel.Rating,
                hotel.Rooms.Select(r => new RoomReadDto(
                    r.Id,
                    r.HotelId,
                    r.RoomNumber,
                    r.Type,
                    r.Price,
                    r.IsAvailable
                ))
            );
        }

        public async Task UpdateAsync(int id, HotelUpdateDto dto)
        {
            var hotel = await _hotel_context.GetByIdAsync(id);
            if (hotel == null)
            {
                throw new KeyNotFoundException($"Hotel with ID {id} not found.");
            }

            hotel.Name = dto.Name;
            hotel.Address = dto.Address;
            hotel.City = dto.City;
            hotel.Country = dto.Country;
            hotel.Rating = dto.Rating;

            _hotel_context.Update(hotel);
            await _hotel_context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hotel = await _hotel_context.GetByIdAsync(id);
            if (hotel == null)
            {
                throw new KeyNotFoundException($"Hotel with ID {id} not found.");
            }

            _hotel_context.Remove(hotel);
            await _hotel_context.SaveChangesAsync();
        }
    }
}
