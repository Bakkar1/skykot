using KotClassLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Data
{
    public class AppDbContext : IdentityDbContext<CustomUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
           base(options)
        {

        }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Expence> Expences { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<HouseSpecification> HouseSpecifications { get; set; }
        public DbSet<HouseExpense> HouseExpenses { get; set; }
        public DbSet<RenterRoom> RenterRooms { get; set; }
        public DbSet<RenterContract> RenterContracts { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomExpense> RoomExpenses { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<RoomSpecification> RoomSpecifications { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<UserHouse> UserHouses { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }
    }
}
