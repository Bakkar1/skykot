using KotClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyKotApp.Services.General
{
    public partial class SkyKotPartialRepository
    {
        public async Task<ICollection<AcademicYear>> GetAcademicYearsAsync()
        {
            return await context.AcademicYears.ToListAsync();
        }
    }
}
