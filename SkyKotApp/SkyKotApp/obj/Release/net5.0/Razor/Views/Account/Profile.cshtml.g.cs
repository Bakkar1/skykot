#pragma checksum "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Account\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d471a805b73450786053d46892707ec3661c3303"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Profile), @"mvc.1.0.view", @"/Views/Account/Profile.cshtml")]
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
using KotClassLibrary.ViewModels.RenterRoomVM;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using SkyKotApp.Data.Default;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using KotClassLibrary.Helpers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d471a805b73450786053d46892707ec3661c3303", @"/Views/Account/Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b663632127d798b9031fd6db3064819e3692e33", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Account_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CustomUser>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Account\Profile.cshtml"
  
    ViewData["Title"] = "Profile";
    var photoPath = "/Images/Profile/"  + (Model.ProfileImage ?? "avatar.png");

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"d-flex justify-content-center p-2\" >\r\n    <div class=\"card\" style=\"width: 18rem;\">\r\n        <img class=\"card-img-top\"");
            BeginWriteAttribute("src", " src=\"", 273, "\"", 289, 1);
#nullable restore
#line 8 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Account\Profile.cshtml"
WriteAttributeValue("", 279, photoPath, 279, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Card image cap\">\r\n        <div class=\"card-body\">\r\n            <h5 class=\"card-title\">");
#nullable restore
#line 10 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Account\Profile.cshtml"
                              Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 10 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Account\Profile.cshtml"
                                               Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <p class=\"card-text\">");
#nullable restore
#line 11 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Account\Profile.cshtml"
                            Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <div>\r\n                <a class=\"btn btn-primary\">Edit</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CustomUser> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591