#pragma checksum "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "686163da7fe3c5692616aea995b27d32a9779518"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_PartialRoomImages), @"mvc.1.0.view", @"/Views/Shared/PartialRoomImages.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using SkyKotApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using SkyKotApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using KotClassLibrary.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using KotClassLibrary.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using SkyKotApp.Data.Default;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using KotClassLibrary.Helpers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"686163da7fe3c5692616aea995b27d32a9779518", @"/Views/Shared/PartialRoomImages.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f720072f458c62116a9eb03d0c6290be0ae8f0a", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_PartialRoomImages : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ICollection<RoomImage>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
 if (Model.Any())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div>\r\n    <div id=\"carousel2\" class=\"carousel slide col-12\" data-ride=\"carousel\">\r\n        <div class=\"carousel-inner\">\r\n");
#nullable restore
#line 8 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
              var teller3 = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
             foreach (var image in Model)
            {
                var img = "/images/Room/" + (image.Path ?? "avatar.png");
                if (teller3 == 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div style=\"cursor:pointer;\" class=\"carousel-item active\" data-toggle=\"modal\" data-target=\"#Modal\">\r\n                        <img class=\"d-block\" style=\"max-height: 100vh\"");
            BeginWriteAttribute("src", " src=", 588, "", 597, 1);
#nullable restore
#line 15 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
WriteAttributeValue("", 593, img, 593, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-target=\"#carousel\" data-slide-to=");
#nullable restore
#line 15 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
                                                                                                                 Write(teller3);

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n                    </div>\r\n");
#nullable restore
#line 17 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div style=\"cursor:pointer;\" class=\"carousel-item\" data-toggle=\"modal\" data-target=\"#Modal\">\r\n                        <img class=\"d-block\" style=\"max-height: 100vh\"");
            BeginWriteAttribute("src", " src=", 919, "", 928, 1);
#nullable restore
#line 21 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
WriteAttributeValue("", 924, img, 924, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-target=\"#carousel\" data-slide-to=");
#nullable restore
#line 21 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
                                                                                                                 Write(teller3);

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n                    </div>\r\n");
#nullable restore
#line 23 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
                }
                teller3++;

            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </div>
        <a class=""carousel-control-prev"" href=""#carousel2"" role=""button"" data-slide=""prev"">
            <span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>
            <span class=""sr-only"">Previous</span>
        </a>
        <a class=""carousel-control-next"" href=""#carousel2"" role=""button"" data-slide=""next"">
            <span class=""carousel-control-next-icon"" aria-hidden=""true""></span>
            <span class=""sr-only"">Next</span>
        </a>
    </div>
    <div class=""col-12 d-none d-md-block d-lg-block d-xl-block"">
        <div class=""row"" id=""gallery"" data-toggle=""carousel2"" data-target=""#carousel2"">
");
#nullable restore
#line 39 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
              var teller = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
             foreach (var image in Model)
            {

                var img = "/images/Room/" + (image.Path ?? "avatar.png");


#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"m-2\">\r\n                    <img style=\"cursor:pointer; max-height:100px;\"");
            BeginWriteAttribute("src", " src=", 1997, "", 2006, 1);
#nullable restore
#line 46 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
WriteAttributeValue("", 2002, img, 2002, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-target=\"#carousel2\" data-slide-to=");
#nullable restore
#line 46 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
                                                                                                              Write(teller);

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n                </div>\r\n");
#nullable restore
#line 48 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"

                teller++;
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n");
#nullable restore
#line 54 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Shared\PartialRoomImages.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ICollection<RoomImage>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
