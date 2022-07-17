using KotClassLibrary.Models;
using Moq;
using SkyKotApp.Services.Blazor;
using SkyKotApp.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyKotApp.Test;

public class BlazorRepositoryTest
{
    private readonly IBlazorRepository _blazorRepository;

    public BlazorRepositoryTest(IBlazorRepository blazorRepository)
    {
        this._blazorRepository = blazorRepository;
    }
    [Fact]
    public async void Test1()
    {
       ICollection<Room> customers = await _blazorRepository.GetRooms();

        var savedUser = Assert.Single(customers);

        Assert.NotNull(savedUser);
    }
}
