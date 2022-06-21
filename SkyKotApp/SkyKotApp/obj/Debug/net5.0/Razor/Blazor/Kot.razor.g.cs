#pragma checksum "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "22dc29c8f53e1d7ee01bf80f9d4b2f6b6d87bc52"
// <auto-generated/>
#pragma warning disable 1591
namespace SkyKotApp.Blazor
{
    #line hidden
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
#line 1 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
using Microsoft.Extensions.DependencyInjection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
using System.IO;

#line default
#line hidden
#nullable disable
    public partial class Kot : OwningComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>Rooms</h3>");
#nullable restore
#line 7 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
 if (isLoading)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(1, "<div class=\"custom-spinner show-spinner\"><div class=\"spinner-border text-primary\" role=\"status\"><span class=\"sr-only\">Loading...</span></div></div>");
#nullable restore
#line 14 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
}

#line default
#line hidden
#nullable disable
            __builder.OpenElement(2, "div");
            __builder.OpenElement(3, "select");
            __builder.AddAttribute(4, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 16 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                       FilterOnHouse

#line default
#line hidden
#nullable disable
            ));
            __builder.OpenElement(5, "option");
            __builder.AddAttribute(6, "value", "0");
            __builder.AddContent(7, "Filter");
            __builder.CloseElement();
#nullable restore
#line 18 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
         if(Houses.Any())
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
             foreach (var house in Houses)
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(8, "option");
            __builder.AddAttribute(9, "value", 
#nullable restore
#line 22 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                house.HouseId

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 22 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
__builder.AddContent(10, house.Name);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 23 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
             
        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(11, "\r\n\r\n     ");
            __builder.OpenElement(12, "select");
            __builder.AddAttribute(13, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 27 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                        FilterOnZipCode

#line default
#line hidden
#nullable disable
            ));
            __builder.OpenElement(14, "option");
            __builder.AddAttribute(15, "value", "0");
            __builder.AddContent(16, "All Zip");
            __builder.CloseElement();
#nullable restore
#line 29 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
         if(Houses.Any())
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
             foreach (var zipCode in ZipCodes)
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(17, "option");
            __builder.AddAttribute(18, "value", 
#nullable restore
#line 33 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                zipCode.ZipCodeId

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 33 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
__builder.AddContent(19, zipCode.City);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 34 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
             
        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n");
            __builder.OpenElement(21, "div");
            __builder.AddAttribute(22, "class", "custom-container");
#nullable restore
#line 39 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
     if (SortedRooms.Any())
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
         foreach (Room room in SortedRooms)
        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(23, "div");
            __builder.AddAttribute(24, "class", "card");
            __builder.AddAttribute(25, "style", "width: 18rem;");
            __builder.OpenElement(26, "div");
            __builder.AddAttribute(27, "class", "card-img-top");
#nullable restore
#line 45 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                   if (room.RoomImages.Any())
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(28, "div");
#nullable restore
#line 48 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                              var carouselId = "carousel-" + room.RoomId;

#line default
#line hidden
#nullable disable
            __builder.OpenElement(29, "div");
            __builder.AddAttribute(30, "id", 
#nullable restore
#line 49 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                      carouselId

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(31, "class", "carousel slide col-12 p-0");
            __builder.AddAttribute(32, "data-ride", "carousel");
            __builder.OpenElement(33, "div");
            __builder.AddAttribute(34, "class", "carousel-inner");
#nullable restore
#line 51 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                      var teller3 = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                     foreach (var image in room.RoomImages)
                                    {
                                        var img = "/images/Room/" + (image.Path ?? "avatar.png");
                                        if (teller3 == 0)
                                        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(35, "div");
            __builder.AddAttribute(36, "class", "carousel-item active");
            __builder.AddAttribute(37, "style", "cursor:pointer;" + " height:" + " 30vh;" + " background-image" + " :" + " url(\'" + (
#nullable restore
#line 57 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                                                                                                                            img

#line default
#line hidden
#nullable disable
            ) + "\');background-position:" + " center;background-size:" + " cover;");
            __builder.AddAttribute(38, "data-toggle", "modal");
            __builder.AddAttribute(39, "data-target", "#Modal");
            __builder.CloseElement();
#nullable restore
#line 59 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(40, "div");
            __builder.AddAttribute(41, "class", "carousel-item");
            __builder.AddAttribute(42, "style", "cursor:pointer;" + " height:" + " 30vh;" + " background-image" + " :" + " url(\'" + (
#nullable restore
#line 62 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                                                                                                                      img

#line default
#line hidden
#nullable disable
            ) + "\');background-position:" + " center;background-size:" + " cover;");
            __builder.AddAttribute(43, "data-toggle", "modal");
            __builder.AddAttribute(44, "data-target", "#Modal");
            __builder.CloseElement();
#nullable restore
#line 64 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                        }
                                        teller3++;

                                    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(45, "\r\n                                ");
            __builder.OpenElement(46, "a");
            __builder.AddAttribute(47, "class", "carousel-control-prev");
            __builder.AddAttribute(48, "href", "#" + (
#nullable restore
#line 69 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                                                         carouselId

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(49, "role", "button");
            __builder.AddAttribute(50, "data-slide", "prev");
            __builder.AddMarkupContent(51, "<span class=\"carousel-control-prev-icon\" aria-hidden=\"true\"></span>\r\n                                    ");
            __builder.AddMarkupContent(52, "<span class=\"sr-only\">Previous</span>");
            __builder.CloseElement();
            __builder.AddMarkupContent(53, "\r\n                                ");
            __builder.OpenElement(54, "a");
            __builder.AddAttribute(55, "class", "carousel-control-next");
            __builder.AddAttribute(56, "href", "#" + (
#nullable restore
#line 73 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                                                                         carouselId

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(57, "role", "button");
            __builder.AddAttribute(58, "data-slide", "next");
            __builder.AddMarkupContent(59, "<span class=\"carousel-control-next-icon\" aria-hidden=\"true\"></span>\r\n                                    ");
            __builder.AddMarkupContent(60, "<span class=\"sr-only\">Next</span>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 79 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(61, "<div style=\"cursor:pointer; height: 20vh\"><img class=\"d-block\" style=\"max-height: 100%; width:100%\" src=\"/images/Profile/avatar.png\"></div>");
#nullable restore
#line 85 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
                    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(62, "\r\n\r\n              ");
            __builder.OpenElement(63, "div");
            __builder.AddAttribute(64, "class", "card-body");
            __builder.OpenElement(65, "h5");
            __builder.AddAttribute(66, "class", "card-title m2");
#nullable restore
#line 89 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
__builder.AddContent(67, room.House.Name);

#line default
#line hidden
#nullable disable
            __builder.AddContent(68, " Price");
            __builder.CloseElement();
            __builder.AddMarkupContent(69, "\r\n                ");
            __builder.AddMarkupContent(70, "<p class=\"card-text\">City </p>");
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 93 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 93 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
         
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(71, "<p>No Rooms</p>");
#nullable restore
#line 98 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 101 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Blazor\Kot.razor"
       
    public IBlazorRepository BlazorRepository => ScopedServices.GetService<IBlazorRepository>();
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public ICollection<Room> Rooms = new List<Room>();
    public ICollection<House> Houses = new List<House>();
    public ICollection<Room> SortedRooms = new List<Room>();
    public ICollection<ZipCode> ZipCodes = new List<ZipCode>();
    public bool isLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        Rooms = await BlazorRepository.GetRooms();
        Houses = await BlazorRepository.GetHouses();
        ZipCodes = await BlazorRepository.GetZipCodes();
        SortedRooms = Rooms;
        isLoading = false;
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

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
