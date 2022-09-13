using KotClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SkyKotApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Services.Api
{
    public class SkyApiRepository : ISkyApiRepository
    {
        private readonly AppDbContext _context;
        public SkyApiRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Room>> GetRooms(int count)
        {
            return await _context.Rooms
                .Include(r => r.House.ZipCode)
                .Include(r => r.RoomImages)
                .Where(r => r.IsAvailable)
                .OrderByDescending(r => r.RoomId)
                .Take(count)
                .ToListAsync();
        }
    }
}
