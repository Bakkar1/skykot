using KotClassLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyKotApp.Services.Api
{
    public interface ISkyApiRepository
    {
        Task<ICollection<Room>> GetRooms(int count);
    }
}
