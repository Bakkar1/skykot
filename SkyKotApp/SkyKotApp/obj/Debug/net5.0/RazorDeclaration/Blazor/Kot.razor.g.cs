// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace SkyKotApp.Blazor
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
#nullable restore
#line 1 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using KotClassLibrary.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using KotClassLibrary.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using KotClassLibrary.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using SkyKotApp.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using SkyKotApp.Services.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using SkyKotApp.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\_Imports.razor"
using SkyKotApp.Data.Default;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
using Microsoft.Extensions.DependencyInjection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
    public partial class Kot : OwningComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 190 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
       
    public IBlazorRepository BlazorRepository => ScopedServices.GetService<IBlazorRepository>();
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public ICollection<Room> Rooms = new List<Room>();
    public ICollection<House> Houses = new List<House>();
    public ICollection<Room> SortedRooms = new List<Room>();
    public ICollection<ZipCode> ZipCodes = new List<ZipCode>();
    public bool isLoading { get; set; } = true;
    public string message { get; set; } = "Loading";

    public bool IsNormalUser { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.IsInRole(Roles.Admin) || user.IsInRole(Roles.Owner))
        {
            IsNormalUser = false;
        }

        Rooms = await BlazorRepository.GetRooms();
        Houses = await BlazorRepository.GetHouses();
        ZipCodes = await BlazorRepository.GetZipCodes();
        SortedRooms = Rooms;
        isLoading = false;
    }
    protected override void OnAfterRender(bool firstRender)
    {
        if (!SortedRooms.Any())
        {
            message = "No rooms";
        }
    }
    protected void SortRooms()
    {
        isLoading = true;
        var st = Rooms.Where(r => r.RoomId == 1).ToList();
        SortedRooms = st;
        isLoading = false;
    }
    protected  void FilterOnHouse(ChangeEventArgs e)
    {
        isLoading = true;
        int.TryParse(e.Value.ToString(), out int houseId);
        if(houseId == 0)
        {
            SortedRooms = Rooms;
        }
        else
        {
            SortedRooms = Rooms.Where(r => r.HouseId == houseId).ToList();
        }  
        isLoading = false;
    }
    protected  void FilterOnZipCode(ChangeEventArgs e)
    {
        isLoading = true;
        int.TryParse(e.Value.ToString(), out int zipId);
        if(zipId == 0)
        {
            SortedRooms = Rooms;
        }
        else
        {
            SortedRooms = Rooms.Where(r => r.House.ZipCodeId == zipId).ToList();
        }  
        isLoading = false;
    }
    // links
    public void EditRoom(int roomId)
    {
        NavigationManager.NavigateTo($"Room/edit/{roomId}", true);
    }
    public void DetailsRoom(int roomId)
    {
        NavigationManager.NavigateTo($"Room/Details/{roomId}", true);
    }
    public void DeleteRoom(int roomId)
    {
        NavigationManager.NavigateTo($"Room/Delete/{roomId}", true);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    }
}
#pragma warning restore 1591
